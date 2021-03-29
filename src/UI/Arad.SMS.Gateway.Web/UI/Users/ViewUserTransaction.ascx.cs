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
	public partial class ViewUserTransaction : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.RequestGuid(this, "UserGuid"); }
		}

		private bool IsMainAdmin
		{
			get { return Helper.GetBool(Session["IsMainAdmin"]); }
		}

		public ViewUserTransaction()
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

			return Facade.Transaction.GetPagedUserTransaction(UserGuid, Guid.Empty, query, sortField, pageNo, pageSize, ref resultCount);
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
						else if (Helper.GetInt(e.CurrentRow[sender.FieldName]) == (int)TypeTransactions.Decrease)
							return string.Format(@"<span class='ui-icon fa fa-arrow-down red' title=""{0}""></span>", Language.GetString("Decrease"));
					}
					else
						return (Helper.GetInt(e.CurrentRow[sender.FieldName]) == (int)Arad.SMS.Gateway.Business.TypeTransactions.Increase) ? Language.GetString("Increase") : Language.GetString("Decrease");
					break;
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
			btnSave.Text = Language.GetString(btnSave.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (Helper.GetInt(drpType.SelectedValue) != 0)
				{
					if (!Facade.Transaction.ChangeCreditByManage(UserGuid, IsMainAdmin, (TypeTransactions)Helper.GetInt(drpType.SelectedValue), Helper.GetDecimal(txtAmount.Text), txtDescription.Text))
						throw new Exception(Language.GetString("ErrorRecord"));

					ClientSideScript = string.Format("result('OK','{0}')", Language.GetString("InsertRecord"));
				}
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("result('error','{0}')", ex.Message);
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)UserControls.UI_Users_User, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ViewUserTransaction);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Users_ViewUserTransaction;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Users_Transaction.ToString());
		}
	}
}