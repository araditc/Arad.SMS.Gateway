// --------------------------------------------------------------------
// Copyright (c) 2005-2020 Arad ITC.
//
// Author : Ammar Heidari <ammar@arad-itc.org>
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------

using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using System;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.HomePages.Arad
{
	public partial class Register : System.Web.UI.Page
	{
		private string DomainName
		{
			get { return Helper.GetDomain(Request.Url.Authority); }
		}

		private int PackageId
		{
			get { return Helper.GetInt(Helper.Request(this, "pid")); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
            btnRegister.Text = Language.GetString("CreateAccount");
            foreach (UserType type in System.Enum.GetValues(typeof(UserType)))
				drpType.Items.Add(new ListItem(Language.GetString(type.ToString()), ((int)type).ToString()));
		}

		protected void btnRegister_Click(object sender, EventArgs e)
		{
			Common.User user = new Common.User();
			try
			{
				bool isHuman = SampleCaptcha.Validate(CaptchaCodeTextBox.Text);
				CaptchaCodeTextBox.Text = null;

				if (!isHuman)
					throw new Exception(Language.GetString("IncorrectCaptcha"));

				if (txtPassword.Text != txtRepeatPassword.Text ||
						string.IsNullOrEmpty(txtPassword.Text) ||
						string.IsNullOrEmpty(txtRepeatPassword.Text))
					throw new Exception(Language.GetString("PasswordNotMatchWithConfirmPassword"));

				if (!Facade.User.CheckUserNameValid(txtUsername.Text, Guid.Empty))
					throw new Exception(Language.GetString("UserNameIsDuplicate"));

				CheckDataConditionsResult emailValidationResult = Helper.CheckDataConditions(txtEmail.Text);
				if (emailValidationResult.IsEmpty)
					throw new Exception(Language.GetString("CompleteEmailField"));
				if (!emailValidationResult.IsEmail)
					throw new Exception(Language.GetString("EmailFieldIsNotCorectFormat"));

				if (Helper.IsCellPhone(txtMobile.Text) == 0)
					throw new Exception(Language.GetString("InvalidMobile"));

				user.Type = Helper.GetInt(drpType.SelectedValue);
				user.UserName = txtUsername.Text;
				user.Password = txtPassword.Text;
				user.Email = txtEmail.Text;
				user.Mobile = Helper.GetLocalMobileNumber(txtMobile.Text);
				user.ParentGuid = Facade.User.GetGuidOfParent(Guid.Empty, DomainName);
				user.RoleGuid = Facade.Role.GetDefaultRoleGuid(DomainName);
				user.PriceGroupGuid = Facade.GroupPrice.GetDefaultGroupPrice(DomainName);
				user.CreateDate = DateTime.Now;
				user.ExpireDate = DateTime.Now.AddYears(1);
				user.IsFixPriceGroup = false;
				user.IsActive = true;
				user.IsAdmin = true;
				user.MaximumAdmin = 0;
				user.MaximumUser = 0;
				user.PanelPrice = 0;
				user.DomainGuid = Facade.Domain.GetDomainGuid(DomainName);

				Guid userGuid = Facade.User.RegisterUser(user);
				if (userGuid == Guid.Empty)
					throw new Exception(Language.GetString("ErrorRecord"));

				Session["UserGuid"] = userGuid;
				Session["ParentGuid"] = user.ParentGuid;

				if (PackageId == 0)
				{
					lblMessage.Attributes["style"] = "color:green";
					lblMessage.Text = Language.GetString("SuccessRegisterUser");
				}
				else
					Response.Redirect(string.Format("http://{0}/package/{1}/{2}", DomainName, PackageId, Language.GetString("BuyPanelURL")));
			}
			catch (Exception ex)
			{
				lblMessage.Text = ex.Message;
			}
		}
	}
}
