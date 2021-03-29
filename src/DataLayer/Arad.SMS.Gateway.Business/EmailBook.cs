using System;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using Common;
using System.Data;

namespace Business
{
	public class EmailBook : BusinessEntityBase
	{
		public EmailBook(DataAccessBase dataAccessProvider = null)
			: base(TableNames.EmailBooks.ToString(), dataAccessProvider) { }

		public bool UpdateName(Common.EmailBook emailBook)
		{
			return base.ExecuteSPCommand("UpdateName", "@Guid", emailBook.EmailBookGuid, "@Name", emailBook.Name);
		}

		public bool UpdateParent(Guid emailBookGuid, Guid parentGuid)
		{
			return base.ExecuteSPCommand("UpdateParent", "@Guid", emailBookGuid, "@ParentGuid", parentGuid);
		}

		public bool CheckingName(string emailBookName, Guid userGuid)
		{
			DataTable dataTable = base.FetchDataTable("SELECT * FROM [EmailBooks] WHERE [IsDeleted]=0 AND [UserGuid]=@UserGuid AND [Name]=@Name", "@Name", emailBookName, "@UserGuid", userGuid);
			return dataTable.Rows.Count > 0 ? false : true;
		}

		public DataTable GetEmailBookUser(bool isAdmin, Guid userGuid, Guid parentUserGuid)
		{
			if (isAdmin)
				return base.FetchSPDataTable("GetEmailBookUser", "@UserGuid", userGuid);
			else
				return base.FetchSPDataTable("GetEmailBookAdmin", "@UserGuid", userGuid, "@ParentUserGuid", parentUserGuid);
		}

		public Guid GetParent(Guid emailBookGuid)
		{
			DataTable dataTable = base.FetchDataTable("Select * FROM [EmailBooks] WHERE [Guid]=@Guid", "@Guid", emailBookGuid);
			return dataTable.Rows.Count == 0 ? Guid.Empty : Helper.GetGuid(dataTable.Rows[0]["ParentGuid"]);
		}

		public DataTable GetRoot()
		{
			return base.FetchDataTable("SELECT * FROM [EmailBooks] WHERE [ParentGuid]=@GuidEmpty", "@GuidEmpty", Guid.Empty);
		}

		public int GetCountEmailUser(Guid userGuid)
		{
			return Helper.GetInt(base.FetchSPDataTable("GetCountEmailUser", "@UserGuid", userGuid).Rows[0]["CountEmails"]);
		}

		public string GetEmailBookName(Guid guid)
		{
			DataTable dataTable = base.FetchDataTable("Select * FROM [EmailBooks] WHERE [Guid]=@Guid", "@Guid", guid);
			return dataTable.Rows.Count == 0 ? string.Empty : Helper.GetString(dataTable.Rows[0]["Name"]);
		}
	}
}
