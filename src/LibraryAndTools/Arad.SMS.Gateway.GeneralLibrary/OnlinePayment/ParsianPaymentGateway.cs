// --------------------------------------------------------------------
// Copyright (c) 2005-2020 Arad ITC.
//
// Author : Ammar Heidari <ammar@arad-itc.org>
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------

using Arad.SMS.Gateway.GeneralLibrary.Parsian;
using System;
using System.Text;

namespace Arad.SMS.Gateway.GeneralLibrary.OnlinePayment
{
    public class ParsianPaymentGateway
    {

        // private EShopService service;
        private ir.shaparak.pec.SaleService saleService; 
        private ir.shaparak.pec1.ConfirmService confirmService;
        private ir.shaparak.pec2.ReversalService reversalService;
        
        //private string postUrl = "https://pec.shaparak.ir/pecpaymentgateway";
        //private string pin;
        //private int orderID;
        //private int amount = 0;
        //private string callBackUrl;
        private static Random random;

        public enum Status
        {
            Successful = 0,
            PreRequest = 1,
            AccessViolation = 20,
            MerchantAuthenticationFailed = 22,
            SaleIsAlreadyDoneSuccessfully = 30,
            InvalidMerchantOrder = 34,
        }

        public string PostUrl { get { return "https://pec.shaparak.ir/NewIPG/?token={0}"; } }

        public string TokenThis { get; set; }

        public string Token { get; set; }

        public string Pin { get; set; }

        public long OrderID { get; set; }

        public int Amount { get; set; }

        public string CallBackUrl { get; set; }

        public string GatewayPaymentIP { get { return ConfigurationManager.GetSetting("ParsianGatewayPaymentIP"); } }

        public ParsianPaymentGateway()
        {

            random = new Random();
            // service = new EShopService();
            saleService = new ir.shaparak.pec.SaleService();
            confirmService = new ir.shaparak.pec1.ConfirmService();
            reversalService = new ir.shaparak.pec2.ReversalService();
            OrderID = DateTime.Now.Ticks;
        }

        public string Request()
        {
            try
            {
                //long authority = 0;
                // byte status = 0;
                System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback((o, xc, xch, sslP) => true);

                ir.shaparak.pec.ClientSaleRequestData clientRequestData = new ir.shaparak.pec.ClientSaleRequestData();

                clientRequestData.Amount = Amount;
                clientRequestData.LoginAccount = Pin;
                clientRequestData.OrderId = OrderID;
                clientRequestData.CallBackUrl = CallBackUrl;

                var clientResponse = saleService.SalePaymentRequest(clientRequestData);

                // service.PinPaymentRequest(Pin, Amount, OrderID, CallBackUrl, ref authority, ref status);
                //throw new Exception(clientResponse.Token + " $ " + clientResponse.Status + " $ " + clientResponse.Message);

                foreach (Status result in Enum.GetValues(typeof(Status)))
                    if (clientResponse.Status == (int) result && result != Status.Successful)
                        throw new Exception(Language.GetString(result.ToString()));

                if (clientResponse.Token == -1)
                    throw new Exception(Language.GetString("InvalidAuthority"));


                TokenThis = clientResponse.Token.ToString();

                return clientResponse.Token.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void VerifyRequest(long authority, ref byte status, ref long invoiceNumber)
        public void VerifyRequest(long token)
        {
            try
            {
                // service.PaymentEnquiry(Pin, authority, ref status, ref invoiceNumber);
                var clientRequestData = new ir.shaparak.pec1.ClientConfirmRequestData();
                clientRequestData.Token = token;
                clientRequestData.LoginAccount = Pin;
                var clientResponse = confirmService.ConfirmPayment(clientRequestData);

                foreach (Status result in Enum.GetValues(typeof(Status)))
                    if (Helper.GetInt(clientResponse.Status) == (int) result && result != (int) Status.Successful)
                        throw new Exception(Language.GetString(result.ToString()));

                if (clientResponse.RRN == -1)
                    throw new Exception(Language.GetString("InvalidInvoiceNumber"));

                // SettleRequest(Pin);
            }
            catch
            {
                ReversalRequest(token);
            }
        }

        public void ReversalRequest(long token)
        {
            try
            {
                byte status = (int) Status.PreRequest;

                var clientRequestData = new ir.shaparak.pec2.ClientReversalRequestData();
                clientRequestData.LoginAccount = Pin;
                clientRequestData.Token = token;

                reversalService.ReversalRequest(clientRequestData);//.PinReversal(Pin, OrderID, OrderID, ref status);

                foreach (Status result in Enum.GetValues(typeof(Status)))
                    if (Helper.GetInt(status) == (int) result && result != (int) Status.Successful)
                        throw new Exception(Language.GetString(result.ToString()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public string GotoGateway(string refId)
        //{
        //    if (refId == "0" || refId == "")
        //        refId = Token;
        //    if (refId == "0" || refId == "")
        //        refId = TokenThis;

        //    StringBuilder script = new StringBuilder();
        //    script.Append("function postRefId(refIdValue) {");          
        //    script.Append("var form = document.createElement('form');");
        //    script.Append("form.setAttribute('method', 'POST');");
        //    script.Append("form.setAttribute('action', '" + PostUrl + "?Token=" + refId + "');");
        //    script.Append("form.setAttribute('target', '_parent');");          
        //    script.Append("document.body.appendChild(form);");
        //    script.Append("form.submit();");
        //    script.Append("document.body.removeChild(form);");
        //    script.Append("}");
        //    script.Append("postRefId('" + refId + "');");
        //    return script.ToString();
        //}


        //public void SettleRequest(string pinCode)
        //{
        //    try
        //    {
        //        byte status = (int) Status.PreRequest;
        //        service.PinSettlement(pinCode, ref status);

        //        foreach (Status result in Enum.GetValues(typeof(Status)))
        //            if (status == (int) result && result != Status.Successful)
        //                throw new Exception(Language.GetString(result.ToString()));
        //    }
        //    catch
        //    {
        //        ReversalRequest();
        //    }
        //}
    }
}