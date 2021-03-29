using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.PhoneBooks
{

	public partial class SearchAllFormats : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public SearchAllFormats()
		{
			AddDataBinderHandlers("gridAllFormats", new DataBindHandler(gridFormats_OnDataBind));
			AddDataRenderHandlers("gridAllFormats", new CellValueRenderEventHandler(gridFormats_OnDataRender));
		}

		public DataTable gridFormats_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			string formatName = Helper.ImportData(searchFiletrs,"FormatName");
			string phoneBookName = Helper.ImportData(searchFiletrs,"PhoneBookName");
			return Facade.SmsFormat.GetPagedAllSmsFormats(UserGuid,formatName,phoneBookName,sortField,pageNo,pageSize, ref resultCount);
		}

		public string gridFormats_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<span class=""ui-icon fa fa-trash-o red"" title=""{0}"" onClick=""deleteFormat(event);""></span>",Language.GetString("Delete"));
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManagePhoneNumber);
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.AddFileNumber);

			isOptionalPermissions = true;

			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_PhoneBooks_SearchAllFormats;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_PhoneBooks_SearchAllFormats.ToString());
		}
	}
}