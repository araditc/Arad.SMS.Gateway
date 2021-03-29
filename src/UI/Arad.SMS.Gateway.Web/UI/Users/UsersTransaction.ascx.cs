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
	public partial class UsersTransaction : UIUserControlBase
	{
		protected Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public UsersTransaction()
		{
			AddDataBinderHandlers("gridUsersTransaction", new DataBindHandler(gridUsersTransaction_OnDataBind));
			AddDataRenderHandlers("gridUsersTransaction", new CellValueRenderEventHandler(gridUsersTransaction_OnDataRender));
		}

		public DataTable gridUsersTransaction_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			decimal totalCount = 0;
			Guid domainGuid = Guid.Empty;
			string username = string.Empty;

			if (!string.IsNullOrEmpty(toolbarFilters))
			{
				JArray array = JArray.Parse(toolbarFilters);
				JObject typeCreditChange = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "TypeCreditChange" && obj.Property("data").Value.ToString() == "0").FirstOrDefault();
				array.Remove(typeCreditChange);

				JObject typeTransaction = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "TypeTransaction" && obj.Property("data").Value.ToString() == "0").FirstOrDefault();
				array.Remove(typeTransaction);

				JObject usernameObj = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "UserName").FirstOrDefault();
				if (usernameObj != null)
				{
					username = usernameObj.Property("data").Value.ToString();
					array.Remove(usernameObj);
				}

				toolbarFilters = array.ToString();
			}

			if (!string.IsNullOrEmpty(searchFiletrs))
			{
				JArray array = JArray.Parse(searchFiletrs);
				domainGuid = Helper.GetGuid(array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "DomainGuid").FirstOrDefault().Property("data").Value);
				array.Remove(array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "DomainGuid").FirstOrDefault());
				searchFiletrs = array.ToString();
			}

			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);

			return Facade.Transaction.GetPagedUsersTransaction(UserGuid, domainGuid, username, query, sortField, pageNo, pageSize, ref resultCount,ref totalCount);
		}

		public string gridUsersTransaction_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
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
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}


		protected void Page_Load(object sender, EventArgs e)
		{
			InitializePage();
		}

		private void InitializePage()
		{
			gridUsersTransaction.TopToolbarItems += string.Format(@"<div style=""margin:1px;"">");
			gridUsersTransaction.TopToolbarItems += string.Format(@"<label>{0}</label><select id=""drpDomain"" style=""width:200px;""></select>", Language.GetString("Domain"));
			gridUsersTransaction.TopToolbarItems += string.Format(@"<a href=""#"" id=""btnSearch"" class=""btn btn-sm btn-success"" style=""margin-right:3px;margin-left:3px"" >{0}</a>", Language.GetString("Search"));
			gridUsersTransaction.TopToolbarItems += string.Format("</div>");
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.UsersTransactionList);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_Users_UsersTransaction;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_Users_UsersTransaction.ToString());
		}
	}
}