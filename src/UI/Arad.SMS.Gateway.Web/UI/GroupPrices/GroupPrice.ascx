<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupPrice.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.GroupPrices.GroupPrice" %>

<generaltools:datagrid runat="server" id="gridGroupPrice" defaultsortfield="MinimumMessage" listcaption="GroupPriceList" listheight="420"
	showtoolbar="true" toolbarposition="Top" showrownumber="true"
	GroupField="IsPrivate" GroupCollapse="false" GroupColumnShow="true" EnableGrouping="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Private" FieldName="IsPrivate"  CellWidth="80" Align="Center" FormattingMethod="BooleanOnOff"/>
		<GeneralTools:DataGridColumnInfo Caption="TheDefault" FieldName="IsDefault" CellWidth="80" Sortable="false" Align="Center" FormattingMethod="BooleanOnOff"/>
		<GeneralTools:DataGridColumnInfo Caption="Tax" FieldName="DecreaseTax" CellWidth="80" Sortable="false" Align="Center" FormattingMethod="BooleanOnOff"/>
		<GeneralTools:DataGridColumnInfo Caption="Title" FieldName="Title" CellWidth="150" Sortable="false" Align="Center" Frozen="true"/>
		<GeneralTools:DataGridColumnInfo Caption="MinimumMessage" FieldName="MinimumMessage" Sortable="false" CellWidth="100" Align="Center" Frozen="true" FormattingMethod="NumberDecimal"/>
		<GeneralTools:DataGridColumnInfo Caption="MaximumMessage" FieldName="MaximumMessage" Sortable="false" CellWidth="100" Align="Center" Frozen="true" FormattingMethod="NumberDecimal"/>
		<GeneralTools:DataGridColumnInfo Caption="BaseSmsPrice" FieldName="BasePrice" Sortable="false" CellWidth="100" FormattingMethod="NumberDecimal" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Sortable="false" Align="Center" CellWidth="110" FormattingMethod="DateTimeShortDate"/>
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="300" Align="Center" FormattingMethod="ImageButton"/>
	</columns>
</generaltools:datagrid>

<script type="text/javascript">
	function refreshGrid(result) {
		if (result == "true") {
			gridGroupPrice.Search();
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("InsertRecord")%>', '', 'alert', 'success');
		}
	}

	function deleteRow(e) {
		gridGroupPrice.Event = e;
		if (!gridGroupPrice.IsSelectedRow())
			return;

		var guid = gridGroupPrice.SelectedGuid;
		messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete") %>', '', 'Confirm', 'danger', function (result) {
			if (result) {
				var isDelete = getAjaxResponse("DeleteGroupPrice", "Guid=" + guid);
				if (isDelete) {
					gridGroupPrice.Search();
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeleteRecord")%>', '', 'alert', 'success');
				}
				else
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
			}
		});
	}
</script>
