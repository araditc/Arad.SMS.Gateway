<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserMessages.ascx.cs" Inherits="MessagingSystem.UI.Domains.UserMessages" %>

<div style="width:400px;">
	<GeneralTools:DataGrid runat="server" ID="gridUserMessage" DefaultSortField="CreateDate" ListCaption="UserMessageList" 
			ListDifferenceHeight="160" ListDirection="RightToLeft" ShowRowNumber="true">
		<Columns>
			<GeneralTools:DataGridColumnInfo Caption="CompanyName" FieldName="Name" CellWidth="110" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Align="Center" CellWidth="70" FormattingMethod="DateTimeShortDate" />
			<GeneralTools:DataGridColumnInfo Caption="Telephone" FieldName="Telephone" Align="Center" CellWidth="80" />
			<GeneralTools:DataGridColumnInfo Caption="CellPhone" FieldName="CellPhone" Align="Center" CellWidth="80" />
			<GeneralTools:DataGridColumnInfo Caption="job" FieldName="job" Align="Center" CellWidth="110"/>
			<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="110" Align="Center" FormattingMethod="ImageButton" />
		</Columns>
	</GeneralTools:DataGrid>
</div>
<script type="text/javascript">
	function refreshGrid(result) {
		if (result == "true") {
			gridUserMessage.Search();
			messageBox('<%=GeneralLibrary.Language.GetString("InsertRecord")%>');
		}
	}

	function editRow(e) {
		gridUserMessage.Event = e;
		if (!gridUserMessage.IsSelectedRow())
			return;
		var guid = gridUserMessage.SelectedGuid;
		var address = '<%=GeneralLibrary.Helper.Encrypt((int)Business.UserControls.UI_Domains_SaveUserMessage,Session)%>';
		getAjaxPage(address, "Guid=" + guid, '<%=GeneralLibrary.Language.GetString("SaveUserMessage") %>', 'refreshGrid');
	}

	function deleteRow(e) {
		gridUserMessage.Event = e;
		if (!gridUserMessage.IsSelectedRow())
			return;
		var guid = gridUserMessage.SelectedGuid;
		messageBox('<%=GeneralLibrary.Language.GetString("ConfirmDelete") %>', '', 'Confirm', function (result) {
			if (result) {
				var isDelete = getAjaxResponse("DeleteUserMessage", "Guid=" + guid);
				if (isDelete) {
					gridUserMessage.Search();
					messageBox('<%=GeneralLibrary.Language.GetString("DeleteRecord")%>');
				}
				else
					messageBox('<%=GeneralLibrary.Language.GetString("ErrorRecord")%>');
			}
		});
	}
</script>