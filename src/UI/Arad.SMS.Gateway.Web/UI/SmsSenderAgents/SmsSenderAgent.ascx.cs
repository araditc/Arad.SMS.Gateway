using System;
using System.Collections.Generic;
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using Arad.SMS.Gateway.Common;

namespace Arad.SMS.Gateway.Web.UI.SmsSenderAgents
{
	public partial class SmsSenderAgent : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public SmsSenderAgent()
		{
			AddDataBinderHandlers("gridAgents", new DataBindHandler(gridAgents_OnDataBind));
			AddDataRenderHandlers("gridAgents", new CellValueRenderEventHandler(gridAgents_OnDataRender));
		}

		public DataTable gridAgents_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.SmsSenderAgent.GetUserAgents(UserGuid);
		}

		public string gridAgents_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "SmsSenderAgentReference":
					return Language.GetString(((SmsSenderAgentReference)Helper.GetInt(e.CurrentRow["SmsSenderAgentReference"])).ToString());

				case "Action":
					return string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&AgentGuid={1}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{2}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SaveSmsSenderAgent, Session),
																e.CurrentRow["Guid"],
																Language.GetString("Edit")) +
								 string.Format(@"<a href=""/PageLoader.aspx?c={0}&AgentGuid={1}""><span class='ui-icon fa fa-usd red' title=""{2}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SmsSenderAgentRatio, Session),
																e.CurrentRow["Guid"],
																Language.GetString("Ratio")) +
								 string.Format(@"<a href=""/PageLoader.aspx?c={0}&AgentGuid={1}""><span class='ui-icon fa fa-arrows-alt purple' title=""{2}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_MessageRoute, Session),
																e.CurrentRow["Guid"],
																Language.GetString("Route"));
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
			gridAgents.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&ActionType=insert\" class=\"toolbarButton\">{1}</a>",
																								 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SaveSmsSenderAgent, Session),
																								 Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageSmsSenderAgent);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SmsSenderAgent;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsSenderAgents_SmsSenderAgent.ToString());
		}
	}
}