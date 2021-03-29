<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInbox.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsReports.UserInbox" %>

<generaltools:datagrid runat="server" id="gridUsersInbox" defaultsortfield="ReceiveDateTime" listcaption="SmsRecievedList" listheight="420"
	showrownumber="true" showsearchtoolbar="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="UserName" FieldName="UserName" Sortable="false" Search="true" SearchOptions="{sopt: ['cn','eq'],clearSearch:true}" CellWidth="80" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="SmsBody" Sortable="false" FieldName="SmsText" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="200" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="Sender" Sortable="false" FieldName="Sender" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="100" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="Receiver" Sortable="false" FieldName="Receiver" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="100" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="ReceivedDate" FieldName="ReceiveDateTime" Search="true" SearchType="Date" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" CellWidth="100" Align="Center" FormattingMethod="DateTimeShortDateTime"/>
	</columns>
</generaltools:datagrid>

<script type="text/javascript">
	function search(gridId) {
		if (!gridId)
			gridUsersInbox.TriggerToolbar();
		gridUsersInbox.Search();
	}
</script>