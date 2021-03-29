using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;

namespace Arad.SMS.Gateway.Web.UI.SmsSenderAgents
{
	public partial class SaveRoute : UIUserControlBase
	{
		public Guid AgentGuid
		{
			get
			{
				return Helper.RequestGuid(this, "SmsAgentGuid");
			}
		}

		private Guid RouteGuid
		{
			get { return Helper.RequestGuid(this, "RouteGuid"); }
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
				btnSave.Text = Language.GetString(btnSave.Text);
				btnCancel.Text = Language.GetString(btnCancel.Text);
				btnSave.Attributes["onclick"] = "return validateRequiredFields();";

				drpOperator.DataSource = Facade.Operator.GetOperators();
				drpOperator.DataTextField = "Name";
				drpOperator.DataValueField = "ID"; ;
				drpOperator.DataBind();

				if (ActionType == "edit")
				{
					Common.Route route = Facade.Route.Load(RouteGuid);

					txtName.Text = route.Name;
					txtUsername.Text = route.Username;
					txtDomain.Text = route.Domain;
					hdnPass.Value = route.Password;
					drpOperator.SelectedValue = route.OperatorID.ToString();
					txtQueueLength.Text = route.QueueLength.ToString();
					txtLink.Text = route.Link;
				}
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.Route route = new Common.Route();
			try
			{
				route.Name = txtName.Text;
				route.Username = txtUsername.Text;
				route.Password = !string.IsNullOrEmpty(txtPassword.Text) ? txtPassword.Text : hdnPass.Value;
				route.Domain = txtDomain.Text;
				route.Link = txtLink.Text;
				route.QueueLength = Helper.GetInt(txtQueueLength.Text);
				route.SmsSenderAgentGuid = AgentGuid;
				route.OperatorID = byte.Parse(drpOperator.SelectedValue);

				if (route.HasError)
					throw new Exception(route.ErrorMessage);

				switch (ActionType)
				{
					case "insert":
						if (!Facade.Route.Insert(route))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
					case "edit":
						route.RouteGuid = RouteGuid;
						if (!Facade.Route.Update(route))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
				}

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}&AgentGuid={1}",
													Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_MessageRoute, Session),
													AgentGuid));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}&AgentGuid={1}",
												Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_MessageRoute, Session),
												AgentGuid));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.MessageRoute);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SaveRoute;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsSenderAgents_SaveRoute.ToString());
		}
	}
}