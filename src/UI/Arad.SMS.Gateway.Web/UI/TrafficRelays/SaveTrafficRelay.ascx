<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveTrafficRelay.ascx.cs" Inherits="Arad.SMS.Gateway.Web.UI.TrafficRelays.SaveTrafficRelay" %>

<div class="row">
	<hr />
	<div class="col-xs-10 col-md-10">
		<div class="bg-warning" style="padding: 20px;">
			<ul>
				<li>یک نسخه از هر پیامک دریافتی به آدرس URL مورد نظر شما انتقال داده خواهد شد</li>
				<li>در وارد کردن آدرس اینترنتی دقت نمایید ، در صورت اشتباه بودن سرویس غیر فعال خواهد شد</li>
				<li>آدرس را به همراه "http://" و "www" وارد نمایید به عنوان مثال "http://www.yoursite.com/getsms.Php"</li>
				<li>از پارامتر های "TEXT$" و "TO$" و "FROM$" به ترتیب برای متن پیامک ، شماره گیرنده و شماره فرستنده استفاده کنید</li>
				<li>پارامتر ها را با کاراکتر "&amp;" جدا نمایید</li>
				<li>اطلاعات توسط متد "GET" به آدرس مورد نظر ارسال خواهد شد</li>
				<li>به عنوان مثال :
          <div style="direction: ltr">http://www.yoursite.com/getsms.aspx?to=$TO&amp;body=$TEXT&amp;from=$FROM</div>
				</li>
				<li>همچنین شما میتوانید اطلاعات اضافی خود را نیز اضافه نمایید
					<br>
					به عنوان مثال :
          <div style="direction: ltr">
						http://www.yoursite.com/getsms.aspx?to=$TO&amp;body=$TEXT&amp;from=$FROM&amp;name=yourname&amp;app=sms
					</div>
				</li>
			</ul>
		</div>
		<div class="bg-info" style="padding: 20px;">
			<ul>
				<li>یک نسخه از هر وضعیت پیامک دریافتی به آدرس URL مورد نظر شما انتقال داده خواهد شد</li>
				<li>در وارد کردن آدرس اینترنتی دقت نمایید ، در صورت اشتباه بودن سرویس غیر فعال خواهد شد</li>
				<li>آدرس را به همراه "//:http" و "www" وارد نمایید به عنوان مثال "http://www.yoursite.com/getsms.Php"</li>
				<li>از پارامتر های "batchid$" و "mobile$" و "status$" به ترتیب برای شناسه درخواست ، شماره گیرنده و وضعیت استفاده کنید</li>
				<li>پارامتر ها را با کاراکتر "&amp;" جدا نمایید</li>
				<li>اطلاعات توسط متد "GET" به آدرس مورد نظر ارسال خواهد شد</li>
				<li>به عنوان مثال :
          <div style="direction: ltr">http://www.yoursite.com/getsms.aspx?batchid=$batchid&amp;mobile=$mobile&amp;status=$status</div>
				</li>
			</ul>
		</div>
		<div class="form-horizontal" role="form">
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Title")%></label>
				<div class="col-sm-9">
					<asp:TextBox ID="txtTitle" CssClass="form-control input-sm" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label"><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("URL")%></label>
				<div class="col-sm-9">
					<asp:TextBox ID="txtUrl" CssClass="form-control input-sm" validationSet="SaveUrl" isrequired="true" TextMode="MultiLine" runat="server"></asp:TextBox>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label">بیشینه ناموفق</label>
				<div class="col-sm-2">
					<asp:DropDownList ID="drpTryCount" class="form-control input-sm" runat="server">
						<asp:ListItem>5</asp:ListItem>
						<asp:ListItem>10</asp:ListItem>
						<asp:ListItem>15</asp:ListItem>
						<asp:ListItem>20</asp:ListItem>
						<asp:ListItem>25</asp:ListItem>
						<asp:ListItem>30</asp:ListItem>
					</asp:DropDownList>
				</div>
			</div>
			<div class="form-group">
				<label class="col-sm-3 control-label">فعال</label>
				<div class="col-sm-2">
					<asp:CheckBox ID="chbIsActive" runat="server" />
				</div>
			</div>
			<div class="clearfix form-actions">
				<asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Register" />
				<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="Cancel" />
			</div>
		</div>
	</div>
</div>
