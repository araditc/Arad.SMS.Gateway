using System;
using System.Collections.Generic;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using Business;
using System.Web.UI.WebControls;

namespace MessagingSystem.UI.Domains
{
	public partial class CreateNicAccount : UIUserControlBase
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
			radioDomainNicActualPerson.Checked = true;
			btnSaveActualPersonAccount.Text = GeneralLibrary.Language.GetString(btnSaveActualPersonAccount.Text);
			btnSaveActualPersonAccount.Attributes["onclick"] = "return validateRequiredFields('SaveActualPersonAccount');";

			btnSaveCivilPersonAccount.Text = GeneralLibrary.Language.GetString(btnSaveCivilPersonAccount.Text);
			btnSaveCivilPersonAccount.Attributes["onclick"] = "return checkValidateCivilPersonAccount();";

			drpNicCivilPersonType.Attributes["onchange"] = "changeCivilType();";
			foreach (NicCivilPersonType civilType in System.Enum.GetValues(typeof(NicCivilPersonType)))
				drpNicCivilPersonType.Items.Add(new ListItem(Language.GetString(civilType.ToString()), ((int)civilType).ToString()));
			drpNicCivilPersonType.SelectedIndex = (int)NicCivilPersonType.NonGovernment;

			foreach (NonGovernmentCompanyType companyType in System.Enum.GetValues(typeof(NonGovernmentCompanyType)))
				drpNonGovernmentType.Items.Add(new ListItem(Language.GetString(companyType.ToString()), ((int)companyType).ToString()));
			drpNonGovernmentType.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			foreach (GovernmentCompanyType companyType in System.Enum.GetValues(typeof(GovernmentCompanyType)))
				drpGovernmentType.Items.Add(new ListItem(Language.GetString(companyType.ToString()), ((int)companyType).ToString()));
			drpGovernmentType.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			foreach (ResearchCenterType companyType in System.Enum.GetValues(typeof(ResearchCenterType)))
				drpResearchCenterType.Items.Add(new ListItem(Language.GetString(companyType.ToString()), ((int)companyType).ToString()));
			drpResearchCenterType.Items.Insert(0, new ListItem(string.Empty, string.Empty));
		}

		protected void btnSaveActualPersonAccount_Click(object sender, EventArgs e)
		{
			try
			{
				Common.DomainAccount domainAccount = new Common.DomainAccount();
				domainAccount.Type = (int)Business.DomainAccountType.NicAccount;
				domainAccount.FirstName = txtFirstName.Text;
				domainAccount.LastName = txtLastName.Text;
				domainAccount.NationalCode = txtNationalID.Text;
				domainAccount.CompanyName = txtCompanyName.Text;
				domainAccount.Address = txtAddress.Text;
				domainAccount.City = txtCity.Text;
				domainAccount.Province = txtProvince.Text;
				domainAccount.Country = txtCountry.Text;
				domainAccount.PostalCode = txtPostalCode.Text;
				domainAccount.Telephone = txtPhoneNumber.Text;
				domainAccount.FaxNumber = txtFaxNo.Text;
				domainAccount.Email = txtEmail.Text;
				domainAccount.NICType = (int)Business.NicDomainType.ActualPerson;
				domainAccount.CreateDate = DateTime.Now;
				domainAccount.UserGuid = UserGuid;

				if (domainAccount.HasError)
					throw new Exception(domainAccount.ErrorMessage);

				if (Facade.DomainAccount.InsertNicAccountActualPerson(domainAccount))
					ShowMessageBox(Language.GetString("InsertRecord"));
				else
					throw new Exception(Language.GetString("ErrorRecord"));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}

		protected void btnSaveCivilPersonAccount_Click(object sender, EventArgs e)
		{
			try
			{
				Common.DomainAccount domainAccount = new Common.DomainAccount();
				domainAccount.Type = (int)Business.DomainAccountType.NicAccount;
				domainAccount.FirstName = txtFirstNameCivil.Text;
				domainAccount.LastName = txtLastNameCivil.Text;
				domainAccount.CompanyName = txtCompanyNameCivil.Text;
				domainAccount.Address = txtAddressCivil.Text;
				domainAccount.City = txtCityCivil.Text;
				domainAccount.Province = txtProvinceCivil.Text;
				domainAccount.Country = txtCountryCivil.Text;
				domainAccount.PostalCode = txtPostalCodeCivil.Text;
				domainAccount.Telephone = txtTelephoneCivil.Text;
				domainAccount.FaxNumber = txtFaxNumber.Text;
				domainAccount.Email = txtEmailCivil.Text;
				domainAccount.NICType = (int)Business.NicDomainType.CivilPerson;
				domainAccount.CivilType = Helper.GetInt(drpNicCivilPersonType.SelectedValue);
				domainAccount.CreateDate = DateTime.Now;
				domainAccount.UserGuid = UserGuid;

				switch (Helper.GetInt(drpNicCivilPersonType.SelectedValue))
				{
					case (int)Business.NicCivilPersonType.NonGovernment:
						domainAccount.CountryOfCompany = txtNonGovernmentRegisteredCountry.Text;
						domainAccount.CityOfCompany = txtNonGovernmentRegisteredCity.Text;
						domainAccount.RegisteredCompanyName = txtNonGovernmentNameOfRegisteredUnit.Text;
						domainAccount.CompanyID = txtNonGovernmentCompanyCode.Text;
						domainAccount.NationalCode = txtNonGovernmentCompanyNationalCode.Text;
						domainAccount.CompanyType = Helper.GetInt(drpNonGovernmentType.SelectedValue);
						break;
					case (int)Business.NicCivilPersonType.Government:
						domainAccount.CountryOfCompany = txtGovernmentCountryName.Text;
						domainAccount.ProvinceOfCompany = txtGovernmentProvinceName.Text;
						domainAccount.CityOfCompany = txtGovernmentCityName.Text;
						domainAccount.CompanyType = Helper.GetInt(drpGovernmentType.SelectedValue);
						break;
					case (int)Business.NicCivilPersonType.ResearchCenter:
						domainAccount.CountryOfCompany = txtResearchCenterCountryName.Text;
						domainAccount.ProvinceOfCompany = txtResearchCenterProvinceName.Text;
						domainAccount.CityOfCompany = txtResearchCenterCityName.Text;
						domainAccount.CompanyType = Helper.GetInt(drpResearchCenterType.SelectedValue);
						break;
				}

				if (domainAccount.HasError)
					throw new Exception(domainAccount.ErrorMessage);

				if (Facade.DomainAccount.InsertNicAccountCivilPerson(domainAccount))
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
			permissions.Add((int)Business.Services.CreateNicAccount);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Business.UserControls.UI_Domains_CreateNicAccount;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Domains_CreateNicAccount.ToString());
		}

	}
}