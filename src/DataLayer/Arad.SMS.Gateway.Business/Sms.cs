using System;
using System.Data;
using Common;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace Business
{
	public class Sms : BusinessEntityBase
	{
		public Sms(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Smses.ToString(), dataAccessProvider) { }

		#region SelectMethods
		public DataTable GetUncertainDeleviryStatusSms(Business.SmsSenderAgentReference smsSenderAgentRefrence)
		{
			return FetchSPDataTable("GetUncertainDeleviryStatusSms", "@SmsSenderAgentRefrence", (int)smsSenderAgentRefrence);
		}

		public DataTable GetGiveBackCreditSms(SmsSenderAgentReference smsSenderAgentReference)
		{
			return FetchSPDataTable("GetGiveBackCreditSms", "@SmsSenderAgentReference", (int)smsSenderAgentReference);
		}
		#endregion

		#region UpdateMethod
		public bool UpdateSmsSendInfo(string xmlSmsInfo)
		{
			return ExecuteSPCommand("UpdateSmsSendInfo", "@XmlSmsInfo", xmlSmsInfo);
		}

		public bool UpdateBlackListSms(Guid userGuid, Guid smsSentGuid, SmsSenderAgentReference agent,
																	 Operators operators, int smsPartCount, SmsSendType smsSendType, int encoding)
		{
			return ExecuteSPCommand("UpdateBlackListSms", "@UserGuid", userGuid,
																									 "@SmsSentGuid", smsSentGuid,
																									 "@Agent", (int)agent,
																									 "@Operator", (int)operators,
																									 "@SmsPartCount", smsPartCount,
																									 "@TypeSend", (int)smsSendType,
																									 "@Encoding", encoding);
		}
		#endregion

		#region SendFutureSms
		public bool SendSingleFutureSms(Guid sentboxGuid, string xmlNumbers)
		{
			return ExecuteSPCommand("SendSingleFutureSms", "@SmsGuid", sentboxGuid,
																										"@Numbers", xmlNumbers);
		}

		public bool SendGroupFutureSms(Guid sentboxGuid, string groupGuid)
		{
			return ExecuteSPCommand("SendGroupFutureSms", "@SmsGuid", sentboxGuid,
																										"@GroupGuid", groupGuid);
		}

		public bool SendBirthDateSms(Guid sentboxGuid, string groupGuid)
		{
			return ExecuteSPCommand("SendBirthDateSms", "@SmsGuid", sentboxGuid,
																									"@GroupGuid", groupGuid);
		}

		public bool SendAdvancedGroupFutureSms(Guid sentboxGuid, string groupGuid, int downRange, int upRange)
		{
			return ExecuteSPCommand("SendAdvancedGroupFutureSms", "@SmsGuid", sentboxGuid,
																														"@GroupGuid", groupGuid,
																														"@DownRange", downRange,
																														"@UpRange", upRange);
		}

		public bool SendSpecialGroupFutureSms(Guid sentboxGuid, string groupGuid)
		{
			return ExecuteSPCommand("SendSpecialGroupFutureSms", "@SmsGuid", sentboxGuid,
																													 "@GroupGuid", groupGuid);
		}

		public bool SendAdvancedSpecialGroupFutureSms(Guid sentboxGuid, string groupGuid, int downRange, int upRange)
		{
			return ExecuteSPCommand("SendAdvancedSpecialGroupFutureSms", "@SmsGuid", sentboxGuid,
																																	 "@GroupGuid", groupGuid,
																																	 "@DownRange", downRange,
																																	 "@UpRange", upRange);
		}

		public bool SendFutureSmsFormat(Guid sentboxGuid, string groupGuid, Guid formatGuid)
		{
			return ExecuteSPCommand("SendFutureSmsFormat", "@SmsGuid", sentboxGuid,
																										 "@GroupGuid", groupGuid,
																										 "@FormatGuid", formatGuid);
		}

		public bool SendPeriodFutureSingleSms(Guid sentboxGuid, string xmlNumbers, DateTime nextDateTime, bool sendIsFinish)
		{
			return ExecuteSPCommand("SendPeriodFutureSingleSms", "@SmsGuid", sentboxGuid,
																													"@XmlString", xmlNumbers,
																													"@NextDateTime", nextDateTime,
																													"@SendIsFinish", sendIsFinish);
		}

		public bool SendPeriodFutureGroupSms(Guid sentboxGuid, string groupGuid, DateTime nextDateTime, bool sendIsFinish)
		{
			return ExecuteSPCommand("SendPeriodFutureGroupSms", "@SmsGuid", sentboxGuid,
																													"@GroupGuid", groupGuid,
																													"@NextDateTime", nextDateTime,
																													"@SendIsFinish", sendIsFinish);
		}

		public bool SendFutureSmsForUsers(Guid sentboxGuid)
		{
			return ExecuteSPCommand("SendFutureSmsForUsers", "@SmsGuid", sentboxGuid);
		}

		public bool SendSmsForUsers(Guid sentboxGuid, Guid smsBodyGuid, Guid userRoleGuid, bool decreaseFromUser, int TypeSend)
		{
			return base.ExecuteSPCommand("SendSmsForUsers", "@SmsGuid", sentboxGuid,
																											"@SmsBodyGuid", smsBodyGuid,
																											"@RoleGuid", userRoleGuid,
																											"@DecreaseFromUser", decreaseFromUser,
																											"@TypeSendSingle", TypeSend);
		}

		public bool SendPrefixSms(Guid sentboxGuid)
		{
			return ExecuteSPCommand("SendPrefixSms", "@SmsGuid", sentboxGuid);
		}

		public bool SendCitySms(Guid sentboxGuid)
		{
			return ExecuteSPCommand("SendCitySms", "@SmsGuid", sentboxGuid);
		}

		public bool SendPostalCodeSms(Guid sentboxGuid)
		{
			return ExecuteSPCommand("SendPostalCodeSms", "@SmsGuid", sentboxGuid);
		}
		#endregion

		//public void UpdateDeliveryStatus(long outerSystemMessageID, int status)
		//{
		//	ExecuteSPCommand("UpdateOneSmsDeliveryStatus", "@OuterSystemSmsID", outerSystemMessageID, "@DeliveryStatus", status);
		//}

		public void UpdateDeliveryStatus(string outerSystemMessageID, int state)
		{
			ExecuteSPCommand("UpdateSmsDeliveryStatus", "@OuterSystemSmsIDs", outerSystemMessageID, "@DeliveryStatus", state);
		}

		public void UpdateDeliveryStatus(string batchID, string numbers, int state)
		{
			ExecuteSPCommand("UpdateSmsDeliveryStatusByNumber", "@BatchID", batchID, "@Numbers", numbers, "@DeliveryStatus", state);
		}

		public DataTable GetPagedSmses(Common.Sms sms, Guid smsSenderAgentGuid, string userName, string senderNumber, string fromEffectiveDate, string fromTime, string toEffectiveDate, string toTime, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			DataSet dataSetSms = base.FetchSPDataSet("GetPagedSmses", "@UserGuid", sms.UserGuid,
																														 "@UserName", userName,
																														 "@SmsSenderAgentGuid", smsSenderAgentGuid,
																														 "@SenderNumber", senderNumber,
																														 "@RecieverNumber", sms.Reciever,
																														 "@FromEffectiveDate", fromEffectiveDate,
																														 "@FromTime", fromTime,
																														 "@ToEffectiveDate", toEffectiveDate,
																														 "@ToTime", toTime,
																														 "@SmsBody", sms.SmsBody,
																														 "@DeliveryStatus", sms.DeliveryStatus,
																														 "@SmsCount", sms.SmsCount,
																														 "@PageNo", pageNo,
																														 "@PageSize", pageSize,
																														 "@SortField", sortField);
			resultCount = Helper.GetInt(dataSetSms.Tables[0].Rows[0]["RowCount"]);
			return dataSetSms.Tables[1];
		}

		public DataSet GetCountSmsInPeriodDate(Guid userGuid)
		{
			return base.FetchSPDataSet("GetCountSmsInPeriodDate", "@UserGuid", userGuid);
		}

		public DataTable GetBlackListNumber(Guid smsSentGuid, SmsSendError smsSendError)
		{
			return FetchSPDataTable("GetSpecificOuterSystemID", "@SmsSentGuid", smsSentGuid,
																													"@OuterSystemSmsID", (int)smsSendError);
		}

		public DataTable GetFailedSmsByPriority(SmsSenderAgentReference smsSenderAgentRefrence, int count)
		{
			return FetchSPDataTable("GetFailedSmsByPriority", "@SmsSenderAgentRefrence", smsSenderAgentRefrence,
																											 "@Count", count);
		}

		public DataTable GetFailedSms(SmsSenderAgentReference smsSenderAgentRefrence, int count)
		{
			return FetchSPDataTable("GetFailedSms", "@SmsSenderAgentRefrence", smsSenderAgentRefrence,
																											 "@Count", count);
		}

		public object UpdateFailedSms(Common.Sms sms)
		{
			return FetchSPDataTable("UpdateFailedSms", "@Guid", sms.SmsGuid,
																								 "@DeliveryStatus", sms.DeliveryStatus,
																								 "@OuterMessageID", sms.OuterSystemSmsID);
		}

		public DataTable GetSentboxQueue(SmsSenderAgentReference smsSenderAgentRefrence, int agentQueueSize, int threadCount)
		{
			return FetchSPDataTable("GetSentboxQueue", "@SmsSenderAgentRefrence", smsSenderAgentRefrence,
																								 "@Count", agentQueueSize,
																								 "@ThreadCount", threadCount);
		}
	}
}
