using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.RegularContents
{
	public partial class SaveRegularContent : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private string ActionType
		{
			get { return Helper.Request(this, "ActionType").ToLower(); }
		}

		private Guid RegularContentGuid
		{
			get { return Helper.RequestGuid(this, "Guid"); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnSave.Text = Language.GetString(btnSave.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);
			dtpStartDateTime.Date = DateManager.GetSolarDate(DateTime.Now);
			dtpStartDateTime.Time = DateTime.Now.TimeOfDay.ToString();
			dtpEndDateTime.Date = DateManager.GetSolarDate(DateTime.Now);
			dtpEndDateTime.Time = DateTime.Now.TimeOfDay.ToString();

			#region GetSenderNumber
			drpSenderNumber.DataSource = Facade.PrivateNumber.GetUserPrivateNumbersForSend(UserGuid);
			drpSenderNumber.DataTextField = "Number";
			drpSenderNumber.DataValueField = "Guid";
			drpSenderNumber.DataBind();
			#endregion


			#region Type
			foreach (RegularContentType type in System.Enum.GetValues(typeof(RegularContentType)))
				drpType.Items.Add(new ListItem(Language.GetString(type.ToString()), ((int)type).ToString()));
			drpType.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			#endregion

			#region GetPeriodType
			foreach (SmsSentPeriodType periodType in System.Enum.GetValues(typeof(SmsSentPeriodType)))
				drpPeriodType.Items.Add(new ListItem(Language.GetString(periodType.ToString()), ((int)periodType).ToString()));
			drpPeriodType.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			#endregion

			#region WarningType
			foreach (WarningType warning in System.Enum.GetValues(typeof(WarningType)))
				drpWatningType.Items.Add(new ListItem(Language.GetString(warning.ToString()), ((int)warning).ToString()));
			#endregion

			if (ActionType == "edit")
			{
				Common.RegularContent regularContent = Facade.RegularContent.Load(RegularContentGuid);

				txtTitle.Text = regularContent.Title;
				drpType.SelectedValue = regularContent.Type.ToString();
				drpPeriodType.SelectedValue = regularContent.PeriodType.ToString();
				txtPeriod.Text = regularContent.Period.ToString();
				drpWatningType.SelectedValue = regularContent.WarningType.ToString();
				dtpStartDateTime.Date = DateManager.GetSolarDate(regularContent.StartDateTime);
				dtpStartDateTime.Time = regularContent.StartDateTime.TimeOfDay.ToString();
				dtpEndDateTime.Date = DateManager.GetSolarDate(regularContent.EndDateTime);
				dtpEndDateTime.Time = regularContent.EndDateTime.TimeOfDay.ToString();
				chbIsActive.Checked = regularContent.IsActive;
				drpSenderNumber.SelectedValue = regularContent.PrivateNumberGuid.ToString();
				Business.RegularContentSerialization regularContentSerialization = new Business.RegularContentSerialization();
				regularContentSerialization = (Business.RegularContentSerialization)GeneralLibrary.SerializationTools.DeserializeXml(regularContent.Config, typeof(Business.RegularContentSerialization));

				switch (regularContent.Type)
				{
					case (int)RegularContentType.URL:
						txtUrl.Text = regularContentSerialization.URL;
						break;
					case (int)RegularContentType.DB:
						txtConnectionString.Text = regularContentSerialization.ConnectionString;
						txtQuery.Text = regularContentSerialization.Query;
						txtField.Text = regularContentSerialization.Field;
						break;
				}
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.RegularContent regularContent = new Common.RegularContent();
			Business.RegularContentSerialization regularContentSerialization = new RegularContentSerialization();
			try
			{
				regularContent.Type = Helper.GetInt(drpType.SelectedValue);
				switch (regularContent.Type)
				{
					case (int)RegularContentType.URL:
						if (string.IsNullOrEmpty(txtUrl.Text))
							throw new Exception(Language.GetString("CompleteURLField"));

						regularContentSerialization.URL = txtUrl.Text;
						break;
					case (int)RegularContentType.DB:
						if (string.IsNullOrEmpty(txtConnectionString.Text))
							throw new Exception(Language.GetString("CompleteConnectionStringField"));
						if (string.IsNullOrEmpty(txtQuery.Text))
							throw new Exception(Language.GetString("CompleteQueryField"));
						if (string.IsNullOrEmpty(txtField.Text))
							throw new Exception(Language.GetString("CompleteColumnField"));

						regularContentSerialization.ConnectionString = txtConnectionString.Text;
						regularContentSerialization.Query = txtQuery.Text;
						regularContentSerialization.Field = txtField.Text;
						break;
				}

				regularContent.Title = txtTitle.Text;
				regularContent.Type = Helper.GetInt(drpType.SelectedValue);
				regularContent.IsActive = chbIsActive.Checked;
				regularContent.PeriodType = Helper.GetInt(drpPeriodType.SelectedValue);
				regularContent.Period = Helper.GetInt(drpPeriodType.SelectedValue);
				regularContent.WarningType = Helper.GetInt(drpWatningType.SelectedValue);
				regularContent.CreateDate = DateTime.Now;
				//regularContent.StartDateTime = DateManager.GetChristianDateTimeForDB(dtpStartDateTime.FullDateTime);
				//regularContent.EndDateTime = DateManager.GetChristianDateTimeForDB(dtpEndDateTime.FullDateTime);
                var dateTime = dtpStartDateTime.FullDateTime;
                var dateTimeEnd = dtpEndDateTime.FullDateTime;
                if (Session["Language"].ToString() == "fa")
                {
                    regularContent.StartDateTime = DateManager.GetChristianDateTimeForDB(dateTime);
                    regularContent.EndDateTime = DateManager.GetChristianDateTimeForDB(dateTimeEnd);
                }
                else
                {
                    regularContent.StartDateTime = DateTime.Parse(dateTime);
                    regularContent.StartDateTime = DateTime.Parse(dateTimeEnd);
                }
                regularContent.EffectiveDateTime = regularContent.StartDateTime;
				regularContent.PrivateNumberGuid = Helper.GetGuid(drpSenderNumber.SelectedValue);
				regularContent.UserGuid = UserGuid;
				regularContent.Config = SerializationTools.SerializeToXml(regularContentSerialization, regularContentSerialization.GetType());

				if (regularContent.HasError)
					throw new Exception(regularContent.ErrorMessage);

				switch (ActionType)
				{
					case "insert":
						if (!Facade.RegularContent.Insert(regularContent))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
					case "edit":
						regularContent.RegularContentGuid = RegularContentGuid;
						if (!Facade.RegularContent.Update(regularContent))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
				}
				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_RegularContents_RegularContent, Session)));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_RegularContents_RegularContent, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.RegularContent);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_RegularContents_SaveRegularContent;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_RegularContents_SaveRegularContent.ToString());
		}
	}
}
