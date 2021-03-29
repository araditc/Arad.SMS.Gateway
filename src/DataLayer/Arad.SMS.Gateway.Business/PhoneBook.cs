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

using System;
using System.Data;
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Business
{
	public class PhoneBook : GeneralLibrary.BaseCore.BusinessEntityBase
	{
		public PhoneBook(DataAccessBase dataAccessProvider = null)
			: base(TableNames.PhoneBooks.ToString(), dataAccessProvider)
		{ }

		public bool UpdateName(Common.PhoneBook phoneBook)
		{
			return base.ExecuteSPCommand("UpdateName", "@Guid", phoneBook.PhoneBookGuid, "@Name", phoneBook.Name);
		}

		//public bool UpdateParent(Guid phoneBookGuid, Guid parentGuid)
		//{
		//	return base.ExecuteSPCommand("UpdateParent", "@Guid", phoneBookGuid, "@ParentGuid", parentGuid);
		//}

		//public bool CheckingName(string phoneBookName, Guid userGuid)
		//{
		//	DataTable dataTable = new DataTable();
		//	dataTable = base.FetchDataTable("SELECT * FROM [PhoneBooks] WHERE [IsDeleted]=0 AND [UserGuid]=@UserGuid AND [Name]=@Name", "@Name", phoneBookName, "@UserGuid", userGuid);
		//	return dataTable.Rows.Count > 0 ? false : true;
		//}

		public DataTable GetPhoneBookUser(Guid userGuid)
		{
			return FetchDataTable("SELECT * FROM [PhoneBooks] WHERE [IsDeleted] = 0 AND [UserGuid] = @UserGuid", "@UserGuid", userGuid);
		}

		public DataTable GetPhoneBookUser(Guid userGuid, Guid parentNodeGuid, string name, bool loadAllPhoneBook = false)//bool isAdmin, Guid userGuid, Guid parentUserGuid)
		{
			//if (isAdmin)
			return base.FetchSPDataTable("GetPhoneBookUser",
																	 "@UserGuid", userGuid,
																	 "@ParentNodeGuid", parentNodeGuid,
																	 "@Name", name,
																	 "@LoadAllPhoneBook", loadAllPhoneBook);
			//else
			//	return base.FetchSPDataTable("GetPhoneBookAdmin", "@UserGuid", userGuid, "@ParentUserGuid", parentUserGuid);
		}

		//public Guid GetParent(Guid phoneBookGuid)
		//{
		//	DataTable dataTable = base.FetchDataTable("Select * FROM PhoneBooks WHERE [Guid]=@Guid", "@Guid", phoneBookGuid);
		//	return dataTable.Rows.Count == 0 ? Guid.Empty : Helper.GetGuid(dataTable.Rows[0]["ParentGuid"]);
		//}

		//public DataTable GetRoot()
		//{
		//	return base.FetchDataTable("SELECT * FROM PhoneBooks WHERE [ParentGuid] = @GuidEmpty", "@GuidEmpty", Guid.Empty);
		//}

		public int GetCountNumberUser(Guid userGuid)
		{
			return Helper.GetInt(base.FetchSPDataTable("GetCountNumberUser", "@UserGuid", userGuid).Rows[0]["CountNumber"]);
		}

		//public string GetPhoneBookName(Guid guid)
		//{
		//	DataTable dataTable = base.FetchDataTable("Select * FROM PhoneBooks WHERE [Guid]=@Guid", "@Guid", guid);
		//	return dataTable.Rows.Count == 0 ? string.Empty : Helper.GetString(dataTable.Rows[0]["Name"]);
		//}

		public bool UpdateVasSetting(Common.PhoneBook phoneBook)
		{
			try
			{
				return base.ExecuteSPCommand("UpdateVasSetting",
																		 "@Guid", phoneBook.PhoneBookGuid,
																		 "@Type", phoneBook.Type,
																		 "@ServiceId", phoneBook.ServiceId,
																		 "@AlternativeUserGuid", phoneBook.AlternativeUserGuid,
																		 "@VASRegisterKeys", phoneBook.VASRegisterKeys,
																		 "@VASUnsubscribeKeys", phoneBook.VASUnsubscribeKeys);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool RegisterService(string mobile, string serviceId,ref bool numberExist)
		{
			try
			{
				numberExist = GetBoolFieldValue("RegisterService",
																				"@Mobile", mobile,
																				"@ServiceId", serviceId);
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool UnSubscribeService(string mobile, string serviceId, ref bool numberExist)
		{
			try
			{
				numberExist = GetBoolFieldValue("UnSubscribeService",
																				"@Mobile", mobile,
																				"@ServiceId", serviceId);
				return true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public DataTable GetActiveServices(string mobile)
		{
			return FetchSPDataTable("GetActiveServices", "@Mobile", mobile);
		}

		public DataTable GetDisabledServices(string mobile)
		{
			return FetchSPDataTable("GetDisabledServices", "@Mobile", mobile);
		}

		public DataTable GetAllServices()
		{
			return FetchSPDataTable("GetAllServices");
		}

		public Guid GetUserVasGroup(int groupId, string serviceId, Guid userGuid)
		{
			return GetSPGuidFieldValue("GetUserVasGroup",
																 "@UserGuid", userGuid,
																 "@ServiceId", serviceId,
																 "@GroupId", groupId);
		}

		public Guid RecipientIsRegisteredToVasGroup(int groupId, string receiver)
		{
			return GetSPGuidFieldValue("RecipientIsRegisteredToVasGroup",
																 "@GroupId", groupId,
																 "@Receiver", receiver);
		}

		public DataTable GetPhoneBookInfo(string phoneBookGuids)
		{
			return FetchSPDataTable("GetPhoneBookInfo",
															"@PhoneBookGuids", phoneBookGuids);
		}

		public bool UpdateGroup(Guid phoneBookGuid, PhoneBookGroupType type, string name)
		{
			return ExecuteSPCommand("UpdateGroup",
															"@PhoneBookGuid", phoneBookGuid,
															"@Type", type,
															"@Name", name);
		}

		public DataTable GetUserMaximumRecordInfo(Guid userGuid)
		{
			return FetchSPDataTable("GetUserMaximumRecordInfo",
															"@UserGuid", userGuid);
		}
	}
}
