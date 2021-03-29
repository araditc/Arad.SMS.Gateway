using System;
using Arad.SMS.Gateway.GeneralLibrary;
using System.Collections.Generic;

namespace Arad.SMS.Gateway.Web.UI.Widgets.PanelInformation
{
	public partial class PanelInformation : System.Web.UI.UserControl
	{
		public static string panelExpireDays;

		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			ltrUserName.Text = Facade.User.GetUserNameAndFamily(UserGuid);
			Common.User user = Facade.User.LoadUser(UserGuid);
			int panelFullDays = user.ExpireDate.Subtract(user.CreateDate).Days;
			int panelRemainingDays = user.ExpireDate.Subtract(DateTime.Now).Days;
			panelExpireDays = string.Format("[{0},{1}]", panelRemainingDays, panelFullDays);
		}
	}
}