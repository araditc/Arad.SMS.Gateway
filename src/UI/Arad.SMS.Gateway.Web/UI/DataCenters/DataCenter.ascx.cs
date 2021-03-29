using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.DataCenters
{
	public partial class DataCenter : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public DataCenter()
		{
			AddDataBinderHandlers("gridDataCenters", new DataBindHandler(gridDataCenters_OnDataBind));
			AddDataRenderHandlers("gridDataCenters", new CellValueRenderEventHandler(gridDataCenters_OnDataRender));
		}

		public DataTable gridDataCenters_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.DataCenter.GetUserDataCenter(UserGuid, Business.DataCenterType.All);
		}

		public string gridDataCenters_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<a href=""/PageLoader.aspx?c={0}&DataCenterGuid={1}&Type={2}""><span class='ui-icon fa fa-plus green' title=""{3}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Data_Contents, Session),
																e.CurrentRow["Guid"],
																e.CurrentRow["Type"],
																Language.GetString("AddContent")) +


								 string.Format(@"<a href=""/PageLoader.aspx?c={0}&DataCenterGuid={1}""><span class='ui-icon fa fa-location-arrow' title=""{2}""></span></a>",
																	Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_DataCenters_SaveDataLocation, Session),
																	e.CurrentRow["Guid"],
																	Language.GetString("DefineLocation")) +


								 string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&Guid={1}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{2}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_DataCenters_SaveDataCenter, Session),
																e.CurrentRow["Guid"],
																Language.GetString("Edit")) +

								 string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteRow(event);""></span>",
																Language.GetString("Delete"));

				case "Type":
					return Language.GetString(((Business.DataCenterType)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());
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
			gridDataCenters.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&ActionType=insert\">{1}</a>",
																											Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_DataCenters_SaveDataCenter, Session),
																											Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.DataCenterList);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_DataCenters_DataCenter;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_DataCenters_DataCenter.ToString());
		}
	}
}