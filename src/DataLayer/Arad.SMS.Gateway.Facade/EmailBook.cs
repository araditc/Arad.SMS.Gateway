using System;
using System.Data;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace Facade
{
	public class EmailBook : FacadeEntityBase
	{
		public static DataTable GetEmailBookOfUser(Guid userGuid)
		{
			Business.EmailBook emailBookController = new Business.EmailBook();
			Common.User user = Facade.User.LoadUser(userGuid);
			bool isAdmin = Helper.GetBool(user.IsAdmin.ToString());
			return emailBookController.GetEmailBookUser(isAdmin, user.UserGuid, user.ParentGuid);
		}

		public static string GetEmailBookOfUser(Guid userGuid, bool showEmailCount, bool showCheckBox)
		{
			Business.EmailBook emailBookController = new Business.EmailBook();
			string tree = string.Empty;
			Common.User user = Facade.User.LoadUser(userGuid);
			bool isAdmin = Helper.GetBool(user.IsAdmin.ToString());
			DataTable dataTableEmailBookUser = emailBookController.GetEmailBookUser(isAdmin, user.UserGuid, user.ParentGuid);

			DataTable dataTableRoot = emailBookController.GetRoot();

			if (dataTableRoot.Rows.Count == 0)
				return string.Empty;

			Guid rootGuid = Helper.GetGuid(dataTableRoot.Rows[0]["Guid"].ToString());

			tree += "<div class='myList'><ul class='browser filetree'>";
			tree += string.Format("<li><span class='root folder' guid='{0}'>{1}{2}</span><ul id='child'>",
																																Guid.Empty,
																																showCheckBox ? "<input onclick='checkBoxControlChecked(this);' type='checkbox'/>" : string.Empty,
																																dataTableRoot.Rows[0]["Name"].ToString());

			GenerateTree(dataTableEmailBookUser, rootGuid, showEmailCount, showCheckBox, ref tree);

			tree += "</ul></li></ul></div>";

			return tree;
		}

		private static void GenerateTree(DataTable dataTableEmailbookUser, Guid root, bool showEmailCount, bool showCheckBox, ref string tree)
		{
			DataView dataViewEmailBookUser = dataTableEmailbookUser.DefaultView;
			dataViewEmailBookUser.RowFilter = string.Format("ParentGuid='{0}'", root);
			DataTable dataTableChildren = new DataTable();
			dataTableChildren = dataViewEmailBookUser.ToTable();
			if (dataTableChildren.Rows.Count > 0)
				tree += "<ul>";
			for (int childrenCounter = 0; childrenCounter < dataTableChildren.Rows.Count; childrenCounter++)
			{
				tree += string.Format("<li><span class='folder' guid='{0}' isActive='True'>{1}{2}{3}</span>",
																			dataTableChildren.Rows[childrenCounter]["Guid"].ToString(),
																			showCheckBox ? "<input id='Checkbox" + (childrenCounter + 1) + "' onclick='checkBoxControlChecked(this);' type='checkbox'/>" : string.Empty,
																			dataTableChildren.Rows[childrenCounter]["Name"].ToString(),
																			showEmailCount ? (" (" + dataTableChildren.Rows[childrenCounter]["CountEmails"].ToString() + ")") : string.Empty);

				Guid childrenGuid = Helper.GetGuid(dataTableChildren.Rows[childrenCounter]["Guid"].ToString());
				GenerateTree(dataTableEmailbookUser, childrenGuid, showEmailCount, showCheckBox,ref tree);
				tree += "</li>";
			}
			if (dataTableChildren.Rows.Count > 0)
				tree += "</ul>";
		}

		public static Guid InsertItemInEmailBook(Common.EmailBook emailBook)
		{
			Business.EmailBook emailBookController = new Business.EmailBook();
			if (emailBook.ParentGuid == Guid.Empty)
				emailBook.ParentGuid = Helper.GetGuid(emailBookController.GetRoot().Rows[0]["Guid"]);

			if (!emailBookController.CheckingName(emailBook.Name, emailBook.UserGuid))
				return Guid.Empty;
			else
			{
				Business.User userController = new Business.User();

				if (userController.IsAdminUser(emailBook.UserGuid))
					emailBook.AdminGuid = emailBook.UserGuid;
				else
					emailBook.AdminGuid = userController.GetParentGuid(emailBook.UserGuid);

				emailBook.CreateDate = DateTime.Now;
				emailBook.UserGuid = emailBook.UserGuid;
				return emailBookController.Insert(emailBook);
			}
		}

		public static bool EditItemInEmailBook(Common.EmailBook emailBook)
		{
			Business.EmailBook emailBookController = new Business.EmailBook();
			if (!emailBookController.CheckingName(emailBook.Name, emailBook.UserGuid))
				return false;
			else
				return emailBookController.UpdateName(emailBook);
		}

		public static bool DeleteItemFromEmailBook(Guid emailBookGuid)
		{
			Business.EmailBook emailBookController = new Business.EmailBook();
			return emailBookController.Delete(emailBookGuid);
		}

		public static bool ChangeParent(Guid emailBookGuid, Guid parentGuid)
		{
			Business.EmailBook emailBookController = new Business.EmailBook();
			if (parentGuid == Guid.Empty)
				parentGuid = Helper.GetGuid(emailBookController.GetRoot().Rows[0]["Guid"]);

			return emailBookController.UpdateParent(emailBookGuid, parentGuid);
		}

		public static string GetEmailBookName(Guid guid)
		{
			Business.EmailBook emailBookController = new Business.EmailBook();
			return emailBookController.GetEmailBookName(guid);
		}
	}
}
