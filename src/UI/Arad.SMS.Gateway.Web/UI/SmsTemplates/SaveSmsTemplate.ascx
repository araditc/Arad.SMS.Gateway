<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveSmsTemplate.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsTemplates.SaveSmsTemplate" %>

<script type="text/javascript">
	function result(type, message) {
		switch (type) {
			case 'ok':
				$("#result").addClass("bg-success div-save-result");
				$("#result").html("<span class='fa fa-check-square fa-2x' style='color:#5cb85c'></span>" + message);
				break;
			case 'error':
				$("#result").addClass("bg-danger div-save-result");
				$("#result").html("<span class='fa fa-times-circle fa-2x' style='color:red'></span>" + message);
				break;
		}
	}
</script>

<div class="row">
	<hr />
	<div class="col-xs-4 col-md-3">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-5 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SmsBody")%></label>
				<div class="col-sm-7">
					<div>
						<GeneralTools:SmsBodyBox runat="server" ID="txtSmsBody" IsRequired="true"></GeneralTools:SmsBodyBox>
					</div>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv">
			<asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" OnClick="btnSave_Click" Text="Register"></asp:Button>
			<asp:Button ID="btnCancel" CssClass="btn btn-default" runat="server" OnClick="btnCancel_Click" Text="Cancel"></asp:Button>
		</div>
		<div class="clear"></div>
		<div id="result" class="div-save-result"></div>
	</div>
</div>
