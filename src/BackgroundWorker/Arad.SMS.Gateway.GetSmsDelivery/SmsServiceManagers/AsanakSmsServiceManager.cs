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
using Arad.SMS.Gateway.GetSmsDelivery.AsanakWebService;
using Arad.SMS.Gateway.SqlLibrary;
using System;
using System.Linq;

namespace Arad.SMS.Gateway.GetSmsDelivery
{
	public class AsanakSmsServiceManager : ISmsServiceManager
	{
		CompositeSmsGatewayService asanakService;
		userCredential userInfo;
		public AsanakSmsServiceManager()
		{
			asanakService = new CompositeSmsGatewayService();
			userInfo = new userCredential();
		}

		public void GetDeliveryStatus(BatchMessage batchMessage)
		{
			long[] messageID = batchMessage.Receivers.Where(sms => sms.DeliveryGetTime <= DateTime.Now).Select(sms => Helper.GetLong(sms.ReturnID)).ToArray();
			if (messageID.Count() == 0)
				return;

			userInfo.username = batchMessage.Username;
			userInfo.password = batchMessage.Password;

			getReportByMsgIdResult result = asanakService.getReportByMsgId(userInfo, messageID);

			if (result.status == 0 && result.reportItems.Count() > 0)
				for (int statusCounter = 0; statusCounter < result.reportItems.Length; statusCounter++)
					batchMessage.Receivers.Where(sms => Helper.GetLong(sms.ReturnID) == messageID[statusCounter]).First().DeliveryStatus = (int)GetDeliveryStatus(result.reportItems[statusCounter].status.ToString());
			else
				LogController<ServiceLogs>.LogInFile(ServiceLogs.DeliverySms, string.Format("Error:{0},status:{1}", result.errorMsg, result.status));
		}

		private Common.DeliveryStatus GetDeliveryStatus(string status)
		{
			switch (status)
			{
				case "":
				case "-1":
				case "1":
				case "4":
				case "9":
					return Common.DeliveryStatus.SentToItc;
				case "2":
				case "10":
				case "11":
				case "12":
				case "13":
					return Common.DeliveryStatus.ReceivedByItc;
				case "7":
				case "8":
					return Common.DeliveryStatus.DidNotReceiveToItc;
				case "6":
					return Common.DeliveryStatus.SentAndReceivedbyPhone;
				case "5":
					return Common.DeliveryStatus.HaveNotReceivedToPhone;
				default:
					return Common.DeliveryStatus.SentToItc;
			}
		}
	}
}
