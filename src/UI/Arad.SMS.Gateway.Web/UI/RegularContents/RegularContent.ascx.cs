using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.RegularContents
{
	public partial class RegularContent : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public RegularContent()
		{
			AddDataBinderHandlers("gridRegularContent", new DataBindHandler(gridRegularContent_OnDataBind));
			AddDataRenderHandlers("gridRegularContent", new CellValueRenderEventHandler(gridRegularContent_OnDataRender));
		}

		public DataTable gridRegularContent_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.RegularContent.GetPagedRegularContents(UserGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridRegularContent_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Type":
					return Language.GetString(((RegularContentType)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());
				case "Period":
					string periodType = ((SmsSentPeriodType)Helper.GetInt(e.CurrentRow["PeriodType"])).ToString();
					return string.Format("{0} {1} {2} {3}", Language.GetString("Every"), e.CurrentRow[sender.FieldName], Language.GetString("Per" + periodType), Language.GetString("Once"));
				case "WarningType":
					switch (Helper.GetInt(e.CurrentRow[sender.FieldName]))
					{
						case (int)WarningType.Email:
						case (int)WarningType.Sms:
							return Language.GetString("WarningType") + " " + Language.GetString(((WarningType)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());
						default:
							return "----------";
					}

				case "Action":
					string contentIcon = "<a href='{0}'><span class='ui-icon fa fa-plus green' title='{1}' style='{2}'></span></a>";
					switch (Helper.GetInt(e.CurrentRow["Type"]))
					{
						case (int)RegularContentType.File:
							contentIcon = string.Format(contentIcon,
																					string.Format("/PageLoader.aspx?c={0}&RegularContentGuid={1}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_RegularContents_Content, Session), e.CurrentRow["Guid"]),
																					Language.GetString("ManageContent"),
																					string.Empty);
							break;
						default:
							contentIcon = string.Format(contentIcon, "#", Language.GetString("ManageContent"), "opacity: 0.5;filter: Alpha(Opacity=20);");
							break;
					}
					return contentIcon +

								 string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&Guid={1}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{2}""></span></a>",
															Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_RegularContents_SaveRegularContent, Session),
															e.CurrentRow["Guid"],
															Language.GetString("Edit")) +

								 string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onclick=""deleteRegularContent(event);""></span>",
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
			gridRegularContent.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&ActionType=insert\">{1}</a>",
																					 Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_RegularContents_SaveRegularContent, Session),
																					 Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.RegularContent);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_RegularContents_RegularContent;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_RegularContents_RegularContent.ToString());
		}
	}
}