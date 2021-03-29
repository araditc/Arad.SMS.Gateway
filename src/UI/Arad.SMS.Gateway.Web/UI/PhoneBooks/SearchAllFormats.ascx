<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchAllFormats.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PhoneBooks.SearchAllFormats" %>

<div id="advanceSearchContainer" class="modalWindow" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AdvancedSearch")%>" width="350px" height="120">
	<div>
		<div style="float: right; width: 300px;">
			<div class="titleDiv"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FormatTitle")%></div>
			<div class="controlDiv">
				<asp:TextBox CssClass="input" ID="txtAdvancedSearchFormatTitle" runat="server"></asp:TextBox>
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

<GeneralTools:DataGrid runat="server" ShowAdvancedSearch="true" ID="gridAllFormats" DefaultSortField="CreateDate" ListCaption="AllFormatList" ListHeight="420"
	SearchDivID="advanceSearchContainer" ShowRowNumber="true">
	<columns>
			<GeneralTools:DataGridColumnInfo Caption="FormatTitle" FieldName="FormatName" CellWidth="180" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="GroupTitle" FieldName="PhoneBookName" CellWidth="180" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="100" Align="Center" FormattingMethod="ImageButton" />
		</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function searchNumber() {
		var searchFilters = "";
		searchFilters += "FormatName{(" + $("#<%=txtAdvancedSearchFormatTitle.ClientID%>")[0].value + ")}";
		searchFilters += "PhoneBookName{(" + $("#<%=txtAdvancedSearchPhoneBookName.ClientID%>")[0].value + ")}";
		gridAllFormats.SearchFilters = searchFilters;
		gridAllFormats.Search();
		hideModal("advanceSearchContainer");
	}

	function deleteFormat(e) {
		gridAllFormats.Event = e;
		if (!gridAllFormats.IsSelectedRow())
			return;

		var guid = gridAllFormats.SelectedGuid;
		messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
			if (result) {
				if (getAjaxResponse('DeleteSmsFormat', "SmsFormatGuid=" + guid) == true) {
					gridAllFormats.Search();
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeleteRecord") %>', '', 'alert', 'success');
				}
				else
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord") %>', '', 'alert', 'danger');
			}
		});
	}
</script>
