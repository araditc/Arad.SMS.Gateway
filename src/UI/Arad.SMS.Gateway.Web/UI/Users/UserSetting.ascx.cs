using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.Users
{
	public partial class UserSetting : UIUserControlBase
	{
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
			btnSave.Text = Language.GetString(btnSave.Text);

			#region GetNumber
			int resultCount = 0;
			drpNumber.DataSource = Facade.PrivateNumber.GetUserPrivateNumbersForSend(UserGuid); //Facade.PrivateNumber.GetUserNumbers(UserGuid, string.Empty, 0, 0, "[CreateDate]", ref resultCount);
			drpNumber.DataTextField = "Number";
			drpNumber.DataValueField = "Guid";
			drpNumber.DataBind();
			drpNumber.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			#endregion

			DataTable dtTrafficRelay = Facade.TrafficRelay.GetPagedTrafficRelays(UserGuid, "CreateDate", 0, 0, ref resultCount);
			drpSmsTrafficRelay.DataSource = dtTrafficRelay;
			drpSmsTrafficRelay.DataTextField = "Url";
			drpSmsTrafficRelay.DataValueField = "Guid";
			drpSmsTrafficRelay.DataBind();
			drpSmsTrafficRelay.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			drpDeliveryTrafficRelay.DataSource = dtTrafficRelay;
			drpDeliveryTrafficRelay.DataTextField = "Url";
			drpDeliveryTrafficRelay.DataValueField = "Guid";
			drpDeliveryTrafficRelay.DataBind();
			drpDeliveryTrafficRelay.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			DataTable dataTableSettings = Facade.UserSetting.GetUserSettings(UserGuid);

			foreach (DataRow row in dataTableSettings.Rows)
			{
				string value = row["Value"].ToString();
				switch (Helper.GetInt(row["Key"]))
				{
					case (int)AccountSetting.LoginWarning:
						txtReceivers.Text = value;
						break;
					case (int)AccountSetting.CreditWarning:
						txtCredit.Text = Helper.FormatDecimalForDisplay(value);
						break;
					case (int)AccountSetting.ExpireWarning:
						drpExpire.SelectedValue = value;
						break;
					case (int)AccountSetting.DefaultNumber:
						drpNumber.SelectedValue = value;
						break;
					case (int)AccountSetting.ApiIP:
						txtIP.Text = value;
						break;
					case (int)AccountSetting.ApiPassword:
						hdnApiPassword.Value = value;
						break;
					case (int)AccountSetting.SmsTrafficRelay:
						drpSmsTrafficRelay.SelectedValue = value;
						break;
					case (int)AccountSetting.DeliveryTrafficRelay:
						drpDeliveryTrafficRelay.SelectedValue = value;
						break;
				}
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Dictionary<int, string> dictionarySetting = new Dictionary<int, string>();
			DataTable dtSettings = new DataTable();
			DataRow row;
			dtSettings.Columns.Add("Key", typeof(int));
			dtSettings.Columns.Add("Value", typeof(string));
			dtSettings.Columns.Add("Status", typeof(int));
			dictionarySetting.Clear();
			try
			{
				List<string> lstReceivers = txtReceivers.Text.Split(new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
				Dictionary<int, int> operatorNumberCount = Facade.Outbox.GetCountNumberOfOperators(ref lstReceivers);

				List<string> lstIPs = txtIP.Text.Split(new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();

				if (lstReceivers.Count > 0)
					dictionarySetting.Add((int)AccountSetting.LoginWarning, string.Join(",", lstReceivers));
				if (Helper.GetLong(txtCredit.Text) > 0)
					dictionarySetting.Add((int)AccountSetting.CreditWarning, Helper.GetLong(txtCredit.Text).ToString());
				if (Helper.GetInt(drpExpire.SelectedValue) > 0)
					dictionarySetting.Add((int)AccountSetting.ExpireWarning, drpExpire.SelectedValue);
				if (Helper.GetGuid(drpNumber.SelectedValue) != Guid.Empty)
					dictionarySetting.Add((int)AccountSetting.DefaultNumber, drpNumber.SelectedValue);
				if (lstIPs.Count > 0)
					dictionarySetting.Add((int)AccountSetting.ApiIP, string.Join(",", lstIPs));
				if (Helper.GetGuid(drpSmsTrafficRelay.SelectedValue) != Guid.Empty)
					dictionarySetting.Add((int)AccountSetting.SmsTrafficRelay, drpSmsTrafficRelay.SelectedValue);
				if (Helper.GetGuid(drpDeliveryTrafficRelay.SelectedValue) != Guid.Empty)
					dictionarySetting.Add((int)AccountSetting.DeliveryTrafficRelay, drpDeliveryTrafficRelay.SelectedValue);

				if (!string.IsNullOrEmpty(txtPassword.Text))
				{
					if (txtPassword.Text != txtConfirmPassword.Text)
						throw new Exception(Language.GetString("PasswordNotMatchWithConfirmPassword"));

					string password = Helper.GetMd5Hash(txtPassword.Text);
					dictionarySetting.Add((int)AccountSetting.ApiPassword, password);
				}
				else if (!string.IsNullOrEmpty(hdnApiPassword.Value))
					dictionarySetting.Add((int)AccountSetting.ApiPassword, hdnApiPassword.Value);

				foreach (AccountSetting setting in Enum.GetValues(typeof(AccountSetting)))
				{
					if (dictionarySetting.Keys.Contains((int)setting))
					{
						row = dtSettings.NewRow();

						row["Key"] = (int)setting;
						row["Value"] = dictionarySetting[(int)setting];
						row["Status"] = 0;
						dtSettings.Rows.Add(row);
					}
				}

				if (!Facade.UserSetting.InsertUserSetting(UserGuid, dtSettings))
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
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.UserSetting);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_Users_UserSetting;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_Users_UserSetting.ToString());
		}
	}
}