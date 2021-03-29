<%@ Page Title="" Language="C#" MasterPageFile="~/HomePages/Arad/Main.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Arad.SMS.Gateway.Web.HomePages.Arad.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content-item grey login" style="margin-top: 50px;">
		<div class="container">
			<div class="row">
				<div class="col-sm-10 col-md-8 col-lg-6 col-sm-offset-1 col-md-offset-2 col-lg-offset-3">
					<div class="form-wrapper">
						<h4><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Forgotpassword") %></h4>
						<div style="padding: 20px 30px 10px;">
                            <asp:Label ID="lblMessage" runat="server" Text="" Style="color: maroon; font-size: large;"></asp:Label>
							<fieldset>
								<div class="form-group">
                                    <label class="block clearfix"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserName") %></label>
                                    <asp:TextBox ID="txtUsername" runat="server" class="form-control input-sm" placeholder=""></asp:TextBox>
                                </div>
                                <div class="">
                                    <BotDetect:WebFormsCaptcha ID="SampleCaptcha" runat="server" />
                                    <asp:TextBox ID="CaptchaCodeTextBox" runat="server" />
                                </div>
                                <div class="clearfix">
                                     <asp:Button ID="btnForgotPassword" runat="server" class="width-35 pull-right btn btn-sm btn-danger" Text="SendwithSMS" OnClick="btnForgotPassword_Click" />
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
			</div>
		</div>
	</div>
	<%--<div class="main-container" style="margin-top: 50px;">
		<div class="main-content">
			<div class="row">
				<div class="col-sm-10 col-sm-offset-1">
					<div class="login-container">
						<div class="space-6"></div>
						<div class="position-relative">
							<div id="forgot-box" class="forgot-box widget-box no-border visible">
								<div class="widget-body">
									<div class="widget-main">
										<h4 class="header red lighter bigger">
											<i class="ace-icon fa fa-key"></i>
											بازیابی کلمه عبور
										</h4>
										<div class="space-3"></div>
										<asp:Label ID="lblMessage" runat="server" Text="" Style="color: maroon; font-size: large;"></asp:Label>
										<div>
											نام کاربری خود را جهت بازیابی رمز عبور وارد کنید
										</div>
										<div>
											<fieldset>
												<label class="block clearfix">
													<span class="block input-icon input-icon-right">
														<asp:TextBox ID="txtUsername" runat="server" class="form-control input-sm" placeholder="نام کاربری"></asp:TextBox>
														<i class="ace-icon fa fa-user"></i>
													</span>
												</label>
												<div class="space-4"></div>
												<BotDetect:WebFormsCaptcha ID="SampleCaptcha" runat="server" />
												<asp:TextBox ID="CaptchaCodeTextBox" runat="server" />
												<div class="space-10"></div>
												<div class="clearfix">
													<asp:Button ID="btnForgotPassword" runat="server" class="width-35 pull-right btn btn-sm btn-danger" Text="ارسال از طریق پیامک" OnClick="btnForgotPassword_Click" />
												</div>
											</fieldset>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>--%>
</asp:Content>
