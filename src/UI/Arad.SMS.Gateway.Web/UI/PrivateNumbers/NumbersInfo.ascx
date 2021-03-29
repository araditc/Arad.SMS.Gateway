<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NumbersInfo.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PrivateNumbers.NumbersInfo" %>

<GeneralTools:DataGrid runat="server" ID="gridUserPrivateNumbers" DefaultSortField="CreateDate" ListCaption="UserPrivateNumbersList" ListHeight="420"
	ShowRowNumber="true" ShowToolbar="true" ShowSearchToolbar="true" ToolbarPosition="Top">
	<columns>
			<GeneralTools:DataGridColumnInfo Caption="NumberGuid" FieldName="Guid" Hidden="true" CellWidth="85" Align="Center" FormattingMethod="Encrypt" />
		<GeneralTools:DataGridColumnInfo Caption="UserGuid" FieldName="UserGuid" Hidden="true" CellWidth="85" Align="Center" FormattingMethod="Encrypt" />
		<GeneralTools:DataGridColumnInfo Caption="LineNumber" FieldName="Number" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="110" Align="Center" Direction="LeftToRight" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="UserName" FieldName="UserName" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="100" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="70" Align="Right" FormattingMethod="ImageButton"/>
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function search(gridId) {
		gridUserPrivateNumbers.Search();
	}
</script>
