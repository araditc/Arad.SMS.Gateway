using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Arad.SMS.Gateway.Web.UI.Settings
{
	public partial class AdminSetting : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public bool IsMainAdmin
		{
			get { return Helper.GetBool(Session["IsMainAdmin"]); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			if (!IsMainAdmin)
				return;

			btnSave.Text = Language.GetString(btnSave.Text);

			btnSave.Attributes["onclick"] = string.Format("return validateRequiredFields('saveSetting');");

			try
			{
				DataTable dtSettings = Facade.Setting.GetSettings(UserGuid);
				string value = string.Empty;

				foreach (DataRow row in dtSettings.Rows)
				{
					value = row["value"].ToString();
					switch (Helper.GetInt(row["Key"]))
					{
						case (int)MainSettings.RegisterFishSmsText:
							txtRegisterFishSmsText.Text = value;
							break;
						case (int)MainSettings.LoginSmsText:
							txtLoginSmsText.Text = value;
							break;
						case (int)MainSettings.LowCreditSmsText:
							txtLowCreditSmsText.Text = value;
							break;
						case (int)MainSettings.RegisterUserSmsText:
							txtRegisterUserSmsText.Text = value;
							break;
						case (int)MainSettings.UserAccountSmsText:
							txtUserAccountSmsText.Text = value;
							break;
						case (int)MainSettings.UserExpireSmsText:
							txtUserExpireSmsText.Text = value;
							break;
						case (int)MainSettings.OnlinePaymentSmsText:
							txtOnlinePaymentSmsText.Text = value;
							break;
						case (int)MainSettings.RetrievePasswordSmsText:
							txtRetrievePasswordSmsText.Text = value;
							break;
						case (int)MainSettings.VasRegisterSmsText:
							txtVasRegisterSmsText.Text = value;
							break;
						case (int)MainSettings.SendSmsAlertMessage:
							txtSendSmsAlertMessage.Text = value;
							break;
					}
				}
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Dictionary<int, string> dictionarySetting = new Dictionary<int, string>();
			DataTable dtSettings = new DataTable();
			DataRow row;
			dtSettings.Columns.Add("Key", typeof(int));
			dtSettings.Columns.Add("Value", typeof(string));
			dictionarySetting.Clear();
			try
			{
				if (!string.IsNullOrEmpty(txtRegisterFishSmsText.Text))
					dictionarySetting.Add((int)MainSettings.RegisterFishSmsText, txtRegisterFishSmsText.Text);
				if (!string.IsNullOrEmpty(txtLoginSmsText.Text))
					dictionarySetting.Add((int)MainSettings.LoginSmsText, txtLoginSmsText.Text);
				if (!string.IsNullOrEmpty(txtLowCreditSmsText.Text))
					dictionarySetting.Add((int)MainSettings.LowCreditSmsText, txtLowCreditSmsText.Text);
				if (!string.IsNullOrEmpty(txtRegisterUserSmsText.Text))
					dictionarySetting.Add((int)MainSettings.RegisterUserSmsText, txtRegisterUserSmsText.Text);
				if (!string.IsNullOrEmpty(txtUserAccountSmsText.Text))
					dictionarySetting.Add((int)MainSettings.UserAccountSmsText, txtUserAccountSmsText.Text);
				if (!string.IsNullOrEmpty(txtUserExpireSmsText.Text))
					dictionarySetting.Add((int)MainSettings.UserExpireSmsText, txtUserExpireSmsText.Text);
				if (!string.IsNullOrEmpty(txtOnlinePaymentSmsText.Text))
					dictionarySetting.Add((int)MainSettings.OnlinePaymentSmsText, txtOnlinePaymentSmsText.Text);
				if (!string.IsNullOrEmpty(txtRetrievePasswordSmsText.Text))
					dictionarySetting.Add((int)MainSettings.RetrievePasswordSmsText, txtRetrievePasswordSmsText.Text);
				if (!string.IsNullOrEmpty(txtVasRegisterSmsText.Text))
					dictionarySetting.Add((int)MainSettings.VasRegisterSmsText, txtVasRegisterSmsText.Text);
				if (!string.IsNullOrEmpty(txtSendSmsAlertMessage.Text))
					dictionarySetting.Add((int)MainSettings.SendSmsAlertMessage, txtSendSmsAlertMessage.Text);

				foreach (MainSettings setting in Enum.GetValues(typeof(MainSettings)))
				{
					if (dictionarySetting.Keys.Contains((int)setting))
					{
						row = dtSettings.NewRow();

						row["Key"] = (int)setting;
						row["Value"] = dictionarySetting[(int)setting];
						dtSettings.Rows.Add(row);
					}
				}

				if (!Facade.Setting.InsertSetting(UserGuid, dtSettings))
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
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.AdminSetting);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_Settings_AdminSetting;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_Settings_AdminSetting.ToString());
		}
	}
}