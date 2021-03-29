<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminSetting.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Settings.AdminSetting" %>

<div class="col-xs-8 col-md-8">
	<div class="form-horizontal" role="form">
		<hr />
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("VasDefaultResponse") %></label>
			<div class="col-sm-4">
				<GeneralTools:SmsBodyBox runat="server" ID="txtVasRegisterSmsText" Width="220" Height="100"></GeneralTools:SmsBodyBox>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendSmsAlertMessage")%></label>
			<div class="col-sm-4">
				<GeneralTools:SmsBodyBox runat="server" ID="txtSendSmsAlertMessage" Width="220" Height="100"></GeneralTools:SmsBodyBox>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RegisterFishSmsText")%></label>
			<div class="col-sm-4">
				<GeneralTools:SmsBodyBox runat="server" ID="txtRegisterFishSmsText" Width="220" Height="100"></GeneralTools:SmsBodyBox>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("OnlinePaymentSmsText")%></label>
			<div class="col-sm-4">
				<GeneralTools:SmsBodyBox runat="server" ID="txtOnlinePaymentSmsText" Width="220" Height="100"></GeneralTools:SmsBodyBox>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("LoginSmsText")%></label>
			<div class="col-sm-4">
				<GeneralTools:SmsBodyBox runat="server" ID="txtLoginSmsText" Width="220" Height="100"></GeneralTools:SmsBodyBox>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("LowCreditSmsText")%></label>
			<div class="col-sm-4">
				<GeneralTools:SmsBodyBox runat="server" ID="txtLowCreditSmsText" Width="220" Height="100"></GeneralTools:SmsBodyBox>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RegisterUserSmsText")%></label>
			<div class="col-sm-4">
				<GeneralTools:SmsBodyBox runat="server" ID="txtRegisterUserSmsText" Width="220" Height="100"></GeneralTools:SmsBodyBox>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserAccountSmsText")%></label>
			<div class="col-sm-4">
				<GeneralTools:SmsBodyBox runat="server" ID="txtUserAccountSmsText" Width="220" Height="100"></GeneralTools:SmsBodyBox>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserExpireSmsText")%></label>
			<div class="col-sm-4">
				<GeneralTools:SmsBodyBox runat="server" ID="txtUserExpireSmsText" Width="220" Height="100"></GeneralTools:SmsBodyBox>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RetrievePasswordSmsText")%></label>
			<div class="col-sm-4">
				<GeneralTools:SmsBodyBox runat="server" ID="txtRetrievePasswordSmsText" Width="220" Height="100"></GeneralTools:SmsBodyBox>
			</div>
		</div>
	</div>
	<div class="col-md-8">
		<asp:Button ID="btnSave" runat="server" Text="Register" OnClick="btnSave_Click" CssClass="btn btn-success" />
	</div>
</div>
<div class="col-md-4">
	<div class="bs-example" data-example-id="striped-table">
		<table class="table table-striped">
			<caption><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("KeywordsList") %></caption>
			<thead>
				<tr>
					<th><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("KeywordsDescription") %></th>
					<th><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("KeywordsName") %></th>
				</tr>
			</thead>
			<tbody>
				<tr>
					<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FishAmount") %></td>
					<td>#billamount#</td>
				</tr>
				<tr>
					<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FishNumber") %></td>
					<td>#billnumber#</td>
				</tr>
				<tr>
					<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FirstName") %></td>
					<td>#firstname#</td>
				</tr>
				<tr>
					<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("LastName") %></td>
					<td>#lastname#</td>
				</tr>
				<tr>
					<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserName") %></td>
					<td>#username#</td>
				</tr>
				<tr>
					<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Password") %></td>
					<td>#password#</td>
				</tr>
				<tr>
					<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DomainName") %></td>
					<td>#domain#</td>
				</tr>
				<tr>
					<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ID") %></td>
					<td>#id#</td>
				</tr>
				<tr>
					<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DateTime") %></td>
					<td>#date#</td>
				</tr>
				<tr>
					<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Time") %></td>
					<td>#time#</td>
				</tr>
				<tr>
					<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MobileNo") %></td>
					<td>#usermobile#</td>
				</tr>
			</tbody>
		</table>
	</div>
</div>
