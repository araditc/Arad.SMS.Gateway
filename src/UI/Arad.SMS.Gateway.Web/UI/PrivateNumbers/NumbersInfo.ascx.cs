using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.GeneralLibrary.Security;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.PrivateNumbers
{
	public partial class NumbersInfo : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public NumbersInfo()
		{
			AddDataBinderHandlers("gridUserPrivateNumbers", new DataBindHandler(gridUserPrivateNumbers_OnDataBind));
			AddDataRenderHandlers("gridUserPrivateNumbers", new CellValueRenderEventHandler(gridUserPrivateNumbers_OnDataRender));
		}

		public DataTable gridUserPrivateNumbers_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);
			return Facade.PrivateNumber.GetPagedAllAssignedLine(query, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridUserPrivateNumbers_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
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

				case "Action":
					return string.Format(@"<a href='{0}'><span class='ui-icon {1}' title='{2}'></span></a>",
																	SecurityManager.HasServicePermission(UserGuid, (int)Arad.SMS.Gateway.Business.Services.ViewUserEditProfile) ? "/PageLoader.aspx?c=" + Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_EditProfile, Session) + "&UserGuid=" + e.CurrentRow["UserGuid"] + "&EditUser=1&ReadOnly=1" : "#",
																	"fa fa-info-circle blue",
																	Language.GetString("UserInfo"));
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.PrivateNumbersInfo);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_NumbersInfo;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PrivateNumbers_NumbersInfo.ToString());
		}
	}
}