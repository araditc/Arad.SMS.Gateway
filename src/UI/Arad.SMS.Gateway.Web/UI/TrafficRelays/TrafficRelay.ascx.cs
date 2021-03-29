using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using GeneralTools.DataGrid;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.TrafficRelays
{
	public partial class TrafficRelay : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public TrafficRelay()
		{
			AddDataBinderHandlers("gridUrls", new DataBindHandler(gridUrls_OnDataBind));
			AddDataRenderHandlers("gridUrls", new CellValueRenderEventHandler(gridUrls_OnDataRender));
		}

		public DataTable gridUrls_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.TrafficRelay.GetPagedTrafficRelays(UserGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridUrls_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&UrlGuid={1}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{2}""></span></a>",
											Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_TrafficRelays_SaveTrafficRelay, Session),
											e.CurrentRow["Guid"],
											Language.GetString("Edit")) +

								 string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteRow(event);""></span>",
											Language.GetString("Delete"));

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
			gridUrls.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&ActionType=insert\">{1}</a>",
																 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_TrafficRelays_SaveTrafficRelay, Session),
																 Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.TrafficRelay);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_TrafficRelays_TrafficRelay;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_TrafficRelays_TrafficRelay.ToString());
		}
	}
}