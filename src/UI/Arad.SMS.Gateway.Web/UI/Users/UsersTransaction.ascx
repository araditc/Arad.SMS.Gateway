<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsersTransaction.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.UsersTransaction" %>

<GeneralTools:DataGrid runat="server" ID="gridUsersTransaction" DefaultSortField="CreateDate" ListCaption="TransactionList" ListHeight="420"
	ShowToolbar="true" ToolbarPosition="Top" ShowFooterRow="true" ShowRowNumber="true" GridComplete="setRowColor" ShowSearchToolbar="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="UserName" FieldName="UserName" Search="true" SearchOptions="{sopt: ['cn','eq'],clearSearch:true}" Sortable="false" Align="Center" CellWidth="80" />
		<GeneralTools:DataGridColumnInfo Caption="TypeCreditChange" FieldName="TypeCreditChange" SearchType="Select" Search="true" SearchOptions="{postData:{id:1},clearSearch:false,buildSelect:'createSelect'}" Sortable="false" CellWidth="85" Align="Center" Frozen="true" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Search="true" SearchType="Date" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" Sortable="false" CellWidth="100" Align="Center" Frozen="true" FormattingMethod="DateTimeShortDateTime" />
		<GeneralTools:DataGridColumnInfo Caption="PreviousCredit" FieldName="CurrentCredit" Sortable="false" CellWidth="110" Align="Center" FormattingMethod="NumberDecimal" />
		<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="Type" Hidden="true" Sortable="false" CellWidth="40" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="TypeTransaction" SearchType="Select" Search="true" SearchOptions="{postData:{id:2},clearSearch:false,buildSelect:'createSelect'}" Sortable="false" CellWidth="80" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="SmsCount" FieldName="Amount" Search="true" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" SearchFilterCssClass="numberInput" Sortable="false" CellWidth="110" Align="Center" FormattingMethod="NumberDecimal" />
		<GeneralTools:DataGridColumnInfo Caption="CurrentCredit" FieldName="NextCredit" Search="true" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" SearchFilterCssClass="numberInput" Sortable="false" CellWidth="90" FormattingMethod="NumberDecimal" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Description" FieldName="Description" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" Sortable="false" Align="Center" CellWidth="150" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
		$(function () {
		$.ajax({
			url: "DataGridHandler.aspx/GetUserDomains",
			contentType: 'application/json; charset=utf-8',
			dataType: "json",
			type: 'POST',
			data: "{userGuid:'<%=UserGuid%>'}",
			async: false,
			cache: false
		}).done(function (data) {
			var options = "<option value=''></option>";
			$.each(data.d, function (i) {
				options += '<option value="' + data.d[i].guid + '">' + data.d[i].name + '</option>';
			});

			$("#drpDomain").append(options);
		});

		$("#btnSearch").click(function (e) {
			search();
		});
		});

	function setRowColor(listId) {
		var rowIDs = $(listId).jqGrid('getDataIDs');
		var ret;
		if (rowIDs.length > 0) {
			{
				for (var i = 0; i < rowIDs.length; i++) {
					{
						ret = $(listId).jqGrid('getRowData', rowIDs[i]);
						if (ret['Type'] == 1)
							$('#' + $.jgrid.jqID(rowIDs[i])).addClass('success-gridRowColor');
						else
							$('#' + $.jgrid.jqID(rowIDs[i])).addClass('warning-gridRowColor');
					}
				}
			}
		}
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

	function search(gridId) {
		if (!gridId)
			gridUsersTransaction.TriggerToolbar();

		var domainGuid = $('#drpDomain option:selected')[0].value;

		var searchFilters = "";
		searchFilters += "[";
		searchFilters += "{'field':'DomainGuid','op':'eq','data':'" + domainGuid + "'}";
		searchFilters += "]";
		gridUsersTransaction.SearchFilters = searchFilters;
		gridUsersTransaction.Search();
	}
</script>
