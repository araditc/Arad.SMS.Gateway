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
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.PhoneBooks
{
	public partial class PhoneNumber : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private bool IsSuperAdmin
		{
			get { return Helper.GetBool(Session["IsSuperAdmin"]); }
		}

		private Guid PhoneBookGuid
		{
			get { return Helper.GetGuid(Helper.Request(this, "Guid").Trim('\'')); }
		}

		private int Type
		{
			get { return Helper.GetInt(Helper.Request(this, "Type")); }
		}

		private string Ip
		{
			get { return Request.UserHostAddress; }
		}

		private string Browser
		{
			get { return Request.Browser.Browser; }
		}

		public PhoneNumber()
		{
			AddDataBinderHandlers("gridNumbers", new DataBindHandler(gridNumbers_OnDataBind));
			AddDataRenderHandlers("gridNumbers", new CellValueRenderEventHandler(gridNumbers_OnDataRender));
		}

		public DataTable gridNumbers_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			if (!string.IsNullOrEmpty(toolbarFilters))
			{
				JArray array = JArray.Parse(toolbarFilters);
				JObject gender = array.Children<JObject>().Where(obj => obj.Property("field").Value.ToString() == "Sex" && obj.Property("data").Value.ToString() == "0").FirstOrDefault();
				array.Remove(gender);

				toolbarFilters = array.ToString();
			}

			string query = Helper.GenerateQueryFromToolbarFilters(toolbarFilters);
			return Facade.PhoneNumber.GetPagedNumbers(PhoneBookGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public string gridNumbers_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Sex":
					if (Helper.GetInt(e.CurrentRow[sender.FieldName]) == 0)
						return string.Empty;
					else
						return Language.GetString(Helper.GetString((Gender)Helper.GetInt(e.CurrentRow[sender.FieldName])));
				case "Action":
					if (Type == (int)PhoneBookGroupType.Normal || (IsSuperAdmin && Type == (int)PhoneBookGroupType.Vas))
					{
						return string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&PhoneNumberGuid={1}&PhoneBookGuid={2}&Type={3}"">
																	<span class='ui-icon fa fa-pencil-square-o blue' title=""{4}""></span>
																 </a>",
																	Helper.Encrypt((int)UserControls.UI_PhoneBooks_SaveSingleNumber, Session),
																	e.CurrentRow["Guid"],
																	PhoneBookGuid,
																	Type,
																	Language.GetString("Edit")) +

									string.Format(@"<span class='ui-icon fa fa-arrow-left blue' title=""{0}"" onClick=""transferNumber(event);""></span>",
																	Language.GetString("Transfer"))+

									 string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteNumber(event);""></span>",
																	Language.GetString("Delete"));

					}
					else
						return string.Empty;

			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			InitializePage();
		}

		private void InitializePage()
		{
			if (Type == (int)PhoneBookGroupType.Normal || (IsSuperAdmin && Type == (int)PhoneBookGroupType.Vas))
			{
				gridNumbers.TopToolbarItems = string.Format("<a class=\"btn btn-success btn-sm toolbarButton\" href=\"/PageLoader.aspx?c={0}&ActionType=insert&PhoneBookGuid={1}&Type={2}\">{3}</a>",
																									 Helper.Encrypt((int)UserControls.UI_PhoneBooks_SaveSingleNumber, Session),
																									 PhoneBookGuid,
																									 Type,
																									 Language.GetString("AddSingle")) +
																			string.Format("<a class=\"btn btn-success btn-sm toolbarButton\" href=\"/PageLoader.aspx?c={0}&Guid={1}&Type={2}\">{3}</a>",
																									 Helper.Encrypt((int)UserControls.UI_PhoneBooks_SaveListNumber, Session),
																									 PhoneBookGuid,
																									 Type,
																									 Language.GetString("AddListNumber")) +
																			string.Format("<a class=\"btn btn-success btn-sm toolbarButton\" href=\"/PageLoader.aspx?c={0}&Guid={1}&Type={2}\">{3}</a>",
																									 Helper.Encrypt((int)UserControls.UI_PhoneBooks_SaveEmail, Session),
																									 PhoneBookGuid,
																									 Type,
																									 Language.GetString("AddListEmail")) +
																			string.Format("<a class=\"btn btn-success btn-sm toolbarButton\" href=\"/PageLoader.aspx?c={0}&PhoneBookGuid={1}&Type={2}\">{3}</a>",
																									 Helper.Encrypt((int)UserControls.UI_PhoneBooks_SaveFileNumber, Session),
																									 PhoneBookGuid,
																									 Type,
																									 Language.GetString("AddFromFile")) +

																			string.Format("<a class=\"btn btn-warning btn-sm toolbarButton\" href=\"#\" onclick=\"addNumberToSendList();\">{0}</a>",
																									 Language.GetString("AddNumberToSendList")) +

																			string.Format("<a class=\"btn btn-warning btn-sm toolbarButton\" href=\"#\" onclick=\"sendSms();\">{0}</a>",
																									 Language.GetString("SendSms")) +

																			string.Format("<a class=\"btn btn-danger btn-sm toolbarButton\" href=\"#\" onclick=\"deleteMultipleNumber();\">{0}</a>",
																									 Language.GetString("Delete"));

				btnSend.Text = Language.GetString(btnSend.Text);
				btnSend.Attributes["onclick"] = "return validateRequiredFields();";

				dtpSendDateTime.Date = DateManager.GetSolarDate(DateTime.Now);
				dtpSendDateTime.Time = DateTime.Now.TimeOfDay.ToString();

				#region GetSenderNumber
				DataTable dtSender = Facade.PrivateNumber.GetUserPrivateNumbersForSend(UserGuid);
				dtSender.Columns.Add("Value", typeof(string));
				foreach (DataRow row in dtSender.Rows)
					row["Value"] = string.Format("{0};{1}", row["Guid"], row["Type"]);
				drpSenderNumber.DataSource = dtSender;
				drpSenderNumber.DataTextField = "Number";
				drpSenderNumber.DataValueField = "Value";
				drpSenderNumber.DataBind();
				drpSenderNumber.Items.Insert(0, new ListItem(string.Empty, string.Empty));
				#endregion

				drpGroup.DataSource = Facade.PhoneBook.GetPhoneBookOfUser(UserGuid);

				DataTable dtGroup = Facade.PhoneBook.GetPhoneBookOfUser(UserGuid);
				DataView dataViewGroup = dtGroup.DefaultView;
				dataViewGroup.RowFilter = string.Format("Type='{0}'", (int)PhoneBookGroupType.Normal);
				dtGroup = dataViewGroup.ToTable();

				drpGroup.DataSource = dtGroup;
				drpGroup.DataTextField = "Name";
				drpGroup.DataValueField = "Guid";
				drpGroup.DataBind();

			}
			else
				gridNumbers.ShowToolbar = false;
		}

		protected void btnSend_Click(object sender, EventArgs e)
		{
			Common.ScheduledSms scheduledSms = new Common.ScheduledSms();
			List<string> lstNumbers = new List<string>();

			try
			{
				int numberType = Helper.GetInt(drpSenderNumber.SelectedValue.Split(';')[1]);

				if (Helper.GetGuid(drpSenderNumber.SelectedValue.Split(';')[0]) == Guid.Empty)
					throw new Exception(Language.GetString("InvalidSenderNumber"));

				if (string.IsNullOrEmpty(txtSmsBody.Text))
					throw new Exception(Language.GetString("BlankMessage"));

				Facade.PhoneBook.CheckPhoneBookType(numberType, string.Format("'{0}'", PhoneBookGuid));

				scheduledSms.PrivateNumberGuid = Helper.GetGuid(drpSenderNumber.SelectedValue.Split(';')[0]);
				lstNumbers = txtRecievers.Text.Split(new string[] { "\n", "\r\n", ",", " ", ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
				Dictionary<int, int> operatorNumberCount = Facade.Outbox.GetCountNumberOfOperators(ref lstNumbers);

				if (operatorNumberCount.Values.Sum() == 0)
					throw new Exception(Language.GetString("RecieverListIsEmpty"));

				scheduledSms.SmsText = txtSmsBody.Text;
				scheduledSms.SmsLen = Helper.GetSmsCount(txtSmsBody.Text);
				scheduledSms.PresentType = txtSmsBody.IsFlashSms ? (int)Messageclass.Flash : (int)Messageclass.Normal;
				scheduledSms.Encoding = Helper.HasUniCodeCharacter(txtSmsBody.Text) ? (int)Encoding.Utf8 : (int)Encoding.Default;
                var dateTime = dtpSendDateTime.FullDateTime;
                if (Session["Language"].ToString() == "fa")
                {
                    scheduledSms.DateTimeFuture = DateManager.GetChristianDateTimeForDB(dateTime);
                }
                else
                {
                    scheduledSms.DateTimeFuture = DateTime.Parse(dateTime);
                }
                //scheduledSms.DateTimeFuture = DateManager.GetChristianDateTimeForDB(dtpSendDateTime.FullDateTime);
				scheduledSms.UserGuid = UserGuid;
				scheduledSms.TypeSend = (int)SmsSendType.SendSms;
				scheduledSms.Status = (int)ScheduledSmsStatus.Stored;
				scheduledSms.IP = Ip;
				scheduledSms.Browser = Browser;

				if (!Facade.ScheduledSms.InsertSms(scheduledSms, lstNumbers))
					throw new Exception(Language.GetString("ErrorRecord"));

				ShowMessageBox(Language.GetString("SendSmsSuccessful"), string.Empty, "success");
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("sendSmsError('{0}');", ex.Message);
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManagePhoneNumber);
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.AddFileNumber);

			isOptionalPermissions = true;

			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_PhoneBooks_PhoneNumber;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_PhoneBooks_PhoneNumber.ToString());
		}
	}
}
