using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.PhoneBooks
{
	public partial class SearchAllGroups : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public SearchAllGroups()
		{
			AddDataBinderHandlers("gridAllNumbers", new DataBindHandler(gridAllNumbers_OnDataBind));
			AddDataRenderHandlers("gridAllNumbers", new CellValueRenderEventHandler(gridAllNumbers_OnDataRender));
		}

		public DataTable gridAllNumbers_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);
			return Facade.PhoneNumber.GetPagedAllNumbers(UserGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridAllNumbers_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Sex":
					if (Helper.GetInt(e.CurrentRow[sender.FieldName]) == 0)
						return string.Empty;
					else
						return Language.GetString(Helper.GetString((Business.Gender)Helper.GetInt(e.CurrentRow[sender.FieldName])));
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			//gridAllNumbers.TopToolbarItems += string.Format("<input type=\"button\" value=\"{0}\" class=\"btn btn-danger\" onclick=\"deleteMultipleNumber();\"/>", Language.GetString("Delete"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SearchAllPhoneBookGroups);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_SearchAllGroups;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PhoneBooks_SearchAllGroups.ToString());
		}
	}
}