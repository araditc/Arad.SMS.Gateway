<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveServiceGroup.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.ServiceGroups.SaveServiceGroup" %>

<asp:HiddenField ID="hdnIcon" runat="server" />
<asp:HiddenField ID="hdnLargeIcon" runat="server" />

<div class="row">
	<hr />
	<div class="col-xs-6 col-md-4">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Title")%></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtTitle" CssClass="form-control input-sm" ValidationSet="AddServiceGroup" isRequired="true" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Order")%></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtOrder" CssClass="form-control input-sm numberInput" ValidationSet="AddServiceGroup" isRequired="true" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MasterGroup")%></label>
				<div class="col-sm-6">
					<asp:DropDownList ID="drpMasterGroup" class="form-control input-sm" validationSet="AddServiceGroup" isrequired="true" runat="server"></asp:DropDownList>
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
			<div class="buttonControlDiv">
				<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Register" />
				<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
			</div>
			<asp:Literal ID="ltrResult" runat="server"></asp:Literal>
		</div>
	</div>
</div>
