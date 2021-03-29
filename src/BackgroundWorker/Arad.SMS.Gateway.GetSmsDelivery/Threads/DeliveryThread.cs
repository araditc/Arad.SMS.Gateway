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
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization.Formatters;
using System.Threading;

namespace Arad.SMS.Gateway.GetSmsDelivery
{
	public class DeliveryThread : WorkerThread
	{
		private ISmsServiceManager ServiceManager { get; set; }
		public SmsSenderAgentReference SenderAgentRefrence { get; private set; }
		private Thread synchronizerDeliveryThread;
		private static string queueName;
		private static string saveDeliveryQueue;
		private BatchMessage batchMessage;
		private List<BatchMessage> batchMessageList;
		private DeliveryMessage deliveryMessage;

		private int capacity;

		public DeliveryThread(int timeOut)
			: base(timeOut)
		{
			this.ThreadException += new ThreadExceptionEventHandler(DeliveryThread_ThreadException);

			queueName = ConfigurationManager.GetSetting("QueueName");
			capacity = Helper.GetInt(ConfigurationManager.GetAttributeValue("QueueName", "Capacity"));
			saveDeliveryQueue = ConfigurationManager.GetSetting("SaveDeliveryQueueName");

			batchMessageList = new List<BatchMessage>();
		}

		protected override void WorkerThreadFunction()
		{
			if (this.IsStopSignaled)
				return;

			if (synchronizerDeliveryThread == null || !synchronizerDeliveryThread.IsAlive)
			{
				synchronizerDeliveryThread = new Thread(new ThreadStart(SynchronizationGetDelivery));

				synchronizerDeliveryThread.Start();
			}
		}

		private void SynchronizationGetDelivery()
		{
			try
			{
				batchMessageList.Clear();
				MessageQueue queue;
				string queuePath = string.Format(@".\private$\{0}", queueName);
				var messages = new List<Message>();
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
					GetDelivery(messages);
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("\r\n WorkerThreadFunction(GetDelivery) : {0}", ex.ToString()));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void GetDelivery(List<Message> messages)
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
				smsListPart.Add(batchMessage);
			}

			for (int i = 0; i < necessaryThread; i++)
			{
				try
				{
					var smsList = smsListPart[i];
					var thread = new Thread(() => GetDeliveryFromWebService(smsList));
					thread.Start();

					senderThreads.Add(thread);
				}
				catch (Exception ex)
				{
					LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("create thread for necessaryThread: {0}", ex.Message));
				}
			}

			foreach (var thread in senderThreads)
				thread.Join();

			#region Integrate Lists
			batchMessageList = new List<BatchMessage>();

			batchMessageList.AddRange(smsListPart);
			#endregion

			InsertDelivery();
		}

		private void GetDeliveryFromWebService(BatchMessage batchMessage)
		{
			try
			{
				this.SenderAgentRefrence = (SmsSenderAgentReference)batchMessage.SmsSenderAgentReference;

				this.ServiceManager = SmsServiceFactory.GetSmsServiceManager((SmsSenderAgentReference)batchMessage.SmsSenderAgentReference);

				ServiceManager.GetDeliveryStatus(batchMessage);
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("\r\n<--<--<--<--<--<--<--<--<--<--<--"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("\r\nGetDeleviry : StackTrace : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("\r\nGetDeleviry : Message : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("\r\n<--<--<--<--<--<--<--<--<--<--<--"));
			}
		}

		private void InsertDelivery()
		{
			List<int> deliveryStayInQueue = new List<int>();
			deliveryStayInQueue.Add((int)DeliveryStatus.SentToItc);
			deliveryStayInQueue.Add((int)DeliveryStatus.ReceivedByItc);
			deliveryStayInQueue.Add((int)DeliveryStatus.Sent);

			foreach (BatchMessage batch in batchMessageList)
			{
				foreach (InProgressSms sms in batch.Receivers)
				{
					if (!sms.SaveDelivery)
						continue;

					deliveryMessage = new DeliveryMessage();

					deliveryMessage.Agent = (int)batch.SmsSenderAgentReference;
					deliveryMessage.BatchId = sms.ReturnID;
					deliveryMessage.ReturnId = sms.ReturnID;
					deliveryMessage.Mobile = sms.RecipientNumber;
					deliveryMessage.Status = (int)sms.DeliveryStatus;
					deliveryMessage.Date = DateTime.Now.Date;
					deliveryMessage.Time = DateTime.Now.TimeOfDay;

					ManageQueue.SendMessage(saveDeliveryQueue, deliveryMessage, string.Format("Agent:{0}=>Mobile:{1}-ReturnId:{2}-Status:{3}", deliveryMessage.Agent, deliveryMessage.Mobile, deliveryMessage.ReturnId, deliveryMessage.Status));
				}

				batch.Receivers.RemoveAll(sms => sms.SaveDelivery && (sms.DeliveryTryCount > 5 || !deliveryStayInQueue.Contains((int)sms.DeliveryStatus)));
			}

			foreach (BatchMessage batch in batchMessageList)
			{
				if (batch.Receivers.Count > 0)
				{
					foreach (InProgressSms sms in batch.Receivers)
						sms.SaveDelivery = false;

					ManageQueue.SendMessage(queueName, batch, string.Format("Id:{0}-batchGuid:{1}=>SenderId:{2}-ReceiverCount:{3}", batch.Id, batch.Guid, batch.SenderNumber, batch.Receivers.Count));
				}

				ReceiveMessageFromQueue(batch.QueueMessageId);
			}
		}

		private void ReceiveMessageFromQueue(string id)
		{
			string queuePath = string.Format(@".\private$\{0}", queueName);
			if (MessageQueue.Exists(queuePath))
			{
				MessageQueue queue = new MessageQueue(queuePath);
				queue.ReceiveById(id);
			}
		}

		private void DeliveryThread_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
