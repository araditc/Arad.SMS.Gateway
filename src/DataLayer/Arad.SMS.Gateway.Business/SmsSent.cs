using System;
using System.Data;
using Common;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace Business
{
	public class SmsSent : BusinessEntityBase
	{
		public SmsSent(DataAccessBase dataAccessProvider = null)
			: base(TableNames.SmsSents.ToString(), dataAccessProvider) { }

		#region InsertMethod
		//public bool InsertSendSingleSms(Common.SmsSent smsSent, string xmlNumbers)
		//{
		//	return ExecuteSPCommand("InsertSendSingleSms", "@Guid", smsSent.SmsSentGuid,
		//																									"@PrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																									"@Reciever", smsSent.Reciever,
		//																									"@PresentType", smsSent.PresentType,
		//																									"@SmsBody", smsSent.SmsBody,
		//																									"@SmsCount", smsSent.SmsCount,
		//																									"@Encoding", smsSent.Encoding,
		//																									"@TypeSend", smsSent.TypeSend,
		//																									"@State", smsSent.State,
		//																									"@CreateDate", smsSent.CreateDate,
		//																									"@UserGuid", smsSent.UserGuid,
		//																									"@XmlString", xmlNumbers);
		//}

		//public bool InsertSendGroupSms(Common.SmsSent smsSent, Guid privateNumberGuid, Business.SmsSenderAgentReference agent)
		//{

		//	return base.ExecuteSPCommand("InsertSendGroupSms", "@Guid", smsSent.SmsSentGuid,
		//																								"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																								"@PrivateNumberGuid", privateNumberGuid,
		//																								"@SmsSenderAgentReference", (int)agent,
		//																								"@PresentType", smsSent.PresentType,
		//																								"@SmsBody", smsSent.SmsBody,
		//																								"@SmsCount", smsSent.SmsCount,
		//																								"@Encoding", smsSent.Encoding,
		//																								"@TypeSend", smsSent.TypeSend,
		//																								"@State", smsSent.State,
		//																								"@CreateDate", smsSent.CreateDate,
		//																								"@GroupGuid", smsSent.GroupGuid,
		//																								"@UserGuid", smsSent.UserGuid);

		//}

		//public bool InsertSendSpecialGroupSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return base.ExecuteSPCommand("InsertSendSpecialGroupSms", "@Guid", smsSent.SmsSentGuid,
		//																										"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																										"@PrivateNumberGuid", privateNumberGuid,
		//																										"@SmsSenderAgentReference", (int)agent,
		//																										"@PresentType", smsSent.PresentType,
		//																										"@SmsBody", smsSent.SmsBody,
		//																										"@SmsCount", smsSent.SmsCount,
		//																										"@Encoding", smsSent.Encoding,
		//																										"@TypeSend", smsSent.TypeSend,
		//																										"@State", smsSent.State,
		//																										"@CreateDate", smsSent.CreateDate,
		//																										"@GroupGuid", smsSent.GroupGuid,
		//																										"@UserGuid", smsSent.UserGuid);

		//}

		//public bool InsertSendAdvancedGroupSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return base.ExecuteSPCommand("InsertSendAdvancedGroupSms", "@Guid", smsSent.SmsSentGuid,
		//																														"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																														"@PrivateNumberGuid", privateNumberGuid,
		//																														"@SmsSenderAgentReference", (int)agent,
		//																														"@PresentType", smsSent.PresentType,
		//																														"@SmsBody", smsSent.SmsBody,
		//																														"@SmsCount", smsSent.SmsCount,
		//																														"@DownRange", smsSent.DownRange,
		//																														"@UpRange", smsSent.UpRange,
		//																														"@Encoding", smsSent.Encoding,
		//																														"@TypeSend", smsSent.TypeSend,
		//																														"@State", smsSent.State,
		//																														"@CreateDate", smsSent.CreateDate,
		//																														"@GroupGuid", smsSent.GroupGuid,
		//																														"@UserGuid", smsSent.UserGuid);
		//}

		//public bool InsertSendAdvancedSpecialGroupSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return base.ExecuteSPCommand("InsertSendAdvancedSpecialGroupSms", "@Guid", smsSent.SmsSentGuid,
		//																														"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																														"@PrivateNumberGuid", privateNumberGuid,
		//																														"@SmsSenderAgentReference", (int)agent,
		//																														"@PresentType", smsSent.PresentType,
		//																														"@SmsBody", smsSent.SmsBody,
		//																														"@SmsCount", smsSent.SmsCount,
		//																														"@DownRange", smsSent.DownRange,
		//																														"@UpRange", smsSent.UpRange,
		//																														"@Encoding", smsSent.Encoding,
		//																														"@TypeSend", smsSent.TypeSend,
		//																														"@State", smsSent.State,
		//																														"@CreateDate", smsSent.CreateDate,
		//																														"@GroupGuid", smsSent.GroupGuid,
		//																														"@UserGuid", smsSent.UserGuid);
		//}

		//public bool InsertSendFormatSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return base.ExecuteSPCommand("InsertSendFormatSms", "@Guid", smsSent.SmsSentGuid,
		//																											"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																											"@PrivateNumberGuid", privateNumberGuid,
		//																											"@SmsSenderAgentReference", (int)agent,
		//																											"@PresentType", smsSent.PresentType,
		//																											"@TypeSend", smsSent.TypeSend,
		//																											"@State", smsSent.State,
		//																											"@CreateDate", smsSent.CreateDate,
		//																											"@GroupGuid", smsSent.GroupGuid,
		//																											"@SmsFormatGuid", smsSent.SmsFormatGuid,
		//																											"@UserGuid", smsSent.UserGuid);
		//}

		//public bool InsertSendSmsForUsers(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return base.ExecuteSPCommand("InsertSendSmsForUsers", "@Guid", smsSent.SmsSentGuid,
		//																												"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																												"@PrivateNumberGuid", privateNumberGuid,
		//																												"@SmsSenderAgentReference", (int)agent,
		//																												"@SmsBody", smsSent.SmsBody,
		//																												"@TypeSend", smsSent.TypeSend,
		//																												"@Encoding", smsSent.Encoding,
		//																												"@PresentType", smsSent.PresentType,
		//																												"@SmsCount", smsSent.SmsCount,
		//																												"@CreateDate", smsSent.CreateDate,
		//																												"@State", smsSent.State,
		//																												"@UserGuid", smsSent.UserGuid,
		//																												"@GroupGuid", smsSent.GroupGuid,
		//																												"@DecreaseFromUser",smsSent.DecreaseFromUser);

		//}

		//public bool InsertSendSingleFutureSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return ExecuteSPCommand("InsertSendSingleFutureSms", "@Guid", smsSent.SmsSentGuid,
		//																											 "@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																											 "@PrivateNumberGuid", privateNumberGuid,
		//																											 "@SmsSenderAgentReference", (int)agent,
		//																											 "@Reciever", smsSent.Reciever,
		//																											 "@PresentType", smsSent.PresentType,
		//																											 "@SmsBody", smsSent.SmsBody,
		//																											 "@SmsCount", smsSent.SmsCount,
		//																											 "@Encoding", smsSent.Encoding,
		//																											 "@TypeSend", smsSent.TypeSend,
		//																											 "@State", smsSent.State,
		//																											 "@CreateDate", smsSent.CreateDate,
		//																											 "@DateTimeFuture", smsSent.DateTimeFuture,
		//																											 "@UserGuid", smsSent.UserGuid);
		//}

		//public Guid InsertSendGroupFutureSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	Guid guid = Guid.NewGuid();
		//	try
		//	{
		//		ExecuteSPCommand("InsertSendGroupFutureSms", "@Guid", guid,
		//																								"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																								"@PrivateNumberGuid", privateNumberGuid,
		//																								"@SmsSenderAgentReference", (int)agent,
		//																								"@PresentType", smsSent.PresentType,
		//																								"@SmsBody", smsSent.SmsBody,
		//																								"@SmsCount", smsSent.SmsCount,
		//																								"@Encoding", smsSent.Encoding,
		//																								"@TypeSend", smsSent.TypeSend,
		//																								"@State", smsSent.State,
		//																								"@CreateDate", smsSent.CreateDate,
		//																								"@DateTimeFuture", smsSent.DateTimeFuture,
		//																								"@GroupGuid", smsSent.GroupGuid,
		//																								"@UserGuid", smsSent.UserGuid);
		//		return guid;
		//	}
		//	catch
		//	{
		//		guid = Guid.Empty;
		//		return guid;
		//	}
		//}

		//public Guid InsertSendAdvancedGroupFutureSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	Guid guid = Guid.NewGuid();
		//	try
		//	{
		//		ExecuteSPCommand("InsertSendAdvancedGroupFutureSms", "@Guid", guid,
		//																												 "@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																												 "@PrivateNumberGuid", privateNumberGuid,
		//																												 "@SmsSenderAgentReference", (int)agent,
		//																												 "@PresentType", smsSent.PresentType,
		//																												 "@SmsBody", smsSent.SmsBody,
		//																												 "@SmsCount", smsSent.SmsCount,
		//																												 "@DownRange", smsSent.DownRange,
		//																												 "@UpRange", smsSent.UpRange,
		//																												 "@Encoding", smsSent.Encoding,
		//																												 "@TypeSend", smsSent.TypeSend,
		//																												 "@State", smsSent.State,
		//																												 "@DateTimeFuture", smsSent.DateTimeFuture,
		//																												 "@CreateDate", smsSent.CreateDate,
		//																												 "@GroupGuid", smsSent.GroupGuid,
		//																												 "@UserGuid", smsSent.UserGuid);
		//		return guid;
		//	}
		//	catch
		//	{
		//		guid = Guid.Empty;
		//		return guid;
		//	}
		//}

		//public Guid InsertSendBirthDateSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	Guid guid = Guid.NewGuid();
		//	try
		//	{
		//		ExecuteSPCommand("InsertSendBirthDateSms", "@Guid", guid,
		//																							"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																							"@PrivateNumberGuid", privateNumberGuid,
		//																							"@SmsSenderAgentReference", (int)agent,
		//																							"@PresentType", smsSent.PresentType,
		//																							"@SmsBody", smsSent.SmsBody,
		//																							"@SmsCount", smsSent.SmsCount,
		//																							"@Encoding", smsSent.Encoding,
		//																							"@TypeSend", smsSent.TypeSend,
		//																							"@State", smsSent.State,
		//																							"@CreateDate", smsSent.CreateDate,
		//																							"@DateTimeFuture", smsSent.DateTimeFuture,
		//																							"@GroupGuid", smsSent.GroupGuid,
		//																							"@UserGuid", smsSent.UserGuid);
		//		return guid;
		//	}
		//	catch
		//	{
		//		guid = Guid.Empty;
		//		return guid;
		//	}
		//}

		//public Guid InsertSendFutureSmsFormat(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	Guid guid = Guid.NewGuid();
		//	try
		//	{
		//		ExecuteSPCommand("InsertSendFutureSmsFormat", "@Guid", guid,
		//																									"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																									"@PrivateNumberGuid", privateNumberGuid,
		//																									"@SmsSenderAgentReference", (int)agent,
		//																									"@PresentType", smsSent.PresentType,
		//																									"@TypeSend", smsSent.TypeSend,
		//																									"@State", smsSent.State,
		//																									"@CreateDate", smsSent.CreateDate,
		//																									"@DateTimeFuture", smsSent.DateTimeFuture,
		//																									"@GroupGuid", smsSent.GroupGuid,
		//																									"@SmsFormatGuid", smsSent.SmsFormatGuid,
		//																									"@UserGuid", smsSent.UserGuid);
		//		return guid;
		//	}
		//	catch
		//	{
		//		guid = Guid.Empty;
		//		return guid;
		//	}
		//}

		//public bool InsertSendPeriodFutureSingleSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	Guid guid = Guid.NewGuid();
		//	return ExecuteSPCommand("InsertSendPeriodFutureSingleSms", "@Guid", guid,
		//																														"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																														"@PrivateNumberGuid", privateNumberGuid,
		//																														"@SmsSenderAgentReference", (int)agent,
		//																														"@Reciever", smsSent.Reciever,
		//																														"@PresentType", smsSent.PresentType,
		//																														"@SmsBody", smsSent.SmsBody,
		//																														"@SmsCount", smsSent.SmsCount,
		//																														"@Encoding", smsSent.Encoding,
		//																														"@TypeSend", smsSent.TypeSend,
		//																														"@State", smsSent.State,
		//																														"@CreateDate", smsSent.CreateDate,
		//																														"@StartDateTime", smsSent.StartDateTime,
		//																														"@EndDateTime", smsSent.EndDateTime,
		//																														"@Period", smsSent.Period,
		//																														"@PeriodType", smsSent.PeriodType,
		//																														"@UserGuid", smsSent.UserGuid);
		//}

		//public bool InsertSendPeriodFutureGroupSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	Guid guid = Guid.NewGuid();
		//	return ExecuteSPCommand("InsertSendPeriodFutureGroupSms", "@Guid", guid,
		//																															"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																															"@PrivateNumberGuid", privateNumberGuid,
		//																															"@SmsSenderAgentReference", (int)agent,
		//																															"@PresentType", smsSent.PresentType,
		//																															"@SmsBody", smsSent.SmsBody,
		//																															"@SmsCount", smsSent.SmsCount,
		//																															"@Encoding", smsSent.Encoding,
		//																															"@TypeSend", smsSent.TypeSend,
		//																															"@State", smsSent.State,
		//																															"@CreateDate", smsSent.CreateDate,
		//																															"@StartDateTime", smsSent.StartDateTime,
		//																															"@EndDateTime", smsSent.EndDateTime,
		//																															"@Period", smsSent.Period,
		//																															"@PeriodType", smsSent.PeriodType,
		//																															"@GroupGuid", smsSent.GroupGuid,
		//																															"@UserGuid", smsSent.UserGuid);
		//}

		//public bool InsertSendFutureSmsForUsers(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	Guid guid = Guid.NewGuid();
		//	return base.ExecuteSPCommand("InsertSendFutureSmsForUsers", "@Guid", guid,
		//																															"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																															"@PrivateNumberGuid", privateNumberGuid,
		//																															"@SmsSenderAgentReference", (int)agent,
		//																															"@SmsBody", smsSent.SmsBody,
		//																															"@TypeSend", smsSent.TypeSend,
		//																															"@Encoding", smsSent.Encoding,
		//																															"@PresentType", smsSent.PresentType,
		//																															"@SmsCount", smsSent.SmsCount,
		//																															"@DateTimeFuture", smsSent.DateTimeFuture,
		//																															"@CreateDate", smsSent.CreateDate,
		//																															"@State", smsSent.State,
		//																															"@UserGuid", smsSent.UserGuid);
		//}

		//public Guid InsertBulk(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent, string xmlRecipients)
		//{
		//	Guid guid = Guid.NewGuid();
		//	try
		//	{
		//		ExecuteSPCommand("InsertBulk", "@Guid", guid,
		//																		"@BulkID", smsSent.BulkID,
		//																		"@SmsBody", smsSent.SmsBody,
		//																		"@PresentType", smsSent.PresentType,
		//																		"@Status", smsSent.State,
		//																		"@SmsCount", smsSent.SmsCount,
		//																		"@Encoding", smsSent.Encoding,
		//																		"@RecipientsNumberCount", smsSent.RecipientsNumberCount,
		//																		"@SendDateTime", smsSent.DateTimeFuture,
		//																		"@TypeSend", smsSent.TypeSend,
		//																		"@CreateDate", smsSent.CreateDate,
		//																		"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																		"@PrivateNumberGuid", privateNumberGuid,
		//																		"@SmsSenderAgentReference", (int)agent,
		//																		"@UserGuid", smsSent.UserGuid,
		//																		"@XmlRecipients", xmlRecipients);
		//		return guid;
		//	}
		//	catch
		//	{
		//		guid = Guid.Empty;
		//	}
		//	return guid;
		//}
		#endregion

		#region UpdateMethod
		//public bool UpdateSendSingleFutureSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return base.ExecuteSPCommand("UpdateSendSingleFutureSms", "@Guid", smsSent.SmsSentGuid,
		//																														"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																														"@PrivateNumberGuid", privateNumberGuid,
		//																														"@SmsSenderAgentReference", (int)agent,
		//																														"@Reciever", smsSent.Reciever,
		//																														"@PresentType", smsSent.PresentType,
		//																														"@SmsBody", smsSent.SmsBody,
		//																														"@SmsCount", smsSent.SmsCount,
		//																														"@Encoding", smsSent.Encoding,
		//																														"@DateTimeFuture", smsSent.DateTimeFuture);
		//}

		//public bool UpdateSendGroupFutureSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return base.ExecuteSPCommand("UpdateSendGroupFutureSms", "@Guid", smsSent.SmsSentGuid,
		//																													"@GroupGuid", smsSent.GroupGuid,
		//																													"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																													"@PrivateNumberGuid", privateNumberGuid,
		//																													"@SmsSenderAgentReference", (int)agent,
		//																													"@PresentType", smsSent.PresentType,
		//																													"@SmsBody", smsSent.SmsBody,
		//																													"@SmsCount", smsSent.SmsCount,
		//																													"@Encoding", smsSent.Encoding,
		//																													"@DateTimeFuture", smsSent.DateTimeFuture);
		//}

		//public bool UpdateSendAdvancedGroupFutureSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return base.ExecuteSPCommand("UpdateSendAdvancedGroupFutureSms", "@Guid", smsSent.SmsSentGuid,
		//																																	"@GroupGuid", smsSent.GroupGuid,
		//																																	"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																																	"@PrivateNumberGuid", privateNumberGuid,
		//																																	"@SmsSenderAgentReference", (int)agent,
		//																																	"@PresentType", smsSent.PresentType,
		//																																	"@SmsBody", smsSent.SmsBody,
		//																																	"@SmsCount", smsSent.SmsCount,
		//																																	"@DownRange", smsSent.DownRange,
		//																																	"@UpRange", smsSent.UpRange,
		//																																	"@Encoding", smsSent.Encoding,
		//																																	"@DateTimeFuture", smsSent.DateTimeFuture);
		//}

		//public bool UpdateSendBirthDateSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return base.ExecuteSPCommand("UpdateSendBirthDateSms", "@Guid", smsSent.SmsSentGuid,
		//																												"@GroupGuid", smsSent.GroupGuid,
		//																												"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																												"@PrivateNumberGuid", privateNumberGuid,
		//																												"@SmsSenderAgentReference", (int)agent,
		//																												"@PresentType", smsSent.PresentType,
		//																												"@SmsBody", smsSent.SmsBody,
		//																												"@SmsCount", smsSent.SmsCount,
		//																												"@Encoding", smsSent.Encoding,
		//																												"@DateTimeFuture", smsSent.DateTimeFuture);
		//}

		//public bool UpdateSendFutureSmsFormat(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return base.ExecuteSPCommand("UpdateSendFutureSmsFormat", "@Guid", smsSent.SmsSentGuid,
		//																														"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																														"@PrivateNumberGuid", privateNumberGuid,
		//																														"@SmsSenderAgentReference", (int)agent,
		//																														"@PresentType", smsSent.PresentType,
		//																														"@GroupGuid", smsSent.GroupGuid,
		//																														"@SmsFormatGuid", smsSent.SmsFormatGuid,
		//																														"@DateTimeFuture", smsSent.DateTimeFuture);
		//}

		//public bool UpdateSendPeriodFutureSingleSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return base.ExecuteSPCommand("UpdateSendPeriodFutureSingleSms", "@Guid", smsSent.SmsSentGuid,
		//																																	"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																																	"@PrivateNumberGuid", privateNumberGuid,
		//																																	"@SmsSenderAgentReference", (int)agent,
		//																																	"@Reciever", smsSent.Reciever,
		//																																	"@PresentType", smsSent.PresentType,
		//																																	"@SmsBody", smsSent.SmsBody,
		//																																	"@SmsCount", smsSent.SmsCount,
		//																																	"@Encoding", smsSent.Encoding,
		//																																	"@StartDateTime", smsSent.StartDateTime,
		//																																	"@EndDateTime", smsSent.EndDateTime,
		//																																	"@Period", smsSent.Period,
		//																																	"@PeriodType", smsSent.PeriodType);
		//}

		//public bool UpdateSendPeriodFutureGroupSms(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return base.ExecuteSPCommand("UpdateSendPeriodFutureGroupSms", "@Guid", smsSent.SmsSentGuid,
		//																																	"@GroupGuid", smsSent.GroupGuid,
		//																																	"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																																	"@PrivateNumberGuid", privateNumberGuid,
		//																																	"@SmsSenderAgentReference", (int)agent,
		//																																	"@PresentType", smsSent.PresentType,
		//																																	"@SmsBody", smsSent.SmsBody,
		//																																	"@SmsCount", smsSent.SmsCount,
		//																																	"@Encoding", smsSent.Encoding,
		//																																	"@StartDateTime", smsSent.StartDateTime,
		//																																	"@EndDateTime", smsSent.EndDateTime,
		//																																	"@Period", smsSent.Period,
		//																																	"@PeriodType", smsSent.PeriodType);
		//}

		//public bool UpdateSendFutureSmsForUsers(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return ExecuteSPCommand("UpdateSendFutureSmsForUsers", "@Guid", smsSent.SmsSentGuid,
		//																												 "@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																												 "@PrivateNumberGuid", privateNumberGuid,
		//																												 "@SmsSenderAgentReference", (int)agent,
		//																												 "@PresentType", smsSent.PresentType,
		//																												 "@SmsBody", smsSent.SmsBody,
		//																												 "@SmsCount", smsSent.SmsCount,
		//																												 "@Encoding", smsSent.Encoding,
		//																												 "@DateTimeFuture", smsSent.DateTimeFuture);
		//}

		//public bool UpdateSendSmsForUsers(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent)
		//{
		//	return ExecuteSPCommand("UpdateSendSmsForUsers", "@Guid", smsSent.SmsSentGuid,
		//																										"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																										"@PrivateNumberGuid", privateNumberGuid,
		//																										"@PresentType", smsSent.PresentType,
		//																										"@SmsBody", smsSent.SmsBody,
		//																										"@GroupGuid", smsSent.GroupGuid,
		//																										"@DecreaseFromUser",smsSent.DecreaseFromUser,
		//																										"@TypeSend", smsSent.TypeSend);
		//}
		//public bool UpdateBulk(Common.SmsSent smsSent, Guid privateNumberGuid, SmsSenderAgentReference agent, string xmlRecipients)
		//{
		//	return ExecuteSPCommand("UpdateBulk", "@Guid", smsSent.SmsSentGuid,
		//																				"@SmsBody", smsSent.SmsBody,
		//																				"@PresentType", smsSent.PresentType,
		//																				"@SmsCount", smsSent.SmsCount,
		//																				"@Encoding", smsSent.Encoding,
		//																				"@RecipientsNumberCount", smsSent.RecipientsNumberCount,
		//																				"@DateTimeFuture", smsSent.DateTimeFuture,
		//																				"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																				"@PrivateNumberGuid", privateNumberGuid,
		//																				"@SmsSenderAgentReference", (int)agent,
		//																				"@XmlRecipients", xmlRecipients);

		//}

		public void UpdateState(string xmlSmsSentInfo)
		{
			ExecuteSPCommand("UpdateSmsSentState", "@XmlInfo", xmlSmsSentInfo);
		}

		public bool RejectSms(Guid smsSentGuid, SmsSentStates smsSentState, Business.SmsSendFailedType failedType, string errorMessage)
		{
			return base.ExecuteSPCommand("RejectSms", "@Guid", smsSentGuid,
																								"@State", (int)smsSentState,
																								"@FailedType", (int)failedType,
																								"@ErrorMessage", errorMessage);
		}
		#endregion

		//public bool InsertSendSmsDateField(Common.SmsSent smsSent)
		//{
		//	Guid guid = Guid.NewGuid();
		//	try
		//	{
		//		base.ExecuteSPCommand("InsertSendDateFieldSms", "@Guid", guid,
		//																								"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																								"@UserDateFieldIndex", smsSent.UserDateFieldIndex,
		//																								"@SmsBody", smsSent.SmsBody,
		//																								"@TypeSend", smsSent.TypeSend,
		//																								"@State", smsSent.State,
		//																								"@CreateDate", smsSent.CreateDate,
		//																								"@GroupGuid", smsSent.GroupGuid,
		//																								"@UserGuid", smsSent.UserGuid,
		//																								"@Period", smsSent.Period);
		//		return true;
		//	}
		//	catch
		//	{
		//		guid = Guid.Empty;
		//		return false;
		//	}
		//}

		//public bool UpdateSendSmsDateField(Common.SmsSent smsSent)
		//{
		//	return base.ExecuteSPCommand("UpdateSendDateFieldSms", "@Guid", smsSent.SmsSentGuid,
		//																												"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																												"@UserDateFieldIndex", smsSent.UserDateFieldIndex,
		//																												"@SmsBody", smsSent.SmsBody,
		//																												"@Period", smsSent.Period);
		//}

		public DataTable GetSmsesByPriority(SmsSenderAgentReference smsSenderAgentRefrence)
		{
			return FetchSPDataTable("GetSmsByPriority", "@SmsSenderAgentRefrence", (int)smsSenderAgentRefrence);
		}

		public DataTable GetPagedSmses(Common.SmsSent smsSent, Guid smsSenderAgentGuid, string userName, string senderNumber, string fromCreateDate, string fromTime, string toCreateDate, string toTime, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			DataSet dataSetOutBox = base.FetchSPDataSet("GetPagedSmses", "@UserGuid", smsSent.UserGuid,
																																		"@UserName", userName,
																																		"@SmsSenderAgentGuid", smsSenderAgentGuid,
																																		"@SenderNumber", senderNumber,
				//"@RecieverNumber", smsSent.Reciever,
																																		"@FromCreateDate", fromCreateDate,
																																		"@FromTime", fromTime,
																																		"@ToCreateDate", toCreateDate,
																																		"@ToTime", toTime,
				//"@SmsBody", smsSent.SmsBody,
				//"@State", smsSent.State,
				//"@TypeSend", smsSent.TypeSend,
																																		"@PageNo", pageNo,
																																		"@PageSize", pageSize,
																																		"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetOutBox.Tables[0].Rows[0]["RowCount"]);

			return dataSetOutBox.Tables[1];
		}

		public DataTable GetChartDetailsAtSpecificDate(Guid userGuid, DateTime fromDateTime, DateTime toDateTime, Business.SmsSentStates state, int pageNo, int pageSize, ref  int rowCount)
		{
			DataSet dataSetChartDetails = base.FetchSPDataSet("GetChartDetailsAtSpecificDate", "@UserGuid", userGuid,
																																		"@FromDateTime", fromDateTime,
																																		"@ToDateTime", toDateTime,
																																		"@State", (int)state,
																																		"@PageNo", pageNo,
																																		"@PageSize", pageSize);

			rowCount = Helper.GetInt(dataSetChartDetails.Tables[0].Rows[0]["RowCount"]);

			return dataSetChartDetails.Tables[1];
		}

		public void UpdateBulkState(Guid guid, SmsSentStates state, SmsSendFailedType faildType)
		{
			ExecuteSPCommand("UpdateBulkStatus", "@Guid", guid,
																					"@State", (int)state,
																					"@SendFaildType", (int)faildType);
		}

		public void UpdateBulkID(Guid bulkGuid, string bulkID)
		{
			ExecuteSPCommand("UpdateBulkID", "@BulkGuid", bulkGuid, "@BulkID", bulkID);
		}

		public DataTable GetUncertainDeleviryStatusBulk(SmsSenderAgentReference smsSenderAgentRefrence)
		{
			return FetchSPDataTable("GetUncertainDeleviryStatusBulk", "@SmsSenderAgentRefrence", smsSenderAgentRefrence);
		}

		public DataTable GetPagedUserBulks(Guid userGuid, string sortField, int pageNo, int pageSize, ref int rowCount)
		{
			DataSet dataSetBulkInfo = base.FetchSPDataSet("GetPagedUserBulks", "@UserGuid", userGuid,
																																 "@PageNo", pageNo,
																																 "@PageSize", pageSize,
																																 "@SortField", sortField);
			rowCount = Helper.GetInt(dataSetBulkInfo.Tables[1].Rows[0]["RowCount"]);

			return dataSetBulkInfo.Tables[0];

		}

		//public Guid InsertReplySmsForSmsParser(Common.SmsSent smsSent)
		//{
		//	Guid guid = Guid.NewGuid();
		//	try
		//	{
		//		base.ExecuteSPCommand("InsertReplySmsForSmsParser", "@Guid", guid,
		//																												"@UserPrivateNumberGuid", smsSent.PrivateNumberGuid,
		//																												"@Reciever", smsSent.Reciever,
		//																												"@PresentType", smsSent.PresentType,
		//																												"@SmsBody", smsSent.SmsBody,
		//																												"@SmsCount", smsSent.SmsCount,
		//																												"@Encoding", smsSent.Encoding,
		//																												"@TypeSend", smsSent.TypeSend,
		//																												"@State", smsSent.State,
		//																												"@CreateDate", smsSent.CreateDate,
		//																												"@UserGuid", smsSent.UserGuid,
		//																												"@ParserFormulaGuid", smsSent.ParserFormulaGuid);
		//		return guid;
		//	}
		//	catch
		//	{
		//		guid = Guid.Empty;
		//		return guid;
		//	}
		//}
	}
}
