<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegularContent.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PhoneBooks.RegularContent" %>

<GeneralTools:DataGrid runat="server" ID="gridPhoneBookRegularContent" DefaultSortField="CreateDate" ListCaption="RegularContentList" ListHeight="420"
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true" DisableNavigationBar="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="Type" Sortable="false" CellWidth="30" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="Title" FieldName="Title" Sortable="false" CellWidth="120" MaxLength="50" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Align="Center" CellWidth="70" FormattingMethod="DateTimeShortDateTime" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="70" Align="Right" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>

<div class="col-md-6">
	<div class="modal fade" id="divSaveRegularContent" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title" id="myModalLabel"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UI_RegularContents_SaveRegularContent") %></h4>
				</div>
				<div class="modal-body">
					<div class="form-horizontal" role="form">
						<div class="form-group">
							<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RegularContent") %></label>
							<div class="col-sm-9">
								<asp:DropDownList ID="drpRegularContent" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
							</div>
						</div>
					</div>
				</div>
				<div class="modal-footer">
					<input id="btnSave" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Register") %>" class="btn btn-success" onclick="saveRegularContent();" />
					<input id="btnCancel" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %>" class="btn btn-default" data-dismiss="modal" />
				</div>
			</div>
		</div>
	</div>
</div>

<script type="text/javascript">
	function addRegularContent() {
		$('#divSaveRegularContent').modal('show');
	}

	function saveRegularContent() {
		var result = getAjaxResponse("AddRegularContentToPhoneBook", "PhoneBookGuid=" + '<%=PhoneBookGuid%>' + "&RegularContentGuid=" + $("#<%=drpRegularContent.ClientID %>")[0].value);
		if (result) {
			$('#divSaveRegularContent').modal('hide');
			gridPhoneBookRegularContent.Search();
		}
		else
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord") %>', '', 'alert', 'danger');
	}

	function deleteRegularContent(e) {
		gridPhoneBookRegularContent.Event = e;
		if (gridPhoneBookRegularContent.IsSelectedRow()) {
			var guid = gridPhoneBookRegularContent.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteRegularContentFromPhoneBook", "RegularGuid=" + guid);
					if (isDelete) {
						gridPhoneBookRegularContent.Search();
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}
</script>
