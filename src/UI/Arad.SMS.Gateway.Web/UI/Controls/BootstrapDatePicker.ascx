<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BootstrapDatePicker.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Controls.BootstrapDatePicker" %>

<script type="text/javascript">
	$(document).ready(function () {
		$("#<%=txtDate.ClientID%>").datepicker({
			changeMonth: true,
			changeYear: true,
		});
	});
</script>
<script type="text/javascript">
    function getDatePickerValue(datePickerID) {
        var dtp = "#" + datePickerID + "_txtDate";
        var dateTime = $(dtp).datepicker().val();
        return dateTime;
    }
</script>
<div id="divDatePicker">
	<div class="form-group">
		<div class="col-sm-12">
			<div class="input-group bootstrap-timepicker">
				<asp:TextBox ID="txtDate" class="form-control dateTime" runat="server" ReadOnly="true"></asp:TextBox>
				<span class="input-group-addon calendar">
					<i class="fa fa-calendar bigger-110"></i>
				</span>
			</div>
		</div>
	</div>
</div>
<style>
    .datepicker {
        width: 235px !important;
    }
    #ui-datepicker-div {
        z-index: 1100 !important;
    }
    .ui-datepicker {
        z-index: 1100 !important;
    }
    .modal-backdrop {
        z-index: 90;
    }
    .bootstrap-timepicker-widget {
        z-index: 9999;
    }
    #advanceSearchContainer {
        z-index: 100;
    }
    .datepicker-switch {
        text-align: center;
    }
</style>

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
<%} %>