<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SavePrivateNumber.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PrivateNumbers.SavePrivateNumber" %>
<%@ Register Src="~/UI/Controls/BootstrapDatePicker.ascx" TagName="DatePicker" TagPrefix="SMS" %>

<script type="text/javascript">
	$(document).ready(function () {
		changeUseForm();
		$("#<%=drpUseForm.ClientID%>").bind('change', function () {
			changeUseForm();
		});
	});

	function changeUseForm() {
		var type = $("#<%=drpUseForm.ClientID%> option:selected")[0].value;
		$(".usetheform").hide();
		switch (type) {
			case "<%=(int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.OneNumber%>":
				$(".number").show();
				break;
			case "<%=(int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.Mask%>":
				$(".mask").show();
				break;
			case "<%=(int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.RangeNumber%>":
				$(".rangeNumber").show();
				break;
		}
	}

	function checkValidation() {
		if (!validateRequiredFields('general'))
			return false;

		var type = $("#<%=drpUseForm.ClientID%> option:selected")[0].value;
		switch (type) {
			case "<%=(int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.OneNumber%>":
				if (!validateRequiredFields('number'))
					return false;
			case "<%=(int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.Mask%>":
				if (!validateRequiredFields('mask'))
					return false;
			case "<%=(int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.RangeNumber%>":
				if (!validateRequiredFields('rangeNumber'))
					return false;
		}

		return true;
	}
</script>
<div class="row">
	<div class="col-md-5">
		<div class="panel panel-primary">
			<div class="panel-heading"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MainSetting") %></div>
			<div class="panel-body">
				<div class="form-horizontal" role="form">
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Type")%></label>
						<div class="col-sm-7">
							<asp:DropDownList ID="drpType" class="form-control input-sm" validationSet="general" isrequired="true" runat="server"></asp:DropDownList>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendPriority")%></label>
						<div class="col-sm-7">
							<asp:DropDownList ID="drpPriority" class="form-control input-sm" validationSet="general" isrequired="true" runat="server"></asp:DropDownList>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ExpireDate")%></label>
						<div class="col-sm-7">
							<SMS:DatePicker ID="dtpExpireDate" runat="server" />
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Price") %></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtPrice" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Active")%></label>
						<div class="col-sm-7">
							<asp:CheckBox ID="chbIsActive" Checked="true" runat="server" />
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("VASNumber") %></label>
						<div class="col-sm-7">
							<asp:CheckBox ID="chbIsRoot" runat="server" />
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendToBlackList")%></label>
						<div class="col-sm-7">
							<asp:CheckBox ID="chbSendToBlackList" runat="server" />
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ReturnBlackList")%></label>
						<div class="col-sm-7">
							<asp:CheckBox ID="chbReturnBlackList" Checked="true" runat="server" />
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeliveryBase")%></label>
						<div class="col-sm-7">
							<asp:CheckBox ID="chbDeliveryBase" runat="server" />
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CheckSmsFilter")%></label>
						<div class="col-sm-7">
							<asp:CheckBox ID="chbCheckFilter" runat="server" />
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Public")%></label>
						<div class="col-sm-7">
							<asp:CheckBox ID="chbIsPublic" runat="server" />
						</div>
					</div>
				</div>
				<div class="col-md-4"></div>
				<div class="col-md-8">
					<div class="form-inline" role="form" style="margin-right: 13px;">
						<div class="form-group">
							<div>
								<label class="block">
									<span class="lbl">SLA</span>
									<asp:CheckBox ID="chbIsSla" name="form-field-checkbox" CssClass="ace" runat="server" />
								</label>
							</div>
						</div>
						<div class="form-group">
							<div>
								<asp:DropDownList ID="drpTryCount" runat="server" class="form-control input-sm" Style="width: 150px;">
									<asp:ListItem>5</asp:ListItem>
									<asp:ListItem>10</asp:ListItem>
									<asp:ListItem>15</asp:ListItem>
								</asp:DropDownList>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv">
			<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Register" />
			<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
		</div>
	</div>
	<div class="col-md-5">
		<div class="panel panel-primary">
			<div class="panel-heading"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsSenderAgentSetting") %></div>
			<div class="panel-body">
				<div class="form-horizontal" role="form">
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsSenderAgent")%></label>
						<div class="col-sm-7">
							<asp:DropDownList ID="drpSmsSenderAgent" class="form-control input-sm" runat="server"></asp:DropDownList>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UseTheForm")%></label>
						<div class="col-sm-7">
							<asp:DropDownList ID="drpUseForm" class="form-control input-sm" runat="server"></asp:DropDownList>
						</div>
					</div>
					<div class="usetheform number">
						<div class="form-group">
							<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PrivateNumber")%></label>
							<div class="col-sm-7">
								<asp:TextBox ID="txtNumber" class="form-control input-sm numberInput" isrequired="true" validationSet="number" runat="server"></asp:TextBox>
							</div>
						</div>
					</div>
					<div class="usetheform mask">
						<div class="form-group">
							<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Mask")%></label>
							<div class="col-sm-7">
								<asp:TextBox ID="txtMask" class="form-control input-sm" isrequired="true" validationSet="mask" runat="server"></asp:TextBox>
							</div>
						</div>
					</div>
					<div class="usetheform rangeNumber">
						<div class="form-group">
							<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Range")%></label>
							<div class="col-sm-7">
								<asp:TextBox ID="txtRange" class="form-control input-sm" isrequired="true" validationSet="rangeNumber" runat="server" Style="direction: ltr;"></asp:TextBox>
							</div>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ServiceID")%></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtServiceID" class="form-control input-sm" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MTNServiceID")%></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtMTNServiceID" class="form-control input-sm" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AggServiceID")%></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtAggServiceID" class="form-control input-sm" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ServicePrice")%></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtServicePrice" class="form-control input-sm numberInput" autoFormatDecimal="true" runat="server"></asp:TextBox>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
