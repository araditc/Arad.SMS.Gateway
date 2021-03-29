using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.RoleServices
{
	public partial class RoleService : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		protected Guid RoleGuid
		{
			get
			{
				return Helper.RequestGuid(this, "RoleGuid");
			}
		}

		private Guid ParentGuid
		{
			get { return Helper.GetGuid(Session["ParentGuid"]); }
		}

		public RoleService()
		{
			AddDataBinderHandlers("gridServices", new DataBindHandler(gridServices_OnDataBind));
			AddDataRenderHandlers("gridServices", new CellValueRenderEventHandler(gridServices_OnDataRender));
		}

		public DataTable gridServices_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			Guid roleGuid = RoleGuid;
			if (roleGuid == Guid.Empty)
				return new DataTable();
			else
				return Facade.RoleService.GetServiceOfRole(UserGuid, roleGuid);
		}

		public string gridServices_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Price":
					return Helper.FormatDecimalForDisplay(e.CurrentRow[sender.FieldName]);
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e) { }

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.DefineServiceOfRole);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_RoleServices_RoleService;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_RoleServices_RoleService.ToString());
		}
	}
}