<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetTrafficRelay.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PrivateNumbers.SetTrafficRelay" %>

<div class="row">
	<hr />
	<div class="col-xs-10 col-md-10">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TrafficRelay") %></label>
				<div class="col-sm-4">
					<asp:DropDownList ID="drpSmsTrafficRelay" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeliveryRelay") %></label>
				<div class="col-sm-4">
					<asp:DropDownList ID="drpDeliveryTrafficRelay" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv col-md-8">
			<asp:Button ID="btnSave" runat="server" Text="Register" OnClick="btnSave_Click" CssClass="btn btn-success" />
			<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
		</div>
	</div>
</div>
