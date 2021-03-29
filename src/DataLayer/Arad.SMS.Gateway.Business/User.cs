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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Arad.SMS.Gateway.Business
{
	public class User : BusinessEntityBase
	{
		public User(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Users.ToString(), dataAccessProvider)
		{ }

		#region SelectMethod
		public DataTable GetCountUserNumberOfOperators(Guid parentGuid)
		{
			return FetchSPDataTable("GetCountUserNumberOfOperators", "@ParentGuid", parentGuid);
		}

		public DataTable GetCountRoleNumberOfOperators(Guid roleGuid, Guid userGuid)
		{
			return FetchSPDataTable("GetCountRoleNumberOfOperators", "@RoleGuid", roleGuid
																															, "@UserGuid", userGuid);
		}
		#endregion

		public DataTable GetPagedUsers(Guid userGuid, Guid domainGuid, string query, string sortField, int pageNo, int pageSize, ref int rowCount)
		{
			DataSet usersInfo = base.FetchSPDataSet("GetPagedUsers",
																							"@UserGuid", userGuid,
																							"@DomainGuid", domainGuid,
																							"@Query", query,
																							"@PageNo", pageNo,
																							"@PageSize", pageSize,
																							"@SortField", sortField);
			rowCount = Helper.GetInt(usersInfo.Tables[0].Rows[0]["RowCount"]);

			return usersInfo.Tables[1];
		}

		public DataTable GetChildren(Common.User user, int userType, int activeStatus)
		{
			return base.FetchSPDataTable("GetChildren", "@ParentGuid", user.ParentGuid,
																									"@UserName", user.UserName,
																									"@FirstName", user.FirstName,
																									"@LastName", user.LastName,
																									"@Email", user.Email,
																									"@CellPhone", user.Mobile,
																									"@CreateDate", Helper.GetDateTimeForDB(user.CreateDate),
																									"@ExpireDate", Helper.GetDateTimeForDB(user.ExpireDate),
																									"@PanelPrice", user.PanelPrice,
																									"@IsActive", (activeStatus == 2 ? DBNull.Value : (object)activeStatus),
																									"@UserType", userType);
		}

		public string GetCurrentPassword(Guid userGuid)
		{
			DataTable dataTable = base.FetchDataTable("SELECT [Password] FROM [Users] WHERE [Guid] = @UserGuid", "@UserGuid", userGuid);
			if (dataTable.Rows.Count == 0)
				return string.Empty;
			else
				return Helper.GetString(dataTable.Rows[0]["Password"]);
		}

		public decimal GetUserCredit(Guid userGuid)
		{
			return base.GetSPDecimalFieldValue("GetUserCredit", "@UserGuid", userGuid);
		}

		public bool CheckUserNameValid(string userName, Guid userGuid)
		{
			DataTable dataTable = base.FetchDataTable("SELECT COUNT(*) AS [Count] FROM [Users] WHERE [UserName] = @UserName AND [Guid] != @UserGuid", "@UserName", userName, "@UserGuid", userGuid);
			return Helper.GetInt(dataTable.Rows[0]["Count"]) == 0 ? true : false;
		}

		public bool UpdateProfile(Common.User user)
		{
			return base.ExecuteSPCommand("UpdateProfile",
																	 "@Guid", user.UserGuid,
																	 "@FirstName", user.FirstName,
																	 "@LastName", user.LastName,
																	 "@FatherName", user.FatherName,
																	 "@NationalCode", user.NationalCode,
																	 "@ShCode", user.ShCode,
																	 "@Email", user.Email,
																	 "@Phone", user.Phone,
																	 "@Mobile", user.Mobile,
																	 "@FaxNumber", user.FaxNumber,
																	 "@Address", user.Address,
																	 "@ZoneGuid", user.ZoneGuid,
																	 "@BirthDate", Helper.GetDateTimeForDB(user.BirthDate),
																	 "@ZipCode", user.ZipCode,
																	 "@Type", user.Type,
																	 "@CompanyName", user.CompanyName,
																	 "@CompanyNationalId", user.CompanyNationalId,
																	 "@EconomicCode", user.EconomicCode,
																	 "@CompanyCEOName", user.CompanyCEOName,
																	 "@CompanyCEONationalCode", user.CompanyCEONationalCode,
																	 "@CompanyCEOMobile", user.CompanyCEOMobile,
																	 "@CompanyPhone", user.CompanyPhone,
																	 "@CompanyZipCode", user.CompanyZipCode,
																	 "@CompanyAddress", user.CompanyAddress);
		}

		public bool AdvanceUpdate(Common.User user)
		{
			try
			{
				if (user.IsActive)
				{
					DataSet dataSetCountChildrenType = FetchSPDataSet("GetCountChildrenType", "@ParentGuid", user.ParentGuid);
					int countCurrentAdminUser = Helper.GetInt(dataSetCountChildrenType.Tables[0].Rows[0]["CountAdmin"]);
					int countCurrentUser = Helper.GetInt(dataSetCountChildrenType.Tables[0].Rows[0]["CountUser"]);
					int maximumAdmin = Helper.GetInt(dataSetCountChildrenType.Tables[1].Rows.Count > 0 ? dataSetCountChildrenType.Tables[1].Rows[0]["MaximumAdmin"] : 0);
					int maximumUser = Helper.GetInt(dataSetCountChildrenType.Tables[1].Rows.Count > 0 ? dataSetCountChildrenType.Tables[1].Rows[0]["MaximumUser"] : 0);
					if ((user.MaximumUser > 0 && user.IsAdmin) || (user.MaximumAdmin > 0 && user.IsAdmin))
					{
						if (countCurrentAdminUser >= maximumUser)
							throw new Exception(Language.GetString("ErrorMaximumAdmin"));
					}
					else if (user.IsAdmin)
					{
						if (countCurrentUser >= maximumUser)
							throw new Exception(Language.GetString("ErrorMaximumUser"));
					}
				}

				return base.ExecuteSPCommand("AdvanceUpdate",
																		 "@Guid", user.UserGuid,
																		 "@UserName", user.UserName,
																		 "@Password", user.Password,
																		 "@Type", user.Type,
																		 "@IsActive", user.IsActive,
																		 "@MaximumAdmin", user.MaximumAdmin,
																		 "@MaximumUser", user.MaximumUser,
																		 "@MaximumPhoneNumber", user.MaximumPhoneNumber,
																		 "@MaximumEmailAddress", user.MaximumEmailAddress,
																		 "@RoleGuid", user.RoleGuid,
																		 "@PriceGroupGuid", user.PriceGroupGuid,
																		 "@IsFixPriceGroup", user.IsFixPriceGroup,
																		 "@IsAuthenticated", user.IsAuthenticated,
																		 "@DomainGuid", user.DomainGuid,
																		 "@PanelPrice", user.PanelPrice,
																		 "@ExpireDate", Helper.GetDateTimeForDB(user.ExpireDate));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool UpdatePassword(Guid userGuid, string password)
		{
			return base.ExecuteSPCommand("UpdatePassword", "@Guid", userGuid, "@Password", password);
		}

		public bool UpdateCreditAndGroupPrice(Guid userGuid, decimal credit, decimal smsCount)
		{
			return ExecuteSPCommand("UpdateCreditAndGroupPrice", "@UserGuid", userGuid,
																													 "@Credit", credit,
																													 "@SmsCount", smsCount);
		}

		public bool UpdateCredit(Guid userGuid, decimal credit)
		{
			return base.ExecuteCommand("UPDATE [Users] SET [Credit] = @Credit WHERE [Guid] = @Guid", "@Credit", credit, "@Guid", userGuid);
		}

		public bool Login(Common.User user, string challengeString, bool autoLogin)
		{
			DataTable dataTable = base.FetchSPDataTable("GetUserForLogin",
																									"@UserName", user.UserName,
																									"@DomainGuid", user.DomainGuid);
			bool isValid = false;

			if (dataTable.Rows.Count == 1)
			{
				var realPassword = Helper.GetMd5Hash(challengeString + Helper.GetString(dataTable.Rows[0]["Password"]));
				if (challengeString != string.Empty && realPassword.ToLower() == user.Password.ToLower())
					isValid = true;
				else if (autoLogin && challengeString == string.Empty)
					isValid = true;

				if (isValid)
				{
					user.IsAuthenticated = Helper.GetBool(dataTable.Rows[0]["IsAuthenticated"].ToString());
					user.IsActive = Helper.GetBool(dataTable.Rows[0]["IsActive"].ToString());
					user.IsAdmin = Helper.GetBool(dataTable.Rows[0]["IsAdmin"].ToString());
					user.IsSuperAdmin = Helper.GetBool(dataTable.Rows[0]["IsSuperAdmin"].ToString());
					user.IsMainAdmin = Helper.GetBool(dataTable.Rows[0]["IsMainAdmin"].ToString());
					user.UserGuid = Helper.GetGuid(dataTable.Rows[0]["Guid"]);
					user.DomainGuid = Helper.GetGuid(dataTable.Rows[0]["DomainGuid"]);
					user.Password = Helper.GetString(dataTable.Rows[0]["Password"]);
					user.ParentGuid = Helper.GetGuid(dataTable.Rows[0]["ParentGuid"]);
					user.ExpireDate = Helper.GetDateTime(dataTable.Rows[0]["ExpireDate"]);
					user.FirstName = Helper.GetString(dataTable.Rows[0]["FirstName"]);
					user.LastName = Helper.GetString(dataTable.Rows[0]["LastName"]);
					user.NationalCode = Helper.GetString(dataTable.Rows[0]["NationalCode"]);
					user.UserName = Helper.GetString(dataTable.Rows[0]["UserName"]);
					user.Mobile = Helper.GetString(dataTable.Rows[0]["Mobile"]);
					user.Email = Helper.GetString(dataTable.Rows[0]["Email"]);
					user.Credit = Helper.GetDecimal(dataTable.Rows[0]["Credit"]);
				}
			}

			return isValid;
		}

		public bool Login(Common.User user, bool autoLogin)
		{
			return Login(user, string.Empty, autoLogin);
		}

		public bool IsAdminUser(Guid userGuid)
		{
			DataTable dataTable = base.FetchDataTable("SELECT [IsAdmin] FROM [Users] WHERE [Guid] = @UserGuid", "@UserGuid", userGuid);
			if (dataTable.Rows.Count == 0)
				return false;
			else
				return Helper.GetBool(dataTable.Rows[0]["IsAdmin"]);
		}

		public Guid GetParentGuid(Guid userGuid)
		{
			DataTable dataTable = base.FetchDataTable("SELECT [ParentGuid] FROM [Users] WHERE [Guid] = @UserGuid", "@UserGuid", userGuid);
			if (dataTable.Rows.Count == 0)
				return Guid.Empty;
			else
				return Helper.GetGuid(dataTable.Rows[0]["ParentGuid"]);
		}

		public DataTable GetServiceOfUser(Guid parentGuid, Guid userGuid)
		{
			return base.FetchSPDataTable("GetServiceOfUserForActivation", "@ParentGuid", parentGuid,
																																		"@UserGuid", userGuid);
		}

		public bool UpdateGroupGuid(Guid userGuid, Guid groupPriceGuid)
		{
			return base.ExecuteSPCommand("UpdateGroupPriceOfUser", "@Guid", userGuid,
																														"@GroupPriceGuid", groupPriceGuid);
		}

		public bool InsertUserService(Guid userGuid, string serviceGuids)
		{
			GeneralLibrary.Security.SecurityManager.ClearServicePermissionCache(userGuid);
			return base.ExecuteSPCommand("InsertUserService", "@UserGuid", userGuid,
																												"@ServiceGuids", serviceGuids);
		}

		public bool InserUserAccessByService(Guid userGuid, string serviceGuids)
		{
			GeneralLibrary.Security.SecurityManager.ClearAccessPermissionCache(userGuid);
			return base.ExecuteSPCommand("InsertUserAccessByService", "@UserGuid", userGuid,
																																"@ServiceGuids", serviceGuids);
		}

		public bool InserUserAccess(Guid userGuid, string accessGuids)
		{
			GeneralLibrary.Security.SecurityManager.ClearAccessPermissionCache(userGuid);
			return base.ExecuteSPCommand("InsertUserAccess", "@UserGuid", userGuid,
																											 "@AccessGuids", accessGuids);
		}

		public DataTable GetAccessOfUser(Guid parentGuid, Guid userGuid)
		{
			return base.FetchSPDataTable("GetAccessOfUserForActivation", "@ParentGuid", parentGuid,
																																	 "@UserGuid", userGuid);
		}

		public bool DeleteAllUserService(Guid userGuid)
		{
			return base.ExecuteSPCommand("DeleteAllUserService", "@UserGuid", userGuid);
		}

		public DataTable GetUserServicesForShortcut(Guid userGuid)
		{
			return base.FetchSPDataTable("GetUserServicesForShortcut", "@UserGuid", userGuid);
		}

		public string GetUserName(Guid userGuid)
		{
			DataTable userInfo = GetUserInfo(userGuid);

			if (userInfo.Rows.Count > 0)
				return Helper.GetString(userInfo.Rows[0]["UserName"]);
			else
				return string.Empty;
		}

		public DataTable GetUserInfo(Guid userGuid)
		{
			return base.FetchSPDataTable("GetUserInfo", "@UserGuid", userGuid);
		}

		public Guid GetUserGroupPrice(Guid userGuid)
		{
			return base.GetSPGuidFieldValue("GetUserGroupPrice", "@UserGuid", userGuid);
		}

		public bool InsertUserGeneralPhoneBook(Guid userGuid, string generalPhoneBookGuids)
		{
			return base.ExecuteSPCommand("InsertUserGeneralPhoneBook", "@UserGuid", userGuid,
																																 "@GeneralPhoneBookGuids", generalPhoneBookGuids);
		}

		public bool DeleteAllUserGeneralPhoneBook(Guid userGuid)
		{
			return base.ExecuteSPCommand("DeleteAllUserGeneralPhoneBook", "@UserGuid", userGuid);
		}

		public DataTable GetGeneralPhoneBookOfUser(Guid parentGuid, Guid userGuid)
		{
			return base.FetchSPDataTable("GetGeneralPhoneBookOfUserForActivation", "@ParentGuid", parentGuid,
																																						 "@UserGuid", userGuid);
		}

		public DataTable GetAllParents(Guid userGuid)
		{
			return FetchSPDataTable("GetAllParents", "@ChildGuid", userGuid);
		}

		public bool UpdateUserRole(Guid userGuid, Guid roleGuid)
		{
			return ExecuteCommand("UPDATE [Users] SET [RoleGuid] = @RoleGuid WHERE [Guid] = @Guid", "@RoleGuid", roleGuid, "@Guid", userGuid);
		}

		public DataTable GetServiceOfUserRole(Guid userGuid)
		{
			return FetchSPDataTable("GetServiceOfUserRole", "@UserGuid", userGuid);
		}

		public DataTable GetGeneralPhoneBookOfUserRole(Guid userGuid)
		{
			return FetchSPDataTable("GetGeneralPhoneBookOfUserRole", "@UserGuid", userGuid);
		}

		public bool UpdateDomainGroupPrice(Guid userGuid, Guid domainGroupPriceGuid)
		{
			return base.ExecuteSPCommand("UpdateDomainGroupPrice", "@UserGuid", userGuid, "@DomainGroupPriceGuid", domainGroupPriceGuid);
		}

		public Guid InsertAccount(Common.User user)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				ExecuteSPCommand("InsertAccount",
												 "@Guid", guid,
												 "@UserName", user.UserName,
												 "@Password", user.Password,
												 "@ParentGuid", user.ParentGuid,
												 "@RoleGuid", user.RoleGuid,
												 "@PriceGroupGuid", user.PriceGroupGuid,
												 "@IsFixPriceGroup", user.IsFixPriceGroup,
												 "@IsAdmin", user.IsAdmin,
												 "@IsActive", user.IsActive,
												 "@ExpireDate", user.ExpireDate,
												 "@MaximumAdmin", user.MaximumAdmin,
												 "@MaximumUser", user.MaximumUser,
												 "@MaximumEmailAddress", user.MaximumEmailAddress,
												 "@MaximumPhoneNumber", user.MaximumPhoneNumber,
												 "@PanelPrice", user.PanelPrice,
												 "@DomainGuid", user.DomainGuid);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}

		public DataTable GetUser(string username)
		{
			return FetchDataTable("SELECT * FROM [Users] WHERE [UserName] = @Username AND [IsDeleted] = 0", "@Username", username);
		}

		public bool SendSmsForUserExpired()
		{
			return ExecuteSPCommand("SendSmsForUserExpired");
		}

		public bool SendSmsForLogin(Guid userGuid)
		{
			return ExecuteSPCommand("SendSmsForLogin", "@UserGuid", userGuid);
		}

		public bool SendUserAccountInfo(string password, string domain, Guid userGuid)
		{
			return ExecuteSPCommand("SendUserAccountInfo",
															"@Password", password,
															"@Domain", domain,
															"@UserGuid", userGuid);
		}

		public Guid RegisterUser(Common.User user)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				ExecuteSPCommand("RegisterUser",
												 "@Guid", guid,
												 "@Type", user.Type,
												 "@UserName", user.UserName,
												 "@Password", user.Password,
												 "@Email", user.Email,
												 "@Mobile", user.Mobile,
												 "@ParentGuid", user.ParentGuid,
												 "@RoleGuid", user.RoleGuid,
												 "@PriceGroupGuid", user.PriceGroupGuid,
												 "@IsFixPriceGroup", user.IsFixPriceGroup,
												 "@IsAdmin", user.IsAdmin,
												 "@IsActive", user.IsActive,
												 "@ExpireDate", user.ExpireDate,
												 "@MaximumAdmin", user.MaximumAdmin,
												 "@MaximumUser", user.MaximumUser,
												 "@PanelPrice", user.PanelPrice,
												 "@DomainGuid", user.DomainGuid);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}

		public bool RetrievePassword(string username, string rawPassword, string newPassword)
		{
			return ExecuteSPCommand("RetrievePassword",
															"@UserName", username,
															"@RawPassword", rawPassword,
															"@NewPassword", newPassword);
		}

		public bool UpdateExpireDate(Guid userGuid, DateTime expireDate)
		{
			return ExecuteSPCommand("UpdateExpireDate",
															"@UserGuid", userGuid,
															"@ExpireDate", expireDate);
		}
	}
}
