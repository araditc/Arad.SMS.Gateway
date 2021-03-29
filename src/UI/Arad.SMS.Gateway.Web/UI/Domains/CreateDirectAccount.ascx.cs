using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace MessagingSystem.UI.Domains
{
	public partial class CreateDirectAccount : UIUserControlBase
	{
		private Guid UserGuid
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
			btnSave.Text = GeneralLibrary.Language.GetString(btnSave.Text);
			btnSave.Attributes["onclick"] = "return validateRequiredFields('SaveDirectDomain')";
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				Common.DomainAccount domainAccount = new Common.DomainAccount();
				domainAccount.Type = (int)Business.DomainAccountType.DirectAccount;
				domainAccount.UserName = txtuserName.Text;
				domainAccount.Password = txtPassword.Text;
				domainAccount.FirstName = txtFirstName.Text;
				domainAccount.CompanyName = txtCompanyName.Text;
				domainAccount.Address = txtAddress.Text;
				domainAccount.City = txtCity.Text;
				domainAccount.Province = txtProvince.Text;
				domainAccount.Country = txtCountry.Text;
				domainAccount.PostalCode = txtPostalCode.Text;
				domainAccount.Telephone = txtPhoneNumber.Text;
				domainAccount.CreateDate = DateTime.Now;
				domainAccount.UserGuid = UserGuid;
				if (domainAccount.HasError)
					throw new Exception(domainAccount.ErrorMessage);

				if (Facade.DomainAccount.InsertDirectAccount(domainAccount))
					ShowMessageBox(Language.GetString("InsertRecord"));
				else
					throw new Exception(Language.GetString("ErrorRecord"));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Business.Services.CreateDirectAccount);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Business.UserControls.UI_Domains_CreateDirectAccount;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Domains_CreateDirectAccount.ToString());
		}

	}
}