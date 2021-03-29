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

using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.ManageThread;
using Arad.SMS.Gateway.RahyabRGSmsSender.ServiceManager;
using Arad.SMS.Gateway.SqlLibrary;
using System;
using System.Collections;
using System.Linq;

namespace Arad.SMS.Gateway.RahyabRGSmsSender
{
    public class RahyabRGSmsServiceManager : ISmsServiceManager
    {
        public SmsSenderAgentReference SmsSenderAgentReference
        {
            get
            {
                return SmsSenderAgentReference.RahyabRG;
            }
        }

        public void SendSms(BatchMessage batchMessage)
        {
            try
            {
                string logFile = string.Format(@"{0}\{1}", ConfigurationManager.GetSetting("RecipientAddress"), batchMessage.Id);
                string sendStatus = "", batchId = "", amount = "", chargingAmount = "";
                Cls_SMS.ClsSend sms_Batch = new Cls_SMS.ClsSend();
                ArrayList ret2 = new ArrayList();
                try
                {
                    LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, " /" + batchMessage.Guid.ToString() + " / " + " receiver Count : " + batchMessage.Receivers.Select(reciever => reciever.RecipientNumber).Count()
                        + " SenderNumber : " + batchMessage.SenderNumber + " Username : " + batchMessage.Username + " Password : " + batchMessage.Password
                        + " SendLink : " + batchMessage.SendLink + " Domain : " + batchMessage.Domain
                        + " IsFlash : " + batchMessage.IsFlash + " SmsText : " + batchMessage.SmsText);

                    ret2 = sms_Batch.SendSMS_Batch_Full(batchMessage.SmsText,
                                                                                   batchMessage.Receivers.Select(reciever => reciever.RecipientNumber).ToArray(),
                                                                                   batchMessage.SenderNumber,
                                                                                   batchMessage.Username,
                                                                                   batchMessage.Password,
                                                                                   batchMessage.SendLink,
                                                                                   batchMessage.Domain,
                                                                                   batchMessage.IsFlash,
                                                                                   ref sendStatus,
                                                                                   ref batchId,
                                                                                   ref amount,
                                                                                   ref chargingAmount);

                    LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, " /" + batchMessage.Guid.ToString() + " / " + " ret2 Count : " + ret2.Count + " sendStatus : " + sendStatus
                        + " batchId : " + batchId + " amount : " + amount + "\\ chargingAmount : " + chargingAmount);
                    try
                    {
                        LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, "Try : 1 | Date : " + DateTime.Now.ToString().Replace(':','-') + " | Guid : " + batchMessage.Guid.ToString() + " | ret2 Count : " + ret2.Count + " | sendStatus : " + sendStatus
                       + " | batchId : " + batchId + " | amount : " + amount + "| chargingAmount : " + chargingAmount);

                    }
                    catch (Exception ex)
                    {
                        LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, "Error : " + ex.Message);
                    }

                    if (sendStatus.Contains("Uknown Error"))
                    {
                        ret2 = new ArrayList();
                        sendStatus = ""; batchId = ""; amount = ""; chargingAmount = "";
                        ret2 = sms_Batch.SendSMS_Batch_Full(batchMessage.SmsText,
                                                                                 batchMessage.Receivers.Select(reciever => reciever.RecipientNumber).ToArray(),
                                                                                 batchMessage.SenderNumber,
                                                                                 batchMessage.Username,
                                                                                 batchMessage.Password,
                                                                                 batchMessage.SendLink,
                                                                                 batchMessage.Domain,
                                                                                 batchMessage.IsFlash,
                                                                                 ref sendStatus,
                                                                                 ref batchId,
                                                                                 ref amount,
                                                                                 ref chargingAmount);

                        LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, "Try 2 /" + batchMessage.Guid.ToString() + " / " + " ret2 Count : " + ret2.Count + " sendStatus : " + sendStatus
                            + " batchId : " + batchId + " amount : " + amount + "\\ chargingAmount : " + chargingAmount);
                        try
                        {
                            LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, "Try : 2 | Date : " + DateTime.Now.ToString().Replace(':', '-') + " | Guid : " + batchMessage.Guid.ToString() + " | ret2 Count : " + ret2.Count + " | sendStatus : " + sendStatus
                           + " | batchId : " + batchId + " | amount : " + amount + "| chargingAmount : " + chargingAmount);

                        }
                        catch (Exception ex)
                        {
                            LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, "Error : " + ex.Message);
                        }

                        if (sendStatus.Contains("Uknown Error"))
                        {
                            ret2 = new ArrayList();
                            sendStatus = ""; batchId = ""; amount = ""; chargingAmount = "";
                            ret2 = sms_Batch.SendSMS_Batch_Full(batchMessage.SmsText,
                                                                                     batchMessage.Receivers.Select(reciever => reciever.RecipientNumber).ToArray(),
                                                                                     batchMessage.SenderNumber,
                                                                                     batchMessage.Username,
                                                                                     batchMessage.Password,
                                                                                     batchMessage.SendLink,
                                                                                     batchMessage.Domain,
                                                                                     batchMessage.IsFlash,
                                                                                     ref sendStatus,
                                                                                     ref batchId,
                                                                                     ref amount,
                                                                                     ref chargingAmount);

                            LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, "Try 3 /" + batchMessage.Guid.ToString() + " / " + " ret2 Count : " + ret2.Count + " sendStatus : " + sendStatus
                                + " batchId : " + batchId + " amount : " + amount + "\\ chargingAmount : " + chargingAmount);
                            try
                            {
                                LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, "Try : 3 | Date : " + DateTime.Now.ToString().Replace(':', '-') + " | Guid : " + batchMessage.Guid.ToString() + " | ret2 Count : " + ret2.Count + " | sendStatus : " + sendStatus
                               + " | batchId : " + batchId + " | amount : " + amount + "| chargingAmount : " + chargingAmount);

                            }
                            catch (Exception ex)
                            {
                                LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, "Error : " + ex.Message);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    foreach (InProgressSms sms in batchMessage.Receivers)
                    {
                        sms.SendStatus = (int)SendStatus.ErrorInSending;
                        sms.DeliveryStatus = (int)DeliveryStatus.NotSent;
                        sms.SendTryCount += 1;
                    }

                    throw ex;
                }

                switch (sendStatus.ToLower())
                {
                    case "true":
                    case "check_ok":
                        // foreach (InProgressSms sms in batchMessage.Receivers)
                        // {                 
                        foreach (InProgressSms sms in batchMessage.Receivers)
                        {
                            sms.ReturnID = batchId.Split('+')[1];
                            sms.SendStatus = (int)SendStatus.Sent;
                            sms.DeliveryStatus = (int)DeliveryStatus.ReceivedByItc;
                        }
                        for (int i = 0; i < ret2.Count; i++)
                        {
                            CheckErrorSms((Cls_SMS.ClsSend.STC_SMSSend)ret2[i], batchMessage.Receivers.Where(_ => _.RecipientNumber == (ret2[i] as Cls_SMS.ClsSend.STC_SMSSend).DestAddress).FirstOrDefault(), batchId);
                        }
                        // }
                        break;
                    case "false":
                    case "check_error":
                        foreach (InProgressSms sms in batchMessage.Receivers)
                        {
                            sms.ReturnID = batchId.Split('+')[1];
                            sms.SendStatus = (int)SendStatus.ErrorInSending;
                            sms.DeliveryStatus = (int)DeliveryStatus.ErrorInSending;
                        }
                        for (int i = 0; i < ret2.Count; i++)
                        {
                            CheckErrorSms((Cls_SMS.ClsSend.STC_SMSSend)ret2[i], batchMessage.Receivers.Where(_ => _.RecipientNumber == (ret2[i] as Cls_SMS.ClsSend.STC_SMSSend).DestAddress).FirstOrDefault(), batchId);
                        }
                        break;
                    default:
                        foreach (InProgressSms sms in batchMessage.Receivers)
                        {
                            sms.SendStatus = (int)SendStatus.ErrorInSending;
                            sms.DeliveryStatus = (int)DeliveryStatus.NotSent;
                            sms.SendTryCount += 1;
                        }
                        break;

                }

                LogController<ServiceLogs>.LogInFile(logFile,
                                                            string.Format("Id={0}Guid={1}Sms={2}Receivers={3}{4}",
                                                            batchMessage.Id,
                                                            batchMessage.Guid,
                                                            batchMessage.SmsText,
                                                            string.Join(";", batchMessage.Receivers.Select<InProgressSms, string>(sms => string.Format("{0}-{1}-{2}", sms.RecipientNumber, sms.ReturnID, batchMessage.PageNo))),
                                                            Environment.NewLine));
            }
            catch (Exception ex)
            {
                LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, string.Format("\r\n-------------------------------------------------------------------------"));
                LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, string.Format("\r\n{0} : Message : {1}", "send sms method", ex.Message));
                LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, string.Format("\r\n{0} : OutboxGuid : {1}", "send sms method", batchMessage.Guid.ToString()));
                LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, string.Format("\r\n{0} : StackTrace : {1}", "send sms method", ex.StackTrace));
                LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, string.Format("\r\n-------------------------------------------------------------------------"));
            }
        }

        private void CheckErrorSms(Cls_SMS.ClsSend.STC_SMSSend result, InProgressSms inProgressSms, string batchId)
        {

            try
            {
                inProgressSms.SendTryCount += 1;
                inProgressSms.RecipientNumber = result.DestAddress;
                //inProgressSms.SendStatus = (int) SendStatus.Sent;
                //inProgressSms.DeliveryStatus = (int) DeliveryStatus.Sent;

                //  LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, result.DestAddress + " / Response : " + result.Response + " / Status : " + result.Status);
                bool setSmsStatus = false;

                switch (result.Response)
                {

                    case "Insufficient_Balance_Amount":
                        setSmsStatus = true;
                        break;

                    case "BLACKLISTED_DESTINATION":
                        inProgressSms.SendStatus = (int)SendStatus.BlackList;
                        inProgressSms.DeliveryStatus = (int)DeliveryStatus.BlackList;
                        break;

                    case "SOURCE_ADDRESS_NOT_ALLOWED":
                        setSmsStatus = true;
                        break;

                }

                if (setSmsStatus)
                {
                    inProgressSms.SendStatus = (int)SendStatus.ErrorInSending;
                    inProgressSms.DeliveryStatus = (int)DeliveryStatus.NotSent;
                }
            }
            catch (Exception ex)
            {
                LogController<ServiceLogs>.LogInFile(ServiceLogs.RahyabRG, ex.Message);
                throw ex;
            }
        }   
    }
}
