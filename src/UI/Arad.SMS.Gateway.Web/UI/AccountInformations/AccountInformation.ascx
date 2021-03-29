<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountInformation.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.AccountInformations.AccountInformation" %>

<GeneralTools:DataGrid runat="server" ID="gridAccountInformations" DefaultSortField="CreateDate" ListCaption="AccountInformationList" ListHeight="420" 
	 ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<Columns>
		<GeneralTools:DataGridColumnInfo Caption="Active" FieldName="IsActive" CellWidth="50" Align="Center"  FormattingMethod="BooleanOnOff"/>
		<GeneralTools:DataGridColumnInfo Caption="OnlineGatewayIsActive" FieldName="OnlineGatewayIsActive" CellWidth="80" Align="Center"  FormattingMethod="BooleanOnOff"/>
		<GeneralTools:DataGridColumnInfo Caption="OwnerAccountnumber" FieldName="Owner" CellWidth="100" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="Bank" FieldName="Bank" CellWidth="70" Align="Center" FormattingMethod="CustomRender"/>
		<GeneralTools:DataGridColumnInfo Caption="AccountNo" FieldName="AccountNo" CellWidth="120" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="CardNo" FieldName="CardNo" CellWidth="120" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Align="Center" CellWidth="70" FormattingMethod="DateTimeShortDate"/>
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="150" Align="Center" FormattingMethod="ImageButton"/>
	</Columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function refreshGrid(result) {
		if (result == "true") {
			gridAccountInformations.Search();
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("InsertRecord")%>', '', 'alert', 'success');
		}
	}

	function deleteRow(e) {
		gridAccountInformations.Event = e;
		if (gridAccountInformations.IsSelectedRow()) {
			var guid = gridAccountInformations.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteAccountInformation", "Guid=" + guid);
					if (isDelete) {
						gridAccountInformations.Search();
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeleteRecord")%>', '', 'alert', 'success');
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}
</script>