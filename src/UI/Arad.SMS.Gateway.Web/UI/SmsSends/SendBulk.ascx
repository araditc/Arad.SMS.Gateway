<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendBulk.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsSends.SendBulk" %>
<%@ Register Src="~/UI/Controls/BootstrapDateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="SMS" %>
<%@ Register Src="~/UI/Controls/BootstrapDatePicker.ascx" TagName="DatePicker" TagPrefix="SMS" %>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<asp:HiddenField ID="hdnRecipients" runat="server" />
<div class="col-xs-12 col-md-12">
	<div class="col-md-4">
		<h3 class="header smaller lighter blue">
			<small><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsSetting") %></small>
		</h3>
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<div class="input-group">
					<span class="input-group-addon" style="background-color: rgb(80, 168, 226); color: #fff;"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sender") %></span>
					<asp:DropDownList ID="drpSenderNumber" runat="server" IsRequired="true" CssClass="form-control input-sm"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<GeneralTools:SmsBodyControl runat="server" ID="txtSmsBody" IsRequired="true"></GeneralTools:SmsBodyControl>
			</div>
			<div class="form-group">
				<label class="control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendTime") %></label>
				<div>
					<SMS:DateTimePicker ID="dtpSendDateTime" IsRequired="true" runat="server"></SMS:DateTimePicker>
				</div>
			</div>
		</div>
	</div>
	<div class="col-md-8">
		<h3 class="header smaller lighter blue">
			<small><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ScopeOfSend") %></small>
		</h3>
		<asp:UpdatePanel ID="updatePanelCity" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="form-inline" role="form" style="margin-right: 13px;">
					<div class="form-group">
						<label class="control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Country")%></label>
						<div>
							<asp:DropDownList ID="drpCountry" runat="server" class="form-control input-sm" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged" AutoPostBack="True" Style="min-width: 150px;"></asp:DropDownList>
						</div>
					</div>
					<div class="form-group">
						<label class="control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Province")%></label>
						<div>
							<asp:DropDownList ID="drpProvince" runat="server" class="form-control input-sm" OnSelectedIndexChanged="drpProvince_SelectedIndexChanged" AutoPostBack="True" Style="min-width: 150px;"></asp:DropDownList>
						</div>
					</div>
					<div class="form-group">
						<label class="control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("City")%></label>
						<div>
							<asp:DropDownList ID="drpCity" runat="server" class="form-control input-sm" Style="min-width: 150px;"></asp:DropDownList>
						</div>
					</div>
				</div>
			</ContentTemplate>
			<Triggers>
				<asp:AsyncPostBackTrigger ControlID="drpCountry" EventName="SelectedIndexChanged" />
				<asp:AsyncPostBackTrigger ControlID="drpProvince" EventName="SelectedIndexChanged" />
			</Triggers>
		</asp:UpdatePanel>
		<hr style="margin: 5px;" />
		<div class="form-horizontal scope" role="form">
			<div class="form-group">
				<label class="col-sm-3 control-label">
					<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CountNumbers") %>
					<a href="javascript:void(0)" class="fa fa-info-circle blue easyui-tooltip" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CountNumbersDescription") %>"></a>
				</label>
				<div class="col-sm-5">
					<asp:TextBox CssClass="form-control input-sm numberInput" ID="txtNumberCount" runat="server" IsRequired="true" autoformatdecimal="true"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label">
					<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("StartFromNumber") %>
					<a href="javascript:void(0)" class="fa fa-info-circle blue easyui-tooltip" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("StartFromNumber") %>"></a>
				</label>
				<div class="col-sm-5">
					<asp:TextBox CssClass="form-control input-sm numberInput" ID="txtFrom" runat="server" autoformatdecimal="true"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label">
					<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Prefix") %>
					<a href="javascript:void(0)" class="fa fa-info-circle blue easyui-tooltip" title="For example 98912"></a>
				</label>
				<div class="col-sm-5">
					<asp:TextBox CssClass="form-control input-sm numberInput" ID="txtPrefix" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label">
					<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AccountNoType") %>
					<a href="javascript:void(0)" class="fa fa-info-circle blue easyui-tooltip" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AccountNoType") %>"></a>
				</label>
				<div class="col-sm-5">
					<asp:DropDownList CssClass="form-control input-sm" ID="drpType" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label">
					<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Operator") %>
					<a href="javascript:void(0)" class="fa fa-info-circle blue easyui-tooltip" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Operator") %>"></a>
				</label>
				<div class="col-sm-5">
					<asp:DropDownList CssClass="form-control input-sm" ID="drpOperator" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label">
					<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ZipCode") %>
					<a href="javascript:void(0)" class="fa fa-info-circle blue easyui-tooltip" title="Maximum 8 digits"></a>
				</label>
				<div class="col-sm-5">
					<asp:TextBox CssClass="form-control input-sm numberInput" ID="txtZipCode" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label red">
					<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CountNumbers") %>
					<a href="javascript:void(0)" class="fa fa-info-circle blue easyui-tooltip" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CountNumbers") %>"></a>
				</label>
				<div class="col-sm-5">
					<div class="input-group">
						<span class="input-group-addon">
							<a href="#" class="btn btn-primary btn-xs" onclick="addRecipient();"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AddRecipient") %></a>
						</span>
						<asp:TextBox CssClass="form-control input-sm red" ID="txtCount" runat="server" ReadOnly="true"></asp:TextBox>
					</div>
				</div>
			</div>
		</div>
		<div class="col-md-offset-4">
			<asp:Button ID="btnSave" runat="server" Text="Send" CssClass="btn btn-success" OnClick="btnSave_Click" OnClientClick="return getRecipients();" />
			<a href="#" onclick="getCount();" class="btn btn-danger"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Counting") %></a>
		</div>
	</div>
</div>
<div class="clear"></div>
<div class="row">
	<div class="col-md-9">
		<GeneralTools:DataGrid runat="server" ID="gridRecipient" DefaultSortField="Count" ListCaption="RecipientsList"
			ShowRowNumber="true" ShowFooterRow="true" DisableNavigationBar="true" ListHeight="150">
			<Columns>
				<GeneralTools:DataGridColumnInfo FieldName="Prefix" Hidden="true" />
				<GeneralTools:DataGridColumnInfo FieldName="ZipCode" Hidden="true" />
				<GeneralTools:DataGridColumnInfo FieldName="ZoneGuid" Hidden="true" />
				<GeneralTools:DataGridColumnInfo FieldName="Type" Hidden="true" />
				<GeneralTools:DataGridColumnInfo FieldName="Operator" Hidden="true" />
				<GeneralTools:DataGridColumnInfo FieldName="FromIndex" Hidden="true" />
				<GeneralTools:DataGridColumnInfo FieldName="Title" Caption="ScopeOfSend" Sortable="false" Align="Center" CellWidth="130" />
				<GeneralTools:DataGridColumnInfo FieldName="Count" Caption="Count" Sortable="false" Align="Center" CellWidth="50" />
				<GeneralTools:DataGridColumnInfo FieldName="SendPrice" Caption="SendPrice" Sortable="false" Align="Center" CellWidth="50" />
				<GeneralTools:DataGridColumnInfo FieldName="ScopeCount" Caption="TotalCount" Sortable="false" Align="Center" CellWidth="50" />
				<GeneralTools:DataGridColumnInfo FieldName="Action" Caption="Action" Sortable="false" CellWidth="25" Align="Center" FormattingMethod="ImageButton" />
			</Columns>
		</GeneralTools:DataGrid>
	</div>

</div>



<script type="text/javascript">
	var sumCount = 0;
	var sumPrice = 0;

	function getCount() {
		$("body").setOverlay('/pic/loader.gif');
		var zoneGuid = $("#<%=drpCity.ClientID%> option:selected").val();
		zoneGuid = zoneGuid ? zoneGuid : '<%=Guid.Empty%>';
		zoneGuid = zoneGuid == '<%=Guid.Empty%>' ? $("#<%=drpProvince.ClientID%> option:selected").val() : zoneGuid;
		zoneGuid = zoneGuid == '<%=Guid.Empty%>' ? $("#<%=drpCountry.ClientID%> option:selected").val() : zoneGuid;

		var sendCount = parseInt($("#<%=txtNumberCount.ClientID%>").val().replace(/\,/g, ''));
		var fromIndex = parseInt($("#<%=txtFrom.ClientID%>").val().replace(/\,/g, ''));
		var prefix = $("#<%=txtPrefix.ClientID%>").val();
		var type = $("#<%=drpType.ClientID%> option:selected").val();
		var opt = $("#<%=drpOperator.ClientID%> option:selected").val();
		var zipcode = $("#<%=txtZipCode.ClientID%>").val();

		var smsText = $("#<%=txtSmsBody.ClientID%> textarea").val();
		var numberGuid = $("#<%=drpSenderNumber.ClientID%> option:selected").val();

		var result = getAjaxResponse("GetBulkRecipientCount",
																 "ZoneGuid=" + zoneGuid +
																 "&Prefix=" + prefix +
																 "&ZipCode=" + zipcode +
																 "&SmsText=" + smsText +
																 "&Operator=" + opt +
																 "&SendCount=" + sendCount +
																 "&Type=" + type +
																 "&SenderNumberGuid=" + numberGuid);

		if (importData(result, "Result") == "OK") {
			$("#<%=txtCount.ClientID%>")[0].value = getFormatDecimal(importData(result, "Count"));
		}
		else
			messageBox(importData(result, "Message"), '', 'alert', 'danger');

		$("body").removeOverlay();
	}

	function addRecipient() {
		var title = "";
		var sendPrice = 0;

		if (!validateRequiredFields())
			return false;

		var zoneGuid = $("#<%=drpCity.ClientID%> option:selected").val();
		if (zoneGuid)
            title = ' <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Province") %> ' + $("#<%=drpProvince.ClientID%> option:selected").text() + ' <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("City") %> ' + $("#<%=drpCity.ClientID%> option:selected").text();
		else
			zoneGuid = '<%=Guid.Empty%>';

		if (zoneGuid == '<%=Guid.Empty%>') {
			zoneGuid = $("#<%=drpProvince.ClientID%> option:selected").val();
            title = ' <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Province") %> ' + $("#<%=drpProvince.ClientID%> option:selected").text();
		}

		if (zoneGuid == '<%=Guid.Empty%>') {
			zoneGuid = $("#<%=drpCountry.ClientID%> option:selected").val();
			title = ' ' + $("#<%=drpCountry.ClientID%> option:selected").text() + ' ';
		}

		var sendCount = parseInt($("#<%=txtNumberCount.ClientID%>").val().replace(/\,/g, ''));
		var fromIndex = parseInt($("#<%=txtFrom.ClientID%>").val().replace(/\,/g, ''));
		var prefix = $("#<%=txtPrefix.ClientID%>").val();
		var type = $("#<%=drpType.ClientID%> option:selected").val();
		var opt = $("#<%=drpOperator.ClientID%> option:selected").val();
		var zipcode = $("#<%=txtZipCode.ClientID%>").val();

		if (prefix != '')
            title += ' <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Prefix") %> ' + prefix;
		if (zipcode != '')
            title += ' <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ZipCode") %> ' + zipcode;
		if (type == '<%=(int)Arad.SMS.Gateway.Business.NumberType.Credits%>')
            title += ' <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PrepaidMobiles") %> ';
		else if (type == '<%=(int)Arad.SMS.Gateway.Business.NumberType.Permanent%>')
            title += ' <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PostpaidMobiles") %> ';

		title += ' ' + $("#<%=drpOperator.ClientID%> option:selected").text() + ' ';

		if (fromIndex >= 0)
            title += ' <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("From") %> ' + getFormatDecimal(fromIndex.toString());
		else {
            title += ' <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Random") %> '
			fromIndex = -1;
		}

		var smsText = $("#<%=txtSmsBody.ClientID%> textarea").val();
		var numberGuid = $("#<%=drpSenderNumber.ClientID%> option:selected").val();

		$("body").setOverlay('/pic/loader.gif');
		var result = getAjaxResponse("GetBulkRecipientCount",
																 "ZoneGuid=" + zoneGuid +
																 "&Prefix=" + prefix +
																 "&ZipCode=" + zipcode +
																 "&SmsText=" + smsText +
																 "&Operator=" + opt +
																 "&SendCount=" + sendCount +
																 "&Type=" + type +
																 "&SenderNumberGuid=" + numberGuid);

		if (importData(result, "Result") == "OK") {
			$("#<%=txtCount.ClientID%>")[0].value = getFormatDecimal(importData(result, "Count"));
			sendPrice = importData(result, "SendPrice");
		}
		else {
			messageBox(importData(result, "Message"), '', 'alert', 'danger');
			$("body").removeOverlay();
			return false;
		}

		var totalCount = parseInt($("#<%=txtCount.ClientID%>").val().replace(/\,/g, ''));

		if (fromIndex != -1 && (totalCount - fromIndex) < sendCount) {
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendCountIsInvalid")%>', '', 'alert', 'danger');
			$("body").removeOverlay();
			return false;
		}

		if (fromIndex == -1 && totalCount < sendCount) {
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendCountIsInvalid")%>', '', 'alert', 'danger');
			$("body").removeOverlay();
			return false;
		}

		addRow(sendCount, totalCount, sendPrice, type, opt, fromIndex, zipcode, prefix, zoneGuid, title);
		$("body").removeOverlay();
	}

	function addRow(sendCount, scopeCount, sendPrice, type, operator, fromIndex, zipCode, prefix, zoneGuid, title) {
		sumCount += val(sendCount);
		sumPrice += parseFloat(sendPrice);
		decimalFormatSumCount = getFormatDecimal(sumCount + '');
		decimalFormatSumPrice = getFormatDecimal(sumPrice + '');
		sendCount = getFormatDecimal(sendCount.toString());
		sendPrice = getFormatDecimal(sendPrice.toString());
		scopeCount = getFormatDecimal(scopeCount.toString());
		gridRecipient.AddRow({ Type: type, Operator: operator, FromIndex: fromIndex, Title: title, Count: sendCount, ScopeCount: scopeCount, SendPrice: sendPrice, ZipCode: zipCode, Prefix: prefix, ZoneGuid: zoneGuid, Action: '<span class="ui-icon fa fa-trash-o red" onclick="deleteRow(event);"></span>' });
		gridRecipient.SetFooterRowData({ Count: decimalFormatSumCount,SendPrice:decimalFormatSumPrice, Title: '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sum")%>' });
		clearTextRecipientInfo();
	}

	function clearTextRecipientInfo() {
		$(".scope").find('input:text').each(function () {
			$(this)[0].value = "";
		});
	}

	function deleteRow(e) {
		gridRecipient.Event = e;
		if (gridRecipient.IsSelectedRow()) {
			count = gridRecipient.GetSelectedRowFieldValue('Count');
			price = gridRecipient.GetSelectedRowFieldValue('SendPrice').replace(/\,/g, '');

			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConfirmDelete")%>', '', 'confirm', 'danger', function (result) {
				if (result) {
					sumCount -= val(count);
					sumPrice -= parseFloat(price);
					gridRecipient.SetFooterRowData({ Count: getFormatDecimal(sumCount + ''), SendPrice: getFormatDecimal(sumPrice + ''), Title: '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sum")%>' });
					gridRecipient.DeleteRow(gridRecipient.GetSelectedRowID());
				}
			});
		}
	}

	function getRecipients() {
		var rowCount = gridRecipient.GetRecordCount();
		if (rowCount > 0) {
			$("#<%=hdnRecipients.ClientID%>")[0].value = gridRecipient.GetAllRowData(["Title", "Prefix", "ZipCode", "ZoneGuid", "Type", "Operator", "FromIndex", "Count", "ScopeCount", "SendPrice"]);
			return true;
		}
		else {
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RecieverListIsEmpty")%>', '', 'alert', 'danger');
			return false;
		}
	}
</script>

