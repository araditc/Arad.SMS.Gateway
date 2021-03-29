using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.Web.HomePages.Arad
{
    public partial class Main : System.Web.UI.MasterPage
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

                
            }
            catch { }
        }

        protected void LanguageToPersian(object sender, EventArgs e)
        {
            Session["Language"] = Language.AvalibaleLanguages.fa;
        }

        protected void LanguageToEnglish(object sender, EventArgs e)
        {
            Session["Language"] = Language.AvalibaleLanguages.en;
        }
    }
}
