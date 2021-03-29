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

namespace Arad.SMS.Gateway.WebApi
{
	public class ManageQueue
	{
		public static bool SendMessage(Queues queueName, object message, string messageLabel = "")
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
				queue.Send(msg, msgTx);
				msgTx.Commit();

				queue.Close();
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool ReceiveMessageFromQueue(string queueName, string messageId)
		{
			try
			{
				string queuePath = string.Format(@".\private$\{0}", queueName);
				if (!MessageQueue.Exists(queuePath))
					return false;
				MessageQueue queue = new MessageQueue(queuePath);
				queue.ReceiveById(messageId);
				return true;
			}
			catch (Exception ex) { throw ex; }
		}
	}
}
