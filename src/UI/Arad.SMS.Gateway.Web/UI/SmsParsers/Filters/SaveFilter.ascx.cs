using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.SmsParsers.Filters
{
	public partial class SaveFilter : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public Guid SmsParserGuid
		{
			get { return Helper.RequestGuid(this, "ParserGuid"); }
		}

		private string ActionType
		{
			get { return Helper.Request(this, "ActionType").ToLower(); }
		}

		public SaveFilter()
		{
			AddDataBinderHandlers("treeOperationGroup", new GeneralTools.TreeView.DataBindHandler(treeOperationGroup_OnDataBind));
			AddDataBinderHandlers("treeGroups", new GeneralTools.TreeView.DataBindHandler(treeGroups_OnDataBind));
			AddDataBinderHandlers("treePhoneBook", new GeneralTools.TreeView.DataBindHandler(treePhoneBook_OnDataBind));
			AddDataBinderHandlers("treeCondition", new GeneralTools.TreeView.DataBindHandler(treeCondition_OnDataBind));
		}

		public List<GeneralTools.TreeView.TreeNode> treeOperationGroup_OnDataBind(string parentID, string search)
		{
			return Facade.PhoneBook.GetAllPhoneBookOfUser(UserGuid);
		}

		public List<GeneralTools.TreeView.TreeNode> treeGroups_OnDataBind(string parentID, string search)
		{
			return Facade.PhoneBook.GetAllPhoneBookOfUser(UserGuid);
		}

		public List<GeneralTools.TreeView.TreeNode> treePhoneBook_OnDataBind(string parentID, string search)
		{
			return Facade.PhoneBook.GetAllPhoneBookOfUser(UserGuid);
		}

		public List<GeneralTools.TreeView.TreeNode> treeCondition_OnDataBind(string parentID, string search)
		{
			return Facade.PhoneBook.GetAllPhoneBookOfUser(UserGuid);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnSave.Attributes["onclick"] = "return submitValidation();";
			btnSave.Text = Language.GetString(btnSave.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);

			drpNumber.DataSource = Facade.PrivateNumber.GetUserPrivateNumbersForReceive(UserGuid);
			drpNumber.DataTextField = "Number";
			drpNumber.DataValueField = "Guid";
			drpNumber.DataBind();

			foreach (SmsFilterSenderNumber senderNumber in Enum.GetValues(typeof(SmsFilterSenderNumber)))
				drpTypeConditionSender.Items.Add(new ListItem(Language.GetString(senderNumber.ToString()), ((int)senderNumber).ToString()));
			drpTypeConditionSender.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			foreach (SmsFilterConditions condition in Enum.GetValues(typeof(SmsFilterConditions)))
				drpConditions.Items.Add(new ListItem(Language.GetString(condition.ToString()), ((int)condition).ToString()));

			foreach (SmsFilterOperations operation in Enum.GetValues(typeof(SmsFilterOperations)))
				drpOperations.Items.Add(new ListItem(Language.GetString(operation.ToString()), ((int)operation).ToString()));
			drpOperations.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			drpSenderNumber.DataSource = Facade.PrivateNumber.GetUserPrivateNumbersForSend(UserGuid);
			drpSenderNumber.DataTextField = "Number";
			drpSenderNumber.DataValueField = "Guid";
			drpSenderNumber.DataBind();
			drpSenderNumber.Items.Insert(0, new ListItem(Language.GetString("SenderNumber"), string.Empty));

			DataTable dtUserFormats = Facade.SmsFormat.GetUserSmsFormats(UserGuid);
			drpAccpetFormat.DataSource = dtUserFormats;
			drpAccpetFormat.DataTextField = "FormatName";
			drpAccpetFormat.DataValueField = "FormatGuid";
			drpAccpetFormat.DataBind();
			drpAccpetFormat.Items.Insert(0, new ListItem(Language.GetString("UseFormatIfMatching"), string.Empty));

			drpRejectFormat.DataSource = dtUserFormats;
			drpRejectFormat.DataTextField = "FormatName";
			drpRejectFormat.DataValueField = "FormatGuid";
			drpRejectFormat.DataBind();
			drpRejectFormat.Items.Insert(0, new ListItem(Language.GetString("UseFormatIncompatibility"), string.Empty));

			int resultCount = 0;
			drpTrafficRelay.DataSource = Facade.TrafficRelay.GetPagedTrafficRelays(UserGuid, "CreateDate", 0, 0, ref resultCount);
			drpTrafficRelay.DataTextField = "Url";
			drpTrafficRelay.DataValueField = "Guid";
			drpTrafficRelay.DataBind();

			dtpStartDate.Date = DateManager.GetSolarDate(DateTime.Now);
			dtpStartDate.Time = DateTime.Now.TimeOfDay.ToString();

			dtpEndDate.Date = DateManager.GetSolarDate(DateTime.Now);
			dtpEndDate.Time = DateTime.Now.TimeOfDay.ToString();

			if (ActionType == "edit")
			{
				Common.SmsParser smsParser = Facade.SmsParser.LoadSmsParser(SmsParserGuid);
				ParserFormulaSerialization parserFormulaSerialization = new ParserFormulaSerialization();

				drpNumber.SelectedValue = smsParser.PrivateNumberGuid.ToString();
				txtTitle.Text = smsParser.Title;
				dtpStartDate.Date = DateManager.GetSolarDate(smsParser.FromDateTime);
				dtpStartDate.Time = smsParser.FromDateTime.TimeOfDay.ToString();
				dtpEndDate.Date = DateManager.GetSolarDate(smsParser.ToDateTime);
				dtpEndDate.Time = smsParser.ToDateTime.TimeOfDay.ToString();
				hdnScopeGuid.Value = smsParser.Scope.ToString();
				drpTypeConditionSender.SelectedValue = smsParser.TypeConditionSender.ToString();
				txtConditionSender.Text = smsParser.ConditionSender;

				DataTable dataTableParserFormulas = Facade.ParserFormula.GetParserFormulas(SmsParserGuid);
				if (dataTableParserFormulas.Rows.Count > 0)
				{
					DataRow row = dataTableParserFormulas.Rows[0];
					drpConditions.SelectedValue = row["Condition"].ToString();
					txtCondition.Text = row["Key"].ToString();

					string reactionExtension = row["ReactionExtention"].ToString();
					parserFormulaSerialization = (ParserFormulaSerialization)SerializationTools.DeserializeXml(reactionExtension, typeof(ParserFormulaSerialization));
					drpOperations.SelectedValue = parserFormulaSerialization.Condition.ToString();
					switch (parserFormulaSerialization.Condition)
					{
						case (int)SmsFilterOperations.AddToGroup:
						case (int)SmsFilterOperations.RemoveFromGroup:
							hdnOperationGroupGuid.Value = parserFormulaSerialization.ReferenceGuid.ToString();
							drpSenderNumber.SelectedValue = parserFormulaSerialization.Sender.ToString();
							txtSmsBody.Text = parserFormulaSerialization.Text;
							txtUrl.Text = parserFormulaSerialization.VasURL;
							break;
						case (int)SmsFilterOperations.SendSmsToGroup:
							txtSmsBody.Text = parserFormulaSerialization.Text;
							drpSenderNumber.SelectedValue = parserFormulaSerialization.Sender.ToString();
							hdnGroupGuid.Value = parserFormulaSerialization.ReferenceGuid.ToString();
							break;
						case (int)SmsFilterOperations.SendSmsToSender:
							txtSmsBody.Text = parserFormulaSerialization.Text;
							drpSenderNumber.SelectedValue = parserFormulaSerialization.Sender.ToString();
							break;
						case (int)SmsFilterOperations.TransferToUrl:
							drpTrafficRelay.SelectedValue = parserFormulaSerialization.ReferenceGuid.ToString();
							break;
						case (int)SmsFilterOperations.TransferToMobile:
							txtOpration.Text = parserFormulaSerialization.Text;
							drpSenderNumber.SelectedValue = parserFormulaSerialization.Sender.ToString();
							break;
						case (int)SmsFilterOperations.SendSmsFromFormat:
							drpRejectFormat.SelectedValue = parserFormulaSerialization.RejectFormatGuid.ToString();
							drpAccpetFormat.SelectedValue = parserFormulaSerialization.AcceptFormatGuid.ToString();
							drpSenderNumber.SelectedValue = parserFormulaSerialization.Sender.ToString();
							break;
						case (int)SmsFilterOperations.ForwardSmsToGroup:
							drpSenderNumber.SelectedValue = parserFormulaSerialization.Sender.ToString();
							hdnOperationGroupGuid.Value = parserFormulaSerialization.ReferenceGuid.ToString();
							break;
						default:
							txtOpration.Text = parserFormulaSerialization.Text;
							break;
					}
				}

				ClientSideScript = "setOption();setCondition('edit');";
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				Common.SmsParser smsParser = new Common.SmsParser();
				Common.ParserFormula parserFormula = new Common.ParserFormula();
				ParserFormulaSerialization parserFormulaSerialization = new ParserFormulaSerialization();

				smsParser.SmsParserGuid = SmsParserGuid;
				smsParser.PrivateNumberGuid = Helper.GetGuid(drpNumber.SelectedValue);
				smsParser.Title = txtTitle.Text;
				smsParser.Type = (int)SmsParserType.Filter;
				smsParser.CreateDate = DateTime.Now;
				smsParser.FromDateTime = DateManager.GetChristianDateTimeForDB(dtpStartDate.FullDateTime);
				smsParser.ToDateTime = DateManager.GetChristianDateTimeForDB(dtpEndDate.FullDateTime);
				smsParser.Scope = Helper.GetGuid(hdnScopeGuid.Value.Trim('\''));
				smsParser.UserGuid = UserGuid;
				smsParser.ConditionSender = Helper.GetLocalMobileNumber(txtConditionSender.Text);
				smsParser.TypeConditionSender = Helper.GetInt(drpTypeConditionSender.SelectedValue);
				parserFormula.Condition = Helper.GetInt(drpConditions.SelectedValue);
				switch (parserFormula.Condition)
				{
					case (int)SmsFilterConditions.EqualWithPhoneBookField:
						parserFormula.Key = string.Format("{0}/{1}", hdnConditionGroupGuid.Value, hdnConditionField.Value);
						break;
					default:
						parserFormula.Key = txtCondition.Text;
						break;
				}

				switch (parserFormula.Condition)
				{
					case (int)SmsFilterConditions.NationalCode:
						break;
					default:
						if (Facade.SmsParser.IsDuplicateSmsParserKey(SmsParserGuid, smsParser.PrivateNumberGuid, parserFormula.Key))
							throw new Exception(Language.GetString("DuplicateKeyword"));
						break;
				}

				parserFormulaSerialization.Condition = Helper.GetInt(drpOperations.SelectedValue);
				switch (parserFormulaSerialization.Condition)
				{
					case (int)SmsFilterOperations.AddToGroup:
					case (int)SmsFilterOperations.RemoveFromGroup:
						parserFormulaSerialization.ReferenceGuid = Helper.GetGuid(hdnOperationGroupGuid.Value.Trim('\''));
						parserFormulaSerialization.Text = txtSmsBody.Text;
						parserFormulaSerialization.Sender = Helper.GetGuid(drpSenderNumber.SelectedValue);
						parserFormulaSerialization.VasURL = txtUrl.Text;
						break;
					case (int)SmsFilterOperations.SendSmsToGroup:
						parserFormulaSerialization.Sender = Helper.GetGuid(drpSenderNumber.SelectedValue);
						parserFormulaSerialization.Text = txtSmsBody.Text;
						parserFormulaSerialization.ReferenceGuid = Helper.GetGuid(hdnGroupGuid.Value.Trim('\''));
						break;
					case (int)SmsFilterOperations.ForwardSmsToGroup:
						parserFormulaSerialization.Sender = Helper.GetGuid(drpSenderNumber.SelectedValue);
						parserFormulaSerialization.ReferenceGuid = Helper.GetGuid(hdnOperationGroupGuid.Value.Trim('\''));
						break;
					case (int)SmsFilterOperations.SendSmsToSender:
						parserFormulaSerialization.Sender = Helper.GetGuid(drpSenderNumber.SelectedValue);
						parserFormulaSerialization.Text = txtSmsBody.Text;
						parserFormulaSerialization.ReferenceGuid = Guid.Empty;
						break;
					case (int)SmsFilterOperations.TransferToUrl:
						parserFormulaSerialization.ReferenceGuid = Helper.GetGuid(drpTrafficRelay.SelectedValue);
						break;
					case (int)SmsFilterOperations.TransferToMobile:
						parserFormulaSerialization.Sender = Helper.GetGuid(drpSenderNumber.SelectedValue);
						parserFormulaSerialization.Text = txtOpration.Text;
						break;
					case (int)SmsFilterOperations.SendSmsFromFormat:
						parserFormulaSerialization.Sender = Helper.GetGuid(drpSenderNumber.SelectedValue);
						parserFormulaSerialization.AcceptFormatGuid = Helper.GetGuid(drpAccpetFormat.SelectedValue);
						parserFormulaSerialization.RejectFormatGuid = Helper.GetGuid(drpRejectFormat.SelectedValue);
						break;
					default:
						parserFormulaSerialization.Text = txtOpration.Text;
						break;
				}
				parserFormula.ReactionExtention = SerializationTools.SerializeToXml(parserFormulaSerialization, parserFormulaSerialization.GetType());

				if (smsParser.HasError)
					throw new Exception(smsParser.ErrorMessage);

				switch (ActionType)
				{
					case "insert":
						if (!Facade.SmsParser.InsertFilter(smsParser, parserFormula))
							throw new Exception(Language.GetString("ErrorRecord"));

						break;
					case "edit":
						if (!Facade.SmsParser.UpdateFilter(smsParser, parserFormula))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
				}

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)UserControls.UI_SmsParsers_Filters_SmsFilter, Session)));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)UserControls.UI_SmsParsers_Filters_SmsFilter, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.AnalystSms);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_SmsParsers_Filters_SaveFilter;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_SmsParsers_Filters_SaveFilter.ToString());
		}
	}
}
