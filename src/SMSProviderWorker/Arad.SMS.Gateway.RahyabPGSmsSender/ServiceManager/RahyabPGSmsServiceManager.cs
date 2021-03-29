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
using Arad.SMS.Gateway.RahyabPGSmsSender.RahyabPGSmsWebService;
using Arad.SMS.Gateway.SqlLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace Arad.SMS.Gateway.RahyabPGSmsSender
{
	public class RahyabPGSmsServiceManager : ISmsServiceManager
	{
		private readonly string parameterPatern = "username={0}&password={1}&from={2}&to={3}&message={4}&farsi={5}";
		private sms smsService;

		public SmsSenderAgentReference SmsSenderAgentReference
		{
			get
			{
				return Common.SmsSenderAgentReference.RahyabPG;
			}
		}

		public RahyabPGSmsServiceManager()
		{
			smsService = new sms();
		}

		public void SendSms(BatchMessage batchMessage)
		{
			var responseData = string.Empty;
			string messageIDs = string.Empty;
			int counter = 0;
			string logFile = string.Format(@"{0}\{1}", ConfigurationManager.GetSetting("RecipientAddress"), batchMessage.Id);

			try
			{
				responseData = HttpGet(string.Format("{0}?{1}", batchMessage.SendLink,
															 string.Format(parameterPatern,
																						 batchMessage.Username,
																						 batchMessage.Password,
																						 batchMessage.SenderNumber,
																						 string.Join(";", batchMessage.Receivers.Select(receiver => receiver.RecipientNumber)),
																						 HttpUtility.UrlEncode(batchMessage.SmsText),
                                                                                         batchMessage.IsUnicode)));

				LogController<ServiceLogs>.LogInFile(logFile, string.Format("responseData={0}", responseData));

				if (responseData.StartsWith("Send OK."))
				{
					messageIDs = XDocument.Parse(responseData.Split('.')[1]).Element("ReturnIDs").Value;

					List<string> lstMessageIDs = messageIDs.Trim().Split(';').ToList();
					foreach (InProgressSms sms in batchMessage.Receivers)
					{
						sms.ReturnID = lstMessageIDs[counter];
						sms.DeliveryStatus = (int)DeliveryStatus.SentToItc;
						sms.DeliveryGetTime = DateTime.Now.AddMinutes(5);
						sms.SendTryCount += 1;
						if (lstMessageIDs[counter] == "-1")
							sms.SendStatus = (int)SendStatus.BlackList;
						else if (!string.IsNullOrEmpty(sms.ReturnID))
							sms.SendStatus = (int)SendStatus.Sent;
						counter++;
					}
				}
				else
				{
					string responseMessage = responseData.StartsWith("<message>") ? XElement.Parse(responseData).Value : responseData;
					foreach (InProgressSms smsItem in batchMessage.Receivers)
						CheckErrorSms(responseMessage, smsItem);
				}

				LogController<ServiceLogs>.LogInFile(logFile,
																						 string.Format("Id={0}Guid={1}Sms={2}Receivers={3}{4}",
																														batchMessage.Id,
																														batchMessage.Guid,
																														batchMessage.SmsText,
																														string.Join(";", batchMessage.Receivers.Select<InProgressSms, string>(sms => string.Format("{0}*{1}*{2}{3}", sms.RecipientNumber, sms.ReturnID, batchMessage.PageNo, Environment.NewLine))),
																														Environment.NewLine));
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabPG, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabPG, string.Format("\r\n{0} : Message : {1}", "RahyabPGWebService.sendsms", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabPG, string.Format("\r\n{0} : SmsSentGuid : {1}", "RahyabPGWebService.sendsms", batchMessage.Guid));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabPG, string.Format("\r\n-------------------------------------------------------------------------"));
			}
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

		private void CheckErrorSms(string errorMessage, InProgressSms smsItem)
		{
			smsItem.SendStatus = (int)SendStatus.ErrorInSending;
			smsItem.DeliveryStatus = (int)DeliveryStatus.NotSent;
			smsItem.SendTryCount += 1;

			switch (errorMessage.Trim())
			{
				case "Error! Authentication Failed.":
				case "Error! Account is not active.":
				case "Error! Account date is expired.":
					break;
				case "Error! Credit is not enough.":
					break;
				case "Error! Debug ID (123).":
				case "Error! Server temporarily out of service, please try later.":
				case "Error! ShortCode is invalid.":
				case "Error! IP is invalid.":
					break;
				case "Error!.":
					smsItem.DeliveryStatus = (int)DeliveryStatus.ErrorInSending;
					break;
				case "Error! Invalid XML file format.":
				case "Error! Valid XML not found.":
					smsItem.DeliveryStatus = (int)DeliveryStatus.InvalidInputXml;
					break;
				case "Error! Your message filtered because it has invalid content.":
					smsItem.DeliveryStatus = (int)DeliveryStatus.SmsIsFilter;
					break;
				case "Error! Valid cellphone not found or maybe blocked.":
					smsItem.DeliveryStatus = (int)DeliveryStatus.BlackList;
					break;
				case "Error! Valid cellphone not found.":
					smsItem.DeliveryStatus = (int)DeliveryStatus.InvalidMobile;
					break;
				case "Error! Invalid arguments length.":
					break;
				default:
					break;
			}
		}
	}
}
