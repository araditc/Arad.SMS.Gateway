﻿using Arad.SMS.Gateway.Business;
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
	public partial class UserManualOutbox : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public UserManualOutbox()
		{
			AddDataBinderHandlers("gridUsersOutbox", new DataBindHandler(gridUsersOutbox_OnDataBind));
			AddDataRenderHandlers("gridUsersOutbox", new CellValueRenderEventHandler(gridUsersOutbox_OnDataRender));
		}

		public DataTable gridUsersOutbox_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			int totalReceiverCount = 0;
			decimal totalPrice = 0;

			if (!string.IsNullOrEmpty(toolbarFilters))
			{
				JArray array = JArray.Parse(toolbarFilters);
				JObject typeCreditChange = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "SmsSendType" && obj.Property("data").Value.ToString() == "0").FirstOrDefault();
				array.Remove(typeCreditChange);

				toolbarFilters = array.ToString();
			}

			if (!string.IsNullOrEmpty(searchFiletrs) && !string.IsNullOrEmpty(toolbarFilters))
			{
				JArray array = JArray.Parse(searchFiletrs);
				JObject sentDateTime = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "SentDateTime" &&
																																	obj.Property("data").Value.ToString() != string.Empty).FirstOrDefault();
				if (sentDateTime != null)
				{
					array = JArray.Parse(toolbarFilters);
					sentDateTime = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "SentDateTime").FirstOrDefault();
					array.Remove(sentDateTime);
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


			DataTable dtOutbox = Facade.Outbox.GetPagedUserManualSmses(query, pageNo, pageSize, sortField, ref resultCount, ref totalReceiverCount, ref totalPrice);

			DataRow summaryRow = dtOutbox.NewRow();

			summaryRow["Price"] = totalPrice;
			summaryRow["ReceiverCount"] = totalReceiverCount;
			summaryRow["SmsText"] = Language.GetString("Sum");
			summaryRow.RowError = "SummaryRow";
			dtOutbox.Rows.Add(summaryRow);

			return dtOutbox;
		}

		public string gridUsersOutbox_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			string imgTagPattern = @"<a href=""/PageLoader.aspx?c={0}&ReferenceGuid={1}&UsersOutbox=1&MasterPage=manualoutbox""><span class='{2}' title='{3}'></span></a>";
			string rawPattern = string.Empty;
			string exportData = string.Empty;
			string confirmRequest = string.Empty;
			string actions = string.Empty;
			switch ((ExportDataStatus)Helper.GetInt(e.CurrentRow["ExportDataStatus"]))
			{
				case ExportDataStatus.None:
					rawPattern = @"<span onclick='exportData(event)' class='{0}' title='{1}'></span>";
					exportData = string.Format(rawPattern,
																		 "ui-icon fa fa-2x fa fa-file-excel-o green",
																		 Language.GetString("ExportToExcel"));
					break;
				case ExportDataStatus.Get:
					rawPattern = @"<img onclick='#' src='{0}' title='{1}' style='cursor:pointer;'/>";
					exportData = string.Format(rawPattern,
																		 "/pic/arrowsloader.gif",
																		 Language.GetString("ExportToExcel"));
					break;
				case ExportDataStatus.Archived:
				case ExportDataStatus.Complete:
					rawPattern = @"<a href=""/Archives/{0}""><span class='{1}' title='{2}'></span></a>";
					exportData = string.Format(rawPattern,
																		 e.CurrentRow["ID"],
																		 "ui-icon fa fa-2x fa-download dark",
																		 Language.GetString("Download"));
					break;
			}
			switch (sender.FieldName)
			{
				case "SendStatus":
					switch (Helper.GetInt(e.CurrentRow[sender.FieldName]))
					{
						case (int)SendStatus.Stored:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<span class='ui-icon fa fa-floppy-o blue' title=""{0}""></span>", Language.GetString(SendStatus.Stored.ToString()))
										 : Language.GetString(SendStatus.Stored.ToString());

						case (int)SendStatus.WatingForSend:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<span class='ui-icon fa fa-clock-o orange' title=""{0}""></span>", Language.GetString(SendStatus.WatingForSend.ToString()))
										 : Language.GetString(SendStatus.WatingForSend.ToString());

						case (int)SendStatus.WatingForConfirm:
						case (int)SendStatus.IsBeingSent:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<img onclick='#' src='{0}' title='{1}'/>", "/pic/arrowsloader.gif", Language.GetString(SendStatus.IsBeingSent.ToString()))
										 : Language.GetString(SendStatus.IsBeingSent.ToString());

						case (int)SendStatus.Sent:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<span class='ui-icon fa fa-check green' title=""{0}""></span>", Language.GetString(SendStatus.Sent.ToString()))
										 : Language.GetString(SendStatus.Sent.ToString());

						case (int)SendStatus.SentAndGiveBackCredit:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<span class='ui-icon fa fa-check-square green' title=""{0}""></span>", Language.GetString(SendStatus.SentAndGiveBackCredit.ToString()))
										 : Language.GetString(SendStatus.SentAndGiveBackCredit.ToString());

						case (int)SendStatus.Archived:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<span class='ui-icon fa fa-archive purple' title=""{0}""></span>", Language.GetString(SendStatus.Archived.ToString()))
										 : Language.GetString(SendStatus.Archived.ToString());

						case (int)SendStatus.Archiving:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<span class='ui-icon fa fa-archive purple' style='opacity: 0.5;' title=""{0}""></span>", Language.GetString(SendStatus.Archived.ToString()))
										 : Language.GetString(SendStatus.Archiving.ToString());

						case (int)SendStatus.ErrorInSending:
							return e.CurrentRowGenerateType == RowGenerateType.Normal
										 ? string.Format(@"<span class='ui-icon fa fa-times red' title=""{0}""></span>", Language.GetString(SendStatus.ErrorInSending.ToString()))
										 : Language.GetString(SendStatus.ErrorInSending.ToString());

						default:
							return Helper.GetString(e.CurrentRow[sender.FieldName]);
					}
				case "Action":

					if (Helper.GetInt(e.CurrentRow["SendStatus"]) != (int)SendStatus.Archived)
					{
						actions += string.Format(imgTagPattern,
																	 Helper.Encrypt((int)UserControls.UI_SmsReports_SentBox, Session),
																	 Helper.Encrypt(e.CurrentRow["Guid"], Session),
																	 "ui-icon fa fa-2x fa-mobile blue",
																	 Language.GetString("Recipients"));
					}

					if (Helper.GetInt(e.CurrentRow["SendStatus"]) == (int)SendStatus.WatingForConfirm)
					{
						actions += string.Format(@"<span class='ui-icon fa fa-thumbs-o-up green' title=""{0}"" onClick=""confirmOutboxBulk(event);""></span>",
													Language.GetString("Confirm"));
					}
					actions += exportData +

										 string.Format(imgTagPattern,
																	 Helper.Encrypt((int)UserControls.UI_Home, Session),
																	 Helper.Encrypt(e.CurrentRow["Guid"], Session),
																	 "ui-icon fa fa-2x fa-line-chart orange",
																	 Language.GetString("ReportSend")) +

										 string.Format(imgTagPattern,
																	 Helper.Encrypt((int)UserControls.UI_Users_Transaction, Session),
																	 Helper.Encrypt(e.CurrentRow["Guid"], Session),
																	 "ui-icon fa fa-2x fa-usd green",
																	 Language.GetString("Transaction")) +

										 string.Format(imgTagPattern,
																	 Helper.Encrypt((int)UserControls.UI_SmsSends_SendDetails, Session),
																	 Helper.Encrypt(e.CurrentRow["Guid"], Session),
																	 "ui-icon fa fa-list-alt orange",
																	 Language.GetString("Details")) +

										 //string.Format(imgTagPattern,
										 //						 Helper.Encrypt((int)UserControls.UI_SmsReports_UpdateDeliveryStatus, Session),
										 //						 Helper.Encrypt(e.CurrentRow["Guid"], Session),
										 //						 "ui-icon fa fa-upload purple",
										 //						 Language.GetString("UpdateDeliveryStatus")) +

										 string.Format(imgTagPattern,
																	 Helper.Encrypt((int)UserControls.UI_SmsSends_SendSms, Session),
																	 Helper.Encrypt(e.CurrentRow["Guid"], Session),
																	 "ui-icon fa fa-2x fa-share red",
																	 Language.GetString("SendToOther"));
					return actions;

				case "SmsSendType":
					return Language.GetString(((SmsSendType)Helper.GetInt(e.CurrentRow[sender.FieldName])).ToString());
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.UserManualOutbox);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_SmsReports_UserManualOutbox;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_SmsReports_UserManualOutbox.ToString());
		}
	}
}