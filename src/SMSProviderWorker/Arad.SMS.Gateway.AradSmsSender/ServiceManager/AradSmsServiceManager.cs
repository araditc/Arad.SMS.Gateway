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
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace Arad.SMS.Gateway.AradSmsSender
{
	public class Sms
	{
		public Guid CheckId { get; set; }
		public string SmsText { get; set; }
		public string Receivers { get; set; }
		public string SenderId { get; set; }
		public bool IsFlash { get; set; }
	}

	public class ResponseMessage
	{
		public bool IsSuccessful { get; set; }
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public Guid BatchId { get; set; }
		public decimal Credit { get; set; }
	}

	public class AradSmsServiceManager : ISmsServiceManager
	{
		public SmsSenderAgentReference SmsSenderAgentReference
		{
			get
			{
				return SmsSenderAgentReference.AradBulk;
			}
		}

		public void SendSms(BatchMessage batchMessage)
		{
			string logFile = string.Format(@"{0}\{1}", ConfigurationManager.GetSetting("RecipientAddress"), batchMessage.Id);
			try
			{
				Sms batch = new Sms();
				batch.CheckId = Helper.GetGuid(batchMessage.CheckId);
				batch.Receivers = string.Join(",", batchMessage.Receivers.Select(sms => sms.RecipientNumber).ToArray());
				batch.SmsText = batchMessage.SmsText;
				batch.IsFlash = batchMessage.IsFlash;
				batch.SenderId = batchMessage.SenderNumber;

				string dataToPost = CreateXML(batch);

				WebRequest request = WebRequest.Create("http://185.37.53.162:8080/Messages/Send");
				request.Method = "POST";
				request.ContentType = "text/xml";
				byte[] byt = System.Text.Encoding.UTF8.GetBytes(batchMessage.Username + ":" + batchMessage.Password);
				request.Headers.Add("authorization", "Basic " + Convert.ToBase64String(byt));
				Stream stream = request.GetRequestStream();
				StreamWriter streamWriter = new StreamWriter(stream);
				streamWriter.Write(dataToPost);
				streamWriter.Flush();
				streamWriter.Close();
				stream.Close();

				WebResponse response = request.GetResponse();
				Stream responseStream = response.GetResponseStream();
				StreamReader objStreamReader = new StreamReader(responseStream);
				string result = objStreamReader.ReadToEnd();
				objStreamReader.Close();
				responseStream.Close();
				response.Close();

				ResponseMessage responseMessage = DeserializeXml(result);


				foreach (InProgressSms smsItem in batchMessage.Receivers)
					CheckErrorSms(responseMessage, smsItem);

				LogController<ServiceLogs>.LogInFile(logFile,
																						 string.Format("Id={0}batchGuid={1}Sms={2}Receivers={3}{4}",
																													 batchMessage.Id,
																													 batchMessage.Guid,
																													 batchMessage.SmsText,
																													 string.Join(";", batchMessage.Receivers.Select<InProgressSms, string>(sms => string.Format("{0}*{1}*{2}{3}", sms.RecipientNumber, sms.ReturnID, batchMessage.PageNo, Environment.NewLine))),
																													 Environment.NewLine));
			}
			catch (Exception ex)
			{
				foreach (InProgressSms smsItem in batchMessage.Receivers)
					CheckErrorSms(null, smsItem);

				LogController<ServiceLogs>.LogInFile(ServiceLogs.Arad, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Arad, string.Format("\r\n{0} : Message : {1}", "aradWebService.SendSms", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Arad, string.Format("\r\n{0} : SmsSentGuid : {1}", "aradWebService.SendSms", batchMessage.Guid.ToString()));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Arad, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void CheckErrorSms(ResponseMessage responseMessage, InProgressSms smsItem)
		{
			smsItem.SendTryCount += 1;
			if (responseMessage == null)
			{
				smsItem.ReturnID = responseMessage.StatusCode.ToString();
				smsItem.SendStatus = (int)SendStatus.ErrorInSending;
				smsItem.DeliveryStatus = (int)DeliveryStatus.NotSent;
			}
			else if (!responseMessage.IsSuccessful)
			{
				smsItem.ReturnID = responseMessage.StatusCode.ToString();
				smsItem.SendStatus = (int)SendStatus.ErrorInSending;
				smsItem.DeliveryStatus = (int)DeliveryStatus.NotSent;
			}
			else if (responseMessage.IsSuccessful)
			{
				smsItem.ReturnID = responseMessage.BatchId.ToString();
				smsItem.DeliveryStatus = (int)DeliveryStatus.SentToItc;
				smsItem.DeliveryGetTime = DateTime.Now.AddMinutes(5);
			}
		}

		public string CreateXML(Object obj)
		{
			XmlDocument xmlDoc = new XmlDocument();
			XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
			using (MemoryStream xmlStream = new MemoryStream())
			{
				xmlSerializer.Serialize(xmlStream, obj);
				xmlStream.Position = 0;
				xmlDoc.Load(xmlStream);
				return xmlDoc.InnerXml;
			}
		}

		public ResponseMessage DeserializeXml(string xmlResult)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(ResponseMessage));
			StringReader rdr = new StringReader(xmlResult);
			ResponseMessage responseMessage = (ResponseMessage)serializer.Deserialize(rdr);
			return responseMessage;
		}
	}
}
