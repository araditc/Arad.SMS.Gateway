<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendSmsFromFormat.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsSends.SendSmsFromFormat" %>
<%@ Register Src="~/UI/Controls/BootstrapDateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="SMS" %>

<script type="text/javascript">
	var formatNumbersInfo = '';
	var formatGroup = '';
	var retVal = '';

	$(document).ready(function () {
		$('#fuelux-wizard').ace_wizard({}).on('change', function (e, info) {
			if (info.step == 1 && info.direction == 'next') {
				if (!validateRequiredFields('step1'))
					return false;

				formatGroup = $("#<%=drpSmsFormat.ClientID%> option:selected")[0].value;

				retVal = getAjaxResponse("GetSmsCountOfFormat", "FormatGuid=" + formatGroup + "&SenderNumber=" + $("#<%=drpSenderNumber.ClientID%> option:selected")[0].value);

				if (importData(retVal, "Result") == 'Error') {
					result('step1', 'Error', importData(retVal, "Message"));
					return false;
				}

				formatNumbersInfo = retVal;

				if (parseInt(importData(retVal, "RecipientCount")) == 0) {
					result('step1', 'Error', '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RecieverListIsEmpty")%>');
					return false;
				}

				setSendPriceTableInfo(formatNumbersInfo);
			}
		});

		$(".dateTime").datepicker({
			changeMonth: true,
			changeYear: true,
			minDate: 0,
			maxDate: "+60D",
		});
	});

	function setSendPriceTableInfo(formatInfo) {
		formatNumbersInfo = formatInfo;
		$("#recipientCount").text(importData(formatInfo, "RecipientCount"));
		$("#mciCount").text(parseInt(importData(formatInfo, "MCI")));
		$("#mtnCount").text(parseInt(importData(formatInfo, "MTN")));
		$("#otherCount").text(parseInt(importData(formatInfo, "Other")));
		$("#totalCount").text(parseFloat(importData(formatInfo, "Price")));
	}

	function result(step, type, message) {
		var wizard = $('#fuelux-wizard').data('wizard');
		switch (step) {
			case 'step1':
				wizard.currentStep = 1;
				wizard.setState();
				if (type == "OK") {
					$("#resultStep1").addClass("bg-success div-save-result");
					$("#resultStep1").html("<span class='fa fa-check-square fa-2x' style='color:#5cb85c'></span>" + message);
				}
				else {
					$("#resultStep1").addClass("bg-danger div-save-result");
					$("#resultStep1").html("<span class='fa fa-times-circle fa-2x' style='color:red'></span>" + message);
				}
				break;
			case 'step2':
				wizard.currentStep = 2;
				wizard.setState();
				if (type == "OK") {
					$("#resultStep2").addClass("bg-success div-save-result");
					$("#resultStep2").html("<span class='fa fa-check-square fa-2x' style='color:#5cb85c'></span>" + message);
				}
				else {
					$("#resultStep2").addClass("bg-danger div-save-result");
					$("#resultStep2").html("<span class='fa fa-times-circle fa-2x' style='color:red'></span>" + message);
				}
				break;
		}
	}
</script>
<div class="col-xs-12 col-md-12">
	<div class="widget-box">
		<div class="widget-body">
			<div class="widget-main">
				<div id="fuelux-wizard" data-target="#step-container">
					<ul class="wizard-steps">
						<li data-target="#step1" class="active">
							<span class="step">1</span>
							<span class="title"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelectSmsFormat") %></span>
						</li>
						<li data-target="#step2" class="">
							<span class="step">2</span>
							<span class="title"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Confirm") %></span>
						</li>
					</ul>
				</div>
				<hr>
				<div class="step-content pos-rel" id="step-container">
					<div class="step-pane active" id="step1" style="height: 300px;">
						<div class="container-fluid">
							<div class="row">
								<div class=" col-md-4">
									<div class="form-horizontal" role="form">
										<div class="form-group">
											<div class="input-group">
												<span class="input-group-addon" style="background-color: rgb(80, 168, 226); color: #fff;"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sender") %></span>
												<asp:DropDownList ID="drpSenderNumber" runat="server" validationSet="step1" IsRequired="true" CssClass="form-control input-sm"></asp:DropDownList>
											</div>
										</div>
										<div class="form-group">
											<div class="input-group">
												<span class="input-group-addon" style="background-color: rgb(80, 168, 226); color: #fff;"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Format") %></span>
												<asp:DropDownList ID="drpSmsFormat" runat="server" validationSet="step1" IsRequired="true" CssClass="form-control input-sm"></asp:DropDownList>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendTime") %></label>
											<div class="col-sm-12">
												<SMS:DateTimePicker ID="dtpSendDateTime" ValidationSet="step1" IsRequired="true" runat="server"></SMS:DateTimePicker>
											</div>
										</div>
										<div class="form-group">
											<div class="col-xs-8 col-sm-8">
												<asp:TextBox ID="txtTestReciever" isRequired="true" validationsSet="sendTestSms" class="form-control input-sm mobileNumberInput" runat="server"></asp:TextBox>
											</div>
											<asp:Button ID="btnSendTestSms" runat="server" class="btn btn-default btn-sm" Text="SendTest" OnClick="btnSendTestSms_Click" />
											<div id="resultStep1" class="div-save-result"></div>
										</div>
									</div>
								</div>
								<div class=" col-md-1"></div>
								<div class=" col-md-8">
                                    <% if(Session["Language"].ToString() == "en") { %>
                                    <div class="panel panel-warning" style="text-align: left;">
                                        <% }  else
                                       {%>
                                        <div class="panel panel-warning">
                                            <%  } %>
                                            <div class="panel-heading"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Guide") %></div>
										<div class="panel-body">
											<%--متن راهنما--%>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
					<div class="step-pane" id="step2" style="height: 300px;">
						<div class="container-fluid">
							<div id="confirmSendSms" class="row">
								<div class="col-md-4 col-xs-6"></div>
								<div class="col-md-4 col-xs-6">
									<table class="table ">
										<tr>
											<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RecipientCount") %>:</td>
											<td id="recipientCount"></td>
											<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Number") %></td>
											<td></td>
										</tr>
										<tr>
											<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PartCount") %>:</td>
											<td id="smsPartCount"></td>
											<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Parts") %></td>
											<td></td>
										</tr>
										<%--<tr class="success">
											<td></td>
											<td>همراه اول</td>
											<td id="mciCount"></td>
											<td>عدد پیامک</td>
										</tr>
										<tr class="success">
											<td></td>
											<td>ایرانسل</td>
											<td id="mtnCount"></td>
											<td>عدد پیامک</td>
										</tr>
										<tr class="success">
											<td></td>
											<td>سایر</td>
											<td id="otherCount"></td>
											<td>عدد پیامک</td>
										</tr>--%>
										<tr class="warning">
											<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendPrice") %>: </td>
											<td id="totalCount"></td>
											<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sms") %></td>
											<td></td>
										</tr>
										<tr>
											<td colspan="4">
												<asp:Button ID="btnSendSms" runat="server" Text="SendSms" CssClass="btn btn-primary btn-lg" Style="width: 100%" OnClick="btnSendSms_Click" />
											</td>
										</tr>
									</table>
									<div id="resultStep2" class="div-save-result"></div>
								</div>
								<div class="col-md-4 col-xs-6"></div>
							</div>
						</div>
					</div>
				</div>
				<hr>
				<div class="wizard-actions">
					<a href="#" class="btn btn-prev" disabled="disabled"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("BackStep") %>
						<i class="ace-icon fa fa-arrow-right icon-on-right"></i>
					</a>
					<a href="#" class="btn btn-success btn-next" data-last="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Finish") %>"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NextStep") %>
						<i class="ace-icon fa fa-arrow-left icon-on-left"></i>
					</a>
				</div>
			</div>
		</div>
	</div>
</div>
