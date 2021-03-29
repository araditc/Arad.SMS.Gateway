using Arad.SMS.Gateway.GeneralLibrary;
using System;

namespace Arad.SMS.Gateway.Web.UI.Controls
{
	public partial class BootstrapDateTimePicker : System.Web.UI.UserControl
	{
		public string Date
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

		public string Time
		{
			get
			{
				if (Request[txtTime.ClientID.Replace("_", "$")] != null)
					return Request[txtTime.ClientID.Replace("_", "$")];
				else
					return txtTime.Text;
			}
			set
			{
				txtTime.Text = value;
			}
		}

		public string FullDateTime
		{
			get
			{
				if (Date != string.Empty)
					return string.Format("{0} {1}", Date, Time);
				else
					return Date;
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
			txtTime.Attributes["isRequired"] = this.IsRequired.ToString().ToLower();

			if (this.ValidationSet != string.Empty)
			{
				txtDate.Attributes["validationSet"] = this.ValidationSet;
				txtTime.Attributes["validationSet"] = this.ValidationSet;
			}

			base.OnInit(e);
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}