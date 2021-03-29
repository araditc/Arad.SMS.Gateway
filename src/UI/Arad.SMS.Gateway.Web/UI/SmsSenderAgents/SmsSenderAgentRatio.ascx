<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmsSenderAgentRatio.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsSenderAgents.SmsSenderAgentRatio" %>

<div class="row">
	<div class="col-md-8">
		<div class="form-inline" style="margin: 10px 10px;">
			<div class="form-group">
				<label><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Operator") %></label>
				<asp:DropDownList ID="drpOperator" runat="server" isRequired="true" class="form-control input-sm"></asp:DropDownList>
			</div>
			<div class="form-group">
				<label><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SMSType") %></label>
				<asp:DropDownList ID="drpSmsType" runat="server" isRequired="true" class="form-control input-sm"></asp:DropDownList>
			</div>
			<div class="form-group">
				<label><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Ratio") %></label>
				<asp:TextBox ID="txtRatio" runat="server" isRequired="true" class="form-control input-sm" autoformatdecimal="true" allowdecimal="true" placeholder=""></asp:TextBox>
			</div>
			<asp:Button ID="btnSave" Text="Register" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Style="border: 0" />
			<asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Style="border: 0" />
		</div>
	</div>
	<div class="clear"></div>
</div>
<GeneralTools:DataGrid runat="server" ID="gridRatio" ListCaption="OperatorRatioList"
	ShowRowNumber="true" ListHeight="200" DisableNavigationBar="true">
	<columns>
		<GeneralTools:DataGridColumnInfo Caption="Operator" FieldName="Operator" Align="Center" />
		<GeneralTools:DataGridColumnInfo Caption="Type" FieldName="SmsType" Align="Center" FormattingMethod="CustomRender"/>
		<GeneralTools:DataGridColumnInfo Caption="Ratio" FieldName="Ratio" Align="Center" />
		<GeneralTools:DataGridColumnInfo FieldName="Action" Caption="Action" Sortable="false" CellWidth="50" Align="Center" FormattingMethod="ImageButton" />
	</columns>
</GeneralTools:DataGrid>

<script type="text/javascript">
	function deleteRow(e) {
		gridRatio.Event = e;
		if (gridRatio.IsSelectedRow()) {
			var guid = gridRatio.SelectedGuid;
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm','danger', function (result) {
				if (result) {
					var isDelete = getAjaxResponse("DeleteAgentRatio", "Guid=" + guid);
					if (isDelete) {
						gridRatio.Search();
					}
					else
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ErrorRecord")%>', '', 'alert', 'danger');
				}
			});
		}
	}
</script>

