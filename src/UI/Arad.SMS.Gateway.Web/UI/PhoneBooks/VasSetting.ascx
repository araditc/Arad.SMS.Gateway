<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VasSetting.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.PhoneBooks.VasSetting" %>

<script type="text/javascript">
	function serachUser() {
		var username = $("#<%=txtOwner.ClientID%>").val();
		var info = getAjaxResponse("GetUserInfo", "Username=" + username);
		$("#ownerInfo").html(info);
	}
</script>
<div class="row">
	<hr />
	<div class="col-xs-6 col-md-6">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Type")%></label>
				<div class="col-sm-8">
					<asp:DropDownList ID="drpType" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ServiceID")%></label>
				<div class="col-sm-8">
					<asp:TextBox ID="txtServiceId" runat="server" CssClass="form-control input-sm" isRequired="true"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("VASRegisterKeyword") %></label>
				<div class="col-sm-8">
					<asp:TextBox ID="txtRegisterKeys" runat="server" CssClass="form-control input-sm"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("VASUnRegisterKeyword") %></label>
				<div class="col-sm-8">
					<asp:TextBox ID="txtUnsubscribeKeys" runat="server" CssClass="form-control input-sm"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UserAccess") %></label>
				<div class="col-sm-8">
					<div class="input-group">
						<asp:TextBox ID="txtOwner" runat="server" CssClass="form-control input-sm" placeholder=""></asp:TextBox>
						<span class="input-group-addon" onclick="serachUser();" style="cursor: pointer"><span class="fa fa-search blue"></span></span>
					</div>
					<span id="ownerInfo" class="bg-warning"></span>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv col-md-4">
			<asp:Button ID="btnSave" runat="server" Text="Register" OnClick="btnSave_Click" CssClass="btn btn-success" />
		</div>
	</div>
</div>
