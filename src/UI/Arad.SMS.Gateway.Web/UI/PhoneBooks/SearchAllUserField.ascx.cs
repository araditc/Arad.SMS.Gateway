using System;
using System.Collections.Generic;
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;

namespace Arad.SMS.Gateway.Web.UI.PhoneBooks
{
	public partial class SearchAllUserField : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public SearchAllUserField()
		{
			AddDataBinderHandlers("gridAllUserFields", new DataBindHandler(gridFields_OnDataBind));
			AddDataRenderHandlers("gridAllUserFields", new CellValueRenderEventHandler(gridFields_OnDataRender));
		}

		public DataTable gridFields_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			string fieldName = Helper.ImportData(searchFiletrs, "FieldName");
			string phoneBookName = Helper.ImportData(searchFiletrs, "PhoneBookName");
			return Facade.UserField.GetPagedAllUserFields(UserGuid, fieldName, phoneBookName, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridFields_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "FieldType":
					return Language.GetString(((Business.UserFieldTypes)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SearchAllUserFields);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_SearchAllUserField;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PhoneBooks_SearchAllUserField.ToString());
		}
	}
}