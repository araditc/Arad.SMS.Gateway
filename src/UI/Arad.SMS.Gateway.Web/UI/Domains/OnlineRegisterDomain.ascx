<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineRegisterDomain.ascx.cs" Inherits="MessagingSystem.UI.Domains.OnlineRegisterDomain" %>
<script src="/script/jquery.contextMenu.js" type="text/javascript"></script>
<link href="/style/jquery.contextMenu.css" rel="stylesheet" type="text/css" />

<asp:HiddenField ID="hdnCheckedDomainGuid" runat="server" />
<asp:HiddenField ID="hdnDomainExtention" runat="server" />
<asp:HiddenField ID="hdnDomainPeriod" runat="server" />
<style type="text/css">
	.divSearchDomain
	{
		border: 1px solid #aaa;
		background:#e6e4e8;
		width:360px;
		height:250px;
		margin:25px auto;
		border-radius: 15px;
		font-weight:bold !important;
		color:#000;
	}
	.divCheckboxListExtention
	{
		border: 1px solid #fff;
		background:#fff;
		width:310px;
		height:180px;
		margin:5px auto;
		border-radius:15px;
		direction:ltr;
	}
	#divRegisterDomain
	{
	border: 1px solid #aaa;
	width:500px;
	height:310px;
	margin:20px auto;
	border-radius: 15px;
	background:#e6e4e8;
	font-weight:bold!important;
	text-align:center!important;
	}
	.divPayment
	{
		border: 1px solid #aaa;
		width:400px;
		height:200px;
		margin:20px auto;
		border-radius: 15px;
		background:#e6e4e8;
		font-weight:bold!important;
		text-align:center;
	}
</style>

<script type="text/javascript">
	$(function () {
		$("#tabs").tabs();
		$(".tabs-bottom .ui-tabs-nav, .tabs-bottom .ui-tabs-nav > *")
			.removeClass("ui-corner-all ui-corner-top")
			.addClass("ui-corner-bottom");
		$(".tabs-bottom .ui-tabs-nav").appendTo(".tabs-bottom");
		$('#tabs ul li').addClass("ui-tabs-selected");
	});
</script>

<div id="loading" class="iframeWindow" style="width: 140px;padding: 2px;">
	<div id='modalHeader' class='ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix'
		style='position: relative; padding: 0.4em 1em; text-align: center; margin-bottom: 2px;width: 115px; height: 20px;'>
		<span class='ui-dialog-title' id='ui-dialog-title'><%=GeneralLibrary.Language.GetString("InSearch")%></span>
	</div>
	<div id='loaderImageDiv'>
		<center><img style='margin: 10px' src='/pic/loader.gif' /></center>
	</div>
</div>

<fieldset id="registerDomain">
	<legend><%=GeneralLibrary.Language.GetString("OnlineRegisterDomain")%></legend>

	<div  class="ui-widget-header" style="height:50px;border-radius:15px 15px 0px 0px">
		<div class="ui-state-error style" style="height:20px;border-radius:15px 15px 0px 0px;" >
			<span style="margin-right:15px"><%=GeneralLibrary.Language.GetString("Attention")%></span>
		</div>
		<div id="divTitle" class="divHeaderHelp"><%=GeneralLibrary.Language.GetString("SearchDomainTab")%></div> 
	</div>

	<div id="tabs" class="tabs-bottom" style="height:380px">
		<ul>
			<li><a href="#divSearchDomain"><span><%=GeneralLibrary.Language.GetString("Search")%></span></a></li>
			<li><a href="#divShowResultSearch"><span><%=GeneralLibrary.Language.GetString("AvailableService")%></span></a></li>
			<li><a href="#divRegisterDomain"><span><%=GeneralLibrary.Language.GetString("OrderRegister")%></span></a></li>
			<li><a href="#divPayment"><span><%=GeneralLibrary.Language.GetString("Payment")%></span></a></li>
		</ul>

		<div id="divSearchDomain" style="height:350px;">
			<div class="divSearchDomain">
				<div style="text-align:center;margin-top:5px;">
					<asp:ImageButton runat="server" ID="btnSearch" OnClick="btnSearch_Click" style="vertical-align:middle;" ImageUrl="/pic/search.png"/>
					<asp:TextBox ID="txtDomain" runat="server" CssClass="input" style="width:200px;text-align:left;padding-left:8px;" Height="22" isRequired="true" ValidationSet="ListExtention" ></asp:TextBox>
					<span>.www</span>
				</div>
				<div class="divCheckboxListExtention">
					<asp:CheckBoxList runat="server" ID="checkBoxListExtention" Height="180px" Width="320px" CellSpacing="10" RepeatColumns="4" isRequired="true" ValidationSet="ListExtention"></asp:CheckBoxList>
				</div>
			</div>
		</div>

		<center>
			<div id="divShowResultSearch" style="height:350px;">
				<GeneralTools:DataGrid runat="server" ID="gridDomainList" ListCaption="DomainList" ListHeight="150" ListDirection="RightToLeft" DisableNavigationBar="true" ShowRowNumber="true">
					<Columns>
							<GeneralTools:DataGridColumnInfo Sortable="false"  Hidden="true" Align="Center"  FieldName="Extention" ShowInExport="false"/>
							<GeneralTools:DataGridColumnInfo Sortable="false" CellWidth="60" Align="Center" Caption="Status" FieldName="Status" FormattingMethod="CustomRender"/>
							<GeneralTools:DataGridColumnInfo Sortable="false" CellWidth="95" Align="Center" Caption="Domain" FieldName="DomainName" FormattingMethod="CustomRender" />
							<GeneralTools:DataGridColumnInfo Sortable="false" CellWidth="125" Align="Center" Caption="Action" FieldName="Action" FormattingMethod="ImageButton"/>
					</Columns>
				</GeneralTools:DataGrid>
				<input type="button" class="button" value='<%=GeneralLibrary.Language.GetString("BackStep")%>' onclick="returnToPreviousTab(1);" />
			</div>
		</center>

		<div id="divRegisterDomain">
			<div id="divSaveNicDomain" style="width:500px;display:none;color:Black!important;">
				<div class="ui-widget-header" style="border-radius: 15px 15px 0px 0px;height:20px">
					<div class="titleDiv" style="border-left:1px solid #fff;width:150px;text-align:center"><%=GeneralLibrary.Language.GetString("DomainName")%></div>
					<div class="titleDiv" style="border-left:1px solid #fff;width:150px;text-align:center"><%=GeneralLibrary.Language.GetString("CountYear")%></div>
					<div class="titleDiv" style="width:150px;"><%=GeneralLibrary.Language.GetString("DomainPrice")%></div>
				</div>
				<div style="color:Black!important;padding-top:6px;">
					<div id="divNicDomainName" class="titleDiv" style="width:150px;text-align:center"></div>
					<div class="titleDiv" style="width:150px;text-align:center">
						<asp:DropDownList ID="drpNicPeriod" runat="server" >
							<asp:ListItem Value="1" Selected="True" Text="1"></asp:ListItem>
							<asp:ListItem Value="5" Text="5"></asp:ListItem>
						</asp:DropDownList>
					</div>
					<div class="titleDiv" id="divNicDomainPrice" style="width:150px;"></div>
				</div>

				<div style="width:220px;float:right;">
					<div class="titleDiv">NS1</div>
					<div class="controlDiv"><asp:TextBox ID="txtNicDNS1" runat="server" CssClass="input"  isRequired="true" ValidationSet="RegisterNicDomain"></asp:TextBox></div>
					<div class="titleDiv">NS2</div>
					<div class="controlDiv"><asp:TextBox ID="txtNicDNS2" runat="server" CssClass="input" isRequired="true" ValidationSet="RegisterNicDomain"></asp:TextBox></div>
					<div class="titleDiv">NS3</div>
					<div class="controlDiv"><asp:TextBox ID="txtNicDNS3" runat="server" CssClass="input"></asp:TextBox></div>
					<div class="titleDiv">NS4</div>
					<div class="controlDiv"><asp:TextBox ID="txtNicDNS4" runat="server" CssClass="input"></asp:TextBox></div>
				</div>
				<div style="width: 220px; float: right">
					<div class="titleDiv">IP1</div>
					<div class="controlDiv"><asp:TextBox ID="txtNicIP1" runat="server" CssClass="input"></asp:TextBox></div>
					<div class="titleDiv">IP2</div>
					<div class="controlDiv"><asp:TextBox ID="txtNicIP2" runat="server" CssClass="input"></asp:TextBox></div>
					<div class="titleDiv">IP3</div>
					<div class="controlDiv"><asp:TextBox ID="txtNicIP3" runat="server" CssClass="input"></asp:TextBox></div>
					<div class="titleDiv">IP4</div>
					<div class="controlDiv"><asp:TextBox ID="txtNicIP4" runat="server" CssClass="input"></asp:TextBox></div>
				</div>
				<div class="clear"></div>
				<hr style="width:80%"/>
				<div style="width:220px;float:right;">
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("CustomerId")%></div>
					<div class="controlDiv"><asp:TextBox ID="txtNicCustomerID" runat="server" CssClass="input" isRequired="true" ValidationSet="RegisterNicDomain"></asp:TextBox></div>
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("OfficeRelation")%></div>
					<div class="controlDiv"><asp:TextBox ID="txtNicOfficeRelation" runat="server" CssClass="input" isRequired="true" ValidationSet="RegisterNicDomain"></asp:TextBox></div>
				</div>
				<div style="width: 220px; float: right">
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("TechnicalRelation")%></div>
					<div class="controlDiv"><asp:TextBox ID="txtNicTechnicalRelation" runat="server" CssClass="input" isRequired="true" ValidationSet="RegisterNicDomain"></asp:TextBox></div>
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("FinancialRelation")%></div>
					<div class="controlDiv"> <asp:TextBox ID="txtNicFinancialRelation" runat="server" CssClass="input" isRequired="true" ValidationSet="RegisterNicDomain"></asp:TextBox></div>
				</div>
				<div class="buttonControlDiv" style="margin-top:20px;">
					<asp:Button runat="server" ID="btnRegisterNicDomain" Text="Register" CssClass="button" OnClick="btnRegisterNicDomain_Click"/>
					<input type="button" class="button" value='<%=GeneralLibrary.Language.GetString("BackStep")%>' onclick="returnToPreviousTab(2);" />
				</div>
			</div>

			<div id="divSaveDirectDomain" style="width:500px;color:Black!important;display:none;">
				<div class="ui-widget-header" style="border-radius: 15px 15px 0px 0px;height:20px">
					<div class="titleDiv" style="border-left:1px solid #fff;width:150px;text-align:center"><%=GeneralLibrary.Language.GetString("DomainName")%></div>
					<div class="titleDiv" style="border-left:1px solid #fff;width:150px;text-align:center"><%=GeneralLibrary.Language.GetString("CountYear")%></div>
					<div class="titleDiv" style="width:150px;"><%=GeneralLibrary.Language.GetString("DomainPrice")%></div>
				</div>
				<div style="color:Black!important;padding-top:6px;">
					<div class="titleDiv" id="divDirectDomainName" style="width:150px;text-align:center"></div>
					<div class="titleDiv" style="width:150px;text-align:center">
						<asp:DropDownList ID="drpDirectPeriod" runat="server">
							<asp:ListItem Selected="true" Value="1" Text="1"></asp:ListItem>
							<asp:ListItem Value="2" Text="2"></asp:ListItem>
							<asp:ListItem Value="3" Text="3"></asp:ListItem>
							<asp:ListItem Value="4" Text="4"></asp:ListItem>
							<asp:ListItem Value="5" Text="5"></asp:ListItem>
							<asp:ListItem Value="6" Text="6"></asp:ListItem>
							<asp:ListItem Value="7" Text="7"></asp:ListItem>
							<asp:ListItem Value="8" Text="8"></asp:ListItem>
							<asp:ListItem Value="9" Text="9"></asp:ListItem>
							<asp:ListItem Value="10" Text="10"></asp:ListItem>
						</asp:DropDownList>
					</div>
					<div class="titleDiv" id="divDirectDomainPrice" style="width:150px;"></div>
				</div>
				<div style="margin-top:80px;">
					<div style="float:right;width:220px;">
						<div class="titleDiv">NS1</div>
						<div class="controlDiv"><asp:TextBox ID="txtDirectDNS1" runat="server" CssClass="input" isRequired="true" ValidationSet="RegisterDirectDomain"></asp:TextBox></div>
						<div class="titleDiv">NS2</div>
						<div class="controlDiv"><asp:TextBox ID="txtDirectDNS2" runat="server" CssClass="input" isRequired="true" ValidationSet="RegisterDirectDomain"></asp:TextBox></div>
					</div>
					<div style="float:right;width:220px;">
						<div class="titleDiv"><%=GeneralLibrary.Language.GetString("CustomerId")%></div>
						<div class="controlDiv"><asp:TextBox ID="txtDirectCustomerID" runat="server" CssClass="input" isRequired="true" ValidationSet="RegisterDirectDomain"></asp:TextBox></div>
						<div class="titleDiv"><%=GeneralLibrary.Language.GetString("Email")%></div>
						<div class="controlDiv"><asp:TextBox ID="txtEmail" runat="server" CssClass="emailInput" isRequired="true" ValidationSet="Register"></asp:TextBox></div>
					</div>
				</div>
				<div class="buttonControlDiv" style="margin-top:20px;">
					<asp:Button runat="server" ID="btnRegisterDirectDomain" Text="Register" CssClass="button" OnClick="btnRegisterDirectDomain_Click"/>
					<input type="button" class="button" value='<%=GeneralLibrary.Language.GetString("BackStep")%>' onclick="returnToPreviousTab(2);" />
				</div>
			</div>
		</div>

		<div id="divPayment" style="height:350px;">
			<div class="divPayment">
				<div class="ui-widget-header" style="height: 25px;border-radius: 15px 15px 0px 0px">
					<div><%=GeneralLibrary.Language.GetString("PaymentTab")%></div>
				</div>
				<hr class="hr"/>
				<div class="clear" style="color:Black!important;margin-bottom:20px;">
					<label id="lblSumPayment" style="width:300px"></label><span><%=GeneralLibrary.Language.GetString("Rial")%></span>
				</div>
				<div class="buttonControlDiv">
					<asp:Button runat="server" ID="btnPayment" Text="Payment" CssClass="button" OnClick="btnPayment_Click" />
				</div>
			</div>
		</div>

	</div>
</fieldset>

<script type="text/javascript">

	function getDomainPrice(domainType) {
		setOverLay();
		switch (domainType) {
			case (<%=(int)Business.DomainType.Nic%>).toString():
				var price = getAjaxResponse("GetDomainPrice", "Extention=" + $("#<%=hdnDomainExtention.ClientID%>")[0].value +
																									"&Period=" + $("#<%=drpNicPeriod.ClientID%> option:selected").attr('value'));
				$("#divNicDomainPrice").html(price == "-1" ? "---------" : price);
				break;
			case (<%=(int)Business.DomainType.Direct%>).toString():
				var price = getAjaxResponse("GetDomainPrice", "Extention=" + $("#<%=hdnDomainExtention.ClientID%>")[0].value +
																									"&Period=" + $("#<%=drpDirectPeriod.ClientID%> option:selected").attr('value'));
				$("#divDirectDomainPrice").html(price == "-1" ? "---------" : price);
				break;
		}
		$("#registerDomain").removeOverlayWithElement('loading');
	}

	function setOverLay() {
		$("#loading").show();
		$("#registerDomain").setOverlayWithElement('loading');
	}

	function returnToPreviousTab(tabIndex) {
		$("#tabs").tabs('destroy');
		$("#tabs").tabs();
		$('#tabs ul li:nth-child(' + tabIndex + ') a').trigger('click');
		$('#tabs ul li').addClass("ui-tabs-selected");
	}

	function validationSearch() {
		if (!validateRequiredFields('ListExtention'))
			return false;
		setOverLay();
	}

	function searchDomain() {
		$("#registerDomain").removeOverlayWithElement('loading');
		$("#tabs").tabs('destroy');
		$("#tabs").tabs();
		$("#tabs ul li:nth-child(2) a").trigger('click');
		$('#tabs ul li').addClass("ui-tabs-selected");
		gridDomainList.Search();
		$("#divTitle").html('<%=GeneralLibrary.Language.GetString("AvailableDomainTab")%>');
	}

	function orderRegisterDomain(e) {
		gridDomainList.Event = e;
		if (!gridDomainList.IsSelectedRow())
			return;

		var checkedDomainGuid = $("#<%=hdnCheckedDomainGuid.ClientID %>")[0].value = gridDomainList.SelectedGuid;
		var domainName = gridDomainList.GetRowFieldValue(gridDomainList.GetSelectedRowID(), 4, "DomainName");
		var extention = $("#<%=hdnDomainExtention.ClientID %>")[0].value = gridDomainList.GetRowFieldValue(gridDomainList.GetSelectedRowID(), 2, "Extention");
		var price = getAjaxResponse("GetDomainPrice", "Extention=" + extention + "&Period=1");
		var isInternationalDomain = getAjaxResponse("IsInternationalDomain", "DomainExtention=" + extention);

		if (isInternationalDomain == "1" || isInternationalDomain == "true") {
			$("#divDirectDomainPrice").html(price == "-1" ? "---------" : price);
			$("#divDirectDomainName").html(domainName);

			$("#divSaveDirectDomain").show();
			$("#divSaveNicDomain").hide();
		}
		else {
			$("#divNicDomainPrice").html(price == "-1" ? "---------" : price);
			$("#divNicDomainName").html(domainName);

			$("#divSaveDirectDomain").hide();
			$("#divSaveNicDomain").show();
		}

		$("#divTitle").html('<%=GeneralLibrary.Language.GetString("RegisterDomainTab")%>');

		$("#tabs").tabs('destroy');
		$("#tabs").tabs();
		$("#tabs ul li:nth-child(3) a").trigger('click');
		$('#tabs ul li').addClass("ui-tabs-selected");
	}

	function paymentDomainPrice(price,period) {
		$("#lblSumPayment").html(price);
		$("#<%=hdnDomainPeriod.ClientID %>")[0].value = period;
		$("#tabs").tabs('destroy');
		$("#tabs").tabs();
		$("#tabs ul li:nth-child(4) a").trigger('click');
		$('#tabs ul li').addClass("ui-tabs-selected");
		$("#divTitle").html('<%=GeneralLibrary.Language.GetString("PaymentTab")%>');
	}

	function checkValidateRegisterNicDomain(){
		if(!validateRequiredFields('RegisterNicDomain'))
			return false;
		var price = getAjaxResponse("GetDomainPrice", "Extention=" + $("#<%=hdnDomainExtention.ClientID%>")[0].value +
																									"&Period=" + $("#<%=drpNicPeriod.ClientID%> option:selected").attr('value'));
		if(price=="-1"){
			messageBox('<%=GeneralLibrary.Language.GetString("UndefinePriceForRegisterDomain")%>');
			return false;
		}
		else
			return true; 
	}
	function checkValidateRegisterDirectDomain(){
		if(!validateRequiredFields('RegisterDirectDomain'))
			return false;
		var price = getAjaxResponse("GetDomainPrice", "Extention=" + $("#<%=hdnDomainExtention.ClientID%>")[0].value +
																									"&Period=" + $("#<%=drpDirectPeriod.ClientID%> option:selected").attr('value'));
		if(price=="-1"){
			messageBox('<%=GeneralLibrary.Language.GetString("UndefinePriceForRegisterDomain")%>');
			return false;
		}
		else
			return true; 
	}
	</script>
