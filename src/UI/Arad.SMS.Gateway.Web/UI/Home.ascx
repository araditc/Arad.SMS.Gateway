<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Home" %>

<script src="/script/charts/highcharts.js" type="text/javascript"></script>
<script src="/script/charts/exporting.js" type="text/javascript"></script>

<script type="text/javascript">

	$(document).ready(function () {
		$('#mychart').highcharts({
			chart: {
				type: 'column',
				height: 500,
				spacingBottom: 15,
				spacingTop: 20,
				spacingLeft: 5,
				spacingRight: 15,
				borderWidth: 1,
				borderColor: '#ddd'
			},
			title: {
                text: '<b><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ReportSend") %></b>'
			},
			subtitle: {
				text: ''
			},
			xAxis: {
				lineWidth: 3,
				categories: ['']
			},
			yAxis: {
				title: {
					text: '<b></b>'
				},
				labels: {
					formatter: function () {
						return '';
					}            
				}
			},
			legend: {
				align: 'right',
				x: -50,
				verticalAlign: 'top',
				y: 50,
				floating: true,
				backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
				borderColor: '#CCC',
				borderWidth: 1,
				shadow: false,
				useHTML: true,
				labelFormatter: function() {
					return   '<span class="legend-title">'+this.name+'</span>';
            
				}
			},
			tooltip: {
				headerFormat: '<span style="font-size:10px">{point.key}</span><table border="1px" style="padding:2px;text-align:center">',
				pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}</td>' +
													'<td style="padding:0"><b>{point.y:,1f} <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Receiver") %></b><br/><b>{point.sms:,1f} <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sms") %></b></td></tr>',
				footerFormat: '</table>',
				shared: true,
				useHTML: true
			},
			plotOptions: {
				column: {
					dataLabels: {
						enabled: true,
						color: '#000',
						style: {
							fontWeight: 'bold'
						}	,
						inside: false,
						rotation: 360
					},
					events: {
						legendItemClick: function () {
							return false; 
						}
					}
				},

			},
			series: [{
				name: '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sent") %>',
				data: [{y:<%=dictionaryDelivery.Keys.Contains(Arad.SMS.Gateway.Common.DeliveryStatus.SentToItc)?dictionaryDelivery[Arad.SMS.Gateway.Common.DeliveryStatus.SentToItc].Item1:0%>,
					sms:<%=dictionaryDelivery.Keys.Contains(Arad.SMS.Gateway.Common.DeliveryStatus.SentToItc)?dictionaryDelivery[Arad.SMS.Gateway.Common.DeliveryStatus.SentToItc].Item2:0%>}],
				color: 'blue'
			}, {
				name: '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SentAndReceivedbyPhone") %>',
				data: [{y:<%=dictionaryDelivery.Keys.Contains(Arad.SMS.Gateway.Common.DeliveryStatus.SentAndReceivedbyPhone)?dictionaryDelivery[Arad.SMS.Gateway.Common.DeliveryStatus.SentAndReceivedbyPhone].Item1:0%>,
					sms:<%=dictionaryDelivery.Keys.Contains(Arad.SMS.Gateway.Common.DeliveryStatus.SentAndReceivedbyPhone)?dictionaryDelivery[Arad.SMS.Gateway.Common.DeliveryStatus.SentAndReceivedbyPhone].Item2:0%>}],
				color: 'green'
			}, {
				name: '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FaildSend") %>',
				data: [{y:<%=dictionaryDelivery.Keys.Contains(Arad.SMS.Gateway.Common.DeliveryStatus.NotSent)?dictionaryDelivery[Arad.SMS.Gateway.Common.DeliveryStatus.NotSent].Item1:0%>,
					sms:<%=dictionaryDelivery.Keys.Contains(Arad.SMS.Gateway.Common.DeliveryStatus.NotSent)?dictionaryDelivery[Arad.SMS.Gateway.Common.DeliveryStatus.NotSent].Item2:0%>}],
				color: 'red'
			}, {
				name: '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ReceivedByItc") %>',
				data: [{y:<%=dictionaryDelivery.Keys.Contains(Arad.SMS.Gateway.Common.DeliveryStatus.ReceivedByItc)?dictionaryDelivery[Arad.SMS.Gateway.Common.DeliveryStatus.ReceivedByItc].Item1:0%>,
					sms:<%=dictionaryDelivery.Keys.Contains(Arad.SMS.Gateway.Common.DeliveryStatus.ReceivedByItc)?dictionaryDelivery[Arad.SMS.Gateway.Common.DeliveryStatus.ReceivedByItc].Item2:0%>}],
				color: 'orange'
			}, {
				name: '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("BlackList") %>',
				data: [{y:<%=dictionaryDelivery.Keys.Contains(Arad.SMS.Gateway.Common.DeliveryStatus.BlackList)?dictionaryDelivery[Arad.SMS.Gateway.Common.DeliveryStatus.BlackList].Item1:0%>,
					sms:<%=dictionaryDelivery.Keys.Contains(Arad.SMS.Gateway.Common.DeliveryStatus.BlackList)?dictionaryDelivery[Arad.SMS.Gateway.Common.DeliveryStatus.BlackList].Item2:0%>}],
				color: '#000'
			}]
		});
	});

</script>

<div class="row">
	<asp:Button ID="btnReturn" runat="server" CssClass="btn btn-default" Text="Return" Style="border: 0; margin-right: 20px;" OnClick="btnReturn_Click" />
	<hr style="margin: 5px 0 5px 0;" />
	<div class="col-md-12 col-xs-12">
		<div class="col-md-1 col-xs-1"></div>
		<div id="mychart" class="col-md-10 col-xs-10"></div>
		<div class="col-md-1 col-xs-1"></div>
	</div>
</div>
