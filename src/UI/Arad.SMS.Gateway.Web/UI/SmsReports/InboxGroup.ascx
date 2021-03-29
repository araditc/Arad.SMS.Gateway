<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InboxGroup.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsReports.InboxGroup" %>

<style type="text/css">
	.rtl .menu-text {
		height: 20px;
		line-height: 20px;
		float: left;
		padding-left: 28px;
	}
    .rtl .panel {
        text-align: right;
    }
	.rtl .menu-icon {
        left: auto; 
        right: 2px;
    }
    .rtl .menu-line {
        left: auto;
        right: 26px;
    }
	.panel-body {
		padding: 0;
	}
    .panel {
        text-align: left;
    }
    .menu-icon {
        right: auto;
        left: 2px;
    }
    .menu-line {
        right: auto;
        left: 26px;
    }
    .menu-text {
        height: 20px;
        line-height: 20px;
        float: right;
        padding-right: 10px;
    }
</style>
<script type="text/javascript">
	var seachIsActive = false;
	function add() {
        var name = '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NewGroup") %>';
		var selectedNode = treeGroups.GetSelectedNode();
		var parentId = selectedNode ? selectedNode.id : null;
		var retVal = getAjaxResponse('InsertItemInInboxGroup', "ParentGuid=" + parentId + "&Name=" + name);
		if (importData(retVal, 'Result') == 'OK') {
			var data = { id: importData(retVal, 'Guid'), text: name, state: 'closed', children: null };
			treeGroups.AddItem(data);
			treeGroups.FindNode(data.id);
			var selectedNode = treeGroups.GetSelectedNode();
			treeGroups.Update(selectedNode);
		}
		else {
			$.messager.alert('Error', importData(retVal, 'Message'));
		}
	}

	function clickNode(node) {
		$("#<%=frm.ClientID%>")[0].src = "/PageLoader.aspx?c=" + '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_Inbox, Session)%>' + "&GroupGuid=" + node.id;
	}

	function beginUpdate() {
		var selectedNode = treeGroups.GetSelectedNode();
		treeGroups.Update(selectedNode);
	}

	function udateNode(node) {
		if (node) {
			var parent = treeGroups.GetParent();
			var retVal = getAjaxResponse('EditItemInInboxGroup', "GroupGuid=" + node.id + "&ParentGuid=" + parent.id + "&Name=" + node.text);
			if (importData(retVal, 'Result') != 'OK') {
				$.messager.alert('Error', importData(retVal, 'Message'));
			}
		}
	}

	function nodeTextFormat(node) {
		var text = node.text;
		if (node.attributes) {
			text += '&nbsp;<span style=\'color:blue\'>(' + node.attributes.count + ')</span>';
		}
		return text;
	}

	function setParam(node, param) {
		if ($("#txtSearch")[0].value && seachIsActive) {
			param.srch = "'" + $("#txtSearch")[0].value + "'";
		}
		seachIsActive = false;
	}

	function searchTree() {
		seachIsActive = true;
		treeGroups.Reload();
	}

	function deleteNode() {
		var selectedNode = treeGroups.GetSelectedNode();
		if (selectedNode) {
            $.messager.confirm('Delete', '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete") %>', function (result) {
				if (result) {
					if (selectedNode.children) {
						$.messager.alert('Error', '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupOfPhoneBookHasSubGroupError")%>');
						return false;
					}
					var retVal = getAjaxResponse('DeleteItemFromInboxGroup', "GroupGuid=" + selectedNode.id);
					if (importData(retVal, 'Result') == 'OK') {
						treeGroups.Reload();
					}
					else {
						$.messager.alert('Error', importData(retVal, 'Message'));
						return false;
					}
				}
			});
		}
	}
</script>

<% if(Session["Language"].ToString() == "en") { %>
<div class="row" style="direction: ltr">
<% }  else
   {%>
<div class="row" style="direction: rtl">
<%  } %>

	<div class="col-md-12">
		<div class="easyui-layout" style="height: 900px;">
			<div data-options="region:'east',split:true" class="col-md-2 col-xs-4" style="padding: 0px;">
				<div class="easyui-panel" title=" " style="overflow: auto; padding: 5px;" data-options="tools:'#tools'">
					<div class="form-group">
						<input type="text" class="form-control input-sm" id="txtSearch" placeholder="Search">
					</div>
					<GeneralTools:TreeView ID="treeGroups" runat="server" OnClick="clickNode(node);" ShowLines="true" ContextDivID="menu"
						OnAfterEdit="udateNode(node);" Formatter="return nodeTextFormat(node);" BeforeLoad="setParam(node,param);"></GeneralTools:TreeView>
				</div>
			</div>
			<div data-options="region:'center',title:'<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Desktop") %>',iconCls:'icon-ok'">
				<iframe id="frm" style="border: 0px; width: 100%; height: 820px; overflow: auto;" runat="server"></iframe>
			</div>
		</div>
	</div>
</div>

<div id="tools">
	<a href="javascript:void(0)" class="fa fa-plus green" onclick="add();"></a>
	<a href="javascript:void(0)" class="fa fa-pencil orange" onclick="beginUpdate();"></a>
	<a href="javascript:void(0)" class="fa fa-times red" onclick="deleteNode();"></a>
	<a href="javascript:void(0)" class="fa fa-search blue" style="font-size: 17px;" onclick="searchTree();"></a>
	<a href="javascript:void(0)" class="fa fa-info-circle easyui-tooltip" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PhoneBookNotice")%>"></a>
</div>

<div id="menu" class="easyui-menu" style="width: 120px;">
	<div onclick="add();" data-options="iconCls:'fa fa-plus fa-2x green'"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AddGroup") %></div>
	<div onclick="beginUpdate();" data-options="iconCls:'fa fa-pencil orange'"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Edit") %></div>
	<div onclick="deleteNode()" data-options="iconCls:'fa fa-times red'"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Delete") %></div>
</div>
