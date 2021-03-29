<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NumberStatus.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PrivateNumbers.NumberStatus" %>

<GeneralTools:DataGrid runat="server" ID="gridUserPrivateNumbers" DefaultSortField="CreateDate" ListCaption="UserPrivateNumbersList" ListHeight="420"
	ShowRowNumber="true" ShowToolbar="true" ShowSearchToolbar="true" ToolbarPosition="Top">
	<columns>
			<GeneralTools:DataGridColumnInfo Caption="NumberGuid" FieldName="Guid" Hidden="true" CellWidth="85" Align="Center" FormattingMethod="Encrypt" />
		<GeneralTools:DataGridColumnInfo Caption="Active" FieldName="IsActive" CellWidth="35" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="Public" FieldName="IsPublic" CellWidth="70" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="Type" CellWidth="70" Align="Center" FormattingMethod="CustomRender"/>
		<GeneralTools:DataGridColumnInfo Caption="ServiceID" FieldName="ServiceID" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="70" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="ServicePrice" FieldName="ServicePrice" CellWidth="110" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="LineNumber" FieldName="Number" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="110" Align="Center" Direction="LeftToRight" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="UserName" FieldName="UserName" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="100" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="70" Align="Right" FormattingMethod="ImageButton"/>
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
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
