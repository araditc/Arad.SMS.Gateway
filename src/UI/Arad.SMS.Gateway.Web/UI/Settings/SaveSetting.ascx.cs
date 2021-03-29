using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Arad.SMS.Gateway.Web.UI.Settings
{
	public partial class SaveSetting : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public bool IsSuperAdmin
		{
			get { return Helper.GetBool(Session["IsSuperAdmin"]); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			if (!IsSuperAdmin)
				return;

			btnSave.Text = Language.GetString(btnSave.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);

			btnSave.Attributes["onclick"] = string.Format("return validateRequiredFields('saveSetting');");

			try
			{
				DataTable dtSettings = Facade.Setting.GetSettings(Guid.Empty);
				string value = string.Empty;

				foreach (DataRow row in dtSettings.Rows)
				{
					value = row["value"].ToString();
					switch (Helper.GetInt(row["Key"]))
					{
						case (int)MainSettings.Tax:
							txtTax.Text = value;
							break;
						case (int)MainSettings.SendQueueRecipientAddress:
							txtSendQueueRecipientAddress.Text = value;
							break;
						case (int)MainSettings.IsRemoteQueue:
							chbIsRemoteQueue.Checked = Helper.GetBool(value);
							break;
						case (int)MainSettings.RemoteQueueIP:
							txtRemoteQueueIP.Text = value;
							break;
						case (int)MainSettings.MaximumFailedTryCount:
							txtMaximumFailedTryCount.Text = value;
							break;
						case (int)MainSettings.AppPath:
							txtAppPath.Text = value;
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
				if (!string.IsNullOrEmpty(txtAppPath.Text))
					dictionarySetting.Add((int)MainSettings.AppPath, txtAppPath.Text);
				if (!string.IsNullOrEmpty(txtTax.Text))
					dictionarySetting.Add((int)MainSettings.Tax, txtTax.Text);
				if (!string.IsNullOrEmpty(txtSendQueueRecipientAddress.Text))
					dictionarySetting.Add((int)MainSettings.SendQueueRecipientAddress, txtSendQueueRecipientAddress.Text);
				if (!string.IsNullOrEmpty(txtMaximumFailedTryCount.Text))
					dictionarySetting.Add((int)MainSettings.MaximumFailedTryCount, txtMaximumFailedTryCount.Text);
				if (!string.IsNullOrEmpty(txtRemoteQueueIP.Text))
					dictionarySetting.Add((int)MainSettings.RemoteQueueIP, txtRemoteQueueIP.Text);

				dictionarySetting.Add((int)MainSettings.IsRemoteQueue, (chbIsRemoteQueue.Checked ? 1 : 0).ToString());

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

				if (!Facade.Setting.InsertSetting(Guid.Empty, dtSettings))
					throw new Exception(Language.GetString("ErrorRecord"));

				ShowMessageBox(Language.GetString("InsertRecord"), string.Empty, "success");
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Settings_Setting, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SaveMainSetting);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Settings_SaveSetting;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Settings_SaveSetting.ToString());
		}
	}
}