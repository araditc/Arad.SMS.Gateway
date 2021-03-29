<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveUserPrivateNumber.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PrivateNumbers.SaveUserPrivateNumber" %>
<%@ Register Src="~/UI/Controls/BootstrapDatePicker.ascx" TagName="DatePicker" TagPrefix="SMS" %>

<script type="text/javascript">
	$(document).ready(function () {
		changeNumber();
		$("#<%=drpNumber.ClientID%>").bind('change', function () {
			changeNumber();
		});
	});

	function changeNumber() {
		var number = $("#<%=drpNumber.ClientID%> option:selected")[0].value;
		var type = number.split(';')[0];
		$(".usetheform").hide();
		switch (type) {
			case "<%=(int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.RangeNumber%>":
				$(".rangeNumber").show();
				break;
		}
	}

	function checkSaveNumberValidation() {
		if (!validateRequiredFields())
			return false;

		var number = $("#<%=drpNumber.ClientID%> option:selected")[0].value;
		var type = number.split(';')[0];
		if (type == '<%=(int)Arad.SMS.Gateway.Business.PrivateNumberUseForm.RangeNumber%>') {
			if (!validateRequiredFields('rangeNumber'))
				return false;
		}
	}

	function saveFailed(error) {
		$("#divError").removeClass();
		$("#divError").addClass("bg-danger");
		$("#divError").html("<span class='fa fa-times-circle fa-2x' style='color:red'></span>" + error);
	}
</script>

<div class="row">
	<hr />
	<div class="col-md-4">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("LineNumber") %></label>
				<div class="col-sm-7">
					<asp:DropDownList ID="drpNumber" isRequired="true" class="form-control input-sm" runat="server" Style="direction: ltr;"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Keyword") %></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtKeyword" class="form-control input-sm" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Price") %></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtPrice" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ExpireDate")%></label>
				<div class="col-sm-7">
					<SMS:DatePicker ID="dtpExpireDate" runat="server" />
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
		</div>
		<div class="buttonControlDiv">
			<asp:Button ID="btnSave" runat="server" Text="Register" CssClass="btn btn-success" OnClick="btnSave_Click" OnClientClick="return checkSaveNumberValidation();" />
			<a class="btn btn-default" href="/PageLoader.aspx?c=<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_AssignPrivateNumberToUsers,Session) %>&UserGuid=<%=Arad.SMS.Gateway.GeneralLibrary.Helper.RequestGuid(this,"UserGuid") %>"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %></a>
		</div>
		<div class="clear">
			<div id="divError" style="padding: 10px; margin-top: 5px;"></div>
		</div>
	</div>
</div>
