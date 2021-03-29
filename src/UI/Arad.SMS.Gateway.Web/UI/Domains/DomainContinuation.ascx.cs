using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using Business;

namespace MessagingSystem.UI.Domains
{
	public partial class DomainContinuation : UIUserControlBase
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
			btnDomainContinuation.Text = GeneralLibrary.Language.GetString("CreateAccount");
			foreach (DomainExtention typeExtention in System.Enum.GetValues(typeof(DomainExtention)))
				drpExtention.Items.Add(new ListItem(Language.GetString(typeExtention.ToString().Replace('_', '.')), ((int)typeExtention).ToString()));
			drpPeriod.Items.Add(new ListItem("1", "1"));
			drpPeriod.Items.Add(new ListItem("5", "5"));
		}

		protected void drpExtention_SelectedIndexChanged(object sender, EventArgs e)
		{
			drpPeriod.Items.Clear();
			if (Language.GetString(((DomainExtention)Helper.GetInt(drpExtention.SelectedValue)).ToString()).Contains("ir"))
			{
				for (int i = 1; i <= 10; i++)
					drpPeriod.Items.Add(new ListItem(i.ToString(), i.ToString()));
			}
			else
			{
				drpPeriod.Items.Add(new ListItem("1", "1"));
				drpPeriod.Items.Add(new ListItem("5", "5"));
			}
		}

		protected void btnDomainContinuation_Click(object sender, EventArgs e)
		{
			//RegisterDomainWebService.RegisterDomain DomainWebService = new RegisterDomainWebService.RegisterDomain();
			//string ext = drpExtention.SelectedItem.ToString();
			//byte period = Convert.ToByte(drpPeriod.SelectedValue);
			//DomainWebService.TamdidDirectDomain(txtDomain.Text, ext, period);
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Business.Services.ManageDomain);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Business.UserControls.UI_Domains_DomainContinuation;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Domains_DomainContinuation.ToString());
		}
	}
}