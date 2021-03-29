<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contents.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Data.Contents" %>

<GeneralTools:DataGrid runat="server" showAdvancedSearch="true" ID="gridData" DefaultSortField="CreateDate" ListCaption="ContentsList" ListHeight="420"
	SearchDivID="advanceSearchContainer" ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Title" FieldName="Title" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="Presentable" FieldName="IsActive" Align="Center" FormattingMethod="BooleanOnOff"/>
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" CellWidth="110" Align="Center" FormattingMethod="DateTimeShortDate"/>
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="250" Align="Center" FormattingMethod="ImageButton"/>
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function deleteRow(e) {
		gridData.Event = e;
		if (gridData.IsSelectedRow()) {
			var guid = gridData.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteNews", "Guid=" + guid);
					if (isDelete) {
						gridData.Search();
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}

	function activeData(e) {
		gridData.Event = e;
		if (gridData.IsSelectedRow()) {
			var guid = gridData.SelectedGuid;
			var result = getAjaxResponse("ActiveData", "Guid=" + guid);
			if (result)
				gridData.Search();
			else
				messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
		}
	}
</script>
