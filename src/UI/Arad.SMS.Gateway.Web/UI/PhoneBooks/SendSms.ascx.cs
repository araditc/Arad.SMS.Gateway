using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.PhoneBooks
{
	public partial class SendSms : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private List<string> ListOfPhoneBookGuid
		{
			get
			{
				string guids = Helper.Request(this, "PhoneBookGuids").TrimEnd(',');
				List<string> lstPhoneBookGuid = guids.Split(',').ToList();
				return lstPhoneBookGuid;
			}
		}

		public string PhoneBookGuids
		{
			get
			{
				return string.Join(",", ListOfPhoneBookGuid.Select(s => s.Trim('\'')).ToList());
			}
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
			try
			{
				btnSave.Attributes["onclick"] = "return validateRequiredFields();";
				btnSave.Text = Language.GetString(btnSave.Text);

				dtpSendDateTime.Date = DateManager.GetSolarDate(DateTime.Now);
				dtpSendDateTime.Time = DateTime.Now.TimeOfDay.ToString();

				#region GetSenderNumber
				DataTable dtSender = Facade.PrivateNumber.GetUserPrivateNumbersForSend(UserGuid);
				dtSender.Columns.Add("Value", typeof(string));
				foreach (DataRow row in dtSender.Rows)
					row["Value"] = string.Format("{0};{1}", row["Guid"], row["Type"]);
				drpSenderNumber.DataSource = dtSender;
				drpSenderNumber.DataTextField = "Number";
				drpSenderNumber.DataValueField = "Value";
				drpSenderNumber.DataBind();
				drpSenderNumber.Items.Insert(0, new ListItem(string.Empty, string.Empty));
				#endregion

				DataTable dtInfo = Facade.PhoneBook.GetPhoneBookInfo(string.Join(",", ListOfPhoneBookGuid));
				foreach (DataRow row in dtInfo.Rows)
					txtGroupName.Text += string.Format("{0},", row["Name"].ToString());

				txtGroupName.Text = txtGroupName.Text.TrimEnd(',');
				txtGroupName.Enabled = false;
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.ScheduledSms scheduledSms = new Common.ScheduledSms();
			try
			{
				int numberType = Helper.GetInt(drpSenderNumber.SelectedValue.Split(';')[1]);

				if (Helper.GetGuid(drpSenderNumber.SelectedValue.Split(';')[0]) == Guid.Empty)
					throw new Exception(Language.GetString("InvalidSenderNumber"));

				if (string.IsNullOrEmpty(txtSmsBody.Text))
					throw new Exception(Language.GetString("BlankMessage"));

				Facade.PhoneBook.CheckPhoneBookType(numberType, string.Join(",", ListOfPhoneBookGuid));

				scheduledSms.PrivateNumberGuid = Helper.GetGuid(drpSenderNumber.SelectedValue.Split(';')[0]);
				scheduledSms.ReferenceGuid = PhoneBookGuids;
				scheduledSms.SmsText = txtSmsBody.Text;
				scheduledSms.SmsLen = Helper.GetSmsCount(txtSmsBody.Text);
				scheduledSms.PresentType = txtSmsBody.IsFlashSms ? (int)Messageclass.Flash : (int)Messageclass.Normal;
				scheduledSms.Encoding = Helper.HasUniCodeCharacter(txtSmsBody.Text) ? (int)Encoding.Utf8 : (int)Encoding.Default;
				scheduledSms.DateTimeFuture = DateManager.GetChristianDateTimeForDB(dtpSendDateTime.FullDateTime);
				scheduledSms.UserGuid = UserGuid;
				scheduledSms.Status = (int)ScheduledSmsStatus.Stored;
				scheduledSms.TypeSend = (int)SmsSendType.SendGroupSms;
				scheduledSms.IP = Ip;
				scheduledSms.Browser = Browser;

				if (!Facade.ScheduledSms.InsertGroupSms(scheduledSms))
					throw new Exception(Language.GetString("ErrorRecord"));

				ShowMessageBox(Language.GetString("InsertRecord"), string.Empty, "success");
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.PhoneBook);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_SendSms;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PhoneBooks_SendSms.ToString());
		}
	}
}