<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendDetails.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsSends.SendDetails" %>
<%@ Register Src="~/UI/Controls/BootstrapDateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="SMS" %>

<asp:MultiView ID="mvDetails" runat="server">
	<asp:View ID="SendSms" runat="server">
		<div class="row">
			<hr />
			<div class="col-xs-10 col-md-10">
				<div class="form-horizontal" role="form">
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ID") %></label>
						<div class="col-sm-4">
							<asp:Label ID="lblId" runat="server" Text=""></asp:Label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TypeSend") %></label>
						<div class="col-sm-4">
							<asp:Label ID="lblSendType" runat="server" Text=""></asp:Label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Group") %></label>
						<div class="col-sm-4">
							<asp:Label ID="lblGroupName" runat="server" Text=""></asp:Label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendFrom") %></label>
						<div class="col-sm-4">
							<asp:Label ID="lblSender" runat="server" Text=""></asp:Label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendTime") %></label>
						<div class="col-sm-4">
							<SMS:datetimepicker id="dtpSendDate" runat="server"></SMS:datetimepicker>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsBody") %></label>
						<div class="col-sm-4">
							<GeneralTools:smsbodybox runat="server" id="txtSmsText" width="220"></GeneralTools:smsbodybox>
						</div>
					</div>
				</div>
			</div>
		</div>
	</asp:View>
	<asp:View ID="SendPeriod" runat="server">
		<div class="row">
			<hr />
			<div class="col-xs-10 col-md-10">
				<div class="form-horizontal" role="form">
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ID") %></label>
						<div class="col-sm-4">
							<asp:Label ID="lblPeriodId" runat="server" Text=""></asp:Label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TypeSend") %></label>
						<div class="col-sm-4 control-label" style="text-align: right">
							<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendPeriodSms") %>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Group") %></label>
						<div class="col-sm-4">
							<asp:Label ID="lblPeriodGroupName" runat="server" Text=""></asp:Label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Range") %></label>
						<div class="col-sm-4">
							<asp:Label ID="lblPeriodType" runat="server" Text=""></asp:Label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendFrom") %></label>
						<div class="col-sm-4">
							<asp:Label ID="lblPeriodSender" runat="server" Text=""></asp:Label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsBody") %></label>
						<div class="col-sm-4">
							<GeneralTools:smsbodybox runat="server" id="txtPeriodSmsText" width="220"></GeneralTools:smsbodybox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("StartSendTime") %></label>
						<div class="col-sm-4">
							<SMS:datetimepicker id="dtpStartDate" runat="server"></SMS:datetimepicker>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("EndSendTime") %></label>
						<div class="col-sm-4">
							<SMS:datetimepicker id="dtpEndDate" runat="server"></SMS:datetimepicker>
						</div>
					</div>
				</div>
			</div>
		</div>
	</asp:View>
	<asp:View ID="SendGradual" runat="server">
		<div class="row">
			<hr />
			<div class="col-xs-10 col-md-10">
				<div class="form-horizontal" role="form">
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ID") %></label>
						<div class="col-sm-4">
							<asp:Label ID="lblGradualId" runat="server" Text=""></asp:Label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TypeSend") %></label>
						<div class="col-sm-4 control-label" style="text-align: right">
							<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendGradualSms") %>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Group") %></label>
						<div class="col-sm-4">
							<asp:Label ID="lblGradualGroup" runat="server" Text=""></asp:Label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Range") %></label>
						<div class="col-sm-4">
							<asp:Label ID="lblGradualPerid" runat="server" Text=""></asp:Label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendFrom") %></label>
						<div class="col-sm-4">
							<asp:Label ID="lblGradualSender" runat="server" Text=""></asp:Label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsBody") %></label>
						<div class="col-sm-4">
							<GeneralTools:smsbodybox runat="server" id="txtGradualSmsText" width="220"></GeneralTools:smsbodybox>
						</div>
					</div>
				</div>
			</div>
		</div>
	</asp:View>
	<asp:View ID="SendBulk" runat="server">
		<div class="row">
			<hr />
			<div class="col-xs-5 col-md-5">
				<div class="form-horizontal" role="form">
					<div class="form-group">
						<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ID") %></label>
						<div class="col-sm-8">
							<asp:Label ID="lblBulkId" runat="server" Text=""></asp:Label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TypeSend") %></label>
						<div class="col-sm-8 control-label" style="text-align: right">
							<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendBulk") %>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendFrom") %></label>
						<div class="col-sm-8">
							<asp:Label ID="lblBulkSender" runat="server" Text=""></asp:Label>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsBody") %></label>
						<div class="col-sm-8">
							<GeneralTools:smsbodybox runat="server" id="txtBulkSmsText" width="220"></GeneralTools:smsbodybox>
						</div>
					</div>
				</div>
			</div>
			<div class="col-xs-6 col-md-6">
				<GeneralTools:DataGrid runat="server" ID="gridRecipient" DefaultSortField="Count" ListCaption="RecipientsList"
					ShowRowNumber="true" ShowFooterRow="true" DisableNavigationBar="true" ListHeight="150" GridComplete="gridComplete">
					<columns>
						<GeneralTools:DataGridColumnInfo FieldName="Title" Caption="ScopeOfSend" Sortable="false" Align="Center" CellWidth="130" />
						<GeneralTools:DataGridColumnInfo FieldName="Count" Caption="Count" Sortable="false" Align="Center" CellWidth="25" FormattingMethod="NumberDecimal"/>
						<GeneralTools:DataGridColumnInfo FieldName="SendPrice" Caption="SendPrice" Sortable="false" Align="Center" CellWidth="40" FormattingMethod="NumberDecimal"/>
						<GeneralTools:DataGridColumnInfo FieldName="ScopeCount" Caption="TotalCount" Sortable="false" Align="Center" CellWidth="30" FormattingMethod="NumberDecimal"/>
					</columns>
				</GeneralTools:DataGrid>
			</div>
		</div>
	</asp:View>
</asp:MultiView>
<div class="clearfix form-actions col-sm-6" style="text-align: left; background-color: white">
	<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Return" />
</div>

<script type="text/javascript">
	function gridComplete() {
		var data = gridRecipient.GetUserData();
		gridRecipient.SetFooterRowData(data[0], false);
	}
</script>
