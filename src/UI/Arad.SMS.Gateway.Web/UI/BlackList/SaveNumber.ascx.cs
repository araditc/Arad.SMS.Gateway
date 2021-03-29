using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arad.SMS.Gateway.Web.UI.BlackList
{
	public partial class SaveNumber : UIUserControlBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnSave.Text = Language.GetString(btnSave.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);
			btnSave.Attributes["onclick"] = "return validateRequiredFields();";
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				List<string> lstNumbers = txtNumbers.Text.Replace("\r\n", "\n").Split('\n').ToList();

				if (!Facade.PersonsInfo.UpdateBlackListStatus(lstNumbers,true))
					throw new Exception(Language.GetString("ErrorRecord"));

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_BlackList_BlackListNumber, Session)));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)UserControls.UI_BlackList_BlackListNumber, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageBlackListNumber);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_BlackList_SaveNumber;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_BlackList_SaveNumber.ToString());
		}
	}
}