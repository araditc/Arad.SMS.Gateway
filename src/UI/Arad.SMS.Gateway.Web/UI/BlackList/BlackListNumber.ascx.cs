using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Arad.SMS.Gateway.Web.UI.BlackList
{
	public partial class BlackListNumber : UIUserControlBase
	{
		public BlackListNumber()
		{
			AddDataBinderHandlers("gridNumbers", new DataBindHandler(gridNumbers_OnDataBind));
			AddDataRenderHandlers("gridNumbers", new CellValueRenderEventHandler(gridNumbers_OnDataRender));
		}

		public DataTable gridNumbers_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			if (!string.IsNullOrEmpty(toolbarFilters))
			{
				JArray array = JArray.Parse(toolbarFilters);
				JObject number = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "Number" && obj.Property("data").Value.ToString() == string.Empty).FirstOrDefault();
				array.Remove(number);

				toolbarFilters = array.ToString();
			}

			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);
			return Facade.PersonsInfo.GetPagedBlackListNumbers(query, pageNo, pageSize, sortField, ref resultCount);
		}

		public string gridNumbers_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteNumber(event);""></span>",
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
			gridNumbers.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&ActionType=insert\">{1}</a>",
																									 Helper.Encrypt((int)UserControls.UI_BlackList_SaveNumber, Session),
																									 Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageBlackListNumber);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_BlackList_BlackListNumber;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_BlackList_BlackListNumber.ToString());
		}
	}
}