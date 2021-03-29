<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateNicAccount.ascx.cs" Inherits="MessagingSystem.UI.Domains.CreateNicAccount" %>

<script type="text/javascript">
	$(document).ready(function () {
		$('#divCivilPerson').hide();
		$("#radios").buttonset();
		$("#radios #<%=radioDomainNicCivilPerson.ClientID %>").click(function () {
			$('#divCivilPerson').show();
			$('#divActualPerson').hide();
		});
		$("#radios #<%=radioDomainNicActualPerson.ClientID %>").click(function () {
			$('#divCivilPerson').hide();
			$('#divActualPerson').show();
		});
	});

	function changeCivilType() {
		var txtCivilType = $("#<%=drpNicCivilPersonType.ClientID %> option:selected")[0].label;
		var civilType = $("#<%=drpNicCivilPersonType.ClientID %> option:selected").attr('value');
		$("#divCivilType").html(txtCivilType);
		$(".companyDetails").hide();
		switch (civilType) {
		case (<%=(int)Business.NicCivilPersonType.NonGovernment%>).toString():
			$("#divFirstName").html('<%=GeneralLibrary.Language.GetString("OwnerCompanyFirstName")%>');
			$("#divLastName").html('<%=GeneralLibrary.Language.GetString("OwnerCompanyLastName")%>');
			$("#divCompanyName").html('<%=GeneralLibrary.Language.GetString("CompanyName")%>');
			$("#divNonGovernmentType").show();
			break;
		case (<%=(int)Business.NicCivilPersonType.Government%>).toString():
			$("#divFirstName").html('<%=GeneralLibrary.Language.GetString("ManagerFirstName")%>');
			$("#divLastName").html('<%=GeneralLibrary.Language.GetString("ManagerLastName")%>');
			$("#divCompanyName").html('<%=GeneralLibrary.Language.GetString("OrganizationName")%>');
			$("#divGovernmentType").show();
			break;
		case (<%=(int)Business.NicCivilPersonType.ResearchCenter%>).toString():
			$("#divFirstName").html('<%=GeneralLibrary.Language.GetString("ManagerFirstName")%>');
			$("#divLastName").html('<%=GeneralLibrary.Language.GetString("ManagerLastName")%>');
			$("#divCompanyName").html('<%=GeneralLibrary.Language.GetString("InstituteName")%>');
			$("#divResearchCenterType").show();
			break;
		}
	}

	function checkValidateCivilPersonAccount() {
		if(!validateRequiredFields('SaveCivilPersonAccount'))
			return false;

		var civilType = $("#<%=drpNicCivilPersonType.ClientID %> option:selected").attr('value');
		switch (civilType) {
		case (<%=(int)Business.NicCivilPersonType.NonGovernment%>).toString():
			if(!validateRequiredFields('SaveNonGovernmentCivilPersonAccount'))
				return false;
			break;
		case (<%=(int)Business.NicCivilPersonType.Government%>).toString():
			if(!validateRequiredFields('SaveGovernmentCivilPersonAccount'))
				return false;
			break;
		case (<%=(int)Business.NicCivilPersonType.ResearchCenter%>).toString():
			if(!validateRequiredFields('SaveResearchCenterCivilPersonAccount'))
				return false;
			break;
		}

		return true;
	}
</script>

<fieldset>
	<legend><%=GeneralLibrary.Language.GetString("CreateNicAccount")%></legend>
	<div class="ui-widget-header" style="height:20px;border-radius:15px 15px 0px 0px">
		<div class="divHeaderHelp"><%=GeneralLibrary.Language.GetString("CreateNicAccountNotice")%></div> 
	</div>
	<div id="radios" style="margin-top:10px">
		<input type="radio" id="radioDomainNicActualPerson" name="radio" runat="server" />
		<label for="<%=radioDomainNicActualPerson.ClientID %>"><%=GeneralLibrary.Language.GetString("ActualPerson")%></label>
		<input type="radio" id="radioDomainNicCivilPerson" name="radio" runat="server" onclick="changeCivilType();" />
		<label for="<%=radioDomainNicCivilPerson.ClientID %>"><%=GeneralLibrary.Language.GetString("CivilPerson")%></label>
	</div>
	<div id="divActualPerson" style="width:500px;height:430px;margin:20px;border:1px solid #aaa;border-radius:10px">
		<div class="clear" style="margin-top:50px;"></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("FirstName")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtFirstName" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveActualPersonAccount"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("LastName") %></div>
		<div class="controlDiv"><asp:TextBox ID="txtLastName" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveActualPersonAccount"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("NationalID")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtNationalID" runat="server" CssClass="natinalCodeInput" isRequired="true" ValidationSet="SaveActualPersonAccount"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("CompanyName")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtCompanyName" runat="server" CssClass="input"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("City")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtCity" runat="server" CssClass="input"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("Province")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtProvince" runat="server" CssClass="input"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("Country")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtCountry" runat="server" CssClass="input"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("PostCode")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtPostalCode" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveActualPersonAccount"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("PhoneNumber")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="numberInput" isRequired="true" ValidationSet="SaveActualPersonAccount"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("FaxNo")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtFaxNo" runat="server" CssClass="numberInput"></asp:TextBox></div>
		<div class="titleDiv"><%=GeneralLibrary.Language.GetString("Email")%></div>
		<div class="controlDiv"><asp:TextBox ID="txtEmail" runat="server" CssClass="emailInput" isRequired="true" ValidationSet="SaveActualPersonAccount"></asp:TextBox></div>
		<div class="multiLineTitleDiv" style="height:0px;"><%=GeneralLibrary.Language.GetString("Address")%></div>
		<div class="multiLineControlDiv"><asp:TextBox ID="txtAddress" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveActualPersonAccount" TextMode="MultiLine" Width="200px"></asp:TextBox></div>
		<div class="buttonControlDiv">
			<asp:Button ID="btnSaveActualPersonAccount" runat="server" Text="Register" CssClass="blackButton" onclick="btnSaveActualPersonAccount_Click"/>
		</div>
	</div>

	<div id="divCivilPerson">
		<div class="controlDiv" style="margin:20px">
			<asp:DropDownList ID="drpNicCivilPersonType" runat="server" CssClass="input" Width="180"></asp:DropDownList>
		</div>
		<div class="clear"></div>
		<div id="divCivilType" class="lblHeader"></div>
		<div style="width:600px;height:500px;margin:20px;margin-top:0px;border:1px solid #aaa;border-radius:10px;padding-top:40px;">
			<div id="divFirstName" class="titleDiv"></div>
			<div class="controlDiv"><asp:TextBox ID="txtFirstNameCivil" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveCivilPersonAccount"></asp:TextBox></div>
			<div id="divLastName"class="titleDiv"></div>
			<div class="controlDiv"><asp:TextBox ID="txtLastNameCivil" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveCivilPersonAccount"></asp:TextBox></div>
			<div id="divCompanyName" class="titleDiv"></div>
			<div class="controlDiv"><asp:TextBox ID="txtCompanyNameCivil" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveCivilPersonAccount"></asp:TextBox></div>
			<div class="multiLineTitleDiv" style="height:0px;"><%=GeneralLibrary.Language.GetString("InterfaceAddress")%></div>
			<div class="multiLineControlDiv"><asp:TextBox ID="txtAddressCivil" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveCivilPersonAccount" TextMode="MultiLine" Width="200px"></asp:TextBox></div>
			<div class="titleDiv"><%=GeneralLibrary.Language.GetString("InterfaceCity")%></div>
			<div class="controlDiv"><asp:TextBox ID="txtCityCivil" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveCivilPersonAccount"></asp:TextBox></div>
			<div class="titleDiv"><%=GeneralLibrary.Language.GetString("InterfaceProvince")%></div>
			<div class="controlDiv"><asp:TextBox ID="txtProvinceCivil" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveCivilPersonAccount"></asp:TextBox></div>
			<div class="titleDiv"><%=GeneralLibrary.Language.GetString("InterfaceCountry")%></div>
			<div class="controlDiv"><asp:TextBox ID="txtCountryCivil" runat="server" CssClass="input" Text="IR" Enabled="false" style="text-align:left;"></asp:TextBox></div>
			<div class="titleDiv"><%=GeneralLibrary.Language.GetString("InterfacePostCode")%></div>
			<div class="controlDiv"><asp:TextBox ID="txtPostalCodeCivil" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveCivilPersonAccount"></asp:TextBox></div>
			<div class="titleDiv"><%=GeneralLibrary.Language.GetString("InterfaceTelephone")%></div>
			<div class="controlDiv"><asp:TextBox ID="txtTelephoneCivil" runat="server" CssClass="numberInput" isRequired="true" ValidationSet="SaveCivilPersonAccount"></asp:TextBox></div>
			<div class="titleDiv"><%=GeneralLibrary.Language.GetString("FaxNo")%></div>
			<div class="controlDiv"><asp:TextBox ID="txtFaxNumber" runat="server" CssClass="numberInput"></asp:TextBox></div>
			<div class="titleDiv"><%=GeneralLibrary.Language.GetString("Email")%></div>
			<div class="controlDiv"><asp:TextBox ID="txtEmailCivil" runat="server" CssClass="emailInput" isRequired="true" ValidationSet="SaveCivilPersonAccount"></asp:TextBox></div>
			<div class="clear"></div>
			<div id="divNonGovernmentType" class="companyDetails" style="display:none;">
				<div class="lblHeader"><%=GeneralLibrary.Language.GetString("CompanyInfo")%></div>
				<div style="padding-top:40px;">
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("RegisteredCountry")%></div>
					<div class="controlDiv"><asp:TextBox ID="txtNonGovernmentRegisteredCountry" runat="server" CssClass="input" Text="IR" Enabled="false"  style="text-align:left;"></asp:TextBox></div>
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("RegisteredCity")%></div>
					<div class="controlDiv"><asp:TextBox ID="txtNonGovernmentRegisteredCity" runat="server" isRequired="true" ValidationSet="SaveNonGovernmentCivilPersonAccount" CssClass="input"></asp:TextBox></div>
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("NameOfRegisteredUnit")%></div>
					<div class="controlDiv"><asp:TextBox ID="txtNonGovernmentNameOfRegisteredUnit" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveNonGovernmentCivilPersonAccount"></asp:TextBox></div>
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("CompanyCode")%></div>
					<div class="controlDiv"><asp:TextBox ID="txtNonGovernmentCompanyCode" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveNonGovernmentCivilPersonAccount"></asp:TextBox></div>
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("CompanyNationalCode")%></div>
					<div class="controlDiv"><asp:TextBox ID="txtNonGovernmentCompanyNationalCode" runat="server" CssClass="input"></asp:TextBox></div>
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("TypeOfCompany")%></div>
					<div class="controlDiv"><asp:DropDownList ID="drpNonGovernmentType" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveNonGovernmentCivilPersonAccount"></asp:DropDownList></div>
				</div>
			</div>
			<div id="divGovernmentType" class="companyDetails" style="display:none;">
				<div class="lblHeader"><%=GeneralLibrary.Language.GetString("OrganizationInfo")%></div>
				<div style="padding-top:40px;">
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("CountryName")%></div>
					<div class="controlDiv"><asp:TextBox ID="txtGovernmentCountryName" runat="server" CssClass="input" Text="IR" Enabled="false"  style="text-align:left;"></asp:TextBox></div>
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("ProvinceName")%></div>
					<div class="controlDiv"><asp:TextBox ID="txtGovernmentProvinceName" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveGovernmentCivilPersonAccount"></asp:TextBox></div>
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("CityName")%></div>
					<div class="controlDiv"><asp:TextBox ID="txtGovernmentCityName" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveGovernmentCivilPersonAccount"></asp:TextBox></div>
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("TypeOrganization")%></div>
					<div class="controlDiv"><asp:DropDownList ID="drpGovernmentType" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveGovernmentCivilPersonAccount"></asp:DropDownList></div>
				</div>
			</div>
			<div id="divResearchCenterType" class="companyDetails" style="display:none;">
				<div class="lblHeader"><%=GeneralLibrary.Language.GetString("InstituteInfo")%></div>
				<div style="padding-top:40px;">
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("CountryName")%></div>
					<div class="controlDiv"><asp:TextBox ID="txtResearchCenterCountryName" runat="server" CssClass="input" Text="IR" Enabled="false"  style="text-align:left;"></asp:TextBox></div>
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("ProvinceName")%></div>
					<div class="controlDiv"><asp:TextBox ID="txtResearchCenterProvinceName" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveResearchCivilPersonAccount"></asp:TextBox></div>
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("CityName")%></div>
					<div class="controlDiv"><asp:TextBox ID="txtResearchCenterCityName" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveResearchCivilPersonAccount"></asp:TextBox></div>
					<div class="titleDiv"><%=GeneralLibrary.Language.GetString("TypeInstitute")%></div>
					<div class="controlDiv"><asp:DropDownList ID="drpResearchCenterType" runat="server" CssClass="input" isRequired="true" ValidationSet="SaveResearchCivilPersonAccount"></asp:DropDownList></div>
				</div>
			</div>
			<div class="buttonControlDiv">
				<asp:Button ID="btnSaveCivilPersonAccount" runat="server" Text="Register" CssClass="blackButton" OnClick="btnSaveCivilPersonAccount_Click" />
			</div>
		</div>
	</div>
</fieldset>
