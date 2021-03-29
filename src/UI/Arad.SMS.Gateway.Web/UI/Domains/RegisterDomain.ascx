<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterDomain.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Domains.RegisterDomain" %>

<script type="text/javascript">
	function serachUser() {
		var username = $("#<%=txtOwner.ClientID%>").val();
		var info = getAjaxResponse("GetUserInfo", "Username=" + username);
		$("#ownerInfo").html(info);
	}
</script>
<div class="row">
	<hr />
	<div class="col-xs-10 col-md-10">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DomainName")%></label>
				<div class="col-sm-4">
					<asp:TextBox ID="txtName" runat="server" CssClass="form-control input-sm" isRequired="true"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("domainOwner")%></label>
				<div class="col-sm-4">
					<div class="input-group">
						<asp:TextBox ID="txtOwner" runat="server" CssClass="form-control input-sm" isRequired="true" placeholder="نام کاربری مالک دامنه"></asp:TextBox>
						<span class="input-group-addon" onclick="serachUser();" style="cursor: pointer"><span class="fa fa-search blue"></span></span>
					</div>
					<span id="ownerInfo" class="bg-warning"></span>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("panelOwner")%></label>
				<div class="col-sm-4">
					<asp:DropDownList ID="drpStartUpPage" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DefaultPage")%></label>
				<div class="col-sm-4">
					<asp:DropDownList ID="drpDefaultPage" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="buttonControlDiv col-md-6">
				<asp:Button ID="btnSave" runat="server" Text="Register" OnClick="btnSave_Click" CssClass="btn btn-success" />
				<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
			</div>
		</div>
	</div>
</div>
