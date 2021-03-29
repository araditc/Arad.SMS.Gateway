<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TrafficRelay.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.TrafficRelays.TrafficRelay" %>

<GeneralTools:DataGrid runat="server" ID="gridUrls" DefaultSortField="CreateDate" ListCaption="TrafficRelay" ListHeight="420"
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Status" FieldName="IsActive" CellWidth="50" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="Title" FieldName="Title" CellWidth="100" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="URL" FieldName="Url" CellWidth="250" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="70" Align="Center" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function deleteRow(e) {
		gridUrls.Event = e;
		if (gridUrls.IsSelectedRow()) {
			var guid = gridUrls.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm','danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteTrafficRelay", "Guid=" + guid);
					if (isDelete)
						gridUrls.Search();
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}
</script>
