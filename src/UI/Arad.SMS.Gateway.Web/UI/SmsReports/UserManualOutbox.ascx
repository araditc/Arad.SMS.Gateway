<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserManualOutbox.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsReports.UserManualOutbox" %>
<%@ Register Src="~/UI/Controls/DateTimePicker.ascx" TagPrefix="SMS" TagName="DateTimePicker" %>

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
								<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendDate")%> <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("From")%></label>
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

<GeneralTools:DataGrid runat="server" ID="gridUsersOutbox" DefaultSortField="SentDateTime" ListCaption="OutBoxList" ListHeight="420"
	ShowRowNumber="true" ShowSearchToolbar="true" ShowAdvancedSearch="true" SearchDivID="advanceSearchContainer">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="#" FieldName="SendStatus" Sortable="false" CellWidth="30" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="ID" FieldName="ID" Sortable="false" CellWidth="50" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="UserName" FieldName="UserName" Sortable="false" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="80" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="TypeSend" FieldName="SmsSendType" Sortable="false" SearchType="Select" Search="true" SearchOptions="{postData:{id:4},clearSearch:false,buildSelect:'createSelect'}" CellWidth="100" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="SmsBody" FieldName="SmsText" Search="true" Sortable="false" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="250" MaxLength="30" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="Price" FieldName="Price" CellWidth="50" FormattingMethod="NumberDecimal" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Recipients" FieldName="ReceiverCount" CellWidth="50" FormattingMethod="NumberDecimal" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="SenderNumber" FieldName="SenderId" Sortable="false" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="120" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="SendTime" FieldName="SentDateTime" Search="true" SearchType="Date" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" CellWidth="120" Align="Center" FormattingMethod="DateTimeShortDateTime" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="150" Align="Right" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function search(gridId) {
		if (!gridId)
			gridUsersOutbox.TriggerToolbar();

		var searchFilters = "";
		searchFilters += "[";
		searchFilters += "{'field':'SentDateTime','op':'ge','data':'" + getDateTimePickerValue("<%=dtpFromDateTime.ClientID%>") + "'},";
		searchFilters += "{'field':'SentDateTime','op':'le','data':'" + getDateTimePickerValue("<%=dtpToDateTime.ClientID%>") + "'}";
		searchFilters += "]";
		gridUsersOutbox.SearchFilters = searchFilters;
		gridUsersOutbox.Search();

		$("#advanceSearchContainer").modal('hide');
	}

	function confirmOutboxBulk(e) {
		gridUsersOutbox.Event = e;
		if (gridUsersOutbox.IsSelectedRow()) {
			var guid = gridUsersOutbox.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmBulkMessage")%>', '', 'confirm','warning', function (result) {
				if (result) {
					var ajaxResult = getAjaxResponse("ConfirmOutboxBulk", "Guid=" + guid);
					if (ajaxResult) {
						gridUsersOutbox.Search();
					}
				}
			});
		}
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

	function exportData(e) {
		gridUsersOutbox.Event = e;
		if (gridUsersOutbox.IsSelectedRow()) {
			var guid = gridUsersOutbox.SelectedGuid;
			var ajaxResult = getAjaxResponse("SetOutboxExportDataStatus", "Guid=" + guid);
			if (ajaxResult) {
				gridUsersOutbox.Search();
			}
		}
	}
</script>