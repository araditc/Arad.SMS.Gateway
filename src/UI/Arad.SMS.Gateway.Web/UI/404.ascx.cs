using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Web.UI
{
	public partial class _404 : UIUserControlBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			return new List<int>();
		}

		protected override int GetUserControlID()
		{
			return 0;
		}

		protected override string GetUserControlTitle()
		{
			return string.Empty;
		}
	}
}