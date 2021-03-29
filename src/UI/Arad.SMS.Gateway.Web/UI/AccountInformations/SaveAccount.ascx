<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveAccount.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.AccountInformations.SaveAccount" %>

<div class="row">
	<hr />
	<div class="col-md-4">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("OwnerAccountnumber")%></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtOwner" class="form-control input-sm" isrequired="true" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Bank")%></label>
				<div class="col-sm-7">
					<asp:DropDownList ID="drpBank" class="form-control input-sm" isrequired="true" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Branch")%></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtBranch" class="form-control input-sm" isrequired="true" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AccountNo")%></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtAccountNo" class="form-control input-sm numberInput" isrequired="true" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CardNo")%></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtCardNo" class="form-control input-sm numberInput" isrequired="true" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Active")%></label>
				<div class="col-sm-7">
					<asp:CheckBox ID="chbIsActive" runat="server" />
				</div>
			</div>
			<div class="alert alert-warning">
				<div class="form-group">
					<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("OnlineGatewayIsActive")%></label>
					<div class="col-sm-7">
						<asp:CheckBox ID="chbOnlineGatewayIsActive" runat="server" />
					</div>
				</div>
				<div>
					<div id="mellat" class="gatewayInfo" style="display: none;">
						<div class="form-group">
							<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TerminalID")%></label>
							<div class="col-sm-7">
								<asp:TextBox ID="txtTerminalID" class="form-control input-sm" isrequired="true" runat="server" validationSet="MellatGateway"></asp:TextBox>
							</div>
						</div>
						<div class="form-group">
							<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserName")%></label>
							<div class="col-sm-7">
								<asp:TextBox ID="txtUserName" class="form-control input-sm" isrequired="true" runat="server" validationSet="MellatGateway"></asp:TextBox>
							</div>
						</div>
						<div class="form-group">
							<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Password")%></label>
							<div class="col-sm-7">
								<asp:TextBox ID="txtPassword" class="form-control input-sm" isrequired="true" runat="server" validationSet="MellatGateway"></asp:TextBox>
							</div>
						</div>
					</div>
					<div id="parsian" class="gatewayInfo" style="display: none;">
						<div class="form-group">
							<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PinCode")%></label>
							<div class="col-sm-7">
								<asp:TextBox ID="txtPinCode" class="form-control input-sm" isrequired="true" runat="server" validationSet="parsianGateway"></asp:TextBox>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="buttonControlDiv">
				<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" />
				<a href="/PageLoader.aspx?c=<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_AccountInformations_AccountInformation,Session)%>" class="btn btn-default"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel")%></a>
			</div>
		</div>
	</div>
</div>

<script type="text/javascript">
	function showOnlineGatewayInfo() {
		$(".gatewayInfo").css({ 'display': 'none' });
		$("#<%=chbOnlineGatewayIsActive.ClientID %>").attr("disabled", true);
		$("#<%=chbOnlineGatewayIsActive.ClientID %>").attr("checked", false);

		switch (parseInt($("#<%=drpBank.ClientID %>")[0].value)) {
			case parseInt(<%=(int)Arad.SMS.Gateway.Business.Banks.Mellat %>):
				$("#mellat").show();
				$("#<%=chbOnlineGatewayIsActive.ClientID %>").removeAttr("disabled");
				$("#<%=chbOnlineGatewayIsActive.ClientID %>").attr("checked", true);
				break;
			case parseInt(<%=(int)Arad.SMS.Gateway.Business.Banks.Parsian %>):
				$("#parsian").show();
				$("#<%=chbOnlineGatewayIsActive.ClientID %>").removeAttr("disabled");
				$("#<%=chbOnlineGatewayIsActive.ClientID %>").attr("checked", true);
				break;
		}
	}

	function saveAccount() {
		if (!validateRequiredFields())
			return false;

		if ($("#<%=chbOnlineGatewayIsActive.ClientID%>").attr('checked') == 'true' || $("#<%=chbOnlineGatewayIsActive.ClientID%>").attr('checked') == 'checked') {
			switch (parseInt($("#<%=drpBank.ClientID %>")[0].value)) {
				case parseInt(<%=(int)Arad.SMS.Gateway.Business.Banks.Mellat %>):
					if (!validateRequiredFields('MellatGateway'))
						return false;
					break;
				case parseInt(<%=(int)Arad.SMS.Gateway.Business.Banks.Parsian %>):
					if (!validateRequiredFields('parsianGateway'))
						return false;
					break;
			}
		}
		else
			return true;
	}
</script>
