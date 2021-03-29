<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendP2PSms.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsSends.SendP2PSms" %>

<%@ Register Src="~/UI/Controls/BootstrapDateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="SMS" %>
<%@ Register Src="~/UI/UserFields/SmsFormat.ascx" TagPrefix="SMS" TagName="SmsFormat" %>

<script src="/script/jquery.ajaxupload.js"></script>
<script src="/script/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>

<asp:HiddenField ID="hdnFilePath" runat="server" />
<asp:HiddenField ID="hdnSmsFormat" runat="server" />

<script type="text/javascript">
	$(document).ready(function () {
		$.ajaxUploadSettings.name = 'upload';
		$('#fileUpload').ajaxUploadPrompt({
			url: '/handler/UploadAndGetColumnInfo.ashx',
			beforeSend: function () { },
			onprogress: function (e) {
				$(".widget-box").setOverlay('/pic/loader.gif');
			},
			error: function (result) {
				$("#fileUpload").css({ 'background-color': '#F2DEDE' });

				$("#divResult").addClass("bg-danger");
				$("#divResult").html(result.responseText);
				$(".widget-box").removeOverlay();
			},
			success: function (data) {
				$('#<%=hdnFilePath.ClientID%>')[0].value = data.Path;

				$("#fileUpload").css({ 'background-color': '#D6ECCD' });
				$(".ace-file-name").attr('data-title', data.FileName);
				$("#divResult").removeClass();
				$("#divResult").html('');
				$(".widget-box").removeOverlay();

				$("#list").empty();
				for (columnCounter = 0; columnCounter < data.lstFileColumn.length; columnCounter++) {
					$("<li field='[F" + data.lstFileColumn[columnCounter].index + "]' draftText='[" + data.lstFileColumn[columnCounter].name + "]' class='Field'>" + data.lstFileColumn[columnCounter].name + "</li>").appendTo("#list")
				}

				initializeColumn();

			}
		});

		$('#fuelux-wizard').ace_wizard({}).on('change', function (e, info) {
			if (info.step == 1 && info.direction == 'next') {
				if (!validateRequiredFields('step1'))
					return false;

				var filePath = $('#<%=hdnFilePath.ClientID%>')[0].value;
				if (filePath == '')
					return false;

				$('.btn-next').hide();
			}
			if (info.step == 2 && info.direction == 'previous') {
				$('.btn-next').show();
			}
		}).on('finished.fu.wizard', function (e) {
			<%= Page.ClientScript.GetPostBackEventReference(btnSave, String.Empty) %>;
		}).on('stepclick.fu.wizard', function (e) {
			e.preventDefault();
		});

		$(".dateTime").datepicker({
			changeMonth: true,
			changeYear: true,
			minDate: 0,
			maxDate: "+45D",
		});
	});

	function initializeColumn() {
		$("#sortable").sortable({ revert: false, cursor: 'hand', tolerance: 'pointer' });
		jQuery("#sortable").disableSelection();
		$("#sortable").bind("sortstop", function (event, ui) {
			jQuery("#sortable > li").each(function (n, item) {
				jQuery(item).unbind();
			});
			jQuery("#sortable > li").each(function (n, item) {
				var hasCheckBox = jQuery(item).find('input:checkbox');
				if (hasCheckBox.length == 0)
					jQuery(item).append('<br/><input id="Checkbox1" style="margin-top:2px;margin-left:0px"  type="checkbox" />');
			});

			var items = $("#sortable").find('li');
			$("#txtDraft")[0].value = '';
			for (i = 0; i < items.length; i++) {
				if ($(items[i]).attr('field') == 'USERTEXT') {
					$("#txtDraft")[0].value += $(items[i]).attr('title') + ' ';
				}
				else {
					$("#txtDraft")[0].value += $(items[i]).attr('draftText') + ' ';
				}
			}
		});
		$("#list li").draggable({
			connectToSortable: "#sortable",
			helper: "clone",
			revert: "invalid"
		});
		$("ul, li").enableSelection();
	}

	function addCustomText() {
		$('#divUserText').modal('show');
	}
</script>

<div class="col-xs-12 col-md-12">
	<div class="widget-box">
		<div class="widget-body">
			<div class="widget-main">
				<div id="fuelux-wizard" data-target="#step-container">
					<ul class="wizard-steps">
						<li data-target="#step1" class="active">
							<span class="step">1</span>
							<span class="title"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelectFile") %></span>
						</li>
						<li data-target="#step2" class="">
							<span class="step">2</span>
							<span class="title"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsSetting") %></span>
						</li>
					</ul>
				</div>
				<hr>
				<div class="step-content pos-rel" id="step-container">
					<div class="step-pane active" id="step1" style="height: 450px;">
						<div class="container-fluid">
							<div class="row">
								<div class=" col-md-4">
									<div class="form-horizontal" role="form">
										<div class="form-group">
											<div class="input-group">
												<span class="input-group-addon" style="background-color: rgb(80, 168, 226); color: #fff;"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sender") %></span>
												<asp:DropDownList ID="drpSenderNumber" runat="server" validationSet="step1" IsRequired="true" CssClass="form-control input-sm"></asp:DropDownList>
											</div>
										</div>
										<div class="form-group"></div>
										<div class="form-group"></div>
										<div class="form-group">
											<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("File")%></label>
											<div class="col-sm-10">
												<label class="ace-file-input">
													<span id="fileUpload" class="ace-file-container" data-title="Choose"><span class="ace-file-name" data-title="No File ..."><i class=" ace-icon fa fa-upload"></i></span></span><a class="remove" href="#"><i class=" ace-icon fa fa-times"></i></a>
												</label>
											</div>
										</div>
										<div class="form-group"></div>
										<div class="form-group"></div>
										<div class="form-group">
											<div class="col-sm-12">
												<SMS:DateTimePicker ID="dtpSendDateTime" ValidationSet="step1" IsRequired="true" runat="server"></SMS:DateTimePicker>
											</div>
										</div>
										<div class="clear"></div>
										<div id="divResult" class="div-save-result"></div>
									</div>
								</div>
								<div class=" col-md-1"></div>
								<div class=" col-md-8">
									
                                        <% if(Session["Language"].ToString() == "en") { %>
                                        <div class="panel panel-success" style="height: 450px; overflow: auto; text-align: left;">
                                        <% }  else
                                           {%>
                                        <div class="panel panel-success" style="height: 450px; overflow: auto">
                                        <%  } %>
										<div class="panel-heading"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Guide") %></div>
										<div class="panel-body">
											<p>شما توسط سرویس <strong>ارسال پیامک پویا</strong> قادر خواهید بود به صورت هوشمند متن ارسالی را برای هر شماره موبایل شخصی سازی نمایید.</p>
											<p style="font-weight: bold; color: darkred">فرمت ستون های فایل حتما از نوع TEXT بوده و ستون اول الزاما شامل شماره گیرنده باشد</p>
											<p style="font-weight: bold;">دقت نمایید که نام جدولی که شماره ها در آن قرار دارند باید Sheet1 باشد</p>
											<p>به عنوان مثال فرض کنید مایل هستید که اعتبار هر یک از کاربرانتان را برای آنها توسط پیامک به صورت زیر ارسال نمایید:</p>
											<div style="padding-right: 20px;">
												<p>آقای مهدی امامی اعتبار شما 123000 ریال می باشد</p>
												<p>خانم مریم اسدی اعتبار شما 2458000 ریال می باشد</p>
												<p>...</p>
											</div>
											<p>برای اینکار کافیست یک فایل اکسل ایجاد نمایید و ستون های حاوی اطلاعات را در آن لحاظ کنید</p>
											<p><strong>لازم به ذکر است فایل اکسل می تواند شامل ستون های مختلف باشد و شما متن پیامک را با توجه به متن ستون ها تنظیم نمایید</strong></p>
											<p>
												سپس فایل را انتخاب کرده و دکمه "مرحله بعد" را بفشارید
												سامانه به صورت خودکار تمامی ستون های فایل اکسل را به شما نمایش می دهد ، شما میتوانید از هر یک از این ستون ها داخل متن پیامک استفاده نمایید		
											</p>
											<p><strong>دقت بفرمایید که سطر اول فایل اکسل خوانده نمی شود بنابراین میتوانید از آن برای درج عنوان استفاده کنید</strong></p>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
					<div class="step-pane" id="step2" style="height: 450px;">
						<div class="container-fluid">
							<div id="confirmSendSms" class="row">
								<div class="col-md-12 col-xs-12">
									<div id="pnlManageFormat">
										<div class="row">
											<div class="ui-state-highlight ui-corner-all" style="margin-bottom: 2px; padding: 0; line-height: 2; font-weight: bold;">
												<p style="margin: 2px;">
													<span class="ui-icon ui-icon-info" style="float: right; margin-left: 5px;"></span>
													<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendP2PSmsNotice")%>
												</p>
											</div>
											<div class="col-xs-2 col-md-2">
												<ul id="list" class="FieldList"></ul>
											</div>
											<div class="col-xs-10 col-md-10">
												<div class="row">
													<div class="col-xs-6 col-md-8">
														<img title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Delete")%>" id="btnDletetField" class="gridImageButton" onclick="deleteSelectedItem();" src="/pic/delete24px.png" />
														<img title="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AddCustomText")%>" id="btnUserText" class="gridImageButton" onclick="addCustomText();" src="/pic/UserText.png" />
														<input id="checkBoxSelectAll" type="checkbox" onclick="selectAll(this.checked);" /><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelectAll") %>
													</div>
												</div>
												<div class="row">
													<ul id="sortable" style="width: 100%; height: 250px; border: 1px solid #438eb9;"></ul>
													<textarea id="txtDraft" class="input" style="width: 100%; margin: 5px 0 5px 0;" readonly="readonly" rows="4" placeholder="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Preview") %>"></textarea>
												</div>
											</div>
											<div class="buttonControlDiv">
												<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-success" Text="Register" OnClientClick="return generateFormat();" />
											</div>
										</div>

									</div>

									<div class="modal fade bs-example-modal-sm" id="divUserText" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
										<div class="modal-dialog modal-sm">
											<div class="modal-content">
												<div class="modal-header">
													<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
													<h4 class="modal-title" id="myModalLabel"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AddCustomText") %></h4>
												</div>
												<div class="modal-body">
													<div class="form-horizontal" role="form">
														<div class="form-group">
															<div>
																<textarea id="txtUserText" class="form-control" rows="3"></textarea>
															</div>
														</div>
													</div>
												</div>
												<div class="modal-footer">
													<input id="btnAdd" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Register") %>" class="btn btn-primary" onclick="addUserText()" />
													<input id="btnCancel" type="button" value="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %>" class="btn btn-default" data-dismiss="modal" />
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
				<hr>
				<div class="wizard-actions">
					<a href="#" class="btn btn-prev" disabled="disabled"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("BackStep") %>
						<i class="ace-icon fa fa-arrow-right icon-on-right"></i>
					</a>
					<a href="#" class="btn btn-success btn-next" data-last="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RegisterRequest") %>"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NextStep") %>
						<i class="ace-icon fa fa-arrow-left icon-on-left"></i>
					</a>
				</div>
			</div>
		</div>
	</div>
</div>

<script type="text/javascript">
	function refreshSortableList() {
		var items = $("#sortable").find('li');
		$("#txtDraft")[0].value = '';
		for (i = 0; i < items.length; i++) {
			if ($(items[i]).attr('field') == 'USERTEXT') {
				$("#txtDraft")[0].value += $(items[i]).attr('title') + ' ';
			}
			else {
				$("#txtDraft")[0].value += $(items[i]).attr('draftText') + ' ';
			}
		}
	}

	function selectAll(status) {
		var items = $("#sortable").find('li');
		for (i = 0; i < items.length; i++) {
			var checkBox = $(items[i]).find('input:checkbox');
			$(checkBox).attr('checked', status);
		}
	}

	function deleteSelectedItem() {
		var items = $("#sortable").find('li');
		for (i = 0; i < items.length; i++) {
			var checkBox = $(items[i]).find('input:checkbox');
			if ($(checkBox).attr('checked')) {
				$(items[i]).remove();
			}
		}
		refreshSortableList();
	}

	function addUserText() {
		var text = $("#txtUserText")[0].value;
		if (text.length > 10) {
			var subStringText = text.substring(0, 10);
			$("#sortable").append('<li field="USERTEXT" title="' + text + '" class="Field">' + subStringText + '...<br/><input id="Checkbox1" type="checkbox" /></li>');
		}
		else
			$("#sortable").append('<li field="USERTEXT" title="' + text + '" class="Field">' + text + '<br/><input id="Checkbox1" type="checkbox" /></li>');

		$("#txtUserText")[0].value = "";
		refreshSortableList();
		$('#divUserText').modal('hide');
	}

	function generateFormat() {
		try {
			var items = $("#sortable").find('li');
			if (items.length <= 0) {
				messageBox('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SetSmsTextFormat") %>', '', 'alert', 'danger');
				return false;
			}

			var format = "";
			var items = $("#sortable").find('li');
			for (i = 0; i < items.length; i++) {
				if ($(items[i]).attr('field') == 'USERTEXT')
					format += "N'" + $(items[i]).attr('title') + "'," + "' ',";
				else
					format += $(items[i]).attr('field') + "," + "' ',";
			}

			$("#<%=hdnSmsFormat.ClientID%>")[0].value = format;
			return true;
		} catch (err) {
			return false;
		}
	}
</script>
