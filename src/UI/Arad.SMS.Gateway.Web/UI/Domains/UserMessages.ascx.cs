using System;
using System.Collections.Generic;
using System.Data;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;

namespace MessagingSystem.UI.Domains
{
	public partial class UserMessages : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private string DomainName
		{
			get { return Helper.GetDomain(Request.Url.Authority); }
		}

		public UserMessages()
		{
			AddDataBinderHandlers("gridUserMessage", new GeneralTools.DataGrid.DataBindHandler(gridUserMessage_OnDataBind));
			AddDataRenderHandlers("gridUserMessage", new GeneralTools.DataGrid.CellValueRenderEventHandler(gridUserMessage_OnDataRender));
		}

		public DataTable gridUserMessage_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount)
		{
			return Facade.UserMessage.GetPagedUserMessages(DomainName, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridUserMessage_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<img src=""{0}"" onclick=""editRow(event);"" class=""gridImageButton"" title=""{1}"" />
																 <img src=""{2}"" onclick=""deleteRow(event);"" class=""gridImageButton"" title=""{3}"" />",
																																																		 "/pic/edit.png",
																																																		 Language.GetString("Edit"),
																																																		 "/pic/REJECTED.png",
																																																		 Language.GetString("Delete"));
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Business.Services.ManageUserMessage);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Business.UserControls.UI_Domains_UserMessages;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Domains_UserMessages.ToString());
		}
	}
}