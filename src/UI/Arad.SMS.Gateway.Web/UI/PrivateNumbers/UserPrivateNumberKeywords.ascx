<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserPrivateNumberKeywords.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PrivateNumbers.UserPrivateNumberKeywords" %>

<GeneralTools:DataGrid runat="server" ShowAdvancedSearch="false" ID="gridKeywords" DefaultSortField="CreateDate" ListCaption="UserPrivateNumbersList" ListHeight="420"
	SearchDivID="advanceSearchContainer" ShowRowNumber="true" ShowToolbar="true" ShowSearchToolbar="true" ToolbarPosition="Top">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Guid" FieldName="Guid" Hidden="true" CellWidth="85" Align="Center" FormattingMethod="Encrypt" />
		<GeneralTools:DataGridColumnInfo Caption="Keyword" FieldName="Keyword" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="70" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="LineNumber" FieldName="Number" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="110" Align="Center" Direction="LeftToRight"/>
		<GeneralTools:DataGridColumnInfo Caption="UserName" FieldName="UserName" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="100" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="70" Align="Right" FormattingMethod="ImageButton"/>
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function search(gridId) {
		gridKeywords.Search();
	}

	function deleteRow(e) {
		gridKeywords.Event = e;
		if (!gridKeywords.IsSelectedRow())
			return;

		var guid = gridKeywords.SelectedGuid;
		messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm','danger', function (result) {
			if (result) {
				var isDelete = getAjaxResponse("DeletePrivateNumberKeyword", "KeywordGuid=" + guid);
				if (isDelete) {
					gridKeywords.Search();
				}
				else
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
			}
		});
	}
</script>