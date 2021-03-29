using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Arad.SMS.Gateway.Web.UI.SmsSends
{
	public partial class SendDetails : UIUserControlBase
	{
		private Guid ReferenceGuid
		{
			get { return Helper.RequestEncryptedGuid(this, "ReferenceGuid"); }
		}

		private string MasterPage
		{
			get { return Helper.Request(this, "MasterPage").ToLower(); }
		}

		private string ReturnPath
		{
			get
			{
				switch (MasterPage)
				{
					case "usersoutbox":
						return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_UserOutbox, Session);
					case "useroutbox":
						return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_OutBox, Session);
					case "manualoutbox":
						return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_UserManualOutbox, Session);
					case "usersqueue":
						return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_UserScheduledSms, Session);
					case "userqueue":
						return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_ScheduledSms, Session);
					default:
						return Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_ScheduledSms, Session);
				}
			}
		}

		public SendDetails()
		{
			AddDataBinderHandlers("gridRecipient", new DataBindHandler(gridRecipient_OnDataBind));
			AddDataRenderHandlers("gridRecipient", new CellValueRenderEventHandler(gridRecipient_OnDataRender));
		}

		public DataTable gridRecipient_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			string xml;
			if (MasterPage == "usersoutbox" || MasterPage == "useroutbox" || MasterPage == "manualoutbox")
				xml = Facade.Outbox.Load(ReferenceGuid).RequestXML;
			else
				xml = Facade.ScheduledSms.Load(ReferenceGuid).RequestXML;

			DataTable dtRecipients = new DataTable();
			dtRecipients.Columns.Add("Guid", typeof(Guid));
			dtRecipients.Columns.Add("Title", typeof(string));
			dtRecipients.Columns.Add("Count", typeof(int));
			dtRecipients.Columns.Add("SendPrice", typeof(decimal));
			dtRecipients.Columns.Add("ScopeCount", typeof(int));

			var receivers = XElement.Parse(xml).Element("Table").Element("Receivers").Elements("Receiver");

			DataRow row;
			DataRow summaryRow = dtRecipients.NewRow();

			foreach (XElement receiver in receivers)
			{
				row = dtRecipients.NewRow();

				row["Guid"] = Guid.NewGuid();
				row["Title"] = receiver.Attribute("Title").Value;
				row["Count"] = Helper.GetInt(receiver.Attribute("Count").Value);
				row["SendPrice"] = Helper.GetDecimal(receiver.Attribute("Price").Value);
				row["ScopeCount"] = Helper.GetInt(receiver.Attribute("ScopeCount").Value);

				dtRecipients.Rows.Add(row);
			}

			summaryRow["Count"] = dtRecipients.Compute("Sum(Count)", "");
			summaryRow["SendPrice"] = dtRecipients.Compute("Sum(SendPrice)", "");
			summaryRow["Title"] = Language.GetString("Sum");
			summaryRow.RowError = "SummaryRow";
			dtRecipients.Rows.Add(summaryRow);

			return dtRecipients;
		}

		public string gridRecipient_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			return string.Empty;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			try
			{

				btnCancel.Text = Language.GetString(btnCancel.Text);
				ScheduledSms scheduledSms = new ScheduledSms();
				Outbox outbox = new Outbox();
				string sender = string.Empty;

				if (MasterPage == "usersoutbox" || MasterPage == "useroutbox" || MasterPage == "manualoutbox")
				{
					outbox = Facade.Outbox.Load(ReferenceGuid);
					sender = outbox.SenderId;
					scheduledSms.RequestXML = outbox.RequestXML;
					scheduledSms.ID = outbox.ID;

					using (StringReader stringReader = new StringReader(outbox.RequestXML))
					using (XmlTextReader reader = new XmlTextReader(stringReader))
					{
						while (reader.Read())
						{
							if (reader.IsStartElement())
							{
								switch (reader.Name)
								{
									case "Guid":
										scheduledSms.ScheduledSmsGuid = Helper.GetGuid(reader.ReadString());
										break;
									case "PrivateNumberGuid":
										scheduledSms.PrivateNumberGuid = Helper.GetGuid(reader.ReadString());
										break;
									case "SmsText":
										scheduledSms.SmsText = reader.ReadString();
										break;
									case "Period":
										scheduledSms.Period = Helper.GetInt(reader.ReadString());
										break;
									case "PeriodType":
										scheduledSms.PeriodType = Helper.GetInt(reader.ReadString());
										break;
									case "SendPageSize":
										scheduledSms.SendPageSize = Helper.GetInt(reader.ReadString());
										break;
									case "TypeSend":
										scheduledSms.TypeSend = Helper.GetInt(reader.ReadString());
										break;
									case "StartDateTime":
										scheduledSms.StartDateTime = Helper.GetDateTime(reader.ReadString());
										break;
									case "EndDateTime":
										scheduledSms.EndDateTime = Helper.GetDateTime(reader.ReadString());
										break;
									case "DateTimeFuture":
										scheduledSms.DateTimeFuture = Helper.GetDateTime(reader.ReadString());
										break;
									case "ZoneGuid":
									case "ReferenceGuid":
										scheduledSms.ReferenceGuid = reader.ReadString();
										break;
								}
							}
						}
					}
				}
				else
				{
					scheduledSms = Facade.ScheduledSms.Load(ReferenceGuid);
					sender = Facade.PrivateNumber.LoadNumber(scheduledSms.PrivateNumberGuid).Number;
				}

				switch ((SmsSendType)Helper.GetInt(scheduledSms.TypeSend))
				{
					case SmsSendType.SendSms:
						lblId.Text = scheduledSms.ID.ToString();
						lblSendType.Text = Language.GetString("SendToNumber");
						lblGroupName.Text = "----------";
						lblSender.Text = sender;
						dtpSendDate.Date = DateManager.GetSolarDate(scheduledSms.DateTimeFuture);
						dtpSendDate.Time = scheduledSms.DateTimeFuture.TimeOfDay.ToString();
						txtSmsText.Text = scheduledSms.SmsText;
						mvDetails.ActiveViewIndex = 0;
						break;
					case SmsSendType.SendGroupSms:
						lblId.Text = scheduledSms.ID.ToString();
						lblSendType.Text = Language.GetString("SendToPhoneBookGroup");
						lblGroupName.Text = Facade.PhoneBook.Load(Helper.GetGuid(scheduledSms.ReferenceGuid)).Name;
						lblSender.Text = sender;
						dtpSendDate.Date = DateManager.GetSolarDate(scheduledSms.DateTimeFuture);
						dtpSendDate.Time = scheduledSms.DateTimeFuture.TimeOfDay.ToString();
						txtSmsText.Text = scheduledSms.SmsText;
						mvDetails.ActiveViewIndex = 0;
						break;
					case SmsSendType.SendFormatSms:
						lblId.Text = scheduledSms.ID.ToString();
						lblSendType.Text = Language.GetString("SendFromFormat");
						lblGroupName.Text = Facade.PhoneBook.Load(Helper.GetGuid(scheduledSms.ReferenceGuid)).Name;
						lblSender.Text = sender;
						dtpSendDate.Date = DateManager.GetSolarDate(scheduledSms.DateTimeFuture);
						dtpSendDate.Time = scheduledSms.DateTimeFuture.TimeOfDay.ToString();
						txtSmsText.Text = scheduledSms.SmsText;
						mvDetails.ActiveViewIndex = 0;
						break;
					case SmsSendType.SendPeriodSms:
						lblPeriodId.Text = scheduledSms.ID.ToString();
						lblPeriodGroupName.Text = Facade.PhoneBook.Load(Helper.GetGuid(scheduledSms.ReferenceGuid)).Name;
						lblPeriodType.Text = string.Format(Language.GetString("PeriodType"), scheduledSms.Period, Language.GetString(((Business.SmsSentPeriodType)Helper.GetInt(scheduledSms.PeriodType)).ToString()));
						lblPeriodSender.Text = sender;
						txtPeriodSmsText.Text = scheduledSms.SmsText;
						dtpStartDate.Date = DateManager.GetSolarDate(scheduledSms.StartDateTime);
						dtpStartDate.Time = scheduledSms.StartDateTime.TimeOfDay.ToString();
						dtpEndDate.Date = DateManager.GetSolarDate(scheduledSms.EndDateTime);
						dtpEndDate.Time = scheduledSms.EndDateTime.TimeOfDay.ToString();
						mvDetails.ActiveViewIndex = 1;
						break;
					case SmsSendType.SendGradualSms:
						lblGradualId.Text = scheduledSms.ID.ToString();
						lblGradualGroup.Text = Facade.PhoneBook.Load(Helper.GetGuid(scheduledSms.ReferenceGuid)).Name;
						lblGradualPerid.Text = string.Format(Language.GetString("GradualCount"), scheduledSms.Period, scheduledSms.SendPageSize);
						lblGradualSender.Text = sender;
						txtGradualSmsText.Text = scheduledSms.SmsText;
						mvDetails.ActiveViewIndex = 2;
						break;
					case SmsSendType.SendBulkSms:
						XDocument xdoc = new XDocument();
						xdoc = XDocument.Parse(scheduledSms.RequestXML);
						IEnumerable<XElement> elements = xdoc.Root.Elements().Elements();

						lblBulkId.Text = scheduledSms.ID.ToString();
						lblBulkSender.Text = Facade.PrivateNumber.LoadNumber(scheduledSms.PrivateNumberGuid).Number;
						txtBulkSmsText.Text = scheduledSms.SmsText;

						mvDetails.ActiveViewIndex = 3;
						break;
				}
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", ReturnPath));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SendDetails);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSends_SendDetails;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsSends_SendDetails.ToString());
		}
	}
}