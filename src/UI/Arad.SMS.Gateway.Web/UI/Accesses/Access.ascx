<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Access.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Accesses.Access" %>

<div id="advanceSearchContainer" class="modalWindow" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AdvancedSearch")%>" width="350px" style="padding-bottom:10px;" height="90">
	<div class="titleDiv"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ServiceName")%></div>
	<div class="controlDiv"><asp:DropDownList ID="drpAdvanceSearchService" runat="server" CssClass="input" Width="180px"></asp:DropDownList></div>
	<div class="buttonControlDiv">
		<input id="btnAdvancedSearch" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Search") %>" class="button" onclick="searchAccess();" />
		<input id="btnCancelAdvancedSearch" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %>" class="button" onclick="hideModal('advanceSearchContainer');" />
	</div>
</div>

<div style="width:900px;">
	<GeneralTools:DataGrid runat="server" showAdvancedSearch="true" ID="gridAccesses" DefaultSortField="CreateDate" ListCaption="AccessesList"  listDifferenceHeight="40"
		SearchDivID="advanceSearchContainer" ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
		<Columns>
			<GeneralTools:DataGridColumnInfo Caption="ServiceGuid" FieldName="ServiceGuid" Hidden="true" CellWidth="85" Align="Center" Frozen="true" FormattingMethod="Encrypt"/>
			<GeneralTools:DataGridColumnInfo Caption="IsUsed" FieldName="IsUsed" Hidden="true" CellWidth="85" Align="Center" Frozen="true"/>
			<GeneralTools:DataGridColumnInfo Caption="ServiceName" FieldName="ServiceName" Sortable="false" CellWidth="150" Align="Center" Frozen="true"/>
			<GeneralTools:DataGridColumnInfo Caption="AccessName" FieldName="ReferencePermissionsKey" CellWidth="300" Align="Center" Frozen="true"  FormattingMethod="CustomRender"/>
			<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Align="Center" CellWidth="110" FormattingMethod="DateTimeShortDate"/>
			<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="250" Align="Center" FormattingMethod="ImageButton"/>
		</Columns>
	</GeneralTools:DataGrid>
</div>

<script type="text/javascript">
	function searchAccess() {
		var searchFilters = "";

		searchFilters += "ServiceGuid{(" + $("#<%=drpAdvanceSearchService.ClientID%>")[0].value + ")}";
		gridAccesses.SearchFilters = searchFilters;
		gridAccesses.Search();
		hideModal("advanceSearchContainer");
	}

	function refreshGrid(result) {
		if (result == "true") {
			gridAccesses.Search();
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("InsertRecord")%>','','alert','success');
		}
	}

	function addNewAccess() {
		var address = '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Accesses_SaveAccess,Session)%>';
		getAjaxPage(address, 'ActionType=insert', '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SaveAccess") %>', 'refreshGrid');
	}

	function editRow(e) {
		gridAccesses.Event = e;
		if (!gridAccesses.IsSelectedRow())
			return;

		var address = '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Accesses_SaveAccess,Session)%>';
		var guid = gridAccesses.SelectedGuid;
		getAjaxPage(address, "Guid=" + guid + "&ActionType=edit", '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("EditAccess") %>', 'refreshGrid');
	}

	function deleteRow(e) {
		gridAccesses.Event = e;
		if (!gridAccesses.IsSelectedRow())
			return;
		var isUsed = gridAccesses.GetSelectedRowFieldValue('IsUsed');
		if (gridAccesses.IsSelectedRow() && isUsed!="1") {
			var guid = gridAccesses.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteAccess", "Guid=" + guid);
					if (isDelete) {
						gridAccesses.Search();
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeleteRecord")%>');
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>');
				}
			});
		}
		else
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeleteAccessError")%>');
	}
</script>