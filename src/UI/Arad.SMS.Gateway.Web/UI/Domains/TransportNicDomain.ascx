<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransportNicDomain.ascx.cs" Inherits="MessagingSystem.UI.Domains.TransportNicDomain" %>
<fieldset>
	<legend><%=GeneralLibrary.Language.GetString("TransportNicDomain")%></legend>
	<div class="ui-state-highlight style" style="border:1px solid #aaa;width:800px;height:200px;margin:10px auto;border-radius:15px">
		<div class="ui-widget-header" style="height:25px;border-radius:15px 15px 0px 0px">
		<div class="divHeaderHelp"><%=GeneralLibrary.Language.GetString("TransportDomainHeader")%></div> 
		<div class="clear"></div>
		<div class="ui-state-error style" style="height:20px;margin-top:10px" ><%=GeneralLibrary.Language.GetString("TransportDomainCommnet")%></div>
		<div class="titleDiv" style="margin-top:30px!important;color:Black;width:200px"><%=GeneralLibrary.Language.GetString("Domain")%></div>
		<div class="controlDiv" style="width:250px;float:right;margin:20px 20px 0px 0px"><asp:TextBox ID="txtDomain" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveNicDomain" Width="320px" Height="20px" ></asp:TextBox></div>
		<div class="clear"></div>
		<div class="titleDiv" style="margin-top:5px!important;color:Black;width:200px"><%=GeneralLibrary.Language.GetString("CurrentUserID")%></div>
		<div class="controlDiv" style="width:250px;float:right;margin:0px 20px 0px 0px"><asp:TextBox ID="txtCurrentUserID" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveNicDomain" Width="320px" Height="20px" ></asp:TextBox></div>
		<div class="clear"></div>
		<div class="buttonControlDiv" style="width:298px;float:right;margin:10px 250px 0px 0px">
			<asp:Button ID="btnGetTransportCode" runat="server" Text="Register" CssClass="button"/>
		</div>
		</div>
	</div>
	<div class="ui-state-highlight style" style="border:1px solid #aaa;width:800px;height:200px;margin:30px auto;border-radius:15px">
		<div class="ui-widget-header" style="height:25px;border-radius:15px 15px 0px 0px">
		<div class="divHeaderHelp"><%=GeneralLibrary.Language.GetString("TransportDomain")%></div> 
		<div class="clear"></div>
		<div class="titleDiv" style="margin-top:30px!important;color:Black;width:200px"><%=GeneralLibrary.Language.GetString("Domain")%></div>
		<div class="controlDiv" style="width:250px;float:right;margin:20px 20px 0px 0px"><asp:TextBox ID="txtTransferDomain" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveNicDomain" Width="320px" Height="20px" ></asp:TextBox></div>
		<div class="clear"></div>
		<div class="titleDiv" style="margin-top:5px!important;color:Black;width:200px"><%=GeneralLibrary.Language.GetString("NextUserID")%></div>
		<div class="controlDiv" style="width:250px;float:right;margin:0px 20px 0px 0px"><asp:TextBox ID="txtNextUserHandle" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveNicDomain" Width="320px" Height="20px" ></asp:TextBox></div>
		<div class="clear"></div>
		<div class="titleDiv" style="margin-top:5px!important;color:Black;width:200px"><%=GeneralLibrary.Language.GetString("TransportCode")%></div>
		<div class="controlDiv" style="width:250px;float:right;margin:0px 20px 0px 0px"><asp:TextBox ID="txtTransporCode" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveNicDomain" Width="320px" Height="20px" ></asp:TextBox></div>
		<div class="clear"></div>
		<div class="buttonControlDiv" style="width:298px;float:right;margin:10px 250px 0px 0px">
			<asp:Button ID="btnTransportDomain" runat="server" Text="TransportDomain" CssClass="button"/>
		</div>
		</div>
	</div>
</fieldset>
