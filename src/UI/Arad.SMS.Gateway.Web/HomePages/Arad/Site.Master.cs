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
using System.Data;
using System.Web.Security;
using System.Web.UI;

namespace Arad.SMS.Gateway.Web.HomePages.Arad
{
	public partial class Site : System.Web.UI.MasterPage
	{
		public string DomainName
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
			try
			{
                //Session["Language"] = Language.AvalibaleLanguages.en;
                //Language.ActiveLanguage = Language.AvalibaleLanguages.en;

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
						case (int)SiteSetting.Footer:
							ltrFooter.Text = row["Value"].ToString();
							break;
					}
				}

				var bottomContent = Facade.Domain.GetContent(DomainName, Business.DataLocation.BottomCenter, Business.Desktop.Default);
				foreach (DataRow row in bottomContent.Rows)
				{
					ltrAddress.Text += row["summary"].ToString();
					ltrAddress.Text += "<br/>";
				}
			}
			catch { }
		}
	}
}
