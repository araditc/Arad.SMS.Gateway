<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DomainSetting.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Domains.DomainSetting" %>

<div class="row">
	<hr />
	<div class="col-xs-10 col-md-10">
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SitesLogo")%></label>
				<div class="col-sm-4">
					<label class="ace-file-input">
						<asp:FileUpload ID="fileUploadLogo" runat="server" CssClass="input" />
						<span class="ace-file-container" data-title="Choose"><span class="ace-file-name" data-title="No File ..."><i class=" ace-icon fa fa-upload"></i></span></span><a class="remove" href="#"><i class=" ace-icon fa fa-times"></i></a>
					</label>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SitesFavicon")%></label>
				<div class="col-sm-4">
					<label class="ace-file-input">
						<asp:FileUpload ID="fileUploadFavicon" runat="server" CssClass="input" />
						<span class="ace-file-container" data-title="Choose"><span class="ace-file-name" data-title="No File ..."><i class=" ace-icon fa fa-upload"></i></span></span><a class="remove" href="#"><i class=" ace-icon fa fa-times"></i></a>
					</label>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("CompanyName")%></label>
				<div class="col-sm-4">
					<asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SitesTitle")%></label>
				<div class="col-sm-4">
					<asp:TextBox ID="txtTitle" runat="server" CssClass="form-control input-sm"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Description")%></label>
				<div class="col-sm-4">
					<asp:TextBox ID="txtDescription" runat="server" CssClass="form-control input-sm" TextMode="MultiLine"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Keywords")%></label>
				<div class="col-sm-4">
					<asp:TextBox ID="txtKeywords" runat="server" CssClass="form-control input-sm" TextMode="MultiLine"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SitesFooter")%></label>
				<div class="col-sm-4">
					<asp:TextBox ID="txtFooter" runat="server" CssClass="form-control input-sm" TextMode="MultiLine"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SlideShowGallery")%></label>
				<div class="col-sm-4">
					<asp:DropDownList ID="drpGallery" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv col-md-8">
			<asp:Button ID="btnSave" runat="server" Text="Register" OnClick="btnSave_Click" CssClass="btn btn-success" />
		</div>
	</div>
</div>
