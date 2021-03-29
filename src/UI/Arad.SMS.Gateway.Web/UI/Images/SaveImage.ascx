<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveImage.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Images.SaveImage" %>

<asp:HiddenField ID="hdnImagePath" runat="server" />

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
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Description")%></label>
				<div class="col-sm-4">
					<asp:TextBox ID="txtDescription" runat="server" CssClass="form-control input-sm" TextMode="MultiLine"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Image")%></label>
				<div class="col-sm-4">
					<label class="ace-file-input">
						<asp:FileUpload ID="uploadImage" runat="server" CssClass="input"/>
						<span class="ace-file-container" data-title="Choose"><span class="ace-file-name" data-title="No File ..."><i class=" ace-icon fa fa-upload"></i></span></span><a class="remove" href="#"><i class=" ace-icon fa fa-times"></i></a>
					</label>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Topic")%></label>
				<div class="col-sm-4">
					<asp:DropDownList ID="drpContent" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv col-md-6">
			<asp:Button ID="btnSave" runat="server" Text="Register" OnClick="btnSave_Click" CssClass="btn btn-success" />
			<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
		</div>
	</div>
</div>
