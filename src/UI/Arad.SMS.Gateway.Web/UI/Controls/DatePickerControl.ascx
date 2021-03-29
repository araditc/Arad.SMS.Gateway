<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePickerControl.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Controls.DatePickerControl" %>

<script type="text/javascript">
	$(document).ready(function () {
		$("#<%=txtDate.ClientID%>").attr('data-targetselector', '#<%=txtDate.ClientID%>');
	});
</script>
<div id="divDatePicker">
	<div class="input-group">
		<div class="input-group-addon" data-mddatetimepicker="true" data-trigger="click"
			data-targetselector="#<%=txtDate.ClientID %>" data-todate="true" data-enabletimepicker="false" data-placement="left">
			<span class="glyphicon glyphicon-calendar"></span>
		</div>
		<asp:TextBox ID="txtDate" class="form-control" Style="direction: ltr"
			data-MdDateTimePicker="true" data-enabletimepicker="false" data-trigger="click"
			data-todate="true" data-placement="left" runat="server"></asp:TextBox>
	</div>
</div>

<script type="text/javascript">
	function getDatePickerValue(datePickerID) {
		var dtp = "#" + datePickerID + "_txtDate"
		solarDate = $(dtp)[0].value;
		return solarDate;
	}
</script>

<% if(Session["Language"].ToString() == "fa") { %>
    <script src="../../Scripts/bootstrap-datepicker.js"></script>
    <script src="../../Scripts/bootstrap-datepicker.fa.js"></script>
    <script src="../../Scripts/bootstrap-timepicker.min.js"></script>
<% }  else
   {%>
    <style>
        
        body {
            direction: ltr !important;
        }
        .control-label {
            float: left;
        }
    </style>
    <script src="../../Scripts/bootstrap-dataepicker-main.min.js"></script>
    <script src="../../Scripts/bootstrap-datepicker.en-GB.min.js"></script>
    <script src="../../Scripts/bootstrap-timepicker.min.js"></script>
<%} %>