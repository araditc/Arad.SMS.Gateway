<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Content.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.RegularContents.Content" %>

<GeneralTools:DataGrid runat="server" ID="gridContent" DefaultSortField="CreateDate" ListCaption="RegularContentList" ListHeight="420"
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true" SelectMode="Multiple">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Text" FieldName="Text" Sortable="false" CellWidth="120" MaxLength="200" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Align="Center" CellWidth="70" FormattingMethod="DateTimeShortDateTime" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function deleteMultipleNumber() {
		var data = gridContent.GetSelectedRowData(["Guid"]);
		if (importData(data, "resultCount") == "0")
			return;

		messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
			if (result) {
				var isDelete = getAjaxResponse("DeleteMultipleContent", "Data=" + data);
				if (isDelete) {
					gridContent.Search();
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeleteRecord")%>', '', 'alert', 'success');
				}
				else
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
			}
		});
	}
</script>
