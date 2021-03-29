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
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Arad.SMS.Gateway.WebApi.Controllers
{
	public class GsmController : ApiController
	{
		[HttpPost]
		//[IPAuthentication]
		public HttpResponseMessage SendReport(HttpRequestMessage request)
		{
			string strIncomingMessage = string.Empty;

			StreamReader MyStreamReader = new StreamReader(request.Content.ReadAsStreamAsync().Result);
			strIncomingMessage = MyStreamReader.ReadToEnd();

			dynamic result = JValue.Parse(strIncomingMessage);
			dynamic reportTasks = JArray.Parse(result.rpts.ToString()) as JArray;
			dynamic failedReportTask;
			dynamic successReportTask;
			DeliveryMessage deliveryMessage = new DeliveryMessage();
			deliveryMessage.FailedMobile = new List<string>();
			deliveryMessage.SuccessMobile = new List<string>();

			LogController<ServiceLogs>.LogInFile(ServiceLogs.WebAPI, string.Format("strIncomingMessage : {0}", strIncomingMessage));

			deliveryMessage.Agent = (int)SmsSenderAgentReference.GSMGateway;

			LogController<ServiceLogs>.LogInFile(ServiceLogs.WebAPI, string.Format("reportTasks[0].failed : {0}", reportTasks[0].failed));
			if (Helper.GetInt(reportTasks[0].failed) > 0)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.WebAPI, string.Format("reportTasks[0].failed is > 0"));
				failedReportTask = JArray.Parse(reportTasks[0].fdr.ToString()) as JArray;
				LogController<ServiceLogs>.LogInFile(ServiceLogs.WebAPI, string.Format("failedReportTask : {0}", failedReportTask));
				foreach (dynamic failedTask in failedReportTask)
				{
					LogController<ServiceLogs>.LogInFile(ServiceLogs.WebAPI, string.Format("failedTask[1] : {0}", failedTask[1].ToString()));
					deliveryMessage.FailedMobile.Add(Helper.GetLocalMobileNumber(failedTask[1].ToString()));
				}
			}

			LogController<ServiceLogs>.LogInFile(ServiceLogs.WebAPI, string.Format("reportTasks[0].sent : {0}", reportTasks[0].sent));
			if (Helper.GetInt(reportTasks[0].sent) > 0)
			{
				successReportTask = JArray.Parse(reportTasks[0].sdr.ToString()) as JArray;
				LogController<ServiceLogs>.LogInFile(ServiceLogs.WebAPI, string.Format("successReportTask : {0}", successReportTask));
				foreach (dynamic successTask in successReportTask)
				{
					LogController<ServiceLogs>.LogInFile(ServiceLogs.WebAPI, string.Format("successTask[1] : {0}", successTask[1].ToString()));
					deliveryMessage.SuccessMobile.Add(Helper.GetLocalMobileNumber(successTask[1].ToString()));
				}
			}

			LogController<ServiceLogs>.LogInFile(ServiceLogs.WebAPI, string.Format("batchid : {0}", reportTasks[0].tid.ToString()));
			deliveryMessage.BatchId = reportTasks[0].tid.ToString();

			ManageQueue.SendMessage(Queues.SaveGsmDelivery, deliveryMessage, string.Format("Agent:{0}=>SuccessMobile:{1}-FailedMobile:{2}-BatchId:{3}", deliveryMessage.Agent, deliveryMessage.SuccessMobile.Count, deliveryMessage.FailedMobile.Count, deliveryMessage.BatchId));
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpPost]
		[IPAuthentication]
		public HttpResponseMessage ReceiveMessage(HttpRequestMessage request)
		{
			string strIncomingMessage = string.Empty;

			StreamReader MyStreamReader = new StreamReader(request.Content.ReadAsStreamAsync().Result);
			strIncomingMessage = MyStreamReader.ReadToEnd();

			dynamic result = JValue.Parse(strIncomingMessage);
			dynamic reportTasks = JArray.Parse(result.sms.ToString()) as JArray;

			foreach (dynamic receiveSms in reportTasks)
			{
				ReceiveMessage receiveMessage = new ReceiveMessage();
				receiveMessage.Sender = receiveSms[3].ToString();
				receiveMessage.Receiver = receiveSms[1].ToString();
				receiveMessage.SmsText = Helper.Base64Decode(receiveSms[5].ToString());
				receiveMessage.ReceiveDateTime = DateTime.Now;
				receiveMessage.UDH = "";

				ManageQueue.SendMessage(Queues.ReceiveMessagesQueue, receiveMessage, string.Format("{0}=>{1}", receiveMessage.Sender, receiveMessage.Receiver));
			}
			return Request.CreateResponse(HttpStatusCode.OK);
		}
	}
}
