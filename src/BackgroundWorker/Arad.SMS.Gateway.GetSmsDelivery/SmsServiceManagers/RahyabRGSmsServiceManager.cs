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
using Arad.SMS.Gateway.SqlLibrary;
using System;
using System.Collections;
using System.Linq;

namespace Arad.SMS.Gateway.GetSmsDelivery
{
	public class RahyabRGSmsServiceManager : ISmsServiceManager
	{
		public void GetDeliveryStatus(BatchMessage batchMessage)
		{
			try
			{
				if (string.IsNullOrEmpty(batchMessage.Receivers.First().ReturnID) || batchMessage.Receivers.First().DeliveryGetTime > DateTime.Now)
					return;

				foreach (InProgressSms receiver in batchMessage.Receivers)
				{
					LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("{0}->{1}->returnId:{2}{3}", receiver.RecipientNumber, receiver.DeliveryStatus,receiver.ReturnID, Environment.NewLine));
				}

				Cls_SMS.ClsStatus sms = new Cls_SMS.ClsStatus();
				ArrayList Arr_Res = new ArrayList();
				Arr_Res = sms.StatusSMS(batchMessage.Username, batchMessage.Password, batchMessage.SendLink, batchMessage.Domain, batchMessage.Domain + "+" + batchMessage.Receivers.First().ReturnID);
				string str = string.Empty;
				for (int i = 0; i < Arr_Res.Count; i++)
				{
					Cls_SMS.ClsStatus.STC_SMSStatus tt = (Cls_SMS.ClsStatus.STC_SMSStatus)Arr_Res[i];
					LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("{0}{1}{2}", tt.ReceiveNumber, tt.DeliveryStatus, Environment.NewLine));
					batchMessage.Receivers.Where(receiver => receiver.RecipientNumber == Helper.GetLocalMobileNumber(tt.ReceiveNumber)).FirstOrDefault().DeliveryStatus = (int)GetDeliveryStatus(tt.DeliveryStatus);
					//numberStatus.Where(item => item.RecipientNumber == Helper.GetLocalMobileNumber(tt.ReceiveNumber)).First().DeliveryStatus = GetDeliveryStatus(tt.DeliveryStatus);
				}
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("\r\n<--<--<--<--<--<--<--<--<--<--<--"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("\r\nGetDeleviry : StackTrace : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("\r\nGetDeleviry : Message : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("\r\n<--<--<--<--<--<--<--<--<--<--<--"));
			}
		}

		private DeliveryStatus GetDeliveryStatus(string Status)
		{
			var statusType = (RahyabRGSmsDeliveryStatus)Enum.Parse(typeof(RahyabRGSmsDeliveryStatus), Status.Replace(' ', '_'));
			switch (statusType)
			{
				case RahyabRGSmsDeliveryStatus.MT_DELIVERED:
					return DeliveryStatus.SentAndReceivedbyPhone;
				case RahyabRGSmsDeliveryStatus.CHECK_OK:
					return DeliveryStatus.ReceivedByItc;
				case RahyabRGSmsDeliveryStatus.SMS_FAILED:
				case RahyabRGSmsDeliveryStatus.CHECK_ERROR:
					return DeliveryStatus.SentToItc;
				default:
					return DeliveryStatus.Sent;
			}
		}
	}
}
