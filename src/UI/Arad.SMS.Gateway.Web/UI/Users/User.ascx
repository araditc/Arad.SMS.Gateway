<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="User.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.User" %>
<%--<%@ Register Src="~/UI/Controls/DatePickerControl.ascx" TagPrefix="SMS" TagName="DatePickerControl" %>--%>
<%@ Register Src="~/UI/Controls/BootstrapDatePicker.ascx" TagName="DatePicker" TagPrefix="SMS" %>

<script src="/script/encryption.js" type="text/javascript"></script>
<asp:HiddenField ID="hdnUserGuid" runat="server" />

<div id="advanceSearchContainer" class="modal fade" role="dialog" aria-labelledby="gridSystemModalLabel">
	<div class="modal-dialog" role="document">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
				<h4 class="modal-title" id="gridSystemModalLabel"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AdvancedSearch") %></h4>
			</div>
			<div class="modal-body">
				<div class="form-horizontal" role="form">
					<div class="row">
						<div class="col-md-6">
							<div class="form-group">
								<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("BirthDate")%> <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("From")%></label>
								<div class="col-sm-7">
									<SMS:DatePicker runat="server" ID="dtpFromBirthDate" />
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="form-group">
								<label class="col-sm-1 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UpTo")%></label>
								<div class="col-sm-8">
									<SMS:DatePicker runat="server" ID="dtpToBirthDate" />
								</div>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-6">
							<div class="form-group">
								<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ExpireDate")%> <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("From")%></label>
								<div class="col-sm-7">
									<SMS:DatePicker runat="server" ID="dtpFromExpireDate" />
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="form-group">
								<label class="col-sm-1 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UpTo")%></label>
								<div class="col-sm-8">
									<SMS:DatePicker runat="server" ID="dtpToExpireDate" />
								</div>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-6">
							<div class="form-group">
								<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PanelPrice")%> <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("From")%></label>
								<div class="col-sm-7">
									<asp:TextBox ID="txtFromPanelPrice" runat="server" CssClass="form-control input-sm" type="number"></asp:TextBox>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="form-group">
								<label class="col-sm-1 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UpTo")%></label>
								<div class="col-sm-8">
									<asp:TextBox ID="txtToPanelPrice" runat="server" CssClass="form-control input-sm" type="number"></asp:TextBox>
								</div>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-6">
							<div class="form-group">
								<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NationalCode")%></label>
								<div class="col-sm-7">
									<asp:TextBox ID="txtNationalCode" runat="server" CssClass="form-control input-sm" type="number"></asp:TextBox>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="form-group">
								<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Status")%></label>
								<div class="col-sm-7">
									<asp:DropDownList ID="drpStatus" class="form-control input-sm" runat="server"></asp:DropDownList>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-primary" onclick="search();"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Search") %></button>
				<button type="button" class="btn btn-default" data-dismiss="modal"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %></button>
			</div>
		</div>
	</div>
</div>

<div id="divUsers">
	<GeneralTools:DataGrid runat="server" ID="gridUsers" DefaultSortField="CreateDate" ListCaption="UsersList" ListHeight="800" ShowSearchToolbar="true"
		ShowToolbar="true" ShowAdvancedSearch="true" SearchDivID="advanceSearchContainer" ToolbarPosition="Top" ShowPagerToTop="true" ShowRowNumber="true" GridComplete="setRowColor">
		<columns>
			<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="UserType" Search="false" CellWidth="30" Align="Center" FormattingMethod="CustomRender" />
			<GeneralTools:DataGridColumnInfo Caption="UserName" FieldName="UserName" Search="true" SearchOptions="{sopt: ['cn','eq'],clearSearch:true}" CellWidth="100" Align="Center" FormattingMethod="CustomRender" />
			<GeneralTools:DataGridColumnInfo Caption="FirstName" FieldName="FirstName" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="100" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="LastName" FieldName="LastName" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" CellWidth="100" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="CellPhone" FieldName="Mobile" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" SearchFilterCssClass="numberInput" Sortable="false" CellWidth="120" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="Credit" FieldName="Credit" Search="true" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" SearchFilterCssClass="numberInput" CellWidth="100" FormattingMethod="NumberDecimal" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="IsExpired" FieldName="IsExpired" Hidden="true" CellWidth="50" Align="Center" />
			<GeneralTools:DataGridColumnInfo Caption="CreateDate" FieldName="CreateDate" Search="true" SearchType="Date" SearchOptions="{sopt: ['gt','ge','lt','le','eq'],clearSearch:true}" Align="Center" CellWidth="120" FormattingMethod="DateTimeShortDate" />
			<GeneralTools:DataGridColumnInfo Caption="Email" FieldName="Email" Search="true" SearchOptions="{sopt: ['cn'],clearSearch:true}" Align="Center" CellWidth="100" MaxLength="20" />
			<GeneralTools:DataGridColumnInfo Caption="Action" FieldName="Action" Sortable="false" ShowInExport="false" CellWidth="200" Align="Center" FormattingMethod="ImageButton" />
			<GeneralTools:DataGridColumnInfo Caption="ParentGuid" FieldName="ParentGuid" Hidden="true" CellWidth="85" Align="Center" FormattingMethod="Encrypt" />
		</columns>
	</GeneralTools:DataGrid>
</div>

<script type="text/javascript">
	$(function () {
		$.ajax({
			url: "DataGridHandler.aspx/GetUserDomains",
			contentType: 'application/json; charset=utf-8',
			dataType: "json",
			type: 'POST',
			data: "{userGuid:'<%=UserGuid%>'}",
			async: false,
			cache: false
		}).done(function (data) {
			var options = "<option value=''></option>";
			$.each(data.d, function (i) {
				options += '<option value="' + data.d[i].guid + '">' + data.d[i].name + '</option>';
			});

			$("#drpDomain").append(options);
		});

		$("#btnSearch").click(function (e) {
			search();
		});

	});

	function setRowColor(listId) {
		var rowIDs = $(listId).jqGrid('getDataIDs');
		var ret;
		if (rowIDs.length > 0) {
			{
				for (var i = 0; i < rowIDs.length; i++) {
					{
						ret = $(listId).jqGrid('getRowData', rowIDs[i]);
						if (ret['IsExpired'] == 1)
							$('#' + $.jgrid.jqID(rowIDs[i])).addClass('warning-gridRowColor');
					}
				}
			}
		}
	}

	function search(gridId) {
		if (!gridId)
			gridUsers.TriggerToolbar();

		var domainGuid = $('#drpDomain option:selected')[0].value;

		var searchFilters = "";
		searchFilters += "[";
		searchFilters += "{'field':'BirthDate','op':'ge','data':'" + getDatePickerValue("<%=dtpFromBirthDate.ClientID%>") + "'},";
		searchFilters += "{'field':'BirthDate','op':'le','data':'" + getDatePickerValue("<%=dtpToBirthDate.ClientID%>") + "'},";
		searchFilters += "{'field':'ExpireDate','op':'ge','data':'" + getDatePickerValue("<%=dtpFromExpireDate.ClientID%>") + "'},";
		searchFilters += "{'field':'ExpireDate','op':'le','data':'" + getDatePickerValue("<%=dtpToExpireDate.ClientID%>") + "'},";
		searchFilters += "{'field':'PanelPrice','op':'ge','data':'" + $("#<%=txtFromPanelPrice.ClientID%>")[0].value + "'},";
		searchFilters += "{'field':'PanelPrice','op':'le','data':'" + $("#<%=txtToPanelPrice.ClientID%>")[0].value + "'},";
		searchFilters += "{'field':'NationalCode','op':'cn','data':'" + $("#<%=txtNationalCode.ClientID%>")[0].value + "'},";
		searchFilters += "{'field':'IsActive','op':'eq','data':'" + $("#<%=drpStatus.ClientID%>")[0].value + "'},";
		searchFilters += "{'field':'DomainGuid','op':'eq','data':'" + domainGuid + "'}";
		searchFilters += "]";
		gridUsers.SearchFilters = searchFilters;
		gridUsers.Search();

		$("#advanceSearchContainer").modal('hide');
	}

	function refreshGrid(result) {
		if (result == "true") {
			gridUsers.Search();
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("InsertRecord")%>', '', 'alert', 'success');
		}
	}

	function deleteUser(e) {
		debugger;
		gridUsers.Event = e;
		if (gridUsers.IsSelectedRow()) {
			var guid = gridUsers.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteUser", "Guid=" + guid);
					if (isDelete) {
						gridUsers.Search();
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeleteRecord")%>', '', 'alert', 'success');
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}

	function goToUsersPanel(userGuid, domainGuid) {
		var ajaxResult = getAjaxResult(getAjaxResponse('GoToUsersPanel', 'UserGuid=' + userGuid + '&DomainGuid=' + domainGuid));
		if (!ajaxResult.result) {
			messageBox(ajaxResult.message);
			return;
		}
		window.open(ajaxResult.message, '_blank', 'width=800,height=600%', true);
	}
</script>
