using System;
using System.Collections.Generic;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.Users
{
	public partial class UserShortcuts : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
            var lang = Session["Language"].ToString();

            string service = string.Empty;

			DataTable dataTableUserService = Facade.User.GetUserServicesForShortcut(UserGuid);
			foreach (DataRow row in dataTableUserService.Rows)
			{
				string icon = row["IconAddress"].ToString();
				icon = string.Format("<span class='{0}'></span>", icon);
                if (lang.Equals("fa"))
                {
                    service += string.Format("<li class='Field' code='{0}'>{1}<br/>{2}<br/></li>",
                                                                    row["Guid"],
                                                                    icon,
                                                                    row["TitleFa"]);
                }else
                {
                    service += string.Format("<li class='Field' code='{0}'>{1}<br/>{2}<br/></li>",
                                                                    row["Guid"],
                                                                    icon,
                                                                    row["Title"]);
                }
			}
			literalServices.Text = service;

			DataTable dataTableUserShortcut = Facade.UserSetting.GetUserShortcutForLoad(UserGuid);
			string shortcutElement = string.Empty;

			foreach (DataRow row in dataTableUserShortcut.Rows)
			{
				string icon = row["IconAddress"].ToString();
				//icon = icon != string.Empty ? string.Format("<img src='{0}'/>", icon) : string.Empty;
				icon = string.Format("<span class='{0}'></span>", icon);
                if (lang.Equals("fa"))
                {
                    shortcutElement += string.Format("<li class='Field' code='{0}'>{1}<br/>{2}<br/><input type='checkbox' /></li>",
                                                                                 row["Guid"],
                                                                                 icon,
                                                                                 row["TitleFa"]);
                }else
                {
                    shortcutElement += string.Format("<li class='Field' code='{0}'>{1}<br/>{2}<br/><input type='checkbox' /></li>",
                                                                                 row["Guid"],
                                                                                 icon,
                                                                                 row["Title"]);
                }
			}
			hdnShortcuts.Value = shortcutElement;
			ClientSideScript = string.Format("loadShortcut();");
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.DefineUserShortcut);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Users_UserShortcuts;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Helper.GetString(Business.UserControls.UI_Users_UserShortcuts));
		}
	}
}
