using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace MessagingSystem.UI.Domains
{
	public partial class TransportNicDomain : UIUserControlBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			btnTransportDomain.Text = GeneralLibrary.Language.GetString("TransportDomain");
			btnGetTransportCode.Text = GeneralLibrary.Language.GetString("GetTransportCode");
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Business.Services.ManageDomain);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Business.UserControls.UI_Domains_TransportNicDomain;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Domains_TransportNicDomain.ToString());
		}
	}
}