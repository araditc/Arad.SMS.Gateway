using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.PrivateNumbers
{
	public partial class DefiningPrivateNumber : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public DefiningPrivateNumber()
		{
			AddDataBinderHandlers("gridPrivateNumbers", new DataBindHandler(gridPrivateNumbers_OnDataBind));
			AddDataRenderHandlers("gridPrivateNumbers", new CellValueRenderEventHandler(gridPrivateNumbers_OnDataRender));
		}

		public DataTable gridPrivateNumbers_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);
			return Facade.PrivateNumber.GetPagedNumbers(UserGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridPrivateNumbers_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&Guid={1}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{2}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_SavePrivateNumber, Session),
																e.CurrentRow["Guid"],
																Language.GetString("Edit"));
				case "Number":
					switch (Helper.GetInt(e.CurrentRow["UseForm"]))
					{
						case (int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.Mask:
						case (int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.OneNumber:
							return e.CurrentRow["Number"].ToString();
						case (int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.RangeNumber:
							return string.Format("{0}", e.CurrentRow["Range"]);
						default:
							return string.Empty;
					}

				case "Type":
					return Helper.GetString((Business.TypePrivateNumberAccesses)Helper.GetInt(e.CurrentRow["Type"]));
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
			gridPrivateNumbers.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&ActionType=insert\">{1}</a>",
																								 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_SavePrivateNumber, Session),
																								 Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.DefinePrivateNumber);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_DefiningPrivateNumber;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PrivateNumbers_DefiningPrivateNumber.ToString());
		}
	}
}