<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tariff.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Widgets.Tariff.Tariff" %>

<div class="widget">
	<div class="div-title"><span><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TariffLists") %></span></div>
	<div class="div-content">
		<div class="div-item">
			<%=SmsRates%>
			<div class="clear"></div>
		</div>
	</div>
</div>