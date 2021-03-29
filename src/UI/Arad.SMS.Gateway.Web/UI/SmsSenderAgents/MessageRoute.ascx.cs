using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.SmsSenderAgents
{
	public partial class MessageRoute : UIUserControlBase
	{
		public Guid AgentGuid
		{
			get
			{
				return Helper.RequestGuid(this, "AgentGuid");
			}
		}

		public MessageRoute()
		{
			AddDataBinderHandlers("gridRoutes", new DataBindHandler(gridRoutes_OnDataBind));
			AddDataRenderHandlers("gridRoutes", new CellValueRenderEventHandler(gridRoutes_OnDataRender));
		}

		public DataTable gridRoutes_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.Route.GetPagedRoutes(AgentGuid);
		}

		public string gridRoutes_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&RouteGuid={1}&SmsAgentGuid={2}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{3}""></span></a>",
																	Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SaveRoute, Session),
																	e.CurrentRow["Guid"],
																	AgentGuid,
																	Language.GetString("Edit"));
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitilizePage();
		}

		private void InitilizePage()
		{
			gridRoutes.TopToolbarItems = string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=insert&SmsAgentGuid={1}"" class=""btn btn-success"" >{2}</a>",
																	 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SaveRoute, Session),
																	 AgentGuid,
																	 Language.GetString("New"));

			gridRoutes.TopToolbarItems += string.Format(@"<a href=""/PageLoader.aspx?c={0}"" class=""btn btn-default"" >{1}</a>",
																		Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SmsSenderAgent, Session),
																		Language.GetString("Return"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.MessageRoute);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_MessageRoute;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsSenderAgents_MessageRoute.ToString());
		}
	}
}