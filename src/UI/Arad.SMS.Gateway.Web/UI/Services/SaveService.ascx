<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveService.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Services.SaveService" %>

<asp:HiddenField ID="hdnIcon" runat="server" />
<asp:HiddenField ID="hdnLargeIcon" runat="server" />

<div class="row">
	<hr />
	<div class="col-xs-6 col-md-4">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ServiceName")%></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtTitle" CssClass="form-control input-sm" ValidationSet="SaveService" isRequired="true" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ViewTheMenu")%></label>
				<div class="col-sm-6">
					<asp:DropDownList ID="drpServiceGroup" class="form-control input-sm" validationSet="SaveService" isrequired="true" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ServicePage")%></label>
				<div class="col-sm-6">
					<asp:DropDownList ID="drpReferencePage" class="form-control input-sm" validationSet="SaveService" isrequired="true" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SelectService")%></label>
				<div class="col-sm-6">
					<asp:DropDownList ID="drpReferenceService" class="form-control input-sm" validationSet="SaveService" isrequired="true" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Order")%></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtOrder" CssClass="form-control input-sm numberInput" ValidationSet="SaveService" isRequired="true" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Icon")%></label>
				<div class="col-sm-6">
					<asp:FileUpload ID="uploadIcon" runat="server" CssClass="button" />
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("LargeIcon")%></label>
				<div class="col-sm-6">
					<asp:FileUpload ID="uploadLargeIcon" runat="server" CssClass="button" />
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Presentable")%></label>
				<div class="col-sm-6">
					<asp:CheckBox ID="chbPresentable" runat="server" CssClass="checkBox" />
				</div>
			</div>
			<div class="buttonControlDiv">
				<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Register" />
				<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
			</div>
			<asp:Literal ID="ltrResult" runat="server"></asp:Literal>
		</div>
	</div>
</div>
