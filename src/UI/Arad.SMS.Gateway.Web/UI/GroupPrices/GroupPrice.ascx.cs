using System;
using System.Collections.Generic;
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;

namespace Arad.SMS.Gateway.Web.UI.GroupPrices
{
	public partial class GroupPrice : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public GroupPrice()
		{
			AddDataBinderHandlers("gridGroupPrice", new DataBindHandler(gridGroupPrice_OnDataBind));
			AddDataRenderHandlers("gridGroupPrice", new CellValueRenderEventHandler(gridGroupPrice_OnDataRender));
		}

		public DataTable gridGroupPrice_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.GroupPrice.GetPagedGroupPrices(UserGuid);
		}

		public string gridGroupPrice_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format("<a href='{0}' class='btn btn-warning gridImageButton' title={1}>{1}</a>",
																string.Format("/PageLoader.aspx?c={0}&ActionType=edit&Guid={1}",
																								Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_GroupPrices_SaveGroupPrice, Session),
																								e.CurrentRow["Guid"]),
																 Language.GetString("Edit")) +

								 string.Format("<a href='#' class='btn btn-danger gridImageButton' onclick='deleteRow(event);' title={0}>{0}</a>",
															 Language.GetString("Delete"));
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
			gridGroupPrice.TopToolbarItems += string.Format("<a href=\"/PageLoader.aspx?c={0}&ActionType=insert\" class=\"btn btn-success toolbarButton\" >{1}</a>",
																											Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_GroupPrices_SaveGroupPrice, Session), Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageGroupPrice);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_GroupPrices_GroupPrice;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_GroupPrices_GroupPrice.ToString());
		}
	}
}