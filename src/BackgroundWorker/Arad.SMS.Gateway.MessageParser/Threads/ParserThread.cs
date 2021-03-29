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
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Messaging;
using System.Threading;
using System.Xml.Linq;

namespace Arad.SMS.Gateway.MessageParser
{
	public class ParserThread : WorkerThread
	{
		private Thread synchronizerParserThread;
		private static string queueName;
		private static int capacity;

		public ParserThread(int timeOut)
			: base(timeOut)
		{
			this.ThreadException += new System.Threading.ThreadExceptionEventHandler(ParserThread_ThreadException);
			queueName = ConfigurationManager.GetSetting("QueueName");
			capacity = Helper.GetInt(ConfigurationManager.GetAttributeValue("QueueName", "Capacity"), 1);
		}

		protected override void WorkerThreadFunction()
		{
			if (this.IsStopSignaled)
				return;

			if (synchronizerParserThread == null || !synchronizerParserThread.IsAlive)
			{
				synchronizerParserThread = new Thread(new ThreadStart(SynchronizationParser));

				synchronizerParserThread.Start();
			}
		}

		private void SynchronizationParser()
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
					Analyze(messages);
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SmsParser, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SmsParser, string.Format("\r\nWorkerThreadFunction(SmsParser) {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SmsParser, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void Analyze(List<Message> messages)
		{
			foreach (Message message in messages)
			{
				try
				{
					ReceiveMessage receiveMessage = (ReceiveMessage)message.Body;
                    
					receiveMessage.Receiver = Helper.GetLocalPrivateNumber(receiveMessage.Receiver);
					receiveMessage.Sender = Helper.GetLocalMobileNumber(receiveMessage.Sender);


					DataTable dtSmsInfo = Facade.Inbox.InsertReceiveMessage(receiveMessage);

					if (dtSmsInfo.Rows.Count > 0)
					{
						foreach (DataRow row in dtSmsInfo.Rows)
						{
							LogController<ServiceLogs>.LogInFile(ServiceLogs.SmsParser,
																									 string.Format(@"On ParseReceivedMessage{6}SmsGuid : {0},{6}NumberGuid : {1},{6}SmsTrafficRelayGuid : {2},{6}Sender :{3},{6}Receiver : {4},{6}Text:{5}{6}",
																									 Helper.GetGuid(row["SmsGuid"]),
																									 Helper.GetGuid(row["NumberGuid"]),
																									 Helper.GetGuid(row["SmsTrafficRelayGuid"]),
																									 receiveMessage.Sender,
																									 receiveMessage.Receiver,
                                                                                                     receiveMessage.SmsText,
																									 Environment.NewLine));

							receiveMessage.Guid = Helper.GetGuid(row["SmsGuid"]);
							receiveMessage.PrivateNumberGuid = Helper.GetGuid(row["NumberGuid"]);
							receiveMessage.UserGuid = Helper.GetGuid(row["OwnerGuid"]);
							receiveMessage.SmsTrafficRelayGuid = Helper.GetGuid(row["SmsTrafficRelayGuid"]);

							try
							{
								WriteReceiveSmsToXmlFile(receiveMessage);
							}
							catch
							{
							}
							finally
							{
								ManageQueue.SendMessage("SmsRelay", receiveMessage, string.Format("{0}->{1}", receiveMessage.Sender, receiveMessage.Receiver));

								Facade.SmsParser.ParseReceiveSms(Helper.GetGuid(row["SmsGuid"]), Helper.GetGuid(row["NumberGuid"]), receiveMessage.SmsText, receiveMessage.Sender);

								ReceiveMessageFromQueue(message.Id);
							}
						}
					}
					else
					{
						ReceiveMessageFromQueue(message.Id);
					}
				}
				catch (Exception ex)
				{
					LogController<ServiceLogs>.LogInFile(ServiceLogs.SmsParser, string.Format("On ProcessMessage : {0}", ex.Message));
					LogController<ServiceLogs>.LogInFile(ServiceLogs.SmsParser, string.Format("On ProcessMessage : {0}", ex.StackTrace));
					throw ex;
				}
			}
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

		private void WriteReceiveSmsToXmlFile(ReceiveMessage receiveMessage)
		{
			try
			{
				XDocument doc;

				string rootDirectory = string.Format("{0}{1}{2}{1}{3}",
																							ConfigurationManager.GetSetting("XmlReceiveFilePath"),
																							Path.DirectorySeparatorChar,
																							receiveMessage.Receiver,
																							string.Format("{0}", DateTime.Now.ToString("yyyyMMdd")));

				if (!Directory.Exists(rootDirectory))
					Directory.CreateDirectory(rootDirectory);

				string filePath = string.Format("{0}{1}Receive.xml", rootDirectory, Path.DirectorySeparatorChar);

				if (!File.Exists(filePath))
				{
					doc = new XDocument(
										new XDeclaration("1.0", "utf-8", "yes"),
											new XElement("Receives"));
					doc.Save(filePath);
				}

				doc = XDocument.Load(filePath);

				doc.Element("Receives").Add(
											new XElement("Sms", new XAttribute("Guid", receiveMessage.Guid),
																					new XAttribute("Line", receiveMessage.Receiver),
																					new XAttribute("IsRead", "0"),
																					new XAttribute("UserGuid", receiveMessage.UserGuid),
											new XElement("SmsText", receiveMessage.SmsText),
											new XElement("Receiver", receiveMessage.Receiver),
											new XElement("Sender", receiveMessage.Sender),
											new XElement("PrivateNumberGuid", receiveMessage.PrivateNumberGuid),
											new XElement("ReceiveDateTime", receiveMessage.ReceiveDateTime)));

				doc.Save(filePath);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void ParserThread_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.SmsParser, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.SmsParser, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
