<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchAllUserField.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PhoneBooks.SearchAllUserField" %>

<div id="advanceSearchContainer" class="modalWindow" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AdvancedSearch")%>" width="350px" height="120">
	<div>
		<div style="float: right; width: 300px;">
			<div class="titleDiv"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FieldTitle")%></div>
			<div class="controlDiv">
				<asp:TextBox CssClass="input" ID="txtAdvancedSearchFieldTitle" runat="server"></asp:TextBox>
			</div>
			<div class="titleDiv"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupName")%></div>
			<div class="controlDiv">
				<asp:TextBox CssClass="input" ID="txtAdvancedSearchPhoneBookName" runat="server"></asp:TextBox>
			</div>
		</div>
	</div>
	<div class="buttonControlDiv" style="margin-bottom: 10px;">
		<input id="btnAdvancedSearch" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Search") %>" class="button" onclick="searchNumber(); " />
		<input id="btnCancelAdvancedSearch" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %>" class="button" onclick="hideModal('advanceSearchContainer');" />
	</div>
</div>

<GeneralTools:DataGrid runat="server" ShowAdvancedSearch="true" ID="gridAllUserFields" DefaultSortField="FieldType" ListCaption="AllFormatList" ListHeight="420"
	SearchDivID="advanceSearchContainer" ShowRowNumber="true">
	<columns>
			<GeneralTools:DataGridColumnInfo Caption="GroupTitle" FieldName="PhoneBookName" CellWidth="180" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="FieldTitle" FieldName="FieldName" CellWidth="180" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="FieldType" CellWidth="180" Align="Center" FormattingMethod="CustomRender" />
		</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function searchNumber() {
		var searchFilters = "";
		searchFilters += "FieldName{(" + $("#<%=txtAdvancedSearchFieldTitle.ClientID%>")[0].value + ")}";
		searchFilters += "PhoneBookName{(" + $("#<%=txtAdvancedSearchPhoneBookName.ClientID%>")[0].value + ")}";
		gridAllUserFields.SearchFilters = searchFilters;
		gridAllUserFields.Search();
		hideModal("advanceSearchContainer");
	}
</script>
