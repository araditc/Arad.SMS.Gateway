<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParserChart.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsParsers.ParserChart" %>

<script type="text/javascript">
	$(document).ready(function () {
		var options = {
			chart: {
				renderTo: 'mychart',
				type: 'column',
			},
			title: {
                text: '<b><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Participants") %></b>',
				x: -20 //center
			},
			xAxis: {
				categories: ['']
			},
			yAxis: {
				lineWidth: 1,
				min: 0,
				title: {
					text: '<b></b>'
				}
			},
			tooltip: {
				headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
				pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
						'<td style="padding:0"><b>{point.y:1f} <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Number") %></b></td></tr>',
				footerFormat: '</table>',
				shared: true,
				useHTML: true
			},
			legend: {
				layout: 'vertical',
				align: 'right',
				verticalAlign: 'top',
				x: -10,
				y: 100,
				borderWidth: 0
			},
			plotOptions: {
				column: {
					dataLabels: {
						enabled: true,
						color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
						style: {
							textShadow: '0 0 3px black, 0 0 3px black'
						}
					}
				}
			},
			series: []
		}

		$.ajax({
			url: "DataGridHandler.aspx/GetParserSmsReport",
			contentType: 'application/json; charset=utf-8',
			dataType: "json",
			type: 'POST',
			data: "{guid:'<%=ParserGuid%>'}",
			async: false,
			cache: false
		}).done(function (data) {
			items = jQuery.parseJSON(data.d);
			for (counter = 0; counter < items.length; counter++) {
				options.series[counter] = items[counter];
			}
			chart = new Highcharts.Chart(options);
		});
	});

</script>

<div class="row">
	<asp:Button ID="btnReturn" runat="server" CssClass="btn btn-default" Text="Return" Style="border: 0; margin-right: 20px;" OnClick="btnReturn_Click" />
	<hr style="margin: 5px 0 5px 0;" />
	<div class="col-md-12 col-xs-12">
		<div class="col-md-1 col-xs-1" style="height: 700px;"></div>
		<div id="mychart" class="col-md-10 col-xs-10" style="border: 1px solid silver; height: 500px;"></div>
		<div class="col-md-1 col-xs-1"></div>
	</div>
</div>

<script src="/script/charts/highcharts.js" type="text/javascript"></script>
<script src="/script/charts/exporting.js" type="text/javascript"></script>
