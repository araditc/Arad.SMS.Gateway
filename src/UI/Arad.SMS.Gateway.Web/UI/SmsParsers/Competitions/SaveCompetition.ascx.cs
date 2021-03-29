using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.SmsParsers.Competitions
{
	public partial class SaveCompetition : UIUserControlBase
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

		public SaveCompetition()
		{
			AddDataBinderHandlers("gridKeywords", new DataBindHandler(gridKeywords_OnDataBind));
			AddDataRenderHandlers("gridKeywords", new CellValueRenderEventHandler(gridKeywords_OnDataRender));
		}

		public DataTable gridKeywords_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			if (ActionType == "edit")
			{
				DataTable dataTableParserFormulas = Facade.ParserFormula.GetParserFormulas(SmsParserGuid);
				DataTable dtSmsParserOptions = new DataTable();
				Business.ParserFormulaSerialization parserFormulaSerialization = new Business.ParserFormulaSerialization();

				DataRow row;

				dtSmsParserOptions.Columns.Add("Guid", typeof(Guid));
				dtSmsParserOptions.Columns.Add("IsCorrect", typeof(bool));
				dtSmsParserOptions.Columns.Add("CorrectKey", typeof(bool));
				dtSmsParserOptions.Columns.Add("Title", typeof(string));
				dtSmsParserOptions.Columns.Add("Key", typeof(string));
				dtSmsParserOptions.Columns.Add("ReferenceGuid", typeof(string));

				for (int parserFormulaIndex = 0; parserFormulaIndex < dataTableParserFormulas.Rows.Count; parserFormulaIndex++)
				{
					row = dtSmsParserOptions.NewRow();

					row["Guid"] = Guid.NewGuid();
					row["IsCorrect"] = Helper.GetBool(dataTableParserFormulas.Rows[parserFormulaIndex]["IsCorrect"]);
					row["CorrectKey"] = Helper.GetBool(dataTableParserFormulas.Rows[parserFormulaIndex]["IsCorrect"]);
					row["Title"] = dataTableParserFormulas.Rows[parserFormulaIndex]["Title"].ToString();
					row["Key"] = dataTableParserFormulas.Rows[parserFormulaIndex]["Key"].ToString();

					string reactionExtension = dataTableParserFormulas.Rows[parserFormulaIndex]["ReactionExtention"].ToString();
					parserFormulaSerialization = (Business.ParserFormulaSerialization)GeneralLibrary.SerializationTools.DeserializeXml(reactionExtension, typeof(Business.ParserFormulaSerialization));

					row["ReferenceGuid"] = parserFormulaSerialization.ReferenceGuid.ToString();
					dtSmsParserOptions.Rows.Add(row);
				}

				return dtSmsParserOptions;
			}
			else
				return new DataTable();
		}

		public string gridKeywords_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format("<span class='ui-icon fa fa-trash-o red' title='{0}' onclick='deleteGridKeywordsRow(event);'></span>", Language.GetString("Delete"));
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
			btnSave.Text = Language.GetString(btnSave.Text);
			btnSave.Attributes["onclick"] = "return save();";
			btnCancel.Text = Language.GetString(btnCancel.Text);

			drpSenderNumber.DataSource = Facade.PrivateNumber.GetUserPrivateNumbersForReceive(UserGuid);
			drpSenderNumber.DataTextField = "Number";
			drpSenderNumber.DataValueField = "Guid";
			drpSenderNumber.DataBind();

			DataTable dtPhoneBook = Facade.PhoneBook.GetPhoneBookOfUser(UserGuid);
			drpPhoneBook.DataSource = dtPhoneBook;
			drpPhoneBook.DataTextField = "Name";
			drpPhoneBook.DataValueField = "Guid";
			drpPhoneBook.DataBind();
			drpPhoneBook.Items.Insert(0, new ListItem(string.Empty, Guid.Empty.ToString()));

			drpScope.DataSource = dtPhoneBook;
			drpScope.DataTextField = "Name";
			drpScope.DataValueField = "Guid";
			drpScope.DataBind();
			drpScope.Items.Insert(0, new ListItem(string.Empty, Guid.Empty.ToString()));

			dtpStartDate.Date = DateManager.GetSolarDate(DateTime.Now);
			dtpStartDate.Time = DateTime.Now.TimeOfDay.ToString();

			dtpEndDate.Date = DateManager.GetSolarDate(DateTime.Now.AddDays(1));
			dtpEndDate.Time = DateTime.Now.TimeOfDay.ToString();

			DataTable dtSender = Facade.PrivateNumber.GetUserPrivateNumbersForSend(UserGuid);
			drpDuplicatePrivateNumber.DataSource = dtSender;
			drpDuplicatePrivateNumber.DataTextField = "Number";
			drpDuplicatePrivateNumber.DataValueField = "Guid";
			drpDuplicatePrivateNumber.DataBind();
			drpDuplicatePrivateNumber.Items.Insert(0, new ListItem(Language.GetString("SenderNumber"), string.Empty));

			drpReplyPrivateNumber.DataSource = dtSender;
			drpReplyPrivateNumber.DataTextField = "Number";
			drpReplyPrivateNumber.DataValueField = "Guid";
			drpReplyPrivateNumber.DataBind();
			drpReplyPrivateNumber.Items.Insert(0, new ListItem(Language.GetString("SenderNumber"), string.Empty));

			if (ActionType == "edit")
			{
				Common.SmsParser smsParser = Facade.SmsParser.LoadSmsParser(SmsParserGuid);

				drpSenderNumber.SelectedValue = smsParser.PrivateNumberGuid.ToString();
				txtTitle.Text = smsParser.Title;
				dtpStartDate.Date = DateManager.GetSolarDate(smsParser.FromDateTime);
				dtpStartDate.Time = smsParser.FromDateTime.TimeOfDay.ToString();
				dtpEndDate.Date = DateManager.GetSolarDate(smsParser.ToDateTime);
				dtpEndDate.Time = smsParser.ToDateTime.TimeOfDay.ToString();
				drpScope.SelectedValue = smsParser.Scope.ToString();
				drpReplyPrivateNumber.SelectedValue = smsParser.ReplyPrivateNumberGuid.ToString();
				txtReplySmsText.Text = smsParser.ReplySmsText;
				drpDuplicatePrivateNumber.SelectedValue = smsParser.DuplicatePrivateNumberGuid.ToString();
				txtDuplicateUserSmsText.Text = smsParser.DuplicateUserSmsText;
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				Common.SmsParser smsParser = new Common.SmsParser();

				#region Create Table Of Options
				string keywords = hdnKeywords.Value;
				Business.ParserFormulaSerialization parserFormulaSerialization = new Business.ParserFormulaSerialization();

				DataTable dtSmsParserOptions = new DataTable();
				DataRow row;
				dtSmsParserOptions.Columns.Add("Guid", typeof(Guid));
				dtSmsParserOptions.Columns.Add("IsCorrect", typeof(bool));
				dtSmsParserOptions.Columns.Add("Title", typeof(string));
				dtSmsParserOptions.Columns.Add("Key", typeof(string));
				dtSmsParserOptions.Columns.Add("ReactionExtention", typeof(string));

				int countOptions = Helper.GetInt(Helper.ImportData(keywords, "resultCount"));
				for (int counterOptions = 0; counterOptions < countOptions; counterOptions++)
				{
					row = dtSmsParserOptions.NewRow();

					row["Guid"] = Guid.NewGuid();
					row["IsCorrect"] = Helper.ImportBoolData(keywords, ("CorrectKey" + counterOptions).ToString());
					row["Title"] = Helper.ImportData(keywords, ("Title" + counterOptions).ToString());
					row["Key"] = Helper.ImportData(keywords, ("Key" + counterOptions).ToString());

					parserFormulaSerialization.ReferenceGuid = Helper.GetGuid(Helper.ImportData(keywords, ("ReferenceGuid" + counterOptions).ToString()));
					row["ReactionExtention"] = SerializationTools.SerializeToXml(parserFormulaSerialization, parserFormulaSerialization.GetType());
					dtSmsParserOptions.Rows.Add(row);
				}

				if (dtSmsParserOptions.Rows.Count == 0)
					throw new Exception(Language.GetString("SelcectCompetitionOptions"));
				#endregion

				smsParser.PrivateNumberGuid = Helper.GetGuid(drpSenderNumber.SelectedValue);
				smsParser.Title = txtTitle.Text;
				smsParser.Type = (int)Arad.SMS.Gateway.Business.SmsParserType.Competition;
				smsParser.CreateDate = DateTime.Now;
				smsParser.FromDateTime = DateManager.GetChristianDateTimeForDB(dtpStartDate.FullDateTime);
				smsParser.ToDateTime = DateManager.GetChristianDateTimeForDB(dtpEndDate.FullDateTime);
                var dateTime = dtpStartDate.FullDateTime;
                var dateTimeEnd = dtpEndDate.FullDateTime;
                if (Session["Language"].ToString() == "fa")
                {
                    smsParser.FromDateTime = DateManager.GetChristianDateTimeForDB(dateTime);
                    smsParser.ToDateTime = DateManager.GetChristianDateTimeForDB(dateTimeEnd);
                }
                else
                {
                    smsParser.FromDateTime = DateTime.Parse(dateTime);
                    smsParser.ToDateTime = DateTime.Parse(dateTimeEnd);
                }
                smsParser.Scope = Helper.GetGuid(drpScope.SelectedValue);
				smsParser.UserGuid = UserGuid;
				smsParser.ReplyPrivateNumberGuid = Helper.GetGuid(drpReplyPrivateNumber.SelectedValue);
				smsParser.ReplySmsText = txtReplySmsText.Text;
				smsParser.DuplicatePrivateNumberGuid = Helper.GetGuid(drpDuplicatePrivateNumber.SelectedValue);
				smsParser.DuplicateUserSmsText = txtDuplicateUserSmsText.Text;

				if (smsParser.HasError)
					throw new Exception(smsParser.ErrorMessage);

				switch (ActionType)
				{
					case "insert":
						if (!Facade.SmsParser.InsertCompetition(smsParser, dtSmsParserOptions))
							throw new Exception(Language.GetString("ErrorRecord"));

						break;
					case "edit":
						smsParser.SmsParserGuid = SmsParserGuid;
						if (!Facade.SmsParser.UpdateCompetition(smsParser, dtSmsParserOptions))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
				}

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsParsers_Competitions_Competition, Session)));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsParsers_Competitions_Competition, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.Competition);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_SmsParsers_Competitions_SaveCompetition;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_SmsParsers_Competitions_SaveCompetition.ToString());
		}
	}
}
