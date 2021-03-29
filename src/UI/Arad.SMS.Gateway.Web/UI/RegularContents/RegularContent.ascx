<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegularContent.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.RegularContents.RegularContent" %>

<GeneralTools:DataGrid runat="server" ID="gridRegularContent" DefaultSortField="CreateDate" ListCaption="RegularContentList" ListHeight="420"
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="Type" Sortable="false" CellWidth="30" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="Status" FieldName="IsActive" Sortable="true" CellWidth="30" Align="Center" Frozen="true" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="Title" FieldName="Title" Sortable="false" CellWidth="120" MaxLength="50" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Period" FieldName="Period" CellWidth="50" Align="Center" FormattingMethod="CustomRender"/>
		<GeneralTools:DataGridColumnInfo Caption="Warning" FieldName="WarningType" CellWidth="80" Align="Center" FormattingMethod="CustomRender"/>
		<GeneralTools:DataGridColumnInfo Caption="FromDateTime" FieldName="StartDateTime" Align="Center" CellWidth="70" FormattingMethod="DateTimeShortDateTime" />
		<GeneralTools:DataGridColumnInfo Caption="ToDateTime" FieldName="EndDateTime" Align="Center" CellWidth="70" FormattingMethod="DateTimeShortDateTime" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="70" Align="Right" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function deleteRegularContent(e) {
		gridRegularContent.Event = e;
		if (gridRegularContent.IsSelectedRow()) {
			var guid = gridRegularContent.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm','danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteRegularContent", "Guid=" + guid);
					if (isDelete) {
						gridRegularContent.Search();
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}
</script>
