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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Threading;

namespace SaveSmsDelivery
{
	public class SaveDeliveryThread : WorkerThread
	{
		private string queueName;
		private int capacity;
		private DeliveryMessage delivery;
		private List<DeliveryMessage> lstDelivery;
		private List<string> lstMessageIds;

		private Thread synchronizerSaveThread;
		public SaveDeliveryThread(int timeOut)
			: base(timeOut)
		{
			this.ThreadException += new ThreadExceptionEventHandler(SaveThread_ThreadException);

			lstDelivery = new List<DeliveryMessage>();
			lstMessageIds = new List<string>();

			queueName = ConfigurationManager.GetSetting("QueueName");
			capacity = Helper.GetInt(ConfigurationManager.GetAttributeValue("QueueName", "Capacity"));
		}

		protected override void WorkerThreadFunction()
		{
			if (this.IsStopSignaled)
				return;

			if (synchronizerSaveThread == null || !synchronizerSaveThread.IsAlive)
			{
				synchronizerSaveThread = new Thread(new ThreadStart(SynchronizationSaveDelivery));

				synchronizerSaveThread.Start();
			}
		}

		private void SynchronizationSaveDelivery()
		{
			try
			{
				MessageQueue queue;
				lstDelivery.Clear();
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
					SaveDelivery(messages);
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveDelivery, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveDelivery, string.Format("\r\nWorkerThreadFunction(SaveDelivery) : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveDelivery, string.Format("\r\nWorkerThreadFunction(SaveDelivery) : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveDelivery, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void SaveDelivery(List<Message> messages)
		{
			try
			{
				foreach (Message msg in messages)
				{
					lstMessageIds.Clear();
					delivery = new DeliveryMessage();
					msg.Formatter = new BinaryMessageFormatter();
					delivery = (DeliveryMessage)msg.Body;
					delivery.MessageId = msg.Id;
					if (string.IsNullOrEmpty(delivery.ReturnId) || delivery.ReturnId == "0")
					{
						ReceiveMessageFromQueue(msg.Id);
						continue;
					}

					switch (delivery.Agent)
					{
						case (int)SmsSenderAgentReference.SLS:
						case (int)SmsSenderAgentReference.RahyabRG:
							lstMessageIds = lstDelivery.Where(item =>
																								item.Agent == delivery.Agent &&
																								item.BatchId == delivery.BatchId &&
																								item.Mobile == delivery.Mobile &&
																								item.Date <= delivery.Date &&
																								item.Time < delivery.Time).Select(item => item.MessageId).ToList();
							lstDelivery.RemoveAll(item =>
																			item.Agent == delivery.Agent &&
																			item.BatchId == delivery.BatchId &&
																			item.Mobile == delivery.Mobile &&
																			item.Date <= delivery.Date &&
																			item.Time < delivery.Time);
							break;
						default:
							lstMessageIds = lstDelivery.Where(item =>
																								item.Agent == delivery.Agent &&
																								item.ReturnId == delivery.ReturnId &&
																								item.Date <= delivery.Date &&
																								item.Time < delivery.Time).Select(item => item.MessageId).ToList();
							lstDelivery.RemoveAll(item =>
																		item.Agent == delivery.Agent &&
																		item.ReturnId == delivery.ReturnId &&
																		item.Date <= delivery.Date &&
																		item.Time < delivery.Time);
							break;
					}

					ReceiveMessageFromQueue(lstMessageIds);

					lstDelivery.Add(delivery);
				}



				if (Arad.SMS.Gateway.Facade.OutboxNumber.UpdateDeliveryStatus(lstDelivery))
					ReceiveMessageFromQueue(lstDelivery.Select(item => item.MessageId).ToList());


				lstDelivery.Clear();
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveDelivery, string.Format("On SaveDeliveryStatus : {0}*{1}", ex.Message, ex.StackTrace));
				throw ex;
			}
		}

		private void ReceiveMessageFromQueue(List<string> lstIds)
		{
			string queuePath = string.Format(@".\private$\{0}", queueName);
			if (MessageQueue.Exists(queuePath))
			{
				MessageQueue queue = new MessageQueue(queuePath);
				foreach (string id in lstIds)
					queue.ReceiveById(id);
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
			LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveDelivery, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveDelivery, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
