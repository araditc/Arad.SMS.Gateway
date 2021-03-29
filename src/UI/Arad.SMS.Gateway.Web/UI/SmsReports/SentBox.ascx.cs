using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Arad.SMS.Gateway.Web.UI.SmsReports
{
	public partial class SentBox : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid OutboxGuid
		{
			get { return Helper.RequestEncryptedGuid(this, "ReferenceGuid"); }
		}

		private string ReturnPath
		{
			get
			{
				if (Helper.RequestBool(this, "UsersOutbox"))
					return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_UserOutbox, Session);
				else if (Helper.RequestBool(this, "manualoutbox"))
					return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_UserManualOutbox, Session);
				else
					return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_OutBox, Session);
			}
		}

		public SentBox()
		{
			AddDataBinderHandlers("gridSentBox", new DataBindHandler(gridSentBox_OnDataBind));
			AddDataRenderHandlers("gridSentBox", new CellValueRenderEventHandler(gridSentBox_OnDataRender));
		}

		public DataTable gridSentBox_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			if (!string.IsNullOrEmpty(toolbarFilters))
			{
				JArray array = JArray.Parse(toolbarFilters);
				JObject typeCreditChange = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "DeliveryStatus" && obj.Property("data").Value.ToString() == "0").FirstOrDefault();
				array.Remove(typeCreditChange);

				toolbarFilters = array.ToString();
			}

			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);

			return Facade.OutboxNumber.GetPagedSmses(OutboxGuid, query, pageNo, pageSize, sortField, ref resultCount);
		}

		public string gridSentBox_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "DeliveryStatus":
					if (e.CurrentRowGenerateType == RowGenerateType.Normal)
						return string.Format(@"<img src=""/pic/status{0}.png""  title=""{1}"" />",
																	e.CurrentRow[sender.FieldName],
																	Language.GetString(((DeliveryStatus)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString()));
					else
						return Language.GetString(((DeliveryStatus)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());
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
			gridSentBox.TopToolbarItems = string.Format(@"<a href=""/PageLoader.aspx?c={0}"" class=""btn btn-default"" >{1}</a>",
																		ReturnPath,
																		Language.GetString("Return"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SentBox);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_SentBox;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Helper.GetString(Business.UserControls.UI_SmsReports_SentBox));
		}
	}
}
