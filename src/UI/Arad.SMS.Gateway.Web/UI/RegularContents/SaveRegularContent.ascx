<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveRegularContent.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.RegularContents.SaveRegularContent" %>
<%@ Register Src="~/UI/Controls/BootstrapDateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="SMS" %>

<script type="text/javascript">
	$(document).ready(function () {
		changePeriodType();
	});

	function changePeriodType() {
		var selectedValue = $('#<%=drpPeriodType.ClientID %>').find('option:selected').attr('value');
		switch (selectedValue) {
			case '':
				$('#lblPer').html('....');
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsSentPeriodType.Minute %>':
				$('#lblPer').html('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PerMinute") %>');
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsSentPeriodType.Hour %>':
				$('#lblPer').html('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PerHour") %>');
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsSentPeriodType.Daily %>':
				$('#lblPer').html('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PerDaily") %>');
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsSentPeriodType.Weekly %>':
				$('#lblPer').html('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PerWeekly") %>');
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsSentPeriodType.Monthly %>':
				$('#lblPer').html('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PerMonthly") %>');
				break;
			case '<%=(int)Arad.SMS.Gateway.Business.SmsSentPeriodType.Yearly %>':
				$('#lblPer').html('<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PerYearly") %>');
				break;
		}
	}
</script>

<div class="row">
	<div class="col-md-6">
		<div class="panel panel-primary">
			<div class="panel-heading"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("MainSetting") %></div>
			<div class="panel-body">
				<div class="form-horizontal" role="form">
							<div class="form-group">
						<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Sender")%></label>
						<div class="col-sm-8">
							<asp:DropDownList ID="drpSenderNumber" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Title")%></label>
						<div class="col-sm-8">
							<asp:TextBox ID="txtTitle" runat="server" isRequired="true" CssClass="form-control input-sm" placeholder="عنوان محتوای منظم"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Type")%></label>
						<div class="col-sm-8">
							<asp:DropDownList ID="drpType" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TypeSend")%></label>
						<div class="col-sm-8">
							<asp:DropDownList ID="drpPeriodType" CssClass="form-control input-sm" runat="server" isRequired="true" onchange="changePeriodType();"></asp:DropDownList>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Per")%>&nbsp;<span id="lblPer">....</span>&nbsp;<%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Once")%></label>
						<div class="col-sm-8">
							<asp:TextBox ID="txtPeriod" runat="server" isRequired="true" CssClass="form-control input-sm numberInput"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("WarningType")%></label>
						<div class="col-sm-8">
							<asp:DropDownList ID="drpWatningType" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("StartDate")%></label>
						<div class="col-sm-8">
							<SMS:DateTimePicker ID="dtpStartDateTime" IsRequired="true" runat="server"></SMS:DateTimePicker>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("EndDate")%></label>
						<div class="col-sm-8">
							<SMS:DateTimePicker ID="dtpEndDateTime" IsRequired="true" runat="server"></SMS:DateTimePicker>
						</div>
					</div>
					<div class="form-group">
						<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Active")%></label>
						<div class="col-sm-8">
							<asp:CheckBox ID="chbIsActive" Checked="true" runat="server" />
						</div>
					</div>

				</div>
			</div>
		</div>
		<div class="buttonControlDiv">
			<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Register" />
			<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
		</div>
	</div>
	<div class="col-md-5">
		<div class="panel panel-primary">
			<div class="panel-heading"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GetContentFromURL") %></div>
			<div class="panel-body">
				<div class="form-group">
					<div class="col-sm-10">
						<asp:TextBox ID="txtUrl" runat="server" isRequired="true" CssClass="form-control input-sm"></asp:TextBox>
					</div>
					<label class="col-sm-2 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("URL")%></label>
				</div>
			</div>
		</div>
		<hr />
		<div class="panel panel-primary">
			<div class="panel-heading"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("GetContentFromDB") %></div>
			<div class="panel-body">
				<div class="form-horizontal" role="form">
					<div class="form-group">
						<div class="col-sm-9">
							<asp:TextBox ID="txtConnectionString" runat="server" isRequired="true" Style="direction: ltr" CssClass="form-control input-sm" Placeholder="Data Source=[ServerName];Initial Catalog=[DBName];User ID=[UserId];pwd=[Password]"></asp:TextBox>
						</div>
						<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("ConnectionString")%></label>
					</div>
					<div class="form-group">
						<div class="col-sm-9">
							<asp:TextBox ID="txtQuery" runat="server" isRequired="true" Style="direction: ltr" CssClass="form-control input-sm" Placeholder="SELECT Text,ID FROM [MyContent]"></asp:TextBox>
						</div>
						<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Query")%></label>
					</div>
					<div class="form-group">
						<div class="col-sm-9">
							<asp:TextBox ID="txtField" runat="server" isRequired="true" Style="direction: ltr" CssClass="form-control input-sm" Placeholder="Text"></asp:TextBox>
						</div>
						<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Column")%></label>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
