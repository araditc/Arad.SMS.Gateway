<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveUserMessage.ascx.cs" Inherits="MessagingSystem.UI.Domains.SaveUserMessage" %>
<div style="width: 300px;float:right;">
	<div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("CompanyName")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtCompanyName" CssClass="input" runat="server" isRequired="true"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("job")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtJob" CssClass="input" runat="server"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("PhoneNumber")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtTelephone" CssClass="numberInput" runat="server" isRequired="true"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("CellPhone")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtCellPhone" CssClass="numberInput" runat="server" isRequired="true"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("Email")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtEmail" CssClass="emailInput" runat="server"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("Description")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtDescription" CssClass="Input" TextMode="MultiLine" Width="150px" runat="server"></asp:TextBox></div>
	</div>
	<div class="buttonControlDiv" style="width: 298px; float: right; margin-top: 30px">
		<asp:Button ID="btnSave" runat="server" Text="Register" CssClass="button" onclick="btnSave_Click" />
		<input id="btnCancel" type="button" value='<%=GeneralLibrary.Language.GetString("Cancel")%>' onclick="closeModal('false');" class="button" />
	</div>
</div>