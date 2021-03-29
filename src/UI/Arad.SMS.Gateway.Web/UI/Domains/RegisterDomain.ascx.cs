using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.Domains
{
	public partial class RegisterDomain : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid DomainGuid
		{
			get { return Helper.RequestGuid(this, "Guid"); }
		}

		private string ActionType
		{
			get { return Helper.Request(this, "ActionType").ToLower(); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			try
			{
				btnSave.Text = Language.GetString(btnSave.Text);
				btnCancel.Text = Language.GetString(btnCancel.Text);
				btnSave.Attributes["onclick"] = "return validateRequiredFields();";

				foreach (Business.Desktop desktop in Enum.GetValues(typeof(Business.Desktop)))
					if (desktop != Business.Desktop.Default)
						drpStartUpPage.Items.Add(new ListItem(Language.GetString(desktop.ToString()), ((int)desktop).ToString()));

				foreach (Business.DefaultPages defaultPage in Enum.GetValues(typeof(Business.DefaultPages)))
					drpDefaultPage.Items.Add(new ListItem(Language.GetString(defaultPage.ToString()), ((int)defaultPage).ToString()));

				if (ActionType.ToLower() == "edit")
				{
					DataTable dtDomain = Facade.Domain.GetDomain(DomainGuid);

					txtName.Text = dtDomain.Rows[0]["Name"].ToString();
					drpStartUpPage.SelectedValue = Helper.GetString(dtDomain.Rows[0]["Desktop"]);
					drpDefaultPage.SelectedValue = Helper.GetString(dtDomain.Rows[0]["DefaultPage"]);
					txtOwner.Text = dtDomain.Rows[0]["UserName"].ToString();
				}
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.Domain domain = new Common.Domain();
			try
			{
				domain.Name = Helper.GetLocalDomain(txtName.Text);
				domain.Desktop = Helper.GetInt(drpStartUpPage.SelectedValue);
				domain.DefaultPage = Helper.GetInt(drpDefaultPage.SelectedValue);
				domain.Theme = 0;
				domain.CreateDate = DateTime.Now;

				DataTable dtUser = Facade.User.GetUser(txtOwner.Text);
				if (dtUser.Rows.Count == 0)
					throw new Exception("UserNotFound");

				domain.UserGuid = Helper.GetGuid(dtUser.Rows[0]["Guid"]);

				if (domain.HasError)
					throw new Exception(domain.ErrorMessage);

				switch (ActionType.ToLower())
				{
					case "insert":
						Facade.Domain.InsertDomain(domain);
						break;
					case "edit":
						domain.DomainGuid = DomainGuid;
						Facade.Domain.UpdateName(domain);
						break;
				}

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Domains_Domain, Session)));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Domains_Domain, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageDomain);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Domains_RegisterDomain;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Domains_RegisterDomain.ToString());
		}
	}
}