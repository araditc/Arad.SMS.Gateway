using System;
using System.Web;
using System.Web.Security;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.Login
{
	public partial class Login : UIUserControlBase
	{
		protected int LoginTriesCount
		{
			get
			{
				if (ViewState["LoginTriesCount"] == null)
					ViewState["LoginTriesCount"] = 0;

				return Helper.GetInt(ViewState["LoginTriesCount"]);
			}
			set
			{
				ViewState["LoginTriesCount"] = value;
			}
		}

		protected bool LoginTriesExceeded
		{
			get
			{
				if (LoginTriesCount >= 5)
					return true;
				else
					return false;
			}
		}

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

		private void RegenerateChallengeString()
		{
			ChallengeString = Helper.GetMd5Hash(Helper.RandomString()).ToLower();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Request.Cookies["UserName"] != null)
				{
					//if (!Helper.GetBool(Session["SessionExpired"]))
					//{
					//  Common.User user = new Common.User();
					//  user.UserName = Request.Cookies["UserName"].Value;

					//  if (!user.HasError && Facade.User.LoginUser(user, true))
					//    ConfirmUser(user, true);
					//}
					//else
					//{
					txtUserName.Text = Request.Cookies["UserName"].Value;
					//}
				}
				InitializePage();
			}
		}

		private void InitializePage()
		{
			btnLogin.Text = Language.GetString(btnLogin.Text);
			//chkRemaindMe.Text = Language.GetString(chkRemaindMe.Text);

			RegenerateChallengeString();
			//if (Helper.GetBool(Session["SessionExpired"]))
			//  MessageBoxText = Language.GetString("SessionExpiredNeedToLoginAgain");

			btnLogin.Attributes["onclick"] = "return validateSubmit();";
		}

		protected void btnLogin_Click(object sender, EventArgs e)
		{
			//Common.User user = new Common.User();
			//user.UserName = txtUserName.Text;
			//user.Password = hdnAuthenticationString.Value;

			if (Helper.GetBool(hdnActiveJavaScript.Value) == false)
				Response.Redirect(string.Format("~/ErrorHandler.aspx?ErrorType={0}", (int)ErrorType.NotEnableJavaScript));

			//if (!user.HasError)
			//{
			//  if (Facade.User.LoginUser(user, ChallengeString, false))
			//    ConfirmUser(user, false);
			//  else
			//  {
			//    lblError.Text = Language.GetString("UserNotFound");
			//    //Session.Remove("ChallengeString");
			//    RegenerateChallengeString();
			//    LoginTriesCount += 1;
			//  }
			//}
			//else
			//  lblError.Text = user.ErrorMessage;
		}

		private void ConfirmUser(Common.User user, bool autoLogin)
		{
			if (user.IsActive == true)
			{
				if (chkRemaindMe.Checked)
				{
					HttpCookie cooky = new HttpCookie("UserName", user.UserName);
					cooky.Expires = DateTime.Now.AddYears(1);
					Response.Cookies.Add(cooky);
				}
				else
				{
					Response.Cookies.Remove("UserName");
				}

				Session["UserGuid"] = user.UserGuid;
				Session["IsAdmin"] = user.IsAdmin;
				Session["IsSuperAdmin"] = user.IsSuperAdmin;
				Session["IsMainAdmin"] = user.IsMainAdmin;
				Session["ParentGuid"] = user.ParentGuid;
				Session["ExpireDate"] = user.ExpireDate;
				Session.Remove("SessionExpired");
				Session.Remove("ChallengeString");

				CheckDomain();
			}
			else if (!autoLogin)
				lblError.Text = Language.GetString("UserIsNotActive");
		}

		private void CheckDomain()
		{
			string domainName = Helper.GetDomain(Request.Url.Authority);
			Business.Desktop desktop;
			Business.Theme theme;
			Business.DefaultPages defaultPage;

			Facade.Domain.GetDomainInfo(domainName, out desktop, out defaultPage, out theme);

			Session["DeskTop"] = (int)desktop;
			Session["Theme"] = (int)theme;

			ClientSideScript = "relaodMainPage();";
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			return new List<int>();
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Login_Login;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Login_Login.ToString());
		}
	}
}
