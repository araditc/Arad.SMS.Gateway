<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserOutbox.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsReports.UserOutbox" %>
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

<GeneralTools:datagrid runat="server" id="gridUsersOutbox" defaultsortfield="SentDateTime" listcaption="OutBoxList" listheight="420"
	ShowFooterRow="true" showrownumber="true" showsearchtoolbar="true" ShowAdvancedSearch="true" SearchDivID="advanceSearchContainer"
	ShowToolbar="true" ToolbarPosition="Top" GridComplete="gridComplete">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="#" FieldName="SendStatus" Sortable="false" SearchType="Select" Search="true" SearchOptions="{postData:{id:8},clearSearch:false,buildSelect:'createSendStatusSelect'}" CellWidth="50" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="ID" FieldName="ID" Sortable="false" Search="true" SearchOptions="{sopt: ['eq'],clearSearch:true}" CellWidth="80" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="UserName" FieldName="UserName" Sortable="false" Search="true" SearchOptions="{sopt: ['cn','eq'],clearSearch:true}" CellWidth="80" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="TypeSend" FieldName="SmsSendType" Sortable="false" SearchType="Select" Search="true" SearchOptions="{postData:{id:4},clearSearch:false,buildSelect:'createSelect'}" CellWidth="70" MaxLength="10" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="SmsBody" FieldName="SmsText" Search="true" Sortable="false" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="200" MaxLength="25"  Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="Price" FieldName="Price" CellWidth="110" FormattingMethod="NumberDecimal" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="SmsLen" FieldName="SmsLen" CellWidth="70" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Recipients" FieldName="ReceiverCount" CellWidth="100" FormattingMethod="NumberDecimal" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="SenderNumber" FieldName="SenderId" Sortable="false" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="100" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="SendTime" FieldName="SentDateTime" Search="true" SearchType="Date" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" CellWidth="130" Align="Center" FormattingMethod="DateTimeShortDateTime" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="120" Align="Right" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:datagrid>

<script type="text/javascript">
	$(function () {
		$.ajax({
			url: "DataGridHandler.aspx/GetUserDomains",
			contentType: 'application/json; charset=utf-8',
			dataType: "json",
			type: 'POST',
			data: "{userGuid:'<%=UserGuid%>'}",
			async: false,
			cache: false
		}).done(function (data) {
			var options = "<option value=''></option>";
			$.each(data.d, function (i) {
				options += '<option value="' + data.d[i].guid + '">' + data.d[i].name + '</option>';
			});

			$("#drpDomain").append(options);
		});

		$("#btnSearch").click(function (e) {
			search();
		});
	});

	function gridComplete() {
		var data = gridUsersOutbox.GetUserData();
		
		gridUsersOutbox.SetFooterRowData(data[0], false);
		
		var control = '<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Home, Session)%>';
		var address = "/PageLoader.aspx?c=" + control + "&DeliveredCount=" + data[1].SentAndReceivedbyPhone.Count + "&DeliveredSmsCount=" + data[1].SentAndReceivedbyPhone.SmsCount + "&FailedCount=" + data[1].NotSent.Count + "&FailedSmsCount=" + data[1].NotSent.SmsCount + "&SentToICTCount=" + data[1].SentToItc.Count + "&SentToICTSmsCount=" + data[1].SentToItc.SmsCount + "&ReceivedByItcCount=" + data[1].ReceivedByItc.Count + "&ReceivedByItcSmsCount=" + data[1].ReceivedByItc.SmsCount + "&BlackListCount=" + data[1].BlackList.Count + "&BlackListSmsCount=" + data[1].BlackList.SmsCount + "&UsersOutbox=1";
		$("#btnChart").click(function (e) {
			$("#btnChart").attr("href", address);
		});
	}

	function search(gridId) {
		if (!gridId)
			gridUsersOutbox.TriggerToolbar();

		var domainGuid = $('#drpDomain option:selected')[0].value;

		var searchFilters = "";
		searchFilters += "[";
		searchFilters += "{'field':'SentDateTime','op':'ge','data':'" + getDateTimePickerValue("<%=dtpFromDateTime.ClientID%>") + "'},";
		searchFilters += "{'field':'SentDateTime','op':'le','data':'" + getDateTimePickerValue("<%=dtpToDateTime.ClientID%>") + "'},";
		searchFilters += "{'field':'DomainGuid','op':'eq','data':'" + domainGuid + "'}";
		searchFilters += "]";
		gridUsersOutbox.SearchFilters = searchFilters;
		gridUsersOutbox.Search();

		$("#advanceSearchContainer").modal('hide');
	}

	function confirmOutboxBulk(e) {
		gridUsersOutbox.Event = e;
		if (gridUsersOutbox.IsSelectedRow()) {
			var guid = gridUsersOutbox.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmBulkMessage")%>', '', 'confirm', 'warning', function (result) {
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

	function createSendStatusSelect(element) {
		var data = element.d;
		var s = '<select>';
		if (data.length) {
			s += '<option value="0"></option>';
			for (var i = 0, l = data.length; i < l ; i++) {
				s += '<option value="' + data[i].Value + '">' + data[i].Text + '</option>';
			}
		}
		s += "</select>";
		return s;
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

	function exportTxt(e) {
		gridUsersOutbox.Event = e;
		if (gridUsersOutbox.IsSelectedRow()) {
			var guid = gridUsersOutbox.SelectedGuid;
			var ajaxResult = getAjaxResponse("SetOutboxExportTxtStatus", "Guid=" + guid);
			if (ajaxResult) {
				gridUsersOutbox.Search();
			}
		}
	}
</script>
