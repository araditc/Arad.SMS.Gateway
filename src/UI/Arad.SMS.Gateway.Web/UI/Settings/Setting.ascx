<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Setting.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Settings.Setting" %>

<GeneralTools:DataGrid runat="server" ID="gridSettings" ListCaption="Setting" ListHeight="420"
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true" DisableNavigationBar="true" >
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Key" FieldName="Key" Search="true" CellWidth="150" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="Value" FieldName="Value" Search="true" CellWidth="150" Align="Center" />
	</columns>
</GeneralTools:DataGrid>