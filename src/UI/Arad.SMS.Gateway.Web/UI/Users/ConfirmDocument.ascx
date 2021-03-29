<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfirmDocument.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.ConfirmDocument" %>

<GeneralTools:DataGrid runat="server" ID="gridDocuments" DefaultSortField="CreateDate" ListCaption="UserDocument" ListHeight="420"
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<columns>
		<GeneralTools:DataGridColumnInfo FieldName="Status" Caption="Status" CellWidth="50" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo FieldName="Document" Caption="Type" CellWidth="150" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo FieldName="Value" Hidden="true"  />
		<GeneralTools:DataGridColumnInfo FieldName="Action" Sortable="false" CellWidth="150" Align="Center" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>
<script type="text/javascript">
	function confirm(e) {
		gridDocuments.Event = e;
		if (!gridDocuments.IsSelectedRow())
			return;

		var guid = gridDocuments.SelectedGuid;
		var retVal = getAjaxResponse("ConfirmUserDocument", "Guid=" + guid);
		var result = importData(retVal, "Result");
		if (result == "OK") {
			gridDocuments.Search();
		}
		else
			messageBox(importData(retVal, "Message"), '', 'alert', 'danger');
	}

	function reject(e) {
		gridDocuments.Event = e;
		if (!gridDocuments.IsSelectedRow())
			return;

		var guid = gridDocuments.SelectedGuid;
		var result = getAjaxResponse("RejectUserDocument", "Guid=" + guid);
		if (result) {
			gridDocuments.Search();
		}
		else
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
	}

	function deleteRow(e) {
		gridDocuments.Event = e;
		if (gridDocuments.IsSelectedRow()) {
			var path = gridDocuments.GetSelectedRowFieldValue('Path');
			var isDelete = getAjaxResponse("DeleteUserDocumentRecord", "Guid=" + gridDocuments.SelectedGuid + "&path=" + path);
			if (isDelete)
				gridDocuments.Search();
		}
	}
</script>
