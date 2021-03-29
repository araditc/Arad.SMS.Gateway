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
	public partial class ConfirmFish : UIUserControlBase
	{
		private Guid ParentGuid
		{
			get { return Helper.GetGuid(Session["ParentGuid"]); }
		}

		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public ConfirmFish()
		{
			AddDataBinderHandlers("gridUserFishes", new DataBindHandler(gridUserFishes_OnDataBind));
			AddDataRenderHandlers("gridUserFishes", new CellValueRenderEventHandler(gridUserFishes_OnDataRender));
		}

		public DataTable gridUserFishes_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			int totalSmsCount = 0;
			decimal totalPrice = 0;

			if (!string.IsNullOrEmpty(toolbarFilters))
			{
				JArray array = JArray.Parse(toolbarFilters);
				JObject typeCreditChange = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "Status" && obj.Property("data").Value.ToString() == "0").FirstOrDefault();
				array.Remove(typeCreditChange);

				toolbarFilters = array.ToString();
			}

			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);
			DataTable dtFish = Facade.Fish.GetPagedFishesForConfirm(query, ParentGuid, UserGuid, sortField, pageNo, pageSize, ref resultCount, ref totalSmsCount, ref totalPrice);

			DataRow summaryRow = dtFish.NewRow();

			summaryRow["Amount"] = totalPrice;
			summaryRow["SmsCount"] = totalSmsCount;
			//summaryRow["PaymentDate"] = Language.GetString("Sum");
			summaryRow.RowError = "SummaryRow";
			dtFish.Rows.Add(summaryRow);

			return dtFish;
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
				case "Bank":
					if (Helper.GetInt(e.CurrentRow[sender.FieldName]) != 0)
						return string.Format(@"<img src='/pic/{0}.png' alt=''/>", (Banks)Helper.GetInt(e.CurrentRow[sender.FieldName]));
					else
						return string.Empty;
				case "Type":
					return Language.GetString(Helper.GetString((TypeFish)Helper.GetInt(e.CurrentRow[sender.FieldName])));

				case "Account":
					Business.Banks bank = (Banks)Enum.Parse(typeof(Banks), e.CurrentRow["Bank"].ToString());
					return string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}{1}{6}{1}{7}",
																									Language.GetString("Bank"),
																									Environment.NewLine,
																									Language.GetString(bank.ToString()),
																									Language.GetString("Account"),
																									e.CurrentRow["AccountNo"],
																									Language.GetString("CardNo"),
																									e.CurrentRow["CardNo"],
																									e.CurrentRow["Owner"]);

				case "Action":
					string imgTagPattern = "<span onclick='{0}' class='{1}' title='{2}' style='{3}'/>";
					string notActiveElementStyle = "opacity: .20;filter: Alpha(Opacity=20);float:right;";
					int status = Helper.GetInt(e.CurrentRow["Status"]);

					return string.Format(imgTagPattern, ((status != (int)FishStatus.Checking || Helper.GetInt(e.CurrentRow["Type"]) == (int)TypeFish.OnLine) ? string.Empty : "confirmFish(event);"),
																							"ui-icon fa fa-check green",
																							Language.GetString("Confirm"),
																							(status != (int)FishStatus.Checking || Helper.GetInt(e.CurrentRow["Type"]) == (int)TypeFish.OnLine) ? notActiveElementStyle : "float:right;") +
								string.Format(imgTagPattern, ((status != (int)FishStatus.Checking || Helper.GetInt(e.CurrentRow["Type"]) == (int)TypeFish.OnLine) ? string.Empty : "rejectFish(event);"),
																							"ui-icon fa fa-times red",
																							Language.GetString("Reject"),
																							(status != (int)FishStatus.Checking || Helper.GetInt(e.CurrentRow["Type"]) == (int)TypeFish.OnLine) ? notActiveElementStyle : "float:right;");
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e) { }

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ConfirmFish);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_UserFishes_ConfirmFish;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_UserFishes_ConfirmFish.ToString());
		}
	}
}