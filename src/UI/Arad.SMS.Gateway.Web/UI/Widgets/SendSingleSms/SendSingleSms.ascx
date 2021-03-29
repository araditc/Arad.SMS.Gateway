<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendSingleSms.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Widgets.SendSingleSms.SendSingleSms" %>

<div class="widget" style="width: 350px;">
	<div class="div-title"><span><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendSingleSmsQuickly")%></span></div>
	<div class="div-content">
		<div class="div-item">
			<div style="float: right; width: 80px;">
				<span><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SenderNumber") %></span>
			</div>
			<div style="float: right;">
				<asp:DropDownList ID="drpSendNumber" runat="server" isRequired="true" CssClass="input" Width="200px"></asp:DropDownList>
			</div>
		</div>
		<div class="clear"></div>
		<br />
		<div class="div-item">
			<div style="float: right; width: 80px;">
				<span><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Reciever") %></span>
			</div>
			<div style="float: right;">
				<asp:TextBox ID="txtReceiverNumber" runat="server" Width="200px" CssClass="textbox"></asp:TextBox>
			</div>
		</div>
		<div class="clear"></div>
		<br />
		<div class="div-item">
			<div style="float: right; width: 80px;">
				<span><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsBody")%></span>
			</div>
			<div style="float: right;">
				<GeneralTools:SmsBodyBox runat="server" ID="txtSmsBody" IsRequired="true" Width="200"></GeneralTools:SmsBodyBox>
			</div>
		</div>
		<div class="clear"></div>
		<br />
		<div class="div-item">
			<asp:Button ID="btnSend" runat="server" Text="Send" CssClass="button" 
				onclick="btnSend_Click" />
		</div>
	</div>
</div>