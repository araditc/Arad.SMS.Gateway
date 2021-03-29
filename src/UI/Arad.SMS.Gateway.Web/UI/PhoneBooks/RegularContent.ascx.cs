using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.PhoneBooks
{
	public partial class RegularContent : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		protected Guid PhoneBookGuid
		{
			get { return Helper.GetGuid(Helper.Request(this, "Guid").Trim('\'')); }
		}

		public RegularContent()
		{
			AddDataBinderHandlers("gridPhoneBookRegularContent", new DataBindHandler(gridPhoneBookRegularContent_OnDataBind));
			AddDataRenderHandlers("gridPhoneBookRegularContent", new CellValueRenderEventHandler(gridPhoneBookRegularContent_OnDataRender));
		}

		public DataTable gridPhoneBookRegularContent_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.PhoneBookRegularContent.GetRegularContents(PhoneBookGuid);
		}

		public string gridPhoneBookRegularContent_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Type":
					return Language.GetString(((RegularContentType)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());

				case "Action":
					return string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onclick=""deleteRegularContent(event);""></span>",
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
			gridPhoneBookRegularContent.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"#\" onClick=\"addRegularContent();\">{0}</a>", Language.GetString("New"));

			drpRegularContent.DataSource = Facade.RegularContent.GetRegularContent(UserGuid);
			drpRegularContent.DataValueField = "Guid";
			drpRegularContent.DataTextField = "Title";
			drpRegularContent.DataBind();
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.RegularContent);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_RegularContent;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PhoneBooks_RegularContent.ToString());
		}
	}
}