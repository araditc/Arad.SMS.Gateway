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
using Arad.SMS.Gateway.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization.Formatters;
using System.Threading;

namespace SaveSentSms
{
	public class SaveThread : WorkerThread
	{
		private BatchMessage batchMessage;
		private List<BatchMessage> batchMessageList;
		private Thread synchronizerSendThread;
		private static Random random = new Random();
		private static int threadCount;
		private static string queueName;
		public SaveThread(int timeOut)
			: base(timeOut)
		{
			this.ThreadException += new ThreadExceptionEventHandler(SaveThread_ThreadException);

			batchMessageList = new List<BatchMessage>();

			threadCount = Helper.GetInt(ConfigurationManager.GetSetting("ThreadCount"));
			queueName = ConfigurationManager.GetSetting("QueueName");
		}

		protected override void WorkerThreadFunction()
		{
			if (this.IsStopSignaled)
				return;

			if (synchronizerSendThread == null || !synchronizerSendThread.IsAlive)
			{
				synchronizerSendThread = new Thread(new ThreadStart(SynchronizationSentbox));

				synchronizerSendThread.Start();
			}
		}

		private void SynchronizationSentbox()
		{
			try
			{
				MessageQueue queue;
				string queuePath = string.Format(@".\private$\{0}", queueName);
				batchMessageList.Clear();

				if (MessageQueue.Exists(queuePath))
				{
					queue = new MessageQueue(queuePath);
					queue.Formatter = new BinaryMessageFormatter();

					var msgEnumerator = queue.GetMessageEnumerator2();
					var messages = new List<Message>();
					while (msgEnumerator.MoveNext(new TimeSpan(0, 0, 1)))
					{
						using (var msg = msgEnumerator.Current)
						{
							messages.Add(msg);
							if (messages.Count == threadCount)
								break;
						}
					}

					if (messages.Count > 0)
						SaveSms(messages);
				}

			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveSentMessage, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveSentMessage, string.Format("\r\n SynchronizationSentbox : {0}", ex.ToString()));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveSentMessage, string.Format("\r\n SynchronizationSentbox : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveSentMessage, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void SaveSms(List<Message> messages)
		{
			try
			{
				int necessaryThread = messages.Count();

				var saveThreads = new List<Thread>();
				var smsListPart = new List<BatchMessage>();

				for (int n = 0; n < necessaryThread; n++)
				{
					batchMessage = new BatchMessage();

					messages[n].Formatter = new BinaryMessageFormatter();
					batchMessage = (BatchMessage)messages[n].Body;
					batchMessage.QueueMessageId = messages[n].Id;
					smsListPart.Add(batchMessage);
				}

				for (int i = 0; i < necessaryThread; i++)
				{
					try
					{
						var smsList = smsListPart[i];
						var thread = new Thread(() => InsertSentMessages(smsList));
						thread.Start();

						saveThreads.Add(thread);
					}
					catch (Exception ex)
					{
						LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveSentMessage, string.Format("create thread for necessaryThread: {0}", ex.Message));
					}
				}

				foreach (var thread in saveThreads)
					thread.Join();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void InsertSentMessages(BatchMessage batch)
		{
			try
			{
				if (Arad.SMS.Gateway.Facade.OutboxNumber.InsertSentMessages(batch))
					ReceiveMessageFromQueue(batch);

				if (batch.Receivers.Count > 0)
					ManageQueue.SendMessage(batch.QueueName, batch, string.Format("{0}-{1}", batch.Id, batch.PageNo));
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveSentMessage, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveSentMessage, string.Format("\r\n SaveSentMessage : {0}", ex.ToString()));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveSentMessage, string.Format("\r\n SaveSentMessage : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveSentMessage, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void ReceiveMessageFromQueue(BatchMessage batch)
		{
			string queuePath = string.Format(@".\private$\{0}", queueName);
			if (MessageQueue.Exists(queuePath))
			{
				MessageQueue queue = new MessageQueue(queuePath);
				queue.ReceiveById(batch.QueueMessageId);
			}
		}

		private void SaveThread_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveSentMessage, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveSentMessage, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
