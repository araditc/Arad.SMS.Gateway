<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentReport.aspx.cs" Inherits="Arad.SMS.Gateway.Web.PaymentReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .divShowResultPayment {
            color: #555555;
            padding: 30px 10px;
            margin: 10px auto 50px auto;
            width: 400px;
            border: 1px solid #aaa;
            border-radius: 10px;
            box-shadow: 0 0 30px #aaa;
            height: 160px;
        }
    </style>
    <script type="text/javascript"> 
        function closethis() {
           // window.opener.location.href = window.opener.location.href;
          //  window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <div class='divShowResultPayment'>
            <asp:Panel ID="pnlSuccessfulPayment" runat="server" Visible="false" Style="direction: rtl">
                <div style='float: right;'>
                    <asp:Image ID="imgBank" runat="server" />
                </div>
                <div style='float: right; font-weight: bold; color: #9e031a;'><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("OnlinePaymentSuccessful")%></div>
                <div class='clear'></div>

                <ul style="list-style-type: square; color: #eb952c;">
                    <li style="padding: 6px">
                        <div style="font: bold 8pt tahoma; color: #545454;">
                            <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("TransactionNo")%>:
									<asp:Label ID="lblBillNumber" runat="server"></asp:Label>
                        </div>
                    </li>
                    <li style="padding: 6px">
                        <div style="font: bold 8pt tahoma; color: #545454">
                            <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("Amount")%>:
									<asp:Label ID="lblAmount" runat="server"></asp:Label>
                        </div>
                    </li>
                    <li style="padding: 6px">
                        <div style="font: bold 8pt tahoma; color: #545454">
                            <%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("PaymentDate")%>:
									<asp:Label ID="lblDatePayment" runat="server"></asp:Label>
                        </div>
                    </li>
                </ul>
            </asp:Panel>
            <asp:Panel ID="pnlFailedPayment" runat="server" Visible="false" Style="direction: rtl">
                <div style='float: right;'>
                    <asp:Image ID="imgBankFailed" runat="server" />
                </div>
                <div style='margin-top: 10px; font-weight: bold;'><%=Arad.SMS.Gateway.GeneralLibrary.Language.GetString("OnlinePaymentUnSuccessful")%></div>
                <div class='clear'></div>
                <hr />
            </asp:Panel>
            <div class="buttonControlDiv">
                <asp:Button ID="btnReturn" runat="server" Text="Return" CssClass="button" OnClientClick="closethis();" OnClick="btnReturn_Click" />
            </div>
        </div>
    </form>
</body>
</html>
