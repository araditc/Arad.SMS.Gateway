<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveContent.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.RegularContents.SaveContent" %>

<div class="row">
	<div class="col-md-5">
		<div class="form-horizontal" role="form">
			<hr />
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("File")%></label>
				<div class="col-sm-10">
					<label class="ace-file-input">
						<asp:FileUpload ID="fileUpload" runat="server" CssClass="input" />
						<span class="ace-file-container" data-title="Choose"><span class="ace-file-name" data-title="No File ..."><i class=" ace-icon fa fa-upload"></i></span></span><a class="remove" href="#"><i class=" ace-icon fa fa-times"></i></a>
					</label>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv">
			<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Register" />
			<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
		</div>
	</div>
</div>
