using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.SmsSends
{
	public partial class SendBulk : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private string Ip
		{
			get { return Request.UserHostAddress; }
		}

		private string Browser
		{
			get { return Request.Browser.Browser; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if (!IsPostBack)
					InitializePage();
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		private void InitializePage()
		{
			btnSave.Text = Language.GetString(btnSave.Text);

			#region GetSenderNumber
			drpSenderNumber.DataSource = Facade.PrivateNumber.GetUserPrivateNumbersForSendBulk(UserGuid);
			drpSenderNumber.DataTextField = "Number";
			drpSenderNumber.DataValueField = "Guid";
			drpSenderNumber.DataBind();
			drpSenderNumber.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			#endregion

			#region Country
			var lstCountry = Facade.Zone.GetZones(Guid.Empty);
			drpCountry.DataSource = lstCountry;
			drpCountry.DataTextField = "Name";
			drpCountry.DataValueField = "Guid";
			drpCountry.DataBind();
			#endregion

			#region Province
			Guid countryGuid = Helper.GetGuid(drpCountry.SelectedValue);
			drpProvince.DataSource = Facade.Zone.GetZones(countryGuid);
			drpProvince.DataTextField = "Name";
			drpProvince.DataValueField = "Guid";
			drpProvince.DataBind();
			drpProvince.Items.Insert(0, new ListItem(string.Empty, Guid.Empty.ToString()));
			#endregion

			#region Operator
			drpOperator.DataSource = Facade.Operator.GetOperators();
			drpOperator.DataTextField = "Title";
			drpOperator.DataValueField = "ID"; ;
			drpOperator.DataBind();
			#endregion

			#region NumberType
			foreach (Business.NumberType type in System.Enum.GetValues(typeof(Business.NumberType)))
				drpType.Items.Add(new ListItem(Language.GetString(type.ToString()), ((int)type).ToString()));
			drpType.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			#endregion

			dtpSendDateTime.Date = DateManager.GetSolarDate(DateTime.Now);
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.ScheduledSms scheduledSms = new Common.ScheduledSms();
			try
			{
				scheduledSms.ScheduledSmsGuid = Guid.NewGuid();
				scheduledSms.PrivateNumberGuid = Helper.GetGuid(drpSenderNumber.SelectedValue);
				scheduledSms.SmsText = txtSmsBody.Text;
				scheduledSms.SmsLen = txtSmsBody.SmsCount;
				scheduledSms.PresentType = txtSmsBody.IsFlashSms ? (int)Messageclass.Flash : (int)Messageclass.Normal;
				scheduledSms.Encoding = Helper.HasUniCodeCharacter(txtSmsBody.Text) ? (int)Encoding.Utf8 : (int)Encoding.Default;
				scheduledSms.UserGuid = UserGuid;
				scheduledSms.TypeSend = (int)SmsSendType.SendBulkSms;
				//scheduledSms.DateTimeFuture = DateManager.GetChristianDateTimeForDB(dtpSendDateTime.FullDateTime);
                var dateTime = dtpSendDateTime.FullDateTime;
                if (Session["Language"].ToString() == "fa")
                {
                    scheduledSms.DateTimeFuture = DateManager.GetChristianDateTimeForDB(dateTime);
                }
                else
                {
                    scheduledSms.DateTimeFuture = DateTime.Parse(dateTime);
                }
                scheduledSms.Status = (int)ScheduledSmsStatus.Stored;
				scheduledSms.IP = Ip;
				scheduledSms.Browser = Browser;

				if (!Facade.ScheduledSms.InsertBulkRequest(scheduledSms,hdnRecipients.Value))
					throw new Exception(Language.GetString("ErrorRecord"));

				ShowMessageBox(Language.GetString("InsertRecord"), string.Empty, "success");
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
		{
			drpCity.Items.Clear();

			Guid countryGuid = Helper.GetGuid(drpCountry.SelectedValue);
			drpProvince.DataSource = Facade.Zone.GetZones(countryGuid);
			drpProvince.DataTextField = "Name";
			drpProvince.DataValueField = "Guid";
			drpProvince.DataBind();
			drpProvince.Items.Insert(0, new ListItem(string.Empty, Guid.Empty.ToString()));
		}

		protected void drpProvince_SelectedIndexChanged(object sender, EventArgs e)
		{
			Guid provinceGuid = Helper.GetGuid(drpProvince.SelectedValue);
			if (provinceGuid != Guid.Empty)
			{
				drpCity.DataSource = Facade.Zone.GetZones(provinceGuid);
				drpCity.DataTextField = "Name";
				drpCity.DataValueField = "Guid";
				drpCity.DataBind();
				drpCity.Items.Insert(0, new ListItem(string.Empty, Guid.Empty.ToString()));
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SendBulkSms);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_SmsSends_SendBulk;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_SmsSends_SendBulk.ToString());
		}
	}
}
