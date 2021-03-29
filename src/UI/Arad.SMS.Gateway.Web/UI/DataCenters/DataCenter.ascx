<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataCenter.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.DataCenters.DataCenter" %>

<GeneralTools:DataGrid runat="server" ID="gridDataCenters" DefaultSortField="CreateDate" ListCaption="CategoriesContentList" ListHeight="420" 
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<Columns>
		<GeneralTools:DataGridColumnInfo Caption="Title" FieldName="Title" Align="Center" Sortable="false"/>
		<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="Type" Align="Center" FormattingMethod="CustomRender" Sortable="false"/>
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" CellWidth="110" Align="Center" FormattingMethod="DateTimeShortDate" Sortable="false"/>
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="250" Align="Center" FormattingMethod="ImageButton" />
	</Columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function deleteRow(e) {
		gridDataCenters.Event = e;
		if (gridDataCenters.IsSelectedRow()) {
			var guid = gridDataCenters.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteDataCenter", "Guid=" + guid);
					if (isDelete) {
						gridDataCenters.Search();
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}
</script>
