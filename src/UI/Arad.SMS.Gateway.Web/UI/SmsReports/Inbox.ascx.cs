using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using Arad.SMS.Gateway.Business;

namespace Arad.SMS.Gateway.Web.UI.SmsReports
{
	public partial class Inbox : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid InboxGroupGuid
		{
			get { return Helper.GetGuid(Helper.Request(this, "GroupGuid").Trim('\'')); }
		}

		public Inbox()
		{
			AddDataBinderHandlers("gridInbox", new DataBindHandler(gridInbox_OnDataBind));
			AddDataRenderHandlers("gridInbox", new CellValueRenderEventHandler(gridInbox_OnDataRender));
		}

		public DataTable gridInbox_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			if (!string.IsNullOrEmpty(searchFiletrs) && !string.IsNullOrEmpty(toolbarFilters))
			{
				JArray array = JArray.Parse(searchFiletrs);
				JObject receiveDateTime = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "ReceiveDateTime" &&
																																	obj.Property("data").Value.ToString() != string.Empty).FirstOrDefault();
				if (receiveDateTime != null)
				{
					array = JArray.Parse(toolbarFilters);
					receiveDateTime = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "ReceiveDateTime").FirstOrDefault();
					array.Remove(receiveDateTime);
					toolbarFilters = array.ToString();
				}
			}

			string query = string.Empty;
			string filterQuery = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);
			string searchQuery = Helper.GenerateQueryFromToolbarFilters(searchFiletrs);

			query = string.Format("{0} {1} {2}",
													filterQuery,
													(!string.IsNullOrEmpty(filterQuery) && !string.IsNullOrEmpty(searchQuery)) ? "AND" : string.Empty,
													searchQuery);

			return Facade.Inbox.GetPagedSmses(UserGuid, InboxGroupGuid, query, pageNo, pageSize, sortField, ref resultCount);
		}

		public string gridInbox_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			string imgTagPattern = @"<a href=""/PageLoader.aspx?c={0}&ReferenceGuid={1}&MasterPage=inbox""><span class='{2}' title='{3}' style='margin:0 5px 0 0;'></span></a>";

			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<span class='ui-icon fa fa-list' title=""{0}"" onClick=""sendToCategory(event);""></span>",
																 Language.GetString("SendToCategory")) +

								 string.Format(imgTagPattern,
															 Helper.Encrypt((int)UserControls.UI_SmsSends_SendSms, Session),
															 Helper.Encrypt(e.CurrentRow["Guid"], Session),
															 "ui-icon fa fa-2x fa-share purple",
															 Language.GetString("SendToOther")) +

								 string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteRow(event);""></span>",
															 Language.GetString("Delete"));
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
			drpInboxGroups.DataSource = Facade.InboxGroup.GetUserInboxGroups(UserGuid);
			drpInboxGroups.DataTextField = "Title";
			drpInboxGroups.DataValueField = "Guid";
			drpInboxGroups.DataBind();
			drpInboxGroups.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			gridInbox.TopToolbarItems = string.Format("<a class=\"btn btn-danger btn-sm toolbarButton\" href=\"#\" onclick=\"deleteRows();\">{0}</a>", Language.GetString("Delete"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.Inbox);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_Inbox;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Helper.GetString((Business.UserControls.UI_SmsReports_Inbox)));
		}
	}
}