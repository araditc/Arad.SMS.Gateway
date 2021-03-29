<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageRoute.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsSenderAgents.MessageRoute" %>

<GeneralTools:DataGrid runat="server" ID="gridRoutes" DefaultSortField="CreateDate" ListCaption="MessageRouteList" ListHeight="420"
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Name" FieldName="Name" CellWidth="120" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Operator" FieldName="Operator" CellWidth="120" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="UserName" FieldName="Username" CellWidth="150" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="100" Align="Center" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>