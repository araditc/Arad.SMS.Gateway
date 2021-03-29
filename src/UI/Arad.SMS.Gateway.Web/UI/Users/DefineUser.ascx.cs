using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.Users
{
	public partial class DefineUser : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid ParentGuid
		{
			get { return Helper.GetGuid(Session["ParentGuid"]); }
		}

		private string DomainName
		{
			get { return Helper.GetDomain(Request.Url.Authority); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			foreach (UserDocumentType doc in Enum.GetValues(typeof(UserDocumentType)))
			{
				if (doc != UserDocumentType.NationalCard &&
						doc != UserDocumentType.BirthCertificate &&
						doc != UserDocumentType.BusinessLicense)
					drpCompanyDocumentType.Items.Add(new ListItem(Language.GetString(doc.ToString()), ((int)doc).ToString()));
			}

			drpPersonalDocumentType.Items.Add(new ListItem(Language.GetString(UserDocumentType.NationalCard.ToString()), ((int)UserDocumentType.NationalCard).ToString()));
			drpPersonalDocumentType.Items.Add(new ListItem(Language.GetString(UserDocumentType.BirthCertificate.ToString()), ((int)UserDocumentType.BirthCertificate).ToString()));
			drpPersonalDocumentType.Items.Add(new ListItem(Language.GetString(UserDocumentType.BusinessLicense.ToString()), ((int)UserDocumentType.BusinessLicense).ToString()));

			#region Roles
			//Guid parentGuid = ParentGuid == Guid.Empty ? UserGuid : ParentGuid;
			drpRole.DataSource = Facade.Role.GetRoles(UserGuid);
			drpRole.DataTextField = "Title";
			drpRole.DataValueField = "Guid";
			drpRole.DataBind();
			drpRole.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			#endregion

			#region GroupPrice
			//Guid userGuid = ParentGuid != Guid.Empty ? UserGuid : UserGuid;
			drpGroupPrice.DataSource = Facade.GroupPrice.GetPagedGroupPrices(UserGuid);
			drpGroupPrice.DataTextField = "Title";
			drpGroupPrice.DataValueField = "Guid";
			drpGroupPrice.DataBind();
			drpGroupPrice.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			#endregion

			#region Country
			var lstCountry = Facade.Zone.GetZones(Guid.Empty);
			drpCountry.DataSource = lstCountry;
			drpCountry.DataTextField = "Name";
			drpCountry.DataValueField = "Guid";
			drpCountry.DataBind();

			//if (lstCountry.Rows.Count > 0)
			//	drpCountry.SelectedValue = lstCountry.Rows.OfType<DataRow>().Where(r => r.Field<string>("OCode") == "IR").First().Field<Guid>("Guid").ToString();
			#endregion

			#region Province
			Guid countryGuid = Helper.GetGuid(drpCountry.SelectedValue);
			drpProvince.DataSource = Facade.Zone.GetZones(countryGuid);
			drpProvince.DataTextField = "Name";
			drpProvince.DataValueField = "Guid";
			drpProvince.DataBind();
			drpProvince.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			#endregion

			btnSaveProfile.Text = Language.GetString(btnSaveProfile.Text);
			btnSavePanelInfo.Text = Language.GetString(btnSavePanelInfo.Text);
			btnSavePanelInfo.Attributes["onclick"] = "return validationAccount();";
			btnSaveProfile.Attributes["onclick"] = "return validationProfile();";
			//dtpExpireDate.Value = DateManager.GetSolarDate(DateTime.Now);
            if (Session["Language"].ToString() == "fa")
            {
                dtpExpireDate.Value = DateManager.GetSolarDate(DateTime.Now);
            }
            else
            {
                dtpExpireDate.Value = DateTime.Now.ToShortDateString();
            }
        }

		protected void btnSavePanelInfo_Click(object sender, EventArgs e)
		{
			Common.User user = new Common.User();

			try
			{
				if (!Facade.User.CheckUserNameValid(txtUserName.Text, Guid.Empty))
					throw new Exception(Language.GetString("UserNameIsDuplicate"));

				user.UserName = txtUserName.Text;
				hdnUserName.Value = user.UserName;
				hdnPassword.Value = Helper.Encrypt(txtPassword.Text);
				user.Password = txtUserPassword.Text;
				user.ParentGuid = Facade.User.GetGuidOfParent(UserGuid, DomainName);
				user.RoleGuid = Helper.GetGuid(drpRole.SelectedValue);
				user.PriceGroupGuid = Helper.GetGuid(drpGroupPrice.SelectedValue);
				user.IsFixPriceGroup = chbIsFixGroupPrice.Checked;
				user.IsActive = chbIsActive.Checked;
                if (Session["Language"].ToString() == "fa")
                {
                    user.ExpireDate = DateManager.GetChristianDateForDB(dtpExpireDate.Value);
                }else
                {
                    user.ExpireDate = Convert.ToDateTime(dtpExpireDate.Value);
                }
				user.IsAdmin = true;
				user.MaximumAdmin = Helper.GetInt(txtMaximumAdmin.Text);
				user.MaximumUser = Helper.GetInt(txtMaximumUser.Text);
				user.MaximumEmailAddress = -1;
				user.MaximumPhoneNumber = -1;
				user.PanelPrice = Helper.GetDecimal(txtPanelPrice.Text);
				user.DomainGuid = Facade.Domain.GetDomainGuid(DomainName);

				Guid createdUserGuid = Facade.User.InsertAccount(user);
				if (createdUserGuid == Guid.Empty)
					throw new Exception(Language.GetString("ErrorRecord"));

				hdnCreatedUserGuid.Value = createdUserGuid.ToString();
				btnSavePanelInfo.Enabled = false;
				ClientSideScript = string.Format("result('step1','','OK','{0}');", Language.GetString("InsertRecord"));
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("result('step1','','Error','{0}');", ex.Message);
			}
		}

		protected void btnSaveProfile_Click(object sender, EventArgs e)
		{
			Common.User user = new Common.User();
			try
			{
				user.UserGuid = Helper.GetGuid(hdnCreatedUserGuid.Value);
				user.FirstName = txtFirstName.Text;
				user.LastName = txtLastName.Text;
				user.FatherName = txtFatherName.Text;
				user.NationalCode = txtNationalCode.Text;
				user.ShCode = txtShCode.Text;
				user.BirthDate = DateManager.GetChristianDateForDB(dtpBirthDate.Value);
				user.Email = txtEmail.Text;
				user.Mobile = Helper.GetLocalMobileNumber(txtMobile.Text);
				user.Phone = txtPhone.Text;
				user.FaxNumber = txtFax.Text;
				user.ZoneGuid = Helper.GetGuid(drpCity.SelectedValue);
				user.ZipCode = txtZipCode.Text;
				user.Address = txtAddress.Text;
				user.Type = Helper.GetInt(hdnUserType.Value);
				user.CompanyName = txtCompanyName.Text;
				user.CompanyNationalId = txtCompanyNationalID.Text;
				user.EconomicCode = txtEconomicCode.Text;
				user.CompanyCEOName = txtCompanyCEOName.Text;
				user.CompanyCEONationalCode = txtCompanyCEONationalCode.Text;
				user.CompanyCEOMobile = Helper.GetLocalMobileNumber(txtCompanyCEOMobile.Text);
				user.CompanyPhone = txtCompanyPhone.Text;
				user.CompanyZipCode = txtCompanyZipCode.Text;
				user.CompanyAddress = txtCompanyAddress.Text;

				if (Helper.IsCellPhone(user.Mobile) == 0)
					throw new Exception(Language.GetString("InvalidMobile"));

				if (!Facade.User.UpdateProfile(user))
					throw new Exception(Language.GetString("ErrorRecord"));

				Facade.User.SendUserAccountInfo(Helper.Decrypt(hdnPassword.Value), DomainName, user.UserGuid);

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)UserControls.UI_Users_User, Session)));
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("result('step2','{0}','Error','{1}');", Helper.GetInt(hdnUserType.Value) == 1 ? "actual" : "legal", ex.Message);
			}
		}

		protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
		{
			drpCity.Items.Clear();

			Guid countryGuid = Helper.GetGuid(drpCountry.SelectedValue);
			drpProvince.DataSource = Facade.Zone.GetZones(countryGuid);
			drpProvince.DataTextField = "Name";
			drpProvince.DataValueField = "Guid";
			drpProvince.DataBind();
			drpProvince.Items.Insert(0, new ListItem(string.Empty, string.Empty));
		}

		protected void drpProvince_SelectedIndexChanged(object sender, EventArgs e)
		{
			Guid provinceGuid = Helper.GetGuid(drpProvince.SelectedValue);
			if (provinceGuid != Guid.Empty)
			{
				drpCity.DataSource = Facade.Zone.GetZones(provinceGuid);
				drpCity.DataTextField = "Name";
				drpCity.DataValueField = "Guid";
				drpCity.DataBind();
				drpCity.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.DefineUser);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_Users_DefineUser;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_Users_DefineUser.ToString());
		}
	}
}
