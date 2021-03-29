<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Image.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Images.Image" %>

<GeneralTools:DataGrid runat="server" ID="gridImages" DefaultSortField="CreateDate" ListCaption="ImagesList" ListHeight="420"
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Title" FieldName="Title" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="Presentable" FieldName="IsActive" CellWidth="110" Align="Center" FormattingMethod="BooleanOnOff"/>
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" CellWidth="110" Align="Center" FormattingMethod="DateTimeShortDate"/>
		<GeneralTools:DataGridColumnInfo Caption="ImagePath" FieldName="ImagePath" Align="Center" Hidden="true" />
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="250" Align="Center" FormattingMethod="ImageButton"/>
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function deleteRow(e) {
		gridImages.Event = e;
		if (gridImages.IsSelectedRow()) {
			var guid = gridImages.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteImage", "Guid=" + guid);
					if (isDelete) {
						gridImages.Search();
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}

	function showImage(e) {
		gridImages.Event = e;
		if (!gridImages.IsSelectedRow())
			return;
		var imagePath = '/Gallery/<%=Arad.SMS.Gateway.GeneralLibrary.Helper.GetHostOfDomain(Request.Url.Authority)%>/' + gridImages.GetSelectedRowFieldValue('ImagePath');
		window.open(imagePath, '_blank');
		window.focus();
	}

	function activeImage(e) {
		gridImages.Event = e;
		if (gridImages.IsSelectedRow()) {
			var guid = gridImages.SelectedGuid;
			var result = getAjaxResponse("ActiveImage", "Guid=" + guid);
			if (result)
				gridImages.Search();
			else
				messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
		}
	}
</script>
