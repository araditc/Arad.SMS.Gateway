<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorHandler.aspx.cs" Inherits="Arad.SMS.Gateway.Web.ErrorHandler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="System.Web.Optimization" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
		<%: Scripts.Render("~/Scripts/jquery") %>
		<script type="text/jscript">$(document).ready(function () { parent.$('#loading').hide(); });</script>
		<style type="text/css">
			body { 
				font-size:8pt; font-family:tahoma;
				scrollbar-face-color:#E5E5E5;
				margin:0px;
				padding:5px;
				direction:rtl;
				background-attachment: fixed;
				background-repeat: no-repeat; 
				background-color:#F8F8F8;
			}
		</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
			<asp:Literal ID="ltrError" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
