using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.RegularContents
{
	public partial class Content : UIUserControlBase
	{
		private Guid RegularContentGuid
		{
			get { return Helper.RequestGuid(this, "RegularContentGuid"); }
		}

		public Content()
		{
			AddDataBinderHandlers("gridContent", new DataBindHandler(gridContent_OnDataBind));
			AddDataRenderHandlers("gridContent", new CellValueRenderEventHandler(gridContent_OnDataRender));
		}

		public DataTable gridContent_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.Content.GetPagedContents(RegularContentGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridContent_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			return string.Empty;
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			gridContent.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&RegularContentGuid={1}&ActionType=insert\">{2}</a>",
																		 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_RegularContents_SaveContent, Session),
																		 RegularContentGuid,
																		 Language.GetString("New")) +

																		string.Format("<a class=\"btn btn-danger\" href=\"#\" onclick=\"deleteMultipleNumber();\">{0}</a>",
																									 Language.GetString("Delete")) +

																		string.Format("<a class=\"btn btn-default\" href=\"/PageLoader.aspx?c={0}\">{1}</a>",
																		 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_RegularContents_RegularContent, Session),
																		 Language.GetString("Cancel"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.RegularContent);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_RegularContents_Content;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_RegularContents_Content.ToString());
		}
	}
}