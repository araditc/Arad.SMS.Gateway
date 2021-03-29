<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.ChangePassword" %>
<script src="/script/encryption.js" type="text/javascript"></script>

<div class="row">
	<hr />
	<div class="col-xs-6 col-md-4">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Password")%></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control input-sm" isRequired="true" validationSet="ChangePassword" TextMode="Password"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NewPassword")%></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control input-sm" isRequired="true" validationSet="ChangePassword" TextMode="Password"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmNewPassword")%></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtConfrimNewPassword" runat="server" CssClass="form-control input-sm" isRequired="true" validationSet="ChangePassword" TextMode="Password"></asp:TextBox>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv">
			<asp:Button ID="btnChangePassword" CausesValidation="false" runat="server" Text="Register" OnClick="btnChangePassword_Click" CssClass="btn btn-success" />
		</div>
	</div>
</div>

<script type="text/javascript">
	function validateSubmit() {
		if (!validateRequiredFields("ChangePassword"))
			return false;

		if ($("#<%=txtNewPassword.ClientID%>")[0].value != $("#<%=txtConfrimNewPassword.ClientID%>")[0].value) {
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PasswordNotMatchWithConfirmPassword") %>', '', 'alert', 'danger');
			return false;
		}

		$("#<%=txtOldPassword.ClientID%>")[0].value = MD5($("#<%=txtOldPassword.ClientID%>")[0].value);
		$("#<%=txtNewPassword.ClientID%>")[0].value = MD5($("#<%=txtNewPassword.ClientID%>")[0].value);
		$("#<%=txtConfrimNewPassword.ClientID%>")[0].value = MD5($("#<%=txtConfrimNewPassword.ClientID%>")[0].value);
		return true;
	}
</script>
