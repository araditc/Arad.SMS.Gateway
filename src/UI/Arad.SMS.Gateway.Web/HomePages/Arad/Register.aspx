<%@ Page Title="" Language="C#" MasterPageFile="~/HomePages/Arad/Main.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Arad.SMS.Gateway.Web.HomePages.Arad.Register" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script src="/script/encryption.js" type="text/javascript"></script>
	<script type="text/javascript">
		var userNameIsValid = false;
		function checkUserName() {
			var userName = $("#<%=txtUsername.ClientID%>")[0].value;
			if (userName != '') {
				var isValid = getAjaxResponse("CheckUserName", "UserName=" + userName);
				if (isValid == true) {
					$("#<%=txtUsername.ClientID%>").css({ 'background-color': '#87b87f', 'color': 'white' });
					userNameIsValid = true;
				}
				else {
					$("#<%=txtUsername.ClientID%>").css({ 'background-color': '#d15b47', 'color': 'white' });
					userNameIsValid = false;
				}
			}
		}

		function validateSubmit() {
			$("#<%=txtPassword.ClientID%>")[0].value = MD5($("#<%=txtPassword.ClientID%>")[0].value);
			$("#<%=txtRepeatPassword.ClientID%>")[0].value = MD5($("#<%=txtRepeatPassword.ClientID%>")[0].value);
		}
	</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content-item grey login" style="margin-top: 50px;">
		<div class="container">
			<div class="row">
				<div class="col-sm-10 col-md-8 col-lg-6 col-sm-offset-1 col-md-offset-2 col-lg-offset-3">
					<div class="form-wrapper">
						<h4><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CreateAccount") %></h4>
						<div style="padding: 20px 30px 10px;">
                            <asp:Label ID="lblMessage" runat="server" Text="" Style="color: maroon; font-size: large;"></asp:Label>
							<fieldset>
								<div class="form-group">
                                    <label class="block clearfix"> </label>
                                    <asp:DropDownList ID="drpType" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label class="block clearfix"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserName") %></label>
                                    <asp:TextBox ID="txtUsername" runat="server" class="form-control input-sm" placeholder='<%# Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserName") %>' onblur="checkUserName()"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label class="block clearfix"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Password") %></label>
                                    <asp:TextBox ID="txtPassword" runat="server" class="form-control input-sm" TextMode="Password" placeholder='<%# Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Password") %>'></asp:TextBox>
                                </div>
                                <div class="form-group">
									<label class="block clearfix"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmPassword") %></label>
                                    <asp:TextBox ID="txtRepeatPassword" runat="server" class="form-control input-sm" TextMode="Password" placeholder='<%#Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmPassword") %>'></asp:TextBox>
								</div>
                                <div class="form-group">
									<label class="block clearfix"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Email") %></label>
                                    <asp:TextBox ID="txtEmail" type="email" runat="server" class="form-control input-sm" placeholder='<%#Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Email") %>'></asp:TextBox>
								</div>
                                <div class="form-group">
                                    <label class="block clearfix"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CellPhone") %></label>
                                    <asp:TextBox ID="txtMobile" runat="server" class="form-control input-sm"></asp:TextBox>
                                </div>
								<div class="">
                                    <BotDetect:WebFormsCaptcha ID="SampleCaptcha" runat="server" />
                                    <asp:TextBox ID="CaptchaCodeTextBox" runat="server" />
                                </div>
                                <div class="clearfix">
									<asp:Button ID="btnRegister" runat="server" Text="CreateAccount" class="btn btn-success" OnClick="btnRegister_Click" OnClientClick="return validateSubmit();" />
                                    <a href="/Login" class="btn btn-white btn-warning"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Login")%></a>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
			</div>
		</div>
	</div>
</asp:Content>



