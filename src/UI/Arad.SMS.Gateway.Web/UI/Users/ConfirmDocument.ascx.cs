using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.Users
{
	public partial class ConfirmDocument : UIUserControlBase
	{
		protected Guid UserGuid
		{
			get { return Helper.RequestGuid(this, "UserGuid"); }
		}

		public ConfirmDocument()
		{
			AddDataBinderHandlers("gridDocuments", new DataBindHandler(gridDocuments_OnDataBind));
			AddDataRenderHandlers("gridDocuments", new CellValueRenderEventHandler(gridDocuments_OnDataRender));
		}

		public DataTable gridDocuments_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.UserDocument.GetUserDocuments(UserGuid);
		}

		public string gridDocuments_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			string pattern = "<span onclick='{0}' class='{1}' title='{2}'/>";
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(pattern, "confirm(event);", "ui-icon fa fa-check green", Language.GetString("Confirm")) +
								 string.Format(pattern, "reject(event);", "ui-icon fa fa-times red", Language.GetString("Reject")) +
								 string.Format(pattern, "deleteRow(event);", "ui-icon fa fa-trash-o red", Language.GetString("Delete"));
				case "Document":
					return string.Format("<a href='/userdocument/{0}' target='_blank'>{1}</a>",
															 e.CurrentRow["Value"],
															 Language.GetString(((UserDocumentType)Helper.GetInt(e.CurrentRow["key"])).ToString()));
				case "Status":
					switch (Helper.GetInt(e.CurrentRow["Status"]))
					{
						case (int)UserDocumentStatus.Confirmed:
							return string.Format(pattern, string.Empty, "ui-icon fa fa-check green", Language.GetString("Confirmed"));
						case (int)UserDocumentStatus.Rejected:
							return string.Format(pattern, string.Empty, "ui-icon fa fa-times red", Language.GetString("Rejected"));
						case (int)UserDocumentStatus.Checking:
							return string.Format("<img src='/pic/arrowsloader.gif' title='{0}'></span>", Language.GetString("Checking"));
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
			gridDocuments.TopToolbarItems += string.Format("<a class=\"btn btn-default\" href=\"/PageLoader.aspx?c={0}\">{1}</a>",
																										 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_User, Session),
																										 Language.GetString("Cancel"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ConfirmUserDocument);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Users_ConfirmDocument;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Users_ConfirmDocument.ToString());
		}
	}
}