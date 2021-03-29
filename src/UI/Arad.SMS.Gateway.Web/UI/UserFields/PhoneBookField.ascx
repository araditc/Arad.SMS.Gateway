<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhoneBookField.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.UserFields.PhoneBookField" %>

<asp:HiddenField ID="hdnActionType" runat="server" />
<asp:HiddenField ID="hdnIndexField" runat="server" />

<div id="divFields">
	<GeneralTools:DataGrid runat="server" ShowAdvancedSearch="false" ID="gridPhoneBookFields" DefaultSortField="Type" ListCaption="FieldsList" ListHeight="420"
		ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true" DisableNavigationBar="true">
		<Columns>
			<GeneralTools:DataGridColumnInfo Caption="Title" FieldName="Title" CellWidth="100" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="TypeName" CellWidth="120" Align="Center" FormattingMethod="CustomRender" />
			<GeneralTools:DataGridColumnInfo Caption="" FieldName="Type" Hidden="true" CellWidth="85" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="" FieldName="Deletable" Hidden="true" CellWidth="85" Align="Center" FormattingMethod="BooleanOneZero" />
			<GeneralTools:DataGridColumnInfo Caption="" FieldName="FieldID" Hidden="true" CellWidth="85" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="120" Align="Center" FormattingMethod="ImageButton" />
		</Columns>
	</GeneralTools:DataGrid>
</div>
<script type="text/javascript">
	function editField(e) {
		gridPhoneBookFields.Event = e;
		if (!gridPhoneBookFields.IsSelectedRow())
			return;

		$("#<%=hdnIndexField.ClientID %>")[0].value = gridPhoneBookFields.GetSelectedRowFieldValue('FieldID');
		$("#<%=hdnActionType.ClientID %>")[0].value = "edit";
		$("#<%=txtTitle.ClientID %>")[0].value = gridPhoneBookFields.GetSelectedRowFieldValue('Title');
		$("#<%=drpFieldType.ClientID %>")[0].value = gridPhoneBookFields.GetSelectedRowFieldValue('Type');
		$("#lgndSaveField").html('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("EditField") %>');
		$("#divFields")[0].style.display = 'none';
		$("#divSaveField")[0].style.display = '';
	}

	function addNewField() {
		$("#lgndSaveField").html('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NewField") %>');
		$("#<%=hdnActionType.ClientID %>")[0].value = "insert";
		$("#divFields")[0].style.display = 'none';
		$("#divSaveField")[0].style.display = '';
		$("#<%=txtTitle.ClientID %>")[0].value = '';
	}

	function deleteField(e) {
		gridPhoneBookFields.Event = e;
		if (!gridPhoneBookFields.IsSelectedRow())
			return;

		var deletable = gridPhoneBookFields.GetSelectedRowFieldValue('Deletable');
		if (deletable == 1) {
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm','danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteUserField", "Guid=" + '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.RequestGuid(this, "Guid")%>' + "&Index=" + gridPhoneBookFields.GetSelectedRowFieldValue('FieldID'));
					if (isDelete) {
						gridPhoneBookFields.Search();
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord") %>', '', 'alert', 'danger');
				}
			});
		}
		else
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorDeleteUserField") %>', '', 'alert', 'danger');
	}

	function cancel() {
		$("#divFields")[0].style.display = '';
		$("#divSaveField")[0].style.display = 'none';
	}
</script>

<div id="divSaveField" style="display: none;">
	<fieldset>
		<legend id="lgndSaveField"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NewField") %></legend>
		<div class="col-xs-12 col-md-12">
			<div class="form-horizontal" role="form">
				<div class="form-group">
					<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Title")%></label>
					<div class="col-sm-3">
						<asp:TextBox CssClass="form-control input-sm" ID="txtTitle" runat="server" isRequired="true" validationSet="SaveField"></asp:TextBox>
					</div>
				</div>
				<div class="form-group">
					<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Type")%></label>
					<div class="col-sm-3">
						<asp:DropDownList ID="drpFieldType" runat="server" class="form-control input-sm" isRequired="true" validationSet="SaveField"></asp:DropDownList>
					</div>
				</div>
				<div class="col-md-3">
					<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" />
					<input id="btnCancel" type="button" value='<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %>' class="btn btn-default" onclick="cancel();" />
				</div>
			</div>
		</div>
	</fieldset>
</div>
