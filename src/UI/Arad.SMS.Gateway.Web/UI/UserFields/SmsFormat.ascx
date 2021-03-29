<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmsFormat.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.UserFields.SmsFormat" %>

<script src="/script/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
<asp:HiddenField ID="hdnSmsFormatGuid" runat="server" />

<script type="text/javascript">
	$(document).ready(function () {
		$("#sortable").sortable({ revert: false, cursor: 'hand', tolerance: 'pointer' });
		jQuery("#sortable").disableSelection();
		$("#sortable").bind("sortstop", function (event, ui) {
			jQuery("#sortable > li").each(function (n, item) {
				jQuery(item).unbind();
			});
			jQuery("#sortable > li").each(function (n, item) {
				var hasCheckBox = jQuery(item).find('input:checkbox');
				if (hasCheckBox.length == 0)
					jQuery(item).append('<br/><input style="margin-top:2px;margin-left:0px"  type="checkbox" />');
			});

			var items = $("#sortable").find('li');
			$("#txtDraft")[0].value = '';
			for (i = 0; i < items.length; i++) {
				if ($(items[i]).attr('field') == 'UserText')
					$("#txtDraft")[0].value += $(items[i]).attr('title') + ' ';
				else if ($(items[i]).attr('field') == 'InputSms')
					$("#txtDraft")[0].value += '[' + $(items[i]).attr('title') + '] ';
				else
					$("#txtDraft")[0].value += $(items[i]).attr('draftText') + ' ';
			}
		});
		$("#list li").draggable({
			connectToSortable: "#sortable",
			helper: "clone",
			revert: "invalid"
		});
		$("ul, li").enableSelection();
	});

	function addCustomText() {
		$('#divUserText').modal('show');
	}
</script>

<div id="pnlSmsFormat">
	<asp:HiddenField ID="hdnActionType" runat="server" />
	<GeneralTools:DataGrid runat="server" ID="gridFormats" DefaultSortField="CreateDate" ListCaption="SmsFormatsList" ListHeight="420"
		ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true" DisableNavigationBar="true">
		<Columns>
			<GeneralTools:DataGridColumnInfo Caption="Title" FieldName="FormatName" CellWidth="150" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="250" Align="Center" FormattingMethod="ImageButton" />
		</Columns>
	</GeneralTools:DataGrid>
</div>

<div id="pnlManageFormat" style="display: none;">
	<div class="row">
		<hr />
		<div class="col-xs-2 col-md-2">
			<ul id="list" class="FieldList" style="margin-right: 0;">
				<asp:Literal ID="literalField" runat="server"></asp:Literal>
			</ul>
		</div>
		<div class="col-xs-8 col-md-8">
			<div class="row">
				<div class="col-xs-6 col-md-4">
					<div class="form-inline" role="form">
						<div class="form-group">
							<label for="txtNameSmsFormat"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Title") %></label>
							<input id="txtNameSmsFormat" isrequired="true" validationset='SmsFormat' type="text" class="form-control input-sm" />
						</div>
					</div>
				</div>
				<div class="col-xs-6 col-md-8">
					<span id="btnDletetField" onclick="deleteSelectedItem();" class="ui-icon fa fa-trash-o red" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Delete")%>"></span>
					<img title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AddCustomText")%>" id="btnUserText" class="gridImageButton" onclick="addCustomText();" src="/pic/UserText.png" />
					<span class="ui-icon fa fa-envelope-o blue" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ReceivedSmsText")%>" onclick="addReceivedSms();"></span>
					<input id="checkBoxSelectAll" type="checkbox" onclick="selectAll(this.checked);" /><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelectAll") %>
				</div>
			</div>
			<hr />
			<div class="row">
				<ul id="sortable" style="width: 100%; height: 250px; border: 1px solid #438eb9;"></ul>
				<textarea id="txtDraft" class="input" style="width: 100%; margin: 5px 0 5px 0;" readonly="readonly" rows="4" placeholder="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Preview") %>"></textarea>
				<div class="buttonControlDiv">
					<input id="btnSave" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Register") %>" class="btn btn-success" onclick="saveFormat();" />
					<input id="btnCancelSave" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %>" class="btn btn-default" onclick="cancel();" />
				</div>
			</div>
		</div>
	</div>
	<div id="errorDiv" style="display: none"></div>
</div>

<div class="modal fade bs-example-modal-sm" id="divUserText" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
	<div class="modal-dialog modal-sm">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
				<h4 class="modal-title" id="myModalLabel"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AddCustomText") %></h4>
			</div>
			<div class="modal-body">
				<div class="form-horizontal" role="form">
					<div class="form-group">
						<div>
							<textarea id="txtUserText" class="form-control" rows="3"></textarea>
						</div>
					</div>
				</div>
			</div>
			<div class="modal-footer">
				<input id="btnAdd" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Register") %>" class="btn btn-primary" onclick="addUserText()" />
				<input id="btnCancel" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %>" class="btn btn-default" data-dismiss="modal" />
			</div>
		</div>
	</div>
</div>

<script type="text/javascript">
	function refreshSortableList() {
		var items = $("#sortable").find('li');
		$("#txtDraft")[0].value = '';
		for (i = 0; i < items.length; i++) {
			if ($(items[i]).attr('field') == 'USERTEXT')
				$("#txtDraft")[0].value += $(items[i]).attr('title') + ' ';
			else if ($(items[i]).attr('field') == 'InputSms')
				$("#txtDraft")[0].value += '[' + $(items[i]).attr('title') + '] ';
			else
				$("#txtDraft")[0].value += $(items[i]).attr('draftText') + ' ';
		}
	}

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
		refreshSortableList();
	}

	function addUserText() {
		var text = $("#txtUserText")[0].value;
		if (text.length > 10) {
			var subStringText = text.substring(0, 10);
			$("#sortable").append('<li field="UserText" title="' + text + '" class="Field">' + subStringText + '...<br/><input type="checkbox" /></li>');
		}
		else
			$("#sortable").append('<li field="UserText" title="' + text + '" class="Field">' + text + '<br/><input type="checkbox" /></li>');

		$("#txtUserText")[0].value = "";
		refreshSortableList();
		$('#divUserText').modal('hide');
	}

	function addReceivedSms() {
		var text = '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ReceivedSmsText")%>';
		$("#sortable").append('<li field="InputSms" title="' + text + '" class="Field">' + text + '<br/><input type="checkbox" /></li>');
		refreshSortableList();
	}

	function checkFormat() {
		var items = $("#sortable").find('li');
		if (items.length > 0)
			return true;
		else {
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompleteSmsFormatField") %>', '', 'alert', 'danger');
			return false;
		}
	}

	function generateFormat() {
		var format = "";
		var items = $("#sortable").find('li');
		for (i = 0; i < items.length; i++) {
			if ($(items[i]).attr('field') == 'UserText')
				format += "<(*$" + $(items[i]).attr('title').trim() + "$*)>";
			else if ($(items[i]).attr('field') == 'InputSms')
				format += "<(!$InputSms$!)>";
			else
				format += "<(%$" + $(items[i]).attr('field') + "$%)>";
		}
		return format;
	}

	function addNewFormat() {
		$("#<%=hdnActionType.ClientID %>")[0].value = "insert";
		$("#pnlSmsFormat")[0].style.display = 'none'
		$("#pnlManageFormat")[0].style.display = '';
	}

	function saveFormat() {
		if (validateRequiredFields('SmsFormat') && checkFormat()) {
			var actionType = $("#<%=hdnActionType.ClientID %>")[0].value;
			var nameFormat = $("#txtNameSmsFormat")[0].value;
			var format = generateFormat();

			var result = getAjaxResponse('SaveSmsFormat', "ActionType=" + actionType +
																										"&FormatGuid=" + $("#<%=hdnSmsFormatGuid.ClientID %>")[0].value +
																										"&Format=" + format +
																										"&NameFormat=" + toUTF8(nameFormat) +
																										"&PhoneBookGuid=" + '<%=PhoneBookGuid%>');
			if (result == true) {
				gridFormats.Search();
				messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("InsertRecord") %>', '', 'alert', 'success');
				cancel();
			}
			else
				messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord") %>', '', 'alert', 'danger');
		}
	}

	function editFormat(e) {
		gridFormats.Event = e;
		if (!gridFormats.IsSelectedRow())
			return;

		var guid = gridFormats.SelectedGuid;
		$("#<%=hdnSmsFormatGuid.ClientID %>")[0].value = guid;
		var result = getAjaxResponse('LoadSmsFormat', "smsFormatGuidLoad=" + guid).split('/*NameFormat*/');
		var list = result[0];
		$("#txtNameSmsFormat")[0].value = result[1];
		selectAll(true);
		deleteSelectedItem();
		$("#sortable").append(list);
		refreshSortableList();
		$("#<%=hdnActionType.ClientID %>")[0].value = "edit";
		$("#pnlSmsFormat")[0].style.display = 'none'
		$("#pnlManageFormat")[0].style.display = '';
	}

	function cancel() {
		$("#pnlSmsFormat")[0].style.display = ''
		$("#pnlManageFormat")[0].style.display = 'none';
		$("#txtNameSmsFormat")[0].value = '';
		selectAll(true);
		deleteSelectedItem();
	}

	function deleteFormat(e) {
		gridFormats.Event = e;
		if (!gridFormats.IsSelectedRow())
			return;

		var guid = gridFormats.SelectedGuid;
		messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
			if (result) {
				if (getAjaxResponse('DeleteSmsFormat', "SmsFormatGuid=" + guid) == true) {
					gridFormats.Search();
				}
				else
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord") %>', '', 'alert', 'danger');
			}
		});
	}
</script>
