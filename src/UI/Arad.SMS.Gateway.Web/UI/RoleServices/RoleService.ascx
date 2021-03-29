<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RoleService.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.RoleServices.RoleService" %>

<asp:HiddenField ID="hdnServices" runat="server" />
<asp:HiddenField ID="hdnPhoneBooks" runat="server" />
<div class="col-md-12">
	<span class="fa fa-2x fa-info-circle blue"></span><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RoleServiceContent1") %>
	<div class="clear"></div>
	<span class="fa fa-2x fa-info-circle blue"></span><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RoleServiceContent2") %>
	
	<div id="divRoleService">
		<GeneralTools:DataGrid runat="server" ID="gridServices" DefaultSortField="Title" ListCaption="ServicesList" ListHeight="420"
			ShowRowNumber="true" SelectMode="Multiple" StatusMultiSelectField="IsDefault" DisableNavigationBar="true"
			GroupField="GroupTitle" GroupCollapse="false" GroupColumnShow="false" EnableGrouping="true">
			<Columns>
				<GeneralTools:DataGridColumnInfo FieldName="Guid" Hidden="true" />
				<GeneralTools:DataGridColumnInfo Caption="ServiceName" FieldName="Title" Sortable="false" CellWidth="250" Align="Center" EditAble="false" />
				<GeneralTools:DataGridColumnInfo Caption="GroupTitle" FieldName="GroupTitle" Hidden="true" Sortable="false" ShowInExport="false" Align="Center" />
				<GeneralTools:DataGridColumnInfo FieldName="IsDefault" Sortable="false" Hidden="true" ShowInExport="false" Align="Center" />
			</Columns>
		</GeneralTools:DataGrid>
		<div class="buttonControlDiv">
			<input id="btnSaveRoleService" type="button" class="btn btn-success" value='<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Register") %>' onclick="getServices();" />
			<a href="/PageLoader.aspx?c=<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Roles_Role, Session) %>" class="btn btn-default"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %></a>
		</div>
	</div>
</div>
<script type="text/javascript">
	function getServices() {
		if (gridServices.GetRecordCount() > 0) {
				var data = gridServices.GetAllRowData(["Guid"]);
				var SelectDefaultRow = gridServices.GetSelectedRowData(["Guid"]);
				var result = getAjaxResponse("SaveRoleService", "Data=" + data + "&SelectedDefaultRow=" + SelectDefaultRow + "&ServiceRoleGuid=" + '<%=RoleGuid%>');
				if (importData(result, "Result") == "OK")
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("InsertRecord")%>','','alert','success');
				else
					messageBox(importData(result, "Message"), '', 'alert', 'danger');
		}
		else
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorExistRecord")%>','', 'alert', 'danger');
	}
</script>
