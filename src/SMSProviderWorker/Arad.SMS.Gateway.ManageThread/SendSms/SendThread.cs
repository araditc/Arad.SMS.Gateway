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
using Arad.SMS.Gateway.SqlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization.Formatters;
using System.Threading;

namespace Arad.SMS.Gateway.ManageThread
{
	public class SendThread : WorkerThread
	{
		protected ISmsServiceManager ServiceManager { get; set; }

		protected InProgressSms inProgressSms;
		protected List<InProgressSms> inProgressSmsList;
		protected BatchMessage batchMessage;
		protected List<BatchMessage> batchMessageList;
		protected static Random random = new Random();
		protected string sentMessageQueue;
		protected Dictionary<string, Tuple<int, TimeSpan, TimeSpan>> sendQueueInfo;
		protected bool isActiveDeliveryRelay;
		protected string getDeliveryQueue;
		protected int sendTryCount = 0;
		private Thread synchronizerSendThread;
		public SendThread(int timeOut)
			: base(timeOut)
		{
			this.ThreadException += new ThreadExceptionEventHandler(SendThread_ThreadException);

			batchMessageList = new List<BatchMessage>();
			sendQueueInfo = new Dictionary<string, Tuple<int, TimeSpan, TimeSpan>>();
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
				batchMessageList.Clear();
				var messages = new List<Message>();
				string queuePath = string.Empty;

				foreach (KeyValuePair<string, Tuple<int, TimeSpan, TimeSpan>> info in sendQueueInfo)
				{
					queuePath = string.Format(@".\private$\{0}", info.Key);
					int capacity = info.Value.Item1;
					bool sendTimeIsValid = (TimeSpan.Compare(info.Value.Item2, info.Value.Item3) == 0 ||
																	(TimeSpan.Compare(DateTime.Now.TimeOfDay, info.Value.Item2) == 1 &&
																	 TimeSpan.Compare(DateTime.Now.TimeOfDay, info.Value.Item3) == -1)
																 ) ? true : false;

					int counter = 0;

					if (MessageQueue.Exists(queuePath) && sendTimeIsValid)
					{
						queue = new MessageQueue(queuePath);
						queue.Formatter = new BinaryMessageFormatter();

						var msgEnumerator = queue.GetMessageEnumerator2();
						while (msgEnumerator.MoveNext(new TimeSpan(0, 0, 1)))
						{
							//using (var msg = queue.ReceiveById(msgEnumerator.Current.Id, new TimeSpan(0, 0, 1)))
							using (var msg = msgEnumerator.Current)
							{
								messages.Add(msg);
								counter++;
								if (counter == capacity)
									break;
							}
						}
					}
				}

				if (messages.Count > 0)
					SendSms(messages);
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SendSms, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SendSms, string.Format("\r\nWorkerThreadFunction(SendSms)({0}) : {1}", ServiceManager.SmsSenderAgentReference, ex.ToString()));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SendSms, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void SendSms(List<Message> messages)
		{
			int necessaryThread = messages.Count();

			var senderThreads = new List<Thread>();
			var smsListPart = new List<BatchMessage>();

			for (int n = 0; n < necessaryThread; n++)
			{
				batchMessage = new BatchMessage();
				messages[n].Formatter = new BinaryMessageFormatter();
				batchMessage = (BatchMessage)messages[n].Body;
				batchMessage.QueueMessageId = messages[n].Id;
				if (sendTryCount != 0)
					batchMessage.MaximumTryCount = sendTryCount;
				smsListPart.Add(batchMessage);
			}

			for (int i = 0; i < necessaryThread; i++)
			{
				try
				{
					var smsList = smsListPart[i];
					var thread = new Thread(() => ServiceManager.SendSms(smsList));
					thread.Start();

					senderThreads.Add(thread);
				}
				catch (Exception ex)
				{
					LogController<ServiceLogs>.LogInFile(ServiceLogs.SendSms, string.Format("create thread for necessaryThread: {0}", ex.Message));
				}
			}

			foreach (var thread in senderThreads)
				thread.Join();

			#region Integrate Lists
			//batchMessageList = new List<BatchMessage>();
			batchMessageList.AddRange(smsListPart);
			#endregion

			InsertSentMessages();
		}

		private void InsertSentMessages()
		{
			string messageId;
			foreach (BatchMessage batch in batchMessageList)
			{
				messageId = ManageQueue.SendMessage(sentMessageQueue, batch, string.Format("{0}-{1}", batch.Id, batch.PageNo));
				ReceiveMessageFromQueue(batch);

				if (!isActiveDeliveryRelay)
				{
					batch.Receivers.RemoveAll(sms => sms.DeliveryStatus != (int)DeliveryStatus.SentToItc);
					if (batch.Receivers.Count > 0)
						ManageQueue.SendMessage(getDeliveryQueue, batch, string.Format("batchId:{0}=>SenderId:{1}-ReceiverCount:{2}", batch.Id, batch.SenderNumber, batchMessage.Receivers.Count));
				}
			}
		}

		private void ReceiveMessageFromQueue(BatchMessage batch)
		{
			string queuePath = string.Format(@".\private$\{0}", batch.QueueName);
			if (MessageQueue.Exists(queuePath))
			{
				MessageQueue queue = new MessageQueue(queuePath);
				queue.ReceiveById(batch.QueueMessageId);
			}
		}

		private void SendThread_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.SendSms, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.SendSms, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
