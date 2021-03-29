<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Domain.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Domains.Domain" %>

<GeneralTools:DataGrid runat="server" ShowToolBar="true" ToolbarPosition="top" ID="gridDomains" ListHeight="420"
	DefaultSortField="Name" ListCaption="DomainList" ShowRowNumber="true" ShowSearchToolbar="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="DomainName" FieldName="Name" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="200" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="StartUpPage" FieldName="Desktop" CellWidth="100" Align="Center" FormattingMethod="CustomRender"/>
		<GeneralTools:DataGridColumnInfo Caption="DefaultPage" FieldName="DefaultPage" CellWidth="100" Align="Center" FormattingMethod="CustomRender"/>
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Align="Center" CellWidth="110" FormattingMethod="DateTimeShortDate"/>
		<GeneralTools:DataGridColumnInfo Caption="Action" Sortable="false" FieldName="Action" ShowInExport="false" CellWidth="250" Align="Center" FormattingMethod="ImageButton"/>
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function search(gridId) {
		gridDomains.Search();
	}

	function deleteRow(e) {
		gridDomains.Event = e;
		if (gridDomains.IsSelectedRow()) {
			var guid = gridDomains.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete") %>', '', 'Confirm', 'danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteDomain", "Guid=" + guid);
					if (isDelete) {
						gridDomains.Search();
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}
</script>