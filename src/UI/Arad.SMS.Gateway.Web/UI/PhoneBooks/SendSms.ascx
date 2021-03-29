<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendSms.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PhoneBooks.SendSms" %>
<%@ Register Src="~/UI/Controls/BootstrapDateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="SMS" %>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<div class="row" style="padding: 30px;">
	<div class=" col-md-5 col-xs-5">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<div class="input-group">
					<span class="input-group-addon" style="background-color: rgb(80, 168, 226); color: #fff;"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendToGroups") %></span>
					<asp:TextBox ID="txtGroupName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<div class="input-group">
					<span class="input-group-addon" style="background-color: rgb(80, 168, 226); color: #fff;"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sender") %></span>
					<asp:DropDownList ID="drpSenderNumber" runat="server" IsRequired="true" CssClass="form-control input-sm"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<GeneralTools:SmsBodyControl runat="server" ID="txtSmsBody" IsRequired="true"></GeneralTools:SmsBodyControl>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendTime") %></label>
				<SMS:DateTimePicker ID="dtpSendDateTime" IsRequired="true" runat="server"></SMS:DateTimePicker>
			</div>
			<div class="buttonControlDiv">
				<asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Register" />
			</div>
		</div>
	</div>
</div>
