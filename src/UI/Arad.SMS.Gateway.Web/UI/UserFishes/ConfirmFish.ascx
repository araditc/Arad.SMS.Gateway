<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfirmFish.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.UserFishes.ConfirmFish" %>

<GeneralTools:DataGrid runat="server" ShowAdvancedSearch="false" ID="gridUserFishes" DefaultSortField="CreateDate" ListCaption="UserFishesList"
	ShowToolbar="false" ShowRowNumber="true" ListHeight="420" ShowFooterRow="true"
	ShowSearchToolbar="true" GridComplete="gridComplete">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Bank" FieldName="Bank" ShowInExport="false" CellWidth="30" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="Status" FieldName="Status" ShowInExport="false" SearchType="Select" Search="true" SearchOptions="{postData:{id:3},clearSearch:false,buildSelect:'createSelect'}" CellWidth="60" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="UserName" FieldName="UserName" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="70" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Search="true" SearchType="Date" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" CellWidth="80" Align="Center" Frozen="true" FormattingMethod="DateTimeShortDate" />
		<GeneralTools:DataGridColumnInfo Caption="PaymentDate" FieldName="PaymentDate" Search="true" SearchType="Date" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" CellWidth="80" Align="Center" Frozen="true" FormattingMethod="DateTimeShortDate" />
		<GeneralTools:DataGridColumnInfo Caption="SmsCount" FieldName="SmsCount" CellWidth="80" Align="Center" FormattingMethod="NumberDecimal" />
		<GeneralTools:DataGridColumnInfo Caption="Amount" FieldName="Amount" CellWidth="80" Align="Center" FormattingMethod="NumberDecimal" />
		<GeneralTools:DataGridColumnInfo Caption="Payment" FieldName="Type" CellWidth="50" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="BillNumber" FieldName="BillNumber" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" SearchFilterCssClass="numberInput" CellWidth="90" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Description" FieldName="Description" CellWidth="100" MaxLength="20" Align="Center" Frozen="true" />
		<GeneralTools:DataGridColumnInfo Caption="Account" FieldName="AccountNo" CellWidth="70" MaxLength="30" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="50" Align="Center" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function search(gridId) {
		gridUserFishes.Search();
	}

	function gridComplete() {
		var data = gridUserFishes.GetUserData();

		gridUserFishes.SetFooterRowData(data[0], false);
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

	function confirmFish(e) {
		gridUserFishes.Event = e;
		if (!gridUserFishes.IsSelectedRow())
			return;

		var guid = gridUserFishes.SelectedGuid;
		var retVal = getAjaxResponse("ConfirmFish", "Guid=" + guid);
		var result = importData(retVal, "Result");
		if (result == "OK") {
			gridUserFishes.Search();
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("InsertRecord")%>', '', 'alert', 'success');
		}
		else
			messageBox(importData(retVal, "Message"), '', 'alert', 'danger');
	}

	function rejectFish(e) {
		gridUserFishes.Event = e;
		if (!gridUserFishes.IsSelectedRow())
			return;

		var guid = gridUserFishes.SelectedGuid;
		var result = getAjaxResponse("RejectFish", "Guid=" + guid);
		if (result) {
			gridUserFishes.Search();
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("InsertRecord")%>', '', 'alert', 'success');
		}
		else
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
	}
</script>
