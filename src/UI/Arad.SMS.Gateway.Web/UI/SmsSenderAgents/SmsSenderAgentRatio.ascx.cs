using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.SmsSenderAgents
{
	public partial class SmsSenderAgentRatio : UIUserControlBase
	{
		public Guid AgentGuid
		{
			get
			{
				return Helper.RequestGuid(this, "AgentGuid");
			}
		}

		public SmsSenderAgentRatio()
		{
			AddDataBinderHandlers("gridRatio", new DataBindHandler(gridRatio_OnDataBind));
			AddDataRenderHandlers("gridRatio", new CellValueRenderEventHandler(gridRatio_OnDataRender));
		}

		public DataTable gridRatio_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.AgentRatio.GetPagedAgentRatio(AgentGuid);
		}

		public string gridRatio_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "SmsType":
					return Language.GetString(((SmsTypes)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());
				case "Action":
					return string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteRow(event);""></span>", Language.GetString("Delete"));
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
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

			drpOperator.DataSource = Facade.Operator.GetOperators();
			drpOperator.DataTextField = "Name";
			drpOperator.DataValueField = "ID"; ;
			drpOperator.DataBind();

			foreach (SmsTypes type in System.Enum.GetValues(typeof(SmsTypes)))
				drpSmsType.Items.Add(new ListItem(Language.GetString(type.ToString()), ((int)type).ToString()));
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.AgentRatio agentRatio = new Common.AgentRatio();
			try
			{
				agentRatio.SmsType = Helper.GetByte(drpSmsType.SelectedValue);
				agentRatio.Ratio = Helper.GetDecimal(txtRatio.Text);
				agentRatio.CreateDate = DateTime.Now;
				agentRatio.OperatorID = Helper.GetByte(drpOperator.SelectedValue);
				agentRatio.SmsSenderAgentGuid = AgentGuid;

				if (agentRatio.HasError)
					throw new Exception(agentRatio.ErrorMessage);

				if (!Facade.AgentRatio.Insert(agentRatio))
					throw new Exception(Language.GetString("ErrorRecord"));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}",
												Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SmsSenderAgent, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SmsSenderAgentRatio);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SmsSenderAgentRatio;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsSenderAgents_SmsSenderAgentRatio.ToString());
		}
	}
}