<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhoneBook.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PhoneBooks.PhoneBook" %>

<style type="text/css">
	.menu-text {
		height: 20px;
		line-height: 20px;
		float: right;
		padding-right: 28px;
	}

	.panel-body {
		padding: 0;
	}
</style>
<script type="text/javascript">
	var seachIsActive = false;
	function add() {
        var name = '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NewGroup") %>';
		var selectedNode = treeGroups.GetSelectedNode();
		var parentId = selectedNode ? selectedNode.id : null;
		var retVal = getAjaxResponse('InsertItemInPhoneBook', "ParentGuid=" + parentId + "&Name=" + name);
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
		var type;
		if (node.attributes)
		{
			if (node.attributes.type)
				type = node.attributes.type;
		}
		else
			type=<%=(int)Arad.SMS.Gateway.Business.PhoneBookGroupType.Normal%>;

		$("#frm")[0].src = "/PageLoader.aspx?c=" + '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_PhoneNumber, Session)%>' + "&Guid=" + node.id + "&Type=" + type;
	}

	function updateGroup(){
		var selectedNode = treeGroups.GetSelectedNode();
		if (selectedNode) {
			$("#frm")[0].src = "/PageLoader.aspx?c=" + '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_UpdateGroup, Session)%>' + "&Guid=" + selectedNode.id;
		}
	}

	function beginUpdate() {
		var selectedNode = treeGroups.GetSelectedNode();
		treeGroups.Update(selectedNode);
	}

	function udateNode(node) {
		if (node) {
			var parent = treeGroups.GetParent();
			var retVal = getAjaxResponse('EditItemInPhoneBook', "PhoneBookGuid=" + node.id + "&ParentGuid=" + parent.id + "&Name=" + node.text);
			if (importData(retVal, 'Result') != 'OK') {
				$.messager.alert('Error', importData(retVal, 'Message'));
			}
		}
	}

	function nodeTextFormat(node) {
		var text = node.text;
		var tooltip = '';
		if (node.attributes) {
			if (node.attributes.type == 1) {
                tooltip = '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupName") %>' + ' : ' + node.text + '&#013;';
                tooltip += '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupType") %>' + ' : ' + '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupTypeNormal") %>' + '&#013;';
                tooltip += '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupCode") %>' + ' : ' + node.attributes.id + '&#013;';
				text += '&nbsp;<span>(' + node.attributes.count + ')</span>';
			}
			else if (node.attributes.type == 2) {
                tooltip = '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupName") %>' + ' : ' + node.text + '&#013;';
                tooltip += '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupType") %>' + ' : ' + '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupTypeVAS") %>'  + '&#013;';
                tooltip += '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupCode") %>' + ' : ' + node.attributes.id + '&#013;';
                tooltip += '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ServiceID") %>' + ' : ' + node.attributes.serviceId + '&#013;';
				text += '&nbsp;<span>(' + node.attributes.count + ')</span><span style=\'color:red\'>(' + '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Vas")%>' + ')</span>';
			}

		text = '<a href="#" title="' + tooltip + '" class="easyui-tooltip">' + text + '</a>';
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
        $.messager.confirm('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Delete") %>', '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete") %>', function (result) {
			if (result) {
				if (selectedNode.children != null) {
					if(selectedNode.children.length>0)
					{
						$.messager.alert('Error', '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupOfPhoneBookHasSubGroupError")%>');
						return false;
					}
				}
				var retVal = getAjaxResponse('DeleteItemFromPhoneBook', "PhoneBookGuid=" + selectedNode.id);
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

function vasSetting() {
	var selectedNode = treeGroups.GetSelectedNode();
	if (selectedNode) {
		$("#frm")[0].src = "/PageLoader.aspx?c=" + '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_VasSetting, Session)%>' + "&Guid=" + selectedNode.id;
	}
}

function regularContent() {
	var selectedNode = treeGroups.GetSelectedNode();
	if (selectedNode) {
		$("#frm")[0].src = "/PageLoader.aspx?c=" + '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_RegularContent, Session)%>' + "&Guid=" + selectedNode.id;
	}
}

function format() {
	var selectedNode = treeGroups.GetSelectedNode();
	if (selectedNode) {
		$("#frm")[0].src = "/PageLoader.aspx?c=" + '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_UserFields_SmsFormat, Session)%>' + "&Guid=" + selectedNode.id;
	}
}

function field() {
	var selectedNode = treeGroups.GetSelectedNode();
	if (selectedNode) {
		$("#frm")[0].src = "/PageLoader.aspx?c=" + '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_UserFields_PhoneBookField, Session)%>' + "&Guid=" + selectedNode.id;
	}
}

function searchNumber() {
	var selectedNode = treeGroups.GetSelectedNode();
	if (selectedNode)
		$("#frm")[0].src = "/PageLoader.aspx?c=" + '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_SearchAllGroups, Session)%>' + "&Guid=" + selectedNode.id;
	else
		$("#frm")[0].src = "/PageLoader.aspx?c=" + '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_SearchAllGroups, Session)%>' + "&Guid='<%=Guid.Empty%>'";
}

function searchFormat() {
	$("#frm")[0].src = "/PageLoader.aspx?c=" + '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_SearchAllFormats, Session)%>';
}

function searchField() {
	$("#frm")[0].src = "/PageLoader.aspx?c=" + '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_SearchAllUserField, Session)%>';
}

function sendSms(){
	var nodes = treeGroups.GetChecked();

	if(nodes.length==0){
        messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelectGroupError") %>', '', 'alert', 'danger');
		return false;
	}

	var nodeIds='';
	for(i=0;i<nodes.length;i++)
		nodeIds+=nodes[i].id + ',';
	$("#frm")[0].src = "/PageLoader.aspx?c=" + '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_SendSms, Session)%>' + "&PhoneBookGuids="+nodeIds;
}
</script>
<div class="row">
	<div class="col-md-12">
		<div class="easyui-layout" style="height: 1100px;">
			<div data-options="region:'east',split:true" class="col-md-2 col-xs-4" style="padding: 0;">
				<div class="easyui-panel" title=" " style="overflow: auto; padding: 5px;" data-options="tools:'#tools'">
					<div class="form-group">
						<input type="text" class="form-control input-sm" id="txtSearch" placeholder="Search">
					</div>
					<div class="form-group">
						<a href="#" onclick="sendSms();" class="btn btn-sm btn-primary" style="white-space: normal"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendToSelectedGroupSms") %></a>
					</div>
					<div class="clear"></div>
					<GeneralTools:TreeView ID="treeGroups" runat="server" OnClick="clickNode(node);" ShowLines="true" ContextDivID="menu"
						OnAfterEdit="udateNode(node);" Formatter="return nodeTextFormat(node);" BeforeLoad="setParam(node,param);" ShowCheckBox="true"></GeneralTools:TreeView>
				</div>
			</div>
			<div data-options="region:'center',title:'<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Desktop") %>',iconCls:'icon-ok'">
				<iframe id="frm" style="border: 0; width: 100%; height: 1020px; overflow: auto;"></iframe>
			</div>
		</div>
	</div>
</div>

<div id="tools">
	<a href="#" class="fa fa-cog red easyui-menubutton" data-options="menu:'#mainMenu',iconCls:'icon-edit'">Edit</a>
	<a href="javascript:void(0)" class="fa fa-plus green" onclick="add();"></a>
	<a href="javascript:void(0)" class="fa fa-info-circle easyui-tooltip" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PhoneBookNotice")%>"></a>
</div>

<div id="menu" class="easyui-menu" style="width: 145px;">
	<div onclick="add();" data-options="iconCls:'fa fa-plus fa-2x green'"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AddGroup") %></div>
	<div onclick="format();" data-options="iconCls:'fa fa-plus fa-2x green'"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AddSmsFormat") %></div>
	<div onclick="field();" data-options="iconCls:'fa fa-plus fa-2x green'"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AddField") %></div>
	<%if (Arad.SMS.Gateway.GeneralLibrary.Helper.GetBool(Session["IsMainAdmin"]) || Arad.SMS.Gateway.GeneralLibrary.Helper.GetBool(Session["IsSuperAdmin"]))
	 {%>
	<div onclick="vasSetting();" data-options="iconCls:'fa fa-cog fa-2x blue'"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PhoneBookVasSetting") %></div>
	<%}%>
	<div onclick="regularContent();" data-options="iconCls:'fa fa-clone fa-2x purple'"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RegularContent") %></div>
	<div onclick="beginUpdate();" data-options="iconCls:'fa fa-pencil orange'"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Edit") %></div>
	<div onclick="deleteNode()" data-options="iconCls:'fa fa-times red'"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Delete") %></div>
</div>

<div id="mainMenu" class="easyui-menu" style="width: 150px;">
	<div onclick="searchNumber();" data-options="iconCls:'fa fa-search blue'"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SearchNumber") %></div>
	<div onclick="searchFormat();" data-options="iconCls:'fa fa-search green'"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AllFormatList") %></div>
	<div onclick="searchField();" data-options="iconCls:'fa fa-search red'"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FieldsList") %></div>
</div>
