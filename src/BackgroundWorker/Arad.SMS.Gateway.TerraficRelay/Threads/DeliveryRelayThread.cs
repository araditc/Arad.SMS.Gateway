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
using System.Messaging;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace TerafficRelay
{
	public class DeliveryRelayThread : WorkerThread
	{
		private Thread synchronizerSendThread;
		private DeliveryMessage deliveryMessage;
		private List<DeliveryMessage> lstDeliveryMessages;
		private static string queueName;
		private static int capacity;
		public DeliveryRelayThread(int timeOut)
			: base(timeOut)
		{
			this.ThreadException += new ThreadExceptionEventHandler(DeliveryRelayThread_ThreadException);

			lstDeliveryMessages = new List<DeliveryMessage>();
			queueName = ConfigurationManager.GetSetting("DeliveryRelayQueue");
			capacity = Helper.GetInt(ConfigurationManager.GetAttributeValue("DeliveryRelayQueue", "capacity"), 5);
		}

		protected override void WorkerThreadFunction()
		{
			if (this.IsStopSignaled)
				return;

			if (synchronizerSendThread == null || !synchronizerSendThread.IsAlive)
			{
				synchronizerSendThread = new Thread(new ThreadStart(SynchronizationDeliveryRelay));

				synchronizerSendThread.Start();
			}
		}

		private void SynchronizationDeliveryRelay()
		{
			try
			{
				MessageQueue queue;
				var messages = new List<Message>();
				lstDeliveryMessages.Clear();
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
						var msg = queue.ReceiveById(msgEnumerator.Current.Id, new TimeSpan(0, 0, 1));
						messages.Add(msg);
						counter++;
						if (counter >= capacity)
							break;
					}
				}

				if (messages.Count > 0)
					RelayStatus(messages);
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("\r\n WorkerThreadFunction : {0})", ex.ToString()));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void RelayStatus(List<Message> messages)
		{
			string response = string.Empty;
			PrivateNumber privateNumber = new PrivateNumber();

			foreach (Message msg in messages)
			{
				response = string.Empty;
				deliveryMessage = new DeliveryMessage();
				msg.Formatter = new BinaryMessageFormatter();
				deliveryMessage = (DeliveryMessage)msg.Body;

				privateNumber = Arad.SMS.Gateway.Facade.PrivateNumber.LoadNumber(deliveryMessage.PrivateNumberGuid);
				deliveryMessage.DeliveryRelayGuid = privateNumber.DeliveryTrafficRelayGuid;

				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay,
																		 string.Format(@"On DeliveryRelay{5}BatchId : {0},{5}PrivateNumberGuid : {1},{5}DeliveryRelayGuid : {2},{5}Mobile :{3},{5}Status : {4},{5}",
																		 deliveryMessage.BatchId,
																		 deliveryMessage.PrivateNumberGuid,
																		 deliveryMessage.DeliveryRelayGuid,
																		 deliveryMessage.Mobile,
																		 deliveryMessage.Status,
																		 Environment.NewLine));


				if (deliveryMessage.DeliveryRelayGuid == Guid.Empty)
					deliveryMessage.DeliveryRelayGuid = Helper.GetGuid(Arad.SMS.Gateway.Facade.UserSetting.GetSettingValue(deliveryMessage.UserGuid, Arad.SMS.Gateway.Business.AccountSetting.DeliveryTrafficRelay));

				if (deliveryMessage.DeliveryRelayGuid != Guid.Empty)
				{
					TrafficRelay relay = Arad.SMS.Gateway.Facade.TrafficRelay.LoadUrl(deliveryMessage.DeliveryRelayGuid);

					if (!string.IsNullOrEmpty(relay.Url))
					{
						relay.Url = Regex.Replace(relay.Url, "\\$batchid", deliveryMessage.BatchId, RegexOptions.IgnoreCase);
						relay.Url = Regex.Replace(relay.Url, "\\$mobile", deliveryMessage.Mobile, RegexOptions.IgnoreCase);
						relay.Url = Regex.Replace(relay.Url, "\\$status", deliveryMessage.Status.ToString(), RegexOptions.IgnoreCase);
						LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("URL : {0}", relay.Url));
						using (var wb = new WebClient())
						{
							response = wb.DownloadString(relay.Url);
						}

						LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay,
																					 string.Format(@"On ParseDeliveryMessage{0}Response : {1},{0}",
																					 Environment.NewLine, response));
					}
				}

			}
			//Facade.OutboxNumber.GetDeliveryRelayInfo(lstDeliveryMessages.Where);
		}

		private void DeliveryRelayThread_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
