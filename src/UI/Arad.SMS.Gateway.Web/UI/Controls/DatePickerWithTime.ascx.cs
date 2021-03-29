using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.Web.UI.Controls
{
	public partial class DatePickerWithTime : System.Web.UI.UserControl
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
				txtShowDate.Text = value;
			}
		}

		public string Hour
		{
			get
			{
				return drpHour.SelectedValue.ToString();
			}
			set
			{
				drpHour.SelectedValue = Helper.GetInt(value).ToString("00");
			}
		}

		public string Minute
		{
			get
			{
				return drpMinute.SelectedValue.ToString();
			}
			set
			{
				drpMinute.SelectedValue = Helper.GetInt(value).ToString("00");
			}
		}

		public string FullDateTime
		{
			get
			{
				if (Value != string.Empty)
					return string.Format("{0} {1}:{2}", Value, Hour, Minute);
				else
					return Value;
			}
		}

		public bool Enabled
		{
			set
			{
				txtIsActive.Text = (Convert.ToInt32(value)).ToString();
				drpHour.Enabled = value;
				drpMinute.Enabled = value;
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
			txtShowDate.Attributes["isRequired"] = this.IsRequired.ToString().ToLower();
			drpHour.Attributes["isRequired"] = this.IsRequired.ToString().ToLower();
			drpMinute.Attributes["isRequired"] = this.IsRequired.ToString().ToLower();
			
			if (this.ValidationSet != string.Empty)
			{
				txtShowDate.Attributes["validationSet"] = this.ValidationSet;
				drpHour.Attributes["validationSet"] = this.ValidationSet;
				drpMinute.Attributes["validationSet"] = this.ValidationSet;
			}

			base.OnInit(e);
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}