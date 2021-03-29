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
using Arad.SMS.Gateway.SqlLibrary;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Arad.SMS.Gateway.WebApi.Models;

namespace Arad.SMS.Gateway.WebApi.Controllers
{
	public class VASMessagesController : ApiController
	{
		//[HttpPost]
		//[RequiredAuthenticationAttribute]
		//public HttpResponseMessage Register(VASParameter param)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		ResponseMessage rm = new ResponseMessage();
		//		rm.IsSuccessful = false;
		//		rm.StatusCode = (int)ErrorCode.ValidationNotValid;
		//		foreach (ModelState modelState in ModelState.Values)
		//		{
		//			foreach (ModelError error in modelState.Errors)
		//				rm.Message += string.Format("{0}", error.ErrorMessage);
		//		}
		//		throw new BusinessException(HttpStatusCode.BadRequest, ErrorCode.ValidationNotValid, rm.Message);
		//	}

		//	if (Facade.PhoneBook.RegisterService(param.Mobile, param.ServiceId))
		//		return Request.CreateResponse(HttpStatusCode.OK);
		//	else
		//		return Request.CreateResponse(HttpStatusCode.InternalServerError);
		//}

		//[HttpPost]
		//[RequiredAuthenticationAttribute]
		//public HttpResponseMessage UnSubscribe(VASParameter param)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		ResponseMessage rm = new ResponseMessage();
		//		rm.IsSuccessful = false;
		//		rm.StatusCode = (int)ErrorCode.ValidationNotValid;
		//		foreach (ModelState modelState in ModelState.Values)
		//		{
		//			foreach (ModelError error in modelState.Errors)
		//				rm.Message += string.Format("{0}", error.ErrorMessage);
		//		}
		//		throw new BusinessException(HttpStatusCode.BadRequest, ErrorCode.ValidationNotValid, rm.Message);
		//	}

		//	if (Facade.PhoneBook.UnSubscribeService(param.Mobile, param.ServiceId))
		//		return Request.CreateResponse(HttpStatusCode.OK);
		//	else
		//		return Request.CreateResponse(HttpStatusCode.InternalServerError);
		//}

		[HttpPost]
		[ActionName("Send")]
		[RequiredAuthentication]
		public HttpResponseMessage Send(PostSendVASMessageModel vas)
		{
			if (!ModelState.IsValid)
			{
				ResponseMessage rm = new ResponseMessage();
				rm.IsSuccessful = false;
				rm.StatusCode = (int)ErrorCode.ValidationNotValid;
				foreach (ModelState modelState in ModelState.Values)
				{
					foreach (ModelError error in modelState.Errors)
						rm.Message += string.Format("{0}", error.ErrorMessage);
				}
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.ValidationNotValid, rm.Message);
			}

			if (string.IsNullOrEmpty(vas.SenderId) || vas.PrivateNumberGuid == Guid.Empty)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.ServiceIdIsInvalid, Language.GetString("ServiceIdIsInvalid"));

			if (vas.ReferenceGuid == Guid.Empty)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.ServiceIdIsInvalid, Language.GetString("ServiceIdIsInvalid"));

			if (vas.InProgressSmsList.Count > 0)
			{
				if (Facade.PhoneBook.RecipientIsRegisteredToVasGroup(vas.GroupId, vas.Receiver) == Guid.Empty)
					throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.ServiceIdIsInvalid, Language.GetString("ServiceIdIsInvalid"));
			}

			if (vas.InProgressSmsList.Count == 0 && !string.IsNullOrEmpty(vas.Receiver))
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.ReceiversIsEmpty, Language.GetString("ReceiversIsEmpty"));

			var principal = System.Threading.Thread.CurrentPrincipal;
			if (principal.Identity.IsAuthenticated)
			{
				Common.User userInfo = ((MyPrincipal)principal).UserDetails;

				BatchMessage batchMessage = new BatchMessage();
				batchMessage.UserGuid = userInfo.UserGuid;
				batchMessage.Guid = vas.BatchId;
				batchMessage.CheckId = vas.CheckId.ToString();
				batchMessage.SmsText = vas.SmsText;
				batchMessage.SmsLen = vas.SmsLen;
				batchMessage.SenderNumber = vas.SenderId;
				batchMessage.PrivateNumberGuid = vas.PrivateNumberGuid;
				batchMessage.ServiceId = vas.ServiceId;
				batchMessage.Receivers = vas.InProgressSmsList;
				batchMessage.IsUnicode = vas.IsUnicode;
				batchMessage.ReferenceGuid = new List<Guid>() { vas.ReferenceGuid };
				batchMessage.SmsSendType = (int)Common.SmsSendType.SendSmsFromAPI;

				if (!ManageQueue.SendMessage(Queues.ApiSendMessage, batchMessage, batchMessage.SenderNumber))
					throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.InternalServerError, Language.GetString("InternalServerError"));

				SendSmsResponse sendsms = new SendSmsResponse();
				sendsms.IsSuccessful = true;
				sendsms.BatchId = batchMessage.Guid;

				return Request.CreateResponse<SendSmsResponse>(HttpStatusCode.Created, sendsms);
			}
			else
				return Request.CreateResponse(HttpStatusCode.Unauthorized);
		}
	}
}
