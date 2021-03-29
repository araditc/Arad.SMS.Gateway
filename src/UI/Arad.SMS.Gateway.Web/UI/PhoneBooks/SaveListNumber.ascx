<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveListNumber.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PhoneBooks.SaveListNumber" %>

<div class="row">
	<hr />
	<div id="divResult" class="div-save-result"></div>
	<div class="col-md-4 col-xs-6">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NumbersList")%></label>
				<div class="col-sm-5">
					<asp:TextBox ID="txtNumbers" class="form-control input-sm mobileNumberInputList" runat="server" Height="100px" isRequired="true" TextMode="MultiLine"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("IncorectNumbers")%></label>
				<div class="col-sm-5">
					<asp:TextBox ID="txtNumbersInvalid" class="form-control input-sm" runat="server" Height="100px" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConditionsRegisterNumber")%></label>
				<div class="col-sm-7">
					<asp:DropDownList ID="drpCheckNumberScope" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div id="infoNumbers">
				<input id="txtCorrectNumber" type="text" class="input" readonly="true" style="width: 100%; border: 0px solid black;" />
				<input id="txtFailNumber" type="text" class="input" readonly="true" style="width: 100%; border: 0px solid black;" />
			</div>
			<div class="buttonControlDiv">
				<asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Register"/>
				<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
			</div>
		</div>
	</div>
</div>