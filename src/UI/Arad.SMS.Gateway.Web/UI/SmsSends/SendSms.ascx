<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendSms.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsSends.SendSms" %>
<%@ Register Src="~/UI/Controls/BootstrapDateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="SMS" %>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<script src="/script/jquery.ajaxupload.js"></script>
<asp:HiddenField ID="hdnFilePath" runat="server" />
<asp:HiddenField ID="hdnFileSoundPath" runat="server" />
<asp:HiddenField ID="hdnFileSoundLength" runat="server" Value="0" />
<asp:HiddenField ID="hdnFileSoundID" runat="server" />
<asp:HiddenField ID="hdnGroupGuid" runat="server" />

<style type="text/css">
	.panel-body {
		padding: 10px;
	}

	hr {
		margin: 5px 0 5px 0;
	}
	.rtl #tree {
        text-align: right;
    }

    #tree {
        text-align: left;
    }

</style>

<script type="text/javascript">
    


	var filePath = '';
	var fileIsValid = false;
	var retVal = '';

	var phoneBookGroup = '';
	var phoneBookGroupType;
	var recievers = '';

	$(document).ready(function () {
		changePeriodType();

		$.ajaxUploadSettings.name = 'upload';
		$('#FileUpload').ajaxUploadPrompt({
			url: '/handler/UploadAndGetFileInfo.ashx',
			beforeSend: function () {
				$(".widget-box").setOverlay('/pic/loader.gif');
				$('.btn-next').attr('disabled', true);
			},
			onprogress: function (e) {
			},
			error: function () {
				alert("Error");
			},
			success: function (data) {
				$(".widget-box").removeOverlay();
				$('.btn-next').attr('disabled', false);
				if (importData(data, "Result") == "OK") {
					$("#fileInfo").removeClass();
					$("#fileInfo").addClass("bg-success");
					$("#fileInfo").html(importData(data, "Info"));

					filePath = importData(data, "Path");
					$("#<%=hdnFilePath.ClientID%>")[0].value = filePath;

                    fileIsValid = parseInt(importData(data, "CorrectNumberCount")) > 0 ? true : false;
                    

					$("#deleteFile").attr('disabled', false);
				}
				else {
					$("#fileInfo").removeClass();
					$("#fileInfo").addClass("bg-danger");
					filePath = '';
					fileIsValid = false;
					$("#fileInfo").html(importData(data, "Message"));
				}
			}
		});

		$('#fuelux-wizard').ace_wizard({}).on('change', function (e, info) {
			$(".btn-next").show();
			if (info.step == 1 && info.direction == 'next') {
				if (!validateRequiredFields('step1'))
					return false;
            }
			if (info.step == 2 && info.direction == 'next') {
                recievers = $("#<%=txtRecievers.ClientID%>")[0].value;
                var isvalidRecievers = $("#<%=txtRecievers.ClientID%>").attr('isvalid');

				if (treeGroups.GetSelectedNode()) {
					phoneBookGroup = treeGroups.GetSelectedNode().id;
					if (treeGroups.GetSelectedNode().attributes)
						phoneBookGroupType = treeGroups.GetSelectedNode().attributes.type;

					var numberType = $("#<%=drpSenderNumber.ClientID%> option:selected")[0].value.split(';')[1];
					if (numberType == '<%=(int)Arad.SMS.Gateway.Business.TypePrivateNumberAccesses.Bulk%>' && phoneBookGroupType == '<%=(int)Arad.SMS.Gateway.Business.PhoneBookGroupType.Vas%>') {
						result('step2', 'Error', '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SenderIdToVasGroupIsInvalid")%>');
						return false;
					}
					if (numberType != '<%=(int)Arad.SMS.Gateway.Business.TypePrivateNumberAccesses.Bulk%>' && phoneBookGroupType == '<%=(int)Arad.SMS.Gateway.Business.PhoneBookGroupType.Normal%>') {
						result('step2', 'Error', '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SenderIdToGroupIsInvalid")%>');
						return false;
					}
				}

				$("#<%=hdnGroupGuid.ClientID%>")[0].value = phoneBookGroup;

				if ((recievers == '' || (recievers != '' && isvalidRecievers == 'false')) &&
						phoneBookGroup == '' &&
					  (filePath == '' ||
						fileIsValid == false)) {
					result('step2', 'Error', '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RecieverListIsEmpty")%>')
					return false;
				}

				var smsPartCount = parseInt(getSmsCount('<%=txtSmsBody.ClientID%>TxtSmsBody'.replace('_', '')));
				var isUnicodeSms = hasUniCodeCharacter($("#<%=txtSmsBody.ClientID %> textarea")[0].value);
                smsPartCount = isNaN(smsPartCount) ? 0 : smsPartCount;

                //debugger;
                retVal = getAjaxResponse("GetSendSmsInfo",
																 "SenderNumber=" + $("#<%=drpSenderNumber.ClientID%> option:selected")[0].value.split(';')[0] +
																 "&Receivers=" + $("#<%=txtRecievers.ClientID%>")[0].value +
																 "&GroupGuid=" + phoneBookGroup +
																 "&FilePath=" + filePath +
																 "&SmsPartCount=" + smsPartCount +
                                                                "&IsUnicodeSms=" + isUnicodeSms);

				if (importData(retVal, "Result") == 'OK') {
					if (parseInt(importData(retVal, "TotalCount")) == 0) {
                        result('step2', 'Error', '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RecieverListIsEmpty")%>');
                        console.log(retVal);                       
						return false;
					}
				}
				else {
					result('step2', 'Error', importData(retVal, "Message"));
					return false;
				}
			}

			if (info.step == 3 && info.direction == 'next') {

				if ($("#<%=rdbSendDateTime.ClientID%>").attr('checked') == 'checked') {
					if (!validateRequiredFields('sendDateTime'))
						return false;
				}
				else if ($("#<%=rdbSendPeriod.ClientID%>").attr('checked') == 'checked') {
					if (!validateRequiredFields('sendPeriod'))
						return false;
				}
				else if ($("#<%=rdbGradual.ClientID%>").attr('checked') == 'checked') {
					if (!validateRequiredFields('sendGradual'))
						return false;
				}

				return setSendPriceTableInfo('step3', retVal);
				//$(".btn-next").hide();
			}
		});


        $('#FileUploadAudio').ajaxUploadPrompt({
            url: '/handler/UploadAndGetFileAudio.ashx',
            beforeSend: function () { },
            onprogress: function (e) {
                $("#messageResult").html("<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("LoadingPleaseWait") %>" + e.loaded / e.total * 100 + " % ");
                $("#messageResult").removeClass('hide');
                $("#messageResult").addClass('text-danger');
                $("#messageResult").removeClass('text-success');
            },
            error: function () {
                alert("Error");
            },
            success: function (data) {
                if (importData(data, "Result") == "OK") {
                    $("#messageResult").html(importData(data, "Info"));
                    $("#messageResult").removeClass('hide');
                    $("#messageResult").removeClass('text-danger');
                    $("#messageResult").addClass('text-success');
                    $("#<%=hdnFileSoundPath.ClientID%>")[0].value = importData(data, "Path");
                    $("#<%=hdnFileSoundID.ClientID%>")[0].value = importData(data, "voiceMessageId");
                    $("#<%=hdnFileSoundLength.ClientID%>")[0].value = importData(data, "voiceLength");
                  }
                  else {
                      $("#messageResult").removeClass();
                      $("#messageResult").addClass("bg-danger");
                      $("#messageResult").html(importData(data, "Message"));
                  }
              }
          });


		$('#accordion2').on('shown.bs.collapse', function () {
			var panelId = $('#accordion2 .collapse.in').attr('id');
			$('input[type=radio]', '#accordion2 a[href="#' + panelId + '"] ').attr('checked', 'checked');
		});
	});

    function setSendPriceTableInfo(step, numbersInfo) {

	retVal = numbersInfo;
	filePath = importData(numbersInfo, "FilePath");
	if (filePath != '') {
		$("#<%=hdnFilePath.ClientID%>")[0].value = filePath;
		fileIsValid = parseInt(importData(numbersInfo, "FileCorrectNumberCount")) > 0 ? true : false;

		fileIsValid = true;
		$("#fileInfo").removeClass();
		$("#fileInfo").addClass("bg-success");
		$("#fileInfo").html(importData(numbersInfo, "FileInfo"));
	}

	var totalCount = (isNaN(parseInt(importData(numbersInfo, "TotalCount"))) ? 0 : parseInt(importData(numbersInfo, "TotalCount")));

	$("#recipientCount").text(totalCount);

	var smsPartCount = parseInt(getSmsCount('<%=txtSmsBody.ClientID%>TxtSmsBody'.replace('_', '')));

	$("#smsPartCount").text(smsPartCount);
	var mciCount = (isNaN(parseInt(importData(numbersInfo, "1"))) ? 0 : parseInt(importData(numbersInfo, "1")));//MCI
	var mtnCount = (isNaN(parseInt(importData(numbersInfo, "2"))) ? 0 : parseInt(importData(numbersInfo, "2")));//MTN
	var otherCount = (totalCount - mciCount - mtnCount);

	if ($("#<%=rdbGradual.ClientID%>").attr('checked') == 'checked' && Math.floor((mciCount + mtnCount + otherCount) / 1000) == 0) {
		result(step, 'Error', '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("InvalidSendGradualSmsPageSize")%>');
		return false;
	}

	if (importData(numbersInfo, "NumberType") == '<%=(int)Arad.SMS.Gateway.Business.TypePrivateNumberAccesses.Bulk%>') {
		mciCount *= smsPartCount;
		mtnCount *= smsPartCount;
		otherCount *= smsPartCount
	}

	$("#mciCount").text(mciCount);
	$("#mtnCount").text(mtnCount);
	$("#otherCount").text(otherCount);

	$("#totalCount").text(parseFloat(importData(numbersInfo, "SendPrice")));
}

    function result(step, type, message) {

	var wizard = $('#fuelux-wizard').data('wizard');
	var successPatern = "<span class='fa fa-check fa-2x green'></span>";
	var unsuccessPatern = "<span class='fa fa-times fa-2x red'></span>";
	switch (step) {
		case 'step1':
			if (type == "OK") {
				$("#resultStep1").addClass("bg-success div-save-result");
				$("#resultStep1").html(successPatern + message);
			}
			else {
				wizard.currentStep = 1;
				wizard.setState();
				$("#resultStep1").addClass("bg-danger div-save-result");
				$("#resultStep1").html(unsuccessPatern + message);
			}
			break;
		case 'step2':
			if (type == "OK") {
				$("#resultStep2").addClass("bg-success div-save-result");
				$("#resultStep2").html(successPatern + message);
			}
			else {
				wizard.currentStep = 2;
				wizard.setState();
				$("#resultStep2").addClass("bg-danger div-save-result");
				$("#resultStep2").html(unsuccessPatern + message);
			}
			break;
		case 'step3':
			if (type == "OK") {
				$("#resultStep3").addClass("bg-success div-save-result");
				$("#resultStep3").html(successPatern + message);
			}
			else {
				wizard.currentStep = 3;
				wizard.setState();
				$("#resultStep3").addClass("bg-danger div-save-result");
				$("#resultStep3").html(unsuccessPatern + message);
			}
			break;
		case 'step4':
			if (type == "OK") {
				wizard.currentStep = 4;
				wizard.setState();
				$("#resultStep4").addClass("bg-success div-save-result");
				$("#resultStep4").html(successPatern + message);
			}
			else {
				wizard.currentStep = 4;
				wizard.setState();
				$("#resultStep4").addClass("bg-danger div-save-result");
				$("#resultStep4").html(unsuccessPatern + message);
			}
			break;
	}
}

function deleteFile() {
	if (filePath != '') {
		filePath = '';
		fileIsValid = false;
		$("#deleteFile").attr('disabled', true);
		$("#fileInfo").html('');
		$("#fileInfo").removeClass();
	}
}

function changePeriodType() {
	var selectedValue = $('#<%=drpPeriodType.ClientID %>').find('option:selected').attr('value');
	switch (selectedValue) {
		case '':
			$('#lblPer').html('....');
			break;
		case '<%=(int)Arad.SMS.Gateway.Business.SmsSentPeriodType.Minute %>':
			$('#lblPer').html('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PerMinute") %>');
			break;
		case '<%=(int)Arad.SMS.Gateway.Business.SmsSentPeriodType.Hour %>':
			$('#lblPer').html('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PerHour") %>');
			break;
		case '<%=(int)Arad.SMS.Gateway.Business.SmsSentPeriodType.Daily %>':
			$('#lblPer').html('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PerDaily") %>');
			break;
		case '<%=(int)Arad.SMS.Gateway.Business.SmsSentPeriodType.Weekly %>':
			$('#lblPer').html('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PerWeekly") %>');
			break;
		case '<%=(int)Arad.SMS.Gateway.Business.SmsSentPeriodType.Monthly %>':
			$('#lblPer').html('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PerMonthly") %>');
			break;
		case '<%=(int)Arad.SMS.Gateway.Business.SmsSentPeriodType.Yearly %>':
			$('#lblPer').html('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PerYearly") %>');
			break;
	}
}

function nodeTextFormat(node) {
	var text = node.text;
	if (node.attributes) {
		text += '&nbsp;<span style=\'color:blue\'>(' + node.attributes.count + ')</span>';
		if (node.attributes.type == '<%=(int)Arad.SMS.Gateway.Business.PhoneBookGroupType.Vas%>')
            text += '&nbsp;<span style=\'color:red\'>(<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("VASGroup") %>)</span>';
	}
	return text;
}

function confirmRequest() {
	var result = confirm('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmSendSms")%>');
	return result;
}

function template() {
	var address = '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsTemplates_LoadSmsTemplate,Session)%>';
	getAjaxPage(address, 'ActionType=insert', '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SaveAccess") %>');
}
</script>

<style type="text/css">
	.panel-heading {
		padding: 5px;
	}
</style>

<div class="col-xs-12 col-md-12">
	<div class="widget-box">
		<div class="widget-body">
			<div class="widget-main">
				<div id="fuelux-wizard" data-target="#step-container">
					<ul class="wizard-steps">
						<li data-target="#step1" class="active">
							<span class="step">1</span>
							<span class="title"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GeneralInformation") %></span>
						</li>
						<li data-target="#step2" class="">
							<span class="step">2</span>
							<span class="title"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Recipients") %></span>
						</li>
						<li data-target="#step3" class="">
							<span class="step">3</span>
							<span class="title"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendTime") %></span>
						</li>
						<li data-target="#step4" class="">
							<span class="step">4</span>
							<span class="title"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Confirm") %></span>
						</li>
					</ul>
				</div>
				<hr style="margin: 0">
				<div class="step-content pos-rel" id="step-container">
					<div class="step-pane active" id="step1" style="height: 430px;">
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
											<GeneralTools:SmsBodyControl runat="server" ID="txtSmsBody" ValidationSet="step1" IsRequired="true"></GeneralTools:SmsBodyControl>
										</div>
										<div class="form-group">
											<div class="col-xs-8 col-sm-8">
												<asp:TextBox ID="txtTestRecievers" CssClass="form-control input-sm" isRequired="true" validationSet="sendTestSms" runat="server" placeholder="Receivers (comma separator)"></asp:TextBox>
											</div>
											<asp:Button ID="btnSendTestSms" runat="server" class="btn btn-default btn-sm" Text="SendTest" OnClick="btnSendTestSms_Click" />
											<div id="resultStep1" class="div-save-result"></div>
										</div>
									</div>
								</div>
								<div class=" col-md-1"></div>
                          <%--      <div class="form-group col-md-5">
                                        <div class="input-group">
                                            <span class="input-group-addon" style="background-color: rgb(80, 168, 226); color: #fff;">عنوان پيام صوتي</span>
                                            <asp:TextBox ID="txtVoiceTitle" CssClass="form-control input-sm" validationSet="voiceTitle" runat="server" placeholder="عنوان پيام صوتي"></asp:TextBox>
                                        </div>
                                    </div>--%>
                         <%--           <div class="form-group col-md-12">
                                        <div class="input-group">
                                            <span class="input-group-addon" style="background-color: rgb(80, 168, 226); color: #fff;">انتخاب فايل</span>
                                            <label class="ace-file-input col-md-6">
                                                <span id="FileUploadAudio" class="ace-file-container" data-title="انتخاب"><span class="ace-file-name" data-title="فايل انتخاب نشده ..."><i class="ace-icon fa fa-upload"></i></span></span><a class="remove" href="#"><i class="ace-icon fa fa-times"></i></a>
                                            </label>
                                        </div>
                                        <div class="col-md-3 col-md-offset-4 well hide" id="messageResult">
                                        </div>
                                    </div>--%>
								<div class=" col-md-8">
									
                                        <% if(Session["Language"].ToString() == "en") { %>
                                            <div class="panel panel-warning" style="text-align: left;">
                                            <% }  else
                                           {%>
                                            <div class="panel panel-warning">
                                                <%  } %>
										<div class="panel-heading"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Guide") %></div>
										<div id="divHelp" class="panel-body">
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
					<div class="step-pane" id="step2" style="height: 430px;">
						<div class="container-fluid">
							<div class="row">
								<div class="col-md-3">
									<div class="form-group">
										<label><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Recipients") %></label>
										<asp:TextBox ID="txtRecievers" runat="server" class="form-control mobileNumberInputList" Height="100px" TextMode="MultiLine" placeholder="Max 2000"></asp:TextBox>
									</div>
									<div class="form-group">
										<label><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("IncorectNumbers") %></label>
										<asp:TextBox ID="txtRecieversInvalid" runat="server" class="form-control" Height="100px" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
									</div>
									<div class="form-group">
										<div id="infoNumbers">
											<input id="txtCorrectNumber" type="text" class="input" readonly="true" style="width: 100%; border: 0 solid black;" />
											<input id="txtFailNumber" type="text" class="input" readonly="true" style="width: 100%; border: 0 solid black;" />
										</div>
									</div>
									<div class="clear"></div>
									<div id="resultStep2" class="div-save-result"></div>
								</div>
								<div class="col-md-9">
									<div class="panel-group" id="accordion">
										<div class="panel panel-default">
											<div class="panel-heading">
												<h4 class="panel-title">
													<a data-toggle="collapse" data-parent="#accordion" href="#collapseOne"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PhoneBook") %></a>
												</h4>
											</div>
											<div id="collapseOne" class="panel-collapse collapse in">
												<div class="panel-body">
													<div class="col-md-4">
														<div class="form-group">
															<select id="drpPhoneBookGroup" style="width: 200px"></select>
															<div id="tree" style="width: 250px">
																<div style="padding: 10px">
																	<GeneralTools:TreeView ID="treeGroups" runat="server" ShowLines="true" OnClick="clickNode(node);" Formatter="return nodeTextFormat(node);"></GeneralTools:TreeView>
																</div>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>
										<div id="divFile" class="panel panel-default">
											<div class="panel-heading">
												<h4 class="panel-title">
													<a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("File") %></a>
												</h4>
											</div>
											<div id="collapseTwo" class="panel-collapse collapse">
												<div class="panel-body">
													<div id="FileUpload" class="btn btn-primary"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelectFile") %></div>
													<div id="deleteFile" class="btn btn-danger" disabled="disabled" onclick="deleteFile();"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeleteFile") %></div>
													<div class="bg-info" style="padding: 10px; margin-top: 5px;">xls , xlsx , csv</div>
													<div style="padding: 10px; margin-top: 5px;" id="fileInfo"></div>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
					<div class="step-pane" id="step3" style="height: 430px;">
						<div class="container-fluid">
							<div class="row">
								<div class="col-md-5">
									<div class="panel-group" id="accordion2">
										<div class="panel panel-default">
											<div class="panel-heading">
												<h4 class="panel-title">
													<a data-toggle="collapse" data-parent="#accordion2" href="#sendFuture">
														<asp:RadioButton ID="rdbSendDateTime" GroupName="sendDate" Checked="true" runat="server" />
														<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendTime") %>
													</a>
												</h4>
											</div>
											<div id="sendFuture" class="panel-collapse collapse in">
												<div class="panel-body">
													<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendTime") %></label>
													<SMS:DateTimePicker ID="dtpSendDateTime" IsRequired="true" ValidationSet="sendDateTime" runat="server"></SMS:DateTimePicker>
												</div>
											</div>
										</div>
										<div class="panel panel-default">
											<div class="panel-heading">
												<h4 class="panel-title">
													<a data-toggle="collapse" data-parent="#accordion2" href="#sendPeriod">
														<asp:RadioButton ID="rdbSendPeriod" GroupName="sendDate" runat="server" />
														<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendPeriodSms") %>
													</a>
												</h4>
											</div>
											<div id="sendPeriod" class="panel-collapse collapse">
												<div class="panel-body">
													<div class="col-md-6">
														<label><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TypeSend")%></label>
														<asp:DropDownList ID="drpPeriodType" isRequired="true" validationSet="sendPeriod" runat="server" CssClass="form-control input-sm " onchange="changePeriodType();"></asp:DropDownList>
													</div>
													<div class="col-md-6">
														<label><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Per")%>&nbsp;<span id="lblPer">....</span>&nbsp;<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Once")%></label>
														<asp:TextBox ID="txtPeriod" runat="server" isRequired="true" validationSet="sendPeriod" CssClass="form-control input-sm numberInput"></asp:TextBox>
													</div>
													<div class="col-md-12">
														<div class="form-group">
															<label><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("StartDate") %></label>
															<SMS:DateTimePicker ID="dtpPeriodStart" IsRequired="true" ValidationSet="sendPeriod" runat="server"></SMS:DateTimePicker>
														</div>
														<div class="form-group">
															<label class="control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("EndDate") %></label>
															<SMS:DateTimePicker ID="dtpPeriodEnd" IsRequired="true" ValidationSet="sendPeriod" runat="server"></SMS:DateTimePicker>
														</div>
													</div>
												</div>
											</div>
										</div>
										<div class="panel panel-default">
											<div class="panel-heading">
												<h4 class="panel-title">
													<a data-toggle="collapse" data-parent="#accordion2" href="#sendGradual">
														<asp:RadioButton ID="rdbGradual" GroupName="sendDate" runat="server" />
														<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendGradualSms") %>
													</a>
												</h4>
											</div>
											<div id="sendGradual" class="panel-collapse collapse">
												<div class="panel-body">
													<div>
														<label><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("EveryFewMinutes") %></label>
														<asp:TextBox ID="txtGradualPeriod" runat="server" isRequired="true" validationSet="sendGradual" CssClass="form-control input-sm numberInput"></asp:TextBox>
													</div>
													<div>
														<label>x * 1000</label>
														<asp:TextBox ID="txtGradualPageSize" runat="server" isRequired="true" validationSet="sendGradual" CssClass="form-control input-sm numberInput"></asp:TextBox>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
							<div class="clear"></div>
							<div id="resultStep3" class="div-save-result"></div>
						</div>
					</div>
					<div class="step-pane" id="step4" style="height: 430px;">
						<div class="container-fluid">
							<div id="confirmSendSms" class="row">
								<div class="col-md-4 col-xs-2"></div>
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
<%--										<tr class="success">
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
												<asp:Button ID="btnSendSms" runat="server" Text="SendSms" CssClass="btn btn-primary btn-lg" Style="width: 100%" OnClientClick="return confirmRequest();" OnClick="btnSendSms_Click" />
											</td>
										</tr>
									</table>
									<div id="resultStep4" class="div-save-result"></div>
								</div>
								<div class="col-md-4 col-xs-2"></div>
							</div>
						</div>
					</div>
				</div>
				<div class="clear"></div>

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
<script type="text/javascript">
	function clickNode(node) {
		var s = node.text;
		$('#drpPhoneBookGroup').combo('setText', s).combo('hidePanel');
	}

	$(function () {
		$('#drpPhoneBookGroup').combo({
			required: true,
			editable: false
		});
		$('#tree').appendTo($('#drpPhoneBookGroup').combo('panel'));
		$('#tree input').click(function () {
			var v = $(this).val();
			var s = $(this).next('span').text();
			$('#drpPhoneBookGroup').combo('setValue', v).combo('setText', s).combo('hidePanel');
		});
	});
</script>
