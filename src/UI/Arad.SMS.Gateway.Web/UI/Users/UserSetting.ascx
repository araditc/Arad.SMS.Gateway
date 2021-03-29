<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserSetting.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.UserSetting" %>

<asp:HiddenField ID="hdnApiPassword" runat="server" />

<div class="row">
	<hr />
	<div class="col-xs-10 col-md-10">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendSmsAfterLogin")%></label>
				<div class="col-sm-4">
					<asp:TextBox ID="txtReceivers" runat="server" CssClass="form-control input-sm" placeholder="Separator , ;"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendWarningSmsWhenCreditIsLowerThan")%></label>
				<div class="col-sm-4">
					<div class="input-group" style="z-index: 1">
						<asp:TextBox ID="txtCredit" class="form-control input-sm numberInput" validationSet="panelInfo" autoFormatDecimal="true" runat="server" Text="0"></asp:TextBox>
						<span class="input-group-addon"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsCount") %></span>
					</div>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DaysBeforeExpire")%></label>
				<div class="col-sm-4">
					<asp:DropDownList ID="drpExpire" class="form-control input-sm" runat="server">
						<asp:ListItem Value="0">0</asp:ListItem>
						<asp:ListItem Value="1">1</asp:ListItem>
						<asp:ListItem Value="2">2</asp:ListItem>
						<asp:ListItem Value="3">3</asp:ListItem>
						<asp:ListItem Value="4">4</asp:ListItem>
						<asp:ListItem Value="5">5</asp:ListItem>
					</asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DefaultNumber")%></label>
				<div class="col-sm-4">
					<asp:DropDownList ID="drpNumber" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("APIPassword") %></label>
				<div class="col-sm-4">
					<asp:TextBox ID="txtPassword" runat="server" CssClass="form-control input-sm" TextMode="Password"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmAPIPassword") %></label>
				<div class="col-sm-4">
					<asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control input-sm" TextMode="Password"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AuthorizedIPs") %></label>
				<div class="col-sm-4">
					<asp:TextBox ID="txtIP" runat="server" CssClass="form-control input-sm" placeholder="Separator , ;"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TrafficRelay") %></label>
				<div class="col-sm-4">
					<asp:DropDownList ID="drpSmsTrafficRelay" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeliveryRelay") %></label>
				<div class="col-sm-4">
					<asp:DropDownList ID="drpDeliveryTrafficRelay" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv col-md-8">
			<asp:Button ID="btnSave" runat="server" Text="Register" OnClick="btnSave_Click" CssClass="btn btn-success" />
		</div>
	</div>
</div>
