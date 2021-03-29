using System;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.Web.UI.Widgets.Tariff
{
	public partial class Tariff : System.Web.UI.UserControl
	{
		public static string SmsRates;
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			//SmsRates = Facade.UserSmsRate.GetSmsRatesToShowInHeader(UserGuid);
		}
	}
}