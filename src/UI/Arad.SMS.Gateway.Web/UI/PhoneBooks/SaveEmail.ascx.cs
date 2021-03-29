using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;

namespace Arad.SMS.Gateway.Web.UI.PhoneBooks
{
	public partial class SaveEmail : UIUserControlBase
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

			foreach (Business.CheckEmailScope scope in System.Enum.GetValues(typeof(Business.CheckEmailScope)))
				drpCheckEmailScope.Items.Add(new ListItem(Language.GetString(scope.ToString()), ((int)scope).ToString()));
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				int countEmailDuplicate = 0;
				List<string> lstFailedEmails = new List<string>();
				List<string> lstEmails = txtEmails.Text.Replace("\r\n", "\n").Split('\n').ToList();

				Business.CheckEmailScope scope = (Business.CheckEmailScope)Enum.Parse(typeof(Business.CheckEmailScope), drpCheckEmailScope.SelectedValue);

				if (!Facade.PhoneNumber.InsertListEmail(UserGuid, PhoneBookGuid, ref lstEmails, ref lstFailedEmails, ref countEmailDuplicate, scope))
					throw new Exception(Language.GetString("ErrorRecord"));

				txtEmailsInvalid.Text += string.Join(Environment.NewLine, lstFailedEmails.ToArray());

				ShowMessageBox(Language.GetString("TotalEmailOfRegistered") + (lstEmails.Count - countEmailDuplicate) + Language.GetString("Number") + "<br/>" +
											 Language.GetString("TotalEmailOfDuplicate") + countEmailDuplicate + Language.GetString("Number") + "<br/>" +
											 Language.GetString("CountEmailFail") + lstFailedEmails.Count + Language.GetString("Number") + "<br/>");
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
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_SaveEmail;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PhoneBooks_SaveEmail.ToString());
		}
	}
}