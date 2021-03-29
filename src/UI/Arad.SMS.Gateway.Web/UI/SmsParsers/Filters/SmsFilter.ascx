<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmsFilter.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsParsers.Filters.SmsFilter" %>

<GeneralTools:DataGrid runat="server" ID="gridSmsFilter" DefaultSortField="CreateDate" ListCaption="FilterSms"
	ListHeight="420" ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Title" FieldName="Title" Sortable="true" CellWidth="250" Align="Center" Frozen="true" />
		<GeneralTools:DataGridColumnInfo Caption="Status" FieldName="IsActive" Sortable="true" CellWidth="70" Align="Center" Frozen="true" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="FromDateTime" FieldName="FromDateTime" Align="Center" CellWidth="70" FormattingMethod="DateTimeShortDateTime" />
		<GeneralTools:DataGridColumnInfo Caption="ToDateTime" FieldName="ToDateTime" Align="Center" CellWidth="70" FormattingMethod="DateTimeShortDateTime" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="70" Align="Center" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>


<script type="text/javascript">

	function deleteRow(e) {
		gridSmsFilter.Event = e;
		if (gridSmsFilter.IsSelectedRow()) {
			var guid = gridSmsFilter.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteSmsParser", "Guid=" + guid);
					if (isDelete) {
						gridSmsFilter.Search();
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}

	function activeParser(e) {
		gridSmsFilter.Event = e;
		if (gridSmsFilter.IsSelectedRow()) {
			var guid = gridSmsFilter.SelectedGuid;
			var result = getAjaxResponse("ActiveSmsParser", "Guid=" + guid);
			if (result)
				gridSmsFilter.Search();
			else
				messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
		}
	}
</script>
