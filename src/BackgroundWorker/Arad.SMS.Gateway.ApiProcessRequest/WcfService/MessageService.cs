using Business;
using Common;
using GeneralLibrary;
using SqlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;

namespace ApiProcessRequest
{
	public class MessageService : IMessageService
	{
		[OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
		public void ProcessMessage(MsmqMessage<BatchMessage> message)
		{
			Common.ScheduledSms scheduledSms = new Common.ScheduledSms();

			try
			{
				BatchMessage batchMessage = (BatchMessage)message.Body;

				if (string.IsNullOrEmpty(batchMessage.ServiceId))
				{
					scheduledSms.PrivateNumberGuid = Facade.PrivateNumber.GetUserNumberGuid(batchMessage.SenderNumber, batchMessage.UserGuid);
					scheduledSms.SmsText = batchMessage.SmsText;
					scheduledSms.SmsLen = batchMessage.SmsLen;
					scheduledSms.PresentType = batchMessage.IsFlash ? (int)Business.Messageclass.Flash : (int)Business.Messageclass.Normal;
					scheduledSms.Encoding = batchMessage.IsUnicode ? (int)Encoding.Utf8 : (int)Encoding.Default;
					scheduledSms.UserGuid = batchMessage.UserGuid;
					scheduledSms.Status = (int)ScheduledSmsStatus.Stored;
					scheduledSms.DateTimeFuture = DateTime.Now;

					List<string> lstReceivers = batchMessage.Receivers.Select<InProgressSms, string>(receiver => receiver.RecipientNumber).ToList();

					Facade.ScheduledSms.InsertSms(scheduledSms, lstReceivers);
				}
				else
				{
					List<string> lstReceivers = new List<string>();

					scheduledSms.PrivateNumberGuid = batchMessage.PrivateNumberGuid;
					scheduledSms.SmsText = batchMessage.SmsText;
					scheduledSms.SmsLen = batchMessage.SmsLen;
					scheduledSms.PresentType = batchMessage.IsFlash ? (int)Business.Messageclass.Flash : (int)Business.Messageclass.Normal;
					scheduledSms.Encoding = batchMessage.IsUnicode ? (int)Encoding.Utf8 : (int)Encoding.Default;
					scheduledSms.UserGuid = batchMessage.UserGuid;
					scheduledSms.Status = (int)ScheduledSmsStatus.Stored;
					scheduledSms.DateTimeFuture = DateTime.Now;
					if (batchMessage.Receivers.Count > 0)
					{
						lstReceivers = batchMessage.Receivers.Select<InProgressSms, string>(receiver => receiver.RecipientNumber).ToList();
						Facade.ScheduledSms.InsertSms(scheduledSms, lstReceivers);
					}
					else
					{
						scheduledSms.ReferenceGuid = batchMessage.ReferenceGuid.ToString();
						Facade.ScheduledSms.InsertGroupSms(scheduledSms);
					}
				}
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.APIProcessRequest, string.Format("On ProcessMessage : {0}", ex.Message));
			}
		}
	}
}
