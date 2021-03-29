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

namespace Arad.SMS.Gateway.Common
{
	public enum TableNames
	{
		Users,
		PhoneBooks,
		Outboxes,
		OutboxNumbers,
		ScheduledSmses,
		Transactions,
		Settings,
		PrivateNumbers,
		AccountInformations,
		Fishes,
		Domains,
		Services,
		ServiceGroups,
		Accesses,
		GroupPrices,
		PhoneNumbers,
		UserFields,
		SmsFormats,
		SmsSenderAgents,
		Inboxes,
		UserSettings,
		SmsTemplates,
		GalleryImages,
		Images,
		DataCenters,
		Datas,
		LoginStats,
		DomainSettings,
		DomainMenus,
		DesktopMenuLocations,
		SmsParsers,
		ParserFormulas,
		FailedOnlinePayments,
		InboxGroups,
		SmsFormatPhoneBooks,
		Roles,
		RoleServices,
		UserDocuments,
		TrafficRelays,
		Zones,
		PersonsInfo,
		Routes,
		Operators,
		AgentRatio,
		ScheduledBulkSmses,
		RegularContents,
		Contents,
		PhoneBookRegularContents,
		FilterWords,
	}

	public enum SendStatus
	{
		Stored = 1,//ذخیره شده
		WatingForSend = 2,// منتظر برای ارسال
		IsBeingSent = 3,// در حال ارسال شدن
		Sent = 4,//به اپراتور ارسال شد
		SentAndGiveBackCredit = 5,//ارسال شده و چک های مربوط به بازگشت هزینه انجام شده است
		Archived = 6,//آرشیو
		ErrorInSending = 7,
		ErrorInGetItc = 8,
		BlackList = 9,
		WatingForConfirm = 10,//منتظر تایید ارسال
		Archiving = 11,//در حال آرشیو سازی
	}

	public enum DeliveryStatus
	{
		SentAndReceivedbyPhone = 1,//پیامک با موفقیت ارسال و توسط گوشی دریافت شد
		HaveNotReceivedToPhone = 2,//به گوشی نرسیده است
		SentToItc = 3,//پیامک به مخابرات ارسال شده
		ReceivedByItc = 4,//به مخابرات رسیده
		DidNotReceiveToItc = 5,//به مخابرات نرسیده
		NotReceivebyServer = 6,//پیامک توسط سرور دریافت نشد
		ErrorInSending = 7,// در ارسال پیامک خطا رخ داده است
		WaitingForSend = 8,//منتظر ارسال
		Sent = 9,//ارسال شده
		NotSent = 10, //ارسال نشده
		Expired = 11,//منقضی
		IsSending = 12, // در حال ارسال
		IsCanceled = 13, //کنسل شده
		BlackList = 14, //لیست سیاه
		SmsIsFilter = 15,//متن پیامک فیلتر میباشد
		IsDeleted = 16,//حذف شده
		WatingForConfirmation = 17,//منتظر برای تایید
		NotEnoughBalance = 18,//کمبود اعتبار
		IsPreparing = 19,//درحال آماده سازی
		IsPreparedForSending = 20,//آماده شده برای ارسال
		AccessDenied = 21,//عدم دسترسی
		TextIsEmpty = 22,//متن پیام خالی است
		InvalidInputXml = 23,//ایکس ام ال ورودی نامعتبر است
		InvalidUserOrPassword = 24,//کاربر یا رمز عبور اشتباه است
		InvalidUsedMethod = 25,//متد استفاده شده نامعتبر است
		InvalidSender = 26,//فرستنده معتبر نیست
		InvalidMobile = 27,//موبایل معتبر نیست
		InvalidReciption = 28,//هیچ گیرنده ای مشخص نشده است
		Stored = 29,// ذخیره شد
		BlackListTable = 30,//شماره در جدول بلک لیست وجود دارد
		GetDeliveryStatus = 31,//تقاضا جهت دریافت آخرین وضعیت
	}

	public enum SmsSendType
	{
		SendSms = 1,
		SendGroupSms = 2,
		SendFormatSms = 3,
		SendPeriodSms = 5,
		SendGradualSms = 6,
		SendBulkSms = 7,
		SendSmsFromAPI = 8,
		SendRegularContentSms = 9,
		SendP2PSms = 10,
		SendGroupSmsFromAPI = 11,
	}

	public enum ServiceLogs
	{
		SendSms,
		ReceiveSms,
		Magfa,
		RahyabRG,
		RahyabPG,
		Arad,
		Armaghan,
		SLS,
		GarbageCollector,
		DeliverySms,
		WinServiceHandler,
		MCI,
		MCIVAS,
		MTN,
		ScheduledSms,
		ScheduledBulkSms,
		ExportData,
		SaveSentMessage,
		GiveBackCredit,
		SmsParser,
		Shreeweb,
		WebAPI,
		TrafficRelay,
		APIProcessRequest,
		SaveDelivery,
		Asanak,
		SocialNetwork,
		RegularContent,
		FFF,
		Log,
		ConfirmBulk,
		GsmGateway,
		SaveGsmDelivery,
        Avanak,
        AradSmppServer,
        AradSmppServerClients,
        Mobbis,
        IconGlobal
    }

	public enum SmsSenderAgentReference
	{
		Magfa = 1,//3000
		Asanak = 2,//021
		Armaghan = 3,//50004
		AradBulk = 4,//6500
		RahyabRG = 5,//1000
		RahyabPG = 6,//50001
		SLS = 7,//50002
		Shreeweb = 8,//International
		AradVas = 9,//6100
		SocialNetworks = 10,//viber,telegram,whats app...
		FFF = 11,//Farda Faraz Fava Sepah Bank
		GSMGateway = 12,//Gsm
        Avanak = 13, // Voice
        Mobbis = 14,  // Mobbis International
        IconGlobal = 15 // Icon Global
    }

	public enum SenderAgentType
	{
		SMS = 1,
		MMS = 2,
		Email = 3,
		Voice = 4,
	}

	public enum ScheduledSmsStatus
	{
		Stored = 1,
		Extracting = 2,
		FailedExtract = 3,
		Ready = 4,
		Failed = 5,
		Completed = 6,
		Extracted = 7,
		WatingForConfirm = 8,//منتظر برای تایید
		Confirmed = 9,//تایید شده
		Rejected = 10,//تایید نشده
	}

	public enum ExportDataStatus
	{
		None = 1,
		Get = 2,
		Complete = 3,
		Archived = 4,
	}
}
