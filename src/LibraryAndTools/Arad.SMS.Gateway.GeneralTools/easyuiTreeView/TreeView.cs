using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GeneralTools.TreeView
{
	public delegate List<GeneralTools.TreeView.TreeNode> DataBindHandler(string parentID, string search);

	[ToolboxItem(false)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
	public class TreeView : WebControl
	{
		public string OnClick { get; set; }

		public string OnAfterEdit { get; set; }

		public string Formatter { get; set; }

		public bool ShowCheckBox { get; set; }

		public bool ShowLines { get; set; }

		public string ContextDivID { get; set; }

		public string BeforeLoad { get; set; }

		public string SuccessLoad { get; set; }

		public string Expand { get; set; }

		public string BeforeExpand { get; set; }

		protected override void RenderContents(HtmlTextWriter output)
		{
			StringBuilder tree = new StringBuilder(string.Empty);
			var getControlID = "function getControlID(queryString) {" +
													"  queryString = queryString.substring(queryString.indexOf('?'));" +
													"  queryString = queryString.substring(queryString.indexOf('c=') + 2);" +
													"  var nextParamIndex = queryString.indexOf('&');" +
													"  if (nextParamIndex != -1)" +
													"  	queryString = queryString.substring(0, nextParamIndex);" +
													"  return queryString;" +
													"}";


			string pattern = @"<script type=""text/javascript"">
														var {2}={{
																			AddItem:function(data){{
																				var selected = $('#{1}').tree('getSelected');
																				var targetNode = null;
																				if(selected) targetNode = selected.target;
																				$('#{1}').tree('append', {{
																					parent: targetNode,
																					data: data
																				}});
																			}},

																			GetSelectedNode:function(){{
																				var selected = $('#{1}').tree('getSelected');
																				var targetNode = null;
																				if(selected) targetNode = selected;
																				return targetNode;
																			}},
																			
																			GetChildren:function(target){{
																				return $('#{1}').tree('getChildren',target);
																			}},

																			Update:function(node){{
																				$('#{1}').tree('beginEdit',node.target);
																			}},
																			
																			GetParent:function(){{
																				var selected = $('#{1}').tree('getSelected');
																				var targetNode = null;
																				if(selected) targetNode = selected.target;
																				var parentNode= $('#{1}').tree('getParent',targetNode);
																				if(!parentNode) return targetNode;
																				else return parentNode;
																			}},

																			FindNode:function(id){{
																				var node = $('#{1}').tree('find', id);
																				if(node)
																					return $('#{1}').tree('select', node.target);
																				else
																					return null;
																			}},

																			Reload:function(){{
																				$('#{1}').tree('reload',null);
																			}},

																			GetChecked:function(){{
																				return $('#{1}').tree('getChecked')
																			}},

																			LoadData:function(data){{
																				return $('#{1}').tree('loadData',data);
																			}}
																		}}

														{0}
														$(document).ready(function (){{
															var queryString = $('#{1}').parents('form:first').attr('action');
															$('#{1}').tree({{
																url:'DataGridHandler.aspx/GetTreeNode',
																method:'get',
																animate:true,
																checkbox:{3},
																lines:{4},
																queryParams:{{id:'\'\'',cID:'\'{2}\'',c:""'""+getControlID(queryString)+""'"",srch:'\'\''}},
																{5}
																onClick: function(node){{
																		{6}
																}},
																onAfterEdit:function(node){{
																		{7}
																}},
																formatter:function(node){{
																	{8}
																}},
																onBeforeLoad:function(node,param){{
																	{9}
																}},
																onLoadSuccess:function(node,data){{
																	{10}
																}},
																onExpand:function(node){{
																	{11}
																}},
																onBeforeExpand:function(node){{
																	{12}
																}}
															}});
														}});

														
												 </script>
												 <ul id='{2}' class='easyui-tree'></ul>";

			tree.AppendFormat(pattern,
												getControlID,//0
												this.ClientID,//1
												this.ID,//2
												ShowCheckBox.ToString().ToLower(),//3
												ShowLines.ToString().ToLower(),//4
												GetContextMenu(),//5
												OnClick, //6
												OnAfterEdit,//7
												GetNodeFormatter(),//8
												BeforeLoad,//9
												SuccessLoad,//10
												Expand,//11
												BeforeExpand//12
												);
			output.Write(tree.ToString());
		}

		private string GetContextMenu()
		{
			if (!string.IsNullOrEmpty(ContextDivID))
			{
				string pattern = @"onContextMenu: function(e,node){{
															e.preventDefault();
															$(this).tree('select',node.target);
															$('#{0}').menu('show',{{
																	left: e.pageX,
																	top: e.pageY
															}});
													}},";
				return string.Format(pattern, ContextDivID);
			}
			else
				return string.Empty;
		}

		private string GetNodeFormatter()
		{
			if (!string.IsNullOrEmpty(Formatter))
				return Formatter;

			else
				return "return node.text;";
		}

		//#region Private Method
		//private string GetScriptResourceUrl(string scriptName)
		//{
		//	return Page.ClientScript.GetWebResourceUrl(this.GetType(), "GeneralTools.TreeView.JavaScripts." + scriptName);
		//}
		//#endregion
	}
}
