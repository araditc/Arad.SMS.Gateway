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

using Arad.SMS.Gateway.GeneralLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Xml.Linq;
using Arad.SMS.Gateway.WebApi.Models;

namespace Arad.SMS.Gateway.WebApi.Controllers
{
	public class ReceiveController : ApiController
	{
		[HttpPost]
		[ActionName("GetMessage")]
		[RequiredAuthentication]
		public HttpResponseMessage GetMessage(PostReceiveSmsModel receiveInfo)
		{
			ReceiveSmsResponseModel receivedSms;
			List<ReceiveSmsResponseModel> lstReceivedSms = new List<ReceiveSmsResponseModel>();

			if (receiveInfo == null)
				return Request.CreateResponse(HttpStatusCode.BadRequest);

			var principal = Thread.CurrentPrincipal;
			if (principal.Identity.IsAuthenticated)
			{
				Common.User userInfo = ((MyPrincipal)principal).UserDetails;

				string filePath = string.Format("{0}{1}{2}{1}{3}{1}Receive.xml",
																				ConfigurationManager.GetSetting("XmlReceiveFilePath"),
																				Path.DirectorySeparatorChar,
																				receiveInfo.Receiver,
																				receiveInfo.Date);

				if (!File.Exists(filePath))
					throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.ReceiveSmsIsEmpty, Language.GetString("ReceiveSmsIsEmpty"));

				XDocument doc = XDocument.Load(filePath);

				List<XElement> lstNodes = doc.Descendants("Sms")
																		.Where(element => Helper.GetGuid(element.Attribute("UserGuid").Value) == userInfo.UserGuid &&
																											Helper.GetBool(element.Attribute("IsRead").Value) == false &&
																											element.Attribute("Line").Value == receiveInfo.Receiver).Take(100).ToList();
				foreach (XElement x in lstNodes)
				{
					receivedSms = new ReceiveSmsResponseModel();

					receivedSms.From = x.Element("Sender").Value;
					receivedSms.To = x.Element("Receiver").Value;
					receivedSms.Text = x.Element("SmsText").Value;
					receivedSms.ReceiveDateTime = DateTime.Parse(x.Element("ReceiveDateTime").Value);

					lstReceivedSms.Add(receivedSms);

					doc.Descendants("Sms").Where(element => element.Attribute("Guid").Value == x.Attribute("Guid").Value).FirstOrDefault()
																.SetAttributeValue("IsRead", "1");
				}

				ReceiveSmsResponse response = new ReceiveSmsResponse();
				response.IsSuccessful = true;
				response.ReceivedMessages = lstReceivedSms;

				doc.Save(filePath);

				return Request.CreateResponse<ReceiveSmsResponse>(HttpStatusCode.Accepted, response);
			}
			else
				return Request.CreateResponse(HttpStatusCode.Accepted);
		}
	}
}