<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveBooking.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Booking.SaveBooking" %>
<%@ Import Namespace="System.Web.Optimization" %>

<%: Scripts.Render("~/Scripts/fullcalendar") %>

<script type="text/javascript">

	$(document).ready(function() {
		
		$('#calendar').fullCalendar({
			header: {
				left: 'next,prev today',
				center: 'title',
				right: 'month,agendaWeek,agendaDay'
			},
			lang: 'fa',
			isJalaali: true,
			isRTL: true,
			defaultDate: '<%=DateTime.Now.ToString("yyyy-MM-dd")%>',
			editable: true,
			eventLimit: true, // allow "more" link when too many events
			events: [
				{
					title: 'All Day Event',
					start: '2016-01-01'
				},
				{
					title: 'Long Event',
					start: '2016-01-07',
					end: '2016-01-10'
				},
				{
					id: 999,
					title: 'Repeating Event',
					start: '2016-01-09T16:00:00'
				},
				{
					id: 999,
					title: 'Repeating Event',
					start: '2016-01-16T16:00:00'
				},
				{
					title: 'Conference',
					start: '2016-01-11',
					end: '2016-01-13'
				},
				{
					title: 'Meeting',
					start: '2016-01-12T10:30:00',
					end: '2016-01-12T12:30:00'
				},
				{
					title: 'Lunch',
					start: '2016-01-12T12:00:00'
				},
				{
					title: 'Meeting',
					start: '2016-01-12T14:30:00'
				},
				{
					title: 'Happy Hour',
					start: '2016-01-12T17:30:00'
				},
				{
					title: 'Dinner',
					start: '2016-01-12T20:00:00'
				},
				{
					title: 'Birthday Party',
					start: '2016-01-13T07:00:00'
				},
				{
					title: 'Click for Google',
					url: 'http://google.com/',
					start: '2016-01-28'
				}
			]
		});
	});
	
</script>

<div class="row">
	<div class="col-xs-12">
		<div class="row">
			<div class="col-sm-1"></div>
			<div class="col-sm-10">
				<div class="space"></div>
				<div id="calendar"></div>
			</div>
			<div class="col-sm-1"></div>
		</div>
	</div>
</div>
