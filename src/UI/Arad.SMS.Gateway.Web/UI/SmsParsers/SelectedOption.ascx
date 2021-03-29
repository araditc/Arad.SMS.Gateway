<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectedOption.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsParsers.SelectedOption" %>

<GeneralTools:DataGrid runat="server" ID="gridSelectedOption" DefaultSortField="ReceiveDateTime" ListCaption="Poll" ListHeight="420"
	ShowToolbar="true" ShowSearchToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="SmsBody" Sortable="false" FieldName="SmsText" CellWidth="200" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="Sender" Sortable="false" FieldName="Sender" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="100" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="Receiver" Sortable="false" FieldName="Receiver" CellWidth="100" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="ReceivedDate" FieldName="ReceiveDateTime" CellWidth="100" Align="Center" FormattingMethod="DateTimeShortDateTime"/>
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	$(function () {
		$.ajax({
			url: "DataGridHandler.aspx/GetParserOption",
			contentType: 'application/json; charset=utf-8',
			dataType: "json",
			type: 'POST',
			data: "{guid:'<%=ParserGuid%>'}",
			async: false,
			cache: false
		}).done(function (data) {
            var seloption = "<option value=''><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("All") %></option>";
			$.each(data.d, function (i) {
				seloption += '<option value="' + data.d[i].guid + '">' + data.d[i].key + '</option>';
			});

			$("#drpOptions").css({ 'width': '100px', 'margin': '5px' });

			$("#drpOptions").append(seloption);
		});

		$("#txtLottery").keyup(function (e) {
			if (e.keyCode == 13) {
				search();
			}
		});

		$("#btnSearch").click(function (e) {
			search();
		});

	});

	function search(gridId) {
		if (!gridId)
			gridSelectedOption.TriggerToolbar();

		var formulaGuid = $('#drpOptions option:selected')[0].value;
		var lottery = $("#txtLottery")[0].value;

		var searchFilters = "";
		searchFilters += "[";
		searchFilters += "{'field':'ParserFormulaGuid','op':'eq','data':'" + formulaGuid + "'},";
		searchFilters += "{'field':'Lottery','op':'eq','data':'" + lottery + "'}";
		searchFilters += "]";
		gridSelectedOption.SearchFilters = searchFilters;
		gridSelectedOption.Search();
	}
</script>
