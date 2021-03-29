<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Login.Login" %>

<asp:HiddenField ID="hdnAuthenticationString" runat="server" />
<asp:HiddenField ID="hdnActiveJavaScript" runat="server" />

<div id="divLogin" class="row">
	<div class="col-md-3 col-xs-3">
		<hr />
		<div class="form-horizontal" role="form">
			<asp:Label ID="lblError" runat="server" class="errorMessage" Text=""></asp:Label>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserName")%></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtUserName" class="form-control input-sm" validationSet="Login" isrequired="true" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Password")%></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtPassword" class="form-control input-sm" validationSet="Login" isrequired="true" runat="server" TextMode="Password"></asp:TextBox>
				</div>
			</div>
			<div class="form-group" style="display:none">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RemaindMe")%></label>
				<div class="col-sm-7">
					<asp:CheckBox ID="chkRemaindMe" runat="server" />
				</div>
			</div>
			<div class="buttonControlDiv">
				<asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" CausesValidation="False" OnClick="btnLogin_Click" />
			</div>
		</div>
	</div>
</div>

<script src="/script/encryption.js" type="text/javascript"></script>

<script type="text/javascript">
	var challengeString = "<%=ChallengeString%>";
	var loginTriesCount = "<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt(LoginTriesCount,Session) %>";

	function validateSubmit() {
		if (!validateRequiredFields("Login") || validateRequiredFields("Login") == 'undefined')
			return false;

		var retVal = getAjaxResponse('LoginUser', "RemaindMe=" + $("#<%=chkRemaindMe.ClientID%>")[0].checked +
																								"&LoginTriesCount" + loginTriesCount + "&UserName=" + $("#<%=txtUserName.ClientID%>")[0].value +
																								"&Password=" + MD5(challengeString + MD5($("#<%=txtPassword.ClientID%>")[0].value)));

		$("#<%=hdnActiveJavaScript.ClientID%>")[0].value = "1";
		$("#<%=txtPassword.ClientID%>")[0].value = "";

		if (importData(retVal, "Result") == "OK") {
			redirectToUrl(importData(retVal, "Url"), true);
		}
		else {
			challengeString = importData(retVal, "ChallengeString");
			loginTriesCount = importData(retVal, "LoginTriesCount");
			$("#<%=lblError.ClientID%>").text(importData(retVal, "ErrorMessage"));
		}

		return false;
	}
</script>
