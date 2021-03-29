<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateDeliveryStatus.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.SmsReports.UpdateDeliveryStatus" %>

<div class="row">
	<hr />
	<div class="col-xs-10 col-md-10">
		<div class="form-horizontal" role="form">
			<div class="alert alert-info">
				<i class="ace-icon fa fa-hand-o-left"></i>
				<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UpdateDeliveryStatusContent1") %><br />
				<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UpdateDeliveryStatusContent2") %> 
				<a href="/uploads/DeliveryStatusSample.xlsx" style="color: red; font-weight: bold"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UpdateDeliveryStatusContent3") %></a>
				<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("UpdateDeliveryStatusContent4") %>
				<br />
				<ul class="list-unstyled spaced">
					<li><i class="ace-icon fa fa-bell-o bigger-110 purple"></i><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("SMSStatuses") %>:	</li>
					<li>
						<img src="/pic/status1.png" alt="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Delivered") %>" /><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Delivered") %> = 1</li>
					<li>
						<img src="/pic/status2.png" alt="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NotDeliver") %>" /><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("NotDeliver") %> = 2	</li>
					<li>
						<img src="/pic/status4.png" alt="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeliveredCommunication") %>" /><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DeliveredCommunication") %> = 4</li>
					<li>
						<img src="/pic/status10.png" alt="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DontSend") %>" /><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("DontSend") %> = 10</li>
					<li>
						<img src="/pic/status14.png" alt="<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("BlackList") %>" /><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("BlackList") %> = 14</li>
				</ul>
			</div>
			<div class="form-group">
				<label class="col-sm-4 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("File")%></label>
				<div class="col-sm-4">
					<label class="ace-file-input">
						<asp:FileUpload ID="fileUpload" runat="server" CssClass="input" />
						<span class="ace-file-container" data-title="Choose"><span class="ace-file-name" data-title="No File ..."><i class=" ace-icon fa fa-upload"></i></span></span><a class="remove" href="#"><i class=" ace-icon fa fa-times"></i></a>
					</label>
				</div>
			</div>
			<div class="buttonControlDiv col-md-8">
				<asp:Button ID="btnSave" runat="server" Text="Register" OnClick="btnSave_Click" CssClass="btn btn-success" />
				<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
			</div>
		</div>
	</div>
</div>
