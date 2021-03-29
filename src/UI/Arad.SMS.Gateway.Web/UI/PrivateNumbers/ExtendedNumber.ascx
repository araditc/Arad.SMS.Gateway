<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExtendedNumber.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PrivateNumbers.ExtendedNumber" %>

	<script type="text/javascript">
		$(function () {
			var addAccountsToDom = function (accounts) {
				var accountTemplate = $("#accountTemplate").template("accountTemplate");
				$.tmpl(accountTemplate, accounts).appendTo("#accountList");
			};

			$.ajax({
				url: "/homepages/arad/index.aspx/GetOnlineAccount",
				contentType: 'application/json; charset=utf-8',
				dataType: "json",
				type: 'POST',
				async: false,
				cache: false
			}).done(function (data) {
				var accountData = { items: data.d };
				addAccountsToDom(accountData);
			});
		});

		function getSelectedAccount() {
			var accountGuid = $(".control-group input:checked").attr('guid');
			if (accountGuid != '<%=Guid.Empty%>') {
				$("#<%=hdnAccountGuid.ClientID%>")[0].value = accountGuid;
				return true;
			}
			else
				return false;
		}
	</script>

<script id="accountTemplate" type="text/html">
		<div class="control-group">
			<label class="control-label bolder blue"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("choosePaymentGateway") %></label>
			{{each items}}
				<div class="radio">
					<label>
						<input name="form-field-radio" type="radio" class="ace" guid="${Guid}" />
						<div class="row lbl" style="border: 1px solid silver; border-radius: 5px; padding: 10px; width: 400px;">
							{{if Bank == 14}}
								<img src="/pic/mellat.png" style="vertical-align: middle" />
							{{else Bank == 15}}
									<img src="/pic/melli.png" style="vertical-align: middle" />
							{{/if}}
								<span><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Account") %> ${AccountNo}</span>
							<span style="font-weight: bold"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("OwnerAccountnumber") %> ${Owner}</span>
						</div>
					</label>
				</div>
			{{/each}}
		</div>
	</script>

<asp:HiddenField ID="hdnAccountGuid" runat="server" />

<div class="main-container" style="margin-top: 50px;">
		<div class="main-content">
			<div class="row">
				<div class="col-sm-10 col-md-10">
					<div class="login-container">
						<div class="space-6"></div>
						<div class="position-relative">
							<div class="widget-body">
								<div class="widget-main">
									<h4 class="header red lighter bigger">
										<i class="ace-icon fa fa-shopping-cart"></i>
										<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ExtendedPrivateNumber") %>
									</h4>
									<div class="space-3"></div>
									<asp:Label ID="lblMessage" runat="server" Text="" Style="color: maroon; font-size: large;"></asp:Label>
									<div>
										<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ExtendedPrivateNumberPrice") %>
									</div>
									<div>
										<fieldset>
											<label class="block clearfix">
												<span class="block input-icon input-icon-right">
													<asp:TextBox ID="txtPrice" runat="server" class="form-control input-sm"></asp:TextBox>
													<i class="ace-icon fa fa-usd"></i>
												</span>
											</label>
											<div class="space-4"></div>
											<div id="accountList">
											</div>
											<div class="space-10"></div>
											<div class="clearfix">
												<asp:Button ID="btnPayment" class="btn btn-success" runat="server" Text="Pay" OnClientClick="return getSelectedAccount();" OnClick="btnPayment_Click" />
												<asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
											</div>
										</fieldset>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
