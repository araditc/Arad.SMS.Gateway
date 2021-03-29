
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BootstrapDateTimePicker.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Controls.BootstrapDateTimePicker" %>

<%@ Import Namespace="System.Web.Optimization" %>
	
<%--<%:Styles.Render("~/Content/bootstrap-datepicker")%>--%>

<script type="text/javascript">
    
    $(document).ready(function () {
        $("#<%=txtDate.ClientID%>").datepicker({
            changeMonth: true,
            changeYear: true,
        });
        $('#<%=txtTime.ClientID%>').timepicker({
            minuteStep: 1,
            showSeconds: true,
            showMeridian: false
        }).next().on(ace.click_event, function () {
            $(this).prev().focus();
        });

        jQuery.fn.extend({
            getDateTimePickerValue: function () {

                var dateTime = $(this).datepicker().val();
                return dateTime;
            }
        });
    });
</script>

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

<div id="divDatePicker">
	<div class="form-group">
		<div class="col-sm-12">
			<div class="input-group bootstrap-timepicker col-sm-6">
				<asp:TextBox ID="txtTime" class="form-control" runat="server" ReadOnly="true" style="text-align:center"></asp:TextBox>
				<span class="input-group-addon">
					<i class="fa fa-clock-o bigger-110"></i>
				</span>
			</div>
			<div class="input-group bootstrap-timepicker col-sm-6">
				<asp:TextBox ID="txtDate" class="form-control dateTime" runat="server" ReadOnly="true"></asp:TextBox>
				<span class="input-group-addon calendar">
					<i class="fa fa-calendar bigger-110"></i>
				</span>
			</div>
		</div>
	</div>
</div>

<%--<%: Scripts.Render("~/Scripts/bootstrap-datepicker") %>--%>



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
