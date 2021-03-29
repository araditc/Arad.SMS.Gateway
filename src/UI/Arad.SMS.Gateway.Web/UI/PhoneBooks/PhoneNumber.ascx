<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhoneNumber.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PhoneBooks.PhoneNumber" %>
<%@ Register Src="~/UI/Controls/BootstrapDateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="SMS" %>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:HiddenField ID="hdnNumberGuid" runat="server" />

<asp:Panel ID="pnlNumbers" runat="server">
	<GeneralTools:DataGrid runat="server" ID="gridNumbers" DefaultSortField="CreateDate" ListCaption="NumbersList" ListHeight="800" ShowSearchToolbar="true"
		ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true" SelectMode="Multiple">
		<Columns>
			<GeneralTools:DataGridColumnInfo Caption="FirstName" FieldName="FirstName" Search="true" SearchOptions="{sopt: ['cn','eq'],clearSearch:true}" CellWidth="90" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="LastName" FieldName="LastName" Search="true" SearchOptions="{sopt: ['cn','eq'],clearSearch:true}" CellWidth="110" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="Sex" FieldName="Sex" SearchType="Select" Search="true" SearchOptions="{postData:{id:7},clearSearch:false,buildSelect:'createSelect'}" CellWidth="50" Align="Center" FormattingMethod="CustomRender" />
			<GeneralTools:DataGridColumnInfo Caption="BirthDate" FieldName="BirthDate" Search="true" SearchType="Date" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" Align="Center" CellWidth="90" FormattingMethod="DateTimeShortDate" />
			<GeneralTools:DataGridColumnInfo Caption="CellPhone" FieldName="CellPhone" Search="true" SearchOptions="{sopt: ['cn','eq'],clearSearch:true}" Sortable="false" CellWidth="110" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Search="true" SearchType="Date" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" Align="Center" CellWidth="90" FormattingMethod="DateTimeShortDate" />
			<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="60" Align="Center" FormattingMethod="ImageButton" />
		</Columns>
	</GeneralTools:DataGrid>
</asp:Panel>

<div class="row" id="sendSms" style="padding: 30px; display: none;">
	<div class=" col-md-5 col-xs-5">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<div class="input-group">
					<span class="input-group-addon" style="background-color: rgb(80, 168, 226); color: #fff;"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sender") %></span>
					<asp:DropDownList ID="drpSenderNumber" runat="server" IsRequired="true" CssClass="form-control input-sm"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<GeneralTools:SmsBodyControl runat="server" ID="txtSmsBody" IsRequired="true"></GeneralTools:SmsBodyControl>
			</div>
			<div class="form-group">
				<label><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendToNumbers") %></label>
				<asp:TextBox ID="txtRecievers" runat="server" class="form-control" IsRequired="true" Height="100px" TextMode="MultiLine"></asp:TextBox>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendTime") %></label>
				<SMS:DateTimePicker ID="dtpSendDateTime" IsRequired="true" runat="server"></SMS:DateTimePicker>
			</div>
			<div class="buttonControlDiv">
				<asp:Button ID="btnSend" runat="server" CssClass="btn btn-primary" OnClick="btnSend_Click" Text="Register" />
				<input id="btnCancelSave" type="button" class="btn btn-default" value='<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %>' onclick="cancelSave();" />
			</div>
		</div>
	</div>
</div>

<div class="col-md-6">
	<div class="modal fade" id="divTransferNumber" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title" id="myModalLabel"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NumberTransfer") %></h4>
				</div>
				<div class="modal-body">
					<div class="form-horizontal" role="form">
						<div class="form-group">
							<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Group") %></label>
							<div class="col-sm-9">
								<asp:DropDownList ID="drpGroup" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
							</div>
						</div>
					</div>
				</div>
				<div class="modal-footer">
					<input id="btnSave" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Register") %>" class="btn btn-success" onclick="saveTransferNumber();" />
				</div>
			</div>
		</div>
	</div>
</div>

<script type="text/javascript">
	function search(gridId) {
		gridNumbers.Search();
	}

	function createSelect(element) {
		var data = element.d;
		var s = '<select>';
		if (data.length) {
			s += '<option value="0"></option>';
			for (var i = 0, l = data.length; i < l ; i++) {
				s += '<option value="' + data[i].Value + '">' + data[i].Text + '</option>';
			}
		}
		return s += "</select>";
	}

	function deleteNumber(e) {
		gridNumbers.Event = e;
		if (!gridNumbers.IsSelectedRow())
			return;

		var guid = gridNumbers.SelectedGuid;
		messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm','danger', function (result) {
			if (result) {
				var isDelete = getAjaxResponse("DeleteNumber", "NumberGuid=" + guid);
				if (isDelete) {
					gridNumbers.Search();
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeleteRecord")%>','','alert','success');
				}
				else
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
			}
		});
	}

	function addNumberToSendList() {
		var data = gridNumbers.GetSelectedRowData(["CellPhone"]);
		var count = importData(data, "resultCount");
		if (count == 0) {
            messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelectNumbers") %>', '', 'alert', 'danger');
			return false;
		}

		var numbers = '';
		for (i = 0; i < count; i++) {
			numbers += '\r\n';
			numbers += importData(data, "CellPhone" + i);
		}

		$("#<%=txtRecievers.ClientID%>")[0].value += numbers;
        messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Count") %> ' + count + '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NumberAdded") %>', '', 'alert', 'success');
	}

	function sendSms() {
		$("#sendSms").show();
		$("#<%=pnlNumbers.ClientID%>").hide();
	}

	function cancelSave() {
		$("#sendSms").hide();
		$("#<%=pnlNumbers.ClientID%>").show();
	}

	function sendSmsError(msg) {
		$("#sendSms").show();
		$("#<%=pnlNumbers.ClientID%>").hide();
		messageBox(msg, '', 'alert', 'danger');
	}

	function deleteMultipleNumber() {
		var data = gridNumbers.GetSelectedRowData(["Guid"]);
		messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
			if (result) {
				var isDelete = getAjaxResponse("DeleteMultipleNumber", "Data=" + data);
				if (isDelete) {
					gridNumbers.Search();
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeleteRecord")%>','','alert','success');
				}
				else
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
			}
		});
	}

	function transferNumber(e) {
		gridNumbers.Event = e;
		if (!gridNumbers.IsSelectedRow())
			return;

		var guid = gridNumbers.SelectedGuid;
		$("#<%=hdnNumberGuid.ClientID%>")[0].value = guid;

		$("#divTransferNumber").modal('show');
	}

	function saveTransferNumber() {
		var guid = $("#<%=hdnNumberGuid.ClientID%>")[0].value;
		getAjaxResponse("TransferNumber", "NumberGuid=" + guid + "&GroupGuid=" + $("#<%=drpGroup.ClientID%>")[0].value);
		gridNumbers.Search();
		$("#divTransferNumber").modal('hide');
	}
</script>
