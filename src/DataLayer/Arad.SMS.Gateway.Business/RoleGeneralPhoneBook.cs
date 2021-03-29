using System;
using System.Data;
using Common;
using GeneralLibrary.BaseCore;

namespace Business
{
	public class RoleGeneralPhoneBook : BusinessEntityBase
	{
		public RoleGeneralPhoneBook(DataAccessBase dataAccessProvider = null)
			: base(TableNames.RoleGeneralPhoneBooks.ToString(), dataAccessProvider) { }

		public DataTable GetGeneralPhoneBookOfRole(Guid userGuid, Guid roleGuid)
		{
			return FetchSPDataTable("GetGeneralPhoneBookOfRole", "@UserGuid", userGuid,
																													"@RoleGuid", roleGuid);
		}

		public bool DeleteGeneralPhoneBookOfRole(Guid roleGuid)
		{
			return ExecuteSPCommand("DeleteGeneralPhoneBookOfRole", "@RoleGuid", roleGuid);
		}

		public Guid InsertGeneralPhoneBook(Common.RoleGeneralPhoneBook roleGeneralPhoneBook)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				ExecuteSPCommand("Insert", "@Guid", guid,
																	"@Price", roleGeneralPhoneBook.Price,
																	"@GeneralPhoneBookGuid", roleGeneralPhoneBook.GeneralPhoneBookGuid,
																	"@RoleGuid", roleGeneralPhoneBook.RoleGuid);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}
	}
}
