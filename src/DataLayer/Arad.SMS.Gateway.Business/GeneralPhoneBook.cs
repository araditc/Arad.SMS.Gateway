using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;
using Common;

namespace Business
{
	public class GeneralPhoneBook : BusinessEntityBase
	{
		public GeneralPhoneBook(DataAccessBase dataAccessProvider = null)
			: base(TableNames.GeneralPhoneBooks.ToString(), dataAccessProvider) { }

		//public DataTable GetGeneralPhoneBookOfUser(bool isAdmin, Guid userGuid, Guid parentUserGuid)
		//{
		//	if (isAdmin)
		//		return base.FetchSPDataTable("GetGeneralPhoneBookOfUser", "@UserGuid", userGuid);
		//	else
		//		return base.FetchSPDataTable("GetPhoneBookAdmin", "@UserGuid", userGuid, "@ParentUserGuid", parentUserGuid);
		//}

		//public DataTable GetRoot()
		//{
		//	return base.FetchDataTable("SELECT * FROM [GeneralPhoneBooks] WHERE [ParentGuid]=@GuidEmpty", "@GuidEmpty", Guid.Empty);
		//}

		//public bool CheckingName(string generalPhoneBookName, Guid parentGuid)
		//{
		//	DataTable dataTable = base.FetchDataTable("SELECT * FROM [GeneralPhoneBooks] WHERE [ParentGuid] = @ParentGuid AND [Name] = @Name", "@Name", generalPhoneBookName, "@ParentGuid", parentGuid);
		//	return dataTable.Rows.Count > 0 ? false : true;
		//}

		public bool UpdateName(Common.GeneralPhoneBook generalPhoneBook)
		{
			return ExecuteSPCommand("UpdateName", "@Guid", generalPhoneBook.GeneralPhoneBookGuid,
																						"@Name", generalPhoneBook.Name);
		}

		//public bool UpdateParent(Guid generalPhoneBookGuid, Guid parentGuid)
		//{
		//	return base.ExecuteSPCommand("UpdateParent", "@Guid", generalPhoneBookGuid, "@ParentGuid", parentGuid);
		//}

		//public Guid GetParent(Guid generalPhoneBookGuid)
		//{
		//	DataTable dataTable = base.FetchDataTable("Select * FROM [GeneralPhoneBooks] WHERE [Guid]=@Guid", "@Guid", generalPhoneBookGuid);
		//	return dataTable.Rows.Count == 0 ? Guid.Empty : Helper.GetGuid(dataTable.Rows[0]["ParentGuid"]);
		//}

		//public string GetGeneralPhoneBookName(Guid guid)
		//{
		//	DataTable dataTable = base.FetchDataTable("Select * FROM [GeneralPhoneBooks] WHERE [Guid]=@Guid", "@Guid", guid);
		//	return dataTable.Rows.Count == 0 ? string.Empty : Helper.GetString(dataTable.Rows[0]["Name"]);
		//}

		public DataTable GetGeneralPhoneBook(Guid parentGuid, string name)
		{
			return FetchSPDataTable("GetGeneralPhoneBook", "@ParentGuid", parentGuid, "@Name", name);
		}

		public DataTable GetSendBulkInfo(DataTable dtRecipient)
		{
			return FetchSPDataTable("GetSendBulkInfo", "@BulkRecipient", dtRecipient);
		}
	}
}
