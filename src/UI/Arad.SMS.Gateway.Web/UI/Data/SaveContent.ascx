<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveContent.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.Data.SaveContent" %>
<%@ Register Src="~/UI/Controls/BootstrapDatePicker.ascx" TagName="DatePicker" TagPrefix="SMS" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<div class="row">
	<div class="col-xs-12 col-md-12">
		<h4 class="header green" style="padding-right: 10px; margin-top: 0;"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SaveContent") %></h4>
		<div class="form-horizontal" role="form">
			<asp:Panel ID="pnlParentData" runat="server" Visible="false">
				<div class="form-group">
					<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MasterMenu")%></label>
					<div class="col-sm-9">
						<asp:DropDownList ID="drpParent" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
					</div>
				</div>
			</asp:Panel>
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Title")%></label>
				<div class="col-sm-9">
					<asp:TextBox ID="txtTitle" runat="server" CssClass="form-control input-sm" isRequired="true"></asp:TextBox>
				</div>
			</div>
		</div>
		<div class="col-sm-2"></div>
		<div class="form-inline" role="form">
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("FromDate")%></label>
				<div class="col-sm-8">
					<SMS:DatePicker ID="dtpFromDate" runat="server"></SMS:DatePicker>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ToDate")%></label>
				<div class="col-sm-8">
					<SMS:DatePicker ID="dtpToDate" runat="server"></SMS:DatePicker>
				</div>
			</div>
		</div>
		<br />
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Priority")%></label>
				<div class="col-sm-2">
					<asp:TextBox ID="txtPriority" runat="server" CssClass="form-control input-sm numberInput"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Keywords")%></label>
				<div class="col-sm-9">
					<asp:TextBox ID="txtKeywords" runat="server" CssClass="form-control input-sm" TextMode="MultiLine"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Summary") %></label>
				<div class="col-sm-9">
					<CKEditor:CKEditorControl ID="txtBreif" runat="server" Skin="kama" Width="100%"></CKEditor:CKEditorControl>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Content") %></label>
				<div class="col-sm-9">
					<CKEditor:CKEditorControl ID="txtBody" runat="server" Skin="kama" Width="100%"></CKEditor:CKEditorControl>
				</div>
			</div>
		</div>
		<div class="buttonControlDiv col-md-11">
			<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Register" OnClick="btnSave_Click" />
			<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
		</div>
	</div>
</div>
