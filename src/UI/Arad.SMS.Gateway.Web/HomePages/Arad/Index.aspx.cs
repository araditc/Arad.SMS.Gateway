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
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Arad.SMS.Gateway.Web.HomePages.Arad
{
	public partial class Index : System.Web.UI.Page
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
            string domainName = "";
            try
            {
                domainName = Helper.GetDomain(HttpContext.Current.Request.Url.Authority);

                var lstImages = Facade.Domain.GetDomainSlideShow(DomainName);

                foreach (DataRow row in lstImages.Rows)
                {
                    ltrSlideShow.Text += string.Format(@"<div class='item' style='background-image:url(/Gallery/{1}/{2})'><div class='slide-mask'></div>
                                                        <div class='slide-body'><div class='container'>
                                                        <a href='{0}'></a></div></div></div>",
                        string.IsNullOrEmpty(row["DataTitle"].ToString())
                            ? "#"
                            : string.Format("/{0}/{1}", row["ID"], row["DataTitle"].ToString().Replace(" ", "-")),
                        Helper.GetHostOfDomain(Request.Url.Authority),
                        row["ImagePath"],
                        row["ImageTitle"]);
                }

                dtlContents.DataSource =
                    Facade.Domain.GetContent(domainName, Business.DataLocation.Center, Business.Desktop.Default);
                dtlContents.DataBind();
            }
            catch (Exception e)
            {
                var exp = e;
            }

            try
            {
                var bottomContent = Facade.Domain.GetContent(domainName, Business.DataLocation.BottomCenter,
                    Business.Desktop.Default);
                foreach (DataRow row in bottomContent.Rows)
                {
                    ltrNews.Text += string.Format(
                        @"<div class='col-sm-4'><div class='recent-post'><h4><a href='{0}'>{1}</a></h4><p>{2}</p>
                                                    <a href='{0}' class='btn btn-normal'>{3}</a></div></div>",
                        string.IsNullOrEmpty(row["Title"].ToString())
                            ? "#"
                            : string.Format("/{0}/{1}", row["ID"], row["Title"].ToString().Replace(" ", "-")),
                        row["Title"],
                        row["summary"],
                        GeneralLibrary.Language.GetString("ReadMore"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static List<Common.Menu> GetMenu(string pos)
		{
			string domainName = Helper.GetDomain(HttpContext.Current.Request.Url.Authority);
			return Facade.DataCenter.GetDomainMenu(Facade.Domain.GetDomainGuid(domainName), Business.DataLocation.TopRight, Business.Desktop.Default);
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static object[] GetSalePackages()
		{
			try
			{
                var lang = HttpContext.Current.Session["Language"].ToString();
                string domainName = Helper.GetDomain(HttpContext.Current.Request.Url.Authority);
				Dictionary<Guid, List<string>> dictionaryServices = new Dictionary<Guid, List<string>>();

				DataTable dtPackages = Facade.Domain.GetSalePackages(domainName, dictionaryServices, lang);

				object[] array = new object[dtPackages.Rows.Count];
				int counter = 0;
				foreach (DataRow row in dtPackages.Rows)
				{
                    array[counter] = new
                    {
                        id = row["ID"],
                        title = row["Title"].ToString(),
                        price = Helper.FormatDecimalForDisplay(row["Price"]),
                        panelprice = Helper.GetDecimal(row["Price"]),
                        services = dictionaryServices[Helper.GetGuid(row["Guid"])].Take(10).ToArray()
                    };
                    counter++;
				}
				return array;
			}
			catch
			{
				return new object[1];
			}
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static List<Common.OnlineAccount> GetOnlineAccount()
		{
            Common.OnlineAccount account;
			List<Common.OnlineAccount> lstAccount = new List<Common.OnlineAccount>();

			DataTable dtAccounts = Facade.AccountInformation.GetAccountsIsActiveOnline(Helper.GetGuid(HttpContext.Current.Session["ParentGuid"]));
			foreach (DataRow row in dtAccounts.Rows)
			{
				account = new Common.OnlineAccount();
				account.Guid = Helper.GetGuid(row["Guid"]);
				account.Owner = row["Owner"].ToString();
				account.Branch = row["Branch"].ToString();
				account.AccountNo = row["AccountNo"].ToString();
				account.Bank = Helper.GetInt(row["Bank"]);

				lstAccount.Add(account);
			}

			return lstAccount;
		}
	}
}
