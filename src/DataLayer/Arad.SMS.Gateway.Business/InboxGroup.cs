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
using System.Collections.Generic;

namespace Arad.SMS.Gateway.Business
{
	public class InboxGroup : BusinessEntityBase
	{
		public InboxGroup(DataAccessBase dataAccessProvider = null)
			: base(TableNames.InboxGroups.ToString(), dataAccessProvider) { }

		public Guid InsertInboxGroup(Common.InboxGroup inboxGroup)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				ExecuteSPCommand("Insert", "@Guid", guid,
																		"@Title", inboxGroup.Title,
																		"@CreateDate", inboxGroup.CreateDate,
																		"@ParentGuid", inboxGroup.ParentGuid,
																		"@UserGuid", inboxGroup.UserGuid);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}

		public bool UpdateInboxGroup(Common.InboxGroup inboxGroup)
		{
			return base.ExecuteSPCommand("UpdatePoll",
																		"@Guid", inboxGroup.InboxGroupGuid,
																		"@Title", inboxGroup.Title,
																		"@ParentGuid", inboxGroup.ParentGuid);
		}

		public DataTable GetUserInboxGroups(Guid userGuid, Guid parentNodeGuid, string name)
		{
			return base.FetchSPDataTable("GetUserInboxGroups",
																	 "@UserGuid", userGuid,
																	 "@ParentNodeGuid", parentNodeGuid,
																	 "@Name", name);
		}

		public DataTable GetUserInboxGroups(Guid userGuid)
		{
			return base.FetchSPDataTable("GetInboxGroups",
																	 "@UserGuid", userGuid);
		}
		//public DataTable GetRoot()
		//{
		//	return base.FetchDataTable("SELECT * FROM [InboxGroups] WHERE [ParentGuid] = @GuidEmpty", "@GuidEmpty", Guid.Empty);
		//}

		//public bool UpdateParent(Guid groupGuid, Guid parentGuid)
		//{
		//	return base.ExecuteSPCommand("UpdateParent", "@Guid", groupGuid, "@ParentGuid", parentGuid);
		//}

		public Guid GetParent(Guid groupGuid)
		{
			DataTable dataTable = base.FetchDataTable("Select * FROM [InboxGroups] WHERE [Guid] = @Guid", "@Guid", groupGuid);
			return dataTable.Rows.Count == 0 ? Guid.Empty : Helper.GetGuid(dataTable.Rows[0]["ParentGuid"]);
		}

		//public bool CheckingName(string groupName, Guid userGuid)
		//{
		//	DataTable dataTable = FetchDataTable("SELECT * FROM [InboxGroups] WHERE [IsDeleted]=0 AND [UserGuid]=@UserGuid AND [Title]=@Title", "@Title", groupName, "@UserGuid", userGuid);
		//	return dataTable.Rows.Count > 0 ? false : true;
		//}

		public bool UpdateName(Common.InboxGroup inboxGroup)
		{
			return ExecuteSPCommand("UpdateName", "@Guid", inboxGroup.InboxGroupGuid, "@Title", inboxGroup.Title);
		}

		//public string GetGroupName(Guid groupGuid)
		//{
		//	DataTable dataTable = FetchDataTable("Select * FROM [InboxGroups] WHERE [Guid]=@Guid", "@Guid", groupGuid);
		//	return dataTable.Rows.Count == 0 ? string.Empty : Helper.GetString(dataTable.Rows[0]["Title"]);
		//}
	}
}
