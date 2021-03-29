using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.Settings
{
	public partial class FilterWord : UIUserControlBase
	{
		public FilterWord()
		{
			AddDataBinderHandlers("gridFilterWord", new DataBindHandler(gridFilterWord_OnDataBind));
			AddDataRenderHandlers("gridFilterWord", new CellValueRenderEventHandler(gridFilterWord_OnDataRender));
		}

		public DataTable gridFilterWord_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.FilterWord.GetWords();
		}

		public string gridFilterWord_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
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
			gridFilterWord.TopToolbarItems += string.Format(@"<div style=""margin:1px;"">");
			gridFilterWord.TopToolbarItems += string.Format(@"</label><input id=""txtWord"" type=""text"" style=""width:300px;height:30px""></input>");
			gridFilterWord.TopToolbarItems += string.Format(@"<a id=""btnAdd"" href=""#"" onclick=""saveFilterWord();"" class=""btn btn-sm btn-success"" style=""margin-right:3px;margin-left:3px"" >{0}</a>", Language.GetString("Add"));
			gridFilterWord.TopToolbarItems += string.Format("</div>");
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.FilterWords);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Settings_FilterWord;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Settings_FilterWord.ToString());
		}
	}
}