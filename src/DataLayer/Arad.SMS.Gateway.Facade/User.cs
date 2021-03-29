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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Arad.SMS.Gateway.Facade
{
	public class User : FacadeEntityBase
	{

		#region SelectMethod
		//public static Dictionary<Operators, int> GetCountUserNumberOfOperators(Guid parentGuid)
		//{
		//	Business.User userController = new Business.User();
		//	DataTable dtSmsInfo = userController.GetCountUserNumberOfOperators(parentGuid);
		//	Dictionary<Business.Operators, int> operatorCountNumberDictionary = new Dictionary<Business.Operators, int>();

		//	foreach (DataRow dataRow in dtSmsInfo.Rows)
		//	{
		//		switch (Helper.GetInt(dataRow["Operator"]))
		//		{
		//			case (int)Business.Operators.MCI:
		//				operatorCountNumberDictionary.Add(Business.Operators.MCI, Helper.GetInt(dataRow["Count"]));
		//				break;
		//			case (int)Business.Operators.MTN:
		//				operatorCountNumberDictionary.Add(Business.Operators.MTN, Helper.GetInt(dataRow["Count"]));
		//				break;
		//			case (int)Business.Operators.Rightel:
		//				operatorCountNumberDictionary.Add(Business.Operators.Rightel, Helper.GetInt(dataRow["Count"]));
		//				break;
		//		}
		//	}
		//	return operatorCountNumberDictionary;
		//}

		//public static Dictionary<Operators, int> GetCountRoleNumberOfOperators(Guid userGuid, Guid roleGuid)
		//{
		//	Business.User userController = new Business.User();
		//	DataTable dtSmsInfo = userController.GetCountRoleNumberOfOperators(roleGuid, userGuid);
		//	Dictionary<Business.Operators, int> operatorCountNumberDictionary = new Dictionary<Business.Operators, int>();

		//	foreach (DataRow dataRow in dtSmsInfo.Rows)
		//	{
		//		switch (Helper.GetInt(dataRow["Operator"]))
		//		{
		//			case (int)Business.Operators.MCI:
		//				operatorCountNumberDictionary.Add(Business.Operators.MCI, Helper.GetInt(dataRow["Count"]));
		//				break;
		//			case (int)Business.Operators.MTN:
		//				operatorCountNumberDictionary.Add(Business.Operators.MTN, Helper.GetInt(dataRow["Count"]));
		//				break;
		//			case (int)Business.Operators.Rightel:
		//				operatorCountNumberDictionary.Add(Business.Operators.Rightel, Helper.GetInt(dataRow["Count"]));
		//				break;
		//		}
		//	}
		//	return operatorCountNumberDictionary;
		//}
		#endregion

		public static Common.User LoadUser(Guid userGuid)
		{
			Business.User userController = new Business.User();
			Common.User commonUser = new Common.User();
			userController.Load(userGuid, commonUser);
			return commonUser;
		}

		public static DataTable GetChildren(Common.User user, int userType, int activeStatus)
		{
			Business.User userController = new Business.User();
			return userController.GetChildren(user, userType, activeStatus);
		}

		public static DataTable GetChildren(Common.User user, int userType)
		{
			Business.User userController = new Business.User();
			return userController.GetChildren(user, userType, 2);
		}

		public static bool UpdateUser(Common.User user)
		{
			Business.User userController = new Business.User();
			return userController.Update(user);
		}

		public static bool DeleteUser(Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.Delete(userGuid);
		}

		public static bool CheckUserNameValid(string userName, Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.CheckUserNameValid(userName, userGuid);
		}

		public static bool UpdateProfile(Common.User user)
		{
			Business.User userController = new Business.User();
			return userController.UpdateProfile(user);
		}

		public static bool AdvanceUpdate(Common.User user)
		{
			try
			{
				Business.User userController = new Business.User();
				return userController.AdvanceUpdate(user);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static Guid InsertUser(Common.User user)
		{
			Business.User userController = new Business.User();
			return userController.Insert(user);
		}

		public static bool LoginUser(Common.User user, string challengeString, bool autoLogin)
		{
			Business.User userController = new Business.User();
			return userController.Login(user, challengeString, autoLogin);
		}

		public static bool LoginUser(Common.User user, bool autoLogin)
		{
			Business.User userController = new Business.User();
			return userController.Login(user, autoLogin);
		}

		public static bool CahngePassword(Guid userGuid, string oldPassword, string newPassword)
		{
			Business.User userController = new Business.User();
			string currentPassword = userController.GetCurrentPassword(userGuid);
			try
			{
				if (oldPassword == currentPassword)
					return userController.UpdatePassword(userGuid, newPassword) ? true : false;
				else
					throw new Exception(Language.GetString("InvalidCurrentPassword"));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetPagedUsers(Guid userGuid, Guid domainGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.User userController = new Business.User();
			return userController.GetPagedUsers(userGuid, domainGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public static Guid GetGuidOfParent(Guid userGuid, string domainName)
		{
			Business.User userController = new Business.User();
			if (userGuid == Guid.Empty)
				return Facade.Domain.GetGuidAdminOfDomain(domainName);
			else
			{
				Common.User user = new Common.User();
				userController.Load(userGuid, user);
				if (user.IsAdmin && user.MaximumUser > 0)
					return user.UserGuid;
				else
					return user.ParentGuid;
			}
		}

		public static DataTable GetServiceOfUser(Guid parentGuid, Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.GetServiceOfUser(parentGuid, userGuid);
		}

		public static bool DeleteAllUserService(Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.DeleteAllUserService(userGuid);
		}

		public static bool InsertUserService(Guid userGuid, string servicesGuids, string newServices, decimal sumPrice, bool decreaseFromPanelCharge)
		{
			Business.User userController = new Business.User();
			userController.BeginTransaction();

			try
			{
				if (!userController.DeleteAllUserService(userGuid))
					throw new Exception("ErrorRecord");
				if (!userController.InsertUserService(userGuid, servicesGuids))
					throw new Exception("ErrorRecord");
				if (!userController.InserUserAccessByService(userGuid, servicesGuids))
					throw new Exception("ErrorRecord");

				if (decreaseFromPanelCharge && sumPrice != 0)
				{
					int count = Helper.ImportIntData(newServices, "Count");
					for (int i = 0; i < count; i++)
						Facade.Transaction.Decrease(userGuid, Helper.ImportDecimalData(newServices, "Price" + i), TypeCreditChanges.ActivationService, Language.GetString("DecreaseFromPanelChargeForActivationService") + " \"" + Helper.ImportData(newServices, "Title" + i) + "\"", Guid.Empty, userController.DataAccessProvider);
				}

				userController.CommitTransaction();
			}
			catch (Exception ex)
			{
				userController.RollbackTransaction();
				throw ex;
			}

			return true;
		}

		public static bool UpdateGroupPrice(Guid userGuid, Guid groupPriceGuid, Guid domainGroupPriceGuid)
		{
			Business.User userController = new Business.User();
			userController.BeginTransaction();
			try
			{
				if (groupPriceGuid == Guid.Empty)
					throw new Exception("CompleteGroupPriceGuid");

				if (!userController.UpdateGroupGuid(userGuid, groupPriceGuid))
					throw new Exception("ErrorRecord");

				if (domainGroupPriceGuid != Guid.Empty)
				{
					if (!userController.UpdateDomainGroupPrice(userGuid, domainGroupPriceGuid))
						throw new Exception("ErrorRecord");
				}

				//if (!Facade.SmsRate.UpdateUserSmsRate(userGuid, userController.DataAccessProvider))
				//	throw new Exception("ErrorRecord");

				userController.CommitTransaction();
				return true;
			}
			catch (Exception ex)
			{
				userController.RollbackTransaction();
				throw ex;
			}
		}

		public static DataTable GetAccessOfUser(Guid parentGuid, Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.GetAccessOfUser(parentGuid, userGuid);
		}

		public static bool InsertUserAccess(Guid userGuid, string accessGuids)
		{
			Business.User userController = new Business.User();
			return userController.InserUserAccess(userGuid, accessGuids);
		}

		public static decimal GetUserCredit(Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.GetUserCredit(userGuid);
		}

		public static DataTable GetUserServicesForShortcut(Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.GetUserServicesForShortcut(userGuid);
		}

		public static string GetUserName(Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.GetUserName(userGuid);
		}

		public static string GetUserNameAndFamily(Guid userGuid)
		{
			Business.User userController = new Business.User();
			DataTable userInfo = userController.GetUserInfo(userGuid);

			if (userInfo.Rows.Count > 0)
				return string.Format("{0} {1}", Helper.GetString(userInfo.Rows[0]["FirstName"]), Helper.GetString(userInfo.Rows[0]["LastName"]));
			else
				return string.Empty;
		}

		public static Guid GetGroupPriceGuid(Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.GetUserGroupPrice(userGuid);
		}

		public static bool InsertUserGeneralPhoneBook(Guid userGuid, string generalPhoneBookGuid, string newGeneralPhoneBook, decimal sumPrice, bool decreaseFromPanelCharge)
		{
			Business.User userController = new Business.User();
			userController.BeginTransaction();

			try
			{
				if (!userController.DeleteAllUserGeneralPhoneBook(userGuid))
					throw new Exception("ErrorRecord");
				if (!userController.InsertUserGeneralPhoneBook(userGuid, generalPhoneBookGuid))
					throw new Exception("ErrorRecord");

				if (decreaseFromPanelCharge && sumPrice != 0)
				{
					int countNewGeneralPhoneBook = Helper.ImportIntData(newGeneralPhoneBook, "Count");
					for (int counterNewGeneralPhoneBook = 0; counterNewGeneralPhoneBook < countNewGeneralPhoneBook; counterNewGeneralPhoneBook++)
						Facade.Transaction.Decrease(userGuid, Helper.ImportDecimalData(newGeneralPhoneBook, "Price" + counterNewGeneralPhoneBook), TypeCreditChanges.ActivationGeneralPhoneBook, Language.GetString("DecreaseFromPanelChargeForActivationGeneralPhoneBook") + " \"" + Helper.ImportData(newGeneralPhoneBook, "Name" + counterNewGeneralPhoneBook) + "\"", Guid.Empty, userController.DataAccessProvider);
				}

				userController.CommitTransaction();
			}
			catch (Exception ex)
			{
				userController.RollbackTransaction();
				throw ex;
			}
			return true;
		}

		public static DataTable GetGeneralPhoneBookOfUser(Guid parentGuid, Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.GetGeneralPhoneBookOfUser(parentGuid, userGuid);
		}

		public static DataTable GetAllParents(Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.GetAllParents(userGuid);
		}

		public static bool UpdateUserRole(Guid userGuid, Guid roleGuid)
		{
			Business.User userController = new Business.User();
			return userController.UpdateUserRole(userGuid, roleGuid);
		}

		public static DataTable GetServiceOfUserRole(Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.GetServiceOfUserRole(userGuid);
		}

		public static DataTable GetGeneralPhoneBookOfUserRole(Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.GetGeneralPhoneBookOfUserRole(userGuid);
		}

		//public static DataSet GetUsers()
		//{
		//  Business.User userController = new Business.User();
		//  return userController.GetUsers();
		//}

		public static Guid InsertAccount(Common.User user)
		{
			Business.User userController = new Business.User();
			return userController.InsertAccount(user);
		}

		public static DataTable GetUser(string username)
		{
			Business.User userController = new Business.User();
			return userController.GetUser(username);
		}

		public static bool SendSmsForUserExpired()
		{
			Business.User userController = new Business.User();
			return userController.SendSmsForUserExpired();
		}

		public static bool SendSmsForLogin(Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.SendSmsForLogin(userGuid);
		}

		public static bool SendUserAccountInfo(string password, string domain, Guid userGuid)
		{
			Business.User userController = new Business.User();
			return userController.SendUserAccountInfo(password, domain, userGuid);
		}

		public static Guid RegisterUser(Common.User user)
		{
			Business.User userController = new Business.User();
			return userController.RegisterUser(user);
		}

		public static bool RetrievePassword(string username, string rawPassword, string newPassword)
		{
			Business.User userController = new Business.User();
			return userController.RetrievePassword(username, rawPassword, newPassword);
		}

		public static bool UpdateExpireDate(Guid userGuid, DateTime expireDate)
		{
			Business.User userController = new Business.User();
			return userController.UpdateExpireDate(userGuid, expireDate);
		}
	}
}
