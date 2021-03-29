<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserAccess.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.UserAccesses.UserAccess" %>

<div style="width:485px;">
	<GeneralTools:DataGrid runat="server" showAdvancedSearch="true" ID="gridUserAccesses" DefaultSortField="Title" ListCaption="UserAccessesList" listDifferenceHeight="40" 
		ShowRowNumber="true" SelectMode="Multiple" StatusMultiSelectField="IsActive" DisableNavigationBar="true">
		<Columns>
			<GeneralTools:DataGridColumnInfo Caption="ServiceName" FieldName="ServiceTitle" Sortable="false" CellWidth="200" Align="Center"/>
			<GeneralTools:DataGridColumnInfo Caption="AccessName" FieldName="AccessTitle" Sortable="false" CellWidth="200" Align="Center" FormattingMethod="CustomRender"/>
			<GeneralTools:DataGridColumnInfo FieldName="IsActive" Hidden="true" Sortable="false" ShowInExport="false" Align="Center"/>
		</Columns>
	</GeneralTools:DataGrid>
	<div class="buttonControlDiv">
		<input id="btnSave" type="button" value='<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Register")%>' onclick="save();" class="button"/>
		<input id="btnCancel" type="button" value='<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel")%>' onclick="closeModal('false');" class="button"/>
	</div>
</div>
<script type="text/javascript">
	function save() {
		var data = gridUserAccesses.GetSelectedRowData(["Guid", "IsActive"]);
		var result = getAjaxResponse("SaveUserAccess", "Data=" + data + "&UserGuid=" + '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Request(this, "UserGuid")%>');

		if (result != "1")
			messageBox(result);
		else
			closeModal('true');
	}
</script>