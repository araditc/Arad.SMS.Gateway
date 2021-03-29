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

using System;
using System.Web.UI;
using Arad.SMS.Gateway.GeneralLibrary;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web
{
	public partial class PageLoader : System.Web.UI.Page
	{
		protected string InlineScript = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			Business.UserControls userControl = Business.UserControls.UI_404;
			//Business.HelpUserControls helpUserControl = Business.HelpUserControls.UI_Help_Home;

			CheckDomain();

			if (Request.Params["h"] != null)
			{
				try
				{
					HtmlTextWriter htmlContent = null;
					Server.Execute("~/UI/AradITCInfo/" + Request.Params["h"], htmlContent);
					Literal ltrHtmlContent = new Literal();
					ltrHtmlContent.Text = htmlContent.ToString();
					mainPanel.Controls.Add(ltrHtmlContent);
				}
				catch
				{
					if (Session["SessionExpired"] == null)
					{
						Session["SessionExpired"] = true;
						InlineScript = "relaodMainPage();";
					}
				}
				return;
			}

			if (Request.Params["c"] != null)
			{
				if (Helper.RequestEncryptedInt(this, "c") > 0)
				{
					int controlID = Helper.RequestEncryptedInt(this, "c");

					try
					{
						if (controlID == 0)
							throw new Exception();

						userControl = (Business.UserControls)controlID;
					}
					catch
					{
						if (Session["SessionExpired"] == null)
						{
							Session["SessionExpired"] = true;
							InlineScript = "relaodMainPage();";
						}
					}
				}
			}
			else
				userControl = Business.UserControls.UI_404;

			//if (Request.Params["p"] != null)
			//{
			//	if (Helper.RequestEncryptedInt(this, "p") > 0)
			//	{
			//		int controlID = Helper.RequestEncryptedInt(this, "p");

			//		try
			//		{
			//			if (controlID == 0)
			//				throw new Exception();

			//			helpUserControl = (Business.HelpUserControls)controlID;
			//		}
			//		catch
			//		{
			//			if (Session["SessionExpired"] == null)
			//			{
			//				Session["SessionExpired"] = true;
			//				InlineScript = "relaodMainPage();";
			//			}
			//		}
			//	}
			//}
			//else
			//	helpUserControl = Business.HelpUserControls.UI_Help_Home;

			//try
			//{
			if (InlineScript == string.Empty && Request.Params["c"] != null)
			{
				Control control = LoadControl(userControl);
				mainPanel.Controls.Add(control);
			}
			//}
			//catch
			//{
			//userControl = Business.UserControls.UI_Home;
			//Control control = LoadControl(userControl);
			//mainPanel.Controls.Add(control);
			//}

			//try
			//{
			//	if (InlineScript == string.Empty && Request.Params["p"] != null)
			//	{
			//		Control control = LoadHelpControl(helpUserControl);
			//		mainPanel.Controls.Add(control);
			//	}
			//}
			//catch
			//{
			//	helpUserControl = Business.HelpUserControls.UI_Help_UnderConstruction;
			//	Control control = LoadHelpControl(helpUserControl);
			//	mainPanel.Controls.Add(control);
			//}
		}

		private Control LoadControl(Business.UserControls userControl)
		{
			Page.Title = Language.GetString(userControl.ToString());
			string path = "~/" + userControl.ToString().Replace("_", "/") + ".ascx";
			return LoadControl(path);
		}

		//private Control LoadHelpControl(Business.HelpUserControls helpUserControl)
		//{
		//	Page.Title = Language.GetString(helpUserControl.ToString());
		//	string path = "~/" + helpUserControl.ToString().Replace("_", "/") + ".ascx";
		//	return LoadControl(path);
		//}

		private void CheckDomain()
		{
			if (Session["DeskTop"] == null)
			{
				string domainName = Helper.GetDomain(Request.Url.Authority);
				Business.Desktop desktop;
				Business.DefaultPages defaultPage;
				Business.Theme theme;

				Facade.Domain.GetDomainInfo(domainName, out desktop, out defaultPage, out theme);

				Session["DeskTop"] = (int)desktop;
				Session["Theme"] = (int)theme;
			}

			//string domainTheme = ((Business.Theme)Helper.GetInt(Session["Theme"])).ToString();
			//ltrTheme.Text = string.Format("<link href='Themes/{0}/{0}.css' rel='stylesheet' type='text/css' />", domainTheme);
		}
	}
}
