<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserFish.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.UserFishes.UserFish" %>

<GeneralTools:DataGrid runat="server" showAdvancedSearch="false" ID="gridUserFishes" DefaultSortField="CreateDate" ListCaption="PaymentsList"
	ShowToolbar="false" ShowRowNumber="true" ListHeight="420">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Status" FieldName="Status" SearchType="Select" Search="true" SearchOptions="{postData:{id:3},clearSearch:false,buildSelect:'createSelect'}" ShowInExport="false" CellWidth="80" Align="Center" FormattingMethod="CustomRender"/>
			<GeneralTools:DataGridColumnInfo Caption="SmsCount" FieldName="SmsCount" Search="true" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" SearchFilterCssClass="numberInput" CellWidth="70" Align="Center" FormattingMethod="NumberDecimal"/>
		<GeneralTools:DataGridColumnInfo Caption="Amount" FieldName="Amount" Search="true" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" SearchFilterCssClass="numberInput" CellWidth="70" Align="Center" FormattingMethod="NumberDecimal"/>
		<GeneralTools:DataGridColumnInfo Caption="TypePayment" FieldName="Type" CellWidth="50" Align="Center" FormattingMethod="CustomRender"/>
		<GeneralTools:DataGridColumnInfo Caption="BillNumber" FieldName="BillNumber" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" SearchFilterCssClass="numberInput" CellWidth="80" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="PaymentDate" Search="true" SearchType="Date" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" FieldName="PaymentDate" CellWidth="80" Align="Center" Frozen="true" FormattingMethod="DateTimeShortDate"/>
		<GeneralTools:DataGridColumnInfo Caption="Account" FieldName="Account" CellWidth="250" Align="Center" FormattingMethod="CustomRender"/>
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
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
		gridUserFishes.Search();
	}
</script>
