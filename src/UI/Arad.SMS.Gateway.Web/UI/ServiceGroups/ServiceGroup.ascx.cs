using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.ServiceGroups
{
	public partial class ServiceGroup : UIUserControlBase
	{
		public ServiceGroup()
		{
			AddDataBinderHandlers("gridServiceGroup", new DataBindHandler(gridServiceGroup_OnDataBind));
			AddDataRenderHandlers("gridServiceGroup", new CellValueRenderEventHandler(gridServiceGroup_OnDataRender));
		}

		public DataTable gridServiceGroup_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			Common.ServiceGroup serviceGroup = new Common.ServiceGroup();

			string title = Helper.ImportData(searchFiletrs, "Title");
			return Facade.ServiceGroup.GetPagedServiceGroup(title, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridServiceGroup_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&Guid={1}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{2}""></span></a>",
											Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_ServiceGroups_SaveServiceGroup, Session),
											e.CurrentRow["Guid"],
											Language.GetString("Edit")) +
								 string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteRow(event);""></span>",
																Language.GetString("Delete"));

				case "IconAddress":
					if (Helper.GetString(e.CurrentRow[sender.FieldName]) != string.Empty)
						return string.Format(@"<span class=""{0}"" ></span>", Helper.GetString(e.CurrentRow[sender.FieldName]));
					else
						return string.Empty;
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				InitializePage();
			}
		}

		private void InitializePage()
		{
			gridServiceGroup.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&ActionType=insert\">{1}</a>",
																											 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_ServiceGroups_SaveServiceGroup, Session),
																											 Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageServiceGroup);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_ServiceGroups_ServiceGroup;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_ServiceGroups_ServiceGroup.ToString());
		}
	}
}