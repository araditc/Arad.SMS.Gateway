using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.SmsTemplates
{
	public partial class SmsTemplate : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid GroupGuid
		{
			get { return Helper.RequestGuid(this, "GroupGuid"); }
		}

		public SmsTemplate()
		{
			AddDataBinderHandlers("gridSmsTemplate", new DataBindHandler(gridSmsTemplate_OnDataBind));
			AddDataRenderHandlers("gridSmsTemplate", new CellValueRenderEventHandler(gridSmsTemplate_OnDataRender));
		}

		public DataTable gridSmsTemplate_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			string body = Helper.ImportData(searchFiletrs, "Body");
			return Facade.SmsTemplate.GetPagedSmsTemplates(body, UserGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridSmsTemplate_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&Guid={1}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{2}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsTemplates_SaveSmsTemplate, Session),
																e.CurrentRow["Guid"],
																Language.GetString("Edit")) +

								 string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteRow(event);""></span>",
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
			gridSmsTemplate.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&ActionType=insert\">{1}</a>",
																											 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsTemplates_SaveSmsTemplate, Session)
																											 , Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permission = new List<int>();
			permission.Add((int)Arad.SMS.Gateway.Business.Services.SmsTemplateList);
			return permission;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsTemplates_SmsTemplate;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsTemplates_SmsTemplate.ToString());
		}
	}
}