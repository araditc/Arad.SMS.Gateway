<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Role.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Roles.Role" %>

<GeneralTools:DataGrid runat="server" ID="gridRoles" DefaultSortField="CreateDate" ListCaption="RolesList" ListHeight="420"
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true" DisableNavigationBar="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="TheDefault" FieldName="IsDefault" Sortable="false" CellWidth="100" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="IsSalePackage" FieldName="IsSalePackage" Sortable="false" CellWidth="100" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="Title" FieldName="Title" Sortable="false" Align="Center" CellWidth="150" />
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Sortable="false" Align="Center" CellWidth="120" FormattingMethod="DateTimeShortDate" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="200" Align="Center" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function deleteRow(e) {
		gridRoles.Event = e;
		if (gridRoles.IsSelectedRow()) {
			var guid = gridRoles.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm','danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteRole", "Guid=" + guid);
					if (isDelete) {
						gridRoles.Search();
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}
</script>
