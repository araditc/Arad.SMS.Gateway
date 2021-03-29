<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvanceEdit.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.AdvanceEdit" %>
<%@ Register Src="~/UI/Controls/BootstrapDatePicker.ascx" TagName="DatePicker" TagPrefix="SMS" %>
<script src="/script/encryption.js" type="text/javascript"></script>
<asp:HiddenField ID="hdnPassword" runat="server" />

<script type="text/javascript">
	function result(type, message) {
		switch (type) {
			case 'error':
				$("#saveResult").addClass("bg-danger div-save-result");
				$("#saveResult").html("<span class='fa fa-times-circle fa-2x' style='color:red'></span>" + message);
				break;
			case 'ok':
				$("#saveResult").addClass(" bg-success div-save-result");
				$("#saveResult").html("<span class='fa fa-check-square fa-2x' style='color:#5cb85c'></span>" + message);
				break;
		}
	}

	function validateSubmit() {
		if (!validateRequiredFields('AdvanceEdit'))
			return false;

		if ($("#<%=txtPassword.ClientID%>")[0].value != "") {
			$("#<%=txtPassword.ClientID%>")[0].value = MD5($("#<%=txtPassword.ClientID%>")[0].value);
		}
		return true;
	}
</script>

<div class="row">
	<hr />
	<div class="col-xs-6 col-md-4">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Type")%></label>
				<div class="col-sm-7">
					<asp:DropDownList ID="drpType" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserName")%></label>
				<div class="col-sm-7">
					<asp:TextBox CssClass="form-control input-sm" ID="txtUserName" runat="server" isRequired="true" validationSet="AdvanceEdit"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Password")%></label>
				<div class="col-sm-7">
					<asp:TextBox CssClass="form-control input-sm" ID="txtPassword" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ExpireDate")%></label>
				<div class="col-sm-7">
					<SMS:DatePicker ID="dtpExpireDate" runat="server" IsRequired="true" ValidationSet="AdvanceEdit"></SMS:DatePicker>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MaximumAdmin")%></label>
				<div class="col-sm-7">
					<asp:TextBox CssClass="form-control input-sm numberInput" ID="txtMaximumAdmin" runat="server" isRequired="true" validationSet="AdvanceEdit"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MaximumUser")%></label>
				<div class="col-sm-7">
					<asp:TextBox CssClass="form-control input-sm numberInput" ID="txtMaximumUser" runat="server" isRequired="true" validationSet="AdvanceEdit"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PanelPrice")%></label>
				<div class="col-sm-7">
					<div class="input-group" style="z-index: 1">
						<asp:TextBox ID="txtPanelPrice" class="form-control input-sm numberInput" autoFormatDecimal="true" runat="server"></asp:TextBox>
						<span class="input-group-addon"><%--ریال--%></span>
					</div>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv">
			<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Register" />
			<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
		</div>
		<div class="clear"></div>
		<div id="saveResult" class="div-save-result"></div>
	</div>
	<div class="col-xs-6 col-md-4">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MaximumPhoneNumber")%></label>
				<div class="col-sm-7">
					<asp:TextBox CssClass="form-control input-sm" ID="txtMaximumPhoenNumber" Text="-1" runat="server" isRequired="true" validationSet="AdvanceEdit"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MaximumEmailAddress")%></label>
				<div class="col-sm-7">
					<asp:TextBox CssClass="form-control input-sm" ID="txtMaximumEmailAddress" Text="-1" runat="server" isRequired="true" validationSet="AdvanceEdit"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Domain")%></label>
				<div class="col-sm-7">
					<asp:DropDownList ID="drpDomain" CssClass="form-control input-sm" runat="server" isRequired="true" validationSet="AdvanceEdit"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Role")%></label>
				<div class="col-sm-7">
					<asp:DropDownList ID="drpRole" CssClass="form-control input-sm" runat="server" isRequired="true" validationSet="AdvanceEdit"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupPrice")%></label>
				<div class="col-sm-7">
					<asp:DropDownList ID="drpGroupPrice" class="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FixTariff")%></label>
				<div class="col-sm-7">
					<asp:CheckBox ID="chbIsFixGroupPrice" runat="server" />
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Active")%></label>
				<div class="col-sm-7">
					<asp:CheckBox ID="chbIsActive" runat="server" />
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("IsAuthenticated")%></label>
				<div class="col-sm-7">
					<asp:CheckBox ID="chbIsAuthenticated" runat="server" />
				</div>
			</div>
		</div>
	</div>
</div>
