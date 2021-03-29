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
using System.Threading;
using Arad.SMS.Gateway.WebApi.Models;
using System.Linq;

namespace Arad.SMS.Gateway.GiveBackCredit
{
	public class ConfirmBulkThread : WorkerThread
	{
		private Thread synchronizerBulkThread;
		private static string queueName;
		private static int capacity;

		public ConfirmBulkThread(int timeOut)
			: base(timeOut)
		{
			this.ThreadException += new ThreadExceptionEventHandler(BulkThread_ThreadException);
			queueName = ConfigurationManager.GetSetting("QueueName");
			capacity = Helper.GetInt(ConfigurationManager.GetAttributeValue("QueueName", "Capacity"), 1);

		}

		protected override void WorkerThreadFunction()
		{
			if (this.IsStopSignaled)
				return;

			if (synchronizerBulkThread == null || !synchronizerBulkThread.IsAlive)
			{
				synchronizerBulkThread = new Thread(new ThreadStart(SynchronizationConfirm));

				synchronizerBulkThread.Start();
			}
		}

		private void SynchronizationConfirm()
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
					Confirm(messages);
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SmsParser, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SmsParser, string.Format("\r\nWorkerThreadFunction(SmsParser) {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SmsParser, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void Confirm(List<Message> messages)
		{
			foreach (Message message in messages)
			{
				try
				{
					BulkSmsModel bulkInfo = (BulkSmsModel)message.Body;
					Dictionary<DeliveryStatus, List<string>> lstNumbers = new Dictionary<DeliveryStatus, List<string>>();
					lstNumbers.Add(DeliveryStatus.NotSent, bulkInfo.Receivers.Split(',').ToList());

					Facade.Outbox.UpdateStatus(Guid.Empty, bulkInfo.Status, bulkInfo.Id);
					Facade.OutboxNumber.UpdateDeliveryStatus(Guid.Empty, lstNumbers, bulkInfo.Id);
					ReceiveMessageFromQueue(message.Id);
				}
				catch (Exception ex)
				{
					LogController<ServiceLogs>.LogInFile(ServiceLogs.ConfirmBulk, string.Format("On ConfirmBulk : {0}", ex.Message));
					LogController<ServiceLogs>.LogInFile(ServiceLogs.ConfirmBulk, string.Format("On ConfirmBulk : {0}", ex.StackTrace));
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

		private void BulkThread_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.ConfirmBulk, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.ConfirmBulk, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
