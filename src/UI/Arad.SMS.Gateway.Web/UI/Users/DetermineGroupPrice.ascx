<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetermineGroupPrice.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Users.DetermineGroupPrice" %>

<div class="row">
	<hr />
	<div class="col-xs-6 col-md-4">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupPrice")%></label>
				<div class="col-sm-7">
					<asp:DropDownList ID="drpGroupPrice" class="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FixTariff")%></label>
				<div class="col-sm-7">
					<asp:CheckBox ID="chbIsFixGroupPrice" runat="server" />
				</div>
			</div>
			<div class="col-md-offset-7">
				<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Register" />
				<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
			</div>
		</div>
	</div>
</div>
