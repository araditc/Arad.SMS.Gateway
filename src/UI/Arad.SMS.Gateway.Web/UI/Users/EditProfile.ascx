<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditProfile.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.EditProfile" %>
<%@ Register Src="~/UI/Controls/BootstrapDatePicker.ascx" TagName="DatePicker" TagPrefix="SMS" %>
<script src="/script/jquery.ajaxupload.js"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<script type="text/javascript">
	$(document).ready(function () {
		$.ajaxUploadSettings.name = 'upload';
		$('#uploadActualUserDocument').ajaxUploadPrompt({
			url: '/handler/UploadUserDocument.ashx',
			data: {
			},
			beforeSend: function (xhr, opts) {
				opts.data.append('user', '<%=username%>');
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
				opts.data.append('user', '<%=username%>');
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
			var insertedGuid = getAjaxResponse("InsertUserDocumentRecord", "UGuid=" + '<%=UserGuid%>' + "&Key=" + documentId + "&Type=" +<%=(int)Arad.SMS.Gateway.Business.UserType.Actual%> +"&Path=" + path);

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
			var insertedGuid = getAjaxResponse("InsertUserDocumentRecord", "UGuid=" + '<%=UserGuid%>' + "&Key=" + documentId + "&Type=" +<%=(int)Arad.SMS.Gateway.Business.UserType.Legal%> +"&Path=" + path)

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
</script>


<asp:Panel ID="pnlActual" runat="server">
	<div class="col-xs-12 col-md-12">
		<div class="form-horizontal" role="form">
			<div class="page-header bg-info">
				<h4><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PersonalProfile") %></h4>
				<%if(!IsAuthenticated) {%><p style="color:darkred;font-weight:bold"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("completeYourDetails") %></p><% }%>
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
									<asp:DropDownList ID="drpProvince" runat="server" class="form-control input-sm" OnSelectedIndexChanged="drpProvince_SelectedIndexChanged" AutoPostBack="True" Style="min-width: 150px;" isRequired="true" validationSet="Edit"></asp:DropDownList>
								</div>
							</div>
							<div class="form-group">
								<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("City")%></label>
								<div class="col-sm-8">
									<asp:DropDownList ID="drpCity" runat="server" class="form-control input-sm" Style="min-width: 150px;" isRequired="true" validationSet="Edit"></asp:DropDownList>
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
							<GeneralTools:DataGridColumnInfo FieldName="Status" Caption="Status" Align="Center" FormattingMethod="CustomRender" />
							<GeneralTools:DataGridColumnInfo FieldName="Document" Caption="Type" Align="Center" FormattingMethod="CustomRender" />
							<GeneralTools:DataGridColumnInfo FieldName="Path" Caption="File" Hidden="true" />
							<GeneralTools:DataGridColumnInfo FieldName="File" Caption="File" Align="Center" />
							<GeneralTools:DataGridColumnInfo FieldName="Description" Caption="Description" Align="Center" />
							<GeneralTools:DataGridColumnInfo FieldName="Action" Sortable="false" CellWidth="50" Align="Center" FormattingMethod="ImageButton" />
						</Columns>
					</GeneralTools:DataGrid>
				</div>
			</div>
		</div>
	</div>
</asp:Panel>
<div class="clear"></div>
<asp:Panel ID="pnlLegal" runat="server" Visible="false">
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
				<label class="col-sm-4 control-label">
					<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompanyCEOMobile")%>
				</label>
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
					<GeneralTools:DataGridColumnInfo FieldName="Status" Caption="Status" Align="Center" FormattingMethod="CustomRender" />
					<GeneralTools:DataGridColumnInfo FieldName="Document" Caption="Type" Align="Center" FormattingMethod="CustomRender" />
					<GeneralTools:DataGridColumnInfo FieldName="Path" Caption="File" Hidden="true" />
					<GeneralTools:DataGridColumnInfo FieldName="File" Caption="File" Align="Center" />
					<GeneralTools:DataGridColumnInfo FieldName="Description" Caption="Description" Align="Center" />
					<GeneralTools:DataGridColumnInfo FieldName="Action" Sortable="false" CellWidth="50" Align="Center" FormattingMethod="ImageButton" />
				</Columns>
			</GeneralTools:DataGrid>
		</div>
	</div>
</asp:Panel>

<div class="clear"></div>
<div class="clearfix form-actions">
	<asp:Button ID="btnSave" Text="Register" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" />
	<asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" />
</div>
<div class="clear"></div>
<div id="saveResult" class="div-save-result"></div>
