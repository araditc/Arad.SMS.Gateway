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

namespace Arad.SMS.Gateway.MagfaSmsSender
{
	public class MagfaSmsServiceManager : ISmsServiceManager
	{
		public SmsSenderAgentReference SmsSenderAgentReference
		{
			get
			{
				return SmsSenderAgentReference.Magfa;
			}
		}

		public void SendSms(BatchMessage batchMessage)
		{
			var magfaWebService = new MagfaWebService.SoapSmsQueuableImplementationService();
			magfaWebService.Credentials = new System.Net.NetworkCredential(batchMessage.Username, batchMessage.Password);
			magfaWebService.PreAuthenticate = true;

			string logFile = string.Format(@"{0}\{1}", ConfigurationManager.GetSetting("RecipientAddress"), batchMessage.Id);

			List<long> result = new List<long>();
			long msgID;
			int counter = 0;

			try
			{
				try
				{
					result = magfaWebService.enqueue(batchMessage.Domain,
																					 new string[1] { batchMessage.SmsText },
																					 batchMessage.Receivers.Select(sms => sms.RecipientNumber).ToArray(),
																					 new string[1] { batchMessage.SenderNumber },
																					 new int[1],
																					 new string[1],
																					 new int[1] { batchMessage.IsFlash ? 0 : 1 },
																					 new int[1],
																					 batchMessage.Receivers.Select(sms => Helper.GetLong(sms.CheckID)).ToArray()).ToList();
				}
				catch (Exception ex)
				{
					LogController<ServiceLogs>.LogInFile(ServiceLogs.Magfa, string.Format("\r\n-------------------------------------------------------------------------"));
					LogController<ServiceLogs>.LogInFile(ServiceLogs.Magfa, string.Format("\r\n{0} : Message : {1}", "magfaWebService.enqueue", ex.Message));
					LogController<ServiceLogs>.LogInFile(ServiceLogs.Magfa, string.Format("\r\n{0} : OutboxGuid : {1}", "magfaWebService.enqueue", batchMessage.Guid.ToString()));
					LogController<ServiceLogs>.LogInFile(ServiceLogs.Magfa, string.Format("\r\n-------------------------------------------------------------------------"));
				}

				if (result.Count == 0)
				{
					foreach (var receiver in batchMessage.Receivers)
					{
						msgID = 0;
						receiver.ReturnID = result.ToString();
						CheckErrorSms(ref msgID, receiver);
					}
				}
				else if (result.Count == 1 && result.Count != batchMessage.Receivers.Count())
				{
					foreach (var receiver in batchMessage.Receivers)
					{
						msgID = result[0];
						CheckErrorSms(ref msgID, receiver);
						receiver.ReturnID = msgID.ToString();
					}
				}
				else if (result.Count == batchMessage.Receivers.Count())
				{
					foreach (var receiver in batchMessage.Receivers)
					{
						msgID = result[counter];
						CheckErrorSms(ref msgID, receiver);
						receiver.ReturnID = msgID.ToString();
						counter++;
					}
				}

				LogController<ServiceLogs>.LogInFile(logFile,
																							string.Format("Id={0}Guid={1}Sms={2}Receivers={3}{4}",
																														 batchMessage.Id,
																														 batchMessage.Guid,
																														 batchMessage.SmsText,
																														 string.Join(";", batchMessage.Receivers.Select<InProgressSms, string>(sms => string.Format("{0}-{1}-{2}", sms.RecipientNumber, sms.ReturnID, batchMessage.PageNo))),
																														 Environment.NewLine));
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Magfa, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Magfa, string.Format("\r\n{0} : Message : {1}", "magfaWebService.SendSms", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Magfa, string.Format("\r\n{0} : SmsSentGuid : {1}", "magfaWebService.SendSms", batchMessage.Guid.ToString()));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Magfa, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private bool CheckErrorSms(ref long result, InProgressSms inProgressSms)
		{
			inProgressSms.SendTryCount += 1;

			if (result < 1000)
			{
				inProgressSms.SendStatus = (int)SendStatus.ErrorInSending;
				inProgressSms.DeliveryStatus = (int)DeliveryStatus.NotSent;

				switch (result)
				{
					case (int)MagfaSmsSendError.AuthenticationFailure:
						result = (int)Business.SmsSendError.AuthenticationFailure;
						break;

					case (int)MagfaSmsSendError.DeactiveAccount:
						result = (int)Business.SmsSendError.DeactiveAccount;
						break;

					case (int)MagfaSmsSendError.ExpiredAccount:
						result = (int)Business.SmsSendError.ExpiredAccount;
						break;

					case (int)MagfaSmsSendError.InvalidUsernameOrPassword:
						result = (int)Business.SmsSendError.InvalidUsernameOrPassword;
						break;

					case (int)MagfaSmsSendError.NotEnoughCrdit:
						result = (int)Business.SmsSendError.NotEnoughCredit;
						break;

					case (int)MagfaSmsSendError.ServerBusy:
						result = (int)Business.SmsSendError.ServerBusy;
						inProgressSms.SendStatus = (int)SendStatus.ErrorInGetItc;
						break;

					case (int)MagfaSmsSendError.ServerError:
						result = (int)Business.SmsSendError.ServerError;
						inProgressSms.SendStatus = (int)SendStatus.ErrorInGetItc;
						break;

					case (int)MagfaSmsSendError.NumberAtBlackList:
						result = (int)Business.SmsSendError.NumberAtBlackList;
						inProgressSms.SendStatus = (int)SendStatus.BlackList;
						inProgressSms.DeliveryStatus = (int)DeliveryStatus.BlackList;
						break;

					case (int)MagfaSmsSendError.SendError:
						result = (int)Business.SmsSendError.SendError;
						inProgressSms.SendStatus = (int)SendStatus.ErrorInGetItc;
						break;

					case (int)MagfaSmsSendError.InvalidCheckMessageID:
						result = (int)Business.SmsSendError.SendError;
						break;
				}
				return true;
			}
			else
			{
				inProgressSms.SendStatus = (int)SendStatus.Sent;
				inProgressSms.DeliveryStatus = (int)DeliveryStatus.SentToItc;
				return false;
			}
		}
	}
}
