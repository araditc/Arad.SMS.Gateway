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
using Arad.SMS.Gateway.GeneralLibrary.Security;
using Arad.SMS.Gateway.SqlLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Xml;
using Arad.SMS.Gateway.WebApi.Models;

namespace Arad.SMS.Gateway.WebApi.Controllers
{

	public class MessagesController : ApiController
	{
		private static Random random = new Random();

        [HttpGet]
		[ActionName("SmsRelay")]
		[IPAuthentication]
		public HttpResponseMessage ReceiveMessage([FromUri]GetReceiveSmsModel receive)
		{
			ReceiveMessage receiveMessage = new ReceiveMessage();
			receiveMessage.Sender = receive.From;
			receiveMessage.Receiver = Helper.GetLocalPrivateNumber(receive.To);
			receiveMessage.SmsText = Helper.PersianNumberToEnglish(receive.Text);
            receiveMessage.ReceiveDateTime = DateTime.Now;
			receiveMessage.UDH = receive.UDH;

			ManageQueue.SendMessage(Queues.ReceiveMessagesQueue, receiveMessage, string.Format("{0}=>{1}", receiveMessage.Sender, receiveMessage.Receiver));
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpGet]
		[IPAuthentication]
		public HttpResponseMessage ReceiveSms(string destAddress, string srcAddress, string msgBody)
		{
			ReceiveMessage receiveMessage = new ReceiveMessage();
			receiveMessage.Sender = srcAddress;
			receiveMessage.Receiver = Helper.GetLocalPrivateNumber(destAddress);
			receiveMessage.SmsText = HttpUtility.UrlDecode(Helper.PersianNumberToEnglish(msgBody));
            receiveMessage.ReceiveDateTime = DateTime.Now;
			receiveMessage.UDH = string.Empty;

			ManageQueue.SendMessage(Queues.ReceiveMessagesQueue, receiveMessage, string.Format("{0}=>{1}", receiveMessage.Sender, receiveMessage.Receiver));
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpGet]
		[ActionName("DeliveryRelay")]
		[IPAuthentication]
		public HttpResponseMessage GetDelivery([FromUri]GetDeliverySmsModel delivery)
		{
			DeliveryMessage deliveryMessage = new DeliveryMessage();
			deliveryMessage.Agent = delivery.Agent;
			deliveryMessage.BatchId = delivery.ReturnId;
			deliveryMessage.ReturnId = delivery.ReturnId;
			deliveryMessage.CheckId = delivery.CheckId;
			deliveryMessage.Mobile = delivery.Mobile;
			deliveryMessage.Status = GetDeliveryStatus(delivery.Agent, Helper.GetInt(delivery.Status));
			deliveryMessage.Date = DateTime.Now;
			deliveryMessage.Time = DateTime.Now.TimeOfDay;

			ManageQueue.SendMessage(Queues.SaveDelivery, deliveryMessage, string.Format("Agent:{0}=>Mobile:{1}-ReturnId:{2}-Status:{3}", delivery.Agent, delivery.Mobile, delivery.ReturnId, delivery.Status));
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpPost]
		[ActionName("TrafficRelay")]
		[IPAuthentication]
		public HttpResponseMessage GetDelivery(HttpRequestMessage request)
		{
			string strIncomingMessage = string.Empty;
			string element = string.Empty;

			StreamReader MyStreamReader = new StreamReader(request.Content.ReadAsStreamAsync().Result);
			strIncomingMessage = MyStreamReader.ReadToEnd();
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.XmlResolver = null;
			settings.DtdProcessing = DtdProcessing.Parse;
			XmlReader objXMLReader = XmlReader.Create(new StringReader(strIncomingMessage), settings);

			while (objXMLReader.Read())
			{
				switch (objXMLReader.NodeType)
				{
					case XmlNodeType.Element:
						element = objXMLReader.Name;
						break;
				}
				if (!string.IsNullOrEmpty(element))
					break;
			}

			switch (element.ToLower())
			{
				case "smsdeliver":
					#region ReceiveSms
					ReceiveMessage receiveMessage = new ReceiveMessage();
					while (objXMLReader.Read())
					{
						switch (objXMLReader.NodeType)
						{
							case XmlNodeType.Element:
								element = objXMLReader.Name;
								if (element == "destAddrNbr")
									receiveMessage.Receiver = objXMLReader.Value;
								if (element == "origAddr")
									receiveMessage.Sender = objXMLReader.Value;
                                if (element == "message")
                                    receiveMessage.SmsText = objXMLReader.Value;

                                break;
							case XmlNodeType.Text:
								if (element == "destAddrNbr")
								{
									receiveMessage.Receiver = objXMLReader.Value;
									break;
								}
								if (element == "origAddr")
								{
									receiveMessage.Sender = objXMLReader.Value;
									break;
								}
								if (element == "message")
								{
									receiveMessage.SmsText = objXMLReader.Value;
                                    break;
								}
								break;
							case XmlNodeType.CDATA:
								if (element == "origAddr")
									receiveMessage.Sender = objXMLReader.Value;
                                if (element == "message")
                                    receiveMessage.SmsText = objXMLReader.Value;
                                    break;
						}
					}
					if (strIncomingMessage.IndexOf("binary=") != -1)
					{
						int index = strIncomingMessage.IndexOf("binary=") + 8;
						string strLang = strIncomingMessage.Substring(index, 1);
                        if (strLang == "t")
                        {
                            receiveMessage.SmsText = DecodeUCS2(receiveMessage.SmsText);
                            receiveMessage.SmsText = Helper.PersianNumberToEnglish(receiveMessage.SmsText);
                        }
					}
					receiveMessage.ReceiveDateTime = DateTime.Now;
                    receiveMessage.SmsText = Helper.PersianNumberToEnglish(receiveMessage.SmsText);

                    ManageQueue.SendMessage(Queues.ReceiveMessagesQueue, receiveMessage, receiveMessage.Sender);
					break;

				#endregion
				case "smsstatusnotification":
					#region DeliveryStatus
					Common.DeliveryMessage deliveryMessage = new Common.DeliveryMessage();
					deliveryMessage.Agent = (int)SmsSenderAgentReference.RahyabRG;
					while (objXMLReader.Read())
					{
						switch (objXMLReader.NodeType)
						{
							case XmlNodeType.Element:
								element = objXMLReader.Name;
								if (element == "batch")
									deliveryMessage.BatchId = objXMLReader.GetAttribute("batchID").Split('+')[1];
								if (element == "sms")
									deliveryMessage.ReturnId = objXMLReader.GetAttribute("smsId");
								if (element == "destAddr")
									deliveryMessage.Mobile = objXMLReader.Value;
								if (element == "status")
									deliveryMessage.Status = GetDeliveryStatus(objXMLReader.Value);
								if (element == "time")
									deliveryMessage.Date = !string.IsNullOrEmpty(objXMLReader.Value) ? DateManager.GetChristianDateForDB(objXMLReader.Value) : DateTime.Now;
								break;
							case XmlNodeType.CDATA:
								if (element == "destAddr")
									deliveryMessage.Mobile = objXMLReader.Value;
								break;
							case XmlNodeType.Text:
								if (element == "status")
									deliveryMessage.Status = GetDeliveryStatus(objXMLReader.Value);
								if (element == "time")
									deliveryMessage.Date = !string.IsNullOrEmpty(objXMLReader.Value) ? DateTime.Parse(objXMLReader.Value.Replace('-', '/')) : DateTime.Now;
								break;
						}
					}

					ManageQueue.SendMessage(Queues.SaveDelivery, deliveryMessage, deliveryMessage.ReturnId);
					break;

					#endregion
			}

			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpPost]
		[ActionName("Send")]
		[RequiredAuthentication]
		public HttpResponseMessage SendSms(PostSendSmsModel sms)
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

			if (sms.ReceiverList.Count == 0)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.ReceiversIsEmpty, Language.GetString("ReceiversIsEmpty"));

			if ((200 / sms.SmsLen) < sms.ReceiverList.Count)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.ReceiverCountNotValid, Language.GetString("ReceiverCountNotValid"));

			var principal = Thread.CurrentPrincipal;
			if (principal.Identity.IsAuthenticated)
			{
				User userInfo = ((MyPrincipal)principal).UserDetails;

				BatchMessage batchMessage = new BatchMessage();
				batchMessage.UserGuid = userInfo.UserGuid;
				batchMessage.Guid = sms.BatchId;
				batchMessage.CheckId = sms.CheckId.ToString();
				batchMessage.SmsText = sms.SmsText;
				batchMessage.SmsLen = sms.SmsLen;
				batchMessage.SenderNumber = sms.SenderId;
				batchMessage.PrivateNumberGuid = Facade.PrivateNumber.GetUserNumberGuid(batchMessage.SenderNumber, batchMessage.UserGuid);
				batchMessage.Receivers = sms.InProgressSmsList;
				batchMessage.IsUnicode = sms.IsUnicode;
				batchMessage.IsFlash = sms.IsFlash;
				batchMessage.ReferenceGuid = null;
				batchMessage.SmsSendType = (int)SmsSendType.SendSmsFromAPI;

				if (batchMessage.PrivateNumberGuid == Guid.Empty)
					throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.SenderIsInvalid, Language.GetString("SenderIsInvalid"));

				string messageLabel = (batchMessage.SenderNumber == null ? batchMessage.Guid.ToString() : batchMessage.SenderNumber);
                if (int.Parse(ConfigurationManager.GetSetting("ActiveQ")) == 0)
                {
                    if (!ManageQueue.SendMessage(Queues.ApiSendMessage, batchMessage, messageLabel))
                        throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.InternalServerError, Language.GetString("InternalServerError"));
                }else if (int.Parse(ConfigurationManager.GetSetting("ActiveQ")) == 1)
                {
                    SaveToDB(batchMessage);
                }

				SendSmsResponse sendsms = new SendSmsResponse();
				sendsms.IsSuccessful = true;
				sendsms.BatchId = batchMessage.Guid;

				return Request.CreateResponse<SendSmsResponse>(HttpStatusCode.Created, sendsms);
			}
			else
				return Request.CreateResponse(HttpStatusCode.Accepted);
		}

        private void SaveToDB(BatchMessage messages)
        {
            Common.ScheduledSms scheduledSms;
            List<string> lstReceivers;
                scheduledSms = new Common.ScheduledSms();
                lstReceivers = new List<string>();

                LogController<ServiceLogs>.LogInFile(ServiceLogs.WebAPI, string.Format("\r\n-------------------------------------------------------------------------"));
                scheduledSms.ScheduledSmsGuid = messages.Guid;
                scheduledSms.PrivateNumberGuid = messages.PrivateNumberGuid;
                scheduledSms.SmsText = messages.SmsText;
                scheduledSms.SmsLen = messages.SmsLen;
                scheduledSms.PresentType = messages.IsFlash ? (int)Business.Messageclass.Flash : (int)Business.Messageclass.Normal;
                scheduledSms.Encoding = messages.IsUnicode ? (int)Business.Encoding.Utf8 : (int)Business.Encoding.Default;
                scheduledSms.UserGuid = messages.UserGuid;
                scheduledSms.Status = (int)ScheduledSmsStatus.Stored;
                scheduledSms.DateTimeFuture = DateTime.Now;
                scheduledSms.TypeSend = messages.SmsSendType;

                LogController<ServiceLogs>.LogInFile(ServiceLogs.WebAPI, string.Format(@"batchMessage.ServiceId : {0},ScheduledSmsGuid : {1},PrivateNumberGuid: {2}", messages.ServiceId, scheduledSms.ScheduledSmsGuid, scheduledSms.PrivateNumberGuid));

                if (messages.ServiceId == null)
                {
                    LogController<ServiceLogs>.LogInFile(ServiceLogs.WebAPI, string.Format("batchMessage.sendernumber : {0},batchMessage.UserGuid:{1}", messages.SenderNumber, messages.UserGuid));
                    scheduledSms.PrivateNumberGuid = Facade.PrivateNumber.GetUserNumberGuid(messages.SenderNumber, messages.UserGuid);
                    LogController<ServiceLogs>.LogInFile(ServiceLogs.WebAPI, string.Format("scheduledSms.PrivateNumberGuid : {0} SendType : {1}", scheduledSms.PrivateNumberGuid, (SmsSendType)messages.SmsSendType));

                    if (messages.SmsSendType == (int)SmsSendType.SendSmsFromAPI)
                    {
                        lstReceivers = messages.Receivers.Select<InProgressSms, string>(receiver => receiver.RecipientNumber).DefaultIfEmpty().ToList();
                        Facade.ScheduledSms.InsertSms(scheduledSms, lstReceivers);
                    }
                    else if (messages.SmsSendType == (int)SmsSendType.SendGroupSmsFromAPI)
                    {
                        scheduledSms.ReferenceGuid = string.Join(",", messages.ReferenceGuid);
                        Facade.ScheduledSms.InsertGroupSms(scheduledSms);
                    }
                }
                else
                {
                    if (messages.Receivers.Count > 0)
                    {
                        lstReceivers = messages.Receivers.Select<InProgressSms, string>(receiver => receiver.RecipientNumber).ToList();
                        Facade.ScheduledSms.InsertSms(scheduledSms, lstReceivers);
                    }
                    else
                    {
                        scheduledSms.ReferenceGuid = messages.ReferenceGuid.ToString();
                        scheduledSms.TypeSend = (int)SmsSendType.SendGroupSmsFromAPI;
                        Facade.ScheduledSms.InsertGroupSms(scheduledSms);
                    }
                }


        }

        [HttpPost]
		[RequiredAuthentication]
		public HttpResponseMessage GetBulkMessages()
		{
			MessageQueue queue;
			BatchMessage batchMessage = new BatchMessage();
			BulkSmsModel bulkInfo = new BulkSmsModel();

			string queuePath = string.Format(@".\private$\SocialNetworks-bulk");

			var principal = Thread.CurrentPrincipal;
			if (!principal.Identity.IsAuthenticated)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.AccountIsInvalid, Language.GetString("AccountIsInvalid"));

			User userInfo = ((MyPrincipal)principal).UserDetails;
			if (!SecurityManager.HasServicePermission(userInfo.UserGuid, (int)Business.Services.GetBulkMessagesFromAPI))
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.AccessDenied, Language.GetString("AccessDenied"));

			if (!MessageQueue.Exists(queuePath))
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.InternalServerError, Language.GetString("InternalServerError"));

			queue = new MessageQueue(queuePath);
			queue.Formatter = new BinaryMessageFormatter();

			BulkSmsResponse response = new BulkSmsResponse();
			response.IsSuccessful = true;

			var msgEnumerator = queue.GetMessageEnumerator2();
			while (msgEnumerator.MoveNext(new TimeSpan(0, 0, 1)))
			{
				using (var msg = msgEnumerator.Current)
				{
					msg.Formatter = new BinaryMessageFormatter();
					batchMessage = (BatchMessage)msg.Body;

					bulkInfo.Id = batchMessage.Id;
					bulkInfo.IsUnicode = batchMessage.IsUnicode;
					bulkInfo.SmsLen = batchMessage.SmsLen;
					bulkInfo.SmsText = batchMessage.SmsText;
					bulkInfo.TotalCount = batchMessage.TotalCount;
					bulkInfo.Status = SendStatus.IsBeingSent;
					bulkInfo.Receivers = string.Join(",", batchMessage.Receivers.Where(receiver => !Helper.GetBool(receiver.IsBlackList)).Select(receiver => receiver.RecipientNumber).ToList());

					response.BulkInfo = bulkInfo;

					Parallel.ForEach<InProgressSms>(batchMessage.Receivers, (receiver) =>
					{
						if (!Helper.GetBool(receiver.IsBlackList))
						{
							receiver.ReturnID = random.Next().ToString();
							receiver.SendStatus = (int)SendStatus.Sent;
							receiver.DeliveryStatus = (int)DeliveryStatus.SentToItc;
							receiver.SendTryCount += 1;
						}
						else
						{
							receiver.SendStatus = (int)SendStatus.BlackList;
							receiver.DeliveryStatus = (int)DeliveryStatus.BlackListTable;
							receiver.SendTryCount += 1;
						}
					});

					SqlLibrary.ManageQueue.SendMessage(ConfigurationManager.GetSetting("SentMessageQueue"), batchMessage, string.Format("{0}-{1}", batchMessage.Id, batchMessage.PageNo));
					queue.ReceiveById(msg.Id);
					break;
				}
			}

			return Request.CreateResponse<BulkSmsResponse>(HttpStatusCode.Accepted, response);
		}

		[HttpPost]
		[RequiredAuthentication]
		public HttpResponseMessage ConfirmBulk(BulkSmsModel bulkInfo)
		{
			var principal = Thread.CurrentPrincipal;
			if (!principal.Identity.IsAuthenticated)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.AccountIsInvalid, Language.GetString("AccountIsInvalid"));

			User userInfo = ((MyPrincipal)principal).UserDetails;
			if (!SecurityManager.HasServicePermission(userInfo.UserGuid, (int)Business.Services.GetBulkMessagesFromAPI))
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.AccessDenied, Language.GetString("AccessDenied"));

			if (bulkInfo.Id == 0)
				return Request.CreateResponse(HttpStatusCode.BadRequest);

			if (!ManageQueue.SendMessage(Queues.ConfirmBulk, bulkInfo, bulkInfo.Id.ToString()))
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.InternalServerError, Language.GetString("InternalServerError"));

			return Request.CreateResponse(HttpStatusCode.OK);
		}

		[HttpPost]
		[ActionName("SendGroup")]
		[RequiredAuthentication]
		public HttpResponseMessage SendGroupSms(PostSendGroupSmsModel sms)
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

			if (sms.ListOfPhoneBooks.Count == 0)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.PhoneBookGroupIsEmpty, Language.GetString("PhoneBookGroupIsEmpty"));

			var principal = Thread.CurrentPrincipal;
			if (principal.Identity.IsAuthenticated)
			{
				Common.User userInfo = ((MyPrincipal)principal).UserDetails;

				BatchMessage batchMessage = new BatchMessage();
				batchMessage.UserGuid = userInfo.UserGuid;
				batchMessage.Guid = sms.BatchId;
				batchMessage.CheckId = sms.CheckId.ToString();
				batchMessage.SmsText = sms.SmsText;
				batchMessage.SmsLen = sms.SmsLen;
				batchMessage.SenderNumber = sms.SenderId;
				batchMessage.PrivateNumberGuid = Facade.PrivateNumber.GetUserNumberGuid(batchMessage.SenderNumber, batchMessage.UserGuid);
				batchMessage.ReferenceGuid = sms.ListOfPhoneBooks;
				batchMessage.IsUnicode = sms.IsUnicode;
				batchMessage.IsFlash = sms.IsFlash;
				batchMessage.SmsSendType = (int)SmsSendType.SendGroupSmsFromAPI;

				if (batchMessage.PrivateNumberGuid == Guid.Empty)
					throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.SenderIsInvalid, Language.GetString("SenderIsInvalid"));

				string messageLabel = (batchMessage.SenderNumber == null ? batchMessage.Guid.ToString() : batchMessage.SenderNumber);
                if (int.Parse(ConfigurationManager.GetSetting("ActiveQ")) == 0)
                {
                    if (!ManageQueue.SendMessage(Queues.ApiSendMessage, batchMessage, messageLabel))
					throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.InternalServerError, Language.GetString("InternalServerError"));
                }
                else if (int.Parse(ConfigurationManager.GetSetting("ActiveQ")) == 1)
                {
                    SaveToDB(batchMessage);
                }


                SendSmsResponse sendsms = new SendSmsResponse();
				sendsms.IsSuccessful = true;
				sendsms.BatchId = batchMessage.Guid;

				return Request.CreateResponse<SendSmsResponse>(HttpStatusCode.Created, sendsms);
			}
			else
				return Request.CreateResponse(HttpStatusCode.Accepted);
		}

		[HttpGet]
		[RequiredAuthentication]
		public HttpResponseMessage SendViaURL([FromUri] GetSendSmsModel sms)
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

			if (sms.ReceiverList.Count == 0)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.ReceiversIsEmpty, Language.GetString("ReceiversIsEmpty"));

			if ((200 / sms.SmsLen) < sms.ReceiverList.Count)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.ReceiverCountNotValid, Language.GetString("ReceiverCountNotValid"));

			var principal = Thread.CurrentPrincipal;
			if (principal.Identity.IsAuthenticated)
			{
				Common.User userInfo = ((MyPrincipal)principal).UserDetails;

				BatchMessage batchMessage = new BatchMessage();
				batchMessage.UserGuid = userInfo.UserGuid;
				batchMessage.Guid = sms.BatchId;
				batchMessage.CheckId = sms.CheckId.ToString();
				batchMessage.SmsText = sms.SmsText;
				batchMessage.SmsLen = sms.SmsLen;
				batchMessage.SenderNumber = sms.SenderId;
				batchMessage.PrivateNumberGuid = Facade.PrivateNumber.GetUserNumberGuid(batchMessage.SenderNumber, batchMessage.UserGuid);
				batchMessage.Receivers = sms.InProgressSmsList;
				batchMessage.IsUnicode = sms.IsUnicode;
				batchMessage.IsFlash = sms.IsFlash;
				batchMessage.ReferenceGuid = null;
				batchMessage.SmsSendType = (int)SmsSendType.SendSmsFromAPI;

				if (batchMessage.PrivateNumberGuid == Guid.Empty)
					throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.SenderIsInvalid, Language.GetString("SenderIsInvalid"));

				string messageLabel = (batchMessage.SenderNumber == null ? batchMessage.Guid.ToString() : batchMessage.SenderNumber);
                if (int.Parse(ConfigurationManager.GetSetting("ActiveQ")) == 0)
                {
                    if (!ManageQueue.SendMessage(Queues.ApiSendMessage, batchMessage, messageLabel))
                        throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.InternalServerError, Language.GetString("InternalServerError"));
                }
                else if (int.Parse(ConfigurationManager.GetSetting("ActiveQ")) == 1)
                {
                    SaveToDB(batchMessage);
                }

                SendSmsResponse sendsms = new SendSmsResponse();
				sendsms.IsSuccessful = true;
				sendsms.BatchId = batchMessage.Guid;

                return Request.CreateResponse<SendSmsResponse>(HttpStatusCode.Created, sendsms);
			}
			else
				return Request.CreateResponse(HttpStatusCode.Accepted);
		}

        [HttpPost]
		[RequiredAuthentication]
		public HttpResponseMessage GetUserInfo()
		{
			var principal = Thread.CurrentPrincipal;
			if (principal.Identity.IsAuthenticated)
			{
				Common.User user = ((MyPrincipal)principal).UserDetails;

				UserInfoResponse userInfo = new UserInfoResponse();
				userInfo.IsSuccessful = true;
				userInfo.Credit = user.Credit;
				userInfo.FirstName = user.FirstName;
				userInfo.LastName = user.LastName;
				userInfo.Mobile = user.Mobile;
				userInfo.Email = user.Email;
				userInfo.ExpireDate = user.ExpireDate;
				userInfo.Domain = user.DomainGuid.ToString();

				return Request.CreateResponse(HttpStatusCode.OK, userInfo);
			}
			else
				return Request.CreateResponse(HttpStatusCode.Accepted);
		}

        [HttpGet]
        [RequiredAuthentication]
        public HttpResponseMessage CheckUserLoginInfo()
        {
            var principal = Thread.CurrentPrincipal;
            if (principal.Identity.IsAuthenticated)
            {
                Common.User user = ((MyPrincipal)principal).UserDetails;

                UserInfoResponse userInfo = new UserInfoResponse();
                userInfo.IsSuccessful = true;
                userInfo.Credit = user.Credit;
                userInfo.FirstName = user.FirstName;
                userInfo.LastName = user.LastName;
                userInfo.Mobile = user.Mobile;
                userInfo.Email = user.Email;
                userInfo.ExpireDate = user.ExpireDate;
                userInfo.Domain = user.DomainGuid.ToString();

                return Request.CreateResponse(HttpStatusCode.OK, userInfo);
            }
            else
                return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        [HttpPost]
		[RequiredAuthentication]
		public HttpResponseMessage GetCredit()
		{
			var principal = Thread.CurrentPrincipal;
			if (principal.Identity.IsAuthenticated)
			{
				Common.User userInfo = ((MyPrincipal)principal).UserDetails;

				UserCreditResponse userCredit = new UserCreditResponse();
				userCredit.IsSuccessful = true;
				userCredit.Credit = userInfo.Credit;
				return Request.CreateResponse(HttpStatusCode.OK, userCredit);
			}
			else
				return Request.CreateResponse(HttpStatusCode.Accepted);
		}

		public string PAKService([FromUri] GetPAKServiceModel service)
		{
			try
			{
				bool numberExist = false;
				string serviceId = Facade.PrivateNumber.GetServiceId(Business.TypeServiceId.AggServiceId, service.PAKServiceId);
				if (string.IsNullOrEmpty(serviceId))
					return "-1";
				else
				{
					if (service.Type == 0)//UnSubscribe
					{
						Facade.PhoneBook.UnSubscribeService(service.Mobile, serviceId, ref numberExist);
						return !numberExist ? "-2" : "0";
					}
					else if (service.Type == 1)//Register
					{
						Facade.PhoneBook.RegisterService(service.Mobile, serviceId, ref numberExist);
						return numberExist ? "-3" : "1";
					}
					else
						return "-9";
				}
			}
			catch
			{
				return "-9";
			}
		}

		private int GetDeliveryStatus(int agent, int status)
		{
			switch (agent)
			{
				case (int)SmsSenderAgentReference.Magfa:
					switch (status)
					{
						case 1:
							return (int)DeliveryStatus.SentAndReceivedbyPhone;
						case 2:
							return (int)DeliveryStatus.HaveNotReceivedToPhone;
						case 8:
							return (int)DeliveryStatus.ReceivedByItc;
						case 16:
							return (int)DeliveryStatus.DidNotReceiveToItc;
						default:
							return status;
					}
				case (int)SmsSenderAgentReference.AradBulk:
				case (int)SmsSenderAgentReference.RahyabPG:
					switch (status.ToString())
					{
						case "-1":
							return (int)DeliveryStatus.Expired;
						case "0":
							return (int)DeliveryStatus.Sent;
						case "2":
							return (int)DeliveryStatus.SentAndReceivedbyPhone;
						case "5":
							return (int)DeliveryStatus.HaveNotReceivedToPhone;
						case "9":
							return (int)DeliveryStatus.BlackList;
						default:
							return status;
					}
				case (int)SmsSenderAgentReference.SLS:
					switch (status)
					{
						case 40:
							return (int)DeliveryStatus.WaitingForSend;
						case 41:
							return (int)DeliveryStatus.Sent;
						case 42:
							return (int)DeliveryStatus.NotSent;
						case 43:
							return (int)DeliveryStatus.Expired;
						case 44:
							return (int)DeliveryStatus.IsSending;
						case 45:
							return (int)DeliveryStatus.IsCanceled;
						case 46:
							return (int)DeliveryStatus.BlackList;
						case 47:
							return (int)DeliveryStatus.ErrorInSending;
						case 48:
							return (int)DeliveryStatus.SentAndReceivedbyPhone;
						case 52:
							return (int)DeliveryStatus.IsDeleted;
						default:
							return status;
					}
				case (int)SmsSenderAgentReference.Armaghan:
					switch (status)
					{
						case 0:
						case 5:
						case 4:
							return (int)DeliveryStatus.SentToItc;
						case 1:
							return (int)DeliveryStatus.ReceivedByItc;
						case 2:
							return (int)DeliveryStatus.SentAndReceivedbyPhone;
						case 3:
							return (int)DeliveryStatus.HaveNotReceivedToPhone;
						case 6:
						case 8:
							return (int)DeliveryStatus.IsCanceled;
						case 7:
							return (int)DeliveryStatus.Expired;
						default:
							return status;
					}
				default:
					return status;
			}
		}

		private int GetDeliveryStatus(string status)
		{
			switch (status.ToLower())
			{
				case "mt_delivered":
					return (int)DeliveryStatus.SentAndReceivedbyPhone;
				case "pending":
					return (int)DeliveryStatus.SentToItc;
				case "gw-failed":
					return (int)DeliveryStatus.BlackList;
				case "failed":
					return (int)DeliveryStatus.HaveNotReceivedToPhone;
				default:
					return (int)DeliveryStatus.Sent;
			}
		}

		private string DecodeUCS2(string Content)
		{
			string hextext = Content;
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hextext.Length; i += 4)
			{
				string hs = hextext.Substring(i, 4);
				sb.Append(Convert.ToChar(Convert.ToUInt32(hs, 16)));
			}
			string ascii = sb.ToString();
			return ascii;
		}
	}
}
