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

using System;
using System.Messaging;

namespace Arad.SMS.Gateway.SqlLibrary
{
	public class ManageQueue
	{
		public enum Queues
		{
			ReceiveMessagesQueue = 1,
			ApiSendMessage = 2,
			SaveDelivery = 3,
			ConfirmBulk = 4,
			SaveGsmDelivery = 5,
			SmsRelay = 6,
			DeliveryRelay = 7,
		}

		public static string SendMessage(string queueName, object message, string messageLabel)
		{
			try
			{
				MessageQueue queue;
				MessageQueueTransaction msgTx = new MessageQueueTransaction();

				Message msg = new Message();

				string queuePath = string.Format(@".\private$\{0}", queueName);

				if (!MessageQueue.Exists(queuePath))
				{
					using (queue = MessageQueue.Create(queuePath, true))
					{
						queue.SetPermissions("Everyone",
																 MessageQueueAccessRights.FullControl,
																 AccessControlEntryType.Allow);

						queue.UseJournalQueue = false;
						queue.Label = queueName.ToString();
					}
				}
				else
					queue = new MessageQueue(queuePath);

				msg.Label = messageLabel;
				msg.Formatter = new BinaryMessageFormatter();
				msg.Body = message;
				msg.UseJournalQueue = false;
				msg.Recoverable = true;

				msgTx.Begin();
				try
				{
					queue.Send(msg, msgTx);
					msgTx.Commit();
				}
				catch
				{
					msgTx.Abort();
				}
				finally
				{
					queue.Close();
				}

				return msg.Id;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static string SendMessage(string queueName, string remoteQueueIp, object message, string messageLabel)
		{
			try
			{
				MessageQueue queue;
				MessageQueueTransaction msgTx = new MessageQueueTransaction();

				Message msg = new Message();

				string queuePath = string.Format(@"FORMATNAME:Direct=TCP:{0}\private$\{1}", remoteQueueIp, queueName);

				queue = new MessageQueue(queuePath);

				msg.Label = messageLabel;
				msg.Formatter = new BinaryMessageFormatter();
				msg.Body = message;
				msg.UseJournalQueue = false;
				msg.Recoverable = true;

				msgTx.Begin();
				try
				{
					queue.Send(msg, msgTx);
					msgTx.Commit();
				}
				catch
				{
					msgTx.Abort();
				}
				finally
				{
					queue.Close();
				}

				return msg.Id;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
