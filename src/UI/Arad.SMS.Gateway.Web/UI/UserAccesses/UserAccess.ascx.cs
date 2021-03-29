using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;

namespace Arad.SMS.Gateway.Web.UI.UserAccesses
{
	public partial class UserAccess : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.RequestEncryptedGuid(this, "UserGuid"); }
		}

		private Guid ParentGuid
		{
			get { return Helper.RequestEncryptedGuid(this, "ParentGuid"); }
		}

		public UserAccess()
		{
			AddDataBinderHandlers("gridUserAccesses", new DataBindHandler(gridUserAccesses_OnDataBind));
			AddDataRenderHandlers("gridUserAccesses", new CellValueRenderEventHandler(gridUserAccesses_OnDataRender));
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{ }
		}

		public DataTable gridUserAccesses_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.User.GetAccessOfUser(ParentGuid, UserGuid);
		}

		public string gridUserAccesses_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "AccessTitle":
					return Language.GetString(((Business.Permissions)Helper.GetInt(e.CurrentRow["ReferencePermissionsKey"])).ToString());
			}

			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageUser);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_UserAccesses_UserAccess;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_UserAccesses_UserAccess.ToString());
		}
	}
}