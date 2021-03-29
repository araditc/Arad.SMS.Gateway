using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arad.SMS.Gateway.GeneralLibrary;
using System.Web.UI.HtmlControls;

namespace GeneralTools.SmsBodyBox
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:SmsBodyBox runat=server></{0}:SmsBodyBox>")]
	[ToolboxItem(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]

	public class SmsBodyBox : Panel
	{
		#region Properties
		private TextBox txtSmsBody;
		private string toolbarItems;

		HtmlTable tblControls = new HtmlTable();

		HtmlTableRow rowShowInfo = new HtmlTableRow();
		HtmlTableRow rowSmsBody = new HtmlTableRow();
		HtmlTableCell cellSmsInfo = new HtmlTableCell();
		HtmlTableCell cellSmsBody = new HtmlTableCell();
		HtmlTableCell cellToolbar = new HtmlTableCell();
		HtmlTableCell cellChangeNumber = new HtmlTableCell();

		public string Text
		{
			get
			{
				if (this.Page.IsPostBack)
					ViewState["Text"] = this.Page.Request[GetRequestParameterName(this.SmsBodyTextBoxID)];

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

		public string PlaceHolder { get; set; }

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
				if (ViewState["Width"] == null)
					return 230;

				return Helper.GetInt(ViewState["Width"]);
			}
		}

		public new int Height
		{
			set
			{
				ViewState["Height"] = value;
			}
			get
			{
				if (ViewState["Height"] == null)
					return 180;

				return Helper.GetInt(ViewState["Height"]);
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

		public bool ShowToolbar
		{
			set
			{
				ViewState["ShowToolbar"] = value;
			}
			get
			{
				return Helper.GetBool(ViewState["ShowToolbar"]);
			}
		}

		public string ToolbarItems
		{
			set
			{
				toolbarItems = value;
			}
			get
			{
				return toolbarItems;
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

		public bool ShowChangeNumberToPersian { get; set; }

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

		protected override void OnInit(EventArgs e)
		{
			this.Controls.Clear();
			this.Attributes.Clear();

			this.Attributes["BasicID"] = this.ID;

			Literal ltrScript = new Literal();
			ltrScript.Text = GenerateJavaScripts();
			ltrScript.Mode = LiteralMode.PassThrough;
			this.Controls.Add(ltrScript);

			txtSmsBody = new TextBox();
			txtSmsBody.ID = this.SmsBodyTextBoxID;
			txtSmsBody.ClientIDMode = ClientIDMode.Static;
			txtSmsBody.Text = this.Text;
			txtSmsBody.TextMode = TextBoxMode.MultiLine;
			txtSmsBody.Rows = 8;
			txtSmsBody.Width = Unit.Pixel(this.Width);
			txtSmsBody.Height = Unit.Pixel(this.Height);
			txtSmsBody.CssClass = "input";
			txtSmsBody.Attributes["isRequired"] = this.IsRequired.ToString().ToLower();
			if (this.ValidationSet != string.Empty)
				txtSmsBody.Attributes["validationSet"] = this.ValidationSet;
			txtSmsBody.Attributes["onkeyup"] = GenerateCalculateSmsCountJavaScripts(false);
			txtSmsBody.Attributes["onblur"] = GenerateCalculateSmsCountJavaScripts(false);
			txtSmsBody.Attributes["onfocus"] = GenerateCalculateSmsCountJavaScripts(false);
			txtSmsBody.Attributes["onmouseup"] = GenerateCalculateSmsCountJavaScripts(false);
			txtSmsBody.Attributes["placeholder"] = PlaceHolder;

			cellSmsBody.Controls.Add(txtSmsBody);
			cellSmsBody.ColSpan = 2;

			cellSmsInfo.ID = this.SmsInfoCell;
			cellSmsInfo.Width = "120px";
			cellSmsInfo.Height = "30px";
			cellSmsInfo.ClientIDMode = ClientIDMode.Static;
			if (!ShowChangeNumberToPersian)
				cellSmsInfo.ColSpan = 2;

			rowSmsBody.Controls.Add(cellSmsBody);
			rowShowInfo.Controls.Add(cellSmsInfo);

			if (ShowChangeNumberToPersian)
			{
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
				rowShowInfo.Controls.Add(cellChangeNumber);
			}

			tblControls.CellPadding = 0;
			tblControls.CellSpacing = 0;
			tblControls.Style.Add(HtmlTextWriterStyle.FontSize, "9px");
            if (Context.Session["Language"].ToString() == "fa")
            {
                tblControls.Style.Add(HtmlTextWriterStyle.Direction, "rtl");
            }else
            if (Context.Session["Language"].ToString() == "en")
            {
                tblControls.Style.Add(HtmlTextWriterStyle.Direction, "ltr");
            }

            tblControls.Controls.Add(rowSmsBody);
			tblControls.Controls.Add(rowShowInfo);

			this.Controls.Add(tblControls);

			Literal ltrInlineScripts = new Literal();
			ltrInlineScripts.Text = GenerateCalculateSmsCountJavaScripts(true);
			ltrInlineScripts.Mode = LiteralMode.PassThrough;
			this.Controls.Add(ltrInlineScripts);

			base.OnInit(e);
		}

		protected override void OnLoad(EventArgs e)
		{
			txtSmsBody.Text = this.Text;

			base.OnLoad(e);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			cellToolbar.Width = "35px";
			cellToolbar.VAlign = "top";
			cellToolbar.InnerHtml = ToolbarItems;
			rowSmsBody.Controls.Add(cellToolbar);

			base.Render(writer);
		}
		#region Private Method
		private string GenerateJavaScripts()
		{
			string scriptHeader = "<script type=\"text/javascript\" src=\"{0}\"></script>";
			string javaScripts = string.Empty;

			javaScripts += string.Format(scriptHeader, GetScriptResourceUrl(GeneralTools.SmsBodyBox.SmsBodyBoxResources.JavaScript_SmsCalculations));

			return javaScripts;
		}

		private string GenerateCalculateSmsCountJavaScripts(bool attachScriptTag)
		{
			string inLineScript = string.Empty;
			if (ShowChangeNumberToPersian)
				inLineScript = string.Format("cahngeNumber($('#{0}')[0], $('#{1}')[0]);calculateSmsCount('{1}','{2}','{3}','{4}');", this.ChangeNumberCheckBox, this.SmsBodyTextBoxID, this.SmsInfoCell, Translate("Character"), Translate("Sms"));
			else
				inLineScript = string.Format("calculateSmsCount('{1}','{2}','{3}','{4}');", this.ChangeNumberCheckBox, this.SmsBodyTextBoxID, this.SmsInfoCell, Translate("Character"), Translate("Sms"));

			if (attachScriptTag)
				return string.Format("<script type=\"text/javascript\">{0}</script>", inLineScript);
			else
				return inLineScript;
		}

		private string GetScriptResourceUrl(string scriptName)
		{
			return Page.ClientScript.GetWebResourceUrl(this.GetType(), "GeneralTools.SmsBodyBox.JavaScripts." + scriptName);
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
}
