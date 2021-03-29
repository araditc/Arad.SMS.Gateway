using Arad.SMS.Gateway.GeneralLibrary;
using System;

namespace Arad.SMS.Gateway.Web.UI.Controls
{
	public partial class DatePickerControl : System.Web.UI.UserControl
	{
		public string Value
		{
			get
			{
				if (Request[txtDate.ClientID.Replace("_", "$")] != null)
					return Request[txtDate.ClientID.Replace("_", "$")];
				else
					return txtDate.Text;
			}
			set
			{
				txtDate.Text = value;
			}
		}

		public string FullDateTime
		{
			get
			{
				if (Value != string.Empty)
					return string.Format("{0} {1}:{2}", Value, DateTime.Now.Hour, DateTime.Now.Minute);
				else
					return DateManager.GetSolarDateTime(DateTime.Now.Date);
			}
		}

		public bool IsRequired
		{
			set
			{
				ViewState["IsRequired"] = value;
			}
			get
			{
				return Helper.GetBool(ViewState["IsRequired"]);
			}
		}

		public string ValidationSet
		{
			set
			{
				ViewState["ValidationSet"] = value;
			}
			get
			{
				return Helper.GetString(ViewState["ValidationSet"]);
			}
		}

		protected override void OnInit(EventArgs e)
		{
			txtDate.Attributes["isRequired"] = this.IsRequired.ToString().ToLower();
			if (this.ValidationSet != string.Empty)
				txtDate.Attributes["validationSet"] = this.ValidationSet;
			base.OnInit(e);
		}

		protected void Page_Load(object sender, EventArgs e) { }
	}
}
