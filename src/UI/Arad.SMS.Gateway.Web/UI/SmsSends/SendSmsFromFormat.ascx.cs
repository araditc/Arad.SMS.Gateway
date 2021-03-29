using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Arad.SMS.Gateway.Common;

namespace Arad.SMS.Gateway.Web.UI.SmsSends
{
	public partial class SendSmsFromFormat : UIUserControlBase
	{
		private string Ip
		{
			get { return Request.UserHostAddress; }
		}

		private string Browser
		{
			get { return Request.Browser.Browser; }
		}

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
			#region GetSenderNumber
			drpSenderNumber.DataSource = Facade.PrivateNumber.GetUserPrivateNumbersForSend(UserGuid);
			drpSenderNumber.DataTextField = "Number";
			drpSenderNumber.DataValueField = "Guid";
			drpSenderNumber.DataBind();
			drpSenderNumber.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			#endregion

			#region GetUserSmsFormat
			drpSmsFormat.DataSource = Facade.SmsFormat.GetUserSmsFormats(UserGuid);
			drpSmsFormat.DataTextField = "FormatName";
			drpSmsFormat.DataValueField = "FormatGuid";
			drpSmsFormat.DataBind();
			drpSmsFormat.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			#endregion

			dtpSendDateTime.Date = DateManager.GetSolarDate(DateTime.Now);
			dtpSendDateTime.Time = DateTime.Now.TimeOfDay.ToString();

			btnSendTestSms.Text = Language.GetString(btnSendTestSms.Text);
			btnSendSms.Text = Language.GetString(btnSendSms.Text);
		}

		protected void btnSendTestSms_Click(object sender, EventArgs e)
		{
			Common.ScheduledSms scheduledSms = new Common.ScheduledSms();
			List<string> lstNumbers = new List<string>();
			try
			{
				if (Helper.GetGuid(drpSenderNumber.SelectedValue) == Guid.Empty)
					throw new Exception(Language.GetString("InvalidSenderNumber"));

				if (Helper.GetGuid(drpSmsFormat.SelectedValue) == Guid.Empty)
					throw new Exception(Language.GetString("SelectFormatError"));

				scheduledSms.PrivateNumberGuid = Helper.GetGuid(drpSenderNumber.SelectedValue);
				lstNumbers = txtTestReciever.Text.Split(new string[] { "\n", "\r\n", ",", " ", ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
				Dictionary<int, int> operatorNumberCount = Facade.Outbox.GetCountNumberOfOperators(ref lstNumbers);

				if (operatorNumberCount.Values.Sum() == 0)
					throw new Exception(Language.GetString("RecieverListIsEmpty"));

				scheduledSms.SmsText = Facade.SmsFormat.GetFormatText(Helper.GetGuid(drpSmsFormat.SelectedValue));
				scheduledSms.SmsLen = Helper.GetSmsCount(scheduledSms.SmsText);
				scheduledSms.PresentType = (int)Arad.SMS.Gateway.Business.Messageclass.Normal;
				scheduledSms.Encoding = Helper.HasUniCodeCharacter(scheduledSms.SmsText) ? (int)Encoding.Utf8 : (int)Encoding.Default;
				scheduledSms.DateTimeFuture = DateTime.Now;
				scheduledSms.UserGuid = UserGuid;
				scheduledSms.TypeSend = (int)SmsSendType.SendSms;
				scheduledSms.Status = (int)Common.ScheduledSmsStatus.Stored;
				scheduledSms.IP = Ip;
				scheduledSms.Browser = Browser;

				Facade.ScheduledSms.InsertSms(scheduledSms, lstNumbers);
				ClientSideScript = string.Format("result('step1','OK','{0}')", Language.GetString("SendSmsSuccessful"));
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("result('step1','Error','{0}')", ex.Message);
			}
		}
		protected void btnSendSms_Click(object sender, EventArgs e)
		{
			Common.ScheduledSms scheduledSms = new Common.ScheduledSms();
			string formatInfo = string.Empty;
			try
			{
				scheduledSms.PrivateNumberGuid = Helper.GetGuid(drpSenderNumber.SelectedValue);
				scheduledSms.ReferenceGuid = drpSmsFormat.SelectedValue;
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
                scheduledSms.UserGuid = UserGuid;
				scheduledSms.Status = (int)ScheduledSmsStatus.Stored;
				scheduledSms.IP = Ip;
				scheduledSms.Browser = Browser;

				if (!Facade.ScheduledSms.InsertFormatSms(scheduledSms, ref formatInfo))
					throw new Exception(Language.GetString("ErrorRecord"));

				ClientSideScript = string.Format("result('step2','OK','{0}');setSendPriceTableInfo('{1}');", Language.GetString("SendSmsSuccessful"), formatInfo);
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("result('step2','Error','{0}');setSendPriceTableInfo('{1}');", ex.Message, formatInfo);
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SendFormatSms);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_SmsSends_SendSmsFromFormat;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_SmsSends_SendSmsFromFormat.ToString());
		}
	}
}
