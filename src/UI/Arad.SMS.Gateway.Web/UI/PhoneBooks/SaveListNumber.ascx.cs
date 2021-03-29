using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.PhoneBooks
{
	public partial class SaveListNumber : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid PhoneBookGuid
		{
			get { return Helper.RequestGuid(this, "Guid"); }
		}

		private int Type
		{
			get { return Helper.GetInt(Helper.Request(this, "Type")); }
		}

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

			foreach (Business.CheckNumberScope scope in System.Enum.GetValues(typeof(Business.CheckNumberScope)))
				drpCheckNumberScope.Items.Add(new ListItem(Language.GetString(scope.ToString()), ((int)scope).ToString()));
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				int countNumberDuplicate = 0;
				List<string> lstFailedNumbers = new List<string>();
				List<string> lstNumbers = txtNumbers.Text.Replace("\r\n", "\n").Split('\n').ToList();

				Business.CheckNumberScope scope = (Business.CheckNumberScope)Enum.Parse(typeof(Business.CheckNumberScope), drpCheckNumberScope.SelectedValue);

				if (!Facade.PhoneNumber.InsertListNumber(UserGuid, PhoneBookGuid, ref lstNumbers, ref lstFailedNumbers, ref countNumberDuplicate, scope))
					throw new Exception(Language.GetString("ErrorRecord"));

				txtNumbersInvalid.Text += string.Join(Environment.NewLine, lstFailedNumbers.ToArray());
				ShowMessageBox(Language.GetString("TotalNumberOfRegistered") + (lstNumbers.Count - countNumberDuplicate) + Language.GetString("Number") + "<br/>" +
												Language.GetString("TotalNumberOfDuplicate") + countNumberDuplicate + Language.GetString("Number") + "<br/>" +
												Language.GetString("CountNumberFail") + lstFailedNumbers.Count + Language.GetString("Number") + "<br/>", string.Empty, "success");
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}&Guid={1}&Type={2}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_PhoneNumber, Session), PhoneBookGuid, Type));
		}
		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.AddListNumber);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_SaveListNumber;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PhoneBooks_SaveListNumber.ToString());
		}
	}
}