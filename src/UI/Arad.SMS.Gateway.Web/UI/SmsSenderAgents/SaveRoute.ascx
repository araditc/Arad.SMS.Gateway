<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveRoute.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsSenderAgents.SaveRoute" %>

<asp:HiddenField ID="hdnPass" runat="server" />
<div class="row">
	<hr />
	<div class="col-xs-6 col-md-4">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Title")%></label>
				<div class="col-sm-7">
					<asp:TextBox CssClass="form-control input-sm" ID="txtName" runat="server" isRequired="true"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Operator")%></label>
				<div class="col-sm-7">
					<asp:DropDownList ID="drpOperator" runat="server" class="form-control input-sm"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserName")%></label>
				<div class="col-sm-7">
					<asp:TextBox CssClass="form-control input-sm" ID="txtUsername" runat="server" isRequired="true"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Password")%></label>
				<div class="col-sm-7">
					<asp:TextBox CssClass="form-control input-sm" ID="txtPassword" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Link")%></label>
				<div class="col-sm-7">
					<asp:TextBox CssClass="form-control input-sm" ID="txtLink" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Domain")%></label>
				<div class="col-sm-7">
					<asp:TextBox CssClass="form-control input-sm" ID="txtDomain" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("QueueLength")%></label>
				<div class="col-sm-7">
					<asp:TextBox CssClass="form-control input-sm numberInput" ID="txtQueueLength" runat="server" isRequired="true"></asp:TextBox>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv">
			<asp:Button ID="btnSave" Text="Register" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" />
			<asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" />
		</div>
	</div>
</div>
