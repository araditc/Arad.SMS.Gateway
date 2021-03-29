<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveGroupPrice.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.GroupPrices.SaveGroupPrice" %>
<asp:HiddenField ID="hdnAgentRatio" runat="server" />

<div class="row">
	<div class="col-md-4">
		<div class="panel panel-primary">
			<div class="panel-heading"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("basicInformation") %></div>
			<div class="panel-body">
				<div class="form-horizontal" role="form">
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Title") %></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtTitle" class="form-control input-sm" isrequired="true" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MinimumMessage")%></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtMinimumMessage" class="form-control input-sm numberInput" isrequired="true" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MaximumMessage")%></label>
						<div class="col-sm-7">
							<asp:TextBox ID="txtMaximumMessage" class="form-control input-sm numberInput" isrequired="true" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("BaseSmsPrice")%></label>
						<div class="col-sm-7">
							<div class="input-group">
								<asp:TextBox ID="txtBasePrice" class="form-control input-sm numberInput" isrequired="true" runat="server"></asp:TextBox>
								<span class="input-group-addon"><%--ریال--%></span>
							</div>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Private")%></label>
						<div class="col-sm-7">
							<asp:CheckBox ID="chbIsPrivate" runat="server" />
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("IsDefaultForUsers")%></label>
						<div class="col-sm-7">
							<asp:CheckBox ID="chbDefaultGroupPrice" runat="server" />
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DecreaseTax")%></label>
						<div class="col-sm-7">
							<asp:CheckBox ID="chbDecreaseTax" runat="server" Checked="true" />
						</div>
					</div>
					<div class="buttonControlDiv">
						<asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Register" OnClick="btnSave_Click"></asp:Button>
						<a class="btn btn-default" href="/PageLoader.aspx?c=<%=Arad.SMS.Gateway.GeneralLibrary.Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_GroupPrices_GroupPrice,Session)%>"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel")%></a>
					</div>
				</div>
			</div>
		</div>
	</div>
	<div class="col-md-6">
		<div class="panel panel-primary">
			<div class="panel-heading"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AgentRatioList") %></div>
			<div class="panel-body">
				<div class="row">
					<div>
						<GeneralTools:DataGrid runat="server" ID="gridAgentRatio" ListCaption="AgentRatioList"
							ShowRowNumber="true" ListHeight="155" DisableNavigationBar="true">
							<Columns>
								<GeneralTools:DataGridColumnInfo FieldName="AgentID" Hidden="true"/>
								<GeneralTools:DataGridColumnInfo FieldName="Agent" Caption="SmsSenderAgent" CellWidth="150" Align="Center" />
								<GeneralTools:DataGridColumnInfo FieldName="CurrentRatio" Hidden="true"/>
								<GeneralTools:DataGridColumnInfo FieldName="NewRatio" Caption="Ratio" Align="Center" CellWidth="50" Editable="true"/>
							</Columns>
						</GeneralTools:DataGrid>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>

<script type="text/javascript">
	function saveAgentRatio() {
		var records = gridAgentRatio.GetAllRowData(["AgentID", "CurrentRatio", "NewRatio"]);
		var isValid = true;
		var currentRatio;
		var newRatio;
		
		if('<%=IsMainAdmin%>' == 'True')
			return true;

		for (i = 0; i < importData(records, 'resultCount') ; i++) {
			currentRatio = parseFloat(importData(records, 'CurrentRatio' + i));
			newRatio = parseFloat(importData(records, 'NewRatio' + i));
			if (newRatio < currentRatio) {
				isValid = false;
				break;
			}
		}
		if (!isValid) {
            messageBox("<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RatioRule") %>", '', 'alert', 'danger');
			return false;
		}

		return true;
	}

	function validateGroupPrice() {
		try {
			if (!validateRequiredFields())
				return false;

			if (!saveAgentRatio())
				return false;

			$("#<%=hdnAgentRatio.ClientID%>")[0].value = gridAgentRatio.GetAllRowData(["AgentID", "CurrentRatio","NewRatio"]);
			return true;
		}
		catch (ex) {
			return false;
		}
	}
</script>
