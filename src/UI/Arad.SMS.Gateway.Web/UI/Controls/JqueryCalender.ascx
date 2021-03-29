<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JqueryCalender.ascx.cs" Inherits="MessagingSystem.UI.Controls.JqueryCalender" %>


<script type="text/javascript">
	$(function () {
		debugger;
		$('#<%=txtDate.ClientID%>').datepicker({
			showAnim: 'fadeIn',
			changeMonth: true,
			changeYear: true,
			isRTL: true,
			dateFormat: 'yy/mm/dd',
			regional: 'fa',
			showButtonPanel: true
		});
	});
</script>

<asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
