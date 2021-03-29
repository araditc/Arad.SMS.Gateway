<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScheduledSms.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsReports.ScheduledSms" %>

<GeneralTools:DataGrid runat="server" ID="gridScheduledSms" DefaultSortField="DateTimeFuture" ListCaption="ListSmsesInQueue" ListHeight="420"
	ShowRowNumber="true" ShowSearchToolbar="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="#" FieldName="Status" Sortable="false" CellWidth="30" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="Id" FieldName="ID" CellWidth="50" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="SmsBody" FieldName="SmsText" Sortable="false" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="200" MaxLength="30" Frozen="true" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="TypeSend" FieldName="TypeSend" Sortable="false" SearchType="Select" Search="true" SearchOptions="{postData:{id:4},clearSearch:false,buildSelect:'createSelect'}" CellWidth="100" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="SenderNumber" FieldName="Number" Sortable="false" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="100" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="SendTime" FieldName="DateTimeFuture" Search="true" SearchType="Date" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" CellWidth="100" Align="Center" FormattingMethod="DateTimeShortDateTime" />
		<GeneralTools:DataGridColumnInfo Caption="Description" FieldName="SmsSendFaildType" Sortable="false" CellWidth="50" MaxLength="30" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="100" Align="Center" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function search(gridId) {
		gridScheduledSms.Search();
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

	function resend(e) {
		gridScheduledSms.Event = e;
		if (gridScheduledSms.IsSelectedRow()) {
			var guid = gridScheduledSms.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmResendSms")%>', '', 'confirm', 'warning', function (result) {
				if (result) {
					$.messager.progress();
					var ajaxResult = getAjaxResponse("ResendSms", "Guid=" + guid);
					if (ajaxResult) {
						gridScheduledSms.Search();
					}
					$.messager.progress('close');
				}
			});
		}
	}

	function deleteRow(e) {
		gridScheduledSms.Event = e;
		if (gridScheduledSms.IsSelectedRow()) {
			var guid = gridScheduledSms.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmRejectSms")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					var ajaxResult = getAjaxResponse("RejectSms", "Guid=" + guid);
					if (ajaxResult) {
						gridScheduledSms.Search();
					}
				}
			});
		}
	}
</script>
