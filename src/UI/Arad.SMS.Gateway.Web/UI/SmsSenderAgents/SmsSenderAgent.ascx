<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmsSenderAgent.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsSenderAgents.SmsSenderAgent" %>

<GeneralTools:DataGrid runat="server" ID="gridAgents" DefaultSortField="CreateDate" ListCaption="SmsSenderAgentsList" ListHeight="420"
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="ID" FieldName="ID" CellWidth="50" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Name" FieldName="Name" CellWidth="120" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="SmsSenderAgent" FieldName="SmsSenderAgentReference" CellWidth="120" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="Send" FieldName="IsSendActive" CellWidth="50" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="Recieve" FieldName="IsRecieveActive" Hidden="true" CellWidth="50" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="SendBulk" FieldName="IsSendBulkActive" CellWidth="100" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="CheckMessageID" FieldName="CheckMessageID" Hidden="true" CellWidth="100" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Align="Center" CellWidth="70" FormattingMethod="DateTimeShortDate" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="100" Align="Center" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function refreshGrid(result) {
		if (result == "true") {
			gridAgents.Search();
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("InsertRecord")%>', '', 'alert', 'success');
		}
	}

	function addNewAgent() {
		var address = '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SaveSmsSenderAgent,Session)%>';
		getAjaxPage(address, "ActionType=insert", '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SaveSmsSenderAgent") %>', 'refreshGrid');
	}

	function editAgent(e) {
		gridAgents.Event = e;
		if (!gridAgents.IsSelectedRow())
			return;

		var address = '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsSenderAgents_SaveSmsSenderAgent,Session)%>';

		getAjaxPage(address, "AgentGuid=" + gridAgents.SelectedGuid + "&ActionType=edit", '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("EditSmsSenderAgent") %>', 'refreshGrid');
	}
</script>
