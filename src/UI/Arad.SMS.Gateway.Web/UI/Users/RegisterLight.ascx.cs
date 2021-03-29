using System;
using System.Collections.Generic;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.Web.UI.Users
{
	public partial class RegisterLight : UIUserControlBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			return new List<int>();
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Users_RegisterLight;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Users_RegisterLight.ToString());
		}
	}
}