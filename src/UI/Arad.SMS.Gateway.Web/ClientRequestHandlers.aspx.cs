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
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Arad.SMS.Gateway.Web
{
	public partial class ClientRequestHandlers : System.Web.UI.Page
	{
        Common.PhoneBook phoneBook = new Common.PhoneBook();
		Common.User user = new Common.User();

		private static List<string> NotRequiredLoginMethodList;

		enum ClientRequestHandlerErrors
		{
			MethodNotFound = 0,
			UserNotLogin = 1,
			AccessDenied = 2
		}

		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		#region Web Form Designer generated code

		override protected void OnInit(EventArgs e)
		{
			this.Load += new System.EventHandler(this.Page_Load);
			base.OnInit(e);
		}
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

			string methodName = Helper.Request(this, "method");

			if (IsRequiredLoginMethod(methodName))
			{
				if (Helper.GetGuid(Session["UserGuid"]) == Guid.Empty)
				{
					int defaultLoginPageControlID = Helper.GetInt(ConfigurationManager.DefaultLoginPageControlID);
					Reply("ClientRequestHandlerError{(" + (int)ClientRequestHandlerErrors.UserNotLogin + ")}");
					return;
				}
				else
				{
					try
					{
						string validationString = Helper.RequestEncrypted(this, "ValidationString");
						string authenticationString = Helper.Request(this, "AuthenticationString");
						string pageRequseter = Helper.GetUrlWithoutPortNumber(Helper.Request(this, "PageRequseter"));
						if (pageRequseter.Contains("#"))
						{
							pageRequseter = pageRequseter.Remove(pageRequseter.IndexOf('#'));
						}
						string[] parameters = validationString.Split('$');
						CryptorEngine encryptor = new CryptorEngine(parameters[0], parameters[1]);
						if (Helper.GetUrlWithoutPortNumber(this.Request.UrlReferrer.OriginalString) != parameters[2] ||
										Helper.GetUrlWithoutPortNumber(this.Request.UrlReferrer.OriginalString) != encryptor.Decrypt(authenticationString) ||
										!Helper.GetUrlWithoutPortNumber(this.Request.UrlReferrer.OriginalString).StartsWith(pageRequseter))
							throw new Exception();
					}
					catch
					{
						Reply("ClientRequestHandlerError{(" + (int)ClientRequestHandlerErrors.AccessDenied + ")}");
						return;
					}
				}
			}

			MethodInfo info = this.GetType().GetMethod(methodName);
			if (info == null)
			{
				Reply("ClientRequestHandlerError{(" + (int)ClientRequestHandlerErrors.MethodNotFound + ")}");
				return;
			}

			info.Invoke(this, null);
		}

		private bool IsRequiredLoginMethod(string methodName)
		{
			if (NotRequiredLoginMethodList == null || NotRequiredLoginMethodList.Count == 0)
				FillNotRequiredLoginMethodList();

			return !NotRequiredLoginMethodList.Contains(methodName);
		}

		private void FillNotRequiredLoginMethodList()
		{
			NotRequiredLoginMethodList = new List<string>();

			NotRequiredLoginMethodList.Add("CheckUserName");
			NotRequiredLoginMethodList.Add("LoginUser");
			NotRequiredLoginMethodList.Add("RegisterUser");
			NotRequiredLoginMethodList.Add("VisitSite");
			NotRequiredLoginMethodList.Add("RegisterLightUser");
			NotRequiredLoginMethodList.Add("GetRoleServices");
			NotRequiredLoginMethodList.Add("GetCountPostCode");
		}

		private void Reply(bool output)
		{
			Reply(output ? "1" : "0");
		}

		private void Reply(string output)
		{
			Response.Expires = -1;    //required to keep the page from being cached on the client's browser

			Response.ContentType = "text/plain";
			Response.Clear();
			Response.Write(output);
			//Response.End();
		}

		private void Reply(bool result, string output)
		{
			Response.Clear();
			Response.ContentType = "text/plain";
			Response.Write(string.Format("Result={0}&Message={1}", result ? "1" : "0", output));
		}

		#region Insert Methods
		public void InsertItemInPhoneBook()
		{
			Common.PhoneBook phoneBook = new Common.PhoneBook();
			try
			{
				phoneBook.Name = Helper.Request(this, "Name");
				phoneBook.ParentGuid = Helper.GetGuid(Helper.Request(this, "ParentGuid").Trim(new char[] { '\'' }));
				phoneBook.IsPrivate = false;
				phoneBook.UserGuid = UserGuid;
				phoneBook.CreateDate = DateTime.Now;
				phoneBook.Type = (int)PhoneBookGroupType.Normal;

				if (string.IsNullOrEmpty(phoneBook.Name))
					throw new Exception(Language.GetString("CompleteGroupNameField"));

				Guid guid = Facade.PhoneBook.InsertItemInPhoneBook(phoneBook);

				if (guid == Guid.Empty)
					throw new Exception(Language.GetString("ErrorRecord"));

				Reply("Result{(OK)}Guid{(" + guid + ")}");
			}
			catch (Exception ex)
			{
				Reply("Result{(Error)}Message{(" + ex.Message + ")}");
			}
		}

		public void InsertItemInInboxGroup()
		{
			Common.InboxGroup inboxGroup = new Common.InboxGroup();
			try
			{
				inboxGroup.Title = Helper.Request(this, "Name");
				inboxGroup.ParentGuid = Helper.GetGuid(Helper.Request(this, "ParentGuid").Trim(new char[] { '\'' }));
				inboxGroup.UserGuid = UserGuid;
				inboxGroup.CreateDate = DateTime.Now;

				if (string.IsNullOrEmpty(inboxGroup.Title))
					throw new Exception(Language.GetString("CompleteGroupNameField"));

				Guid guid = Facade.InboxGroup.InsertItemInInboxGroups(inboxGroup);

				if (guid == Guid.Empty)
					throw new Exception(Language.GetString("ErrorRecord"));

				Reply("Result{(OK)}Guid{(" + guid + ")}");
			}
			catch (Exception ex)
			{
				Reply("Result{(Error)}Message{(" + ex.Message + ")}");
			}
		}

		public void SaveSmsFormat()
		{
			Common.SmsFormat smsFormat = new Common.SmsFormat();
			string actionType = Helper.Request(this, "ActionType");

			smsFormat.SmsFormatGuid = Helper.RequestEncryptedGuid(this, "FormatGuid");
			smsFormat.Name = Helper.Request(this, "NameFormat");
			smsFormat.Format = Helper.Request(this, "Format");
			smsFormat.CreateDate = DateTime.Now;
			smsFormat.PhoneBookGuid = Helper.RequestGuid(this, "PhoneBookGuid");

			if (actionType.Equals("insert", StringComparison.OrdinalIgnoreCase))
				Reply(Facade.SmsFormat.InsertFormatForGroups(smsFormat));
			else if (actionType.Equals("edit", StringComparison.OrdinalIgnoreCase))
				Reply(Facade.SmsFormat.UpdateFormatForGroups(smsFormat));
		}

		public void SaveRoleService()
		{
			try
			{
				string data = Helper.Request(this, "Data");
				Guid roleGuid = Helper.RequestGuid(this, "ServiceRoleGuid");

				#region SelectDefaultRow
				string SelectedRow = Helper.Request(this, "SelectedDefaultRow");
				List<Guid> lstSelectedRow = new List<Guid>();
				int countService = Helper.ImportIntData(SelectedRow, "resultCount");
				for (int counterService = 0; counterService < countService; counterService++)
					lstSelectedRow.Add(Helper.ImportGuidData(SelectedRow, ("Guid" + counterService)));
				#endregion

				#region ServicePrice
				Dictionary<Guid, decimal> dictionaryServicePrice = new Dictionary<Guid, decimal>();
				countService = Helper.ImportIntData(data, "resultCount");
				for (int counterService = 0; counterService < countService; counterService++)
					dictionaryServicePrice.Add(Helper.ImportGuidData(data, "Guid" + counterService), Helper.ImportDecimalData(data, "Price" + counterService, -1));
				#endregion

				if (!Facade.RoleService.InsertRoleServices(dictionaryServicePrice, lstSelectedRow, roleGuid))
					throw new Exception(Language.GetString("ErrorRecord"));

				Reply("Result{(OK)}");
			}
			catch (Exception ex)
			{
				Reply("Result{(Error)}Message{(" + ex.Message + ")}");
			}
		}

		public void SaveUserAccess()
		{
			string data = Helper.Request(this, "Data");
			Guid userGuid = Helper.RequestEncryptedGuid(this, "UserGuid");
			int count = Helper.ImportIntData(data, "resultCount");
			string accessGuids = string.Empty;

			for (int i = 0; i < count; i++)
				accessGuids += "'" + Helper.DecryptGuid(Helper.ImportData(data, "Guid" + i), Session) + "',";

			try
			{
				if (Facade.User.InsertUserAccess(userGuid, accessGuids.TrimEnd(',')))
					Reply(true);
			}
			catch (Exception ex)
			{
				Reply(Language.GetString(ex.Message));
			}
		}

		public void SaveShortcut()
		{
			string value = Helper.Request(this, "Value");
			Guid userGuid = Helper.GetGuid(Session["UserGuid"]);
			Reply(Facade.UserSetting.UpdateSetting(userGuid, Business.AccountSetting.Shortcut, value));
		}

		public void RegisterUser()
		{
			Common.User user = new Common.User();

			string password = Helper.Request(this, "Password");
			string confirmPassword = Helper.Request(this, "ConfirmPassword");
			string domainName = Helper.GetDomain(Request.Url.Authority);
			string errorMessage = string.Empty;

			if (password == confirmPassword)
			{
				user.UserName = Helper.Request(this, "UserName");
				if (Facade.User.CheckUserNameValid(user.UserName, Guid.Empty))
				{
					user.ParentGuid = Facade.User.GetGuidOfParent(UserGuid, domainName);
					user.Password = password;
					user.SecondPassword = string.Empty;
					user.FirstName = Helper.Request(this, "FirstName");
					user.LastName = Helper.Request(this, "LastName");
					user.Email = Helper.Request(this, "Email");
					user.Phone = Helper.Request(this, "Telephone");
					user.Mobile = Helper.Request(this, "MobileNo");
					user.FaxNumber = Helper.Request(this, "FaxNo");
					user.Address = Helper.Request(this, "Address");
					//user.Position = Helper.Request(this, "Location");
					user.BirthDate = DateManager.GetChristianDateForDB(Helper.Request(this, "BirthDate"));
					user.CreateDate = DateTime.Now;
					user.ExpireDate = DateTime.Now.AddYears(1);
					user.Credit = 0;
					user.PanelPrice = Helper.GetDecimal(Helper.Request(this, "PanelPrice"));
					user.IsActive = true;
					user.IsAdmin = true;
					user.MaximumAdmin = 0;
					user.MaximumUser = 0;
					user.MaximumPhoneNumber = 10000;
					user.PriceGroupGuid = Facade.GroupPrice.GetDefaultGroupPrice(domainName);
					user.DomainGuid = Facade.Domain.GetDomainGuid(domainName);
					user.RoleGuid = Facade.Role.GetDefaultRoleGuid(domainName);
					if (!user.HasError)
					{
						Guid userGuid = Facade.User.InsertUser(user);
						if (userGuid != Guid.Empty)
						{
							if (user.PriceGroupGuid != Guid.Empty)
								Facade.User.UpdateGroupPrice(userGuid, user.PriceGroupGuid, Guid.Empty);
							Reply("Result{(OK)}Message{(" + Language.GetString("SuccessRegisterUser") + ")}");
						}
						else
							errorMessage = Language.GetString("ErrorRecord");
					}
					else
						errorMessage = user.ErrorMessage;
				}
				else
					errorMessage = Language.GetString("UserNameIsDuplicate");
			}
			else
				errorMessage = Language.GetString("PasswordNotMatchWithConfirmPassword");

			if (errorMessage != string.Empty)
				Reply("Result{(Error)}ErrorMessage{(" + errorMessage + ")}");
		}

		public void RegisterLightUser()
		{
			Common.User user = new Common.User();

			string password = Helper.Request(this, "Password");
			string confirmPassword = Helper.Request(this, "ConfirmPassword");
			string domainName = Request.Url.Authority;
			string errorMessage = string.Empty;

			if (password == confirmPassword)
			{
				user.UserName = Helper.Request(this, "UserName");
				if (Facade.User.CheckUserNameValid(user.UserName, Guid.Empty))
				{
					user.ParentGuid = Facade.User.GetGuidOfParent(UserGuid, domainName);
					user.Password = password;
					user.SecondPassword = string.Empty;
					user.Email = Helper.Request(this, "Email");
					user.Mobile = Helper.Request(this, "MobileNo");
					user.CreateDate = DateTime.Now;
					user.ExpireDate = DateTime.Now.AddYears(1);
					user.Credit = 0;
					user.PanelPrice = 0;
					user.IsActive = false;
					user.IsAdmin = false;
					user.MaximumAdmin = 0;
					user.MaximumUser = 0;
					user.MaximumPhoneNumber = 0;
					user.PriceGroupGuid = Facade.GroupPrice.GetDefaultGroupPrice(domainName);
					user.DomainGuid = Facade.Domain.GetDomainGuid(domainName);
					if (!user.HasError)
					{
						if (Facade.User.InsertUser(user) != Guid.Empty)
							Reply("Result{(OK)}Message{(" + Language.GetString("SuccessRegisterUser") + ")}");
						else
							errorMessage = Language.GetString("ErrorRecord");
					}
					else
						errorMessage = user.ErrorMessage;
				}
				else
					errorMessage = Language.GetString("UserNameIsDuplicate");
			}
			else
				errorMessage = Language.GetString("PasswordNotMatchWithConfirmPassword");

			if (errorMessage != string.Empty)
				Reply("Result{(Error)}ErrorMessage{(" + errorMessage + ")}");
		}

		public void InsertNumber()
		{
			try
			{
				Common.PhoneNumber phoneNumber = new Common.PhoneNumber();
				int mobileCount = 0;
				int emailCount = 0;
				string data = Helper.Request(this, "Data");
				string mobile = Helper.ImportData(data, "CellPhone");
				string email = Helper.ImportData(data, "Email");

				if (!Helper.CheckingCellPhone(ref mobile) && !Helper.CheckDataConditions(email).IsEmail)
					throw new Exception(Language.GetString("ContactInfoIsIncorrect"));

				if (!Helper.CheckDataConditions(email).IsEmail)
					email = string.Empty;

				phoneNumber.CreateDate = DateTime.Now;
				phoneNumber.CellPhone = mobile;
				phoneNumber.PhoneNumberGuid = Helper.ImportGuidData(data, "NumberGuid");
				phoneNumber.FirstName = Helper.ImportData(data, "FirstName");
				phoneNumber.LastName = Helper.ImportData(data, "LastName");
				phoneNumber.NationalCode = Helper.ImportData(data, "NationalCode");
				phoneNumber.BirthDate = DateManager.GetChristianDateForDB(Helper.ImportData(data, "BirthDate"));
				phoneNumber.Sex = Helper.GetByte(Helper.ImportData(data, "Sex"));
				phoneNumber.Email = email;
				phoneNumber.Job = Helper.ImportData(data, "Job");
				phoneNumber.Telephone = Helper.ImportData(data, "Telephone");
				phoneNumber.FaxNumber = Helper.ImportData(data, "FaxNumber");
				phoneNumber.Address = Helper.ImportData(data, "Address");
				phoneNumber.PhoneBookGuid = Helper.ImportGuidData(data, "PhoneBookGuid");
				int scope = Helper.GetInt(Helper.ImportData(data, "Scope"));

				#region CustomFields
				string value = string.Empty;
				for (int counterField = 1; counterField <= 20; counterField++)
				{
					value = Helper.ImportData(data, ("Field" + counterField) + "_Value").Trim();
					if (value == string.Empty) continue;
					switch (("Field" + counterField))
					{
						case "Field1":
							phoneNumber.F1 = value;
							break;
						case "Field2":
							phoneNumber.F2 = value;
							break;
						case "Field3":
							phoneNumber.F3 = value;
							break;
						case "Field4":
							phoneNumber.F4 = value;
							break;
						case "Field5":
							phoneNumber.F5 = value;
							break;
						case "Field6":
							phoneNumber.F6 = value;
							break;
						case "Field7":
							phoneNumber.F7 = value;
							break;
						case "Field8":
							phoneNumber.F8 = value;
							break;
						case "Field9":
							phoneNumber.F9 = value;
							break;
						case "Field10":
							phoneNumber.F10 = value;
							break;
						case "Field11":
							phoneNumber.F11 = value;
							break;
						case "Field12":
							phoneNumber.F12 = value;
							break;
						case "Field13":
							phoneNumber.F13 = value;
							break;
						case "Field14":
							phoneNumber.F14 = value;
							break;
						case "Field15":
							phoneNumber.F15 = value;
							break;
						case "Field16":
							phoneNumber.F16 = value;
							break;
						case "Field17":
							phoneNumber.F17 = value;
							break;
						case "Field18":
							phoneNumber.F18 = value;
							break;
						case "Field19":
							phoneNumber.F19 = value;
							break;
						case "Field20":
							phoneNumber.F20 = value;
							break;
					}
				}
				#endregion

				string actionType = Helper.ImportData(data, "ActionType");

				switch (actionType)
				{
					case "insert":
						Common.User user = Facade.User.LoadUser(UserGuid);
						Facade.PhoneBook.GetUserMaximumRecordInfo(UserGuid, ref mobileCount, ref emailCount);

						if (user.MaximumPhoneNumber != -1 && user.MaximumPhoneNumber <= mobileCount && !string.IsNullOrEmpty(phoneNumber.CellPhone))
							throw new Exception(Language.GetString("ErrorMaximumPhoneNumber"));

						if (user.MaximumEmailAddress != -1 && user.MaximumEmailAddress <= emailCount && !string.IsNullOrEmpty(phoneNumber.Email))
							throw new Exception(Language.GetString("ErrorMaximumEmailAddress"));

						if (Facade.PhoneNumber.IsDuplicateNumber(UserGuid, phoneNumber.PhoneBookGuid, (CheckNumberScope)scope, phoneNumber.CellPhone, phoneNumber.Email))
							throw new Exception(Language.GetString("ErrorDuplicateNumber"));

						if (!Facade.PhoneNumber.InsertNumber(phoneNumber))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
					case "edit":
						if (!Facade.PhoneNumber.UpdateNumber(phoneNumber, UserGuid, (CheckNumberScope)scope))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
				}

				Reply("Result{(OK)}Message{(" + Language.GetString("InsertRecord") + ")}");
			}
			catch (Exception ex)
			{
				Reply("Result{(Error)}Message{(" + ex.Message + ")}");
			}
		}

		public void InsertUserDocumentRecord()
		{
			try
			{
				Common.UserDocument userDocument = new Common.UserDocument();
				userDocument.UserGuid = Helper.RequestGuid(this, "UGuid");
				userDocument.Type = Helper.RequestInt(this, "Type");
				userDocument.Key = Helper.RequestInt(this, "Key");
				userDocument.Value = Helper.Request(this, "Path");
				userDocument.Status = (int)UserDocumentStatus.Checking;
				userDocument.Description = string.Empty;

				Guid inserted = Facade.UserDocument.Insert(userDocument);

				Reply(Helper.Encrypt(inserted, HttpContext.Current.Session));
			}
			catch
			{
				Reply(false);
			}
		}

		public void SaveFilterWord()
		{
			try
			{
				Common.FilterWord filterWord = new Common.FilterWord();
				filterWord.Title = Helper.Request(this, "Word");

				Guid inserted = Facade.FilterWord.Insert(filterWord);
				Reply(inserted != Guid.Empty ? true : false);
			}
			catch
			{
				Reply(false);
			}
		}
		#endregion

		#region Update Methods
		public void EditItemInPhoneBook()
		{
			Common.PhoneBook phoneBook = new Common.PhoneBook();
			try
			{
				phoneBook.PhoneBookGuid = Helper.GetGuid(Helper.Request(this, "PhoneBookGuid").Trim(new char[] { '\'' }));
				phoneBook.ParentGuid = Helper.GetGuid(Helper.Request(this, "ParentGuid").Trim(new char[] { '\'' }));
				phoneBook.Name = Helper.Request(this, "Name");
				phoneBook.UserGuid = UserGuid;

				Facade.PhoneBook.EditItemInPhoneBook(phoneBook);
				Reply("Result{(OK)}");
			}
			catch (Exception ex)
			{
				Reply("Result{(Error)}Message{(" + ex.Message + ")}");
			}

		}

		public void EditItemInInboxGroup()
		{
			Common.InboxGroup inboxGroup = new Common.InboxGroup();
			try
			{
				inboxGroup.InboxGroupGuid = Helper.GetGuid(Helper.Request(this, "GroupGuid").Trim(new char[] { '\'' }));
				inboxGroup.ParentGuid = Helper.GetGuid(Helper.Request(this, "ParentGuid").Trim(new char[] { '\'' }));
				inboxGroup.Title = Helper.Request(this, "Name");

				Facade.InboxGroup.EditItemInGroups(inboxGroup);
				Reply("Result{(OK)}");
			}
			catch (Exception ex)
			{
				Reply("Result{(Error)}Message{(" + ex.Message + ")}");
			}


			//inboxGroup.Title = Helper.Request(this, "Name");
			//inboxGroup.InboxGroupGuid = Helper.RequestGuid(this, "GroupGuid");
			//inboxGroup.UserGuid = UserGuid;

			//Reply(Facade.InboxGroup.EditItemInGroups(inboxGroup));
		}

		public void RejectFish()
		{
			Guid guid = Helper.RequestEncryptedGuid(this, "Guid");
			Reply(Facade.Fish.UpdateStatus(guid, FishStatus.Rejected));
		}

		public void RejectUserDocument()
		{
			try
			{
				Guid guid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.UserDocument.UpdateStatus(guid, UserDocumentStatus.Rejected));
			}
			catch (Exception ex)
			{
				Reply(false);
			}
		}

		public void ConfirmFish()
		{
			try
			{
				Guid guid = Helper.RequestEncryptedGuid(this, "Guid");
				Common.Fish fish = Facade.Fish.LoadFish(guid);
				string descriptionIncrease = string.Empty;
				string tax = Facade.Setting.GetValue((int)MainSettings.Tax);
				TypeCreditChanges creditChange = TypeCreditChanges.Fish;
				switch (fish.Type)
				{
					case (int)TypeFish.Account:
						descriptionIncrease = string.Format(Language.GetString("FishPaymentTransaction"), fish.BillNumber, fish.SmsCount, tax);
						creditChange = TypeCreditChanges.Fish;

						if (!Facade.Fish.ConfirmFish(fish.UserGuid, fish.SmsCount, creditChange, descriptionIncrease, guid))
							throw new Exception(Language.GetString("ErrorRecord"));

						Reply("Result{(OK)}");
						break;
				}
			}
			catch (Exception ex)
			{
				Reply("Result{(Error)}Message{(" + ex.Message + ")}");
			}
		}

		public void ConfirmUserDocument()
		{
			try
			{
				Guid guid = Helper.RequestEncryptedGuid(this, "Guid");

				if (!Facade.UserDocument.UpdateStatus(guid, UserDocumentStatus.Confirmed))
					throw new Exception(Language.GetString("ErrorRecord"));

				Reply("Result{(OK)}");
			}
			catch (Exception ex)
			{
				Reply("Result{(Error)}Message{(" + ex.Message + ")}");
			}
		}

		public void RejectSms()
		{
			Guid scheduledSmsGuid = Helper.RequestEncryptedGuid(this, "Guid");
			Reply(Facade.ScheduledSms.RejectSms(scheduledSmsGuid));
		}

		public void ConfirmBulk()
		{
			Guid scheduledSmsGuid = Helper.RequestEncryptedGuid(this, "Guid");
			Reply(Facade.ScheduledSms.UpdateStatus(scheduledSmsGuid, ScheduledSmsStatus.Confirmed));
		}

		public void ConfirmOutboxBulk()
		{
			Guid guid = Helper.RequestEncryptedGuid(this, "Guid");
			Reply(Facade.Outbox.UpdateStatus(guid, SendStatus.Sent));
		}

		public void RejectBulk()
		{
			Guid scheduledSmsGuid = Helper.RequestEncryptedGuid(this, "Guid");
			Reply(Facade.ScheduledSms.UpdateStatus(scheduledSmsGuid, ScheduledSmsStatus.Rejected));
		}

		public void AddRegularContentToPhoneBook()
		{
			Common.PhoneBookRegularContent phoneBookRegularContent = new Common.PhoneBookRegularContent();

			phoneBookRegularContent.PhoneBookGuid = Helper.RequestGuid(this, "PhoneBookGuid");
			phoneBookRegularContent.RegularContentGuid = Helper.RequestGuid(this, "RegularContentGuid");
			phoneBookRegularContent.CreateDate = DateTime.Now;

			Reply(Facade.PhoneBookRegularContent.Insert(phoneBookRegularContent));
		}

		public void ChangeInboxGroup()
		{
			Guid inboxGuid = Helper.RequestEncryptedGuid(this, "InboxGuid");
			Guid inboxGroupGuid = Helper.RequestGuid(this, "InboxGroupGuid");
			Reply(Facade.Inbox.ChangeInboxGroup(inboxGuid, inboxGroupGuid));
		}

		public void ActiveSmsParser()
		{
			try
			{
				Guid smsparserGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.SmsParser.ActiveSmsParser(smsparserGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void ActiveData()
		{
			try
			{
				Guid dataGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.Data.ActiveData(dataGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void ActiveGalleryImage()
		{
			try
			{
				Guid galleryGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.GalleryImage.ActiveGallery(galleryGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void ActiveImage()
		{
			try
			{
				Guid imageGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.Image.ActiveImage(imageGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void ResendSms()
		{
			try
			{
				Guid scheduledSmsGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.ScheduledSms.ResendSms(scheduledSmsGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void ResendFailedSms()
		{
			try
			{
				Guid outboxGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.Outbox.ResendFailedSms(outboxGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void SetPublicNumber()
		{
			try
			{
				Guid numberGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.PrivateNumber.SetPublicNumber(numberGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void SetOutboxExportDataStatus()
		{
			try
			{
				Guid outboxGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.Outbox.SetOutboxExportDataStatus(outboxGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void SetOutboxExportTxtStatus()
		{
			try
			{
				Guid outboxGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.Outbox.SetOutboxExportTxtStatus(outboxGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void TransferNumber()
		{
			try
			{
				Guid numberGuid = Helper.RequestEncryptedGuid(this, "NumberGuid");
				Guid groupGuid = Helper.RequestGuid(this, "GroupGuid");
				Reply(Facade.PhoneNumber.TransferNumber(numberGuid, groupGuid));
			}
			catch
			{
				Reply(false);
			}
		}
		#endregion

		#region Select Methods
		public void GetPhoneBookName()
		{
			Guid guid = Helper.GetGuid(Helper.Request(this, "PhoneBookGuid").Trim('\''));

			Reply(Facade.PhoneBook.GetName(guid));
		}

		public void LoadSmsFormat()
		{
			Guid smsFormatGuid = Helper.RequestEncryptedGuid(this, "smsFormatGuidLoad");
			Common.SmsFormat smsFormat = new Common.SmsFormat();
			smsFormat = Facade.SmsFormat.LoadFormat(smsFormatGuid);

			string format = smsFormat.Format;
			string prefix = string.Empty;
			string list = string.Empty;
			int id = -10;
			string filed = string.Empty;
			string titleFiled = string.Empty;
			string draftText = string.Empty;
			string userText = string.Empty;

			while (format.Length > 0)
			{
				prefix = format.Substring(0, 4);

				if (prefix == "<(%$")
				{
					format = format.Substring(4);
					filed = format.Substring(0, format.IndexOf("$%)>", 0));
					format = format.Substring(filed.Length + 4);

					switch (filed)
					{
						case "FIRSTNAME":
							draftText = "[نام]";
							titleFiled = "نام";
							break;
						case "LASTNAME":
							draftText = "[نام خانوادگی]";
							titleFiled = "نام خانوادگی";
							break;
						case "NationalCode":
							draftText = "[کدملی]";
							titleFiled = "کدملی";
							break;
						case "BIRTHDATE":
							draftText = "[ناریخ تولد]";
							titleFiled = "تاریخ تولد";
							break;
						case "SEX":
							draftText = "[آقای/خانم]";
							titleFiled = "جنسیت";
							break;
						case "CELLPHONE":
							draftText = "[تلفن همراه]";
							titleFiled = "تلفن همراه";
							break;
						case "EMAIL":
							draftText = "[ایمیل]";
							titleFiled = "ایمیل";
							break;
						case "JOB":
							draftText = " [شغل]";
							titleFiled = "شغل";
							break;
						case "TELEPHONE":
							draftText = "[تلفن]";
							titleFiled = "تلفن";
							break;
						case "FAXNUMBER":
							draftText = "[فاکس]";
							titleFiled = "فاکس";
							break;
						case "ADDRESS":
							draftText = "[آدرس]";
							titleFiled = "آدرس";
							break;
						default:
							draftText = filed.Substring(filed.IndexOf("@$!$@", 0) + 5);
							titleFiled = filed.Substring(filed.IndexOf("@$!$@", 0) + 5);
							//filed = filed.Substring(0,filed.IndexOf("@$!$@", 0));
							break;
					}
					list += "<li id=" + id + " field='" + filed + "' draftText='" + draftText + "' class='Field'>" + titleFiled + "<br/><input type='checkbox' /></li>";
					id++;
				}
				else if (prefix == "<(*$")
				{
					format = format.Substring(4);
					userText = format.Substring(0, format.IndexOf("$*)>", 0));
					format = format.Substring(userText.Length + 4);

					if (userText.Length > 10)
						list += "<li field='USERTEXT' title='" + userText + "' class='Field'>" + userText.Substring(0, 10) + "...<br/><input type='checkbox' /></li>";
					else
						list += "<li field='USERTEXT' title='" + userText + "' class='Field'>" + userText + "<br/><input type='checkbox' /></li>";
				}
				else if (prefix == "<(!$")
				{
					format = format.Substring(4);
					string text = format.Substring(0, format.IndexOf("$!)>", 0));
					format = format.Substring(text.Length + 4);

					if (text.Length > 10)
						list += "<li field='InputSms' title='" + Language.GetString(text) + "' class='Field'>" + Language.GetString(text).Substring(0, 10) + "...<br/><input type='checkbox' /></li>";
					else
						list += "<li field='InputSms' title='" + Language.GetString(text) + "' class='Field'>" + Language.GetString(text) + "<br/><input type='checkbox' /></li>";
				}
			}

			list += "/*NameFormat*/" + smsFormat.Name;
			Reply(list);
		}

		public void GetRoleServices()
		{
			try
			{
				string htmlUl = "<div rel='custom-scrollbar'><ul class='resp-tabs-list' style='overflow-x:hidden;overflow-y:auto;direction:ltr;'>";
				string htmlDiv = "<div class='resp-tabs-container' style='font:10pt tahoma'>";
				Guid userGuid = Facade.Domain.GetGuidAdminOfDomain(Helper.GetDomain(Request.Url.Authority));
				DataSet dataSetRoleServicePrice = Facade.RoleService.GetRoleServices(userGuid);
				DataTable dataTableRole = dataSetRoleServicePrice.Tables[0];

				for (int roleCount = 0; roleCount < dataTableRole.Rows.Count; roleCount++)
				{
					DataView dataViewwRoleServicePrice = new DataView(dataSetRoleServicePrice.Tables[1]);
					dataViewwRoleServicePrice.RowFilter = "RoleGuid ='" + dataTableRole.Rows[roleCount]["Guid"] + "'";
					htmlDiv += "<div><div style='height:220px;overflow-x:hidden;overflow-y: auto;direction: ltr;' rel='custom-scrollbar'>";
					for (int roleServiceCount = 0; roleServiceCount < dataViewwRoleServicePrice.Count; roleServiceCount++)
					{
						htmlDiv += "<div style='float:right;width:250px;'>" + dataViewwRoleServicePrice[roleServiceCount]["Title"] +
																															"<img width='13px' height='13px' src='/pic/Service.gif'></div>";
					}
					htmlDiv += "</div><hr style='color:#c9c9c9;clear: both;'>" +
						"<div class='div-footer-right-online' style='font-weight:bold'>" + dataTableRole.Rows[roleCount]["Description"] + "<span style='display:none'>" +
										Helper.FormatDecimalForDisplay(dataTableRole.Rows[roleCount]["Price"]) + "</span></div></div>";
					htmlUl += " <li style='text-align:center;'><p>" + dataTableRole.Rows[roleCount]["Title"] + "</p></li> ";
				}
				htmlDiv += "</div>";
				htmlUl += "</ul></div>";
				if (dataTableRole.Rows.Count > 0)
					Reply(htmlDiv + htmlUl);
				else
					Reply("<div><img src='/pic/loader1.gif' style='width:100%;height:280px;'></div>");
			}
			catch
			{
				Reply("<div><img src='/pic/loader1.gif' style='width:100%;height:280px;'></div>");
			}
		}

		public void GetFormatOfPhoneBook()
		{
			Guid guid = Helper.RequestEncryptedGuid(this, "Guid");
			DataTable dataTableFormats = Facade.SmsFormat.GetFormatOfPhoneBook(guid);
			string replyString = string.Empty;
			foreach (DataRow dataRowFormat in dataTableFormats.Rows)
				replyString += string.Format("<option value=\"{0}\">{1}</option>", Helper.Encrypt(Helper.GetString(dataRowFormat["Guid"]), Session), Helper.GetString(dataRowFormat["Name"]));
			Reply(replyString);
		}

		public void CheckUserName()
		{
			string userName = Helper.Request(this, "UserName");

			if (userName != string.Empty)
				Reply(Facade.User.CheckUserNameValid(userName, Guid.Empty));
			else
				Reply(false);
		}

		public void LoginUser()
		{
			string domainName = Helper.GetDomain(Request.Url.Authority);
			Common.LoginStat loginStat = new Common.LoginStat();

			string challengeString = Helper.GetString(Session["ChallengeString"]);
			int loginTriesCount = Helper.RequestEncryptedInt(this, "LoginTriesCount");
			bool remaindMe = Helper.RequestBool(this, "RemaindMe");
			string message = string.Empty;

			user.UserName = Helper.Request(this, "UserName");
			user.Password = Helper.Request(this, "Password");
			user.DomainGuid = Facade.Domain.GetDomainGuid(domainName);

			if (loginTriesCount > 5 && loginTriesCount < 0)
				Reply("Result{(Error)}");
			else if (!user.HasError)
			{
				bool isLoginValid = Facade.User.LoginUser(user, challengeString, false);

				if (isLoginValid && user.IsActive)
				{
					Session["UserGuid"] = user.UserGuid;
					Session["IsAdmin"] = user.IsAdmin;
					Session["IsMainAdmin"] = user.IsMainAdmin;
					Session["ParentGuid"] = user.ParentGuid;
					Session["ExpireDate"] = user.ExpireDate;
					Session["IsAuthenticated"] = user.IsAuthenticated;
					Session.Remove("SessionExpired");
					Session.Remove("ChallengeString");

					Desktop desktop;
					Theme theme;
					DefaultPages defaultPage;
					Facade.Domain.GetDomainInfo(domainName, out desktop, out defaultPage, out theme);

					//int userTheme = Helper.GetInt(Facade.UserSetting.GetSettingValue(user.UserGuid, "Theme"));
					//if (userTheme != 0)
					//	theme = (Business.Theme)userTheme;

					Session["DeskTop"] = (int)desktop;
					//Session["Theme"] = (int)theme;

					try
					{
						loginStat.IP = Request.UserHostAddress;
						loginStat.Type = (int)Arad.SMS.Gateway.Business.LoginStatsType.SignIn;
						loginStat.CreateDate = DateTime.Now;
						loginStat.UserGuid = user.UserGuid;
						Facade.LoginStat.Insert(loginStat);
					}
					catch { }

					if (remaindMe)
					{
						HttpCookie cooky = new HttpCookie("UserName", user.UserName);
						cooky.Expires = DateTime.Now.AddYears(1);
						Response.Cookies.Add(cooky);
					}
					else
					{
						Response.Cookies.Remove("UserName");
					}

					Facade.User.SendSmsForLogin(user.UserGuid);

					Reply("Result{(OK)}Url{(" + string.Format("http://{0}/{1}", domainName, desktop.ToString().ToLower()) + ")}");
				}
				else if (isLoginValid)
					message = Language.GetString("UserIsNotActive");
				else
				{
					challengeString = Helper.GetMd5Hash(Helper.RandomString()).ToLower();
					Session["ChallengeString"] = challengeString;

					message = Language.GetString("UserNotFound");
				}
			}
			else
				message = user.ErrorMessage;

			if (message != string.Empty)
				Reply("Result{(Error)}ChallengeString{(" + challengeString + ")}ErrorMessage{(" + message + ")}LoginTriesCount{(" + Helper.Encrypt(loginTriesCount + 1, Session) + ")}");
		}

		public void VisitSite()
		{
			Common.User user = new Common.User();
			string message = string.Empty;
			user.UserName = "demo";

			bool isLoginValid = Facade.User.LoginUser(user, true);

			if (!user.HasError)
			{
				if (isLoginValid && user.IsActive)
				{
					Session["UserGuid"] = user.UserGuid;
					Session["IsAdmin"] = user.IsAdmin;
					Session["IsMainAdmin"] = user.IsMainAdmin;
					Session["ParentGuid"] = user.ParentGuid;
					Session.Remove("SessionExpired");
					Session.Remove("ChallengeString");

					string domainName = Helper.GetDomain(Request.Url.Authority);
					Business.Desktop desktop;
					Business.Theme theme;
					Business.DefaultPages defaultPage;

					Facade.Domain.GetDomainInfo(domainName, out desktop, out defaultPage, out theme);

					Session["DeskTop"] = (int)desktop;
					Session["Theme"] = (int)theme;

					Reply("Result{(OK)}Url{(" + string.Format("http://{0}/HomePages/{1}.aspx", domainName, desktop.ToString()) + ")}");
				}
				else if (isLoginValid)
					message = Language.GetString("UserIsNotActive");
				else
				{
					message = Language.GetString("UserNotFound");
				}
			}
			else
				message = user.ErrorMessage;

			if (message != string.Empty)
				Reply("Result{(Error)}ErrorMessage{(" + message + ")}");
		}

		public void GoToUsersPanel()
		{
			Guid userGuid = Helper.RequestEncryptedGuid(this, "UserGuid");
			Guid domainGuid = Helper.RequestEncryptedGuid(this, "DomainGuid");
			Common.Domain domainInfo = Facade.Domain.Load(domainGuid);
			Common.User user = Facade.User.LoadUser(userGuid);
			Session["UserGuid"] = userGuid;
			Session["IsAdmin"] = user.IsAdmin;
			Session["IsMainAdmin"] = user.IsMainAdmin;
			Session["ParentGuid"] = user.ParentGuid;
			Session["ExpireDate"] = user.ExpireDate;
			Session["IsAuthenticated"] = user.IsAuthenticated;
			string userPanelPath = string.Format("{0}://{1}/{2}", Request.Url.Scheme, Request.Url.Authority, ((Desktop)domainInfo.Desktop).ToString().ToLower());
			Reply(true, userPanelPath);
		}

		public void GetInboxGroupOfSms()
		{
			Guid guid = Helper.RequestEncryptedGuid(this, "RecieveSmsGuid");
			Reply(Facade.Inbox.Load(guid).InboxGroupGuid.ToString());
		}

		public void GetNews()
		{
			Guid dataGuid = Helper.RequestEncryptedGuid(this, "NewsGuid");
			Common.Data data = Facade.Data.LoadData(dataGuid);
			StringBuilder newsValuesString = new StringBuilder();
			newsValuesString.Append("Title{(" + data.Title + ")}");
			newsValuesString.Append("Summary{(" + data.Summary + ")}");
			newsValuesString.Append("Content{(" + data.Content + ")}");
			newsValuesString.Append("FromDate{(" + DateManager.GetSolarDate(data.FromDate) + ")}");
			newsValuesString.Append("ToDate{(" + DateManager.GetSolarDate(data.ToDate) + ")}");
			newsValuesString.Append("IsActive{(" + data.IsActive + ")}");
			newsValuesString.Append("DataCenter{(" + data.DataCenterGuid + ")}");
			Reply(newsValuesString.ToString());
		}

		public void GetMenu()
		{
			Guid dataGuid = Helper.RequestEncryptedGuid(this, "MenuGuid");
			Common.Data data = Facade.Data.LoadData(dataGuid);
			StringBuilder menuValuesString = new StringBuilder();
			menuValuesString.Append("Title{(" + data.Title + ")}");
			menuValuesString.Append("Priority{(" + data.Priority + ")}");
			menuValuesString.Append("Content{(" + data.Content + ")}");
			menuValuesString.Append("Keywords{(" + data.Keywords + ")}");
			menuValuesString.Append("Parent{(" + data.ParentGuid + ")}");
			menuValuesString.Append("IsActive{(" + data.IsActive.ToString().ToLower() + ")}");
			menuValuesString.Append("HomePage{(" + data.ShowInHomePage.ToString().ToLower() + ")}");
			menuValuesString.Append("DataCenter{(" + data.DataCenterGuid + ")}");
			Reply(menuValuesString.ToString());
		}

		public void GetPhoneBookDateField()
		{
			try
			{
				Guid phoneBookGuid = (Guid)Helper.RequestGuid(this, "phoneBookGuid");
				DataTable dataTablePhoneBookDateField = Facade.UserField.GetPhoneBookDateField(phoneBookGuid);
				string itemes = string.Format("<option></option>");
				if (dataTablePhoneBookDateField.Rows.Count > 0)
				{
					for (int i = 1; i <= 20; i++)
					{
						if ((dataTablePhoneBookDateField.Rows[0][i] != null) &&
										Helper.GetInt(dataTablePhoneBookDateField.Rows[0]["FieldType" + i]) == (int)Arad.SMS.Gateway.Business.UserFieldTypes.DateTime)
							itemes += string.Format("<option  value='{0}'>{1}</option>", i, dataTablePhoneBookDateField.Rows[0][i].ToString());
					}
				}
				Reply(itemes);
			}
			catch
			{
				Reply(string.Empty);
			}
		}

		public void GetSendSmsInfo()
		{
			try
			{
				Guid senderNumber = Helper.RequestGuid(this, "SenderNumber");
				int smsPartCount = Helper.RequestInt(this, "SmsPartCount");
				SmsTypes smsType = Helper.RequestBool(this, "IsUnicodeSms") ? SmsTypes.Farsi : SmsTypes.Latin;
				string phoneBookGuid = Helper.Request(this, "GroupGuid").Trim('\'');
				string filePath = Helper.Request(this, "FilePath");
				string receivers = Helper.Request(this, "Receivers");

				Dictionary<int, int> operatorNumberCount = new Dictionary<int, int>();
				Dictionary<string, string> dicFileInfo = new Dictionary<string, string>();

				if (!string.IsNullOrEmpty(phoneBookGuid))
					operatorNumberCount = Facade.PhoneNumber.GetCountPhoneBooksNumberOperator(phoneBookGuid);

				List<string> recieversNumber = new List<string>();
				string fileInfo = string.Empty;
				if (!string.IsNullOrEmpty(filePath))
				{
					dicFileInfo = Facade.PhoneNumber.GetFileNumberInfo(Server.MapPath("~/Uploads/" + filePath), recieversNumber);
					fileInfo = string.Format(@"<p>عنوان فایل {0}</p>
																		<p>تعداد کل شماره ها {1} عدد</p>
																		<p>تعداد شماره های تکراری {2} عدد</p>
																		<p>تعداد شماره های صحیح {3} عدد</p>", filePath.Split('/')[1], dicFileInfo["TotalNumberCount"], dicFileInfo["DuplicateNumberCount"], dicFileInfo["CorrectNumberCount"]);

				}
				recieversNumber.AddRange(receivers.Split(new string[] { "\n", "\r\n", ",", " ", ";" }, StringSplitOptions.RemoveEmptyEntries).ToList());

				Facade.Outbox.AddCountNumberOfOperatorsToDictionary(ref recieversNumber, ref operatorNumberCount);

				decimal sendPrice = Facade.Transaction.GetSendPrice(UserGuid, smsType, smsPartCount, senderNumber, operatorNumberCount);

				int numberType = Facade.PrivateNumber.LoadNumber(senderNumber).Type;

				string result = "Result{(OK)}TotalCount{(" + operatorNumberCount.Values.Sum() + ")}SendPrice{(" + sendPrice + ")}NumberType{(" + numberType + ")}";

				if (!string.IsNullOrEmpty(fileInfo))
					result += "FileInfo{(" + fileInfo + ")}FilePath{(" + filePath + ")}FileCorrectNumberCount{(" + dicFileInfo["CorrectNumberCount"] + ")}";

				foreach (KeyValuePair<int, int> opt in operatorNumberCount)
					result += opt.Key + "{(" + opt.Value + ")}";

				Reply(result);
			}
			catch (Exception ex)
			{
				Reply("Result{(Error)}Message{(" + ex.Message + ")}");
			}
		}

		public void GetSmsCountOfFormat()
		{
			try
			{
				string numbersInfo = string.Empty;
				Guid formatGuid = Helper.RequestGuid(this, "FormatGuid");
				Guid senderNumber = Helper.RequestGuid(this, "SenderNumber");
				Facade.Transaction.GetSendFormatPrice(UserGuid, formatGuid, senderNumber, ref numbersInfo);
				Reply(@"Result{(OK)}" + numbersInfo);
			}
			catch (Exception ex)
			{
				Reply("Result{(Error)}Message{(" + ex.Message + ")}");
			}
		}

		public void IsDuplicateSmsParserKey()
		{
			try
			{
				Guid numberGuid = Helper.RequestGuid(this, "NumberGuid");
				string key = Helper.Request(this, "Key");

				//Reply(Facade.SmsParser.IsDuplicateSmsParserKey(numberGuid, key));
				Reply(false);
			}
			catch
			{
				Reply(false);
			}
		}

		public void GetUserInfo()
		{
			try
			{
				string username = Helper.Request(this, "Username");
				DataTable dtUser = Facade.User.GetUser(username);
				if (dtUser.Rows.Count == 0)
					throw new Exception("UserNotFound");

				Reply(string.Format("{0} {1}", dtUser.Rows[0]["FirstName"], dtUser.Rows[0]["LastName"]));
			}
			catch (Exception ex)
			{
				Reply(ex.Message);
			}
		}

		public void GetBulkRecipientCount()
		{
			try
			{
				Dictionary<int, int> operatorCount = new Dictionary<int, int>();

				Guid zoneGuid = Helper.RequestGuid(this, "ZoneGuid");
				string prefix = Helper.Request(this, "Prefix");
				string zipcode = Helper.Request(this, "ZipCode");
				string smsText = Helper.Request(this, "SmsText");
				int opt = Helper.RequestInt(this, "Operator");
				int sendCount = Helper.RequestInt(this, "SendCount");
				NumberType type = (NumberType)Helper.RequestInt(this, "Type");
				SmsTypes smsType = Helper.HasUniCodeCharacter(smsText) ? SmsTypes.Farsi : SmsTypes.Latin;
				Guid privateNumberGuid = Helper.RequestGuid(this, "SenderNumberGuid");

				int count = Facade.PersonsInfo.GetCount(zoneGuid, prefix, zipcode, type, opt);
				operatorCount.Add(opt, sendCount);

				decimal sendPrice = Facade.Transaction.GetSendPrice(UserGuid, smsType, Helper.GetSmsCount(smsText), privateNumberGuid, operatorCount);

				Reply("Result{(OK)}Count{(" + count + ")}SendPrice{(" + sendPrice + ")}");
			}
			catch (Exception ex)
			{
				Reply("Result{(Error)}Message{(" + ex.Message + ")}");
			}
		}

		public void GetPhonebookField()
		{
			try
			{
				DataTable dtFields = new DataTable();
				Guid phoneBookGuid = Helper.GetGuid(Helper.Request(this, "PhoneBookGuid").Trim('\''));

				dtFields.Columns.Add("Name", typeof(string));
				dtFields.Columns.Add("Title", typeof(string));

				DataRow row;

				foreach (PhoneBookFields field in Enum.GetValues(typeof(PhoneBookFields)))
				{
					row = dtFields.NewRow();

					row["Name"] = field;
					row["Title"] = Language.GetString(field.ToString());

					dtFields.Rows.Add(row);
				}

				DataTable dtCustomFields = Facade.UserField.GetPhoneBookField(phoneBookGuid);
				foreach (DataRow customfield in dtCustomFields.Rows)
				{
					row = dtFields.NewRow();

					row["Name"] = "F" + customfield["FieldID"];
					row["Title"] = customfield["Title"];

					dtFields.Rows.Add(row);
				}

				string jsonString = JsonConvert.SerializeObject(dtFields);
				Reply(jsonString);
			}
			catch
			{
				Reply(string.Empty);
			}
		}
		#endregion

		#region Delete Methods
		public void DeleteItemFromPhoneBook()
		{
			try
			{
				Guid phoneBookGuid = Helper.GetGuid(Helper.Request(this, "PhoneBookGuid").Trim(new char[] { '\'' }));
				//if (Facade.PhoneNumber.GetCountNumberOfPhoneBook(phoneBookGuid) > 0)
				//	throw new Exception(Language.GetString("GroupOfPhoneBookHasNumberError"));

				if (!Facade.PhoneBook.DeleteItemFromPhoneBook(phoneBookGuid))
					throw new Exception("ErrorRecord");

				Reply("Result{(OK)}");
			}
			catch (Exception ex)
			{
				Reply("Result{(Error)}Message{(" + ex.Message + ")}");
			}
		}

		public void DeleteItemFromInboxGroup()
		{
			try
			{
				Guid groupGuid = Helper.GetGuid(Helper.Request(this, "GroupGuid").Trim(new char[] { '\'' }));
				//if (Facade.Inbox.GetCountNumberOfGroup(groupGuid) > 0)
				//	throw new Exception(Language.GetString("GroupOfPhoneBookHasNumberError"));

				if (!Facade.InboxGroup.DeleteItemFromInboxGroups(groupGuid))
					throw new Exception("ErrorRecord");

				Reply("Result{(OK)}");
			}
			catch (Exception ex)
			{
				Reply("Result{(Error)}Message{(" + ex.Message + ")}");
			}
		}

		public void DeleteSmsFormat()
		{
			Guid smsFormatGuid = Helper.RequestEncryptedGuid(this, "SmsFormatGuid");
			Reply(Facade.SmsFormat.Delete(smsFormatGuid));
		}

		public void DeleteDomain()
		{
			try
			{
				Guid guidDomain = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.Domain.Delete(guidDomain));
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteServiceGroup()
		{
			try
			{
				Guid serviceGroupGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.ServiceGroup.Delete(serviceGroupGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteService()
		{
			try
			{
				Guid serviceGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.Service.Delete(serviceGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteAccess()
		{
			try
			{
				Guid AccessGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.Access.Delete(AccessGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteUserField()
		{
			Guid phoneBookGuid = Helper.RequestGuid(this, "Guid");
			int index = Helper.RequestInt(this, "Index");
			Reply(Facade.UserField.DeleteField(index, phoneBookGuid));
		}

		public void DeleteNumber()
		{
			Guid guid = Helper.RequestEncryptedGuid(this, "NumberGuid");
			Reply(Facade.PhoneNumber.DeleteNumber(guid));
		}

		public void DeleteBlackListNumber()
		{
			Guid guid = Helper.RequestEncryptedGuid(this, "NumberGuid");
			Reply(Facade.PersonsInfo.UpdateBlackListStatus(guid, false));
		}

		public void DeleteMultipleNumber()
		{
			string data = Helper.Request(this, "Data");
			int countNumber = Helper.GetInt(Helper.ImportData(data, "resultCount"));
			StringBuilder guidList = new StringBuilder();
			for (int counter = 0; counter < countNumber; counter++)
			{
				Guid guid = Helper.DecryptGuid(Helper.ImportData(data, ("Guid" + counter).ToString()), Session);
				guidList.Append("'" + guid.ToString() + "',");
			}
			guidList.Remove(guidList.Length - 1, 1);
			Reply(Facade.PhoneNumber.DeleteMultipleNumber(guidList.ToString()));
		}

		public void DeleteMultipleReceivedSms()
		{
			string data = Helper.Request(this, "Data");
			int countRow = Helper.GetInt(Helper.ImportData(data, "resultCount"));
			StringBuilder guidList = new StringBuilder();
			for (int counter = 0; counter < countRow; counter++)
			{
				Guid guid = Helper.DecryptGuid(Helper.ImportData(data, ("Guid" + counter).ToString()), Session);
				guidList.Append("'" + guid.ToString() + "',");
			}
			guidList.Remove(guidList.Length - 1, 1);
			Reply(Facade.Inbox.DeleteMultipleRow(guidList.ToString()));
		}

		public void DeleteMultipleContent()
		{
			try
			{
				string data = Helper.Request(this, "Data");
				int countNumber = Helper.GetInt(Helper.ImportData(data, "resultCount"));
				List<string> guidList = new List<string>();
				for (int counter = 0; counter < countNumber; counter++)
				{
					Guid guid = Helper.DecryptGuid(Helper.ImportData(data, ("Guid" + counter).ToString()), Session);
					guidList.Add(guid.ToString());
				}

				Reply(Facade.Content.Delete(string.Join(",", guidList)));
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeletePrivateNumber()
		{
			Guid guid = Helper.RequestEncryptedGuid(this, "Guid");
			Reply(Facade.PrivateNumber.DeleteNumber(guid));
		}

		public void DeleteAccountInformation()
		{
			Guid guid = Helper.RequestEncryptedGuid(this, "Guid");
			Reply(Facade.AccountInformation.Delete(guid));
		}

		public void DeleteUser()
		{
			Guid guid = Helper.RequestEncryptedGuid(this, "Guid");

			Reply(Facade.User.DeleteUser(guid));
		}

		public void DeleteSmsTemplate()
		{
			Guid guid = Helper.RequestEncryptedGuid(this, "Guid");
			Reply(Facade.SmsTemplate.DeleteSmsTemplate(guid));
		}

		public void DeleteGalleryImage()
		{
			try
			{
				Guid galleryImageGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.GalleryImage.Delete(galleryImageGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteImage()
		{
			try
			{
				Guid imageGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.Image.Delete(imageGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteDataCenter()
		{
			try
			{
				Guid dataCenterGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.DataCenter.Delete(dataCenterGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteNews()
		{
			try
			{
				Guid newsGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.Data.Delete(newsGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteMenu()
		{
			try
			{
				Guid menuGuid = Helper.RequestEncryptedGuid(this, "MenuGuid");
				Reply(Facade.Data.Delete(menuGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteGroupPrice()
		{
			try
			{
				Guid groupGuid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.GroupPrice.DeleteGroupPrice(groupGuid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteSmsParser()
		{
			try
			{
				Guid guid = Helper.RequestEncryptedGuid(this, "Guid");

				if (!Facade.SmsParser.Delete(guid))
					throw new Exception();

				Reply(true);
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteRole()
		{
			Guid roleGuid = Helper.RequestEncryptedGuid(this, "Guid");
			Reply(Facade.Role.Delete(roleGuid));
		}

		public void DeleteInboxSms()
		{
			Guid guid = Helper.RequestEncryptedGuid(this, "Guid");
			Reply(Facade.Inbox.Delete(guid));
		}

		public void DeleteTrafficRelay()
		{
			try
			{
				Guid guid = Helper.RequestEncryptedGuid(this, "Guid");

				if (!Facade.TrafficRelay.Delete(guid))
					throw new Exception();

				Reply(true);
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteAgentRatio()
		{
			Guid agentGuid = Helper.RequestEncryptedGuid(this, "Guid");
			Reply(Facade.AgentRatio.Delete(agentGuid));
		}

		public void DeleteUserDocumentRecord()
		{
			try
			{
				Guid guid = Helper.RequestEncryptedGuid(this, "Guid");
				if (Facade.UserDocument.Delete(guid))
				{
					string file = Helper.Request(this, "path");
					string path = Server.MapPath(string.Format("~/Uploads/UserDocuments/{0}", file));

					if (File.Exists(path))
						File.Delete(path);

					Reply(true);
				}
				else
					Reply(false);
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteRegularContent()
		{
			try
			{
				Guid guid = Helper.RequestEncryptedGuid(this, "Guid");
				Reply(Facade.RegularContent.Delete(guid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteRegularContentFromPhoneBook()
		{
			try
			{
				Guid guid = Helper.RequestEncryptedGuid(this, "RegularGuid");

				Reply(Facade.PhoneBookRegularContent.Delete(guid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeletePrivateNumberKeyword()
		{
			try
			{
				Guid guid = Helper.RequestEncryptedGuid(this, "KeywordGuid");

				Reply(Facade.PrivateNumber.DeleteKeyword(guid));
			}
			catch
			{
				Reply(false);
			}
		}

		public void DeleteFilterWord()
		{
			try
			{
				Guid guid = Helper.RequestEncryptedGuid(this, "Guid");

				Reply(Facade.FilterWord.Delete(guid));
			}
			catch
			{
				Reply(false);
			}
		}
		#endregion
	}
}