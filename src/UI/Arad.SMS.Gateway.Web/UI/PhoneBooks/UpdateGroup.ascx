<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateGroup.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PhoneBooks.UpdateGroup" %>

<div class="row">
	<hr />
	<div class="col-xs-6 col-md-4">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupName") %></label>
				<div class="col-sm-7">
					<asp:TextBox ID="txtName" CssClass="form-control input-sm" runat="server" isRequired="true"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GroupName") %></label>
				<div class="col-sm-7">
					<asp:DropDownList ID="drpType" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="buttonControlDiv">
				<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Register" OnClick="btnSave_Click" />
			</div>
		</div>
	</div>
</div>
