<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveFileNumber.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PhoneBooks.SaveFileNumber" %>
<script src="/script/jquery.ajaxupload.js"></script>
<script type="text/javascript">
	$(document).ready(function () {
		$.ajaxUploadSettings.name = 'upload';
		$('#fileUpload').ajaxUploadPrompt({
			url: '/handler/UploadAndGetFileInfo.ashx',
			beforeSend: function () { },
			onprogress: function (e) { },
			error: function () {
				alert("Error");
			},
			success: function (data) {
				if (importData(data, "Result") == "OK") {
					$("#<%=hdnFilePath.ClientID%>")[0].value = importData(data, "Path");
				}
				else {
					$("#divResult").removeClass();
					$("#divResult").addClass("bg-danger");
					$("#divResult").html(importData(data, "Message"));
				}
			}
		});
	});
</script>
<asp:HiddenField ID="hdnFilePath" runat="server" />

<div class="row">
	<div class="col-md-12 col-xs-12">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelectFile")%></label>
				<div class="col-sm-4">
					<label class="ace-file-input">
						<span id="fileUpload" class="ace-file-container" data-title="Choose"><span class="ace-file-name" data-title="No File ..."><i class=" ace-icon fa fa-upload"></i></span></span><a class="remove" href="#"><i class=" ace-icon fa fa-times"></i></a>
					</label>
					<asp:Button ID="btnImportFile" runat="server" CssClass="btn btn-primary" OnClick="btnImportFile_Click" Text="ReadFile" Style="border: 0;" />
				</div>
			</div>
			<div class="form-group">
				<div style="margin-right: 180px;">
					<label class="block">
						<input name="form-field-checkbox" type="checkbox" runat="server" class="ace" id="chbHeaderRow">
						<span class="lbl"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FirstRowHasColumnNames")%></span>
					</label>
				</div>
			</div>
			<div>
				<asp:GridView ID="gridFile" runat="server" RowStyle-CssClass="gridRow" AlternatingRowStyle-CssClass="gridRowAlternatingRow" Height="100px" Width="100%" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
					<AlternatingRowStyle CssClass="gridRowAlternatingRow" />
					<FooterStyle BackColor="#CCCCCC" />
					<HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
					<PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
					<RowStyle CssClass="gridRow" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="White" />
					<SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
					<SortedAscendingCellStyle BackColor="#F1F1F1" />
					<SortedAscendingHeaderStyle BackColor="#808080" />
					<SortedDescendingCellStyle BackColor="#CAC9C9" />
					<SortedDescendingHeaderStyle BackColor="#383838" />
				</asp:GridView>
			</div>
		</div>
	</div>
	<div class="clear"></div>
	<hr />
	<div class="col-md-4 col-xs-6">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FirstName") %><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FromColumn") %></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtFirstName" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("LastName") %><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FromColumn") %></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtLastName" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("BirthDate") %><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FromColumn") %></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtBirthDate" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sex") %><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FromColumn") %></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtSex" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CellPhone") %><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FromColumn") %></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtCellPhone" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Email") %><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FromColumn") %></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtEmail" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Job") %><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FromColumn") %></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtJob" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Telephone") %><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FromColumn") %></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtTelephone" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FaxNumber") %><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FromColumn") %></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtFaxNumber" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Address") %><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FromColumn") %></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtAddress" class="form-control input-sm numberInput" runat="server"></asp:TextBox>
				</div>
			</div>
<%--			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConditionsRegisterNumber") %></label>
				<div class="col-sm-6">
					<asp:DropDownList ID="drpCheckNumberScope" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>--%>
			<div class="buttonControlDiv">
				<asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Register" />
				<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
			</div>
		</div>
	</div>
	<div class="col-md-4 col-xs-6">
		<asp:Panel ID="pnlUserField" runat="server" class="form-horizontal" role="form"></asp:Panel>
	</div>
	<div class="clear"></div>
	<div id="divResult" class="div-save-result" style="margin-right: 20px;"></div>
</div>

<script type="text/javascript">
	function saveResult(resultType, message) {
		$("#divResult").removeClass();
		switch (resultType) {
			case 'Error':
				$("#divResult").addClass("bg-danger div-save-result");
				$("#divResult").html("<span class='fa fa-times-circle fa-2x' style='color:red'></span>" + message);
				break;
			case 'OK':
				$("#divResult").addClass(" bg-success div-save-result");
				$("#divResult").html("<span class='fa fa-check-square fa-2x' style='color:#5cb85c'></span>" + message);
				break;
		}
	}
</script>
