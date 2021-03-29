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

using Arad.SMS.Gateway.GeneralLibrary;
using System;

namespace Arad.SMS.Gateway.Web.HomePages.Arad
{
	public partial class Login : System.Web.UI.Page
	{
		protected string ChallengeString
		{
			get
			{
				return Helper.GetString(Session["ChallengeString"]);
			}
			set
			{
				Session["ChallengeString"] = value;
			}
		}

		public string DomainName
		{
			get { return Helper.GetDomain(Request.Url.Authority); }
		}

		private void RegenerateChallengeString()
		{
			ChallengeString = Helper.GetMd5Hash(Helper.RandomString()).ToLower();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
            {
                RegenerateChallengeString();
                InitializePage();
            }
        }

        private void InitializePage()
        {
            btnLogin.Text = Language.GetString("Login");
        }
        private void LoginUser(Common.User user, string domainName)
		{
			try
			{
				#region SetUserSetting
				Business.Desktop desktop;
				Business.Theme theme;
				Business.DefaultPages defaultPage;
				Facade.Domain.GetDomainInfo(domainName, out desktop, out defaultPage, out theme);
				Session["DeskTop"] = (int)desktop;
				#endregion

				#region SaveLoginInfo
				Common.LoginStat loginStat = new Common.LoginStat();
				loginStat.IP = Request.UserHostAddress;
				loginStat.Type = (int)Business.LoginStatsType.SignIn;
				loginStat.CreateDate = DateTime.Now;
				loginStat.UserGuid = user.UserGuid;
				Facade.LoginStat.Insert(loginStat);
				#endregion

				Session["UserGuid"] = user.UserGuid;
				Session["IsAdmin"] = user.IsAdmin;
				Session["IsSuperAdmin"] = user.IsSuperAdmin;
				Session["IsMainAdmin"] = user.IsMainAdmin;
				Session["ParentGuid"] = user.ParentGuid;
				Session["ExpireDate"] = user.ExpireDate;
				Session["IsAuthenticated"] = user.IsAuthenticated;
				Session.Remove("SessionExpired");
				Session.Remove("ChallengeString");

				if (user.ExpireDate < DateTime.Now)
					Response.Redirect(string.Format("http://{0}/{1}", domainName, Language.GetString("ExtendedPanelURL")));

				Facade.User.SendSmsForLogin(user.UserGuid);

				Response.Redirect(string.Format("http://{0}/{1}", domainName, desktop.ToString().ToLower()));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected void btnLogin_Click(object sender, EventArgs e)
		{
			try
			{
				Common.User user = new Common.User();
				bool isHuman = SampleCaptcha.Validate(CaptchaCodeTextBox.Text);
				CaptchaCodeTextBox.Text = null;

				if (!isHuman)
					throw new Exception(Language.GetString("IncorrectCaptcha"));

				string challengeString = Helper.GetString(Session["ChallengeString"]);

                user.Password = Helper.GetMd5Hash(challengeString + Helper.GetMd5Hash(txtPassword.Text));

                user.UserName = txtUsername.Text;
				//user.Password = txtUserPassword.Text;

				if (user.HasError)
					throw new Exception(user.ErrorMessage);

				user.DomainGuid = Facade.Domain.GetDomainGuid(DomainName);
				bool isLoginValid = Facade.User.LoginUser(user, challengeString, false);

				if (isLoginValid && user.IsActive)
					LoginUser(user, DomainName);
				else if (!isLoginValid)
					throw new Exception(Language.GetString("InvalidUserOrPassword"));
				else if (!user.IsActive)
					throw new Exception(Language.GetString("UserIsNotActive"));
				else
				{
					RegenerateChallengeString();
					throw new Exception(Language.GetString("UserNotFound"));
				}
			}
			catch (Exception ex)
			{
				lblMessage.Text = ex.Message;
			}
		}
	}
}
