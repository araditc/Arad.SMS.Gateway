<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateDirectAccount.ascx.cs" Inherits="MessagingSystem.UI.Domains.CreateDirectAccount" %>

<fieldset>
	<legend><%=GeneralLibrary.Language.GetString("SaveDirectDomain")%></legend>
	<div style="width:500px;height:450px;margin:20px;margin-top:0px;border:1px solid #aaa;border-radius:10px;padding-top:20px;">
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("UserName")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtuserName" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveDirectDomain"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("Password")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input" isRequired="true" ValidationSet="SaveDirectDomain"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("FirstName")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtFirstName" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveDirectDomain"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("CompanyName")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtCompanyName" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveDirectDomain"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("City")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtCity" runat="server" CssClass="input"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("Province")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtProvince" runat="server" CssClass="input"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("Country")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtCountry" runat="server" CssClass="input" Text="IR" Enabled="false" style="text-align:left;"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("PostCode")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtPostalCode" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveDirectDomain"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("PhoneNumber")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="numberInput" isRequired="true" ValidationSet="SaveDirectDomain"></asp:TextBox></div>
		<div class="multiLineTitleDiv" style="height:0px;"><%=GeneralLibrary.Language.GetString("Address")%></div>
		<div class="multiLineControlDiv"><asp:TextBox ID="txtAddress" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveDirectDomain" TextMode="MultiLine" Width="200px"></asp:TextBox></div>
		<div class="buttonControlDiv"><asp:Button ID="btnSave" ForeColor="#ffffff" BackColor="#77717b" runat="server" Text="Register" CssClass="button" OnClick="btnSave_Click" /></div>
	</div>
</fieldset>
