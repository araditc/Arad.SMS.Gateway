using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Arad.SMS.Gateway.Web.UI.SmsParsers
{
	public partial class SelectedOption : UIUserControlBase
	{
		public Guid ParserGuid
		{
			get { return Helper.RequestGuid(this, "ParserGuid"); }
		}

		private string ReturnPath
		{
			get
			{
				switch (Helper.RequestInt(this, "ParserType"))
				{
					case (int)Arad.SMS.Gateway.Business.SmsParserType.Poll:
						return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsParsers_Polls_Poll, Session);
					case (int)Arad.SMS.Gateway.Business.SmsParserType.Competition:
						return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsParsers_Competitions_Competition, Session);
					default:
						return string.Empty;
				}
			}
		}

		public SelectedOption()
		{
			AddDataBinderHandlers("gridSelectedOption", new DataBindHandler(gridSelectedOption_OnDataBind));
			AddDataRenderHandlers("gridSelectedOption", new CellValueRenderEventHandler(gridSelectedOption_OnDataRender));
		}

		public DataTable gridSelectedOption_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			Guid formulaGuid = Guid.Empty;
			int lottery = 0;
			string sender = string.Empty;

			toolbarFilters = toolbarFilters == "[]" ? string.Empty : toolbarFilters;

			if (!string.IsNullOrEmpty(searchFiletrs))
			{
				JArray array = JArray.Parse(searchFiletrs);
				formulaGuid = Helper.GetGuid(array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "ParserFormulaGuid").FirstOrDefault().Property("data").Value.ToString());
				lottery = Helper.GetInt(array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "Lottery").FirstOrDefault().Property("data").Value.ToString());
			}

			if (!string.IsNullOrEmpty(toolbarFilters))
			{
				JArray array = JArray.Parse(toolbarFilters);
				sender = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "Sender").FirstOrDefault().Property("data").Value.ToString();
			}

			return Facade.Inbox.GetPagedParserSms(ParserGuid, formulaGuid, lottery, sender, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridSelectedOption_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			gridSelectedOption.TopToolbarItems = string.Format(@"<a href=""/PageLoader.aspx?c={0}"" class=""btn btn-default"" >{1}</a>",
																					 ReturnPath,
																					 Language.GetString("Return"));
			gridSelectedOption.TopToolbarItems += string.Format(@"<label>{0}</label><select id=""drpOptions"" onchange=""return search();""></select><label>قرعه کشی</label><input id=""txtLottery"" type=""text""></input><label>شماره</label>", Language.GetString("SelectOption"));
			gridSelectedOption.TopToolbarItems += string.Format(@"<a href=""#"" id=""btnSearch"" class=""btn btn-success"" >{1}</a>",
																						ReturnPath,
																						Language.GetString("Search"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			isOptionalPermissions = true;
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.Competition);
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.Poll);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsParsers_SelectedOption;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsParsers_SelectedOption.ToString());
		}
	}
}