<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveRole.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Roles.SaveRole" %>

<script type="text/javascript">
	function result(type, message) {
		switch (type) {
			case 'ok':
				$("#result").addClass("bg-success div-save-result");
				$("#result").html("<span class='fa fa-check-square fa-2x' style='color:#5cb85c'></span>" + message);
				break;
			case 'error':
				$("#result").addClass("bg-danger div-save-result");
				$("#result").html("<span class='fa fa-times-circle fa-2x' style='color:red'></span>" + message);
				break;
		}
	}

	$(document).ready(function () {
		$("input[type=checkbox]").addClass('ace');
	});
</script>

<div class="row">
	<hr />
	<div class="col-xs-6 col-md-4">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Title")%></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtTitle" CssClass="form-control input-sm" runat="server" isRequired="true"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsPackagePrice") %></label>
				<div class="col-sm-7">
					<div class="input-group">
						<asp:TextBox ID="txtPrice" class="form-control input-sm numberInput" autoFormatDecimal="true" runat="server"></asp:TextBox>
						<span class="input-group-addon"><%--ریال--%></span>
					</div>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsPackageDetails") %></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtDescription" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Order") %></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtPriority" CssClass="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DefaultRole")%></label>
				<div class="col-sm-7">
					<label class="middle">
						<asp:CheckBox ID="chbDefaultRole" runat="server" />
						<span class="lbl"></span>
					</label>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsSalesPackage") %></label>
				<div class="col-sm-7">
					<label class="middle">
						<asp:CheckBox ID="ChbIsSalePackage" runat="server" />
						<span class="lbl"></span>
					</label>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv">
			<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Register" OnClick="btnSave_Click" />
			<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
		</div>
		<div class="clear"></div>
		<div id="result" class="div-save-result"></div>
	</div>
</div>
