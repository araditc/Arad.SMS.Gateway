using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.SmsSenderAgents
{
	public partial class SaveSmsSenderAgent : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid SmsSenderAgentGuid
		{
			get { return Helper.RequestGuid(this, "AgentGuid"); }
		}

		private string ActionType
		{
			get { return Helper.Request(this, "ActionType").ToLower(); }
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
				drpSmsSenderAgents.Items.Add(new ListItem());
				foreach (SmsSenderAgentReference agent in Enum.GetValues(typeof(SmsSenderAgentReference)))
					drpSmsSenderAgents.Items.Add(new ListItem(Language.GetString(agent.ToString()), ((int)agent).ToString()));

				foreach (SenderAgentType agentType in Enum.GetValues(typeof(SenderAgentType)))
					drpType.Items.Add(new ListItem(Language.GetString(agentType.ToString()), ((int)agentType).ToString()));

				if (ActionType.ToLower() == "edit")
				{
					Common.SmsSenderAgent smsSenderAgent = new Common.SmsSenderAgent();
					smsSenderAgent = Facade.SmsSenderAgent.LoadAgent(SmsSenderAgentGuid);

					drpSmsSenderAgents.SelectedValue = smsSenderAgent.SmsSenderAgentReference.ToString();
					drpType.SelectedValue = smsSenderAgent.Type.ToString();
					txtName.Text = smsSenderAgent.Name;
					txtDefaultNumber.Text = smsSenderAgent.DefaultNumber;
					drpStartHour.SelectedValue = smsSenderAgent.StartSendTime.Hours.ToString("00");
					drpStartMinute.SelectedValue = smsSenderAgent.StartSendTime.Minutes.ToString("00");
					drpEndHour.SelectedValue = smsSenderAgent.EndSendTime.Hours.ToString();
					drpEndMinute.SelectedValue = smsSenderAgent.EndSendTime.Minutes.ToString();
					chkSmsAlert.Checked = smsSenderAgent.SendSmsAlert;
					chkIsSendActive.Checked = smsSenderAgent.IsSendActive;
					chkIsRecieveActive.Checked = smsSenderAgent.IsRecieveActive;
					chkIsSendBulkActive.Checked = smsSenderAgent.IsSendBulkActive;
					chkSendBulkIsAutomatic.Checked = smsSenderAgent.SendBulkIsAutomatic;
					chkCheckMessageID.Checked = smsSenderAgent.CheckMessageID;
					chkRouteActive.Checked = smsSenderAgent.RouteActive;
					chbIsSmpp.Checked = smsSenderAgent.IsSmpp;
					txtQueueLength.Text = smsSenderAgent.QueueLength.ToString();
					txtUsername.Text = smsSenderAgent.Username;
					hdnPass.Value = smsSenderAgent.Password;
					txtSendLink.Text = smsSenderAgent.SendLink;
					txtReceiveLink.Text = smsSenderAgent.ReceiveLink;
					txtDeliveryLink.Text = smsSenderAgent.DeliveryLink;
					txtDomain.Text = smsSenderAgent.Domain;
				}

				btnSave.Text = Language.GetString(btnSave.Text);
				btnCancel.Text = Language.GetString(btnCancel.Text);
			}
			catch { }
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.SmsSenderAgent smsSenderAgent = new Common.SmsSenderAgent();
			try
			{
				smsSenderAgent.SmsSenderAgentReference = Helper.GetInt(drpSmsSenderAgents.SelectedValue);
				smsSenderAgent.Type = Helper.GetInt(drpType.SelectedValue);
				smsSenderAgent.Name = txtName.Text;
				smsSenderAgent.DefaultNumber = txtDefaultNumber.Text;
				smsSenderAgent.StartSendTime = new TimeSpan(Helper.GetInt(drpStartHour.SelectedValue), Helper.GetInt(drpStartMinute.SelectedValue), 0);
				smsSenderAgent.EndSendTime = new TimeSpan(Helper.GetInt(drpEndHour.SelectedValue), Helper.GetInt(drpEndMinute.SelectedValue), 0);

				smsSenderAgent.SendSmsAlert = chkSmsAlert.Checked;
				smsSenderAgent.IsSendActive = chkIsSendActive.Checked;
				smsSenderAgent.IsRecieveActive = chkIsRecieveActive.Checked;
				smsSenderAgent.IsSendBulkActive = chkIsSendBulkActive.Checked;
				smsSenderAgent.SendBulkIsAutomatic = chkSendBulkIsAutomatic.Checked;
				smsSenderAgent.CheckMessageID = chkCheckMessageID.Checked;

				smsSenderAgent.RouteActive = chkRouteActive.Checked;
				smsSenderAgent.IsSmpp = chbIsSmpp.Checked;
				smsSenderAgent.QueueLength = Helper.GetInt(txtQueueLength.Text);
				smsSenderAgent.Username = txtUsername.Text;
				smsSenderAgent.Password = !string.IsNullOrEmpty(txtPassword.Text) ? txtPassword.Text : hdnPass.Value;
				smsSenderAgent.Domain = txtDomain.Text;
				smsSenderAgent.SendLink = txtSendLink.Text;
				smsSenderAgent.ReceiveLink = txtReceiveLink.Text;
				smsSenderAgent.DeliveryLink = txtDeliveryLink.Text;
				smsSenderAgent.CreateDate = DateTime.Now;
				smsSenderAgent.UserGuid = UserGuid;

				if (smsSenderAgent.HasError)
					throw new Exception(smsSenderAgent.ErrorMessage);

				switch (ActionType.ToLower())
				{
					case "edit":
						smsSenderAgent.SmsSenderAgentGuid = SmsSenderAgentGuid;
						if (!Facade.SmsSenderAgent.UpdateAgent(smsSenderAgent))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;

					case "insert":
						if (!Facade.SmsSenderAgent.Insert(smsSenderAgent))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
				}

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SmsSenderAgent, Session)));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SmsSenderAgent, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageSmsSenderAgent);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SaveSmsSenderAgent;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsSenderAgents_SaveSmsSenderAgent.ToString());
		}
	}
}