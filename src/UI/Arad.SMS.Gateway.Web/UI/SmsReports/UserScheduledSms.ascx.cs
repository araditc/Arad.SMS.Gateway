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
	public partial class UserScheduledSms : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public UserScheduledSms()
		{
			AddDataBinderHandlers("gridUserScheduledSms", new DataBindHandler(gridUserScheduledSms_OnDataBind));
			AddDataRenderHandlers("gridUserScheduledSms", new CellValueRenderEventHandler(gridUserScheduledSms_OnDataRender));
		}

		public DataTable gridUserScheduledSms_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			if (!string.IsNullOrEmpty(toolbarFilters))
			{
				JArray array = JArray.Parse(toolbarFilters);
				JObject typeCreditChange = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "TypeSend" && obj.Property("data").Value.ToString() == "0").FirstOrDefault();
				array.Remove(typeCreditChange);

				toolbarFilters = array.ToString();
			}

			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);

			return Facade.ScheduledSms.GetPagedUsersQueue(UserGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridUserScheduledSms_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			string imgTagPattern = @"<a href=""/PageLoader.aspx?c={0}&ReferenceGuid={1}&UsersOutbox=0&MasterPage=usersqueue""><span class='{2}' title='{3}'></span></a>";
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
						case (int)ScheduledSmsStatus.Extracting:
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
					string actions = string.Empty;
					List<int> lstInvalidFailedTypeForResend = new List<int>()
					{
						(int)SmsSendFailedType.None,
						(int)SmsSendFailedType.SendError,
						(int)SmsSendFailedType.SystemIsOutOfService,
						(int)SmsSendFailedType.SmsTextIsFilter
					};

					if (lstInvalidFailedTypeForResend.Contains(Helper.GetInt(e.CurrentRow["SmsSendFaildType"])))
						resendIsActive = false;


					if (Helper.GetInt(e.CurrentRow["Status"]) == (int)ScheduledSmsStatus.WatingForConfirm ||
							Helper.GetInt(e.CurrentRow["Status"]) == (int)ScheduledSmsStatus.Rejected)
					{
						actions += string.Format(@"<span class='ui-icon fa fa-thumbs-o-up green' title=""{0}"" onClick=""confirmBulk(event);""></span>",
																			Language.GetString("Confirm"));

						actions += string.Format(@"<span class='ui-icon fa fa-thumbs-o-down red' title=""{0}"" onClick=""rejectBulk(event);""></span>",
																			Language.GetString("Reject"));
					}

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

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.UsersSendQueue);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_SmsReports_ScheduledSms;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_SmsReports_ScheduledSms.ToString());
		}
	}
}