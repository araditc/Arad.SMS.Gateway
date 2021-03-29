using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arad.SMS.Gateway.GeneralLibrary;

namespace GeneralTools.SearchBox
{
	[ToolboxItem(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
	public class SearchBox : Panel
	{
		#region Properties
		public string Text
		{
			get
			{
				if (this.Page.IsPostBack)
					ViewState["Text"] = this.Page.Request[GetRequestParameterName(this.SearchTextBoxID)];

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

		public string Value
		{
			get
			{
				if (this.Page.IsPostBack)
					ViewState["Value"] = this.Page.Request[GetRequestParameterName(this.SearchValuBoxID)];

				return Helper.GetString(ViewState["Value"]);
			}
			set
			{
				ViewState["Value"] = value;
			}
		}

		public override string CssClass
		{
			get
			{
				return Helper.GetString(ViewState["CssClass"]);
			}
			set
			{
				ViewState["CssClass"] = value;
			}
		}

		public int MinChar
		{
			get
			{
				int minChar = Helper.GetInt(ViewState["MinChar"]);
				return (minChar == 0 ? 2 : minChar);
			}
			set
			{
				ViewState["MinChar"] = value;
			}
		}

		public bool Caching
		{
			get
			{
				return Helper.GetBool(ViewState["Cache"]);
			}
			set
			{
				ViewState["Cache"] = value;
			}
		}

		public string SearchMethod
		{
			get
			{
				return Helper.GetString(ViewState["SearchMethod"]);
			}
			set
			{
				ViewState["SearchMethod"] = value;
			}
		}

		public int MaxItemsToShow
		{
			get
			{
				if (ViewState["MaxItemsToShow"] == null)
					return -1;
				return Helper.GetInt(ViewState["MaxItemsToShow"]);
			}
			set
			{
				ViewState["MaxItemsToShow"] = value;
			}
		}

		public bool AutoFormatDecimal
		{
			get
			{
				return Helper.GetBool(ViewState["AutoFormatDecimal"]);
			}
			set
			{
				ViewState["AutoFormatDecimal"] = value;
			}
		}

		public bool AllowDecimal
		{
			get
			{
				return Helper.GetBool(ViewState["AllowDecimal"]);
			}
			set
			{
				ViewState["AllowDecimal"] = value;
			}
		}

		public bool AllowNegativeSign
		{
			get
			{
				return Helper.GetBool(ViewState["AllowNegativeSign"]);
			}
			set
			{
				ViewState["AllowNegativeSign"] = value;
			}
		}

		public bool AllowSpaces
		{
			get
			{
				return Helper.GetBool(ViewState["AllowSpaces"]);
			}
			set
			{
				ViewState["AllowSpaces"] = value;
			}
		}

		public bool AllowCommas
		{
			get
			{
				return Helper.GetBool(ViewState["AllowCommas"]);
			}
			set
			{
				ViewState["AllowCommas"] = value;
			}
		}

		public bool AvoidSelection
		{
			get
			{
				return Helper.GetBool(ViewState["AvoidSelection"]);
			}
			set
			{
				ViewState["AvoidSelection"] = value;
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

		private string SearchUrl
		{
			get
			{
				return string.Format("ClientRequestHandlers.aspx?method={0}", SearchMethod);
			}
		}

		private string SearchTextBoxID
		{
			get
			{
				return this.ClientID.Replace("_", string.Empty) + "TxtSearch";
			}
		}

		private string SearchValuBoxID
		{
			get
			{
				return this.ClientID.Replace("_", string.Empty) + "HdnSearch";
			}
		}
		#endregion

		protected override void OnInit(EventArgs e)
		{
			this.Controls.Clear();
			this.Attributes.Clear();

			this.Attributes["BasicID"] = this.ID;

			Literal ltrScript = new Literal();
			ltrScript.Text = GenerateStyleAndJavaScripts() + GenerateInlineScripts();
			ltrScript.Mode = LiteralMode.PassThrough;
			this.Controls.Add(ltrScript);

			HiddenField hdnSearch = new HiddenField();
			hdnSearch.ID = this.SearchValuBoxID;
			hdnSearch.ClientIDMode = ClientIDMode.Static;
			hdnSearch.Value = this.Value;
			this.Controls.Add(hdnSearch);

			TextBox txtSearch = new TextBox();
			txtSearch.ID = this.SearchTextBoxID;
			txtSearch.ClientIDMode = ClientIDMode.Static;
			txtSearch.Text = this.Text;
			txtSearch.CssClass = this.CssClass;

			txtSearch.Attributes["isRequired"] = this.IsRequired.ToString().ToLower();
			if (this.ValidationSet != string.Empty)
			txtSearch.Attributes["validationSet"] = this.ValidationSet;

			if (this.CssClass.ToLower() == "numberinput")
			{
				txtSearch.Attributes["AutoFormatDecimal"] = this.AutoFormatDecimal.ToString().ToLower();
				txtSearch.Attributes["AllowNegativeSign"] = this.AllowNegativeSign.ToString().ToLower();
				txtSearch.Attributes["AllowDecimal"] = this.AllowDecimal.ToString().ToLower();
				txtSearch.Attributes["AllowSpaces"] = this.AllowSpaces.ToString().ToLower();
				txtSearch.Attributes["AllowCommas"] = this.AllowCommas.ToString().ToLower();
				txtSearch.Attributes["AvoidSelection"] = this.AvoidSelection.ToString().ToLower();
			}
			this.Controls.Add(txtSearch);

			base.OnInit(e);
		}

		#region Private Method
		private string GenerateInlineScripts()
		{
			string scriptPattern = @"<script>
																$(document).ready(function() {{
																		$('#{0}').autocomplete('{1}',
																			{{ 
																				minChars:{2}, 
																				width:{3},
																				cacheLength: {4},
																				maxItemsToShow: {5},
																				delay: 1,
																				selectFirst: true,
																				onItemSelect: function(item){{ 
																														$('#{6}')[0].value = item.extra[0];
																														$('#{0}')[0].focus();
																											}}
																			}});
																}});
															</script>";

			return string.Format(scriptPattern,
															this.SearchTextBoxID,
															this.SearchUrl,
															this.MinChar,
															this.Width.Value,
															this.Caching ? "1000" : "0",
															this.MaxItemsToShow,
															this.SearchValuBoxID
															).Replace("\t", string.Empty);
		}

		private string GenerateStyleAndJavaScripts()
		{
			string styleHeader = "<link rel=\"stylesheet\" type=\"text/css\" media=\"screen\" href=\"{0}\" />";
			string scriptHeader = "<script type=\"text/javascript\" src=\"{0}\"></script>";
			string cssStyles = string.Empty;
			string javaScripts = string.Empty;

			cssStyles = string.Format(styleHeader, GetStyleResourceUrl(GeneralTools.SearchBox.SearchBoxResources.Css_Styles));
			javaScripts += string.Format(scriptHeader, GetScriptResourceUrl(GeneralTools.SearchBox.SearchBoxResources.JavaScript_Jquery_Autocomplete));

			return javaScripts + cssStyles;
		}

		private string GetScriptResourceUrl(string scriptName)
		{
			return Page.ClientScript.GetWebResourceUrl(this.GetType(), "GeneralTools.SearchBox.JavaScripts." + scriptName);
		}

		private string GetStyleResourceUrl(string styleSheetName)
		{
			return Page.ClientScript.GetWebResourceUrl(this.GetType(), "GeneralTools.SearchBox.Css." + styleSheetName);
		}

		private string GetRequestParameterName(string parameter)
		{
			return this.ClientID.Split(new string[] { this.ID }, StringSplitOptions.None)[0].Replace("_", "$") + parameter;
		}
		#endregion
	}
}
