using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.Domains
{
	public partial class Domain : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public bool IsMainAdmin
		{
			get { return Helper.GetBool(Session["IsMainAdmin"]); }
		}

		public Domain()
		{
			AddDataBinderHandlers("gridDomains", new DataBindHandler(gridDomains_OnDataBind));
			AddDataRenderHandlers("gridDomains", new CellValueRenderEventHandler(gridDomains_OnDataRender));
		}

		public DataTable gridDomains_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);
			return Facade.Domain.GetPagedDomains(UserGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridDomains_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Desktop":
					return Language.GetString(((Desktop)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());
				case "DefaultPage":
					return Language.GetString(((DefaultPages)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());
				case "Theme":
					return Language.GetString(((Theme)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());
				case "Action":
					return string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&Guid={1}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{2}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Domains_RegisterDomain, Session),
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
			if (!IsMainAdmin)
				return;

			gridDomains.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&ActionType=insert\">{1}</a>",
																									Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Domains_RegisterDomain, Session),
																									Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageDomain);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Domains_Domain;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Domains_Domain.ToString());
		}
	}
}
