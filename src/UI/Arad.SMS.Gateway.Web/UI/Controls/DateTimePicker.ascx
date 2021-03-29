<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateTimePicker.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Controls.DateTimePicker" %>

<script type="text/javascript">
	$(document).ready(function () {
		$("#<%=txtDateTime.ClientID%>").attr('data-targetselector', '#<%=txtDateTime.ClientID%>');
	});
</script>

<div id="divDatePicker">
	<div class="input-group">
		<div class="input-group-addon" data-mddatetimepicker="true" data-trigger="click"
			data-targetselector="#<%=txtDateTime.ClientID %>" data-todate="true" data-enabletimepicker="true" data-placement="left">
			<span class="glyphicon glyphicon-calendar"></span>
		</div>
		<asp:TextBox ID="txtDateTime" class="form-control" Style="direction: ltr"
			data-MdDateTimePicker="true" data-enabletimepicker="true" data-trigger="click"
			data-todate="true" data-placement="bottom" runat="server"></asp:TextBox>
	</div>
</div>

<script type="text/javascript">
	function getDateTimePickerValue(dateTimePickerID) {
		var dtp = "#" + dateTimePickerID + "_txtDateTime"
		solarDateTime = $(dtp)[0].value;
		return solarDateTime;
	}
</script>