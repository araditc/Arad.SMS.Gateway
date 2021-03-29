using System;
using System.Collections.Generic;
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;

namespace Arad.SMS.Gateway.Web.UI.UserServices
{
	public partial class BuyService : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public BuyService()
		{
			AddDataBinderHandlers("gridServices", new DataBindHandler(gridServices_OnDataBind));
			AddDataRenderHandlers("gridServices", new CellValueRenderEventHandler(gridServices_OnDataRender));
			AddDataBinderHandlers("gridGeneralPhoneBooks", new DataBindHandler(gridGeneralPhoneBooks_OnDataBind));
			AddDataRenderHandlers("gridGeneralPhoneBooks", new CellValueRenderEventHandler(gridGeneralPhoneBooks_OnDataRender));
		}

		public DataTable gridServices_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			if (UserGuid == Guid.Empty)
				return new DataTable();
			else
				return Facade.User.GetServiceOfUserRole(UserGuid);
		}

		public string gridServices_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Price":
					return Helper.FormatDecimalForDisplay(e.CurrentRow[sender.FieldName]);
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		public DataTable gridGeneralPhoneBooks_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			if (UserGuid == Guid.Empty)
				return new DataTable();
			else
				return Facade.User.GetGeneralPhoneBookOfUserRole(UserGuid);
		}

		public string gridGeneralPhoneBooks_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Price":
					return Helper.FormatDecimalForDisplay(e.CurrentRow[sender.FieldName]);
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			hdnUserGuid.Value = Helper.Encrypt(UserGuid, Session);
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			return new List<int>();
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_UserServices_BuyService;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Helper.GetString(Business.UserControls.UI_UserServices_BuyService));
		}
	}
}