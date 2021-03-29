<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserScheduledSms.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsReports.UserScheduledSms" %>

<GeneralTools:DataGrid runat="server" ID="gridUserScheduledSms" DefaultSortField="DateTimeFuture" ListCaption="ListSmsesInQueue"
	ListHeight="420" ShowRowNumber="true" ShowSearchToolbar="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="#" FieldName="Status" Sortable="false" CellWidth="30" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="#" FieldName="ID" CellWidth="50" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="UserName" FieldName="UserName" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="80" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="SmsBody" FieldName="SmsText" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="200" MaxLength="30" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="TypeSend" FieldName="TypeSend" SearchType="Select" Search="true" SearchOptions="{postData:{id:4},clearSearch:false,buildSelect:'createSelect'}" CellWidth="100" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="SenderNumber" FieldName="Number" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="100" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="SendTime" FieldName="DateTimeFuture" Search="true" SearchType="Date" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" CellWidth="100" Align="Center" FormattingMethod="DateTimeShortDateTime" />
		<GeneralTools:DataGridColumnInfo Caption="Description" FieldName="SmsSendFaildType" CellWidth="70" MaxLength="30" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="80" Align="Right" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function search(gridId) {
		gridUserScheduledSms.Search();
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

	function confirmBulk(e) {
		debugger;
		gridUserScheduledSms.Event = e;
		if (gridUserScheduledSms.IsSelectedRow()) {
			var guid = gridUserScheduledSms.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmBulkMessage")%>', '', 'confirm','warning', function (result) {
				debugger;
				if (result) {
					var ajaxResult = getAjaxResponse("ConfirmBulk", "Guid=" + guid);
					if (ajaxResult) {
						gridUserScheduledSms.Search();
					}
				}
			});
		}
	}

	function rejectBulk(e) {
		gridUserScheduledSms.Event = e;
		if (gridUserScheduledSms.IsSelectedRow()) {
			var guid = gridUserScheduledSms.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RejectBulkMessage")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					var ajaxResult = getAjaxResponse("RejectBulk", "Guid=" + guid);
					if (ajaxResult) {
						gridUserScheduledSms.Search();
					}
				}
			});
		}
	}

	function resend(e) {
		gridUserScheduledSms.Event = e;
		if (gridUserScheduledSms.IsSelectedRow()) {
			var guid = gridUserScheduledSms.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmResendSms")%>', '', 'confirm', 'warning', function (result) {
				if (result) {
					$.messager.progress();
					var ajaxResult = getAjaxResponse("ResendSms", "Guid=" + guid);
					if (ajaxResult) {
						gridUserScheduledSms.Search();
					}
					$.messager.progress('close');
				}
			});
		}
	}

	function deleteRow(e) {
		gridUserScheduledSms.Event = e;
		if (gridUserScheduledSms.IsSelectedRow()) {
			var guid = gridUserScheduledSms.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmRejectSms")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					var ajaxResult = getAjaxResponse("RejectSms", "Guid=" + guid);
					if (ajaxResult) {
						gridUserScheduledSms.Search();
					}
				}
			});
		}
	}
</script>
