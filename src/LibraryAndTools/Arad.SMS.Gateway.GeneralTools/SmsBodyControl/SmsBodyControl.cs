using Arad.SMS.Gateway.GeneralLibrary;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GeneralTools.SmsBodyControl
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:SmsBodyControl runat=server></{0}:SmsBodyControl>")]
	[ToolboxItem(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]

	public class SmsBodyControl : Panel
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(System.Web.HttpContext.Current.Session["UserGuid"]); }
		}

		#region Properties
		HtmlTable tblControls = new HtmlTable();
		HtmlTable tblSmsTemplates = new HtmlTable();

		UpdatePanel updatePanel = new UpdatePanel();
		TextBox txtSmsBody;
		Panel pnlSmsBody = new Panel();
		Panel pnlSmsTemplate = new Panel();
		LinkButton btnSmsTemplate = new LinkButton();
		LinkButton btnReturn = new LinkButton();
		LinkButton btnErasor = new LinkButton();
		GridView gridTemplates = new GridView();
		HiddenField hdnSmsText = new HiddenField();

		HtmlTableRow rowSmsTemplate = new HtmlTableRow();
		HtmlTableRow rowShowInfoTop = new HtmlTableRow();
		HtmlTableRow rowShowInfo = new HtmlTableRow();
		HtmlTableRow rowSmsBody = new HtmlTableRow();

		HtmlTableCell cellSmsTemplate = new HtmlTableCell();
		HtmlTableCell cellTemplateToolbar = new HtmlTableCell();
		HtmlTableCell cellSmsInfoTop = new HtmlTableCell();
		HtmlTableCell cellSmsInfo = new HtmlTableCell();
		HtmlTableCell cellSmsBody = new HtmlTableCell();
		HtmlTableCell cellToolbar = new HtmlTableCell();
		HtmlTableCell cellChangeNumber = new HtmlTableCell();

		public static string smsText = string.Empty;

		public string Text
		{
			get
			{
				if (this.Page.IsPostBack)
					if (this.Page.Request[GetRequestParameterName(this.SmsBodyTextBoxID)] != null)
						ViewState["Text"] = this.Page.Request[GetRequestParameterName(this.SmsBodyTextBoxID)].Replace("\r", "");

				if (this.CssClass.ToLower() == "numberinput")
					return Helper.GetString(ViewState["Text"]).Replace(",", string.Empty);
				else
					return Helper.GetString(ViewState["Text"]);
			}
			set
			{
				ViewState["Text"] = value;
			}
		}

		public int SmsCount
		{
			get
			{
				return Helper.GetSmsCount(this.Text);
			}
		}

		public new int Width
		{
			set
			{
				ViewState["Width"] = value;
			}
			get
			{
				if (ViewState["width"] == null)
					return 100;

				return Helper.GetInt(ViewState["Width"]);
			}
		}

		public bool IsRequired
		{
			set
			{
				ViewState["IsRequired"] = value;
			}
			get
			{
				return Helper.GetBool(ViewState["IsRequired"]);
			}
		}

		public string ValidationSet
		{
			set
			{
				ViewState["ValidationSet"] = value;
			}
			get
			{
				return Helper.GetString(ViewState["ValidationSet"]);
			}
		}

		private string IsFlashCheckBoxID
		{
			get
			{
				return this.ClientID.Replace("_", string.Empty) + "chkIsFlash";
			}
		}

		public bool IsFlashSms
		{
			set
			{
				ViewState["IsFlashSms"] = value;
			}
			get
			{
				if (this.Page.IsPostBack)
					ViewState["IsFlashSms"] = this.Page.Request[GetRequestParameterName(this.IsFlashCheckBoxID)];

				return Helper.GetBool(ViewState["IsFlashSms"]);
			}
		}

		public bool ChangeNumberToPersian
		{
			set
			{
				ViewState["ChangeNumberToPersian"] = value;
			}
			get
			{
				return Helper.GetBool(ViewState["ChangeNumberToPersian"]);
			}
		}

		private string SmsBodyTextBoxID
		{
			get
			{
				return this.ClientID.Replace("_", string.Empty) + "TxtSmsBody";
			}
		}

		private string SmsInfoCell
		{
			get
			{
				return this.ClientID.Replace("_", string.Empty) + "SmsInfoCell";
			}
		}

		private string SmsInfoCellTop
		{
			get
			{
				return this.ClientID.Replace("_", string.Empty) + "SmsInfoCellTop";
			}
		}

		private string ChangeNumberCell
		{
			get
			{
				return this.ClientID.Replace("_", string.Empty) + "ChangeNumberCell";
			}
		}

		private string ChangeNumberCheckBox
		{
			get
			{
				return this.ClientID.Replace("_", string.Empty) + "chkChangeNumber";
			}
		}
		#endregion

		public GridView GetnerateTemplateGrid()
		{
			gridTemplates.HeaderStyle.BackColor = System.Drawing.Color.CornflowerBlue;
			gridTemplates.HeaderStyle.Font.Bold = true;
			gridTemplates.HeaderStyle.ForeColor = System.Drawing.Color.White;
			gridTemplates.CellPadding = 5;
			gridTemplates.AutoGenerateColumns = false;
			gridTemplates.Width = Unit.Percentage(100);

			BoundField nameColumn = new BoundField();
			nameColumn.DataField = "Body";
			nameColumn.HeaderText = Language.GetString("Text");
			nameColumn.ItemStyle.Width = Unit.Percentage(80);
			gridTemplates.Columns.Add(nameColumn);

			TemplateField TmpCol = new TemplateField();
			TmpCol.HeaderText = string.Empty;
			gridTemplates.Columns.Add(TmpCol);
			TmpCol.ItemTemplate = new TemplateHandler();

			gridTemplates.RowDataBound += new GridViewRowEventHandler(gridTemplates_RowDataBound);

			return gridTemplates;
		}

		private void gridTemplates_RowDataBound(Object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
				((LinkButton)e.Row.FindControl("select")).CommandName = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Body"));
		}

		protected override void OnInit(EventArgs e)
		{
			this.Controls.Clear();
			this.Attributes.Clear();

			cellToolbar.Width = "20px";
			cellToolbar.VAlign = "top";

			cellTemplateToolbar.Width = "20px";
			cellTemplateToolbar.VAlign = "top";

			cellSmsTemplate.VAlign = "top";

			this.Attributes["BasicID"] = this.ID;

			Literal ltrScript = new Literal();
			ltrScript.Text = GenerateJavaScripts();
			ltrScript.Mode = LiteralMode.PassThrough;
			this.Controls.Add(ltrScript);

			cellSmsInfoTop.InnerHtml = string.Format(@"<div class='form-group'>
																										<div class='input-group' style='width:96%;padding-right:3%'>
																											<span class='input-group-addon' style='background-color: rgb(80, 168, 226); color: #fff'>متن پیامک</span>
																											<div class='form-control' id='{0}'></div>
																										</div>
																									</div>", this.SmsInfoCellTop);
			cellSmsInfoTop.ColSpan = 3;

			txtSmsBody = new TextBox();
			txtSmsBody.ID = this.SmsBodyTextBoxID;
			txtSmsBody.ClientIDMode = ClientIDMode.Static;
			txtSmsBody.Text = this.Text;
			txtSmsBody.TextMode = TextBoxMode.MultiLine;
			txtSmsBody.Rows = 8;
			txtSmsBody.Width = Unit.Percentage(this.Width);
			txtSmsBody.CssClass = "form-control input-sm";
			txtSmsBody.Attributes["isRequired"] = this.IsRequired.ToString().ToLower();
			if (this.ValidationSet != string.Empty)
				txtSmsBody.Attributes["validationSet"] = this.ValidationSet;
			txtSmsBody.Attributes["onkeyup"] = GenerateCalculateSmsCountJavaScripts(false);
			txtSmsBody.Attributes["onblur"] = GenerateCalculateSmsCountJavaScripts(false);
			txtSmsBody.Attributes["onfocus"] = GenerateCalculateSmsCountJavaScripts(false);
			txtSmsBody.Attributes["onmouseup"] = GenerateCalculateSmsCountJavaScripts(false);

			cellSmsBody.Controls.Add(txtSmsBody);
			cellSmsBody.ColSpan = 2;

			CheckBox chkIsFlashSms = new CheckBox();
			chkIsFlashSms.ID = this.IsFlashCheckBoxID;
			chkIsFlashSms.Attributes["name"] = this.IsFlashCheckBoxID;
			chkIsFlashSms.ClientIDMode = ClientIDMode.Static;
			chkIsFlashSms.Text = Translate("SendAsFlashSms");
			chkIsFlashSms.Checked = IsFlashSms;
			chkIsFlashSms.Font.Bold = true;

			cellSmsInfo.Controls.Add(chkIsFlashSms);
			cellSmsInfo.ID = this.SmsInfoCell;
			cellSmsInfo.Width = "150px";
			cellSmsInfo.Height = "30px";
			cellSmsInfo.ClientIDMode = ClientIDMode.Static;

			btnSmsTemplate.ID = "btnSmsTemplate";
			btnSmsTemplate.ToolTip = Language.GetString("SmsPattern");
			btnSmsTemplate.Text = "";
			btnSmsTemplate.CssClass = "ui-icon fa fa-2x fa-file-text-o";
			btnSmsTemplate.Click += (sender, eventArgs) =>
			{
                Arad.SMS.Gateway.Business.SmsTemplate smsTemplateController = new Arad.SMS.Gateway.Business.SmsTemplate();
				gridTemplates.DataSource = smsTemplateController.GetUserSmsTemplates(UserGuid);
				gridTemplates.DataBind();
				hdnSmsText.Value = txtSmsBody.Text;
				pnlSmsBody.Visible = false;
				pnlSmsTemplate.Visible = true;
			};

			btnErasor.ID = "btnErasor";
			btnErasor.ToolTip = Language.GetString("Delete");
			btnErasor.Text = "";
			btnErasor.CssClass = "ui-icon fa fa-2x fa-eraser red";
			btnErasor.Click += (sender, eventArgs) =>
			{
				txtSmsBody.Text = string.Empty;
				txtSmsBody.Focus();
			};

			cellToolbar.Controls.Add(btnSmsTemplate);
			cellToolbar.Controls.Add(btnErasor);

			btnReturn.ID = "btnReturn";
			btnReturn.Text = "";
			btnReturn.ToolTip = Language.GetString("Return");
			btnReturn.CssClass = "ui-icon fa fa-2x fa-chevron-left red";
			btnReturn.Click += (sender, eventArgs) =>
			{
				txtSmsBody.Text += !string.IsNullOrEmpty(hdnSmsText.Value) ? hdnSmsText.Value : string.Empty;
				txtSmsBody.Text += !string.IsNullOrEmpty(smsText) ? smsText : hdnSmsText.Value;
				smsText = string.Empty;
				pnlSmsBody.Visible = true;
				pnlSmsTemplate.Visible = false;
				txtSmsBody.Focus();
			};
			cellTemplateToolbar.Controls.Add(btnReturn);

			CheckBox chkChangeNumber = new CheckBox();
			chkChangeNumber.ID = this.ChangeNumberCheckBox;
			chkChangeNumber.ClientIDMode = ClientIDMode.Static;
			chkChangeNumber.Text = Translate("ChangeNumberToPersian");
			chkChangeNumber.Checked = ChangeNumberToPersian;
			chkChangeNumber.Attributes["onclick"] = GenerateCalculateSmsCountJavaScripts(false);
			chkChangeNumber.Font.Bold = true;

			cellChangeNumber.Controls.Add(chkChangeNumber);
			cellChangeNumber.ID = this.ChangeNumberCell;
			cellChangeNumber.Height = "30px";
			cellChangeNumber.ClientIDMode = ClientIDMode.Static;

			rowSmsBody.Controls.Add(cellSmsBody);
			rowSmsBody.Controls.Add(cellToolbar);
			rowShowInfo.Controls.Add(cellSmsInfo);
			rowShowInfo.Controls.Add(cellChangeNumber);
			rowShowInfoTop.Controls.Add(cellSmsInfoTop);

			tblControls.CellPadding = 0;
			tblControls.CellSpacing = 0;
			tblControls.Style.Add(HtmlTextWriterStyle.Width, "100%");
			tblControls.Style.Add(HtmlTextWriterStyle.Height, "250px");

			tblSmsTemplates.CellPadding = 0;
			tblSmsTemplates.CellSpacing = 0;
			tblSmsTemplates.Style.Add(HtmlTextWriterStyle.Width, "100%");
			tblSmsTemplates.Style.Add(HtmlTextWriterStyle.Height, "250px");

			tblControls.Controls.Add(rowShowInfoTop);
			tblControls.Controls.Add(rowSmsBody);
			tblControls.Controls.Add(rowShowInfo);

			pnlSmsBody.Visible = true;
			pnlSmsTemplate.Visible = false;

			Label lbl = new Label();

			cellSmsTemplate.Controls.Add(gridTemplates);
			cellSmsTemplate.Style.Add(HtmlTextWriterStyle.BorderStyle, "1 px solid silver");
			cellSmsTemplate.ColSpan = 2;

			rowSmsTemplate.Controls.Add(cellSmsTemplate);
			rowSmsTemplate.Controls.Add(cellTemplateToolbar);
			tblSmsTemplates.Controls.Add(rowSmsTemplate);

			pnlSmsTemplate.ScrollBars = ScrollBars.Vertical;
			pnlSmsTemplate.Height = Unit.Pixel(250);
			pnlSmsTemplate.Controls.Add(tblSmsTemplates);

			pnlSmsBody.Controls.Add(tblControls);
			pnlSmsBody.Controls.Add(hdnSmsText);

			updatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
			updatePanel.ID = "updatePanelSmsBody";
			updatePanel.ChildrenAsTriggers = true;
			updatePanel.ContentTemplateContainer.Controls.Add(pnlSmsBody);
			updatePanel.ContentTemplateContainer.Controls.Add(pnlSmsTemplate);

			GetnerateTemplateGrid();

			this.Controls.Add(updatePanel);

			Literal ltrInlineScripts = new Literal();
			ltrInlineScripts.Text = GenerateCalculateSmsCountJavaScripts(true);
			ltrInlineScripts.Mode = LiteralMode.PassThrough;
			this.Controls.Add(ltrInlineScripts);

			base.OnInit(e);
		}

		private string GetImageResourceUrl(string imageName)
		{
			return Page.ClientScript.GetWebResourceUrl(this.GetType(), "GeneralTools.SmsBodyControl.Images." + imageName);
		}

		protected override void OnLoad(EventArgs e)
		{
			txtSmsBody.Text = this.Text;

			base.OnLoad(e);
		}

		protected override void OnPreRender(EventArgs e)
		{
			if (!string.IsNullOrEmpty(smsText))
			{
				txtSmsBody.Text += !string.IsNullOrEmpty(hdnSmsText.Value) ? hdnSmsText.Value : string.Empty;
				txtSmsBody.Text += !string.IsNullOrEmpty(smsText) ? smsText : hdnSmsText.Value;
				smsText = string.Empty;
				pnlSmsBody.Visible = true;
				pnlSmsTemplate.Visible = false;
				txtSmsBody.Focus();
			}
			base.OnPreRender(e);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		#region Private Method
		private string GenerateJavaScripts()
		{
			string scriptHeader = "<script type=\"text/javascript\" src=\"{0}\"></script>";
			string javaScripts = string.Empty;

			javaScripts += string.Format(scriptHeader, GetScriptResourceUrl(GeneralTools.SmsBodyControl.SmsBodyControlResources.Scripts_SmsCalculations));

			return javaScripts;
		}

		private string GenerateCalculateSmsCountJavaScripts(bool attachScriptTag)
		{
			string inLineScript = string.Format("changeNumber($('#{0}')[0], $('#{1}')[0]);calculateSmsCount('{1}','{2}','{3}','{4}');", this.ChangeNumberCheckBox, this.SmsBodyTextBoxID, this.SmsInfoCellTop, Translate("CharacterCount"), Translate("Sms"));

			if (attachScriptTag)
				return string.Format("<script type=\"text/javascript\">{0}</script>", inLineScript);
			else
				return inLineScript;
		}

		private string GetScriptResourceUrl(string scriptName)
		{
			return Page.ClientScript.GetWebResourceUrl(this.GetType(), "GeneralTools.SmsBodyControl.Scripts." + scriptName);
		}

		private string GetRequestParameterName(string parameter)
		{
			return this.ClientID.Split(new string[] { this.ID }, StringSplitOptions.None)[0].Replace("_", "$") + parameter;
		}

		private string Translate(string value)
		{
			return Language.GetString(value);
		}
		#endregion
	}

	public class TemplateHandler : ITemplate
	{
		void ITemplate.InstantiateIn(Control container)
		{
			LinkButton cmd = new LinkButton();
			cmd.ID = "select";
			cmd.Text = Language.GetString("Selection");
			cmd.Click += new EventHandler(Dynamic_Method);
			container.Controls.Add(cmd);
		}

		protected void Dynamic_Method(object sender, EventArgs e)
		{
			SmsBodyControl.smsText = ((LinkButton)sender).CommandName;
		}
	}
}
