using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Arad.SMS.Gateway.Web.UI.Users
{
	public partial class Transaction : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid ReferenceGuid
		{
			get { return Helper.RequestEncryptedGuid(this, "ReferenceGuid"); }
		}

		public Transaction()
		{
			AddDataBinderHandlers("gridUserTransaction", new DataBindHandler(gridUserTransaction_OnDataBind));
			AddDataRenderHandlers("gridUserTransaction", new CellValueRenderEventHandler(gridUserTransaction_OnDataRender));
		}

		public DataTable gridUserTransaction_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			if (!string.IsNullOrEmpty(toolbarFilters))
			{
				JArray array = JArray.Parse(toolbarFilters);
				JObject typeCreditChange = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "TypeCreditChange" && obj.Property("data").Value.ToString() == "0").FirstOrDefault();
				array.Remove(typeCreditChange);

				JObject typeTransaction = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "TypeTransaction" && obj.Property("data").Value.ToString() == "0").FirstOrDefault();
				array.Remove(typeTransaction);

				toolbarFilters = array.ToString();
			}

			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);

			return Facade.Transaction.GetPagedUserTransaction(UserGuid, ReferenceGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridUserTransaction_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "TypeCreditChange":
					return Language.GetString(((TypeCreditChanges)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());

				case "TypeTransaction":
					if (e.CurrentRowGenerateType == RowGenerateType.Normal)
					{
						if (Helper.GetInt(e.CurrentRow[sender.FieldName]) == (int)TypeTransactions.Increase)
							return string.Format(@"<span class='ui-icon fa fa-arrow-up green' title=""{0}""></span>", Language.GetString("Increase"));
						else if (Helper.GetInt(e.CurrentRow[sender.FieldName]) == (int)Arad.SMS.Gateway.Business.TypeTransactions.Decrease)
							return string.Format(@"<span class='ui-icon fa fa-arrow-down red' title=""{0}""></span>", Language.GetString("Decrease"));
					}
					else
						return (Helper.GetInt(e.CurrentRow[sender.FieldName]) == (int)TypeTransactions.Increase) ? Language.GetString("Increase") : Language.GetString("Decrease");
					break;
				case "Action":
					string pattern = @"<a href=""/PageLoader.aspx?c={0}&ReferenceGuid={1}""><span class='{2}' title='{3}'></span></a>";
					switch (Helper.GetInt(e.CurrentRow["TypeCreditChange"]))
					{
						case (int)TypeCreditChanges.SendSms:
						case (int)TypeCreditChanges.GiveBackCostOfUnsuccessfulSent:
							return string.Format(pattern,
																 Helper.Encrypt((int)UserControls.UI_SmsReports_OutBox, Session),
																 Helper.Encrypt(e.CurrentRow["ReferenceGuid"], Session),
																 "ui-icon fa fa-2x fa-external-link blue",
																 Language.GetString("Details"));
						default:
							return string.Empty;
					}
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e) { }

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.TransactionList);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_Users_Transaction;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_Users_Transaction.ToString());
		}
	}
}