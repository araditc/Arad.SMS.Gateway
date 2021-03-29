using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.SmsTemplates
{
	public partial class LoadSmsTemplate : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public LoadSmsTemplate()
		{
			AddDataBinderHandlers("gridSmsTemplate", new DataBindHandler(gridSmsTemplate_OnDataBind));
			AddDataRenderHandlers("gridSmsTemplate", new CellValueRenderEventHandler(gridSmsTemplate_OnDataRender));
		}

		public DataTable gridSmsTemplate_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.SmsTemplate.GetPagedSmsTemplates(string.Empty, UserGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridSmsTemplate_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			//int resultCount = 0;
			//drpGroupTemplate.DataSource = Facade.GroupTemplate.GetPagedGroupTemplates(string.Empty, UserGuid, "[CreateDate]", 0, 0, ref resultCount);
			//drpGroupTemplate.DataTextField = "Title";
			//drpGroupTemplate.DataValueField = "Guid";
			//drpGroupTemplate.DataBind();

			//drpGroupTemplate.Attributes["onchange"] = "return showTemplateOfGroup();";
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			//List<int> permissions = new List<int>();
			//permissions.Add((int)Arad.SMS.Gateway.Business.Services.LoadSmsTemplate);
			//return permissions;

			return new List<int>();
		}

		protected override int GetUserControlID()
		{
			throw new NotImplementedException();
		}

		protected override string GetUserControlTitle()
		{
			throw new NotImplementedException();
		}
	}
}