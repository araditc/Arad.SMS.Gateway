<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuyService.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.UserServices.BuyService" %>

<asp:HiddenField ID="hdnUserGuid" runat="server" />
<script type="text/javascript">
	$(document).ready(function () {
		$('#divRoleGeneralPhoneBook').hide();
		$("#radios").buttonset();
		$("#radios #radioDetermineRoleService").click(function () {
			$('#divRoleService').show();
			$('#divRoleGeneralPhoneBook').hide();
		});
		$("#radios #radioDetermineRoleGeneralPhoneBook").click(function () {
			$('#divRoleService').hide();
			$('#divRoleGeneralPhoneBook').show();
		});
	});
	</script>

<div id="radios">
	<input type="radio" id="radioDetermineRoleService" name="radio" checked="checked" /><label for="radioDetermineRoleService"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Services")%></label>
	<input type="radio" id="radioDetermineRoleGeneralPhoneBook" name="radio" /><label for="radioDetermineRoleGeneralPhoneBook"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GeneralPhoneBooks")%></label>
</div>
<div style="width:400px;">
	<div id="divRoleService" style="padding-top:10px;">
		<GeneralTools:DataGrid runat="server" showAdvancedSearch="true" ID="gridServices" DefaultSortField="Title" ListCaption="ActivationService" ListHeight="420" 
			DisableNavigationBar="true" ShowRowNumber="true" SelectMode="Multiple" EnableGrouping="true" 
				StatusMultiSelectField="IsActive" DisableActiveMultiSelectField="true" DisableMultiSelectField="IsActive" GroupField="GroupTitle" GroupCollapse="false" GroupColumnShow="false">
			<Columns>
				<GeneralTools:DataGridColumnInfo Caption="IsActive" FieldName="IsActive" Hidden="true" Sortable="false" ShowInExport="false" Align="Center"/>
				<GeneralTools:DataGridColumnInfo Caption="ServiceName" FieldName="Title" Sortable="false" CellWidth="250" Align="Center" EditAble="false"/>
				<GeneralTools:DataGridColumnInfo Caption="GroupTitle" FieldName="GroupTitle" Hidden="true" Sortable="false" ShowInExport="false" Align="Center"/>
				<GeneralTools:DataGridColumnInfo Caption="Price" FieldName="Price" Sortable="false" ShowInExport="false" CellWidth="100" Align="Center" FormattingMethod="CustomRender"/>
			</Columns>
		</GeneralTools:DataGrid>
		<div class="buttonControlDiv">
			<input id="btnSaveUserService" type="button" class="button" value='<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Register") %>' onclick="saveActivatinService();"/>
		</div>
	</div>
	<div id="divRoleGeneralPhoneBook" style="padding-top:10px;">
		<GeneralTools:DataGrid runat="server" showAdvancedSearch="true" ID="gridGeneralPhoneBooks" DefaultSortField="Name" ListCaption="GeneralPhoneBookList" ListDifferenceHeight="100" 
			DisableNavigationBar="true" ShowRowNumber="true" SelectMode="Multiple"
			StatusMultiSelectField="IsActive" DisableActiveMultiSelectField="true" DisableMultiSelectField="IsActive">
			<Columns>
				<GeneralTools:DataGridColumnInfo Caption="IsActive" FieldName="IsActive" Hidden="true" Sortable="false" ShowInExport="false" Align="Center"/>
				<GeneralTools:DataGridColumnInfo Caption="Title" Sortable="false" FieldName="Name" CellWidth="220" Align="Center" EditAble="false"/>
				<GeneralTools:DataGridColumnInfo Caption="Price" FieldName="Price" Sortable="false" ShowInExport="false" CellWidth="100" Align="Center" FormattingMethod="CustomRender"/>
			</Columns>
		</GeneralTools:DataGrid>
		<div class="buttonControlDiv">
			<input id="btnSaveUserGeneralPhoneBook" type="button" class="button" value='<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Register") %>'  onclick="saveActivationGeneralPhoneBook();"/>
		</div>
	</div>
</div>
<script type="text/javascript">
	function searchUserService() {
		var searchFilters = "";
		searchFilters += "UserGuid{(" + $("#<%=hdnUserGuid.ClientID%>")[0].value + ")}";
		gridServices.SearchFilters = searchFilters;
		gridServices.Search();
	}

	function searchUserGeneralPhoneBook() {
		var searchFilters = "";
		searchFilters += "UserGuid{(" + $("#<%=hdnUserGuid.ClientID%>")[0].value + ")}";
		gridGeneralPhoneBooks.SearchFilters = searchFilters;
		gridGeneralPhoneBooks.Search();
	}

	function saveActivatinService() {
		var decrease = true;
		var data = gridServices.GetSelectedRowData(["Guid", "Price", "Title", "IsActive"]);
		var result = getAjaxResponse("SaveUserService", "Data=" + data + "&UserGuidForUserService=" + $("#<%=hdnUserGuid.ClientID %>")[0].value + "&Decrease=" + decrease);
		if (result != "1")
			messageBox(result, '', 'alert', 'danger');
		else
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("InsertRecord")%>', '', 'alert', 'success');
	}

	function saveActivationGeneralPhoneBook() {
		var decrease = true;
		var data = gridGeneralPhoneBooks.GetSelectedRowData(["Guid", "Price", "Name", "IsActive"]);
		var result = getAjaxResponse("SaveUserGeneralPhoneBook", "Data=" + data + "&UserGuidForGeneralPhoneBook=" + $("#<%=hdnUserGuid.ClientID %>")[0].value + "&Decrease=" + decrease);
		if (result != "1")
			messageBox(result, '', 'alert', 'danger');
		else
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("InsertRecord")%>', '', 'alert', 'success');
	}
</script>