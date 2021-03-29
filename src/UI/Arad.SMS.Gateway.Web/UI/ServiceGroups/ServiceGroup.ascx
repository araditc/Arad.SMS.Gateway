<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceGroup.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.ServiceGroups.ServiceGroup" %>

<div id="advanceSearchContainer" class="modalWindow" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AdvancedSearch")%>" width="350px" Height="90">
	<div class="titleDiv"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Title")%></div>
	<div class="controlDiv"><asp:TextBox ID="txtAdvancedSearchTitle" runat="server" CssClass="input"></asp:TextBox></div>
	<div class="buttonControlDiv">
		<input id="btnAdvancedSearch" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Search") %>" class="button" onclick="searchServiceGroup();" />
		<input id="btnCancelAdvancedSearch" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %>" class="button" onclick="hideModal('advanceSearchContainer');" />
	</div>
</div>

<GeneralTools:DataGrid runat="server" showAdvancedSearch="true" ID="gridServiceGroup" DefaultSortField="Order" ListCaption="ServiceGroupsList" ListHeight="420"
	SearchDivID="advanceSearchContainer" ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<Columns>
		<GeneralTools:DataGridColumnInfo Caption="Icon"  ShowInExport="false" Sortable="false" FieldName="IconAddress" CellWidth="85" Align="Center" FormattingMethod="Image"/>
		<GeneralTools:DataGridColumnInfo Caption="Title" FieldName="Title" CellWidth="150" Align="Center" Frozen="true"/>
		<GeneralTools:DataGridColumnInfo Caption="Order" FieldName="Order" CellWidth="100" Align="Center" Frozen="true"/>
		<GeneralTools:DataGridColumnInfo Caption="CountService" FieldName="CountService" CellWidth="100" Align="Center" Frozen="true"/>
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Align="Center" CellWidth="110" FormattingMethod="DateTimeShortDate"/>
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="250" Align="Center" FormattingMethod="ImageButton"/>
	</Columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function searchServiceGroup() {
		var searchFilters = "";
		searchFilters += "Title{(" + $("#<%=txtAdvancedSearchTitle.ClientID%>")[0].value + ")}";
		gridServiceGroup.SearchFilters = searchFilters;
		gridServiceGroup.Search();
		hideModal("advanceSearchContainer");
	}

	function deleteRow(e) {
		gridServiceGroup.Event = e;
		if (gridServiceGroup.IsSelectedRow()) {
			var guid = gridServiceGroup.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteServiceGroup", "Guid=" + guid);
					if (isDelete) {
						gridServiceGroup.Search();
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeleteRecord")%>', '', 'alert', 'success');
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}
</script>