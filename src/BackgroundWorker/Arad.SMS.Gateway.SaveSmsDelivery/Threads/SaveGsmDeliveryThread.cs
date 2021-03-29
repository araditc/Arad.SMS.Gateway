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
using System.Messaging;
using System.Threading;

namespace SaveSmsDelivery
{
	public class SaveGsmDeliveryThread : WorkerThread
	{
		private string queueName;
		private int capacity;

		private Thread synchronizerSaveThread;
		public SaveGsmDeliveryThread(int timeOut)
			: base(timeOut)
		{
			this.ThreadException += new ThreadExceptionEventHandler(SaveThread_ThreadException);

			queueName = ConfigurationManager.GetSetting("GsmQueueName");
			capacity = Helper.GetInt(ConfigurationManager.GetAttributeValue("GsmQueueName", "Capacity"));
		}

		protected override void WorkerThreadFunction()
		{
			if (this.IsStopSignaled)
				return;

			if (synchronizerSaveThread == null || !synchronizerSaveThread.IsAlive)
			{
				synchronizerSaveThread = new Thread(new ThreadStart(SynchronizationSaveGsmDelivery));

				synchronizerSaveThread.Start();
			}
		}

		private void SynchronizationSaveGsmDelivery()
		{
			try
			{
				MessageQueue queue;
				var messages = new List<Message>();
				string queuePath = string.Format(@".\private$\{0}", queueName);
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
					SaveGsmDelivery(messages);
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveGsmDelivery, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveGsmDelivery, string.Format("\r\nWorkerThreadFunction(SaveDelivery) : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveGsmDelivery, string.Format("\r\nWorkerThreadFunction(SaveDelivery) : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveGsmDelivery, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void SaveGsmDelivery(List<Message> messages)
		{
			try
			{
				DeliveryMessage deliveryMessage;
				foreach (Message msg in messages)
				{
					deliveryMessage = new DeliveryMessage();
					msg.Formatter = new BinaryMessageFormatter();
					deliveryMessage = (DeliveryMessage)msg.Body;
					deliveryMessage.MessageId = msg.Id;

					try
					{
						if (deliveryMessage.FailedMobile.Count > 0)
                            Arad.SMS.Gateway.Facade.OutboxNumber.UpdateGsmDeliveryStatus(DeliveryStatus.NotSent, deliveryMessage.FailedMobile, deliveryMessage.BatchId, deliveryMessage);
						if (deliveryMessage.SuccessMobile.Count > 0)
                            Arad.SMS.Gateway.Facade.OutboxNumber.UpdateGsmDeliveryStatus(DeliveryStatus.SentAndReceivedbyPhone, deliveryMessage.SuccessMobile, deliveryMessage.BatchId, deliveryMessage);

						if (deliveryMessage.FailedMobile.Count > 0 || deliveryMessage.SuccessMobile.Count > 0)
							ManageQueue.SendMessage(queueName, deliveryMessage, string.Format("Agent:{0}=>SuccessMobile:{1}-FailedMobile:{2}-BatchId:{3}", deliveryMessage.Agent, deliveryMessage.SuccessMobile.Count, deliveryMessage.FailedMobile.Count, deliveryMessage.BatchId));

                        ReceiveMessageFromQueue(msg.Id);
					}
					catch (Exception ex)
					{
						LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveDelivery, string.Format("On UpdateGsmDeliveryStatus Method : {0}*{1}", ex.Message, ex.StackTrace));
					}
				}
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveGsmDelivery, string.Format("On SaveDeliveryStatus : {0}*{1}", ex.Message, ex.StackTrace));
				throw ex;
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

		private void SaveThread_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveGsmDelivery, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveGsmDelivery, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
