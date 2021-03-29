using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.SmsSends
{
	public partial class SendP2PSms : UIUserControlBase
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
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnSave.Text = Language.GetString(btnSave.Text);

			#region GetSenderNumber
			DataTable dtSender = Facade.PrivateNumber.GetUserPrivateNumbersForSend(UserGuid);
			drpSenderNumber.DataSource = dtSender;
			drpSenderNumber.DataTextField = "Number";
			drpSenderNumber.DataValueField = "Guid";
			drpSenderNumber.DataBind();
			drpSenderNumber.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			#endregion

			//dtpSendDateTime.Date = DateManager.GetSolarDate(DateTime.Now);

            if (Session["Language"].ToString() == "fa")
            {
                dtpSendDateTime.Date = DateManager.GetSolarDate(DateTime.Now);
            }
            else
            {
                dtpSendDateTime.Date = DateTime.Now.ToShortDateString();
            }

            dtpSendDateTime.Time = DateTime.Now.TimeOfDay.ToString();
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			ScheduledSms scheduledSms = new ScheduledSms();
			try
			{
				scheduledSms.ScheduledSmsGuid = Guid.NewGuid();
				scheduledSms.PrivateNumberGuid = Helper.GetGuid(drpSenderNumber.SelectedValue.Split(';')[0]);
                var dateTime = dtpSendDateTime.FullDateTime;
                if (Session["Language"].ToString() == "fa")
                {
                    scheduledSms.DateTimeFuture = DateManager.GetChristianDateTimeForDB(dateTime);
                }
                else
                {
                    scheduledSms.DateTimeFuture = DateTime.Parse(dateTime);
                }
                //scheduledSms.DateTimeFuture = DateManager.GetChristianDateTimeForDB(dtpSendDateTime.FullDateTime);
				scheduledSms.FilePath = hdnFilePath.Value;
				scheduledSms.SmsPattern = hdnSmsFormat.Value.TrimEnd(',');
				scheduledSms.UserGuid = UserGuid;
				scheduledSms.Status = (int)ScheduledSmsStatus.Stored;
				scheduledSms.TypeSend = (int)Common.SmsSendType.SendP2PSms;
				scheduledSms.IP = Ip;
				scheduledSms.Browser = Browser;

				Facade.ScheduledSms.InsertP2PSms(scheduledSms);

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_ScheduledSms, Session)));
			}
			catch(Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SendP2PSms);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSends_SendP2PSms;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsSends_SendP2PSms.ToString());
		}
	}
}
