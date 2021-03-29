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

using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.ManageThread;
using Arad.SMS.Gateway.SqlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Threading;

namespace Arad.SMS.Gateway.ApiProcessRequest
{
	public class APIThread : WorkerThread
	{
		private Thread synchronizerAPIThread;
		private static string queueName;
		private static int capacity;

		public APIThread(int timeOut)
			: base(timeOut)
		{
			this.ThreadException += new ThreadExceptionEventHandler(APIThread_ThreadException);
			queueName = ConfigurationManager.GetSetting("QueueName");
			capacity = Helper.GetInt(ConfigurationManager.GetAttributeValue("QueueName", "Capacity"), 1);
		}

		protected override void WorkerThreadFunction()
		{
			if (this.IsStopSignaled)
				return;

			if (synchronizerAPIThread == null || !synchronizerAPIThread.IsAlive)
			{
				synchronizerAPIThread = new Thread(new ThreadStart(SynchronizationAPI));

				synchronizerAPIThread.Start();
			}
		}

		private void SynchronizationAPI()
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
				LogController<ServiceLogs>.LogInFile(ServiceLogs.APIProcessRequest, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.APIProcessRequest, string.Format("{1}WorkerThreadFunction(APIProcessRequest) Message: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.APIProcessRequest, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void Save(List<Message> messages)
		{
			Common.ScheduledSms scheduledSms;
			List<string> lstReceivers;
			BatchMessage batchMessage;
			foreach (Message msg in messages)
			{
				batchMessage = new BatchMessage();
				batchMessage = (BatchMessage)msg.Body;
				scheduledSms = new Common.ScheduledSms();
				lstReceivers = new List<string>();

				LogController<ServiceLogs>.LogInFile(ServiceLogs.APIProcessRequest, string.Format("\r\n-------------------------------------------------------------------------"));

				scheduledSms.ScheduledSmsGuid = batchMessage.Guid;
				scheduledSms.PrivateNumberGuid = batchMessage.PrivateNumberGuid;
				scheduledSms.SmsText = batchMessage.SmsText;
				scheduledSms.SmsLen = batchMessage.SmsLen;
				scheduledSms.PresentType = batchMessage.IsFlash ? (int)Messageclass.Flash : (int)Messageclass.Normal;
				scheduledSms.Encoding = batchMessage.IsUnicode ? (int)Encoding.Utf8 : (int)Encoding.Default;
				scheduledSms.UserGuid = batchMessage.UserGuid;
				scheduledSms.Status = (int)ScheduledSmsStatus.Stored;
				scheduledSms.DateTimeFuture = DateTime.Now;
				scheduledSms.TypeSend = batchMessage.SmsSendType;

				LogController<ServiceLogs>.LogInFile(ServiceLogs.APIProcessRequest, string.Format(@"batchMessage.ServiceId : {0},ScheduledSmsGuid : {1},PrivateNumberGuid: {2}", batchMessage.ServiceId, scheduledSms.ScheduledSmsGuid, scheduledSms.PrivateNumberGuid));

				if (batchMessage.ServiceId == null)
				{
					LogController<ServiceLogs>.LogInFile(ServiceLogs.APIProcessRequest, string.Format("batchMessage.sendernumber : {0},batchMessage.UserGuid:{1}", batchMessage.SenderNumber, batchMessage.UserGuid));

					scheduledSms.PrivateNumberGuid = Facade.PrivateNumber.GetUserNumberGuid(batchMessage.SenderNumber, batchMessage.UserGuid);

					LogController<ServiceLogs>.LogInFile(ServiceLogs.APIProcessRequest, string.Format("scheduledSms.PrivateNumberGuid : {0} SendType : {1}", scheduledSms.PrivateNumberGuid, (SmsSendType)batchMessage.SmsSendType));

					if (batchMessage.SmsSendType == (int)SmsSendType.SendSmsFromAPI)
					{
						lstReceivers = batchMessage.Receivers.Select<InProgressSms, string>(receiver => receiver.RecipientNumber).DefaultIfEmpty().ToList();
						if (Facade.ScheduledSms.InsertSms(scheduledSms, lstReceivers))
							ReceiveMessageFromQueue(msg.Id);
					}
					else if (batchMessage.SmsSendType == (int)SmsSendType.SendGroupSmsFromAPI)
					{
						scheduledSms.ReferenceGuid = string.Join(",", batchMessage.ReferenceGuid);
						if (Facade.ScheduledSms.InsertGroupSms(scheduledSms))
							ReceiveMessageFromQueue(msg.Id);
					}
				}
				else
				{
					if (batchMessage.Receivers.Count > 0)
					{
						lstReceivers = batchMessage.Receivers.Select<InProgressSms, string>(receiver => receiver.RecipientNumber).ToList();
						if (Facade.ScheduledSms.InsertSms(scheduledSms, lstReceivers))
							ReceiveMessageFromQueue(msg.Id);
					}
					else
					{
						scheduledSms.ReferenceGuid = batchMessage.ReferenceGuid.ToString();
						scheduledSms.TypeSend = (int)SmsSendType.SendGroupSmsFromAPI;
						if (Facade.ScheduledSms.InsertGroupSms(scheduledSms))
							ReceiveMessageFromQueue(msg.Id);
					}
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

		private void APIThread_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.APIProcessRequest, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.APIProcessRequest, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
