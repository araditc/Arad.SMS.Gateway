<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PanelInformation.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Widgets.PanelInformation.PanelInformation" %>

<script type="text/javascript">
	function setProgress(progress, x, max) {
		var value = parseInt((x * 100) / max, 10);
		$('#' + progress).progressbar({ value: value });
	}
	$(document).ready(function () {
		var panelExpireDays = eval('<%=panelExpireDays %>');
		$('#lblPanelExpire').html(panelExpireDays[0] + ' / ' + panelExpireDays[1]);
		setProgress('progressPanelExpire', panelExpireDays[0], panelExpireDays[1]);
	});
</script>

<div class="widget">
	<div class="div-title"><span><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PanelInformation")%></span></div>
	<div class="div-content">
		<div class="div-item">
			<img style="float: right;" src="../../../HomePages/AndroidDesktopFiles/Images/user.png" />
			<span style="float: right; margin-right: 10px;"><asp:Literal ID="ltrUserName" runat="server"></asp:Literal></span>
		</div>
		<div class="clear"></div>
		<br />
		<div class="div-item">
			<span style="float: right;"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RemainingDaysToExpirePanel")%></span>
			<span id="lblPanelExpire" style="float: left; direction: ltr;"></span>
			<div class="clear"></div>
			<br />
			<div id="progressPanelExpire" class="progressbar"></div>
		</div>
	</div>
</div>