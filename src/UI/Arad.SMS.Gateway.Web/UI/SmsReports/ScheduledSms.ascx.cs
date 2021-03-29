using Arad.SMS.Gateway.Business;
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
	public partial class ScheduledSms : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public ScheduledSms()
		{
			AddDataBinderHandlers("gridScheduledSms", new DataBindHandler(gridScheduledSms_OnDataBind));
			AddDataRenderHandlers("gridScheduledSms", new CellValueRenderEventHandler(gridScheduledSms_OnDataRender));
		}

		public DataTable gridScheduledSms_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			if (!string.IsNullOrEmpty(toolbarFilters))
			{
				JArray array = JArray.Parse(toolbarFilters);
				JObject typeCreditChange = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "TypeSend" && obj.Property("data").Value.ToString() == "0").FirstOrDefault();
				array.Remove(typeCreditChange);

				toolbarFilters = array.ToString();
			}

			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);

			return Facade.ScheduledSms.GetUserQueue(UserGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridScheduledSms_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			string imgTagPattern = @"<a href=""/PageLoader.aspx?c={0}&ReferenceGuid={1}&MasterPage=userqueue""><span class='{2}' title='{3}' style='margin-right:10px;'></span></a>";
			//string notActiveElementStyle = "opacity: 0.5;filter: Alpha(Opacity=20);";
			bool resendIsActive = true;

			switch (sender.FieldName)
			{
				case "Status":
					switch (Helper.GetInt(e.CurrentRow[sender.FieldName]))
					{
						case (int)ScheduledSmsStatus.Stored:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<span class='ui-icon fa fa-floppy-o blue' title=""{0}""></span>", Language.GetString(ScheduledSmsStatus.Stored.ToString()))
										 : Language.GetString(SendStatus.Stored.ToString());

						case (int)ScheduledSmsStatus.FailedExtract:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<span class='ui-icon fa fa-times orange' title=""{0}""></span>", Language.GetString(ScheduledSmsStatus.FailedExtract.ToString()))
										 : Language.GetString(ScheduledSmsStatus.FailedExtract.ToString());

						case (int)ScheduledSmsStatus.Extracted:
						case (int)ScheduledSmsStatus.Ready:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<img onclick='#' src='{0}' title='{1}'/>", "/pic/arrowsloader.gif", Language.GetString(ScheduledSmsStatus.Ready.ToString()))
										 : Language.GetString(ScheduledSmsStatus.Ready.ToString());

						case (int)ScheduledSmsStatus.Failed:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<span class='ui-icon fa fa-times red' title=""{0}""></span>", Language.GetString(ScheduledSmsStatus.Failed.ToString()))
										 : Language.GetString(ScheduledSmsStatus.Failed.ToString());

						case (int)ScheduledSmsStatus.WatingForConfirm:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<span class='ui-icon fa fa-clock-o orange' title=""{0}""></span>", Language.GetString(ScheduledSmsStatus.WatingForConfirm.ToString()))
										 : Language.GetString(ScheduledSmsStatus.WatingForConfirm.ToString());

						case (int)ScheduledSmsStatus.Confirmed:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<span class='ui-icon fa fa-thumbs-o-up green' title=""{0}""></span>", Language.GetString(ScheduledSmsStatus.Confirmed.ToString()))
										 : Language.GetString(ScheduledSmsStatus.Confirmed.ToString());

						case (int)ScheduledSmsStatus.Rejected:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<span class='ui-icon fa fa-times red' title=""{0}""></span>", Language.GetString(ScheduledSmsStatus.Rejected.ToString()))
										 : Language.GetString(ScheduledSmsStatus.Rejected.ToString());

						default:
							return Helper.GetString(e.CurrentRow[sender.FieldName]);
					}
				case "Action":
					List<int> lstInvalidFailedTypeForResend = new List<int>()
					{
						(int)SmsSendFailedType.None,
						(int)SmsSendFailedType.SendError,
						(int)SmsSendFailedType.SystemIsOutOfService,
						(int)SmsSendFailedType.SmsTextIsFilter
					};

					if (lstInvalidFailedTypeForResend.Contains(Helper.GetInt(e.CurrentRow["SmsSendFaildType"])))
						resendIsActive = false;

					string actions = string.Empty;

					if (resendIsActive)
						actions += string.Format(@"<span class='ui-icon fa fa-arrow-circle-up red' title=""{0}"" onClick='resend(event);'></span>",
																			 Language.GetString("Resend"));

					actions += string.Format(imgTagPattern,
																		Helper.Encrypt((int)UserControls.UI_SmsSends_SendDetails, Session),
																		Helper.Encrypt(e.CurrentRow["Guid"], Session),
																	 "ui-icon fa fa-list-alt orange",
																		Language.GetString("Details"));

					actions += string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteRow(event);""></span>",
																 Language.GetString("Delete"));
					return actions;
				case "TypeSend":
					return Language.GetString(((SmsSendType)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());

				case "SmsSendFaildType":
					return Language.GetString(((SmsSendFailedType)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());

			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e) { }

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ScheduledSms);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_SmsReports_ScheduledSms;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsReports_ScheduledSms.ToString());
		}
	}
}