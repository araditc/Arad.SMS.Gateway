<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePicker.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Controls.DatePicker" EnableViewState="True" %>
<style type="text/css">
	.divDatePicker
	{
		display:inherit !important;
		width:100%;
	}
</style>
<asp:Panel ID="pnlDatePicker" runat="server">
	<asp:TextBox ID="txtIsActive" style="display:none" runat="server"></asp:TextBox>
	<div id="divDatePicker" class="divDatePicker">
		<div style="float:right;">
			<asp:TextBox ID="txtDate" style="display:none" runat="server"></asp:TextBox>
			<asp:TextBox CssClass="input" style="direction:rtl" ID="txtShowDate" runat="server" ReadOnly="true"></asp:TextBox>
		</div>
		<div style="float:right;">
			<img src="/pic/calendar.jpg" alt="" style="margin:0" onclick="if($('#<%=txtIsActive.ClientID %>')[0].value!='0') displayDatePicker('<%=txtShowDate.ClientID.Replace("_","$")%>','<%=txtDate.ClientID.Replace("_","$")%>', this);" />
			<img src="/pic/clear.gif" alt="" style="margin:0" onclick="if($('#<%=txtIsActive.ClientID %>')[0].value!='0') {$('#<%=txtShowDate.ClientID%>')[0].value = '';$('#<%=txtDate.ClientID%>')[0].value = '';}" />
		</div>
	</div>
</asp:Panel>
<script type="text/javascript">
	$("#<%=txtShowDate.ClientID%>")[0].value = $("#<%=txtDate.ClientID%>")[0].value;

	function getChristianDate(datePickerID) {
		var dtp = "#" + datePickerID + "_txtDate"
		solarDate = $(dtp)[0].value;
		solarYear = solarDate.split('/')[0];
		solarMounth = solarDate.split('/')[1];
		solarDay = solarDate.split('/')[2];

		var objDate = new Date();
		objDate.setFullYear(solarYear);
		objDate.setMonth(new Number(solarMounth) - 1);
		objDate.setDate(solarDay);
		var jdArray = new Array(
									objDate.getFullYear(),
									objDate.getMonth()+1 ,
									objDate.getDate()
									);
		var christianDate = ArrayToGregorianDate(jdArray);
		christianDate = christianDate.replace(/-/g, '/');
		return new Date(christianDate);
	}

	function getDatePickerValue(datePickerID) {
		var dtp = "#" + datePickerID + "_txtDate"
		solarDate = $(dtp)[0].value;
		return solarDate;
	}

	function setDatePickerValue(datePickerID, value) {
		var dtp = "#" + datePickerID + "_txtDate";
		var dtpShowDate = "#" + datePickerID + "_txtShowDate";

		$(dtp)[0].value = value.substring(0, 10);
		$(dtpShowDate)[0].value = value.substring(0, 10);
	}
</script>
