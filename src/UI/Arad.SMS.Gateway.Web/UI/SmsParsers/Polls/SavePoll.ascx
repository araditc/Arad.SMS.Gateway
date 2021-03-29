<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SavePoll.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsParsers.Polls.SavePoll" %>
<%@ Register Src="~/UI/Controls/BootstrapDateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="SMS" %>


<input id="hdnKeywords" type="hidden" runat="server" />
<style type="text/css">
	.radio label, .checkbox label {
		padding-right: 0;
	}
</style>

<script type="text/javascript">
	function addKeyword() {
		if (!validateRequiredFields('addOption')) {
			return;
		}
		var keyword = $('#txtKeyword').val();
		var number = $("#<%=drpSenderNumber.ClientID %> option:selected").val();
		var isDuplicatekey = getAjaxResponse("IsDuplicateSmsParserKey", "NumberGuid=" + number + "&Key=" + keyword);
		if (isDuplicatekey == "1" || isDuplicatekey == "true") {
            messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DuplicateKeyword") %>', '', 'alert', 'danger');
			return;
		}

		var keywordTitle = $('#txtKeywordTitle').val();
		var phoneBook = $("#<%=drpPhoneBook.ClientID %> option:selected").val();

		addRow(keywordTitle, keyword, phoneBook);
	}

	function addRow(keywordTitle, keyword, phoneBook) {
		gridKeywords.AddRow({
			Title: keywordTitle,
			Key: keyword,
			ReferenceGuid: phoneBook,
			Action: '<span class="ui-icon fa fa-trash-o red" title="Delete" onclick="deleteGridKeywordsRow(event);"></span>'
		});
	}

	function deleteGridKeywordsRow(e) {
		gridKeywords.Event = e;
		if (gridKeywords.IsSelectedRow()) {
			gridKeywords.DeleteRow(gridKeywords.GetSelectedRowID());
		}
	}

	function save() {
		if (!validateRequiredFields('savePoll'))
			return false;

		if (gridKeywords.GetRecordCount() == 0) {
			messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelcectCompetitionOptions")%>', '', 'alert', 'danger');
			return false;
		}

		var keywords = gridKeywords.GetAllRowData(["Title",
																								"Key",
																								"ReferenceGuid"]);

		$("#<%=hdnKeywords.ClientID%>")[0].value = keywords;

		return true;
	}
</script>
<div class="row">
	<div class="col-md-5">
		<div class="width-100 label label-info label-xlg arrowed-in arrowed-in-right"><i class="ace-icon fa fa-circle light-red"></i><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PollSpecifications")%></div>
		<div class="form-horizontal" role="form">
			<hr />
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompetitionFromMobile") %></label>
				<div class="col-sm-9">
					<asp:DropDownList ID="drpSenderNumber" class="form-control input-sm" validationSet="savePoll" isRequired="true" runat="server"></asp:DropDownList>
                    
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PollTitle")%></label>
				<div class="col-sm-9">
					<asp:TextBox ID="txtTitle" class="form-control input-sm" validationSet="savePoll" isRequired="true" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("StartDate")%></label>
				<div class="col-sm-9">
					<SMS:DateTimePicker ID="dtpStartDate" IsRequired="true" ValidationSet="savePoll" runat="server"></SMS:DateTimePicker>
                </div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("EndDate")%></label>
				<div class="col-sm-9">
					<SMS:DateTimePicker ID="dtpEndDate" IsRequired="true" ValidationSet="savePoll" runat="server"></SMS:DateTimePicker>
                </div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label">
					<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompetitionMember") %>
					<a href="javascript:void(0)" class="fa fa-info-circle orange easyui-tooltip" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompetitionGroupAllert") %>"></a>
				</label>
				<div class="col-sm-9">
					<asp:DropDownList ID="drpScope" runat="server" class="form-control input-sm"></asp:DropDownList>
				</div>
			</div>
		</div>
		<div class="col-md-12">
			<div class="width-100 label label-info label-xlg arrowed-in arrowed-in-right"><i class="ace-icon fa fa-circle light-red"></i><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompetitionResponse") %></div>
			<div role="form" style="height: 250px">
				<hr />
				<div class="col-md-6 col-xs-6">
					<div class="form-group">
						<asp:DropDownList ID="drpReplyPrivateNumber" runat="server" class="form-control input-sm" Width="220"></asp:DropDownList><br />
						<GeneralTools:SmsBodyBox runat="server" ID="txtReplySmsText" IsRequired="true" Width="220" Height="100" PlaceHolder=""></GeneralTools:SmsBodyBox>
					</div>
				</div>
				<div class="col-md-6 col-xs-6">
					<div class="form-group">
						<asp:DropDownList ID="drpDuplicatePrivateNumber" runat="server" class="form-control input-sm" Width="220"></asp:DropDownList><br />
						<GeneralTools:SmsBodyBox runat="server" ID="txtDuplicateUserSmsText" IsRequired="true" Width="220" Height="100" PlaceHolder=""></GeneralTools:SmsBodyBox>
					</div>
				</div>
			</div>
		</div>
	</div>
	<div class="col-md-7">
		<div class="width-100 label label-info label-xlg arrowed-in arrowed-in-right"><i class="ace-icon fa fa-circle light-red"></i><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompetitionOptions")%></div>
		<div class="form-inline" role="form" style="margin-right: 13px;">
			<hr />
			<div class="form-group">
				<label class="control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("OptionName")%></label>
				<div>
					<input type="text" isrequired="true" validationset="addOption" class="form-control input-sm" id="txtKeywordTitle">
				</div>
			</div>
			<div class="form-group">
				<label class="control-label">
					<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Keyword")%>
					<a href="javascript:void(0)" class="fa fa-info-circle orange easyui-tooltip" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompetitionAnswerText") %>"></a>
				</label>
				<div>
					<input type="text" isrequired="true" validationset="addOption" class="form-control input-sm" id="txtKeyword">
				</div>
			</div>
			<div class="form-group">
				<label class="control-label">
					<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SaveInPhoneBook")%>
					<a href="javascript:void(0)" class="fa fa-info-circle orange easyui-tooltip" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompetitionSaveToGroupText") %>"></a>
				</label>
				<div>
					<asp:DropDownList ID="drpPhoneBook" runat="server" class="form-control input-sm" Style="width: 150px;"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="control-label">&nbsp;</label>
				<div>
					<span class="fa fa-3x fa-plus green" title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AddRecipient") %>" onclick="addKeyword();" style="cursor: pointer"></span>
				</div>
			</div>
		</div>

		<div class="clear"></div>
		<div class="col-md-12">
			<GeneralTools:DataGrid runat="server" ID="gridKeywords" DefaultSortField="Type" ListCaption="OptionsList" ShowRowNumber="true" DisableNavigationBar="true" ListHeight="150">
				<Columns>
					<GeneralTools:DataGridColumnInfo FieldName="Title" Caption="Title" Sortable="false" Align="Center" CellWidth="70" />
					<GeneralTools:DataGridColumnInfo FieldName="Key" Caption="Keyword" Sortable="false" Align="Center" CellWidth="70" />
					<GeneralTools:DataGridColumnInfo FieldName="ReferenceGuid" Caption="PhoneBook" Hidden="true"/>
					<GeneralTools:DataGridColumnInfo FieldName="Action" Caption="Action" Sortable="false" CellWidth="50" Align="Center" FormattingMethod="ImageButton" />
				</Columns>
			</GeneralTools:DataGrid>
			<hr />
			<div class="buttonControlDiv">
				<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Register" />
				<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
			</div>
		</div>
	</div>
</div>
