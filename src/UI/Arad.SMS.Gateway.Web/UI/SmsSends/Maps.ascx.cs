using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;

namespace Arad.SMS.Gateway.Web.UI.SmsSends
{
	public partial class Maps : UIUserControlBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SendBulkSms);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_SmsSends_Maps;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_SmsSends_Maps.ToString());
		}
	}
}