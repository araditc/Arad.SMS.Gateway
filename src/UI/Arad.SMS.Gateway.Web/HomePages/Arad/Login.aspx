<%@ Page Title="" Language="C#" MasterPageFile="~/HomePages/Arad/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Arad.SMS.Gateway.Web.HomePages.Arad.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script type="text/javascript">
		function validateSubmit() {
            try {
                var challengeString = "<%=ChallengeString%>";
				$("#<%=txtUserPassword.ClientID%>")[0].value = MD5(challengeString + MD5($("#<%=txtPassword.ClientID%>")[0].value));
				$("#<%=txtPassword.ClientID%>")[0].value = "";
				return true;
			}
            catch (err) {
                console.log(err);
				return false;
			}
		}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content-item grey login" style="margin-top: 50px;">
		<div class="container">
			<div class="row">
				<div class="col-sm-10 col-md-8 col-lg-6 col-sm-offset-1 col-md-offset-2 col-lg-offset-3">
					<div class="form-wrapper">
						<h4><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SingInToSystem") %></h4>
						<div style="padding: 20px 30px 10px;">
                            <asp:Label ID="lblMessage" runat="server" Text="" Style="color: maroon; font-size: large;"></asp:Label>
							<fieldset>
								<div class="form-group">
                                    <label class="block clearfix"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserName") %></label>
                                    <asp:TextBox ID="txtUsername" runat="server" class="form-control input-sm" placeholder="UserName"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label class="block clearfix"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Password") %></label>
                                    <asp:TextBox ID="txtUserPassword" runat="server" Style="display: none"></asp:TextBox>
                                    <asp:TextBox ID="txtPassword" runat="server" class="form-control input-sm" TextMode="Password" placeholder="Password"></asp:TextBox>
                                </div>
								<div class="">
                                    <BotDetect:WebFormsCaptcha ID="SampleCaptcha" runat="server" />
                                    <asp:TextBox ID="CaptchaCodeTextBox" runat="server" />
                                </div>
                                <div class="clearfix">
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-primary" OnClick="btnLogin_Click"  />
                                    <a href="/Register" class="btn btn-white btn-warning"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CreateAccount")%></a>
                                    <a class="pull-right" href="/Forgotpassword"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Forgotpassword") %></a>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
			</div>
		</div>
	</div>
</asp:Content>
