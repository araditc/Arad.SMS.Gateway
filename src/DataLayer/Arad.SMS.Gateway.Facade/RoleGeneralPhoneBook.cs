using System;
using System.Data;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace Facade
{
	public class RoleGeneralPhoneBook : FacadeEntityBase
	{
		public static DataTable GetGeneralPhoneBookOfRole(Guid userGuid, Guid roleGuid)
		{
			Business.RoleGeneralPhoneBook roleGeneralPhoneBookController = new Business.RoleGeneralPhoneBook();
			return roleGeneralPhoneBookController.GetGeneralPhoneBookOfRole(userGuid, roleGuid);
		}

		public static bool InsertRoleGeneralPhoneBooks(string data, Guid roleGuid)
		{
			Business.RoleGeneralPhoneBook roleGeneralPhoneBookController = new Business.RoleGeneralPhoneBook();
			Common.RoleGeneralPhoneBook roleGeneralPhoneBook = new Common.RoleGeneralPhoneBook();
			roleGeneralPhoneBookController.BeginTransaction();
			try
			{
				int count = Helper.ImportIntData(data, "resultCount");
				if (count > 0)
				{
					if (!roleGeneralPhoneBookController.DeleteGeneralPhoneBookOfRole(roleGuid))
						throw new Exception(Language.GetString("ErrorRecord"));
				}

				roleGeneralPhoneBook.RoleGuid = roleGuid;
				for (int i = 0; i < count; i++)
				{
					string price = Helper.ImportData(data, ("Price" + i).ToString());
					if (price != string.Empty)
					{
						roleGeneralPhoneBook.GeneralPhoneBookGuid = Helper.ImportGuidData(data, "Guid" + i);
						roleGeneralPhoneBook.Price = Helper.ImportDecimalData(data, "Price" + i);
						if (roleGeneralPhoneBook.HasError)
							throw new Exception(roleGeneralPhoneBook.ErrorMessage);
						if (roleGeneralPhoneBookController.InsertGeneralPhoneBook(roleGeneralPhoneBook) == Guid.Empty)
							throw new Exception(Language.GetString("ErrorRecord"));
					}
				}

				roleGeneralPhoneBookController.CommitTransaction();
				return true;
			}
			catch (Exception ex)
			{
				roleGeneralPhoneBookController.RollbackTransaction();
				throw ex;
			}
		}
	}
}
