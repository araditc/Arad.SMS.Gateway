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

using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Arad.SMS.Gateway.WebApi.Models;
using System.Linq;

namespace Arad.SMS.Gateway.WebApi.Controllers
{
	public class PhoneBookController : ApiController
	{
		[HttpPost]
		[RequiredAuthentication]
		public HttpResponseMessage GetPhoneBook(PhoneBookModel phoneBook)
		{
			var principal = Thread.CurrentPrincipal;
			List<PhoneBookResponseModel> lstGroups = new List<PhoneBookResponseModel>();
			PhoneBookResponseModel group;
			if (principal.Identity.IsAuthenticated)
			{
				Common.User user = ((MyPrincipal)principal).UserDetails;

				DataTable dtGroups = Facade.PhoneBook.GetPhoneBookOfUser(user.UserGuid, phoneBook.PhoneBookGuid, string.Empty);
				foreach (DataRow row in dtGroups.Rows)
				{
					group = new PhoneBookResponseModel();
					group.PhoneBookGuid = Helper.GetGuid(row["Guid"]);
					group.Name = row["Name"].ToString();

					lstGroups.Add(group);
				}

				PhoneBookResponse response = new PhoneBookResponse();
				response.IsSuccessful = true;
				response.PhoneBooks = lstGroups;

				return Request.CreateResponse(HttpStatusCode.OK, response);
			}
			else
				return Request.CreateResponse(HttpStatusCode.Accepted);
		}

		[HttpPost]
		[RequiredAuthentication]
		public HttpResponseMessage AddGroup(PhoneBookModel phonebookInfo)
		{
			Common.PhoneBook phoneBook = new Common.PhoneBook();
			PhoneBookResponseModel phonebookResponse = new PhoneBookResponseModel();
			List<PhoneBookResponseModel> lstGroups = new List<PhoneBookResponseModel>();

			var principal = Thread.CurrentPrincipal;
			if (!principal.Identity.IsAuthenticated)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.AccountIsInvalid, Language.GetString("AccountIsInvalid"));

			if (string.IsNullOrEmpty(phonebookInfo.Name))
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.BadRequest, Language.GetString("BadRequest"));

			Common.User userInfo = ((MyPrincipal)principal).UserDetails;

			phoneBook.Name = phonebookInfo.Name;
			phoneBook.ParentGuid = Guid.Empty;
			phoneBook.IsPrivate = false;
			phoneBook.UserGuid = userInfo.UserGuid;
			phoneBook.CreateDate = DateTime.Now;
			phoneBook.Type = (int)PhoneBookGroupType.Normal;
			Guid guid = Facade.PhoneBook.InsertItemInPhoneBook(phoneBook);

			phonebookResponse.Name = phoneBook.Name;
			phonebookResponse.PhoneBookGuid = guid;

			lstGroups.Add(phonebookResponse);
			PhoneBookResponse response = new PhoneBookResponse();
			response.IsSuccessful = true;
			response.PhoneBooks = lstGroups;

			return Request.CreateResponse(HttpStatusCode.OK, response);
		}

		[HttpPost]
		[RequiredAuthentication]
		public HttpResponseMessage RemoveGroup(PhoneBookModel phonebookInfo)
		{
			var principal = Thread.CurrentPrincipal;
			if (!principal.Identity.IsAuthenticated)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.AccountIsInvalid, Language.GetString("AccountIsInvalid"));

			Common.User userInfo = ((MyPrincipal)principal).UserDetails;

			Facade.PhoneBook.DeleteItemFromPhoneBook(phonebookInfo.PhoneBookGuid);

			PhoneBookResponse response = new PhoneBookResponse();
			response.IsSuccessful = true;

			return Request.CreateResponse(HttpStatusCode.OK, response);
		}

		[HttpPost]
		[RequiredAuthentication]
		public HttpResponseMessage Register(PhoneBookModel phonebookInfo)
		{
			Common.PhoneNumber phoneNumber;
			List<Common.PhoneNumber> lstNumbers = new List<Common.PhoneNumber>();

			var principal = Thread.CurrentPrincipal;
			if (!principal.Identity.IsAuthenticated)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.AccountIsInvalid, Language.GetString("AccountIsInvalid"));

			if (phonebookInfo.PhoneBookGuid == Guid.Empty ||
					phonebookInfo.Numbers.Count > 100 ||
					phonebookInfo.Numbers.Count == 0)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.BadRequest, Language.GetString("BadRequest"));

			Common.User userInfo = ((MyPrincipal)principal).UserDetails;

			Parallel.ForEach<PostPhoneNumberModel>(phonebookInfo.Numbers, (numberInfo) =>
			{
				numberInfo.Mobile = Helper.GetLocalMobileNumber(numberInfo.Mobile);
				phoneNumber = new Common.PhoneNumber();
				phoneNumber.PhoneNumberGuid = Guid.NewGuid();
				phoneNumber.FirstName = numberInfo.FirstName;
				phoneNumber.LastName = numberInfo.LastName;
				phoneNumber.BirthDate = numberInfo.BirthDate;
				phoneNumber.CreateDate = DateTime.Now;
				phoneNumber.Telephone = numberInfo.Telephone;
				phoneNumber.CellPhone = numberInfo.Mobile;
				phoneNumber.FaxNumber = numberInfo.FaxNumber;
				phoneNumber.Job = numberInfo.Job;
				phoneNumber.Address = numberInfo.Address;
				phoneNumber.Email = numberInfo.Email;
				phoneNumber.Sex = Helper.GetByte(numberInfo.Gender);
				phoneNumber.PhoneBookGuid = phonebookInfo.PhoneBookGuid;

				if (Helper.IsCellPhone(numberInfo.Mobile) != 0)
					lstNumbers.Add(phoneNumber);
			});

			if (lstNumbers.Count == 0)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.ReceiversIsEmpty, Language.GetString("ReceiversIsEmpty"));

			if (!Facade.PhoneNumber.InsertBulkNumbers(lstNumbers, userInfo.UserGuid))
				throw new Exception(Language.GetString("ErrorRecord"));

			ResponseMessage response = new ResponseMessage();
			response.IsSuccessful = true;

			return Request.CreateResponse(HttpStatusCode.OK, response);
		}

		[HttpPost]
		[RequiredAuthentication]
		public HttpResponseMessage UnSubscribe(PhoneBookModel phonebookInfo)
		{
			List<string> lstNumbers = new List<string>();

			var principal = Thread.CurrentPrincipal;
			if (!principal.Identity.IsAuthenticated)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.AccountIsInvalid, Language.GetString("AccountIsInvalid"));

			if (phonebookInfo.PhoneBookGuid == Guid.Empty ||
					phonebookInfo.Numbers.Count > 100 ||
					phonebookInfo.Numbers.Count == 0)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.BadRequest, Language.GetString("BadRequest"));

			Common.User userInfo = ((MyPrincipal)principal).UserDetails;

			Parallel.ForEach<PostPhoneNumberModel>(phonebookInfo.Numbers, (numberInfo) =>
			{
				numberInfo.Mobile = Helper.GetLocalMobileNumber(numberInfo.Mobile);

				if (Helper.IsCellPhone(numberInfo.Mobile) != 0)
					lstNumbers.Add(numberInfo.Mobile);
			});

			if (lstNumbers.Count == 0)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.ReceiversIsEmpty, Language.GetString("ReceiversIsEmpty"));

			if (!Facade.PhoneNumber.DeleteMultipleNumber(phonebookInfo.PhoneBookGuid, lstNumbers))
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.InternalServerError, Language.GetString("InternalServerError"));

			ResponseMessage response = new ResponseMessage();
			response.IsSuccessful = true;

			return Request.CreateResponse(HttpStatusCode.OK, response);
		}

		[HttpPost]
		[RequiredAuthentication]
		public HttpResponseMessage GetPagedNumbers(PhoneBookModel phonebookInfo)
		{
			PostPhoneNumberModel phoneNumberModel;
			List<PostPhoneNumberModel> lstNumbers = new List<PostPhoneNumberModel>();
			int resultCount = 0;
			var principal = Thread.CurrentPrincipal;
			if (!principal.Identity.IsAuthenticated)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.AccountIsInvalid, Language.GetString("AccountIsInvalid"));

			if (phonebookInfo.PhoneBookGuid == Guid.Empty)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.BadRequest, Language.GetString("BadRequest"));

			Common.User userInfo = ((MyPrincipal)principal).UserDetails;

			DataTable dtPhoneNumbers = Facade.PhoneNumber.GetPagedNumbers(phonebookInfo.PhoneBookGuid, string.Empty, "CreateDate", phonebookInfo.PageId, 100, ref resultCount);

			Parallel.ForEach<DataRow>(dtPhoneNumbers.AsEnumerable(), (row) =>
			{
				phoneNumberModel = new PostPhoneNumberModel();

				phoneNumberModel.FirstName = row["FirstName"].ToString();
				phoneNumberModel.LastName = row["LastName"].ToString();
				phoneNumberModel.BirthDate = Helper.GetDateTime(row["BirthDate"]);
				phoneNumberModel.Telephone = row["Telephone"].ToString();
				phoneNumberModel.Mobile = row["CellPhone"].ToString();
				phoneNumberModel.FaxNumber = row["FaxNumber"].ToString();
				phoneNumberModel.Job = row["Job"].ToString();
				phoneNumberModel.Address = row["Address"].ToString();
				phoneNumberModel.ZipCode = row["ZipCode"].ToString();
				phoneNumberModel.Email = row["Email"].ToString();
				phoneNumberModel.Gender = Helper.GetInt(row["Sex"]);
				phoneNumberModel.ZoneGuid = Helper.GetGuid(row["ZoneGuid"]);

				lstNumbers.Add(phoneNumberModel);
			});

			PhoneNumberResponse response = new PhoneNumberResponse();
			response.IsSuccessful = true;
			response.TotalCount = resultCount;
			response.PhonebookGuid = phonebookInfo.PhoneBookGuid;
			response.Numbers = lstNumbers;

			return Request.CreateResponse(HttpStatusCode.OK, response);
		}
	}
}
