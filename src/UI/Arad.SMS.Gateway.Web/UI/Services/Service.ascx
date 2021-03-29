<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Service.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Services.Service" %>

<GeneralTools:DataGrid runat="server" ShowAdvancedSearch="false" ID="gridServices" DefaultSortField="CreateDate" ListCaption="ServicesList" ListHeight="800"
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true" ShowSearchToolbar="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="ViewTheMenu" FieldName="MenuTitle" SearchType="Select" Search="true" SearchOptions="{postData:{id:5},clearSearch:false,buildSelect:'createSelect'}" CellWidth="80" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Icon" ShowInExport="false" Sortable="false" FieldName="IconAddress" CellWidth="20" Align="Center" FormattingMethod="Image" />
		<GeneralTools:DataGridColumnInfo Caption="ServiceName" FieldName="Title" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}"  CellWidth="100" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Order" FieldName="Order" CellWidth="20" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Presentable" FieldName="Presentable" CellWidth="35" Align="Center" FormattingMethod="BooleanOnOff" />
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" CellWidth="50" Align="Center" FormattingMethod="DateTimeShortDate" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="50" Align="Center" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function deleteRow(e) {
		gridServices.Event = e;
		if (gridServices.IsSelectedRow()) {
			var guid = gridServices.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm','danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteService", "Guid=" + guid);
					if (isDelete) {
						gridServices.Search();
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
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

	function search(gridId) {
		gridServices.Search();
	}
</script>



