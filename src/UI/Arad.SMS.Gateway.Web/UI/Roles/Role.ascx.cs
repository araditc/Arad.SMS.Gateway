using System;
using System.Collections.Generic;
using System.Data;
using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;

namespace Arad.SMS.Gateway.Web.UI.Roles
{
	public partial class Role : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public Role()
		{
			AddDataBinderHandlers("gridRoles", new DataBindHandler(gridRole_OnDataBind));
			AddDataRenderHandlers("gridRoles", new CellValueRenderEventHandler(gridRole_OnDataRender));
		}

		public DataTable gridRole_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.Role.GetRoles(UserGuid);
		}

		public string gridRole_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<a href=""/PageLoader.aspx?c={0}&RoleGuid={1}""><span class='ui-icon fa fa-shield orange' title=""{2}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_RoleServices_RoleService, Session),
																e.CurrentRow["Guid"],
																Language.GetString("AssignServiceToRole")) +

								string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=Edit&RoleGuid={1}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{2}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Roles_SaveRole, Session),
																e.CurrentRow["Guid"],
																Language.GetString("Edit")) +

								 string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteRow(event);""></span>",
																Language.GetString("Delete"));

			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			InitializePage();
		}

		private void InitializePage()
		{
			gridRoles.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&ActionType=insert\">{1}</a>",
																								 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Roles_SaveRole, Session),
																								 Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.Role);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_Roles_Role;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_Roles_Role.ToString());
		}
	}
}