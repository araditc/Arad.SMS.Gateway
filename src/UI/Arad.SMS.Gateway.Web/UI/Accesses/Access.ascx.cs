using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Collections.Generic;
using GeneralTools.DataGrid;

namespace Arad.SMS.Gateway.Web.UI.Accesses
{
	public partial class Access : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public Access()
		{
			AddDataBinderHandlers("gridAccesses", new DataBindHandler(gridAccesses_OnDataBind));
			AddDataRenderHandlers("gridAccesses", new CellValueRenderEventHandler(gridAccesses_OnDataRender));
		}

		public DataTable gridAccesses_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			Guid serviceGuid = Helper.GetGuid(Helper.ImportData(searchFiletrs, "ServiceGuid"));
			return Facade.Access.GetPagedAccess(serviceGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridAccesses_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"
																 <img src=""{0}"" onClick=""editRow(event);"" class=""gridImageButton"" title=""{1}""/>
																 <img src=""{2}"" onclick=""deleteRow(event);"" class=""gridImageButton"" title=""{3}""/>",
																																																		 "../Images/edit.png",
																																																		 Language.GetString("Edit"),
																																																		 "../Images/REJECTED.png",
																																																		 Language.GetString("Delete")
																																																		 );

				case "ReferencePermissionsKey":
					return Language.GetString(((Business.Permissions)e.CurrentRow[sender.FieldName]).ToString());
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
			int rowCount = 0;

			drpAdvanceSearchService.DataSource = Facade.Service.GetPagedService(string.Empty, "[CreateDate]", 0, 0, ref rowCount);
			drpAdvanceSearchService.DataTextField = "Title";
			drpAdvanceSearchService.DataValueField = "Guid";
			drpAdvanceSearchService.DataBind();

			gridAccesses.TopToolbarItems = string.Format("<input type=\"button\" value=\"{0}\" class=\"toolbarButton\" onclick=\"addNewAccess();\"/>", Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageAccess);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Accesses_Access;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Accesses_Access.ToString());
		}
	}
}