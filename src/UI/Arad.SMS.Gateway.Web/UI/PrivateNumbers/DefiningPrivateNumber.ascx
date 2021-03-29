<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefiningPrivateNumber.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PrivateNumbers.DefiningPrivateNumber" %>

<GeneralTools:DataGrid runat="server" ID="gridPrivateNumbers" DefaultSortField="CreateDate" ListCaption="PrivateNumbersList" ListHeight="420"
	ShowSearchToolbar="true" ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Active" FieldName="IsActive" CellWidth="50" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="Public" FieldName="IsPublic" CellWidth="50" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="SmsSenderAgent" FieldName="Name" CellWidth="110" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="Type" CellWidth="50" Align="Center" FormattingMethod="CustomRender"/>
		<GeneralTools:DataGridColumnInfo Caption="ServiceID" FieldName="ServiceID" CellWidth="70" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="ServicePrice" FieldName="ServicePrice" CellWidth="60" Align="Center" FormattingMethod="NumberDecimal"/>
		<GeneralTools:DataGridColumnInfo Caption="LineNumber" FieldName="Number" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="110" Align="Center" Direction="LeftToRight" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Align="Center" CellWidth="70" FormattingMethod="DateTimeShortDate" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="100" Align="Center" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function search(gridId) {
		gridPrivateNumbers.Search();
	}
</script>
