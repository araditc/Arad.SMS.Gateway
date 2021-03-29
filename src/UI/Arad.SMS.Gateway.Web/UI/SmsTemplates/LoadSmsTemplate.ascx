<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoadSmsTemplate.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsTemplates.LoadSmsTemplate" %>

<GeneralTools:DataGrid runat="server" ID="gridSmsTemplate" DefaultSortField="CreateDate" ListCaption="SmsTemplateList"
	ShowRowNumber="true" SelectMode="Multiple" DisableNavigationBar="true" ListHeight="200">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="SmsBody" FieldName="Body" CellWidth="250" Align="Center" />
	</columns>
</GeneralTools:DataGrid>

<div class="buttonControlDiv">
	<input id="btnAddTemplate" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Add") %>" class="button" onclick="addSmsTemplateToSmsBody();" />
</div>


<script type="text/javascript">
	function showTemplateOfGroup() {
		var searchFilters = "";
		
		gridSmsTemplate.SearchFilters = searchFilters;
		gridSmsTemplate.Search();
	}

	function addSmsTemplateToSmsBody() {
		var data = gridSmsTemplate.GetSelectedRowData(["Body"]);
		var templateList = "";
		for (var counter = 0; counter < parseInt(importData(data, "resultCount")) ; counter++) {
			templateList += (importData(data, ("Body" + counter)) + "\n");
		}
		return closeModal(templateList);
	}
</script>
