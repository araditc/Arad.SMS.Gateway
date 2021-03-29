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
	public class SmsRelayThread : WorkerThread
	{
		private Thread synchronizerSendThread;
		private ReceiveMessage receiveMessage;
		private static string queueName;
		private static int capacity;
		public SmsRelayThread(int timeOut)
			: base(timeOut)
		{
			this.ThreadException += new ThreadExceptionEventHandler(SmsRelayThread_ThreadException);

			queueName = ConfigurationManager.GetSetting("SmsRelayQueue");
			capacity = Helper.GetInt(ConfigurationManager.GetAttributeValue("SmsRelayQueue", "capacity"), 5);
		}
		protected override void WorkerThreadFunction()
		{
			if (this.IsStopSignaled)
				return;

			if (synchronizerSendThread == null || !synchronizerSendThread.IsAlive)
			{
				synchronizerSendThread = new Thread(new ThreadStart(SynchronizationSmsRelay));

				synchronizerSendThread.Start();
			}
		}

		private void SynchronizationSmsRelay()
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
						var msg = queue.ReceiveById(msgEnumerator.Current.Id, new TimeSpan(0, 0, 1));
						messages.Add(msg);
						counter++;
						if (counter >= capacity)
							break;
					}
				}

				if (messages.Count > 0)
					RelaySms(messages);
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("\r\n WorkerThreadFunction : {0})", ex.ToString()));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void RelaySms(List<Message> messages)
		{
			string response = string.Empty;

			foreach (Message msg in messages)
			{
				response = string.Empty;
				receiveMessage = new ReceiveMessage();
				msg.Formatter = new BinaryMessageFormatter();
				receiveMessage = (ReceiveMessage)msg.Body;

				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay,
																						 string.Format(@"On ParseReceivedMessage{6}SmsGuid : {0},{6}NumberGuid : {1},{6}SmsTrafficRelayGuid : {2},{6},Sender :{3},{6}Receiver : {4},{6}Text:{5}{6}",
																						 receiveMessage.Guid,
																						 receiveMessage.PrivateNumberGuid,
																						 receiveMessage.SmsTrafficRelayGuid,
																						 receiveMessage.Sender,
																						 receiveMessage.Receiver,
																						 receiveMessage.SmsText,
																						 Environment.NewLine));

				if (receiveMessage.SmsTrafficRelayGuid == Guid.Empty)
					receiveMessage.SmsTrafficRelayGuid = Helper.GetGuid(Arad.SMS.Gateway.Facade.UserSetting.GetSettingValue(receiveMessage.UserGuid, Arad.SMS.Gateway.Business.AccountSetting.SmsTrafficRelay));

				if (receiveMessage.SmsTrafficRelayGuid != Guid.Empty)
				{
					TrafficRelay relay = Arad.SMS.Gateway.Facade.TrafficRelay.LoadUrl(receiveMessage.SmsTrafficRelayGuid);

					if (!string.IsNullOrEmpty(relay.Url))
					{
						relay.Url = Regex.Replace(relay.Url, "\\$to", receiveMessage.Receiver, RegexOptions.IgnoreCase);
						relay.Url = Regex.Replace(relay.Url, "\\$from", receiveMessage.Sender, RegexOptions.IgnoreCase);
						relay.Url = Regex.Replace(relay.Url, "\\$text", receiveMessage.SmsText, RegexOptions.IgnoreCase);
						LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("URL : {0}", relay.Url));
						using (var wb = new WebClient())
						{
							response = wb.DownloadString(relay.Url);
						}

						LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay,
																					 string.Format(@"On ParseReceivedMessage{0}Response : {1},{0}",
																					 Environment.NewLine, response));
					}
				}
			}
		}

		private void SmsRelayThread_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
