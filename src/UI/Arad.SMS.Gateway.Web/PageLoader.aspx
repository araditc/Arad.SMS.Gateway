<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageLoader.aspx.cs" AspCompat="true" Inherits="Arad.SMS.Gateway.Web.PageLoader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<%@ Import Namespace="System.Web.Optimization" %>

<head runat="server">
	<%: Styles.Render("~/Content/bootstrap") %>
	<%: Styles.Render("~/Content/font-awesome") %>
	<%: Styles.Render("~/Content/bootstrap-datepicker")%>
	<%: Styles.Render("~/Content/datetimepicker")%>
	<%: Styles.Render("~/Content/ace") %>
	<%: Styles.Render("~/Content/easyui") %>
	<%: Scripts.Render("~/Scripts/jquery") %>
	<script src="/script/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
	<script src="/script/routines.js" type="text/javascript"></script>
	<%: Scripts.Render("~/Scripts/bootstrap") %>
	<script src="Scripts/jquery.easyui.js" type="text/javascript"></script>
	<script src="Scripts/easyui-rtl.js" type="text/javascript"></script>
	<%: Scripts.Render("~/Scripts/acescript") %>
	<link href="/Content/mycss.css" rel="stylesheet" />


	<script type="text/javascript">
		$(document).ready(function () {
			var height = 0;
			var body = window.document.body;
			if (window.innerHeight) {
				height = window.innerHeight;
			} else if (body.parentElement.clientHeight) {
				height = body.parentElement.clientHeight;
			} else if (body && body.clientHeight) {
				height = body.clientHeight;
			}

			$(".setHeight").each(function () {
				var diff = $(this).attr('differenceHeight');
				$(this).css('height', ((height - diff) + "px"));
			});
		});

		<%=InlineScript %> 
	</script>
</head>
    <% if(Session["Language"].ToString() == "en") { %>
    <body class="no-skin">
    <% }  else
       {%>
    <body class="no-skin rtl">
    <%  } %>

	<form id="form1" runat="server">
		<asp:PlaceHolder ID="mainPanel" runat="server"></asp:PlaceHolder>
	</form>
	<div id="lightMask"></div>
	<div id="mask" class="ui-widget-overlay" style="display: none"></div>
</body>

<script src="/script/dynamicRoutine.js" type="text/javascript"></script>
<%: Scripts.Render("~/Scripts/bootstrap-datepicker") %>
<%:Scripts.Render("~/Scripts/datetimepicker") %>
</html>

