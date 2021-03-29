using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.PrivateNumbers
{
	public partial class AssignPrivateNumberToUsers : UIUserControlBase
	{
		private Guid ParentGuid
		{
			get { return Helper.RequestGuid(this, "ParentGuid"); }
		}

		private Guid UserGuid
		{
			get { return Helper.RequestGuid(this, "UserGuid"); }
		}

		private Guid UserNumberGuid
		{
			get { return Helper.GetGuid(ViewState["UserNumberGuid"]); }
			set { ViewState["UserNumberGuid"] = value; }
		}

		public AssignPrivateNumberToUsers()
		{
			AddDataBinderHandlers("gridUserPrivateNumbers", new DataBindHandler(gridUserPrivateNumbers_OnDataBind));
			AddDataRenderHandlers("gridUserPrivateNumbers", new CellValueRenderEventHandler(gridUserPrivateNumbers_OnDataRender));
		}

		public DataTable gridUserPrivateNumbers_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.PrivateNumber.GetUserNumbers(UserGuid, string.Empty, 0, 0, "[CreateDate]", ref resultCount);
		}

		public string gridUserPrivateNumbers_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			string action = string.Empty;
			switch (sender.FieldName)
			{
				case "Action":
					action += string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteRow(event);""></span>",
																	 Language.GetString("Delete"));
					return action;
				case "Type":
					return string.Format(Language.GetString(Helper.GetString((TypePrivateNumberAccesses)e.CurrentRow["Type"])));
				case "Number":
					switch (Helper.GetInt(e.CurrentRow["UseForm"]))
					{
						case (int)PrivateNumberUseForm.Mask:
						case (int)PrivateNumberUseForm.OneNumber:
							return e.CurrentRow["Number"].ToString();
						case (int)PrivateNumberUseForm.RangeNumber:
							return string.Format("{0}", e.CurrentRow["Range"]);
						default:
							return string.Empty;
					}
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
			gridUserPrivateNumbers.TopToolbarItems = string.Format("<a href=\"/PageLoader.aspx?c={0}&UserGuid={1}&ParentGuid={2}\" class=\"btn btn-success toolbarButton\">{3}</a>",
																														 Helper.Encrypt((int)UserControls.UI_PrivateNumbers_SaveUserPrivateNumber, Session),
																														 UserGuid,
																														 ParentGuid,
																														 Language.GetString("New"));
			gridUserPrivateNumbers.TopToolbarItems += string.Format("<a href=\"/PageLoader.aspx?c={0}\" class=\"btn btn-default toolbarButton\">{1}</a>",
																											 Helper.Encrypt((int)UserControls.UI_Users_User, Session),
																											 Language.GetString("Cancel"));

			//if (IsModal)
			//	gridUserPrivateNumbers.ListHeight = 320;
			//else
			//	gridUserPrivateNumbers.ListDifferenceHeight = 230;
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.AssignPrivateNumberToUser);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_PrivateNumbers_AssignPrivateNumberToUsers;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_PrivateNumbers_AssignPrivateNumberToUsers.ToString());
		}
	}
}