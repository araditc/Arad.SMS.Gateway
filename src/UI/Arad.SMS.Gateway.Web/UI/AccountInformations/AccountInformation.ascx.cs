using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;

namespace Arad.SMS.Gateway.Web.UI.AccountInformations
{
	public partial class AccountInformation : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public AccountInformation()
		{
			AddDataBinderHandlers("gridAccountInformations", new DataBindHandler(gridAccountInformations_OnDataBind));
			AddDataRenderHandlers("gridAccountInformations", new CellValueRenderEventHandler(gridAccountInformations_OnDataRender));
		}

		public DataTable gridAccountInformations_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.AccountInformation.GetPagedAccountInformations(UserGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridAccountInformations_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Bank":
					return Language.GetString(Helper.GetString((Banks)Helper.GetInt(e.CurrentRow[sender.FieldName])));
				case "Action":
					return string.Format("<a href='{0}' class='btn btn-warning gridImageButton'>{1}</a>",
																string.Format("/PageLoader.aspx?c={0}&Guid={1}&Actiontype=edit",
																								GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_AccountInformations_SaveAccount, Session),
																								Helper.GetString(e.CurrentRow["Guid"])),
																 Language.GetString("Edit")) +

									string.Format("<a href='#' class='btn btn-danger gridImageButton' onclick='deleteRow(event);'>{0}</a>",
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
			gridAccountInformations.TopToolbarItems += string.Format("<a href=\"/PageLoader.aspx?c={0}&ActionType=insert\" class=\"btn btn-success toolbarButton\" >{1}</a>",
																															 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_AccountInformations_SaveAccount, Session), Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.AccountInformation);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_AccountInformations_AccountInformation;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_AccountInformations_AccountInformation.ToString());
		}
	}
}