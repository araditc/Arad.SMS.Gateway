using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.Data
{
	public partial class Contents : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid DataCenterGuid
		{
			get { return Helper.RequestGuid(this, "DataCenterGuid"); }
		}

		private int Type
		{
			get { return Helper.RequestInt(this, "Type"); }
		}

		public Contents()
		{
			AddDataBinderHandlers("gridData", new DataBindHandler(gridData_OnDataBind));
			AddDataRenderHandlers("gridData", new CellValueRenderEventHandler(gridData_OnDataRender));
		}

		public DataTable gridData_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.Data.GetPagedData(UserGuid, DataCenterGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridData_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<span class='ui-icon fa fa-play-circle-o green' title=""{0}"" onClick=""activeData(event);""></span>",
																Language.GetString("Active")) +

								 string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&Guid={1}&DataCenterGuid={2}&Type={3}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{4}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Data_SaveContent, Session),
																e.CurrentRow["Guid"],
																DataCenterGuid,
																Type,
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
			gridData.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&DataCenterGuid={1}&ActionType=insert&Type={2}\">{3}</a>",
																								Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Data_SaveContent, Session),
																								DataCenterGuid,
																								Type,
																								Language.GetString("New"));

			gridData.TopToolbarItems += string.Format("<a class=\"btn btn-default\" href=\"/PageLoader.aspx?c={0}\">{1}</a>",
																								Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_DataCenters_DataCenter, Session),
																								Language.GetString("Cancel"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ContentList);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Data_Contents;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Data_Contents.ToString());
		}
	}
}