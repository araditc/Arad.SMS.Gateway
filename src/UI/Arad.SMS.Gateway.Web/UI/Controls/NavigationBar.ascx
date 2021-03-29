<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationBar.ascx.cs" Inherits="MessagingSystem.UI.Controls.NavigationBar" %>
<table style="width: 450px;">
	<tr>
		<td>
			
		</td>
		<td>
			<asp:Label ID="lblRecordNo" runat="server" Text=""></asp:Label>
		</td>
		<td class="titleCell">
			<asp:Label ID="lblPage" runat="server" Text="صفحه"></asp:Label>
		</td>
		<td>
			<asp:DropDownList ID="drpPageNo" Width="100" class="input"  runat="server">
			</asp:DropDownList>
		</td>
		<td class="titleCell">
			<asp:Label ID="lblRowCount" runat="server" Text="تعداد سطر"></asp:Label>
		</td>
		<td>
			<asp:DropDownList ID="drpPageSize" Width="50" class="input" runat="server">
				<asp:ListItem>10</asp:ListItem>
				<asp:ListItem>25</asp:ListItem>
				<asp:ListItem>50</asp:ListItem>
				<asp:ListItem>100</asp:ListItem>
			</asp:DropDownList>
		</td>
	</tr>
</table>