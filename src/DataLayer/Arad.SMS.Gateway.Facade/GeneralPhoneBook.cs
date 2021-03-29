using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GeneralLibrary;

namespace Facade
{
	public class GeneralPhoneBook
	{
		//public static DataTable GetGeneralPhoneBookOfUser(Guid userGuid)
		//{
		//	Business.GeneralPhoneBook generalPhoneBookController = new Business.GeneralPhoneBook();
		//	Common.User user = Facade.User.LoadUser(userGuid);
		//	bool isAdmin = Helper.GetBool(user.IsAdmin.ToString());
		//	return generalPhoneBookController.GetGeneralPhoneBookOfUser(isAdmin, user.UserGuid, user.ParentGuid);
		//}

		//public static string GetGeneralPhoneBookOfUser(Guid userGuid, bool showNumberCount, bool showCheckBox)
		//{
		//	Business.GeneralPhoneBook generalPhoneBookController = new Business.GeneralPhoneBook();
		//	string tree = string.Empty;
		//	Common.User user = Facade.User.LoadUser(userGuid);
		//	bool isAdmin = Helper.GetBool(user.IsAdmin.ToString());

		//	DataTable dataTableGeneralPhoneBookUser = generalPhoneBookController.GetGeneralPhoneBookOfUser(isAdmin, user.UserGuid, user.ParentGuid);
		//	DataTable dataTableRoot = new DataTable();//generalPhoneBookController.GetRoot();

		//	if (dataTableRoot.Rows.Count == 0)
		//		return string.Empty;

		//	Guid rootGuid = Helper.GetGuid(dataTableRoot.Rows[0]["Guid"].ToString());

		//	tree += "<div class='myList'><ul class='browser filetree'>";
		//	tree += string.Format("<li><span class='root folder' guid='{0}'>{1}{2}</span><ul id='child'>",
		//																														Guid.Empty,
		//																														showCheckBox ? "<input id='Checkbox0' onclick='checkBoxControlChecked(this);' type='checkbox'/>" : string.Empty,
		//																														dataTableRoot.Rows[0]["Name"].ToString());

		//	GenerateTree(dataTableGeneralPhoneBookUser, rootGuid, showNumberCount, showCheckBox, ref tree);

		//	tree += "</ul></li></ul></div>";

		//	return tree;
		//}

		//private static void GenerateTree(DataTable dataTableGeneralPhoneBookUser, Guid root, bool showNumberCount, bool showCheckBox, ref string tree)
		//{
		//	DataView dataViewGeneralPhoneBookUser = dataTableGeneralPhoneBookUser.DefaultView;
		//	dataViewGeneralPhoneBookUser.RowFilter = string.Format("ParentGuid='{0}'", root);
		//	DataTable dataTableChildren = new DataTable();
		//	dataTableChildren = dataViewGeneralPhoneBookUser.ToTable();
		//	if (dataTableChildren.Rows.Count > 0)
		//		tree += "<ul>";
		//	for (int childrenCounter = 0; childrenCounter < dataTableChildren.Rows.Count; childrenCounter++)
		//	{
		//		tree += string.Format(Helper.GetBool(dataTableChildren.Rows[childrenCounter]["IsActive"].ToString()) ? "<li><span class='folder'  guid='{0}' isActive='{1}'>{2}{3}{4}</span>" : "<li><span class='folder' style='opacity:0.3' disabled='disabled' guid='{0}' isActive='{1}'>{2}{3}{4}</span>",
		//														dataTableChildren.Rows[childrenCounter]["Guid"].ToString(),
		//														Helper.GetBool(dataTableChildren.Rows[childrenCounter]["IsActive"].ToString()),
		//														showCheckBox ? (Helper.GetBool(dataTableChildren.Rows[childrenCounter]["IsActive"].ToString()) ? "<input id='Checkbox" + (childrenCounter + 1) + "' type='checkbox' onclick='checkBoxControlChecked(this);'/>" : "<input id='Checkbox" + (childrenCounter + 1) + "' type='checkbox' disabled='disabled' type='checkbox'/>") : string.Empty,
		//														dataTableChildren.Rows[childrenCounter]["Name"].ToString(),
		//														showNumberCount ? (" (" + dataTableChildren.Rows[childrenCounter]["CountGeneralNumbers"].ToString() + ")") : string.Empty);

		//		Guid childrenGuid = Helper.GetGuid(dataTableChildren.Rows[childrenCounter]["Guid"].ToString());
		//		GenerateTree(dataTableGeneralPhoneBookUser, childrenGuid, showNumberCount, showCheckBox, ref tree);
		//		tree += "</li>";
		//	}
		//	if (dataTableChildren.Rows.Count > 0)
		//		tree += "</ul>";
		//}

		public static Guid InsertItemInGeneralPhoneBook(Common.GeneralPhoneBook generalPhoneBook, Guid userGuid)
		{
			Business.GeneralPhoneBook generalPhoneBookController = new Business.GeneralPhoneBook();
			Business.User userController = new Business.User();
			Common.User user = new Common.User();

			try
			{
				//if (!generalPhoneBookController.CheckingName(generalPhoneBook.Name, generalPhoneBook.ParentGuid))
				//	throw new Exception(Language.GetString("DuplicateGroupName"));
				userController.Load(userGuid, user);

				generalPhoneBook.AdminGuid = user.IsAdmin ? userGuid : user.ParentGuid;
				generalPhoneBook.CreateDate = DateTime.Now;
				generalPhoneBook.UserGuid = userGuid;
				generalPhoneBook.IsPrivate = false;
				return generalPhoneBookController.Insert(generalPhoneBook);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool EditItemInGeneralPhoneBook(Common.GeneralPhoneBook generalPhoneBook)
		{
			Business.GeneralPhoneBook generalPhoneBookController = new Business.GeneralPhoneBook();
			try
			{
				//if (!generalPhoneBookController.CheckingName(generalPhoneBook.Name, generalPhoneBook.ParentGuid))
				//	throw new Exception(Language.GetString("DuplicateGroupName"));

				if (!generalPhoneBookController.UpdateName(generalPhoneBook))
					throw new Exception(Language.GetString("ErrorRecord"));

				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool DeleteItemFromGeneralPhoneBook(Guid generalPhoneBookGuid)
		{
			Business.GeneralPhoneBook generalPhoneBookController = new Business.GeneralPhoneBook();
			return generalPhoneBookController.Delete(generalPhoneBookGuid);
		}

		//public static bool ChangeParent(Guid generalPhoneBookGuid, Guid parentGuid)
		//{
		//	Business.GeneralPhoneBook generalPhoneBookController = new Business.GeneralPhoneBook();
		//	//if (parentGuid == Guid.Empty)
		//	//	parentGuid = Helper.GetGuid(generalPhoneBookController.GetRoot().Rows[0]["Guid"]);

		//	return generalPhoneBookController.UpdateParent(generalPhoneBookGuid, parentGuid);
		//}

		//public static string GetName(Guid generalPhoneBookGuid)
		//{
		//	Business.GeneralPhoneBook generalPhoneBookController = new Business.GeneralPhoneBook();
		//	Common.GeneralPhoneBook generalPhoneBook = new Common.GeneralPhoneBook();
		//	generalPhoneBookController.Load(generalPhoneBookGuid, generalPhoneBook);

		//	return generalPhoneBook.Name;
		//}

		//public static string GetGeneralPhoneBookName(Guid guid)
		//{
		//	Business.GeneralPhoneBook generalPhoneBookController = new Business.GeneralPhoneBook();
		//	return generalPhoneBookController.GetGeneralPhoneBookName(guid);
		//}

		public static DataTable GetGeneralPhoneBook(Guid parentGuid,string name)
		{
			Business.GeneralPhoneBook generalPhoneBookController = new Business.GeneralPhoneBook();
			return generalPhoneBookController.GetGeneralPhoneBook(parentGuid,name);
		}

		public static DataTable GetSendBulkInfo(DataTable dtRecipient)
		{
			Business.GeneralPhoneBook generalPhoneBookController = new Business.GeneralPhoneBook();
			return generalPhoneBookController.GetSendBulkInfo(dtRecipient);
		}
	}
}
