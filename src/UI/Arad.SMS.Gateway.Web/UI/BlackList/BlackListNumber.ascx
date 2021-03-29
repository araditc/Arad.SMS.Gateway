<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlackListNumber.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.BlackList.BlackListNumber" %>

<GeneralTools:DataGrid runat="server" showAdvancedSearch="false" ID="gridNumbers" DefaultSortField="Mobile" ListCaption="NumbersList" ListHeight="420"
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true" ShowSearchToolbar="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="CellPhone" FieldName="Mobile" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}"  Sortable="false" CellWidth="110" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="80" Align="Center" FormattingMethod="ImageButton"/>
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function search(gridId) {
		gridNumbers.Search();
	}

	function deleteNumber(e) {
		gridNumbers.Event = e;
		if (!gridNumbers.IsSelectedRow())
			return;

		var guid = gridNumbers.SelectedGuid;
		messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
			if (result) {
				var isDelete = getAjaxResponse("DeleteBlackListNumber", "NumberGuid=" + guid);
				if (isDelete) {
					gridNumbers.Search();
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeleteRecord")%>', '', 'alert', 'success');
				}
				else
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
			}
		});
	}
</script>
