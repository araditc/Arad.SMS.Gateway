<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignPrivateNumberToUsers.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PrivateNumbers.AssignPrivateNumberToUsers" %>

<GeneralTools:DataGrid runat="server" ID="gridUserPrivateNumbers" DefaultSortField="CreateDate" ListCaption="UserPrivateNumbersList"
	SearchDivID="advanceSearchContainer" ShowRowNumber="true" ShowToolbar="true"
	ToolbarPosition="Top" ListHeight="420" GridComplete="setRowColor">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Active" FieldName="IsActive" CellWidth="35" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="Public" FieldName="IsPublic" CellWidth="70" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="Type" CellWidth="50" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="ServiceID" FieldName="ServiceID" CellWidth="70" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="IsExpired" FieldName="IsExpired" Hidden="true" CellWidth="50" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="ServicePrice" FieldName="ServicePrice" CellWidth="80" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="PriceNumber" FieldName="Price" CellWidth="80" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="LineNumber" FieldName="Number" CellWidth="200" Align="Center" Direction="LeftToRight" FormattingMethod="CustomRender" />
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

	function deleteRow(e) {
		gridUserPrivateNumbers.Event = e;
		if (!gridUserPrivateNumbers.IsSelectedRow())
			return;

		var guid = gridUserPrivateNumbers.SelectedGuid;
		messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm','danger', function (result) {
			if (result) {
				var isDelete = getAjaxResponse("DeletePrivateNumber", "Guid=" + guid);
				if (isDelete) {
					gridUserPrivateNumbers.Search();
				}
				else
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
			}
		});
	}
</script>
