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
using System.IO;
using System.Linq;
using System.Net;

namespace Arad.SMS.Gateway.GetSmsDelivery
{
	public class RahyabPGSmsServiceManager : ISmsServiceManager
	{
		private readonly string parameterPatern = "Username={0}&ReturnIDs={1}";

		public void GetDeliveryStatus(BatchMessage batchMessage)
		{
			string[] messageID = batchMessage.Receivers.Where(sms => sms.DeliveryGetTime <= DateTime.Now).Select(sms => sms.ReturnID).ToArray();
			if (messageID.Count() == 0)
				return;

			string[] status = HttpGet(string.Format("{0}?{1}", batchMessage.DeliveryLink,
																string.Format(parameterPatern,
																							batchMessage.Username,
																							string.Join(";", messageID)))).Split(';');

			for (int statusCounter = 0; statusCounter < status.Length; statusCounter++)
				batchMessage.Receivers.Where(sms => sms.ReturnID == messageID[statusCounter]).First().DeliveryStatus = (int)GetDeliveryStatus(status[statusCounter]);
		}

		private string HttpGet(string Url)
		{
			string result;
			try
			{
				var Request = WebRequest.Create(Url) as HttpWebRequest;
				Request.KeepAlive = false;
				Request.MaximumResponseHeadersLength = Int16.MaxValue;
				using (var Response = Request.GetResponse() as HttpWebResponse)
				{
					var reader = new StreamReader(stream: Response.GetResponseStream());
					result = reader.ReadToEnd();
					reader.Close();
				}
			}
			catch (Exception exp)
			{
				result = "Error:" + exp.Message;
			}
			return result;
		}

		private Common.DeliveryStatus GetDeliveryStatus(string status)
		{
			switch (status)
			{
				case "":
				case "-1":
					return Common.DeliveryStatus.SentToItc;
				case "0":
					return Common.DeliveryStatus.ReceivedByItc;
				case "2":
					return Common.DeliveryStatus.SentAndReceivedbyPhone;
				case "5":
					return Common.DeliveryStatus.HaveNotReceivedToPhone;
				case "9":
					return Common.DeliveryStatus.BlackList;
				default:
					return Common.DeliveryStatus.SentToItc;
			}
		}
	}
}
