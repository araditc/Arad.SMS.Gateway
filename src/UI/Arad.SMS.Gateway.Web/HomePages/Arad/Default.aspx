<%@Page Title="" Language="C#" MasterPageFile="~/HomePages/Arad/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Arad.SMS.Gateway.Web.HomePages.Arad.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="clearfix"></div>
	<% if(Session["Language"].ToString() == "en") { %>
        <div class="container" style="margin-top:80px;">
    <% }  else
       {%>
        <div class="container" style="margin-top:80px;" dir="rtl">
    <%  } %>
		<h1>
			<asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
		</h1>
		<div>
			<asp:Literal ID="ltrBody" runat="server"></asp:Literal>
		</div>
	</div>
</asp:Content>
