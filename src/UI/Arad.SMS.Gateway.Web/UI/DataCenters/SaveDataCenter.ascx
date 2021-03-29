<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveDataCenter.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.DataCenters.SaveDataCenter" %>

<div class="row">
	<hr />
	<div class="col-xs-6 col-md-4">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Title")%></label>
				<div class="col-sm-6">
					<asp:TextBox ID="txtTitle" CssClass="form-control input-sm" isRequired="true" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-6 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Type")%></label>
				<div class="col-sm-6">
					<asp:DropDownList ID="drpType" class="form-control input-sm" isrequired="true" runat="server"></asp:DropDownList>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv">
			<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Register" OnClick="btnSave_Click" />
			<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
		</div>
	</div>
</div>
