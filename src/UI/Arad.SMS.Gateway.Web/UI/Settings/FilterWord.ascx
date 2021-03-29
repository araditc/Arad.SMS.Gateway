<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FilterWord.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Settings.FilterWord" %>

<GeneralTools:DataGrid runat="server" ID="gridFilterWord" ListCaption="FilterWordsList" ListHeight="420"
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Title" FieldName="Title" Search="true" CellWidth="150" Align="Center" FormattingMethod="CustomRender" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="20" Align="Center" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>


<script type="text/javascript">
	function deleteRow(e) {
		gridFilterWord.Event = e;
		if (gridFilterWord.IsSelectedRow()) {
			var guid = gridFilterWord.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteFilterWord", "Guid=" + guid);
					if (isDelete) {
						gridFilterWord.Search();
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}

	function saveFilterWord() {
		var word = $("#txtWord")[0].value;
		var result = getAjaxResponse("SaveFilterWord", "Word=" + word);
		if (result) {
			gridFilterWord.Search();
		}
		else {
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
		}
	}
</script>
