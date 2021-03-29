<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GalleryImage.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.GalleryImages.GalleryImage" %>

<GeneralTools:DataGrid runat="server" ID="gridGalleryImages" DefaultSortField="CreateDate" ListCaption="GalleryImagesList" ListHeight="420" 
	ShowToolbar="true" ToolbarPosition="Top" ShowRowNumber="true">
	<Columns>
		<GeneralTools:DataGridColumnInfo Caption="Title" FieldName="Title" Align="Center"/>
		<GeneralTools:DataGridColumnInfo Caption="Presentable" FieldName="IsActive" CellWidth="110" Align="Center" FormattingMethod="BooleanOnOff"/>
		<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" CellWidth="110" Align="Center" FormattingMethod="DateTimeShortDate"/>
		<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="250" Align="Center" FormattingMethod="ImageButton"/>
	</Columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function deleteRow(e) {
		gridGalleryImages.Event = e;
		if (gridGalleryImages.IsSelectedRow()) {
			var guid = gridGalleryImages.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteGalleryImage", "Guid=" + guid);
					if (isDelete) {
						gridGalleryImages.Search();
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}

	function activeGalleryImage(e) {
		gridGalleryImages.Event = e;
		if (gridGalleryImages.IsSelectedRow()) {
			var guid = gridGalleryImages.SelectedGuid;
			var result = getAjaxResponse("ActiveGalleryImage", "Guid=" + guid);
			if (result)
				gridGalleryImages.Search();
			else
				messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
		}
	}
</script>
