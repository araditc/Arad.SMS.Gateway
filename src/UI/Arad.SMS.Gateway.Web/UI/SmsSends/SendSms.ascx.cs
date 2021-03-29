using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.SmsSends
{
	public partial class SendSms : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid ParentGuid
		{
			get { return Helper.GetGuid(Session["ParentGuid"]); }
		}

		private bool IsAdmin
		{
			get { return Helper.GetBool(Session["IsAdmin"]); }
		}

		private Guid ReferenceGuid
		{
			get { return Helper.RequestEncryptedGuid(this, "ReferenceGuid"); }
		}

		private string MasterPage
		{
			get { return Helper.Request(this, "MasterPage").ToLower(); }
		}

		private string Ip
		{
			get { return Request.UserHostAddress; }
		}

		private string Browser
		{
			get { return Request.Browser.Browser; }
		}

		public SendSms()
		{
			AddDataBinderHandlers("treeGroups", new GeneralTools.TreeView.DataBindHandler(treeGroups_OnDataBind));
		}

		public List<GeneralTools.TreeView.TreeNode> treeGroups_OnDataBind(string parentID, string search)
		{
			DataTable dtGroups = Facade.PhoneBook.GetPhoneBookOfUser(UserGuid, Helper.GetGuid(parentID), search);
			var nodes = new List<GeneralTools.TreeView.TreeNode>();

			foreach (DataRow row in dtGroups.Rows)
			{
				var node = new GeneralTools.TreeView.TreeNode();
				node.id = string.Format("'{0}'", row["Guid"]);
				node.state = "closed";
				node.text = row["Name"].ToString();
				node.attributes = new { count = row["CountPhoneNumbers"].ToString(), type = Helper.GetInt(row["Type"], 1) };
				nodes.Add(node);
			}

			return nodes;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();

			GridView gv = new GridView();
			gv.HeaderStyle.BackColor = System.Drawing.Color.CornflowerBlue;
			gv.HeaderStyle.Font.Bold = true;
			gv.HeaderStyle.ForeColor = System.Drawing.Color.White;
			gv.CellPadding = 5;
			gv.AutoGenerateColumns = false;

			BoundField nameColumn = new BoundField();
			nameColumn.DataField = "FirstName";
			nameColumn.HeaderText = "First Name";
			gv.Columns.Add(nameColumn);
		}

		private void InitializePage()
		{
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

			#region GetPeriodType
			foreach (SmsSentPeriodType periodType in System.Enum.GetValues(typeof(SmsSentPeriodType)))
				drpPeriodType.Items.Add(new ListItem(Language.GetString(periodType.ToString()), ((int)periodType).ToString()));

			drpPeriodType.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            #endregion

            
            dtpSendDateTime.Time = DateTime.Now.TimeOfDay.ToString();

            btnSendTestSms.Text = Language.GetString(btnSendTestSms.Text);
			btnSendSms.Text = Language.GetString(btnSendSms.Text);

            if (Session["Language"].ToString() == "fa")
            {
                dtpSendDateTime.Date = DateManager.GetSolarDate(DateTime.Now);
            }
            else
            {
                dtpSendDateTime.Date = DateTime.Now.ToShortDateString();
            }

            if (ReferenceGuid != Guid.Empty)
			{
				switch (MasterPage.ToLower())
				{
					case "usersoutbox":
					case "useroutbox":
						Common.Outbox outbox = Facade.Outbox.Load(ReferenceGuid);
						txtSmsBody.Text = outbox.SmsText;
						break;
					case "inbox":
						Common.Inbox inbox = Facade.Inbox.Load(ReferenceGuid);
						txtSmsBody.Text = inbox.SmsText;
						break;
				}
			}
		}

		protected void btnSendTestSms_Click(object sender, EventArgs e)
		{
			Common.ScheduledSms scheduledSms = new Common.ScheduledSms();
			List<string> lstNumbers = new List<string>();
			try
			{
				if (Helper.GetGuid(drpSenderNumber.SelectedValue.Split(';')[0]) == Guid.Empty)
					throw new Exception(Language.GetString("InvalidSenderNumber"));

				if (string.IsNullOrEmpty(txtSmsBody.Text))
					throw new Exception(Language.GetString("BlankMessage"));

				scheduledSms.PrivateNumberGuid = Helper.GetGuid(drpSenderNumber.SelectedValue.Split(';')[0]);
				lstNumbers = txtTestRecievers.Text.Split(new string[] { "\n", "\r\n", ",", " ", ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
				Dictionary<int, int> operatorNumberCount = Facade.Outbox.GetCountNumberOfOperators(ref lstNumbers);

				if (operatorNumberCount.Values.Sum() == 0)
					throw new Exception(Language.GetString("RecieverListIsEmpty"));

				scheduledSms.SmsText = txtSmsBody.Text;
				scheduledSms.SmsLen = Helper.GetSmsCount(txtSmsBody.Text);
				scheduledSms.PresentType = txtSmsBody.IsFlashSms ? (int)Messageclass.Flash : (int)Messageclass.Normal;
				scheduledSms.Encoding = Helper.HasUniCodeCharacter(txtSmsBody.Text) ? (int)Encoding.Utf8 : (int)Encoding.Default;
				scheduledSms.DateTimeFuture = DateTime.Now;
				scheduledSms.UserGuid = UserGuid;
				scheduledSms.Status = (int)ScheduledSmsStatus.Stored;
				scheduledSms.TypeSend = (int)SmsSendType.SendSms;
				scheduledSms.IP = Ip;
				scheduledSms.Browser = Browser;
                scheduledSms.ScheduledSmsGuid = Guid.NewGuid();
                

				if (!Facade.ScheduledSms.InsertSms(scheduledSms, lstNumbers))
					throw new Exception(Language.GetString("ErrorRecord"));

				ClientSideScript = string.Format("result('step1','OK','{0}')", Language.GetString("SendSmsSuccessful"));
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("result('step1','Error','{0}')", ex.Message);
			}
		}

		protected void btnSendSms_Click(object sender, EventArgs e)
		{
          

            Common.ScheduledSms scheduledSms = new Common.ScheduledSms();
			try
			{
				#region GetSendSmsInfo
				List<string> recieversNumber = new List<string>();
				Dictionary<int, int> operatorNumberCount = new Dictionary<int, int>();
				Dictionary<string, string> dicFileInfo = new Dictionary<string, string>();
				SmsTypes smsType = Helper.HasUniCodeCharacter(txtSmsBody.Text) ? SmsTypes.Farsi : SmsTypes.Latin;

				if (Helper.GetGuid(hdnGroupGuid.Value.Trim('\'')) != Guid.Empty)
				{
					scheduledSms.ReferenceGuid = hdnGroupGuid.Value.Trim('\'');
					operatorNumberCount = Facade.PhoneNumber.GetCountPhoneBooksNumberOperator(scheduledSms.ReferenceGuid);
				}

				string fileInfo = string.Empty;
				if (!string.IsNullOrEmpty(hdnFilePath.Value))
				{
					dicFileInfo = Facade.PhoneNumber.GetFileNumberInfo(Server.MapPath("~/Uploads/" + hdnFilePath.Value), recieversNumber);
					fileInfo = string.Format(@"<p>عنوان فایل {0}</p>
																		<p>تعداد کل شماره ها {1} عدد</p>
																		<p>تعداد شماره های تکراری {2} عدد</p>
																		<p>تعداد شماره های صحیح {3} عدد</p>", hdnFilePath.Value.Split('/')[1], dicFileInfo["TotalNumberCount"], dicFileInfo["DuplicateNumberCount"], dicFileInfo["CorrectNumberCount"]);

				}
				recieversNumber.AddRange(txtRecievers.Text.Split(new string[] { "\n", "\r\n", ",", " ", ";" }, StringSplitOptions.RemoveEmptyEntries).ToList());

				Facade.Outbox.AddCountNumberOfOperatorsToDictionary(ref recieversNumber, ref operatorNumberCount);

				decimal sendPrice = Facade.Transaction.GetSendPrice(UserGuid, smsType, Helper.GetSmsCount(txtSmsBody.Text), Helper.GetGuid(drpSenderNumber.SelectedValue.Split(';')[0]), operatorNumberCount);

				int numberType = Facade.PrivateNumber.LoadNumber(Helper.GetGuid(drpSenderNumber.SelectedValue.Split(';')[0])).Type;

				string result = "Result{(OK)}TotalCount{(" + operatorNumberCount.Values.Sum() + ")}SendPrice{(" + sendPrice + ")}NumberType{(" + numberType + ")}";

				if (!string.IsNullOrEmpty(fileInfo))
					result += "FileInfo{(" + fileInfo + ")}FilePath{(" + hdnFilePath.Value + ")}FileCorrectNumberCount{(" + dicFileInfo["CorrectNumberCount"] + ")}";

				foreach (KeyValuePair<int, int> opt in operatorNumberCount)
					result += opt.Key + "{(" + opt.Value + ")}";
				#endregion

				scheduledSms.PrivateNumberGuid = Helper.GetGuid(drpSenderNumber.SelectedValue.Split(';')[0]);
                if (!string.IsNullOrEmpty(hdnFileSoundPath.Value))
                {
                     scheduledSms.SmsLen = int.Parse(hdnFileSoundLength.Value);
                    //scheduledSms.SmsText = txtVoiceTitle.Text;
                    scheduledSms.VoiceURL = Server.MapPath("~/Uploads/" + hdnFileSoundPath.Value);
                    scheduledSms.VoiceMessageId = int.Parse(hdnFileSoundID.Value);
                }
                else
                {
                    scheduledSms.SmsText = txtSmsBody.Text;
                    scheduledSms.SmsLen = Helper.GetSmsCount(txtSmsBody.Text);
                }
				scheduledSms.PresentType = txtSmsBody.IsFlashSms ? (int)Arad.SMS.Gateway.Business.Messageclass.Flash : (int)Arad.SMS.Gateway.Business.Messageclass.Normal;
				scheduledSms.Encoding = Helper.HasUniCodeCharacter(txtSmsBody.Text) ? (int)Encoding.Utf8 : (int)Encoding.Default;
				scheduledSms.UserGuid = UserGuid;
				scheduledSms.Status = (int)ScheduledSmsStatus.Stored;
				scheduledSms.IP = Ip;
				scheduledSms.Browser = Browser;
                scheduledSms.ScheduledSmsGuid = Guid.NewGuid();


                if (operatorNumberCount.Values.Sum() == 0)
					throw new Exception(Language.GetString("RecieverListIsEmpty"));

				if (rdbSendDateTime.Checked)
                {
                    var dateTime = dtpSendDateTime.FullDateTime;
                    if (Session["Language"].ToString() == "fa")
                    {
                        scheduledSms.DateTimeFuture = DateManager.GetChristianDateTimeForDB(dateTime);
                    }
                    else
                    {
                        scheduledSms.DateTimeFuture = DateTime.Parse(dateTime);
                    }
                    
					if (scheduledSms.DateTimeFuture < DateTime.Now)
						scheduledSms.DateTimeFuture = DateTime.Now.AddSeconds(15);

					if (recieversNumber.Count > 0)
					{
						scheduledSms.TypeSend = (int)SmsSendType.SendSms;
						Facade.ScheduledSms.InsertSms(scheduledSms, recieversNumber);
					}

					if (!string.IsNullOrEmpty(scheduledSms.ReferenceGuid))
					{
						scheduledSms.TypeSend = (int)SmsSendType.SendGroupSms;
						Facade.ScheduledSms.InsertGroupSms(scheduledSms);
					}
				}
				else if (rdbSendPeriod.Checked)
				{
					scheduledSms.PeriodType = Helper.GetInt(drpPeriodType.SelectedValue);
					scheduledSms.Period = Helper.GetInt(txtPeriod.Text);
					//scheduledSms.StartDateTime = DateManager.GetChristianDateTimeForDB(dtpPeriodStart.FullDateTime);
					//scheduledSms.EndDateTime = DateManager.GetChristianDateTimeForDB(dtpPeriodEnd.FullDateTime);
                    var dateTime = dtpPeriodStart.FullDateTime;
                    var dateTimeEnd = dtpPeriodEnd.FullDateTime;
                    if (Session["Language"].ToString() == "fa")
                    {
                        scheduledSms.StartDateTime = DateManager.GetChristianDateTimeForDB(dateTime);
                        scheduledSms.EndDateTime = DateManager.GetChristianDateTimeForDB(dateTimeEnd);
                    }
                    else
                    {
                        scheduledSms.StartDateTime = DateTime.Parse(dateTime);
                        scheduledSms.EndDateTime = DateTime.Parse(dateTimeEnd);
                    }


                    Facade.ScheduledSms.InsertPeriodSms(scheduledSms, recieversNumber);
				}
				else if (rdbGradual.Checked)
				{
					scheduledSms.PeriodType = (int)SmsSentPeriodType.Minute;
					scheduledSms.Period = Helper.GetInt(txtGradualPeriod.Text);
					scheduledSms.SendPageSize = Helper.GetInt(txtGradualPageSize.Text);
					scheduledSms.SendPageNo = 0;
					scheduledSms.DateTimeFuture = DateTime.Now.AddMinutes(scheduledSms.Period);

					Facade.ScheduledSms.InsertGradualSms(scheduledSms, recieversNumber);
				}

				ClientSideScript = string.Format("result('step4','OK','{0}');setSendPriceTableInfo('step4','{1}');", Language.GetString("SendSmsSuccessful"), result);
				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)UserControls.UI_SmsReports_ScheduledSms, Session)));
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("result('step4','Error','{0}');setSendPriceTableInfo('step4','{1}');", ex.Message, string.Empty);
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SendSms);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSends_SendSms;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsSends_SendSms.ToString());
		}
	}
}
