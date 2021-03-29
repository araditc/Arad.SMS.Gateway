using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Arad.SMS.Gateway.Web.UI.Services
{
	public partial class Service : UIUserControlBase
	{
		public Service()
		{
			AddDataBinderHandlers("gridServices", new DataBindHandler(gridServices_OnDataBind));
			AddDataRenderHandlers("gridServices", new CellValueRenderEventHandler(gridServices_OnDataRender));
		}

		public DataTable gridServices_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			if (!string.IsNullOrEmpty(toolbarFilters))
			{
				JArray array = JArray.Parse(toolbarFilters);
				JObject group = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "MenuTitle" && obj.Property("data").Value.ToString() == "0").FirstOrDefault();
				array.Remove(group);

				JObject menu = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "MenuTitle").FirstOrDefault();
				JObject title = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "Title").FirstOrDefault();
				if (menu != null)
					menu.Property("field").Value = "ServiceGroupGuid";
				if (title != null)
					title.Property("field").Value = "srvc.[Title]";

				toolbarFilters = array.ToString();
			}

			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);

			return Facade.Service.GetPagedService(query, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridServices_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&Guid={1}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{2}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Services_SaveService, Session),
																e.CurrentRow["Guid"],
																Language.GetString("Edit")) +

								 string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteRow(event);""></span>",
																Language.GetString("Delete"));

				case "IconAddress":
					return string.Format(@"<span class=""{0}"" ></span>", Helper.GetString(e.CurrentRow[sender.FieldName]));

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
			gridServices.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&ActionType=insert\">{1}</a>",
																									 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Services_SaveService, Session),
																									 Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			if (!Helper.GetBool(Session["IsSuperAdmin"]))
				permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageService);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Services_Service;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Services_Service.ToString());
		}
	}
}
