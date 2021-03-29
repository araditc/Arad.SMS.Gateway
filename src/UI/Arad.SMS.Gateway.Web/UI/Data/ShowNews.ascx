<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowNews.ascx.cs" Inherits="MessagingSystem.UI.Data.ShowNews" %>

<div style="margin: 0 auto; border: 1px solid #aaa; border-radius: 5px; width: 98%; overflow: hidden;">
	<div style="background-color: #eee; padding: 5px 5px 5px 0px; color: #f00; font-weight: bold; text-align: right;"><span style="color: #333;"><%=GeneralLibrary.Language.GetString("Title") %>&nbsp;:</span>&nbsp;<asp:Literal ID="ltrTitle" runat="server"></asp:Literal></div>
	<div style="padding: 10px;"><asp:Literal ID="ltrContent" runat="server"></asp:Literal></div>
</div>