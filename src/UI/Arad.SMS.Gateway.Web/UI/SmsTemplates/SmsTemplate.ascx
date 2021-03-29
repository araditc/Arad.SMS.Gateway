<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmsTemplate.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsTemplates.SmsTemplate" %>

<div id="advanceSearchContainer" class="modalWindow" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AdvancedSearch")%>" Width="300px" height="120">
	<div class="titleDiv"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsBody") %></div>
	<div class="controlDiv"><asp:TextBox runat="server" id="txtAdvancedSearchSmsBody" CssClass="input"></asp:TextBox></div>
	<div class="buttonControlDiv">
		<input id="btnAdvancedSearch" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Search") %>" class="button" onclick="searchSmsTemplate();" />
		<input id="btnCancelAdvancedSearch" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %>" class="button"  onclick="hideModal('advanceSearchContainer');" />
	</div>
</div>

<GeneralTools:DataGrid runat="server" ID="gridSmsTemplate" DefaultSortField="CreateDate" ListCaption="SmsTemplateList" ListHeight="420" 
	SearchDivID="advanceSearchContainer" ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<Columns>
		<GeneralTools:DataGridColumnInfo Caption="SmsBody" FieldName="Body" CellWidth="250" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Align="Center" CellWidth="70" FormattingMethod="DateTimeShortDate"/>
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="50" Align="Center" FormattingMethod="ImageButton"/>
	</Columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function searchSmsTemplate() {
		var searchFilters = "";
		searchFilters += "Body{(" + $("#<%=txtAdvancedSearchSmsBody.ClientID%>")[0].value + ")}";
		gridSmsTemplate.SearchFilters = searchFilters;
		gridSmsTemplate.Search();
		hideModal('advanceSearchContainer');
	}

	function refreshGrid(result) {
		if (result == "true") {
			gridSmsTemplate.Search();
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("InsertRecord")%>', '', 'alert', 'success');
		}
	}

	function deleteRow(e) {
		gridSmsTemplate.Event = e;
		if (!gridSmsTemplate.IsSelectedRow())
			return;

		var guid = gridSmsTemplate.SelectedGuid;
		messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete") %>', '', 'Confirm','danger', function (result) {
			if (result) {
				var isDelete = getAjaxResponse("DeleteSmsTemplate", "Guid=" + guid);
				if (isDelete) {
					gridSmsTemplate.Search();
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeleteRecord")%>', '', 'alert', 'success');
				}
				else
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
			}
		});
	}
	</script>