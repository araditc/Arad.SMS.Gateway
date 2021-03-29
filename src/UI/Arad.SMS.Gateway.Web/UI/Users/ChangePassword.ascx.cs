using System;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Collections.Generic;

namespace Arad.SMS.Gateway.Web.UI.Users
{
	public partial class ChangePassword : UIUserControlBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				btnChangePassword.Text = Language.GetString(btnChangePassword.Text);
				btnChangePassword.Attributes["onclick"] = "return validateSubmit();";
			}
		}

		protected void btnChangePassword_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtNewPassword.Text != txtConfrimNewPassword.Text)
					throw new Exception(Language.GetString("PasswordNotMatchWithConfirmPassword"));

				if (!Facade.User.CahngePassword(Helper.GetGuid(Session["UserGuid"]), txtOldPassword.Text, txtNewPassword.Text))
					throw new Exception(Language.GetString("ErrorRecord"));

				ShowMessageBox(Language.GetString("ChangePasswordDoneSuccessfully"),string.Empty,"success");
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected override System.Collections.Generic.List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ChangePassword);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.Services.ChangePassword;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.Services.ChangePassword.ToString());
		}
	}
}