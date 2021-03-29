<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowUserSmsRates.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.ShowUserSmsRates" %>

<GeneralTools:DataGrid runat="server" ID="gridAgentRatio" ListCaption="TariffLists"
	ShowRowNumber="true" ListHeight="420" DisableNavigationBar="true">
	<Columns>
		<GeneralTools:DataGridColumnInfo FieldName="AgentID" Hidden="true" Align="Center" />
		<GeneralTools:DataGridColumnInfo FieldName="Price" Caption="BasePrice" CellWidth="150" Align="Center" />
		<GeneralTools:DataGridColumnInfo FieldName="Agent" Caption="SmsSenderAgent" CellWidth="150" Align="Center" />
		<GeneralTools:DataGridColumnInfo FieldName="Ratio" Caption="Ratio" Align="Center" />
	</Columns>
</GeneralTools:DataGrid>
