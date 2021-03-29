<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveSingleNumber.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PhoneBooks.SaveSingleNumber" %>
<%@ Register Src="~/UI/Controls/BootstrapDatePicker.ascx" TagName="DatePicker" TagPrefix="SMS" %>

<div class="row">
	<hr />
	<div id="divResult" class="div-save-result"></div>
	<div class="col-md-4 col-xs-6">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FirstName")%></label>
				<div class="col-sm-5">
					<asp:TextBox ID="txtFirstName" class="form-control input-sm" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("LastName")%></label>
				<div class="col-sm-5">
					<asp:TextBox ID="txtLastName" class="form-control input-sm" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NationalCode")%></label>
				<div class="col-sm-5">
					<asp:TextBox ID="txtNationalCode" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("BirthDate")%></label>
				<div class="col-sm-5">
					<SMS:DatePicker ID="dtpBirthDate" runat="server" />
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sex")%></label>
				<div class="col-sm-5">
					<asp:DropDownList ID="drpSex" class="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MobileNo")%></label>
				<div class="col-sm-5">
					<asp:TextBox ID="txtCellPhone" class="form-control input-sm mobileNumberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Email")%></label>
				<div class="col-sm-5">
					<asp:TextBox ID="txtEmail" class="form-control input-sm emailInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Job")%></label>
				<div class="col-sm-5">
					<asp:TextBox ID="txtJob" class="form-control input-sm" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Telephone")%></label>
				<div class="col-sm-5">
					<asp:TextBox ID="txtTelephone" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FaxNumber")%></label>
				<div class="col-sm-5">
					<asp:TextBox ID="txtFaxNumber" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Address")%></label>
				<div class="col-sm-5">
					<asp:TextBox ID="txtAddress" class="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConditionsRegisterNumber")%></label>
				<div class="col-sm-7">
					<asp:DropDownList ID="drpCheckNumberScope" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="buttonControlDiv">
				<input type="button" onclick="saveNumber()" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Register") %>" class="btn btn-primary" />
				<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
			</div>
		</div>
	</div>
	<div class="col-md-4 col-xs-6">
		<div id="customField" class="form-horizontal" role="form">
		</div>
	</div>
</div>

<script type="text/javascript">
	function customField(fields) {
		var customFieldsHtml = '';
		var type;
		for (counterField = 0; counterField < fields.length; counterField++) {
			type = importData(fields, 'Field' + counterField + "_type");
			if (type != '') {
				switch (type) {
					case '<%=((int)Arad.SMS.Gateway.Business.UserFieldTypes.Strings) %>':
						customFieldsHtml += "<div class='form-group'><label class='col-sm-4 control-label'>" + importData(fields, 'Field' + counterField + "_title") + "</label><div class='col-sm-7'><input type='text' id='Field" + counterField + "' fieldType='" + type + "' class='form-control input-sm' value='" + importData(fields, 'Field' + counterField + "_value") + "'/></div></div>";
						break;
					case '<%=(int)Arad.SMS.Gateway.Business.UserFieldTypes.Number %>':
						customFieldsHtml += "<div class='form-group'><label class='col-sm-4 control-label'>" + importData(fields, 'Field' + counterField + "_title") + "</label><div class='col-sm-7'><input type='text' id='Field" + counterField + "' fieldType='" + type + "' class='form-control input-sm numberInput' value='" + importData(fields, 'Field' + counterField + "_value") + "'/></div></div>";
						break;
					case '<%=(int)Arad.SMS.Gateway.Business.UserFieldTypes.DateTime %>':
						customFieldsHtml += "<div class='form-group'><label class='col-sm-4 control-label'>" + importData(fields, 'Field' + counterField + "_title") + "</label><div class='col-sm-7'><input type='text' id='Field" + counterField + "' fieldType='" + type + "' class='form-control input-sm date' value='" + importData(fields, 'Field' + counterField + "_value") + "'/></div></div>";
						break;
				}
			}
		}

		$("#customField").html(customFieldsHtml);

		$(".date").datepicker({
			changeMonth: true,
			changeYear: true,
		});
	}

	function saveResult(resultType, message) {
		$("#divResult").removeClass();
		switch (resultType) {
			case 'Error':
				$("#divResult").addClass("bg-danger div-save-result");
				$("#divResult").html("<span class='fa fa-times fa-2x red'></span>" + message);
				break;
			case 'OK':
				$("#divResult").addClass(" bg-success div-save-result");
				$("#divResult").html("<span class='fa fa-check fa-2x green'></span>" + message);
				break;
		}
	}

	function getUserFieldValue() {
		var fieldList = "";

		$("#customField input[type=text]").each(function () {
			fieldList += $(this).attr('id') + "_Value" + "{(" + $(this)[0].value + ")}" + $(this).attr('id') + "_Type" + "{(" + $(this).attr('fieldType') + ")}";
		});
		return fieldList;
	}

	function saveNumber() {
		try {
			var stringSave = "FirstName{(" + $("#<%=txtFirstName.ClientID %>")[0].value + ")}";
			stringSave += "LastName{(" + $("#<%=txtLastName.ClientID %>")[0].value + ")}";
			stringSave += "NationalCode{(" + $("#<%=txtNationalCode.ClientID %>")[0].value + ")}";
			stringSave += "BirthDate{(" + $("#<%=dtpBirthDate.ClientID%>_txtDate")[0].value + ")}";
			stringSave += "Sex{(" + $("#<%=drpSex.ClientID %>")[0].value + ")}";
			stringSave += "CellPhone{(" + $("#<%=txtCellPhone.ClientID %>")[0].value + ")}";
			stringSave += "Email{(" + $("#<%=txtEmail.ClientID %>")[0].value + ")}";
			stringSave += "Job{(" + $("#<%=txtJob.ClientID %>")[0].value + ")}";
			stringSave += "Telephone{(" + $("#<%=txtTelephone.ClientID %>")[0].value + ")}";
			stringSave += "FaxNumber{(" + $("#<%=txtFaxNumber.ClientID %>")[0].value + ")}";
			stringSave += "Address{(" + $("#<%=txtAddress.ClientID %>")[0].value + ")}";
			stringSave += "Scope{(" + $("#<%=drpCheckNumberScope.ClientID%>")[0].value + ")}";
			stringSave += "ActionType{(" + '<%=ActionType%>' + ")}";
			stringSave += "NumberGuid{(" + '<%=PhoneNumberGuid%>' + ")}";
			stringSave += "PhoneBookGuid{(" + '<%=PhoneBookGuid%>' + ")}";
			stringSave += getUserFieldValue();

			var retVal = getAjaxResponse('InsertNumber', "Data=" + stringSave);
			saveResult(importData(retVal, "Result"), importData(retVal, "Message"));
		}
		catch (ex) {
			saveResult('error', ex.message);
		}
	}
</script>
