using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using GeneralTools.DataGrid;

namespace Arad.SMS.Gateway.Web.UI.Settings
{
	public partial class Setting : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public Setting()
		{
			AddDataBinderHandlers("gridSettings", new DataBindHandler(gridSettings_OnDataBind));
			AddDataRenderHandlers("gridSettings", new CellValueRenderEventHandler(gridSettings_OnDataRender));
		}

		public DataTable gridSettings_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.Setting.GetSettings(Guid.Empty);
		}

		public string gridSettings_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Key":
					return Language.GetString(((Business.MainSettings)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());

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
			gridSettings.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}\">{1}</a>",
																									 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Settings_SaveSetting, Session),
																									 Language.GetString("Register"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.MainSetting);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Settings_Setting;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Settings_Setting.ToString());
		}
	}
}