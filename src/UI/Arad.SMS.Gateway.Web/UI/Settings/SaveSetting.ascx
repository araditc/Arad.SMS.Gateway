<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveSetting.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Settings.SaveSetting" %>

<script type="text/javascript">
	$(document).ready(function () {
		$("input[type=checkbox]").addClass('ace');
	});
</script>

<hr />
<div class="col-xs-8 col-md-8">
	<div class="form-horizontal" role="form">
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Tax")%></label>
			<div class="col-sm-4">
				<div class="input-group" style="z-index: 1">
					<asp:TextBox ID="txtTax" class="form-control input-sm numberInput" isRequired="true" validationSet="saveSetting" runat="server"></asp:TextBox>
					<span class="input-group-addon">%</span>
				</div>
			</div>
		</div>
			<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("AppPath")%></label>
			<div class="col-sm-4">
				<asp:TextBox ID="txtAppPath" runat="server" Style="text-align: left; direction: ltr;" isRequired="true" validationSet="saveSetting" CssClass="form-control input-sm"></asp:TextBox>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SendQueueRecipientAddress")%></label>
			<div class="col-sm-4">
				<asp:TextBox ID="txtSendQueueRecipientAddress" runat="server" Style="text-align: left; direction: ltr;" isRequired="true" validationSet="saveSetting" CssClass="form-control input-sm"></asp:TextBox>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("IsRemoteQueue")%></label>
			<div class="col-sm-7">
				<label class="middle">
					<asp:CheckBox ID="chbIsRemoteQueue" runat="server" />
					<span class="lbl"></span>
				</label>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("RemoteQueueIP")%></label>
			<div class="col-sm-4">
				<asp:TextBox ID="txtRemoteQueueIP" runat="server" Style="text-align: left; direction: ltr;" CssClass="form-control input-sm"></asp:TextBox>
			</div>
		</div>
		<div class="form-group">
			<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MaximumFailedTryCount")%></label>
			<div class="col-sm-4">
				<asp:TextBox ID="txtMaximumFailedTryCount" runat="server" isRequired="true" validationSet="saveSetting" CssClass="form-control input-sm"></asp:TextBox>
			</div>
		</div>

	</div>
	<div class="buttonControlDiv col-md-8">
		<asp:Button ID="btnSave" runat="server" Text="Register" OnClick="btnSave_Click" CssClass="btn btn-success" />
		<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" />
	</div>
</div>
