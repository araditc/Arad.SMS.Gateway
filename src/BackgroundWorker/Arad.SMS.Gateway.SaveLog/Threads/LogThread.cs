// --------------------------------------------------------------------
// Copyright (c) 2005-2020 Arad ITC.
//
// Author : Ammar Heidari <ammar@arad-itc.org>
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------

using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.ManageThread;
using Arad.SMS.Gateway.SqlLibrary;
using Arad.SMS.Gateway.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Messaging;
using System.Threading;
using System.Xml.Linq;

namespace SaveLog
{
	public class LogThread : WorkerThread
	{
		private Thread synchronizerParserThread;
		private static string queueName;
		private static int capacity;

		public LogThread(int timeOut)
			: base(timeOut)
		{
			this.ThreadException += new ThreadExceptionEventHandler(LogThread_ThreadException);
			queueName = ConfigurationManager.GetSetting("QueueName");
			capacity = Helper.GetInt(ConfigurationManager.GetAttributeValue("QueueName", "Capacity"), 1);
		}

		protected override void WorkerThreadFunction()
		{
			if (this.IsStopSignaled)
				return;

			if (synchronizerParserThread == null || !synchronizerParserThread.IsAlive)
			{
				synchronizerParserThread = new Thread(new ThreadStart(SynchronizationLog));

				synchronizerParserThread.Start();
			}
		}

		private void SynchronizationLog()
		{
			try
			{
				MessageQueue queue;
				var messages = new List<Message>();
				string queuePath = string.Empty;

				queuePath = string.Format(@".\private$\{0}", queueName);

				int counter = 0;

				if (MessageQueue.Exists(queuePath))
				{
					queue = new MessageQueue(queuePath);
					queue.Formatter = new BinaryMessageFormatter();

					var msgEnumerator = queue.GetMessageEnumerator2();
					while (msgEnumerator.MoveNext(new TimeSpan(0, 0, 1)))
					{
						using (var msg = msgEnumerator.Current)
						{
							messages.Add(msg);
							counter++;
							if (counter == capacity)
								break;
						}
					}
				}

				if (messages.Count > 0)
					Save(messages);
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("\r\n WorkerThreadFunction(SaveLog) {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("\r\n WorkerThreadFunction(SaveLog) {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void Save(List<Message> messages)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("\r\n Save Count : {0} ---", messages.Count));
			foreach (Message msg in messages)
			{
				Log log = (Log)msg.Body;

				XDocument doc;

				string rootDirectory = string.Format("{0}{1}{2}{1}{3}",
																							ConfigurationManager.GetSetting("LogFilePath"),
																							Path.DirectorySeparatorChar,
																							string.Format("{0}", DateTime.Now.ToString("yyyyMMdd")),
																							(LogType)log.Type);

				if (!Directory.Exists(rootDirectory))
					Directory.CreateDirectory(rootDirectory);

				string filePath = string.Format("{0}{1}log.xml", rootDirectory, Path.DirectorySeparatorChar);

				if (!File.Exists(filePath))
				{
					doc = new XDocument(
										new XDeclaration("1.0", "utf-8", "yes"),
											new XElement("Logs"));
					doc.Save(filePath);
				}

				doc = XDocument.Load(filePath);

				doc.Element("Logs").Add(
											new XElement("Log", new XAttribute("Source", log.Source != null ? log.Source : string.Empty),
																					new XAttribute("IP", log.IPAddress != null ? log.IPAddress : string.Empty),
																					new XAttribute("Browser", log.Browser != null ? log.Browser : string.Empty),
																					new XAttribute("CreateDate", log.CreateDate != null ? log.CreateDate : DateTime.MinValue),
											new XElement("Name", log.Name != null ? log.Name : string.Empty),
											new XElement("Text", log.Text != null ? log.Text : string.Empty),
											new XElement("ReferenceGuid", log.ReferenceGuid != null ? log.ReferenceGuid : Guid.Empty),
											new XElement("UserGuid", log.UserGuid != null ? log.UserGuid : Guid.Empty)));

				doc.Save(filePath);

				ReceiveMessageFromQueue(msg.Id);
			};
		}

		private void ReceiveMessageFromQueue(string messageId)
		{
			string queuePath = string.Format(@".\private$\{0}", queueName);
			if (MessageQueue.Exists(queuePath))
			{
				MessageQueue queue = new MessageQueue(queuePath);
				queue.ReceiveById(messageId);
			}
		}

		private void LogThread_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
