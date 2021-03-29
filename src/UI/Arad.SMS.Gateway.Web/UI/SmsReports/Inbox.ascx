<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inbox.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsReports.Inbox" %>
<%--<%@ Register Src="~/UI/Controls/DateTimePicker.ascx" TagPrefix="SMS" TagName="DateTimePicker" %>--%>
<%@ Register Src="~/UI/Controls/BootstrapDateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="SMS" %>
<div id="advanceSearchContainer" class="modal fade" role="dialog" aria-labelledby="gridSystemModalLabel">
	<div class="modal-dialog " role="document">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
				<h4 class="modal-title" id="gridSystemModalLabel"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AdvancedSearch") %></h4>
			</div>
			<div class="modal-body">
				<div class="form-horizontal" role="form">
					<div class="row">
						<div class="col-md-12 col-sm-12">
							<div class="form-group">
								<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ReceivedDate")%> <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("From")%></label>
								<div class="col-sm-7">
									<SMS:DateTimePicker runat="server" ID="dtpFromDateTime" />
								</div>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-12 col-sm-12">
							<div class="form-group">
								<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UpTo")%></label>
								<div class="col-sm-7">
									<SMS:DateTimePicker runat="server" ID="dtpToDateTime" />
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-primary" onclick="search();"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Search") %></button>
				<button type="button" class="btn btn-default" data-dismiss="modal"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %></button>
			</div>
		</div>
	</div>
</div>

<GeneralTools:DataGrid runat="server" ShowAdvancedSearch="true" ID="gridInbox" DefaultSortField="ReceiveDateTime" ListCaption="SmsRecievedList" ListHeight="420"
	 ShowRowNumber="true" ShowSearchToolbar="true" ShowToolbar="true" ToolbarPosition="Top" ShowPagerToTop="true" SelectMode="Multiple" SearchDivID="advanceSearchContainer">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="SmsBody" Sortable="false" FieldName="SmsText" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="200" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="Sender" Sortable="false" FieldName="Sender" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="100" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="Receiver" Sortable="false" FieldName="Receiver" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="100" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="ReceivedDate" FieldName="ReceiveDateTime" Search="true" SearchType="Date" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" CellWidth="100" Align="Center" FormattingMethod="DateTimeShortDateTime"/>
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="50" Align="Center" FormattingMethod="ImageButton"/>
	</columns>
</GeneralTools:DataGrid>
<asp:HiddenField ID="hdnInboxGuid" runat="server"></asp:HiddenField>

<div class="col-md-6">
	<div class="modal fade" id="divSendToCategory" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title" id="myModalLabel"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendToCategory") %></h4>
				</div>
				<div class="modal-body">
					<div class="form-horizontal" role="form">
						<div class="form-group">
							<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Group") %></label>
							<div class="col-sm-10">
								<asp:DropDownList ID="drpInboxGroups" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
							</div>
						</div>
					</div>
				</div>
				<div class="modal-footer">
					<input id="btnChangeInboxGroup" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Register") %>" class="btn btn-success" onclick="changeInboxGroup();" />
					<input id="btnCancelChangeInboxGroup" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %>" class="btn btn-default" data-dismiss="modal" />
				</div>
			</div>
		</div>
	</div>
</div>

<script type="text/javascript">
	function search(gridId) {
		if (!gridId)
			gridInbox.TriggerToolbar();

		var searchFilters = "";
		searchFilters += "[";
        searchFilters += "{'field':'ReceiveDateTime','op':'ge','data':'" + $("#" + "<%=dtpFromDateTime.ClientID%>" + "_txtDate").getDateTimePickerValue() + "'},";
        searchFilters += "{'field':'ReceiveDateTime','op':'le','data':'" + $("#" + "<%=dtpToDateTime.ClientID%>" + "_txtDate").getDateTimePickerValue() + "'}";
		searchFilters += "]";
		gridInbox.SearchFilters = searchFilters;
		gridInbox.Search();

		$("#advanceSearchContainer").modal('hide');
	}

	function sendToCategory(e) {
		gridInbox.Event = e;
		if (!gridInbox.IsSelectedRow())
			return;

		var guid = gridInbox.SelectedGuid;
		$("#<%=hdnInboxGuid.ClientID %>")[0].value = guid;
		var groupGuid = getAjaxResponse("GetInboxGroupOfSms", "RecieveSmsGuid=" + guid);
		$("#<%=drpInboxGroups.ClientID %>")[0].value = groupGuid;
		$('#divSendToCategory').modal('show');
	}

	function changeInboxGroup() {
		var inboxGuid = $("#<%=hdnInboxGuid.ClientID %>")[0].value;
		var inboxGroupGuid = $("#<%=drpInboxGroups.ClientID %>")[0].value;
		var result = getAjaxResponse("ChangeInboxGroup", "InboxGuid=" + inboxGuid + "&InboxGroupGuid=" + inboxGroupGuid);
		if (result) {
			$('#divSendToCategory').modal('hide');
			gridInbox.Search();
		}
		else
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord") %>', '', 'alert', 'danger');
	}

	function deleteRow(e) {
		gridInbox.Event = e;
		if (gridInbox.IsSelectedRow()) {
			var guid = gridInbox.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteInboxSms", "Guid=" + guid);
					if (isDelete) {
						gridInbox.Search();
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}

	function deleteRows() {
		var data = gridInbox.GetSelectedRowData(["Guid"]);
		messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
			if (result) {
				var isDelete = getAjaxResponse("DeleteMultipleReceivedSms", "Data=" + data);
				if (isDelete) {
					gridInbox.Search();
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeleteRecord")%>', '', 'alert', 'success');
				}
				else
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
			}
		});
	}
</script>
