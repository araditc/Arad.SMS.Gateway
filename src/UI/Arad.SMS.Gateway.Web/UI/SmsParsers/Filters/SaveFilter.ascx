<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveFilter.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsParsers.Filters.SaveFilter" %>
<%@ Register Src="~/UI/Controls/BootstrapDateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="SMS" %>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<asp:HiddenField ID="hdnScopeGuid" runat="server" />
<asp:HiddenField ID="hdnOperationGroupGuid" runat="server" />
<asp:HiddenField ID="hdnGroupGuid" runat="server" />
<asp:HiddenField ID="hdnConditionField" runat="server" />
<asp:HiddenField ID="hdnConditionGroupGuid" runat="server" />
<asp:HiddenField ID="hdnFieldName" runat="server" />

<script type="text/javascript">
	function refereshForm() {
		$("#sendSms").hide();
		$('#operationGroup').hide();
		$('#<%=drpTrafficRelay.ClientID%>').hide();
		$("#formatSetting").hide();
		$('#<%=txtOpration.ClientID%>').hide();
	}

	function setCondition(actionType) {
		var selectedItem = $('#<%=drpConditions.ClientID %> option:selected').val();
		if (actionType != 'edit')
			$('#<%=drpOperations.ClientID %>').val('');
		refereshForm();
		switch (selectedItem) {
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterConditions.Everything%>':
				$("#<%=txtCondition.ClientID%>").hide();
				$("#conditionGroup").hide();
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterConditions.NationalCode%>':
				$("#<%=txtCondition.ClientID%>").hide();
				$("#conditionGroup").hide();
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterConditions.EqualWithPhoneBookField%>':
				$("#<%=txtCondition.ClientID%>").hide();
				$("#conditionGroup").show();
				break;
			default:
				$("#conditionGroup").hide();
				$("#<%=txtCondition.ClientID%>").show();
				break;
		}
	}

	function setOption() {
		var selectedItem = $('#<%=drpOperations.ClientID %> option:selected').val();
		refereshForm();
		$('#<%=txtUrl.ClientID%>').hide();
		switch (selectedItem) {
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterOperations.ForwardSmsToGroup%>':
				$("#sendSms").show();
				$("#group").hide();
				$("#<%=txtSmsBody.ClientID%>").hide();
				$('#operationGroup').show();
				$('#<%=txtOpration.ClientID%>').hide();
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterOperations.AddToGroup%>':
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterOperations.RemoveFromGroup%>':
				refereshForm();
				$("#sendSms").show();
				$("#group").hide();
				$("#<%=txtSmsBody.ClientID%>").show();
				$('#operationGroup').show();
				$('#<%=txtOpration.ClientID%>').hide();
				$('#<%=txtUrl.ClientID%>').show();
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterOperations.TransferToEmail%>':
				refereshForm();
				$('#<%=txtOpration.ClientID%>').show();
				$('#<%=txtOpration.ClientID%>').attr('placeHolder', '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ReceiverEmail")%>');
				$('#<%=txtOpration.ClientID%>')[0].value = '';
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterOperations.TransferToUrl%>':
				refereshForm();
				$('#<%=drpTrafficRelay.ClientID%>').show();
				$('#<%=txtOpration.ClientID%>').hide();
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterOperations.TransferToMobile%>':
				refereshForm();
				$('#<%=txtOpration.ClientID%>').show();
				$('#<%=txtOpration.ClientID%>').attr('placeHolder', '<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RecieverNumber")%>');
					$("#sendSms").show();
					$("#group").hide();
					$("#<%=txtSmsBody.ClientID%>").hide();
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterOperations.SendSmsToSender%>':
				refereshForm();
				$('#<%=txtOpration.ClientID%>').hide();
				$("#sendSms").show();
				$("#group").hide();
				$("#<%=txtSmsBody.ClientID%>").show();
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterOperations.SendSmsToGroup%>':
				refereshForm();
				$('#<%=txtOpration.ClientID%>').hide();
				$("#sendSms").show();
				$("#group").show();
				$("#<%=txtSmsBody.ClientID%>").show();
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterOperations.SendSmsFromFormat%>':
				refereshForm();
				var condition = $('#<%=drpConditions.ClientID %> option:selected').val();
				switch (condition) {
					case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterConditions.NationalCode%>':
					case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterConditions.EqualWithPhoneBookField%>':
						$('#<%=txtOpration.ClientID%>').hide();
						$("#sendSms").show();
						$("#group").hide();
                        $("#<%=txtSmsBody.ClientID%>").hide();
						$("#formatSetting").show();
						break;
					default:
						$("#<%=txtOpration.ClientID%>").hide();
						$('#<%=drpOperations.ClientID %>').val('');
						messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ParserOperationInvalid")%>', '', 'alert', 'danger');
						break;
				}
				break;
		}
	}

	function submitValidation() {
		if (!validateRequiredFields('saveFilter'))
			return false;

		var selectedCondition = $('#<%=drpConditions.ClientID %> option:selected').val();
		switch (selectedCondition) {
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterConditions.NationalCode%>':
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterConditions.Everything%>':
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterConditions.EqualWithPhoneBookField%>':
				if ($("#<%=hdnConditionGroupGuid.ClientID%>")[0].value == '') {
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PleaseSelectGroup")%>', '', 'alert', 'danger');
					return false;
				}
				if ($("#<%=hdnFieldName.ClientID%>")[0].value == '') {
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelectFieldOfPhoneBookGroup")%>', '', 'alert', 'danger');
					return false;
				}
				break;
			default:
				if (!validateRequiredFields('condition'))
					return false;
				break;
		}

		var selectedItem = $('#<%=drpOperations.ClientID %> option:selected').val();
		switch (selectedItem) {
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterOperations.SendSmsToSender%>':
				if (!validateRequiredFields('sendSms'))
					return false;
				else
					break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterOperations.SendSmsToGroup%>':
				if (!validateRequiredFields('sendSms'))
					return false;
				if ($("#<%=hdnGroupGuid.ClientID%>")[0].value == '') {
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PleaseSelectGroup")%>', '', 'alert', 'danger');
					return false;
				}
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsFilterOperations.SendSmsFromFormat%>':
				if (!validateRequiredFields('SendSmsFromFormat'))
					return false;
				if (!$("#<%=drpSenderNumber.ClientID%> option:selected").val()) {
					messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelectSenderNumber")%>', '', 'alert', 'danger');
					return false;
				}
				break;
		}
	}

</script>
<div class="col-md-5">
	<div class="width-100 label label-info label-xlg arrowed-in arrowed-in-right"><i class="ace-icon fa fa-circle light-red"></i><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GeneralInformation") %></div>
	<hr />
	<div class="form-inline" role="form">
		<div class="form-group">
			<label class="control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Title") %></label>
			<div>
				<asp:TextBox ID="txtTitle" runat="server" isrequired="true" validationset="saveFilter" class="form-control input-sm"></asp:TextBox>
			</div>
		</div>
		<div class="form-group">
			<label class="control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RecieverNumber") %></label>
			<div class="col-md-12">
				<asp:DropDownList ID="drpNumber" runat="server" class="form-control input-sm" Style="width: 100%"></asp:DropDownList>
			</div>
		</div>
	</div>
	<br />
	<div class="form-horizontal bg-warning" role="form" style="padding: 10px">
		<div class="form-group">
			<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("StartDate")%></label>
			<div class="col-sm-9">
				<SMS:DateTimePicker ID="dtpStartDate" IsRequired="true" ValidationSet="saveFilter" runat="server"></SMS:DateTimePicker>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("EndDate")%></label>
			<div class="col-sm-9">
				<SMS:DateTimePicker ID="dtpEndDate" IsRequired="true" ValidationSet="saveFilter" runat="server"></SMS:DateTimePicker>
			</div>
		</div>
	</div>
	<div class="form-inline" role="form">
		<div class="form-group">
			<label class="control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SenderNumber") %></label>
			<div class="col-md-12">
				<asp:DropDownList ID="drpTypeConditionSender" runat="server" class="form-control input-sm" Style="width: 100%"></asp:DropDownList>
			</div>
		</div>
		<div class="form-group">
			<label class="control-label">&nbsp;</label>
			<div>
				<asp:TextBox ID="txtConditionSender" runat="server" CssClass="form-control input-sm"></asp:TextBox>
			</div>
		</div>
	</div>
	<br />
	<div class="form-horizontal bg-warning" role="form" style="padding: 10px">
		<div class="form-group">
			<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompetitionMember") %></label>
			<div class="col-sm-9">
				<select id="drpScope" style="width: 200px"></select>
				<div id="treeScope" style="width: 250px">
					<div style="padding: 10px">
						<GeneralTools:TreeView ID="treePhoneBook" runat="server" ShowLines="true" OnClick="clickNodeScope(node);"
							Formatter="return nodeTextFormat(node);" SuccessLoad="return scopeSuccessLoad(node,data);"></GeneralTools:TreeView>
					</div>
				</div>
			</div>
		</div>
	</div>
	<div class="clearfix form-actions">
		<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Register" OnClick="btnSave_Click" />
		<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
	</div>
</div>
<div class="col-md-5">
	<div class="width-100 label label-info label-xlg arrowed-in arrowed-in-right"><i class="ace-icon fa fa-circle light-red"></i><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FilterConditions") %></div>
	<hr />
	<div class="form-horizontal" role="form">
		<div class="form-group">
			<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("If") %></label>
			<div class="col-sm-9">
				<asp:DropDownList ID="drpConditions" runat="server" class="form-control input-sm"></asp:DropDownList>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-3 control-label"></label>
			<div class="col-sm-9">
				<asp:TextBox ID="txtCondition" class="form-control input-sm" validationSet="condition" isRequired="true" runat="server"></asp:TextBox>
				<div id="conditionGroup" style="display: none">
					<select id="drpConditionGroup"></select><br />
					<div id="treeConditionGroup" style="width: 338px;">
						<div style="padding: 10px">
							<GeneralTools:TreeView ID="treeCondition" runat="server" ShowLines="true" OnClick="clickNodeConditionGroup(node);"
								Formatter="return nodeTextFormat(node);" SuccessLoad="return conditionGroupSuccessLoad(node,data);"></GeneralTools:TreeView>
						</div>
					</div>
					<select id="drpPhoneBookFields" style="margin-top: 10px; width: 338px;"></select>
				</div>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-3 control-label"></label>
			<div class="col-sm-9" style="text-align: center">
				<span class="fa fa-3x fa-arrow-down green"></span>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Then") %></label>
			<div class="col-sm-9">
				<asp:DropDownList ID="drpOperations" runat="server" class="form-control input-sm" validationSet="saveFilter" isRequired="true"></asp:DropDownList>
			</div>
		</div>
		<div>
			<div class="form-group">
				<label class="col-sm-3 control-label"></label>
				<div class="col-sm-9">
					<asp:TextBox ID="txtOpration" class="form-control input-sm" validationSet="saveFilter" isRequired="true" runat="server"></asp:TextBox><br />
					<div id="operationGroup" style="display: none">
						<select id="drpOperationGroup"></select><br />
						<div id="tree" style="width: 200px;">
							<div style="padding: 10px">
								<GeneralTools:TreeView ID="treeOperationGroup" runat="server" ShowLines="true" OnClick="clickNodeOperationGroup(node);"
									Formatter="return nodeTextFormat(node);" SuccessLoad="return operationGroupSuccessLoad(node,data);"></GeneralTools:TreeView>
							</div>
						</div>
					</div>
					<asp:DropDownList ID="drpTrafficRelay" runat="server" class="form-control input-sm" Style="display: none"></asp:DropDownList>
					<div id="sendSms" style="display: none">
						<hr />
						<asp:DropDownList ID="drpSenderNumber" validationSet="sendSms" IsRequired="true" runat="server" class="form-control input-sm"></asp:DropDownList><br />
						<div id="group" style="margin: 0 0 10px 0;">
							<select id="drpGroup"></select><br />
							<div id="treeGroup" style="width: 200px;">
								<div style="padding: 10px">
									<GeneralTools:TreeView ID="treeGroups" runat="server" ShowLines="true" OnClick="clickNodeGroup(node);"
										Formatter="return nodeTextFormat(node);" SuccessLoad="return groupSuccessLoad(node,data);"></GeneralTools:TreeView>
								</div>
							</div>
						</div>
						<div id="formatSetting" style="display: none">
							<asp:DropDownList ID="drpAccpetFormat" runat="server" class="form-control input-sm" validationSet="SendSmsFromFormat" IsRequired="true"></asp:DropDownList>
							<asp:DropDownList ID="drpRejectFormat" runat="server" class="form-control input-sm" Style="margin-top: 10px;" validationSet="SendSmsFromFormat" IsRequired="true"></asp:DropDownList>
						</div>
						<GeneralTools:SmsBodyControl runat="server" ID="txtSmsBody" ValidationSet="sendSms" IsRequired="true"></GeneralTools:SmsBodyControl>
						<asp:TextBox ID="txtUrl" placeholder="" runat="server" class="form-control input-sm"></asp:TextBox>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>

<script type="text/javascript">
	function nodeTextFormat(node) {
		var text = node.text;
		if (node.attributes) {
			text += '&nbsp;<span style=\'color:blue\'>(' + node.attributes.count + ')</span>';
			if (node.attributes.type == '<%=(int)Arad.SMS.Gateway.Business.PhoneBookGroupType.Vas%>')
                text += '&nbsp;<span style=\'color:red\'>(<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("VASGroup") %>)</span>';
		}
		return text;
	}


	function operationGroupSuccessLoad(node, data) {
		var text = getAjaxResponse("GetPhoneBookName", "PhoneBookGuid=" + $("#<%=hdnOperationGroupGuid.ClientID%>")[0].value);
		$('#drpOperationGroup').combo('setText', text).combo('hidePanel');
	}

	function groupSuccessLoad(node, data) {
		var text = getAjaxResponse("GetPhoneBookName", "PhoneBookGuid=" + $("#<%=hdnGroupGuid.ClientID%>")[0].value);
		$('#drpGroup').combo('setText', text).combo('hidePanel');

	}

	function scopeSuccessLoad(node, data) {
		var text = getAjaxResponse("GetPhoneBookName", "PhoneBookGuid=" + $("#<%=hdnScopeGuid.ClientID%>")[0].value);
		$('#drpScope').combo('setText', text).combo('hidePanel');
	}

	function conditionGroupSuccessLoad(node, data) {
		var text = getAjaxResponse("GetPhoneBookName", "PhoneBookGuid=" + $("#<%=hdnConditionField.ClientID%>")[0].value);
		$('#drpConditionGroup').combo('setText', text).combo('hidePanel');
	}

	function clickNodeOperationGroup(node) {
		var s = node.text;
		$('#drpOperationGroup').combo('setText', s).combo('hidePanel');
		$("#<%=hdnOperationGroupGuid.ClientID%>")[0].value = node.id;
	}

	function clickNodeGroup(node) {
		var s = node.text;
		$('#drpGroup').combo('setText', s).combo('hidePanel');
		$("#<%=hdnGroupGuid.ClientID%>")[0].value = node.id;
	}

	function clickNodeScope(node) {
		var s = node.text;
		$('#drpScope').combo('setText', s).combo('hidePanel');
		$("#<%=hdnScopeGuid.ClientID%>")[0].value = node.id;
	}

	function clickNodeConditionGroup(node) {
		$("#drpPhoneBookFields").empty();

		var s = node.text;
		$('#drpConditionGroup').combo('setText', s).combo('hidePanel');

		var retVal = getAjaxResponse('GetPhonebookField', "PhoneBookGuid=" + node.id);
		var obj = jQuery.parseJSON(retVal);

		var option = '';
		for (counter = 0; counter < obj.length; counter++) {
			option = "<option value='" + obj[counter].Name + "'>" + obj[counter].Title + "</option>";
			$("#drpPhoneBookFields").append(option);
		}

		$("#<%=hdnConditionGroupGuid.ClientID%>")[0].value = node.id;
		$("#<%=hdnFieldName.ClientID%>")[0].value = $("#drpPhoneBookFields option:selected").val();
	}

	$(function () {
		$('#drpOperationGroup').combo({
			required: true,
			editable: false,
			width: 338,
			onShowPanel: function () {
				var operationGroupGuid = "'" + $("#<%=hdnOperationGroupGuid.ClientID%>")[0].value + "'";
				treeOperationGroup.FindNode(operationGroupGuid);
			}
		});
		$('#tree').appendTo($('#drpOperationGroup').combo('panel'));

		$('#drpGroup').combo({
			required: true,
			editable: false,
			width: 338,
			onShowPanel: function () {
				var groupGuid = "'" + $("#<%=hdnGroupGuid.ClientID%>")[0].value + "'";
				treeGroups.FindNode(groupGuid);
			}
		});
		$('#treeGroup').appendTo($('#drpGroup').combo('panel'));

		$('#drpScope').combo({
			required: true,
			editable: false,
			width: 338,
			onShowPanel: function () {
				var scopeGuid = "'" + $("#<%=hdnScopeGuid.ClientID%>")[0].value + "'";
				treePhoneBook.FindNode(scopeGuid);
			}
		});
		$('#treeScope').appendTo($('#drpScope').combo('panel'));

		$('#drpConditionGroup').combo({
			required: true,
			editable: false,
			width: 338,
			height: 27,
			panelWidth: 400,
			onShowPanel: function () {
				var groupGuid = "'" + $("#<%=hdnConditionGroupGuid.ClientID%>")[0].value + "'";
				treeCondition.FindNode(groupGuid);
			}
		});
		$('#treeConditionGroup').appendTo($('#drpConditionGroup').combo('panel'));

		$(function () {
			setOption();
			$('#<%=drpOperations.ClientID %>').change(function () {
				setOption();
			});

			//setCondition();
			$('#<%=drpConditions.ClientID %>').change(function () {
				setCondition();
			});

			$('#drpPhoneBookFields').change(function () {
				$("#<%=hdnFieldName.ClientID%>")[0].value = $("#drpPhoneBookFields option:selected").val();
			});
		});
	});
</script>
