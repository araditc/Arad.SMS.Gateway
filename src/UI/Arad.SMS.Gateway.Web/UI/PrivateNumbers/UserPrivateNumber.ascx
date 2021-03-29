<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserPrivateNumber.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PrivateNumbers.UserPrivateNumber" %>

<GeneralTools:DataGrid runat="server" ShowAdvancedSearch="false" ID="gridUserPrivateNumbers" DefaultSortField="CreateDate" ListCaption="UserPrivateNumbersList"
	ListHeight="420" ShowRowNumber="true" ShowToolbar="true"
	ShowSearchToolbar="true" ToolbarPosition="Top" GridComplete="setRowColor">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="NumberGuid" FieldName="Guid" Hidden="true" CellWidth="85" Align="Center" FormattingMethod="Encrypt" />
		<GeneralTools:DataGridColumnInfo Caption="Active" FieldName="IsActive" CellWidth="35" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="Public" FieldName="IsPublic" CellWidth="70" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="Type" CellWidth="50" Align="Center" FormattingMethod="CustomRender"/>
		<GeneralTools:DataGridColumnInfo Caption="ServiceID" FieldName="ServiceID" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="70" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="ServicePrice" FieldName="ServicePrice" CellWidth="80" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="PriceNumber" FieldName="Price" CellWidth="80" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="IsExpired" FieldName="IsExpired" Hidden="true" CellWidth="50" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="LineNumber" FieldName="Number" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="110" Align="Center" Direction="LeftToRight" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="50" Align="Right" FormattingMethod="ImageButton"/>
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function setRowColor(listId) {
		var rowIDs = $(listId).jqGrid('getDataIDs');
		var ret;
		if (rowIDs.length > 0) {
			{
				for (var i = 0; i < rowIDs.length; i++) {
					{
						ret = $(listId).jqGrid('getRowData', rowIDs[i]);
						if (ret['IsExpired'] == 1)
							$('#' + $.jgrid.jqID(rowIDs[i])).addClass('warning-gridRowColor');
					}
				}
			}
		}
	}

	function search(gridId) {
		gridUserPrivateNumbers.Search();
	}

	function setPublic(e) {
		gridUserPrivateNumbers.Event = e;
		if (gridUserPrivateNumbers.IsSelectedRow()) {
			var guid = gridUserPrivateNumbers.SelectedGuid;
			var result = getAjaxResponse("SetPublicNumber", "Guid=" + guid);
			if (result)
				gridUserPrivateNumbers.Search();
			else
				messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
		}
	}
</script>
