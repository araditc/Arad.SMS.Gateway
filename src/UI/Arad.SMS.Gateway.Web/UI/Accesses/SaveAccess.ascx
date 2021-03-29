<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveAccess.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Accesses.SaveAccess" %>
<fieldset style="width: 350px">
	<legend><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AddAccess")%></legend>
	<div class="titleDiv"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Service") %></div>
	<div class="controlDiv"><asp:DropDownList ID="drpService" runat="server" CssClass="input" Width="180px"></asp:DropDownList></div>
	<div class="titleDiv"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Access") %></div>
	<div class="controlDiv"><asp:DropDownList ID="drpAccess" runat="server" CssClass="input" Width="180px"></asp:DropDownList></div>
	<div class="buttonControlDiv">
		<asp:Button ID="btnSave" CssClass="button" runat="server" 
			onclick="btnSave_Click"/>
		<input id="btnCancel" type="button" value='<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel")%>' onclick="closeModal('false');" class="button"/>
	</div>
</fieldset>