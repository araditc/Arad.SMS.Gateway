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
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Arad.SMS.Gateway.Web.HomePages.AdminPanel.Arad
{
	public partial class Index : System.Web.UI.Page
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private DateTime ExpireDate
		{
			get { return Helper.GetDateTime(Session["ExpireDate"]); }
		}

		public string DomainName
		{
			get { return Helper.GetDomain(Request.Url.Authority); }
		}

		protected string homePage;
		protected string credit;
		protected void Page_Load(object sender, EventArgs e)
        {
            if (UserGuid == Guid.Empty)
				Response.Redirect("~/Index.aspx");

			if (ExpireDate < DateTime.Now)
				Response.Redirect(string.Format("http://{0}/{1}", DomainName, Language.GetString("ExtendedPanelURL")));

			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			try
			{
				//homePage = string.Empty;//GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Home, Session);
				//iframe0.Attributes["src"] = string.Format("/PageLoader.aspx?c={0}", homePage);

				Common.User user = Facade.User.LoadUser(UserGuid);
				lblName.Text = string.Format("{0} {1}", user.FirstName, user.LastName);
				credit = Helper.FormatDecimalForDisplay(user.Credit);

				Guid domainGuid = Facade.Domain.GetDomainGuid(DomainName);
				DataTable dtSettings = Facade.DomainSetting.GetDomainSettings(domainGuid);

				foreach (DataRow row in dtSettings.Rows)
				{
					switch (Helper.GetInt(row["Key"]))
					{
						case (int)SiteSetting.Title:
							Page.Title = row["Value"].ToString();
							break;
						case (int)SiteSetting.Logo:
							imgLogo.ImageUrl = string.Format("~/Images/{0}/{1}", Helper.GetHostOfDomain(Request.Url.Host), row["Value"]);
							break;
						case (int)SiteSetting.Favicon:
							favicon.Href = string.Format("~/Images/{0}/{1}", Helper.GetHostOfDomain(Request.Url.Host), row["Value"]);
							break;
						case (int)SiteSetting.Description:
							description.Attributes["content"] = row["Value"].ToString();
							break;
						case (int)SiteSetting.Keywords:
							keywords.Attributes["content"] = row["Value"].ToString();
							break;
						case (int)SiteSetting.CompanyName:
							ltrCompanyName.Text = row["Value"].ToString();
							break;
					}
				}
			}
			catch { }
		}

        [WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static List<Common.Menu> GetShortcut()
		{
			return Facade.UserSetting.GetUserShortcut(Helper.GetGuid(HttpContext.Current.Session["UserGuid"]));
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static List<Common.Menu> GetMenu()
		{
            var lang = HttpContext.Current.Session["Language"].ToString();
            if (string.IsNullOrWhiteSpace(lang))
            {
                HttpContext.Current.Session["Language"] = Language.AvalibaleLanguages.fa;
            }
            //var menu = Facade.Service.GenerateUserServiceMenu(Helper.GetGuid(HttpContext.Current.Session["UserGuid"]));
            return Facade.Service.GenerateUserServiceMenu(Helper.GetGuid(HttpContext.Current.Session["UserGuid"]), lang);
		}

		protected void btnSignOut_Click(object sender, EventArgs e)
		{
			Common.LoginStat loginStat = new Common.LoginStat();
			try
			{
				loginStat.IP = Request.UserHostAddress;
				loginStat.Type = (int)Business.LoginStatsType.SignOut;
				loginStat.CreateDate = DateTime.Now;
				loginStat.UserGuid = Helper.GetGuid(Session["UserGuid"]);
				Facade.LoginStat.Insert(loginStat);
			}
			catch { }

            var lang = Session["Language"];

            Session.RemoveAll();

            Session["Language"] = lang;

            Response.Redirect("~/Index.aspx");
		}

        protected void Button1_Click(object sender, EventArgs e)
        {
            var lang = Session["Language"].ToString();

            if (lang.Equals("en"))
            {
                Session["Language"] = Language.AvalibaleLanguages.fa;
            }
            else
            {
                Session["Language"] = Language.AvalibaleLanguages.en;
            }
        }
    }
}
