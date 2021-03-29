<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserShortcuts.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.UserShortcuts" %>

<!--JqueryDrag&Drop-->
<script src="/script/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>

	<script type="text/javascript">
		$(document).ready(function () {
			$("#sortable").sortable({ revert: false, cursor: 'hand', tolerance: 'pointer' });
			$("#sortable").bind("sortstop", function (event, ui) {
				jQuery("#sortable > li").each(function (n, item) {
					var hasCheckBox = jQuery(item).find('input:checkbox');
					if (hasCheckBox.length == 0)
						jQuery(item).append('<input style="margin-top:2px;margin-left:0px"  type="checkbox" />');

					var items = $("#sortable").find('li');
					var count = 0;
					for (i = 0; i < items.length; i++) {
						if ($(items[i]).attr('code') == $(item).attr('code'))
							count++;
					}
					if (count > 1 || items.length > 11)
						jQuery(item).remove();
				});
			});
			$("#list li").draggable({
				connectToSortable: "#sortable",
				helper: "clone",
				revert: "invalid"
			});
		});
	</script>

<asp:HiddenField ID="hdnShortcuts" runat="server" />
    <% if(Session["Language"].ToString() == "fa") { %>
        <div dir="rtl">
    <% }  else
       {%>
        <div dir="ltr">
    <%  } %>

	<div class="ui-state-highlight ui-corner-all" style="margin-bottom:2px;padding:0px;line-height:2;font-weight:bold;">
		<p style="margin:2px;">
			<span class="ui-icon ui-icon-info" style="float: right; margin-left: 5px;"></span>
			<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ShortcutNotice")%>
		</p>
	</div>
	<% if(Session["Language"].ToString() == "fa") { %>
        <div style="border:1px solid Silver;padding:5px;float:right;width:150px; margin-left:5px;overflow:auto;height:400px;">
    <% }  else
       {%>
        <div style="border:1px solid Silver;padding:5px;float:left;width:150px; margin-left:5px;overflow:auto;height:400px;">
    <%  } %>
	
		<ul id="list" class="FieldList">
			<asp:Literal ID="literalServices" runat="server"></asp:Literal>
		</ul>
	</div>
	<% if(Session["Language"].ToString() == "fa") { %>
        <div style="float:right;width:850px"> 
			<div style="margin-left:5px;margin-top:15px;">
				<ul id="sortable" style="width:100%;height:250px;border:1px solid #438EB9;" ></ul>
			</div>
    <% }  else
       {%>
        <div style="float:left;width:850px"> 
			<div style="margin-left:5px;margin-top:15px;">
				<ul id="sortable" style="width:100%;height:250px;border:1px solid #438EB9;" ></ul>
			</div>
    <%  } %>
		<div class="clear"></div>
		<hr />
		<div>
			<input id="checkBoxSelectAll" type="checkbox"  onclick="selectAll(this.checked);"/><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelectAll") %>
			<input id="btnDletetField" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Delete") %>" class="btn btn-danger" onclick="deleteSelectedItem();"/>
			<input id="btnSave" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Register") %>" class="btn btn-success" onclick="saveShortcut();" />
		</div>
	</div>
</div>

<script type="text/javascript">
	function selectAll(status) {
		var items = $("#sortable").find('li');
		for (i = 0; i < items.length; i++) {
			var checkBox = $(items[i]).find('input:checkbox');
			$(checkBox).attr('checked', status);
		}
	}

	function deleteSelectedItem() {
		var items = $("#sortable").find('li');
		for (i = 0; i < items.length; i++) {
			var checkBox = $(items[i]).find('input:checkbox');
			if ($(checkBox).attr('checked')) {
				$(items[i]).remove();
			}
		}
	}

	function checkFormat() {
		var items = $("#sortable").find('li');
		if (items.length > 0)
			return true;
		else {
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompleteShortcutList") %>', '', 'alert', 'danger');
			return false;
		}
	}

	function saveShortcut() {
		if (checkFormat()) {
			var shortcut = "";
			var items = $("#sortable").find('li');
			for (i = 0; i < items.length; i++) {
				if ($(items[i]).attr('code') != undefined)
					shortcut += $(items[i]).attr('code') + ",";
			}

			shortcut = shortcut.substring(0, shortcut.length - 1);
			var result = getAjaxResponse('SaveShortcut', "Value=" + shortcut);
			if (result == true) {
				messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("InsertRecord") %>', '', 'alert', 'success');
			}
			else
				messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord") %>', '', 'alert', 'danger');
		}
	}

	function loadShortcut() {
		var shortcuts = $("#<%=hdnShortcuts.ClientID %>")[0].value;
		$("#sortable").append(shortcuts);
	}
</script>