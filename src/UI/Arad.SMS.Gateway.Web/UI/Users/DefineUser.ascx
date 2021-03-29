<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefineUser.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.DefineUser" %>
<%@ Register Src="~/UI/Controls/BootstrapDatePicker.ascx" TagName="DatePicker" TagPrefix="SMS" %>
<script src="/script/encryption.js" type="text/javascript"></script>
<script src="/script/jquery.ajaxupload.js"></script>

<asp:HiddenField ID="hdnUserType" runat="server" />
<asp:HiddenField ID="hdnCreatedUserGuid" runat="server" />
<asp:HiddenField ID="hdnUserName" runat="server" />
<asp:HiddenField ID="hdnPassword" runat="server" />
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<script type="text/javascript">
	$(document).ready(function () {
		$('#fuelux-wizard').ace_wizard({}).on('change', function (e, info) {
			if (info.step == 1 && info.direction == 'next') {
				return validationAccount();
			}
		}).on('finished', function (e) {
			$("#return")[0].click();
		}).on('stepclick', function (e) {
			e.preventDefault();
		});

		$.ajaxUploadSettings.name = 'upload';
		$('#uploadActualUserDocument').ajaxUploadPrompt({
			url: '/handler/UploadUserDocument.ashx',
			data: {
			},
			beforeSend: function (xhr, opts) {
				opts.data.append('user', $('#<%=hdnUserName.ClientID%>')[0].value);
				opts.data.append('documentId', $('#<%=drpPersonalDocumentType.ClientID%> option:selected')[0].value);
			},
			onprogress: function (e) {
			},
			error: function () {
			},
			success: function (data) {
				if (importData(data, "Result") == "OK") {
					$("#userDocumentResult").removeClass();
					$("#userDocumentResult").html('');


					addRowToUserDocumentList(importData(data, "DocumentId"),
																	 $('#<%=drpPersonalDocumentType.ClientID%> option:selected').text(),
																	 importData(data, "Path"),
																	 importData(data, "File"));
					}
					else {
						$("#userDocumentResult").removeClass();
						$("#userDocumentResult").addClass("bg-danger div-save-result");
						$("#userDocumentResult").html("<span class='fa fa-times fa-2x red'></span>" + importData(data, "Message"));
					}
			}
		});

		$.ajaxUploadSettings.name = 'upload';
		$('#uploadLegalUserDocument').ajaxUploadPrompt({
			url: '/handler/UploadUserDocument.ashx',
			data: {
			},
			beforeSend: function (xhr, opts) {
				opts.data.append('user', $('#<%=hdnUserName.ClientID%>')[0].value);
				opts.data.append('documentId', $('#<%=drpCompanyDocumentType.ClientID%> option:selected')[0].value);
			},
			onprogress: function (e) {
			},
			error: function () {
			},
			success: function (data) {
				if (importData(data, "Result") == "OK") {
					$("#companyDocumentResult").removeClass();
					$("#companyDocumentResult").html('');


					addRowToCompanyDocumentList(importData(data, "DocumentId"),
																			$('#<%=drpCompanyDocumentType.ClientID%> option:selected').text(),
																			importData(data, "Path"),
																			importData(data, "File"));
					}
					else {
						$("#companyDocumentResult").removeClass();
						$("#companyDocumentResult").addClass("bg-danger div-save-result");
						$("#companyDocumentResult").html("<span class='fa fa-times fa-2x red'></span>" + importData(data, "Message"));
					}
			}
		});
	});

		function addRowToUserDocumentList(documentId, document, path, file) {
			var insertedGuid = getAjaxResponse("InsertUserDocumentRecord", "UGuid=" + $('#<%=hdnCreatedUserGuid.ClientID%>')[0].value + "&Key=" + documentId + "&Type=" +<%=(int)Arad.SMS.Gateway.Business.UserType.Actual%> +"&Path=" + path);

			gridActualUserDocument.AddRow({
				Guid: insertedGuid,
				DocumentId: documentId,
				Status: '<img class="gridImageButton" src="/pic/arrowsloader.gif"/>',
				Document: document,
				Path: path,
				File: file,
				Action: "<span class='ui-icon fa fa-trash-o red' onclick='deleteUserDocument(event);'></span>"
			});
		}

		function addRowToCompanyDocumentList(documentId, document, path, file) {
			var insertedGuid = getAjaxResponse("InsertUserDocumentRecord", "UGuid=" + $('#<%=hdnCreatedUserGuid.ClientID%>')[0].value + "&Key=" + documentId + "&Type=" +<%=(int)Arad.SMS.Gateway.Business.UserType.Legal%> +"&Path=" + path)

		gridLegalUserDocument.AddRow({
			DocumentId: documentId,
			Status: '<img class="gridImageButton" src="/pic/arrowsloader.gif"/>',
			Document: document,
			Path: path,
			File: file,
			Action: "<span class='ui-icon fa fa-trash-o red' onclick='deleteCompanyDocument(event);'></span>"
		});
	}

	function deleteUserDocument(e) {
		gridActualUserDocument.Event = e;
		if (gridActualUserDocument.IsSelectedRow()) {
			var path = gridActualUserDocument.GetSelectedRowFieldValue('Path');
			var isDelete = getAjaxResponse("DeleteUserDocumentRecord", "Guid=" + gridActualUserDocument.SelectedGuid + "&path=" + path);
			if (isDelete)
				gridActualUserDocument.DeleteRow(gridActualUserDocument.GetSelectedRowID());
		}
	}

	function deleteCompanyDocument(e) {
		gridLegalUserDocument.Event = e;
		if (gridLegalUserDocument.IsSelectedRow()) {
			var path = gridLegalUserDocument.GetSelectedRowFieldValue('Path');
			var isDelete = getAjaxResponse("DeleteUserDocumentRecord", "Guid=" + gridLegalUserDocument.SelectedGuid + "&path=" + path);
			if (isDelete)
				gridLegalUserDocument.DeleteRow(gridLegalUserDocument.GetSelectedRowID());
		}
	}

	function showActualUserForm() {
        $("#actualUser .page-header h4").text('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PersonProfile") %>');
		$("#<%=hdnUserType.ClientID%>")[0].value = '<%=(int)Arad.SMS.Gateway.Business.UserType.Actual%>';
		$("#selectUser").hide();
		$("#actualUser").show();
		$("#buttons").show();
	}

	function showLegalUserForm() {
        $("#actualUser .page-header h4").text('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AgentProfile") %>');
		$("#<%=hdnUserType.ClientID%>")[0].value = '<%=(int)Arad.SMS.Gateway.Business.UserType.Legal%>';
		$("#selectUser").hide();
		$("#actualUser").show();
		$("#legalUser").show();
		$("#buttons").show();
	}

	function cancelProfile() {
		$("#selectUser").show();
		$("#actualUser").hide();
		$("#legalUser").hide();
		$("#buttons").hide();
	}

	var userNameIsValid = false;
	function checkUserName() {
		var userName = $("#<%=txtUserName.ClientID%>")[0].value;
			if (userName != '') {
				var isValid = getAjaxResponse("CheckUserName", "UserName=" + userName);
				if (isValid == true) {
					$("#<%=txtUserName.ClientID%>").parents('.form-group').removeClass('has-error').addClass('has-success');
					userNameIsValid = true;
				}
				else {
					$("#<%=txtUserName.ClientID%>").parents('.form-group').removeClass('has-success').addClass('has-error');
					userNameIsValid = false;
				}
			}
		}

		function result(step, part, type, message) {
			var wizard = $('#fuelux-wizard').data('wizard');
			var successPatern = "<span class='fa fa-check fa-2x green'></span>";
			var unsuccessPatern = "<span class='fa fa-times fa-2x red'></span>";
			switch (step) {
				case 'step1':
					if (type == "OK") {
						wizard.currentStep = 2;
						wizard.setState();
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
						wizard.currentStep = 2;
						wizard.setState();
						if (part == 'actual')
							showActualUserForm();
						else
							showLegalUserForm();

						$("#resultStep2").addClass("bg-success div-save-result");
						$("#resultStep2").html(successPatern + message);
					}
					else {
						wizard.currentStep = 2;
						wizard.setState();
						if (part == 'actual')
							showActualUserForm();
						else
							showLegalUserForm();

						$("#resultStep2").addClass("bg-danger div-save-result");
						$("#resultStep2").html(unsuccessPatern + message);
					}
					break;
			}
		}

		function validationAccount() {
			if ($("#<%=txtPassword.ClientID%>")[0].value != '') {
				$("#<%=txtUserPassword.ClientID%>")[0].value = MD5($("#<%=txtPassword.ClientID%>")[0].value);
			}
			return validateRequiredFields('panelInfo');
		}

		function validationProfile() {
			var userType = $("#<%=hdnUserType.ClientID%>")[0].value;
			switch (userType) {
				case '<%=(int)Arad.SMS.Gateway.Business.UserType.Actual%>':
					if (!validateRequiredFields('ActualUser'))
						return false;

					return true;
					break;
				case '<%=(int)Arad.SMS.Gateway.Business.UserType.Legal%>':
					if (!validateRequiredFields('ActualUser') && !validateRequiredFields('LegalUser'))
						return false;

					return true;
					break;
			}
		}
</script>

<div class="col-xs-12 col-md-12">
	<div class="widget-box">
		<div class="widget-body">
			<div class="widget-main">
				<a id="return" href="/PageLoader.aspx?c=<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_User,Session)%>"><i class="fa fa-arrow-right fa-3x" style="color: green;"></i></a>
				<div id="fuelux-wizard" data-target="#step-container">
					<ul class="wizard-steps">
						<li data-target="#step1" class="active">
							<span class="step">1</span>
							<span class="title"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CreateAccount") %></span>
						</li>
						<li data-target="#step2" class="">
							<span class="step">2</span>
							<span class="title"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Profile") %></span>
						</li>
					</ul>
				</div>
				<hr style="margin: 0">
				<div class="step-content pos-rel" id="step-container">
					<div class="step-pane active" id="step1" style="min-height: 400px;">
						<div class="row">
							<div class="col-md-4">
								<div class="form-horizontal" role="form">
									<div class="form-group">
										<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserName")%></label>
										<div class="col-sm-7">
											<asp:TextBox ID="txtUserName" class="form-control input-sm" validationSet="panelInfo" isRequired="true" runat="server" onblur="checkUserName()"></asp:TextBox>
										</div>
									</div>
									<div class="form-group">
										<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Password")%></label>
										<div class="col-sm-7">
											<asp:TextBox ID="txtUserPassword" runat="server" Style="display: none"></asp:TextBox>
											<asp:TextBox ID="txtPassword" class="form-control input-sm" validationSet="panelInfo" isRequired="true" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="form-group">
										<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Role")%></label>
										<div class="col-sm-7">
											<asp:DropDownList ID="drpRole" class="form-control input-sm" validationSet="panelInfo" isRequired="true" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="form-group">
										<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupPrice")%></label>
										<div class="col-sm-7">
											<asp:DropDownList ID="drpGroupPrice" class="form-control input-sm" validationSet="panelInfo" isRequired="true" runat="server"></asp:DropDownList>
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
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-horizontal" role="form">
									<div class="form-group">
										<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ExpireDate")%></label>
										<div class="col-sm-7">
											<SMS:DatePicker ID="dtpExpireDate" ValidationSet="panelInfo" IsRequired="true" runat="server"></SMS:DatePicker>
										</div>
									</div>
									<div class="form-group">
										<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MaximumAdmin")%></label>
										<div class="col-sm-7">
											<asp:TextBox ID="txtMaximumAdmin" class="form-control input-sm numberInput" validationSet="panelInfo" isRequired="true" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="form-group">
										<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MaximumUser")%></label>
										<div class="col-sm-7">
											<asp:TextBox ID="txtMaximumUser" class="form-control input-sm numberInput" validationSet="panelInfo" isRequired="true" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="form-group">
										<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PanelPrice")%></label>
										<div class="col-sm-7">
											<div class="input-group" style="z-index: 1">
												<asp:TextBox ID="txtPanelPrice" class="form-control input-sm numberInput" validationSet="panelInfo" isRequired="true" autoFormatDecimal="true" runat="server"></asp:TextBox>
												<span class="input-group-addon"><%--ریال--%></span>
											</div>
										</div>
									</div>
								</div>
								<div class="buttonControlDiv">
									<asp:Button ID="btnSavePanelInfo" CssClass="btn btn-warning" runat="server" Text="Register" OnClick="btnSavePanelInfo_Click" />
								</div>
							</div>
							<div class="clear"></div>
							<div id="resultStep1" class="div-save-result"></div>
						</div>
					</div>
					<div class="step-pane" id="step2" style="min-height: 400px;">
						<div class="row">
							<div id="selectUser">
								<a href="#" class="alert-link" onclick="showActualUserForm();">
									<div class="col-md-2" style="display: table; text-align: center">
										<div class="alert alert-info" style="height: 100px; display: table-cell; vertical-align: middle;">
											<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Actual") %>
										</div>
									</div>
								</a>
								<a href="#" onclick="showLegalUserForm();">
									<div class="col-md-2" style="display: table; text-align: center">
										<div class="alert alert-success" style="height: 100px; display: table-cell; vertical-align: middle;">
											<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Legal") %>
										</div>
									</div>
								</a>
							</div>
							<div class="clear"></div>
							<div id="actualUser" style="display: none;">
								<div class="page-header bg-info" style="margin: 0 0 15px;">
									<h4><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PersonalProfile") %></h4>
								</div>
								<div class="col-md-4">
									<div class="form-horizontal" role="form">
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FirstName")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtFirstName" class="form-control input-sm" validationSet="ActualUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("LastName")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtLastName" class="form-control input-sm" validationSet="ActualUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FatherName")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtFatherName" class="form-control input-sm" validationSet="ActualUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NationalCode")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtNationalCode" class="form-control input-sm numberInput" validationSet="ActualUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ShCode")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtShCode" class="form-control input-sm numberInput" validationSet="ActualUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("BirthDate")%></label>
											<div class="col-sm-8">
												<SMS:DatePicker ID="dtpBirthDate" ValidationSet="ActualUser" IsRequired="true" runat="server"></SMS:DatePicker>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Email")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtEmail" class="form-control input-sm emailInput" validationSet="ActualUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
									</div>
								</div>
								<div class="col-md-4">
									<div class="form-horizontal" role="form">
										<div class="form-group">
											<label class="col-sm-4 control-label">
												<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MobileNo")%>
											</label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtMobile" class="form-control input-sm mobileNumberInput" validationSet="ActualUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PhoneNo")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtPhone" class="form-control input-sm numberInput" validationSet="ActualUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FaxNo")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtFax" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
											</div>
										</div>
										<asp:UpdatePanel ID="updatePanelCity" runat="server" UpdateMode="Conditional">
											<ContentTemplate>
												<div class="form-group">
													<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Country")%></label>
													<div class="col-sm-8">
														<asp:DropDownList ID="drpCountry" runat="server" class="form-control input-sm" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged" AutoPostBack="True" Style="min-width: 150px;"></asp:DropDownList>
													</div>
												</div>
												<div class="form-group">
													<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Province")%></label>
													<div class="col-sm-8">
														<asp:DropDownList ID="drpProvince" runat="server" class="form-control input-sm" OnSelectedIndexChanged="drpProvince_SelectedIndexChanged" AutoPostBack="True" Style="min-width: 150px;" validationSet="ActualUser" isRequired="true"></asp:DropDownList>
													</div>
												</div>
												<div class="form-group">
													<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("City")%></label>
													<div class="col-sm-8">
														<asp:DropDownList ID="drpCity" runat="server" class="form-control input-sm" Style="min-width: 150px;" validationSet="ActualUser" isRequired="true"></asp:DropDownList>
													</div>
												</div>
											</ContentTemplate>
											<Triggers>
												<asp:AsyncPostBackTrigger ControlID="drpCountry" EventName="SelectedIndexChanged" />
												<asp:AsyncPostBackTrigger ControlID="drpProvince" EventName="SelectedIndexChanged" />
											</Triggers>
										</asp:UpdatePanel>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ZipCode")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtZipCode" class="form-control input-sm numberInput" validationSet="ActualUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Address")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtAddress" class="form-control input-sm" validationSet="ActualUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
									</div>
								</div>
								<div class="col-md-4">
									<div class="form-group">
										<label class="col-sm-1 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Type")%></label>
										<div class="col-sm-5">
											<asp:DropDownList ID="drpPersonalDocumentType" class="form-control input-sm" runat="server"></asp:DropDownList>
										</div>
										<div class="col-sm-5">
											<div id="uploadActualUserDocument" class="btn btn-primary" style="border: 0;"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelectFile") %></div>
										</div>
									</div>
									<div class="clear"></div>
									<div class="bg-info" style="padding: 10px; margin-top: 5px;">jpg, jpeg, png, pdf</div>
									<div class="clear"></div>
									<div id="userDocumentResult" class="div-save-result"></div>
									<div id="actualUserDocument">
										<GeneralTools:DataGrid runat="server" ID="gridActualUserDocument" DefaultSortField="Type" ListCaption="ActualUserDocumentList" ShowRowNumber="true" DisableNavigationBar="true" ListHeight="155">
											<Columns>
												<GeneralTools:DataGridColumnInfo FieldName="DocumentId" Hidden="true" />
												<GeneralTools:DataGridColumnInfo FieldName="Document" Caption="Type" Align="Center" />
												<GeneralTools:DataGridColumnInfo FieldName="Path" Caption="File" Hidden="true" />
												<GeneralTools:DataGridColumnInfo FieldName="File" Caption="File" Align="Center" />
												<GeneralTools:DataGridColumnInfo FieldName="Action" Sortable="false" CellWidth="50" Align="Center" FormattingMethod="ImageButton" />
											</Columns>
										</GeneralTools:DataGrid>
									</div>
								</div>
							</div>
							<div class="clear"></div>
							<div id="legalUser" style="display: none;">
								<hr />
								<div class="page-header bg-warning" style="margin: 0 0 15px;">
									<h4><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("LegalInformation") %></h4>
								</div>
								<div class="col-md-4">
									<div class="form-horizontal" role="form">
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompanyName")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtCompanyName" class="form-control input-sm" validationSet="LegalUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompanyNationalID")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtCompanyNationalID" class="form-control input-sm" validationSet="LegalUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("EconomicCode")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtEconomicCode" class="form-control input-sm" validationSet="LegalUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompanyCEOName")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtCompanyCEOName" class="form-control input-sm" validationSet="LegalUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompanyCEONationalCode")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtCompanyCEONationalCode" class="form-control input-sm" validationSet="LegalUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
									</div>
								</div>
								<div class="col-md-4">
									<div class="form-horizontal" role="form">
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompanyCEOMobile")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtCompanyCEOMobile" class="form-control input-sm" validationSet="LegalUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompanyPhone")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtCompanyPhone" class="form-control input-sm" validationSet="LegalUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompanyZipCode")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtCompanyZipCode" class="form-control input-sm" validationSet="LegalUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompanyAddress")%></label>
											<div class="col-sm-8">
												<asp:TextBox ID="txtCompanyAddress" class="form-control input-sm" validationSet="LegalUser" isRequired="true" runat="server"></asp:TextBox>
											</div>
										</div>
									</div>
								</div>
								<div class="col-md-4">
									<div class="form-group">
										<label class="col-sm-1 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Type")%></label>
										<div class="col-sm-5">
											<asp:DropDownList ID="drpCompanyDocumentType" class="form-control input-sm" runat="server"></asp:DropDownList>
										</div>
										<div class="col-sm-5">
											<div id="uploadLegalUserDocument" class="btn btn-primary" style="border: 0;"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelectFile") %></div>
										</div>
									</div>
									<div class="clear"></div>
									<div class="bg-info" style="padding: 10px; margin-top: 5px;">jpg, jpeg, png, pdf</div>
									<div class="clear"></div>
									<div id="companyDocumentResult" class="div-save-result"></div>
									<div id="legalUserDocument">
										<GeneralTools:DataGrid runat="server" ID="gridLegalUserDocument" DefaultSortField="Type" ListCaption="LegalUserDocumentList" ShowRowNumber="true" DisableNavigationBar="true" ListHeight="155">
											<Columns>
												<GeneralTools:DataGridColumnInfo FieldName="DocumentId" Hidden="true" />
												<GeneralTools:DataGridColumnInfo FieldName="Document" Caption="Type" Align="Center" />
												<GeneralTools:DataGridColumnInfo FieldName="Path" Caption="File" Hidden="true" />
												<GeneralTools:DataGridColumnInfo FieldName="File" Caption="File" Align="Center" />
												<GeneralTools:DataGridColumnInfo FieldName="Action" Sortable="false" CellWidth="50" Align="Center" FormattingMethod="ImageButton" />
											</Columns>
										</GeneralTools:DataGrid>
									</div>
								</div>
							</div>
							<div class="clear"></div>
							<div id="resultStep2" class="div-save-result"></div>
							<div id="buttons" class="buttonControlDiv" style="display: none;">
								<asp:Button ID="btnSaveProfile" runat="server" Text="Register" CssClass="btn btn-warning" OnClick="btnSaveProfile_Click" />
								<a class="btn btn-default" onclick="cancelProfile();"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel")%></a>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
