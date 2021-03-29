using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using GeneralTools.DataGrid;

namespace Arad.SMS.Gateway.Web.UI.PrivateNumbers
{
	public partial class UserPrivateNumberKeywords : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public UserPrivateNumberKeywords()
		{
			AddDataBinderHandlers("gridKeywords", new DataBindHandler(gridKeywords_OnDataBind));
			AddDataRenderHandlers("gridKeywords", new CellValueRenderEventHandler(gridKeywords_OnDataRender));
		}

		public DataTable gridKeywords_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);
			return Facade.PrivateNumber.GetPagedAssignedKeywords(UserGuid,query, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridKeywords_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteRow(event);""></span>",
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
			gridKeywords.TopToolbarItems += string.Format("<a href=\"/PageLoader.aspx?c={0}\" class=\"btn btn-default toolbarButton\" >{1}</a>",
																										Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_NumberStatus, Session), Language.GetString("Cancel"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManagePrivateNumber);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_UserPrivateNumberKeywords;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PrivateNumbers_UserPrivateNumberKeywords.ToString());
		}
	}
}