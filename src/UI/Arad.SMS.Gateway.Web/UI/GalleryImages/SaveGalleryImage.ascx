<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveGalleryImage.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.GalleryImages.SaveGalleryImage" %>

<div class="row">
	<hr />
	<div class="col-xs-10 col-md-10">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Title")%></label>
				<div class="col-sm-4">
					<asp:TextBox ID="txtTitle" runat="server" CssClass="form-control input-sm" isRequired="true"></asp:TextBox>
				</div>
			</div>
			<div class="buttonControlDiv col-md-4">
				<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Register" OnClick="btnSave_Click" />
				<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
			</div>
		</div>
	</div>
</div>
