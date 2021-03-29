<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveDataLocation.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.DataCenters.SaveDataLocation" %>

<div class="row">
	<hr />
	<div class="col-xs-10 col-md-10">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("page") %></label>
				<div class="col-sm-4">
					<asp:DropDownList ID="drpDesktop" CssClass="form-control input-sm" runat="server" isRequired="true"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Location")%></label>
				<div class="col-sm-4">
					<asp:DropDownList ID="drpLocation" CssClass="form-control input-sm" runat="server" isRequired="true"></asp:DropDownList>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv col-md-8">
			<asp:Button ID="btnSave" runat="server" Text="Register" OnClick="btnSave_Click" CssClass="btn btn-success" />
			<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
		</div>
	</div>
</div>