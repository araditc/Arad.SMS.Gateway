using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Arad.SMS.Gateway.Web.UI.UserFishes
{
	public partial class UserFish : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public UserFish()
		{
			AddDataBinderHandlers("gridUserFishes", new DataBindHandler(gridUserFishes_OnDataBind));
			AddDataRenderHandlers("gridUserFishes", new CellValueRenderEventHandler(gridUserFishes_OnDataRender));
		}

		public DataTable gridUserFishes_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			if (!string.IsNullOrEmpty(toolbarFilters))
			{
				JArray array = JArray.Parse(toolbarFilters);
				JObject typeCreditChange = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "Status" && obj.Property("data").Value.ToString() == "0").FirstOrDefault();
				array.Remove(typeCreditChange);

				toolbarFilters = array.ToString();
			}

			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);

			Common.Fish fish = new Common.Fish();
			fish.UserGuid = UserGuid;
			return Facade.Fish.GetPagedUserFishes(UserGuid,query, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridUserFishes_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Status":
					if (Helper.GetInt(e.CurrentRow[sender.FieldName]) == (int)FishStatus.Checking)
						return string.Format(@"<img src=""{0}"" title=""{1}"" />", "/pic/indicator.gif", Language.GetString("Checking"));
					else if (Helper.GetInt(e.CurrentRow[sender.FieldName]) == (int)FishStatus.Confirmed)
						return string.Format(@"<img src=""{0}"" title=""{1}"" />", "/pic/FINISHED.png", Language.GetString("Confirmed"));
					else if (Helper.GetInt(e.CurrentRow[sender.FieldName]) == (int)FishStatus.Rejected)
						return string.Format(@"<img src=""{0}"" title=""{1}"" />", "/pic/REJECTED.png", Language.GetString("Rejected"));
					else
						return Helper.GetString(e.CurrentRow[sender.FieldName]);

				case "Type":
					return Language.GetString(Helper.GetString((TypeFish)Helper.GetInt(e.CurrentRow[sender.FieldName])));

				case "Account":
					Business.Banks bank = (Banks)Enum.Parse(typeof(Banks), e.CurrentRow["Bank"].ToString());
					return string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}{1}{6}{1}{7}",
																									Language.GetString("Bank"),
																									" ",
																									Language.GetString(bank.ToString()),
																									Language.GetString("Account"),
																									e.CurrentRow["AccountNo"],
																									Language.GetString("CardNo"),
																									e.CurrentRow["CardNo"],
																									e.CurrentRow["Owner"]);
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e) { }

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.UserFish);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_UserFishes_UserFish;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_UserFishes_UserFish.ToString());
		}
	}
}