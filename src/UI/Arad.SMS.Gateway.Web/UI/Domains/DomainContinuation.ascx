<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DomainContinuation.ascx.cs"
	Inherits="MessagingSystem.UI.Domains.DomainContinuation" %>
<fieldset>
	<legend><%=GeneralLibrary.Language.GetString("DomainContinuation")%></legend>
	<div class="ui-widget-header" style="height:50px;border-radius:15px 15px 0px 0px">
		<div class="ui-state-error style" style="height:20px;border-radius:15px 15px 0px 0px;" ><span style="margin-right:15px"><%=GeneralLibrary.Language.GetString("Attention")%></span></div>
		<div class="divHeaderHelp"><%=GeneralLibrary.Language.GetString("DomainContinuationAttention")%></div> 
	</div>
	<div style="border: 1px solid #aaa;width: 500px; height: 200px;margin: 30px auto; border-radius: 15px;font-weight:bold!important;">
		<div class="ui-widget-header" style="height: 25px;border-radius: 15px 15px 0px 0px">
			<div class="titleDiv" style="border-right:1px solid #fff;float:left;width:115px"></div>
			<div class="titleDiv" style="border-right:1px solid #fff;float:left;text-align:center;width:200px"><%=GeneralLibrary.Language.GetString("DomainName")%></div>
			<div class="titleDiv" style="border-right:1px solid #fff;float:left;text-align:center;width:90px"><%=GeneralLibrary.Language.GetString("DomainExtention")%></div>
			<div class="titleDiv" style="width:70px;text-align:center;"><%=GeneralLibrary.Language.GetString("Continuation")%></div>
		</div>
		<div class="controlDiv" style="text-align:center;direction:ltr; width:480px;margin-top:10px">
			<span style="width:80px;margin-right:25px;">WWW.</span>
			<asp:TextBox ID="txtDomain" runat="server" CssClass="input" Style="width:200px;margin-right:10px;" isRequired="true" ValidationSet="SaveNicDomain" Width="280px" Height="20px"></asp:TextBox>
			<asp:DropDownList ID="drpExtention" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpExtention_SelectedIndexChanged"></asp:DropDownList>
			<asp:DropDownList ID="drpPeriod" runat="server"></asp:DropDownList>
			<div class="clear" style="margin:20px;"><span>مبلغ </span><asp:Label runat="server" Text="42452.0000" Width="100px"></asp:Label><span>ریال</span></div>
		</div>
		<br />
		<div class="buttonControlDiv" style="margin-top:60px;text-align:center"><asp:Button ID="btnDomainContinuation" runat="server" Text="Register" CssClass="blackButton" OnClick="btnDomainContinuation_Click" /></div>
	</div>
</fieldset>
