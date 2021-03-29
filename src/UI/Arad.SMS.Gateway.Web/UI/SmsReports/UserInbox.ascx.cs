using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.SmsReports
{
	public partial class UserInbox : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public UserInbox()
		{
			AddDataBinderHandlers("gridUsersInbox", new DataBindHandler(gridUsersInbox_OnDataBind));
			AddDataRenderHandlers("gridUsersInbox", new CellValueRenderEventHandler(gridUsersInbox_OnDataRender));
		}

		public DataTable gridUsersInbox_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			string query =  Helper.GenerateQueryFromToolbarFilters(toolbarFilters);

			return Facade.Inbox.GetPagedUserSmses(UserGuid, query, pageNo, pageSize, sortField, ref resultCount);
		}

		public string gridUsersInbox_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.UserInbox);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_SmsReports_UserInbox;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_SmsReports_UserInbox.ToString());
		}
	}
}