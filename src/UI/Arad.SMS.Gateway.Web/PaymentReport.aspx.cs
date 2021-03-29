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

using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.OnlinePayment;
using Arad.SMS.Gateway.SqlLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace Arad.SMS.Gateway.Web
{
    public partial class PaymentReport : System.Web.UI.Page
    {
        Dictionary<OnlinePaymentParams, string> dicParams;
        private bool isPaymentSuccessful = false;
        public void InitializePage()
        {
            dicParams = new Dictionary<OnlinePaymentParams, string>();
            dicParams.Add(OnlinePaymentParams.IsExtendedPanel, Helper.Request(this, "extendedpanel"));
            dicParams.Add(OnlinePaymentParams.IsExtendedNumber, Helper.Request(this, "extendednumber"));
            dicParams.Add(OnlinePaymentParams.SalePackageId, Request["salepackage"]);
            //dicParams.Add(OnlinePaymentParams.Ip, GetIp());

            switch (Helper.GetInt(Request["bank"]))
            {
                case (int)Banks.Mellat:
                    imgBank.ImageUrl = "/pic/mellat.png";
                    imgBankFailed.ImageUrl = "/pic/mellat.png";
                    dicParams.Add(OnlinePaymentParams.ReferenceId, Request.Params["RefId"]);
                    dicParams.Add(OnlinePaymentParams.ResCode, Helper.Request(this, "ResCode"));
                    dicParams.Add(OnlinePaymentParams.SaleOrderId, Helper.Request(this, "SaleOrderId"));
                    dicParams.Add(OnlinePaymentParams.SaleReferenceId, Helper.Request(this, "SaleReferenceId"));
                    break;
                case (int)Banks.Parsian:
                    imgBank.ImageUrl = "/pic/parsian.png";
                    imgBankFailed.ImageUrl = "/pic/parsian.png";
                    dicParams.Add(OnlinePaymentParams.Token, Request.Params["Token"]);
                    dicParams.Add(OnlinePaymentParams.ReferenceId, Request.Params["Token"]);
                    dicParams.Add(OnlinePaymentParams.OrderId, Request.Params["OrderId"]);
                    dicParams.Add(OnlinePaymentParams.TerminalNo, Request.Params["TerminalNo"]);
                    dicParams.Add(OnlinePaymentParams.RRN, Request.Params["RRN"]);
                    dicParams.Add(OnlinePaymentParams.status, Request.Params["status"]);
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitializePage();

            Common.FailedOnlinePayment failedOnlinePayment = new Common.FailedOnlinePayment();
            StringBuilder result = new StringBuilder();
            btnReturn.Text = Language.GetString(btnReturn.Text);
            DataTable fishInfo = new DataTable();

            try
            {
                string errorMessage = string.Empty;

                fishInfo = Facade.AccountInformation.GetAccountOfReferenceID(dicParams[OnlinePaymentParams.ReferenceId]);

                if (fishInfo.Rows.Count == 0)
                    throw new Exception();

                switch (Helper.GetInt(Request["bank"]))
                {
                    case (int)Banks.Mellat:
                        BehPardakhtMellat behPardakht = new BehPardakhtMellat();

                        //if (dicParams[OnlinePaymentParams.Ip] != behPardakht.GatewayPaymentIP)
                        //	throw new Exception(string.Format("{0},InputIP={1},ValidIp={2}", Language.GetString("IPIsInvalid"), dicParams[OnlinePaymentParams.Ip], behPardakht.GatewayPaymentIP));

                        foreach (BehPardakhtMellat.ResCode resCodeList in Enum.GetValues(typeof(BehPardakhtMellat.ResCode)))
                            if (Helper.GetInt(dicParams[OnlinePaymentParams.ResCode]) == (int)resCodeList &&
                                    resCodeList != (int)BehPardakhtMellat.ResCode.TransactionWasSuccessful)
                                throw new Exception(Language.GetString(resCodeList.ToString()));

                        behPardakht.TerminalID = Helper.GetLong(fishInfo.Rows[0]["TerminalID"]);
                        behPardakht.UserName = fishInfo.Rows[0]["UserName"].ToString();
                        behPardakht.Password = fishInfo.Rows[0]["Password"].ToString();
                        behPardakht.OrderID = Helper.GetLong(dicParams[OnlinePaymentParams.SaleOrderId]);
                        behPardakht.VerifyRequest(Helper.GetLong(dicParams[OnlinePaymentParams.SaleReferenceId]));

                        isPaymentSuccessful = true;

                        break;
                    case (int)Banks.Parsian:
                        ParsianPaymentGateway parsian = new ParsianPaymentGateway();

                        if (dicParams[OnlinePaymentParams.status] != ((int)ParsianPaymentGateway.Status.Successful).ToString())
                            throw new Exception();

                        parsian.Pin = fishInfo.Rows[0]["PinCode"].ToString();
                        parsian.VerifyRequest(Helper.GetLong(dicParams[OnlinePaymentParams.Token]));
                        dicParams[OnlinePaymentParams.SaleReferenceId] = dicParams[OnlinePaymentParams.RRN].ToString();

                        isPaymentSuccessful = true;

                        break;
                }

                if (!isPaymentSuccessful)
                    throw new Exception();

                if (Helper.GetBool(dicParams[OnlinePaymentParams.IsExtendedPanel]))
                {
                    Facade.Fish.UpdateDescription(Helper.GetGuid(fishInfo.Rows[0]["FishGuid"]), Language.GetString("ExtendedPanel"), FishStatus.Confirmed);
                    if (!Facade.User.UpdateExpireDate(Helper.GetGuid(fishInfo.Rows[0]["UserGuid"]), DateTime.Now.AddYears(1)))
                        throw new Exception(Language.GetString("ErrorRecord"));
                }
                else if (Helper.GetBool(dicParams[OnlinePaymentParams.IsExtendedNumber]))
                {
                    Common.PrivateNumber privateNumber = Facade.PrivateNumber.LoadNumber(Helper.GetGuid(fishInfo.Rows[0]["ReferenceGuid"]));
                    Facade.Fish.UpdateDescription(Helper.GetGuid(fishInfo.Rows[0]["FishGuid"]), string.Format("{0}-{1}", Language.GetString("ExtendedNumber"), privateNumber.Number), FishStatus.Confirmed);
                    if (!Facade.PrivateNumber.UpdateExpireDate(privateNumber.NumberGuid, DateTime.Now.AddYears(1)))
                        throw new Exception(Language.GetString("ErrorRecord"));
                }
                else if (Helper.GetInt(dicParams[OnlinePaymentParams.SalePackageId]) != 0)
                {
                    Facade.Fish.UpdateDescription(Helper.GetGuid(fishInfo.Rows[0]["FishGuid"]), Language.GetString("BuyPanel"), FishStatus.Confirmed);
                    DataTable dtRole = Facade.Role.GetPackage(Helper.GetInt(dicParams[OnlinePaymentParams.SalePackageId]));
                    Facade.User.UpdateUserRole(Helper.GetGuid(fishInfo.Rows[0]["UserGuid"]), Helper.GetGuid(dtRole.Rows[0]["Guid"]));
                }
                else
                {
                    Facade.Fish.ConfirmOnlineFish(Helper.GetGuid(fishInfo.Rows[0]["UserGuid"]),
                                                                                Helper.GetLong(fishInfo.Rows[0]["SmsCount"]),
                                                                                TypeCreditChanges.OnlinePayment,
                                                                                string.Format(Language.GetString("OnlinePaymentTransaction"), dicParams[OnlinePaymentParams.SaleReferenceId]),
                                                                                Helper.GetGuid(fishInfo.Rows[0]["FishGuid"]),
                                                                                Helper.GetLong(dicParams[OnlinePaymentParams.SaleReferenceId]));
                }

                lblAmount.Text = fishInfo.Rows[0]["Amount"].ToString();
                lblBillNumber.Text = dicParams[OnlinePaymentParams.SaleReferenceId];
                lblDatePayment.Text = DateManager.GetSolarDate(DateTime.Now);

                pnlFailedPayment.Visible = false;
                pnlSuccessfulPayment.Visible = true;
            }
            catch (Exception ex)
            {
                Log log = new Log();
                Guid refenceGuid = fishInfo.Rows.Count > 0 ? Helper.GetGuid(fishInfo.Rows[0]["FishGuid"]) : Guid.Empty;

                log.Type = (int)LogType.Error;
                log.ReferenceGuid = refenceGuid;
                log.Source = "RegisterFish";
                log.Name = "OnlinePayment";
                log.Text = ex.Message;
                log.CreateDate = DateTime.Now;
                log.IPAddress = Request.UserHostAddress;
                log.Browser = Request.Browser.Browser;
                SQLHelper.InsertLogInfo(log);

                if (refenceGuid != Guid.Empty)
                    Facade.Fish.UpdateDescription(refenceGuid, ex.Message, FishStatus.Rejected);

                pnlSuccessfulPayment.Visible = false;
                pnlFailedPayment.Visible = true;

                Label1.Text += "//" + ex.Message.ToString();
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                string domainName = Helper.GetDomain(Request.Url.Authority);
                Desktop desktop;
                Theme theme;
                DefaultPages defaultPage;
                Facade.Domain.GetDomainInfo(domainName, out desktop, out defaultPage, out theme);

                //int userTheme = Helper.GetInt(Facade.UserSetting.GetSettingValue(Helper.GetGuid(Session["UserGuid"]), "Theme"));
                //if (userTheme != 0)
                //theme = (Business.Theme)userTheme;

                Session["DeskTop"] = (int)desktop;
                //Session["Theme"] = (int)theme;
                Response.Redirect(string.Format("http://{0}/{1}", domainName, desktop.ToString().ToLower()));

            }
            catch { }
        }

        //private string GetIp()
        //{
        //	string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //	if (string.IsNullOrEmpty(ip))
        //		ip = Request.ServerVariables["REMOTE_ADDR"];

        //	return ip;
        //}
    }
}