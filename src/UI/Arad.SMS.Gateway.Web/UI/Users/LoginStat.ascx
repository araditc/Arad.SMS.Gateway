<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginStat.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.LoginStat" %>

<GeneralTools:DataGrid runat="server" ID="gridLoginStats" DefaultSortField="CreateDate" ListCaption="LoginStat" ListHeight="420"
	ShowRowNumber="true">
	<Columns>
		<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="Type" CellWidth="150" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="IP" FieldName="IP" Align="Center" CellWidth="200" />
		<GeneralTools:DataGridColumnInfo Caption="DateTime" FieldName="CreateDate" Align="Center" CellWidth="100" FormattingMethod="DateTimeShortDateTime" />
	</Columns>
</GeneralTools:DataGrid>
