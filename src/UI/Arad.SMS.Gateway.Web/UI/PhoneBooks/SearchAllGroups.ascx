<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchAllGroups.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PhoneBooks.SearchAllGroups" %>
<%@ Register Src="~/UI/Controls/DatePicker.ascx" TagName="DatePicker" TagPrefix="SMS" %>

<GeneralTools:DataGrid runat="server" ID="gridAllNumbers" DefaultSortField="CreateDate" ListCaption="AllNumbersList" ListHeight="420"
	ShowSearchToolbar="true" ShowRowNumber="true">
	<columns>
			<GeneralTools:DataGridColumnInfo Caption="Group" FieldName="PhoneBookTitle" CellWidth="130" Align="Center"/>
			<GeneralTools:DataGridColumnInfo Caption="FirstName" FieldName="FirstName" CellWidth="70" Align="Center"/>
			<GeneralTools:DataGridColumnInfo Caption="LastName" FieldName="LastName" CellWidth="70" Align="Center"/>
			<GeneralTools:DataGridColumnInfo Caption="Sex" FieldName="Sex" CellWidth="60" Align="Center" FormattingMethod="CustomRender"/>
			<GeneralTools:DataGridColumnInfo Caption="BirthDate" FieldName="BirthDate" Align="Center" CellWidth="70" FormattingMethod="DateTimeShortDate"/>
			<GeneralTools:DataGridColumnInfo Caption="CellPhone" FieldName="CellPhone" Search="true" SearchOptions="{sopt: ['cn','eq'],clearSearch:true}" Sortable="false" CellWidth="90" Align="Center"/>
			<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Align="Center" CellWidth="70" FormattingMethod="DateTimeShortDate"/>
		</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function search(gridId) {
		gridAllNumbers.Search();
	}
</script>
