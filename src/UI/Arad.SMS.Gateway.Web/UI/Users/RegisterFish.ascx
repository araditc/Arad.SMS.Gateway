<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterFish.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.RegisterFish" %>
<%@ Register Src="~/UI/Controls/BootstrapDatePicker.ascx" TagName="DatePicker" TagPrefix="SMS" %>

<script type="text/javascript">
	$(document).ready(function () {
		$(".form").hide();
		gridDefaultColor();
	});

	function showAccountForm() {
		$("#selectPayment").hide();
		$(".form").hide();
		$("#divAccount").show();
		$("#Tariff").show();
	}

	function showOnlineForm() {
		$("#selectPayment").hide();
		$(".form").hide();
		$("#divOnline").show();
		$("#Tariff").show();
	}

	function cancel() {
		gridDefaultColor();
		$("#selectPayment").show();
		$("#divOnline").hide();
		$("#divAccount").hide();
		$("#Tariff").hide();
	}

	function gridDefaultColor() {
		$('#<%=gridTariff.ClientID%> tr').css("background-color", "White").css("color", "#284775");
		$('#<%=gridTariff.ClientID%> tr:first').css("background-color", "#5D7B9D").css("color", "White");
		$('#<%=gridTariff.ClientID%> tr:odd').css("background-color", "#F7F6F3").css("color", "#333333");
	}


	function checkSmsCount(type) {
		var basePrice = 0;
		var count = 0;
		var decreaseTax = 'true';
		var tax = parseInt('<%=Arad.SMS.Gateway.Facade.Setting.GetValue((int)Arad.SMS.Gateway.Business.MainSettings.Tax)%>');
		var taxAmount;
		var amount = 0;
		var rowAmount = 0;
		var price = 0;
		var minPrice = 0;
		var maxPrice = 0;
		var min = 0;
		var max = 0;
		try {
			if (type == '<%=(int)Arad.SMS.Gateway.Business.TypeFish.OnLine%>') {
				count = $("#<%=txtOnlineSmsCount.ClientID%>")[0].value;
				count = parseInt(count.replace(/\,/g, ''));
			}
			else if (type == '<%=(int)Arad.SMS.Gateway.Business.TypeFish.Account%>') {
				amount = $("#<%=txtAccountAmount.ClientID%>")[0].value;
				amount = parseInt(amount.replace(/\,/g, ''));

				if ($("#<%=gridTariff.ClientID%> tr").length > 1) {
					$("#<%=gridTariff.ClientID%> tr").not(':first').each(function () {
						price = parseInt($(this).find('span.price').html().replace(/\,/g, ''));
						min = parseInt($(this).find('span.min').html().replace(/\,/g, ''));
						max = parseInt($(this).find('span.max').html().replace(/\,/g, ''));
						decreaseTax = $(this).find('span.tax').html().toString();

						minPrice = min * price;
						maxPrice = max * price;

						if (decreaseTax.toLowerCase() == 'true' || decreaseTax == 1)
							rowAmount = Math.round(amount / ((tax / 100) + 1));

						if (rowAmount >= minPrice && rowAmount <= maxPrice) {
							count = Math.round(rowAmount / price);
							return false;
						}
					});
				}
				else {
					basePrice = parseFloat('<%=basePrice%>');
					decreaseTax = '<%=decreaseTax.ToString().ToLower()%>';

					if (decreaseTax.toLowerCase() == 'true' || decreaseTax == 1)
						amount = Math.round(amount / ((tax / 100) + 1));

					count = Math.round(amount / basePrice);
				}
			}


			if ($("#<%=gridTariff.ClientID%> tr").length > 1) {
				gridDefaultColor();

				$("#<%=gridTariff.ClientID%> tr").not(':first').each(function () {
					var min = parseInt($(this).find('span.min').html().replace(/\,/g, ''));
					var max = parseInt($(this).find('span.max').html().replace(/\,/g, ''));

					if (count >= min && count <= max) {
						$(this).css("background-color", "rgb(66, 110, 191)").css("color", "#ffffff");
						basePrice = parseInt($(this).find('span.price').html().replace(/\,/g, ''));
						decreaseTax = $(this).find('span.tax').html().toString();
					}
				});
			}
			else {
				basePrice = parseFloat('<%=basePrice%>');
				decreaseTax = '<%=decreaseTax.ToString().ToLower()%>';
			}

			

			if (type == '<%=(int)Arad.SMS.Gateway.Business.TypeFish.OnLine%>') {
				var smsPrice = count * basePrice;
				if (decreaseTax.toString().toLowerCase() == 'true')
					taxAmount = Math.round(count * basePrice * (tax / 100));
				else
					taxAmount = 0;

				$("#<%=txtSmsPrice.ClientID%>")[0].value = getFormatDecimal(smsPrice.toString());
				$("#<%=txtTax.ClientID%>")[0].value = getFormatDecimal(taxAmount.toString());
				$("#<%=txtOnlineAmount.ClientID%>")[0].value = getFormatDecimal((smsPrice + taxAmount).toString());
			}
			else if (type == '<%=(int)Arad.SMS.Gateway.Business.TypeFish.Account%>') {
				if (decreaseTax.toLowerCase() == 'true' || decreaseTax == 1)
					taxAmount = amount - Math.round(amount / ((tax / 100) + 1));
				else
					taxAmount = 0;

				$("#<%=txtAccountTax.ClientID%>")[0].value = getFormatDecimal(taxAmount.toString());
				$("#<%=txtSmsCount.ClientID%>")[0].value = getFormatDecimal(count.toString());
			}
		}
		catch (e) { }
	}

	function saveResult(saveType, resultType, message) {
		switch (saveType) {
			case "account":
				showAccountForm();
				$("#saveAccountResult").removeClass();
				switch (resultType) {
					case 'error':
						$("#saveAccountResult").addClass("bg-danger div-save-result");
						$("#saveAccountResult").html("<span class='fa fa-times-circle fa-2x' style='color:red'></span>" + message);
						break;
					case 'ok':
						$("#saveAccountResult").addClass(" bg-success div-save-result");
						$("#saveAccountResult").html("<span class='fa fa-check-square fa-2x' style='color:#5cb85c'></span>" + message);
						break;
				}
				break;

			case "online":
				$("#selectPayment").hide();
				$(".form").hide();
				$("#divOnline").hide();
				$("#proforma").hide();

				$("#saveOnlineResult").removeClass();
				switch (resultType) {
					case 'error':
						$("#saveOnlineResult").addClass("bg-danger div-save-result");
						$("#saveOnlineResult").html("<span class='fa fa-times-circle fa-2x' style='color:red'></span>" + message);
						break;
					case 'ok':
						$("#saveOnlineResult").addClass("bg-success div-save-result");
						$("#saveOnlineResult").html("<span class='fa fa-check-square fa-2x' style='color:#5cb85c'></span>" + message);
						break;
				}
				break;
		}
	}

	function showProforma() {
		var counter = 1;
		var smsCount = $("#<%=txtOnlineSmsCount.ClientID%>")[0].value;
		smsCount = parseInt(smsCount.replace(/\,/g, ''));
		var basePrice = parseFloat('<%=basePrice%>');

		$("#tblServices").append(
			'<tr class="info">' +
				'<td>' + counter + '</td>' +
				'<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sms") %></td>' +
				'<td>' + smsCount + '</td>' +
				'<td>' + basePrice + '</td>' +
				'<td>' + $("#<%=txtSmsPrice.ClientID%>")[0].value + '</td>' +
				'</tr>'
			);

		$("#tdTax").html($("#<%=txtTax.ClientID%>")[0].value);
		$("#tdTotalPrice").html($("#<%=txtOnlineAmount.ClientID%>")[0].value);

		$("#divOnline").hide();
		$("#Tariff").hide();
		$("#proforma").show();
	}

	function loader() {
		$("body").setOverlay('/pic/loader.gif');
	}
</script>

<div id="selectPayment" class="row">
	<hr />
	<a href="#" class="alert-link" onclick="showAccountForm();">
		<div class="col-md-2" style="display: table; text-align: center">
			<div class="alert alert-info" style="height: 100px; display: table-cell; vertical-align: middle;">
				<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FishPayment") %>
			</div>
		</div>
	</a>
	<a href="#" onclick="showOnlineForm();">
		<div class="col-md-2" style="display: table; text-align: center">
			<div class="alert alert-success" style="height: 100px; display: table-cell; vertical-align: middle;">
				<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("OnlinePayment") %>
			</div>
		</div>
	</a>
</div>

<div class="row">
	<hr />
	<div id="saveOnlineResult"></div>
	<div class="col-md-5">
		<div id="divAccount" class="form-horizontal form" role="form">
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PaymentAmount") %></label>
				<div class="col-sm-7">
					<div class="input-group">
						<asp:TextBox ID="txtAccountAmount" class="form-control input-sm numberInput" autoFormatDecimal="true" runat="server" onblur="checkSmsCount(4);"></asp:TextBox>
						<span class="input-group-addon"><%--ریال--%></span>
					</div>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsCount")%></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtSmsCount" class="form-control input-sm numberInput" autoFormatDecimal="true" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.Facade.Setting.GetValue((int)Arad.SMS.Gateway.Business.MainSettings.Tax) %> % - <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TaxValueAdded") %></label>
				<div class="col-sm-7">
					<div class="input-group">
						<asp:TextBox ID="txtAccountTax" class="form-control input-sm numberInput" autoFormatDecimal="true" runat="server" disabled></asp:TextBox>
						<span class="input-group-addon"><%--ریال--%></span>
					</div>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Account")%></label>
				<div class="col-sm-7">
					<asp:DropDownList ID="drpAccount" class="form-control input-sm" isrequired="true" validationSet="account" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SerialNumber")%></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtSerialNumber" class="form-control input-sm" isrequired="true" validationSet="account" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PaymentDate")%></label>
				<div class="col-sm-7">
					<SMS:DatePicker ID="dtpPaymentDate" IsRequired="true" ValidationSet="account" runat="server"></SMS:DatePicker>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Description")%></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtDescription" class="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
				</div>
			</div>
			<div class="buttonControlDiv">
				<asp:Button ID="btnSaveFishPayment" runat="server" Text="Register" CssClass="btn btn-success" OnClick="btnSaveFishPayment_Click" />
				<a class="btn btn-default" href="#" onclick="cancel();"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %></a>
			</div>
			<div class="clear"></div>
			<div id="saveAccountResult" class="div-save-result"></div>
		</div>
		<div id="divOnline" class="form">
			<div class="form-horizontal" role="form">
				<div class="form-group">
					<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsCount")%></label>
					<div class="col-sm-7">
						<div class="input-group">
							<asp:TextBox ID="txtOnlineSmsCount" class="form-control input-sm numberInput" isrequired="true" validationSet="onlinepayment" autoFormatDecimal="true" runat="server" onkeyup="checkSmsCount(5);"></asp:TextBox>
							<span class="input-group-addon"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Number") %></span>
						</div>
					</div>
				</div>
				<div class="form-group">
					<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CostOfSmsRequest")%></label>
					<div class="col-sm-7">
						<div class="input-group">
							<asp:TextBox ID="txtSmsPrice" class="form-control input-sm numberInput" autoFormatDecimal="true" runat="server" disabled></asp:TextBox>
							<span class="input-group-addon"><%--<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Rial")%>--%></span>
						</div>
					</div>
				</div>
				<div class="form-group">
					<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.Facade.Setting.GetValue((int)Arad.SMS.Gateway.Business.MainSettings.Tax) %><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TaxValueAdded") %></label>
					<div class="col-sm-7">
						<div class="input-group">
							<asp:TextBox ID="txtTax" class="form-control input-sm numberInput" autoFormatDecimal="true" runat="server" disabled></asp:TextBox>
							<span class="input-group-addon"><%--<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Rial")%>--%></span>
						</div>
					</div>
				</div>
				<div class="form-group">
					<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PaymentAmount") %></label>
					<div class="col-sm-7">
						<div class="input-group">
							<asp:TextBox ID="txtOnlineAmount" class="form-control input-sm numberInput" autoFormatDecimal="true" runat="server"></asp:TextBox>
							<span class="input-group-addon"><%--ریال--%></span>
						</div>
					</div>
				</div>
				<div class="form-group">
					<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("OnlinePayment") %></label>
					<div class="col-sm-7">
						<div class="input-group">
							<span class="input-group-addon">
								<input name="bank" type="radio" id="rdbMellat" runat="server" />
							</span>
							<div class="form-control" style="height: 45px;">
								<%--<img src="/pic/mellat.png" />
								بانک ملت--%>
							</div>
						</div>
						<div class="input-group">
							<span class="input-group-addon">
								<input name="bank" type="radio" id="rdbParsian" runat="server" />
							</span>
							<div class="form-control" style="height: 45px;">
								<%--<img src="/pic/parsian.png" />
								بانک پارسیان--%>
							</div>
						</div>
					</div>
				</div>
				<div class="buttonControlDiv">
					<a class="btn btn-success" href="#" onclick="showProforma();"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Pay") %></a>
					<a class="btn btn-default" href="#" onclick="cancel();"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Cancel") %></a>
				</div>
			</div>
			<div class="clear"></div>
			<hr />
		</div>
	</div>
	<div class="col-md-5">
		<div id="Tariff" style="display: none">
			<% if(Session["Language"].ToString() == "fa") { %>
			<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered" Style="text-align: center">
				<Columns>
					<asp:TemplateField HeaderText="قیمت پایه پیامک">
						<ItemTemplate>
							<asp:Label ID="lblBasePrice" CssClass="price" runat="server" Text='<%# Arad.SMS.Gateway.GeneralLibrary.Helper.FormatDecimalForDisplay(Eval("BasePrice")) %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="حداقل تعداد پیامک">
						<ItemTemplate>
							<asp:Label ID="lblMinimum" CssClass="min" runat="server" Text='<%# Arad.SMS.Gateway.GeneralLibrary.Helper.FormatDecimalForDisplay(Eval("MinimumMessage")) %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="حداکثر تعداد پیامک">
						<ItemTemplate>
							<asp:Label ID="lblMaximum" CssClass="max" runat="server" Text='<%# Arad.SMS.Gateway.GeneralLibrary.Helper.FormatDecimalForDisplay(Eval("MaximumMessage")) %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField>
						<ItemTemplate>
							<asp:Label ID="lblDecreaseTax" CssClass="tax" Style="display: none;" runat="server" Text='<%# Arad.SMS.Gateway.GeneralLibrary.Helper.GetBool(Eval("DecreaseTax")) %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
			<% }  else
			{%>
			<asp:GridView ID="gridTariff" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered" Style="text-align: center">
				<Columns>
					<asp:TemplateField HeaderText="SMS base price">
						<ItemTemplate>
							<asp:Label ID="lblBasePrice" CssClass="price" runat="server" Text='<%# Arad.SMS.Gateway.GeneralLibrary.Helper.FormatDecimalForDisplay(Eval("BasePrice")) %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Minimum number of SMS">
						<ItemTemplate>
							<asp:Label ID="lblMinimum" CssClass="min" runat="server" Text='<%# Arad.SMS.Gateway.GeneralLibrary.Helper.FormatDecimalForDisplay(Eval("MinimumMessage")) %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Maximum number of SMS">
						<ItemTemplate>
							<asp:Label ID="lblMaximum" CssClass="max" runat="server" Text='<%# Arad.SMS.Gateway.GeneralLibrary.Helper.FormatDecimalForDisplay(Eval("MaximumMessage")) %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField>
						<ItemTemplate>
							<asp:Label ID="lblDecreaseTax" CssClass="tax" Style="display: none;" runat="server" Text='<%# Arad.SMS.Gateway.GeneralLibrary.Helper.GetBool(Eval("DecreaseTax")) %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
			<%  } %>

		</div>
	</div>

	<div id="proforma" class="row col-md-10" style="display: none;">
		<div class="col-md-1"></div>
		<table class="table">
			<tr>
				<td>
					<table class="table table-bordered">
						<tr>
							<td style="width: 80%" rowspan="2">
								<p style="text-align: center">
									<span style="font-size: 27.0pt;"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PreInvoice") %></span>
								</p>
							</td>
							<td style="width: 20%">
								<p style='text-align: right;'>
									<span><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Date") %> :<asp:Label ID="lblDate" runat="server"></asp:Label>
									</span>
								</p>
							</td>
						</tr>
						<tr>
							<td style="width: 20%">
								<p style='text-align: right;'>
									<span><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Time") %>:<%=string.Format("{0}:{1}",DateTime.Now.Hour,DateTime.Now.Minute) %></span>
								</p>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<table class="table table-bordered">
						<tr class="warning">
							<td rowspan="2" style="width: 30%; text-align: center; vertical-align: middle"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Seller") %>
							</td>
							<td style="width: 5%"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Name") %></td>
							<td style="width: 30%">
								<asp:Label ID="lblSellerName" runat="server"></asp:Label></td>
							<td style="width: 5%"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PhoneNumber") %></td>
							<td style="width: 30%">
								<asp:Label ID="lblSellerPhone" runat="server"></asp:Label></td>
						</tr>
						<tr class="warning">
							<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Address") %></td>
							<td colspan="3">
								<asp:Label ID="lblSellerAddress" runat="server"></asp:Label></td>
						</tr>
						<tr class="danger">
							<td rowspan="2" style="width: 30%; text-align: center; vertical-align: middle"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("buyer") %></td>
							<td style="width: 5%"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Name") %></td>
							<td style="width: 30%">
								<asp:Label ID="lblShopperName" runat="server"></asp:Label></td>
							<td style="width: 5%"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PhoneNumber") %></td>
							<td style="width: 30%">
								<asp:Label ID="lblShopperPhone" runat="server"></asp:Label></td>
						</tr>
						<tr class="danger">
							<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Address") %></td>
							<td colspan="3">
								<asp:Label ID="lblShopperAddress" runat="server"></asp:Label></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<table class="table table-bordered" id="tblServices">
						<tr class="active">
							<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RowNumber") %></td>
							<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GoodsServices") %></td>
							<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Count") %></td>
							<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("BasePrice") %></td>
							<td><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Amount") %></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<table class="table table-bordered">
						<tr>
							<td colspan="2"></td>
							<td style="width: 15%" class="success"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TaxValueAdded") %></td>
							<td style="width: 20%" id="tdTax" class="success"></td>
						</tr>
						<tr>
							<td colspan="2"></td>
							<td style="width: 15%" class="success"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Amount") %></td>
							<td style="width: 20%" id="tdTotalPrice" class="success"></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Button ID="btnSaveOnlinePayment" runat="server" Text="Pay" CssClass="btn btn-lg btn-success" OnClientClick="loader();" OnClick="btnSaveOnlinePayment_Click" />
				</td>
			</tr>
		</table>
		<div class="col-md-1"></div>
	</div>
</div>


