<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SentBox.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsReports.SentBox" %>

<GeneralTools:DataGrid runat="server" ShowAdvancedSearch="false" ID="gridSentBox" DefaultSortField="DeliveryStatus" ListCaption="SmsSentsList" ListHeight="420"
	SearchDivID="advanceSearchContainer" ShowRowNumber="true" ShowExportData="false" ShowToolbar="true" ToolbarPosition="Top" ShowSearchToolbar="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Status" FieldName="DeliveryStatus" SearchType="Select" Search="true" SearchOptions="{postData:{id:6},clearSearch:false,buildSelect:'createSelect'}" CellWidth="120" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="RecieverNumber" FieldName="ToNumber" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="300" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Time" FieldName="StatusDateTime" CellWidth="120" Align="Center" FormattingMethod="DateTimeShortDateTime" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function search(gridId) {
		gridSentBox.Search();
	}

	function createSelect(element) {
		var data = element.d;
		var s = '<select>';
		if (data.length) {
			s += '<option value="0"></option>';
			for (var i = 0, l = data.length; i < l ; i++) {
				s += '<option value="' + data[i].Value + '">' + data[i].Text + '</option>';
			}
		}
		return s += "</select>";
	}
</script>
