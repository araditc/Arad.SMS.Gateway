<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterLight.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.RegisterLight" %>
<style type="text/css">
	.emailInput{ border: border: 1px solid silver!important; }
	.emailInput:focus{ border: border: 1px solid silver!important; }
	.emailInput:hover{ border: border: 1px solid silver!important; }
</style>
<script src="../../JScripts/encryption.js" type="text/javascript"></script>

<div class="div-title"><label for="<%=txtRegisterUserName.ClientID %>"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserName")%></label></div>
<div class="div-control">
	<asp:TextBox ID="txtRegisterUserName" runat="server" Width="110px" validationSet="Register" isRequired="true"></asp:TextBox>
</div>
<div class="clear"></div>
<br />
<div class="div-title"><label for="<%=txtRegisterPassword.ClientID %>"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Password")%></label></div>
<div class="div-control"><asp:TextBox ID="txtRegisterPassword" runat="server" TextMode="Password" Width="110px" validationSet="RegisterUser" isRequired="true"></asp:TextBox></div>
<div class="clear"></div>
<br />
<div class="div-title"><label for="<%=txtConfirmRegisterPassword.ClientID %>"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmPassword")%></label></div>
<div class="div-control"><asp:TextBox ID="txtConfirmRegisterPassword" runat="server" Width="110px" TextMode="Password" validationSet="RegisterUser" isRequired="true"></asp:TextBox></div>
<div class="clear"></div>
<br />
<div class="div-title"><label for="<%=txtEmail.ClientID %>"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Email")%></label></div>
<div class="div-control"><asp:TextBox ID="txtEmail" runat="server" Width="110px" CssClass="emailInput" validationSet="RegisterUser" isRequired="true"></asp:TextBox></div>
<div class="clear"></div>
<br />
<div class="div-title"><label for="<%=txtMobileNumber.ClientID %>"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CellPhone")%></label></div>
<div class="div-control"><asp:TextBox ID="txtMobileNumber" runat="server" Width="110px" CssClass="mobileNumberInput" validationSet="RegisterUser" isRequired="true"></asp:TextBox></div>
<div class="clear"></div>
<br />
<input id="btnRegister" type="button" value='<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Register")%>' onclick="validateRegister();" class="btnRegister"/>

<script type="text/javascript">
	function validateRegister() {
		if (!validateRequiredFields("RegisterUser"))
			return false;

		if ($("#<%=txtRegisterPassword.ClientID%>")[0].value != $("#<%=txtConfirmRegisterPassword.ClientID%>")[0].value) {
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PasswordNotMatchWithConfirmPassword") %>', '', 'alert', 'danger');
			return false;
		}

		$("#<%=txtRegisterPassword.ClientID%>")[0].value = MD5($("#<%=txtRegisterPassword.ClientID%>")[0].value);
		$("#<%=txtConfirmRegisterPassword.ClientID%>")[0].value = MD5($("#<%=txtConfirmRegisterPassword.ClientID%>")[0].value);
		
		var retVal = getAjaxResponse('RegisterLightUser',
																								"&Email=" + $("#<%=txtEmail.ClientID%>")[0].value +
																								"&UserName=" + $("#<%=txtRegisterUserName.ClientID%>")[0].value +
																								"&Password=" + $("#<%=txtRegisterPassword.ClientID%>")[0].value +
																								"&ConfirmPassword=" + $("#<%=txtConfirmRegisterPassword.ClientID%>")[0].value +
																								"&MobileNo=" + $("#<%=txtMobileNumber.ClientID%>")[0].value);
		
		$("#<%=txtRegisterPassword.ClientID%>")[0].value = "";

		if (importData(retVal, "Result") == "OK") {
			messageBox(importData(retVal, "Message"), '', 'alert', 'success');
			if ('<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Request(this,"CallBack")%>' != '') {
				eval('<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Request(this,"CallBack")%>()');
			}
			else {
				messageBox(importData(retVal, "Message"), '', 'alert', 'success');
			}
		}
		else {
			messageBox(importData(retVal, "ErrorMessage"), '', 'alert', 'danger');
		}

	}

	function checkUserName() {
		var userName = $("#<%=txtRegisterUserName.ClientID%>")[0].value;
		var isValid = getAjaxResponse("CheckUserName", "UserName=" + userName);
		if (isValid == true)
			$('#<%=txtRegisterUserName.ClientID%>').css('background-color', '#0f0');
		else
			$('#<%=txtRegisterUserName.ClientID%>').css('background-color', '#f00');
	}
	$(document).ready(function () {
		$('#<%=txtRegisterUserName.ClientID%>').focusout(function () {
			if ($(this).val() == null || $(this).val() == '')
				return;
			checkUserName();
		});
	});
	</script>