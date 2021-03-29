using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GeneralLibrary;
using System.Data;

namespace GeneralTools.TreeView
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:TreeView runat=server></{0}:TreeView>")]
	public class TreeView : Literal
	{
		#region Properties
		private Guid rootGuid;
		private string rootTitle;
		private DataTable dataSource;
		private string parentGuidFieldName;
		private string nodeGuidFieldName;
		private string nodeTitleFieldName;
		private string childrenCountFieldName;
		private string onCheckedMethodName;
		private StringBuilder tree = new StringBuilder(string.Empty);

		public bool ShowNumberCount
		{
			get
			{
				return Helper.GetBool(ViewState["ShowNumberCount"]);
			}
			set
			{
				ViewState["ShowNumberCount"] = value;
			}
		}

		public bool ShowCheckBox
		{
			get
			{
				return Helper.GetBool(ViewState["ShowCheckBox"]);
			}
			set
			{
				ViewState["ShowCheckBox"] = value;
			}
		}

		public Guid RootGuid
		{
			get
			{
				return rootGuid;
			}
			set
			{
				rootGuid = value;
			}
		}

		public string RootTitle
		{
			get
			{
				return rootTitle;
			}
			set
			{
				rootTitle = value;
			}
		}

		public DataTable DataSource
		{
			get
			{
				return dataSource;
			}
			set
			{
				dataSource = value;
			}
		}

		public string ParentGuidFieldName
		{
			get
			{
				if (parentGuidFieldName != string.Empty)
					return parentGuidFieldName;
				else
					return "ParentGuid";
			}
			set
			{
				parentGuidFieldName = value;
			}
		}

		public string NodeGuidFieldName
		{
			get
			{
				if (nodeGuidFieldName != string.Empty)
					return nodeGuidFieldName;
				else
					return "Guid";
			}
			set
			{
				nodeGuidFieldName = value;
			}
		}

		public string NodeTitleFieldName
		{
			get
			{
				if (nodeTitleFieldName != string.Empty)
					return nodeTitleFieldName;
				else
					return "Title";
			}
			set
			{
				nodeTitleFieldName = value;
			}
		}

		public string ChildrenCountFieldName
		{
			get
			{
				if (childrenCountFieldName != string.Empty)
					return childrenCountFieldName;
				else
					return "Count";
			}
			set
			{
				childrenCountFieldName = value;
			}
		}

		public string OnCheckedMethodName
		{
			get
			{
				return onCheckedMethodName;
			}
			set
			{
				onCheckedMethodName = value;
			}
		}
		#endregion

		protected override void OnInit(EventArgs e)
		{
			this.Text = string.Empty;

			tree.Append("<div id='myList'><ul id='browser' class='filetree'>");
			tree.Append(string.Format("<li><span id='root' class='folder' guid='{0}'>{1}{2}</span>",
																																Guid.Empty,
																																ShowCheckBox ? string.Format("<input onclick='{0}(this);' type='checkbox'/>", this.OnCheckedMethodName == string.Empty ? "checkBoxControlChecked" : this.OnCheckedMethodName) : string.Empty,
																																RootTitle));

			GenerateTree(DataSource, RootGuid);

			tree.Append("</li></ul></div>");

			this.Text = tree.ToString();
			this.Mode = LiteralMode.PassThrough;
		}

		private void GenerateTree(DataTable dataSource, Guid rootGuid)
		{
			DataView dataViewPhoneBookUser = dataSource.DefaultView;
			dataViewPhoneBookUser.RowFilter = string.Format("{0}='{1}'", ParentGuidFieldName, rootGuid);
			DataTable dataTableChildren = new DataTable();
			dataTableChildren = dataViewPhoneBookUser.ToTable();
			if (dataTableChildren.Rows.Count > 0)
				tree.Append("<ul>");

			for (int childrenCounter = 0; childrenCounter < dataTableChildren.Rows.Count; childrenCounter++)
			{
				tree.Append(string.Format("<li><span class='folder' guid='{0}'>{1}{2}{3}</span>",
																			dataTableChildren.Rows[childrenCounter][NodeGuidFieldName].ToString(),
																			ShowCheckBox ? string.Format("<input onclick='{0}(this);' type='checkbox'/>", this.OnCheckedMethodName == string.Empty ? "checkBoxControlChecked" : this.OnCheckedMethodName) : string.Empty,
																			dataTableChildren.Rows[childrenCounter][NodeTitleFieldName].ToString(),
																			ShowNumberCount ? (" (" + dataTableChildren.Rows[childrenCounter][ChildrenCountFieldName].ToString() + ")") : string.Empty));

				Guid childrenGuid = Helper.GetGuid(dataTableChildren.Rows[childrenCounter]["Guid"].ToString());
				GenerateTree(dataSource, childrenGuid);
				tree.Append("</li>");
			}
			if (dataTableChildren.Rows.Count > 0)
				tree.Append("</ul>");
		}

		#region Private Method
		private string GetScriptResourceUrl(string scriptName)
		{
			return Page.ClientScript.GetWebResourceUrl(this.GetType(), "GeneralTools.TreeView.JavaScripts." + scriptName);
		}
		#endregion
	}
}
