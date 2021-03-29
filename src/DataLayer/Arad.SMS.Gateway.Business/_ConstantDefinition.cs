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

namespace Arad.SMS.Gateway.Business
{
	//This ENUM use in DB dont change it!
	public enum UserControls
	{
		UI_Home = 10000,
		UI_Services_Service = 10001,
		UI_Users_User = 10002,
		//UI_Users_Register = 10003,
		UI_Users_ChangePassword = 10004,
		UI_Users_EditProfile = 10005,
		//UI_UserServices_UserService = 10006,
		UI_PhoneBooks_PhoneBook = 10007,
		UI_PhoneBooks_PhoneNumber = 10008,
		UI_Accesses_Access = 10009,
		UI_UserFields_UserField = 10010,
		UI_UserFields_PhoneBookField = 10011,
		UI_UserFields_SmsFormat = 10012,
		UI_GroupPrices_GroupPrice = 10013,
		//UI_PriceRanges_PriceRange = 10014,
		UI_ServiceGroups_ServiceGroup = 10015,
		//UI_SmsSends_SendSingleSms = 10016,
		UI_PrivateNumbers_DefiningPrivateNumber = 10018,
		UI_PrivateNumbers_UserPrivateNumber = 10019,
		UI_Users_Transaction = 10020,
		UI_Login_Login = 10021,
		UI_PrivateNumbers_AssignPrivateNumberToUsers = 10022,
		UI_AccountInformations_AccountInformation = 10023,
		UI_Users_RegisterFish = 10024,
		UI_UserFishes_ConfirmFish = 10025,
		//UI_SmsSends_SendGroupSms = 10026,
		//UI_SmsSends_SendBulkSms = 10027,
		//UI_SmsReports_BulkList = 10028,
		//UI_SmsReports_BulkDetails = 10029,
		UI_Domains_Domain = 10030,
		UI_Domains_RegisterDomain = 10031,
		//UI_Users_IncreaseCredit = 10032,
		//UI_Users_DecreaseCredit = 10033,
		//UI_UserServices_UserServicePrice = 10034,
		UI_ServiceGroups_SaveServiceGroup = 10035,
		UI_Services_SaveService = 10037,
		UI_Accesses_SaveAccess = 10038,
		UI_GroupPrices_SaveGroupPrice = 10039,
		UI_Users_DetermineGroupPrice = 10040,
		//UI_PriceRanges_SavePriceRange = 10041,
		UI_Users_AdvanceEdit = 10042,
		UI_PhoneBooks_SaveSingleNumber = 10043,
		UI_UserAccesses_UserAccess = 10044,
		UI_PhoneBooks_SaveListNumber = 10045,
		UI_PhoneBooks_SaveFileNumber = 10046,
		UI_SmsSenderAgents_SmsSenderAgent = 10047,
		UI_SmsSenderAgents_SaveSmsSenderAgent = 10048,
		UI_PrivateNumbers_SavePrivateNumber = 10049,
		UI_PrivateNumbers_NumberStatus = 10050,
		UI_PrivateNumbers_SaveUserPrivateNumber = 10051,
		UI_SmsReports_SentBox = 10052,
		UI_SmsReports_Inbox = 10053,
		//UI_UserServices_DefineGlobalServicePrice = 10054,
		UI_UserServices_BuyService = 10055,
		UI_AccountInformations_SaveAccount = 10056,
		//UI_UserFishes_FishDetails = 10057,
		UI_SmsReports_OutBox = 10058,
		UI_Users_UserShortcuts = 10059,
		//UI_SmsSends_SendAdvancedGroupSms = 10060,
		//UI_SmsSends_SendFormatSms = 10061,
		//UI_SmsSends_SendSingleFutureSms = 10062,
		//UI_SmsSends_SendGroupFutureSms = 10063,
		//UI_SmsSends_SendFutureSmsFormat = 10064,
		//UI_SmsSends_SendPeriodFutureSingleSms = 10065,
		//UI_SmsSends_SendPeriodFutureGroupSms = 10066,
		//UI_SmsSends_SendBirthDateSms = 10067,
		//UI_GroupTemplates_GroupTemplate = 10068,
		//UI_GroupTemplates_SaveGroupTemplate = 10069,
		UI_SmsTemplates_SmsTemplate = 10070,
		UI_SmsTemplates_SaveSmsTemplate = 10071,
		UI_GalleryImages_GalleryImage = 10072,
		UI_GalleryImages_SaveGalleryImage = 10073,
		UI_Images_Image = 10074,
		UI_Images_SaveImage = 10075,
		UI_DataCenters_DataCenter = 10076,
		UI_DataCenters_SaveDataCenter = 10077,
		UI_Data_Contents = 10078,
		UI_Data_SaveContent = 10079,
		UI_UserFishes_UserFish = 10080,
		//UI_SmsReports_ShowChartDetails = 10081,
		UI_SmsTemplates_LoadSmsTemplate = 10082,
		//UI_GeneralPhoneBooks_GeneralPhoneBook = 10083,
		//UI_GeneralPhoneBooks_GeneralNumber = 10084,
		//UI_GeneralPhoneBooks_SaveSingleGeneralNumber = 10085,
		//UI_GeneralPhoneBooks_SaveListGeneralNumber = 10086,
		//UI_GeneralPhoneBooks_SaveFileGeneralNumber = 10087,
		//UI_SmsSends_SendCitySms = 10088,
		//UI_SmsSends_SendPrefixSms = 10089,
		UI_Users_LoginStat = 10090,
		UI_Users_UserSetting = 10091,
		//UI_GeneralPhoneBooks_DefineGeneralGroupPrice = 10092,
		//UI_SmsSends_SendPostalCodeSms = 10093,
		//UI_SmsSends_SendAdvancedSearchGroupSms = 10094,
		UI_Domains_DomainSetting = 10095,
		//UI_Data_ShowNews = 10096,
		//UI_DomainMenu_SaveDomainMenu = 10097,
		//UI_DomainMenu_DomainMenu = 10098,
		UI_DataCenters_SaveDataLocation = 10099,
		//UI_DesktopMenuLocations_DesktopMenuLocation = 10100,
		//UI_EmailBooks_EmailBook = 10101,
		//UI_EmailBooks_EmailAddress = 10102,
		//UI_EmailBooks_SaveEmail = 10103,
		//UI_EmailSends_SendSingleEmail = 10104,
		UI_Users_RegisterLight = 10105,
		//UI_EmailSettings_UserEmailSetting = 10106,
		//UI_EmailSettings_SaveSetting = 10107,
		//UI_EmailSends_SendGroupEmail = 10108,
		UI_SmsParsers_Filters_SmsFilter = 10109,
		//UI_SmsSends_SendAdvancedGroupFutureSms = 10110,
		//UI_SmsReports_ShowBlackListNumber = 10111,
		UI_SmsParsers_Competitions_Competition = 10112,
		UI_SmsParsers_Polls_Poll = 10113,
		//UI_EmailTemplates_EmailTemplate = 10114,
		//UI_EmailReports_EmailSentbox = 10115,
		//UI_EmailReports_EmailOutbox = 10116,
		UI_Users_ShowUserSmsRates = 10117,
		//UI_SmsSends_SendSmsForUsers = 10118,
		//UI_SmsSends_SendFutureSmsForUsers = 10119,
		//UI_Domains_CreateNicAccount = 10120,
		//UI_Domains_CreateDirectAccount = 10121,
		//UI_Domains_OnlineRegisterDomain = 10122,
		//UI_Domains_TransportNicDomain = 10123,
		UI_Roles_Role = 10124,
		UI_Roles_SaveRole = 10125,
		UI_RoleServices_RoleService = 10126,
		UI_PhoneBooks_SearchAllGroups = 10127,
		UI_PhoneBooks_SearchAllFormats = 10128,
		UI_PhoneBooks_SearchAllUserField = 10129,
		UI_SmsReports_InboxGroup = 10130,
		//UI_Users_DetermineUserRole = 10131,
		//UI_SmsSends_SendSmsFromDateField = 10132,
		//UI_Domains_DomainContinuation = 10133,
		//UI_DomainPrices_DomainGroupPrice = 10134,
		//UI_DomainPrices_DomainPrice = 10135,
		//UI_DomainPrices_SaveDomainGroupPrice = 10136,
		//UI_DomainPrices_SaveDomainPrice = 10137,
		//UI_Domains_UserMessages = 10138,
		//UI_Domains_SaveUserMessage = 10139,
		HomePages_AdminPanel_Arad_Dashboard = 10140,
		UI_SmsSends_SendSms = 10141,
		UI_SmsSends_SendSmsFromFormat = 10142,
		UI_SmsSends_SendBulk = 10143,
		UI_Users_DefineUser = 10144,
		UI_Users_ViewUserTransaction = 10145,
		UI_SmsReports_ScheduledSms = 10146,
		UI_SmsParsers_Polls_SavePoll = 10147,
		UI_SmsParsers_Competitions_SaveCompetition = 10148,
		UI_SmsParsers_Filters_SaveFilter = 10149,
		UI_TrafficRelays_TrafficRelay = 10150,
		UI_TrafficRelays_SaveTrafficRelay = 10151,
		//UI_Logs_Log = 10152,
		UI_PrivateNumbers_SetTrafficRelay = 10153,
		UI_SmsSenderAgents_MessageRoute = 10154,
		UI_SmsSenderAgents_SaveRoute = 10155,
		UI_SmsSenderAgents_SmsSenderAgentRatio = 10156,
		UI_404 = 10157,
		UI_500 = 10158,
		UI_SmsReports_UserScheduledSms = 10159,
		UI_SmsReports_UserOutbox = 10160,
		UI_SmsSends_SendDetails = 10161,
		UI_Settings_Setting = 10162,
		UI_Settings_SaveSetting = 10163,
		UI_Users_ConfirmDocument = 10164,
		UI_PhoneBooks_VasSetting = 10165,
		UI_SmsReports_UpdateDeliveryStatus = 10166,
		UI_BlackList_BlackListNumber = 10167,
		UI_BlackList_SaveNumber = 10168,
		UI_RegularContents_RegularContent = 10169,
		UI_RegularContents_SaveRegularContent = 10170,
		UI_RegularContents_Content = 10171,
		UI_RegularContents_SaveContent = 10172,
		UI_PhoneBooks_RegularContent = 10173,
		UI_PhoneBooks_SendSms = 10174,
		UI_PhoneBooks_UpdateGroup = 10175,
		UI_PhoneBooks_SaveEmail = 10176,
		UI_PrivateNumbers_UserPrivateNumberKeywords = 10177,
		UI_SmsSends_SendP2PSms = 10178,
		UI_SmsParsers_SelectedOption = 10179,
		UI_SmsParsers_ParserChart = 10180,
		UI_Booking_SaveBooking = 10181,
		UI_PrivateNumbers_NumbersInfo = 10182,
		UI_SmsReports_UserManualOutbox = 10183,
		UI_SmsSends_Maps = 10184,
		UI_SmsReports_UserInbox = 10185,
		UI_Settings_AdminSetting = 10186,
		UI_PrivateNumbers_ExtendedNumber = 10187,
		UI_Settings_FilterWord = 10188,
		UI_Users_UsersTransaction = 10189,
	}

	//public enum MainHelpUserControls
	//{
	//	Help_Home = 10000,
	//	Help_User = 10002,
	//	Help_ChangePassword = 10004,
	//	Help_EditProfile = 10005,
	//	Help_PhoneBook = 10007,
	//	Help_UserField = 10010,
	//	Help_SmsFormat = 10012,
	//	Help_GroupPrice = 10013,
	//	Help_PriceRange = 10014,
	//	Help_SendSingleSms = 10016,
	//	Help_UserPrivateNumber = 10019,
	//	Help_Transaction = 10020,
	//	Help_AccountInformation = 10023,
	//	Help_RegisterFish = 10024,
	//	Help_ConfirmFish = 10025,
	//	Help_SendGroupSms = 10026,
	//	Help_BulkList = 10028,
	//	Help_UserServicePrice = 10034,
	//	Help_SentBox = 10052,
	//	Help_Inbox = 10053,
	//	Help_DefineGlobalServicePrice = 10054,
	//	Help_FishDetails = 10057,
	//	Help_OutBox = 10058,
	//	Help_UserShortcut = 10059,
	//	Help_SendAdvancedGroupSms = 10060,
	//	Help_SendFormatSms = 10061,
	//	Help_SendFutureSms = 10062,
	//	Help_SendBirthDateSms = 10067,
	//	Help_GroupTemplate = 10068,
	//	Help_SmsTemplate = 10070,
	//	Help_GalleryImage = 10072,
	//	Help_Image = 10074,
	//	Help_DataCenter = 10076,
	//	Help_News = 10078,
	//	Help_UserFish = 10080,
	//	Help_SendCitySms = 10088,
	//	Help_SendPrefixSms = 10089,
	//	Help_LoginStat = 10090,
	//	Help_SendPostalCodeSms = 10091,
	//}

	//public enum HelpUserControls
	//{
	//	UI_Help_Home = 10000,
	//	//UI_Services_Service = 10001,
	//	UI_Help_User = 10002,
	//	//UI_Users_Register = 10003,
	//	UI_Help_ChangePassword = 10004,
	//	UI_Help_EditProfile = 10005,
	//	//UI_UserServices_UserService = 10006,
	//	UI_Help_PhoneBook = 10007,
	//	//UI_PhoneBooks_PhoneNumber = 10008,
	//	//UI_Accesses_Access = 10009,
	//	UI_Help_UserField = 10010,
	//	//UI_UserFields_PhoneBookField = 10011,
	//	UI_Help_SmsFormat = 10012,
	//	UI_Help_GroupPrice = 10013,
	//	UI_Help_PriceRange = 10014,
	//	//UI_ServiceGroups_ServiceGroup = 10015,
	//	UI_Help_SendSingleSms = 10016,
	//	//UI_PrivateNumbers_DefiningPrivateNumber = 10018,
	//	UI_Help_UserPrivateNumber = 10019,
	//	UI_Help_Transaction = 10020,
	//	//UI_Login_Login = 10021,
	//	//UI_PrivateNumbers_AssignPrivateNumberToUsers = 10022,
	//	UI_Help_AccountInformation = 10023,
	//	UI_Help_RegisterFish = 10024,
	//	UI_Help_ConfirmFish = 10025,
	//	UI_Help_SendGroupSms = 10026,
	//	//UI_SmsSends_SendBulkSms = 10027,
	//	UI_Help_BulkList = 10028,
	//	//UI_SmsReports_BulkDetails = 10029,
	//	//UI_Domains_Domain = 10030,
	//	//UI_Domains_RegisterDomain = 10031,
	//	//UI_Users_IncreaseCredit = 10032,
	//	//UI_Users_DecreaseCredit = 10033,
	//	UI_Help_UserServicePrice = 10034,
	//	//UI_ServiceGroups_SaveServiceGroup = 10035,
	//	//UI_Services_SaveService = 10037,
	//	//UI_Accesses_SaveAccess = 10038,
	//	//UI_GroupPrices_SaveGroupPrice = 10039,
	//	//UI_Users_DetermineGroupPrice = 10040,
	//	//UI_PriceRanges_SavePriceRange = 10041,
	//	//UI_Users_AdvanceEdit = 10042,
	//	//UI_PhoneBooks_SaveSingleNumber = 10043,
	//	//UI_UserAccesses_UserAccess = 10044,
	//	//UI_PhoneBooks_SaveListNumber = 10045,
	//	//UI_PhoneBooks_SaveFileNumber = 10046,
	//	//UI_SmsSenderAgents_SmsSenderAgent = 10047,
	//	//UI_SmsSenderAgents_SaveSmsSenderAgent = 10048,
	//	//UI_PrivateNumbers_SavePrivateNumber = 10049,
	//	//UI_PrivateNumbers_NumberStatus = 10050,
	//	//UI_PrivateNumbers_SaveUserPrivateNumber = 10051,
	//	UI_Help_SentBox = 10052,
	//	UI_Help_Inbox = 10053,
	//	UI_Help_DefineGlobalServicePrice = 10054,
	//	//UI_UserServices_BuyService = 10055,
	//	//UI_AccountInformations_SaveAccount = 10056,
	//	UI_Help_FishDetails = 10057,
	//	UI_Help_OutBox = 10058,
	//	UI_Help_UserShortcut = 10059,
	//	UI_Help_SendAdvancedGroupSms = 10060,
	//	UI_Help_SendFormatSms = 10061,
	//	UI_Help_SendSingleFutureSms = 10062,
	//	UI_Help_SendGroupFutureSms = 10063,
	//	UI_Help_SendFutureSmsFormat = 10064,
	//	UI_Help_SendPeriodFutureSingleSms = 10065,
	//	UI_Help_SendPeriodFutureGroupSms = 10066,
	//	UI_Help_SendBirthDateSms = 10067,
	//	UI_Help_GroupTemplate = 10068,
	//	//UI_GroupTemplates_SaveGroupTemplate = 10069,
	//	UI_Help_SmsTemplate = 10070,
	//	//UI_SmsTemplates_SaveSmsTemplate = 10071,
	//	UI_Help_GalleryImage = 10072,
	//	//UI_GalleryImages_SaveGalleryImage = 10073,
	//	UI_Help_Image = 10074,
	//	//UI_Images_SaveImage = 10075,
	//	UI_Help_DataCenter = 10076,
	//	//UI_NewsCenters_SaveNewsCenter = 10077,
	//	UI_Help_News = 10078,
	//	//UI_News_SaveNews = 10079,
	//	UI_Help_UserFish = 10080,
	//	//UI_SmsReports_ShowChartDetails = 10081,
	//	//UI_SmsTemplates_LoadSmsTemplate = 10082,
	//	//UI_GeneralPhoneBooks_GeneralPhoneBook = 10083,
	//	//UI_GeneralPhoneBooks_GeneralNumber = 10084,
	//	//UI_GeneralPhoneBooks_SaveSingleGeneralNumber = 10085,
	//	//UI_GeneralPhoneBooks_SaveListGeneralNumber = 10086,
	//	//UI_GeneralPhoneBooks_SaveFileGeneralNumber = 10087,
	//	UI_Help_SendCitySms = 10088,
	//	UI_Help_SendPrefixSms = 10089,
	//	UI_Help_LoginStat = 10090,
	//	//UI_Users_UserSetting = 10091,
	//	//UI_GeneralPhoneBooks_DefineGeneralGroupPrice = 10092,
	//	UI_Help_SendPostalCodeSms = 10093,
	//	//UI_SmsSends_SendAdvancedSearchGroupSms = 10094,
	//	//UI_Domains_DomainSetting = 10095,
	//	//UI_News_ShowNews = 10096,
	//	//UI_DomainMenu_SaveDomainMenu = 10097,
	//	//UI_DomainMenu_DomainMenu = 10098,
	//	//UI_DesktopMenuLocations_SaveDesktopMenuLocation = 10099,
	//	//UI_DesktopMenuLocations_DesktopMenuLocation = 10100,
	//	//UI_EmailBooks_EmailBook = 10101,
	//	//UI_EmailBooks_EmailAddress = 10102,
	//	//UI_EmailBooks_SaveEmail = 10103,
	//	//UI_EmailSends_SendSingleEmail = 10104,
	//	//UI_Users_RegisterLight = 10105,
	//	//UI_EmailSettings_UserEmailSetting = 10106,
	//	//UI_EmailSettings_SaveSetting = 10107,
	//	//UI_EmailSends_SendGroupEmail = 10108,
	//	//UI_SmsParsers_SimpleSecretary = 10109,
	//	UI_Help_UnderConstruction,
	//	UI_Help_EmailTemplate = 10114,

	//}

	//This ENUM use in DB dont change it!
	public enum Services
	{
		ManageServiceGroup = 1,
		ManageService = 2,
		ManageAccess = 3,
		ManageGroupPrice = 4,
		ManagePriceRange = 5,
		PhoneBook = 6,
		AddSingleNumber = 7,
		AddListNumber = 8,
		AddFileNumber = 9,
		AddUserField = 10,
		AddSmsFormat = 11,
		ManageUser = 12,
		UserService = 13,
		ChangePassword = 14,
		EditProfile = 15,
		RegisterUser = 16,
		SendBulkSms = 17,
		SendSingleSms = 18,
		SendGroupSms = 19,
		ManageDomain = 20,
		SaveService = 21,
		SaveServiceGroup = 22,
		TransactionList = 23,
		RegisterFish = 24,
		IncreaseCredit = 25,
		DecreaseCredit = 26,
		BulkList = 27,
		DefinePrivateNumber = 28,
		ManagePrivateNumber = 29,
		ManageSmsSenderAgent = 30,
		ConfirmFish = 31,
		AccountInformation = 32,
		Inbox = 33,
		SentBox = 34,
		OutBox = 35,
		DefineUserShortcut = 36,
		StatisticsSend = 37,
		AssignPrivateNumberToUser = 38,
		DefineGlobalServicePrice = 39,
		DefineUserServicePrice = 40,
		DefinePhoneBookField = 41,
		ManagePhoneNumber = 42,
		DefineUserGroupPrice = 43,
		DefineUserAccess = 44,
		AdvancedEditUser = 45,
		ViewUserTransaction = 46,
		ViewUserEditProfile = 47,
		SendAvancedGroupSms = 48,
		SendFormatSms = 49,
		SendSingleFutureSms = 50,
		SendGroupFutureSms = 51,
		SendFutureSmsFormat = 52,
		SendPeriodFutureSingleSms = 53,
		SendPeriodFutureGroupSms = 54,
		SendBirthDateSms = 55,
		GroupTemplateList = 56,
		AddGroupTemplate = 57,
		SmsTemplateList = 58,
		AddSmsTemplate = 59,
		GalleryImageList = 60,
		ImagesList = 61,
		DataCenterList = 62,
		ContentList = 63,
		UserFish = 64,
		SaveGalleryImage = 65,
		SaveImage = 66,
		SaveDataCenter = 67,
		SaveContent = 68,
		ChartDetails = 69,
		LoadSmsTemplate = 70,
		GeneralPhoneBook = 71,
		ManageGeneralNumber = 72,
		AddFileGeneralNumber = 73,
		AddSingleGeneralNumber = 74,
		AddListGeneralNumber = 75,
		SendPostalCodeSms = 76,
		SendCitySms = 77,
		SendPrefixSms = 78,
		LoginStat = 79,
		UserSetting = 80,
		DefineGeneralGroupPrice = 81,
		UserGeneralPhoneBook = 82,
		SendAdvancedSearchGroupSms = 83,
		DomainSetting = 84,
		ShowNews = 85,
		//SaveDomainMenu = 86,
		//DomainMenu = 87,
		SaveDataLocation = 88,
		//DesktopMenuLocation = 89,
		EmailBook = 90,
		ManageEmail = 91,
		SaveEmail = 92,
		SendSingleEmail = 93,
		RegisterLight = 94,
		UserEmailSetting = 95,
		SaveEmailSetting = 96,
		SendGroupEmail = 97,
		AnalystSms = 98,
		SendAdvancedGroupFutureSms = 99,
		ShowBlackListNumber = 100,
		Competition = 101,
		Poll = 102,
		EmailTemplate = 103,
		EmailOutbox = 104,
		EmailSentbox = 105,
		SendSmsForUsers = 106,
		SendFutureSmsForUsers = 107,
		CreateNicAccount = 108,
		CreateDirectAccount = 109,
		OnlineRegisterDomain = 110,
		TransportNicDomain = 111,
		Role = 112,
		SaveRole = 113,
		DefineServiceOfRole = 114,
		SearchAllPhoneBookGroups = 115,
		SearchAllSmsFormats = 116,
		SearchAllUserFields = 117,
		InboxGroup = 118,
		DetermineUserRole = 119,
		BuyService = 120,
		SendSmsFromDateField = 121,
		DomainContinuation = 122,
		DomainPrice = 123,
		DomainGroupPrice = 124,
		SaveDomainGroupPrice = 125,
		SaveDomainPrice = 126,
		ManageUserMessage = 127,
		SendSms = 128,
		DefineUser = 129,
		ScheduledSms = 130,
		SavePoll = 131,
		SaveCompetition = 132,
		SaveSmsFilter = 133,
		TrafficRelay = 134,
		SaveTrafficRelay = 135,
		SystemLog = 136,
		SetTrafficRelayForPrivateNumber = 137,
		MessageRoute = 138,
		SmsSenderAgentRatio = 139,
		UsersSendQueue = 140,
		UserOutbox = 141,
		SendDetails = 142,
		MainSetting = 143,
		SaveMainSetting = 144,
		ConfirmUserDocument = 145,
		PhoneBookVasSetting = 146,
		AggregationPanel = 147,
		ManageBlackListNumber = 148,
		UpdateDeliveryStatusManually = 149,
		RegularContent = 150,
		SendP2PSms = 151,
		Booking = 152,
		GetBulkMessagesFromAPI = 153,
		PrivateNumbersInfo = 154,
		UserManualOutbox = 155,
		UserInbox = 156,
		AdminSetting = 157,
		FilterWords = 158,
		UsersTransactionList = 159,
	}

	//This ENUM use in DB dont change it!
	public enum Permissions
	{
		AddAccess = 1,
		EditAccess = 2,
		DeleteAccess = 3,
		AddGroupPrice = 4,
		EditGroupPrice = 5,
		DeleteGroupPrice = 6,
		AddPhoneBookGroup = 7,
		EditPhoneBookGroup = 8,
		CutPhoneBookGroup = 9,
		PastePhoneBookGroup = 10,
		DeletePhoneBookGroup = 11,
		AddPriceRange = 12,
		EditPriceRange = 13,
		DeletePriceRange = 14,
		AddServiceGroup = 15,
		EditServiceGroup = 16,
		DeleteServiceGroup = 17,
		AddService = 18,
		EditService = 19,
		DeleteService = 20,
		AdvanceEditUser = 21,
		DeleteUser = 22,
		EditUserService = 23,
		EditUserAccess = 24,
	}

	//This ENUM use in DB dont change it!
	//public enum CountryStates
	//{
	//	Esfahan = 1,
	//	Qom = 2,
	//	Tehran = 3,
	//	Alborz = 4,
	//	AzerbaijanEast = 5,
	//	AzerbaijanWest = 6,
	//	Ardabil = 7,
	//	Ilam = 8,
	//	Bushehr = 9,
	//	ChaharmahalBakhtiari = 10,
	//	KhorasanNorth = 11,
	//	KhorasanRazavi = 12,
	//	KhorasanSouth = 13,
	//	Khuzestan = 14,
	//	Zanjan = 15,
	//	Semnan = 16,
	//	SistanBaluchestan = 17,
	//	Fars = 18,
	//	Qazvin = 19,
	//	Kurdistan = 20,
	//	Kerman = 21,
	//	Kermanshah = 22,
	//	KohgiluyehVaBoyerAhmad = 23,
	//	Gulistan = 24,
	//	Gilan = 25,
	//	Lorestan = 26,
	//	Mazandaran = 27,
	//	Markazi = 28,
	//	Hormozgan = 29,
	//	Hamedan = 30,
	//	Yazd = 31,
	//}

	//This ENUM use in DB dont change it!
	//Dont User 0 Value!
	public enum UserFieldTypes
	{
		Strings = 1,
		Number = 2,
		DateTime = 3,
	}

	//This ENUM use in DB dont change it!
	public enum TypeTransactions
	{
		Increase = 1,
		Decrease = 2,
	}

	//This ENUM use in DB dont change it!
	public enum TypeCreditChanges
	{
		Manage = 1,
		BuyPanel = 2,
		ExtendedPanel = 3,
		Fish = 4,
		OnlinePayment = 5,
		//SendSingleSms = 6,
		//SendGroupSms = 7,
		//SendAdvancedGroupSms = 8,
		//SendSmsRange = 9,
		//SendFutureSingleSms = 10,
		//SendFutureGroupSms = 11,
		//SendFutureAdvancedGroupSms = 12,
		//SendFutureSingleSmsInPeriod = 13,
		//SendFutureGroupSmsInPeriod = 14,
		//SendFutureAdvancedGroupSmsInPeriod = 15,
		//SendFutureSmsInDateTimeField = 16,
		//SendBulkSms = 17,
		//SendCitySms = 18,
		//Tax = 19,
		//Card = 20,
		PriceNumber = 21,
		ActivationService = 22,
		GiveBackCostOfUnsuccessfulSent = 23,
		//GiveBackCostOfUnsuccessfulSentBulk = 24,
		ActivationGeneralPhoneBook = 25,
		//SendAdvancedSpecialGroupSms = 26,
		//SendSpecialGroupSms = 27,
		//SendSpecialGroupFutureSms = 28,
		//SendBirthDateSms = 29,
		//SendSmsFromSecretary = 30,
		//RegisterDomain = 31,
		SendSms = 32,
		DeleteUser = 33,
	}

	//This ENUM use in DB dont change it!
	public enum MainSettings
	{
		Tax = 1,
		SendQueueRecipientAddress = 2,
		ExportFileAddress = 3,
		RegisterFishSmsText = 4,
		LoginSmsText = 5,
		LowCreditSmsText = 6,
		RegisterUserSmsText = 7,
		UserAccountSmsText = 8,
		UserExpireSmsText = 9,
		OnlinePaymentSmsText = 10,
		RetrievePasswordSmsText = 11,
		MaximumFailedTryCount = 12,
		VasRegisterSmsText = 13,
		IsRemoteQueue = 14,
		RemoteQueueIP = 15,
		AppPath = 16,
		SendSmsAlertMessage = 17,
	}

	//This ENUM use in DB dont change it!
	//public enum SmsSendType
	//{
	//	//SendSingleSms = 1,
	//	//SendSingleFutureSms = 2,
	//	//SendGroupFutureSms = 3,
	//	//SendFormatGroupFutureSms = 4,
	//	//SendAdvancedGroupFutureSms = 5,
	//	//SendFormatAdvancedGroupFuture = 6,
	//	//SendPeriodFutureSingleSms = 7,
	//	//SendPeriodFutureGroupSms = 8,
	//	//SendFormatPeriodFutureGroupSms = 9,
	//	//SendPeriodFutureAdvancedGroupSms = 10,
	//	//SendFormatPeriodFutureAdvancedGroupSms = 11,
	//	//SendFutureFieldSms = 12,
	//	//SendFormatFutureFieldSms = 13,
	//	//SendGroupSms = 14,
	//	//SendFormatSms = 15,
	//	//SendAdvancedGroupSms = 16,
	//	//SendFormatAdvancedGroupSms = 17,
	//	//SendRangesSms = 18,
	//	//SendFormatRangesSms = 19,
	//	//SendBulkSms = 20,
	//	//SendCitySms = 21,
	//	//SendFutureSmsFormat = 22,
	//	//SendBirthDateSms = 23,
	//	//SendPostalCodeSms = 24,
	//	//SendSpecialGroupFutureSms = 25,
	//	//SendSpecialGroupSms = 26,
	//	//SendAdvancedSpecialGroupSms = 27,
	//	//SendAdvancedSpecialGroupFutureSms = 28,
	//	//SendSmsForUsers = 29,
	//	//SendFutureSmsForUsers = 30,
	//	//SendSmsFromSecretary = 31,
	//	//SendPrefixSms = 32,
	//	//SendDateFieldSms = 33
	//	SendSms = 1,
	//	SendGroupSms = 2,
	//	SendFormatSms = 3,
	//	//SendAdvancedGroupSms = 4,
	//	SendPeriodSms = 5,
	//	SendGradualSms = 6,
	//	SendBulkSms = 7,
	//	//SendPeriodBulkSms = 8,
	//	//SendGradualBulkSms = 9,
	//	//SendBirthDateSms = 10,
	//	//SMPP = 11,
	//	//Email = 12,
	//	//BulkMail = 13,

	//}

	//This ENUM use in DB dont change it!
	public enum SendPriority
	{
		Lowest = 0,
		VeryLow = 1,
		Low = 2,
		Normal = 3,
		AboveNormal = 4,
		High = 5,
		VeryHigh = 6,
		Highest = 7,
	}

	//This ENUM use in DB dont change it!
	public enum EmailSendType
	{
		SendSingleEmail = 1,
		//SendSingleFutureEmail = 2,
		SendGroupEmail = 3,
		//SendGroupFutureEmail = 4,
	}

	//This ENUM use in DB dont change it!
	public enum SmsSentPeriodType
	{
		Minute = 1,
		Hour = 2,
		Daily = 3,
		Weekly = 4,
		Monthly = 5,
		Yearly = 6
	}

	//This ENUM use in DB dont change it!
	//public enum SendStatus
	//{
	//	Stored = 1,//ذخیره شده
	//	WatingForSend = 2,// منتظر برای ارسال
	//	IsBeingSent = 3,// در حال ارسال شدن
	//	Sent = 4,//به اپراتور ارسال شد
	//	ErrorInSending = 5,// در ارسال پیامک خطا رخ داده است
	//	ErrorInGetItc = 6,//در دریافت پیامک توسط اپرتور خطا رخ داده است
	//	BlackList = 7,
	//	SentToCpanel = 8,
	//	TryLimit = 9,
	//	CreditNotEnough = 10,
	//}

	//This ENUM use in DB dont change it!
	public enum SmsSentStates
	{
		UNCERTAIN = 0,
		Pending = 1,
		PENDING = 2,
		InProgress = 2,
		Completed = 3,
		Failed = 4,
		COMPOSING = 5,
		ACCEPTED = 6,
		REJECTED = 7,
		STARTED = 8,
		FINISHED = 9,
		HALTED = 10,
		SendAndWaite = 11,
	}

	//This ENUM use in DB dont change it!
	public enum Messageclass
	{
		Flash = 0,
		Normal = 1,
		Simcard = 2,
		ApplicationSms = 3
	}

	//This ENUM use in DB dont change it!
	public enum Encoding
	{
		Default = 1,
		Utf8 = 2,
		Data = 5,
		Binary = 6
	}

	//This ENUM use in DB dont change it!
	public enum TypePrivateNumberAccesses
	{
		Bulk = 0,
		InteractiveBulk = 1,
		SmsMT = 2,
		SmsMO = 3,
		USSDMT = 4,
		USSDMO = 5,

	}

	//This ENUM use in DB dont change it!
	public enum Banks
	{
		EghtesadNovin = 1,
		Pasargad = 2,
		Parsian = 3,
		Tat = 4,
		Tejarat = 5,
		Saman = 6,
		Sepah = 7,
		Shahr = 8,
		Saderat = 9,
		Mehr = 10,
		Keshavarzi = 11,
		Gardeshgari = 12,
		Maskan = 13,
		Mellat = 14,
		Melli = 15,
	}

	//This ENUM use in DB dont change it!
	public enum TypeFish
	{
		//Cash = 1,
		//Cheque = 2,
		//Card = 3,
		Account = 4,
		OnLine = 5,
	}

	//This ENUM use in DB dont change it!
	public enum FishStatus
	{
		Confirmed = 1,
		Rejected = 2,
		Checking = 3,
	}

	public enum UserDocumentStatus
	{
		Confirmed = 1,
		Rejected = 2,
		Checking = 3,
	}

	//This ENUM use in DB dont change it!
	public enum NumberType
	{
		All = 0,
		Credits = 1,
		Permanent = 2,
	}

	//This ENUM use in DB dont change it!
	public enum SmsSendFailedType
	{
		None = 0,
		UserCreditNotEnough = 1,
		AdminCreditNotEnough = 2,
		PrivateNumberIsInactive = 3,
		PrivateNumberIsExpired = 4,
		UserIsInactive = 5,
		AdminIsInactive = 6,
		PrivateNumberNotValid = 7,
		NotDefineSmsRateType = 8,
		SendError = 9,
		UserCancelRequest = 10,
		SystemIsOutOfService = 11,
		SendTimeNotValid = 12,
		SmsTextIsFilter = 13,
		UserIsExpired = 14,
		AdminIsExpired = 15,
	}

	public enum CheckNumberScope
	{
		DeleteDuplicateNumberInGroup = 1,
		DeleteDuplicateNumberInTotalGroup = 2,
	}

	public enum CheckEmailScope
	{
		DeleteDuplicateEmailInGroup = 1,
		DeleteDuplicateEmailInTotalGroup = 2,
	}

	public enum Gender
	{
		Man = 1,
		Woman = 2,
	}

	public enum SmsTypes
	{
		Farsi = 1,
		Latin = 2,
		Recieve = 3,
	}

	public enum SmsSendError
	{
		SendError = 0,
		NotEnoughCredit = -1,
		ServerError = -2,
		DeactiveAccount = -3,
		ExpiredAccount = -4,
		InvalidUsernameOrPassword = -5,
		AuthenticationFailure = -6,
		ServerBusy = -7,
		NumberAtBlackList = -8,
		LimitedInSendDay = -9,
		LimitedInVolume = -10,
		InvalidSenderNumber = -11,
		InvalidRecieverNumber = -12,
		InvalidDestinationNetwork = -13,
		UnreachableNetwork = -14,
		DeactiveSenderNumber = -15,
		InvalidFormatOfSenderNumber = -16,
	}

	public enum Desktop
	{
		MainDesktop = 1,
		OrangeDesktop = 2,
		WindowsDesktop = 3,
		AndroidDesktop = 4,
		Default = 5,
        AradDesktop = 6,
		mscdesktop = 7,
        Desktop = 8,
    }

	public enum Theme
	{
		BlackTie = 1,
		Blitzer = 2,
		Cupertino = 3,
		DarkHive = 4,
		Darkness = 5,
		DotLuv = 6,
		Eggplant = 7,
		ExciteBike = 8,
		Flick = 9,
		HotSneaks = 10,
		Humanity = 11,
		LeFrog = 12,
		Lightness = 13,
		MintChoc = 14,
		Overcast = 15,
		PepperGrinder = 16,
		Redmond = 17,
		Smoothness = 18,
		SouthStreet = 19,
		Start = 20,
		Sunny = 21,
		SwankyPurse = 22,
		Trontastic = 23,
		Vader = 24,
		AceGrid = 25,
	}

	public enum Chart
	{
		column = 1,
		LineRenderer = 2,
		PieRenderer = 3
	}

	public enum DataCenterType
	{
		All = 0,
		News = 1,
		Menu = 2,
		Article = 3,
	}

	public enum LoginStatsType
	{
		SignIn = 1,
		SignOut = 2
	}

	public enum PhoneBookType
	{
		Normal = 1,
		Special = 2
	}

	public enum UserUseType
	{
		Agent = 1,
		NormalUser = 2,
		UserNonReal = 3,
	}

	public enum DataLocation
	{
		TopRight = 1,
		TopCenter = 2,
		TopLeft = 3,
		BottomRight = 4,
		BottomCenter = 5,
		BottomLeft = 6,
		Right = 7,
		Left = 8,
		Center = 9,
	}

	public enum DomainNameTargetType
	{
		SelfPage = 1,
		BlankPage = 2,
		//ParentPage = 3
	}

	public enum EmailHostType
	{
		Yahoo = 1,
		Gmail = 2,
		Hotmail = 3,
	}

	public enum DefaultPages
	{
		Default = 1,
		Android = 2,
		Windows = 3,
	}

	public class GoogleEmailInfo
	{
		public static readonly bool EnabledSsl = true;
		public static readonly string Host = "smtp.gmail.com";
		public static readonly int Port = 587;
	}

	public class YahooEmailInfo
	{
		public static readonly bool EnabledSsl = false;
		public static readonly string Host = "smtp.mail.yahoo.com";
		public static readonly int Port = 587;
	}

	public class HotmailEmailInfo
	{
		public static readonly bool EnabledSsl = true;
		public static readonly string Host = "smtp.live.com";
		public static readonly int Port = 25;
	}

	public enum SmsFilterConditions
	{
		Equal = 1,
		StartsWith = 2,
		//StartsWithAfterDeleteSelf = 3,
		EndsWith = 4,
		//EndsWithAfterDeleteSelf = 5,
		Include = 6,
		//IsMobile = 7,
		Everything = 8,
		GreaterThan = 9,
		Smaller = 10,
		NationalCode = 11,
		EqualWithPhoneBookField = 12,
	}

	public enum SmsFilterOperations
	{
		AddToGroup = 1,
		RemoveFromGroup = 2,
		TransferToMobile = 3,
		TransferToUrl = 4,
		TransferToEmail = 5,
		SendSmsToSender = 6,
		SendSmsToGroup = 7,
		ForwardSmsToGroup = 8,
		SendSmsFromFormat = 9,
	}

	public enum SmsFilterSenderNumber
	{
		Equal = 1,
		Include = 2,
		StartsWith = 3,
		EndsWith = 4,
		GreaterThan = 5,
		Smaller = 6,
		UnEqual = 7,
	}

	public enum SmsParserType
	{
		Competition = 1,
		Poll = 2,
		Filter = 3,
		Weather = 4,
		PrayerTime = 5,
		Listen = 6,
	}

	public enum Operators
	{
		NotValid = 0,
		MCI = 1,
		MTN = 2,
		Rightel = 3,
		Taliya = 4,
		MTCE = 5,
		KishTCI = 6,
	}

	public enum WebSiteContent
	{
		HomePages_Windows_Content_TravelAgency = 1000,
		HomePages_Windows_Content_Banks = 1001,
		HomePages_Windows_Content_CultureCenters = 1002,
		HomePages_Windows_Content_Doctors = 1003,
		HomePages_Windows_Content_FAQ = 1004,
		HomePages_Windows_Content_HighSchool = 1005,
		HomePages_Windows_Content_Hotels = 1006,
		HomePages_Windows_Content_InsuranceAgencies = 1007,
		HomePages_Windows_Content_NormalPanel = 1008,
		HomePages_Windows_Content_PhoneBook = 1009,
		HomePages_Windows_Content_Poll = 1010,
		HomePages_Windows_Content_ReportSendSms = 1011,
		HomePages_Windows_Content_Resturants = 1012,
		HomePages_Windows_Content_Schools = 1013,
		HomePages_Windows_Content_SendBirthDateSms = 1014,
		HomePages_Windows_Content_SendCitySms = 1015,
		HomePages_Windows_Content_SendDynamicSms = 1016,
		HomePages_Windows_Content_SendFromExcel = 1017,
		HomePages_Windows_Content_SendFutureSms = 1018,
		HomePages_Windows_Content_SendGroupSms = 1019,
		HomePages_Windows_Content_SendOut = 1020,
		HomePages_Windows_Content_SendSingleSms = 1021,
		HomePages_Windows_Content_SendToEsfahanBank = 1022,
		HomePages_Windows_Content_SendToPostalCode = 1023,
		HomePages_Windows_Content_SendToPrefix = 1024,
		HomePages_Windows_Content_SmsCompetition = 1025,
		HomePages_Windows_Content_SmsSecretary = 1026,
		HomePages_Windows_Content_Stores = 1027,
		HomePages_Windows_Content_ShowContent = 1028,
		HomePages_AradSms_Contents_ShowContent = 1029,
		HomePages_AradITC_Contents_ShowContent = 1030,
		HomePages_AradITC_Contents_Index = 1031,
		HomePages_AradITC_Contents_PanelSms = 1032,
		HomePages_AradITC_Contents_SendSingleSms = 1033,
		HomePages_AradITC_Contents_SendGroupSms = 1034,
		HomePages_AradITC_Contents_SendFetureSms = 1035,
		HomePages_AradITC_Contents_SendSmsFromExcel = 1036,
		HomePages_AradITC_Contents_SendSmsTheBirthDay = 1037,
		HomePages_AradITC_Contents_SendDynamicSms = 1038,
		HomePages_AradITC_Contents_PhoneBook = 1039,
		HomePages_AradITC_Contents_OnlinePropaganda = 1040,
		HomePages_AradITC_Contents_DownloadForms = 1041,
		HomePages_AradITC_Contents_DownloadBrowser = 1042,
		HomePages_AradITC_Contents_DownloadMap = 1043,
		HomePages_AradITC_Contents_ShowNews = 1044,
		PanelDemo = 1045,
		HomePages_AradITC_Contents_PanelNewUtility = 1046,
		HomePages_AradITC_Contents_SendSmsCount = 1047,
		HomePages_AradITC_Contents_BestTimeForSendSMS = 1048,
		HomePages_AradITC_Contents_WhoSendSms = 1049,
		HomePages_AradITC_Contents_CollectInformationContacts = 1050,
		HomePages_AradITC_Contents_AdvantageMarketing = 1051,
		HomePages_AradITC_Contents_SettingTextSms = 1052,
		HomePages_AradITC_Contents_DedicatedLine = 1053,
		HomePages_AradITC_Contents_Representation = 1054,
		HomePages_AradITC_Contents_GoldenTipsSms = 1055,
		HomePages_AradITC_Contents_FAQ = 1056,
		HomePages_AradITC_Contents_EventSms = 1057,
		HomePages_AradITC_Contents_EventBirthday = 1058,
		HomePages_AradITC_Contents_Eventnorooz = 1059,
		HomePages_AradITC_Contents_EventFather = 1060,
		HomePages_AradITC_Contents_EventMother = 1061,
		HomePages_AradITC_Contents_EventTeacher = 1062,
		HomePages_AradITC_Contents_Event22bahman = 1063,
		HomePages_AradITC_Contents_EventBirthdayEmamzaman = 1064,
		HomePages_AradITC_Contents_EventGhadir = 1065,
		HomePages_AradITC_Contents_EventFetr = 1066,
		HomePages_AradITC_Contents_EventMabas = 1067,
		HomePages_AradITC_Contents_EventBirthdayPayambar = 1068,
		HomePages_AradITC_Contents_EventGhorban = 1069,
		HomePages_AradITC_Contents_EventBirthdayEmamReza = 1070,
		HomePages_AradITC_Contents_EventBirthdayHazrateMasoome = 1071,
		HomePages_AradITC_Contents_EventAshoora = 1072,
		HomePages_AradITC_Contents_EventArbaeen = 1073,
		HomePages_AradITC_Contents_EventTasliatePayambar = 1074,
		HomePages_AradITC_Contents_EventTasliateHazrateFateme = 1075,
		HomePages_AradITC_Contents_EventTasliateEmameali = 1076,
		HomePages_AradITC_Contents_EventTasliateEmambagher = 1077,
		HomePages_AradITC_Contents_EventTasliateEmamjafar = 1078,
		HomePages_AradITC_Contents_EventTasliateEmamAsgari = 1079,
		HomePages_AradITC_Contents_EventYalda = 1080,
		HomePages_AradITC_Contents_SendSms = 1081,
		HomePages_AradITC_Contents_AdvertisingSms = 1082,
		HomePages_AradITC_Contents_SendFreeSms = 1083,
		HomePages_AradITC_Contents_PaymentPanel = 1084,
	}

	public enum PrivateNumberUseForm
	{
		OneNumber = 0,
		Mask = 1,
		RangeNumber = 2
	}

	public enum UserDocumentType
	{
		NationalCard = 1,
		BirthCertificate = 2,
		BusinessLicense = 3,
		LeaseOrDocument = 4,
		PhoneBill = 5,
		EstablishAd = 6,
		ChangesAd = 7,
		CommitmentsForm = 8,
		CEONationalCard = 9,
		CEOBirthCertificate = 10,
	}

	public enum UserType
	{
		Actual = 1,
		Legal = 2,
	}

	public enum LogType
	{
		Warning = 1,
		Error = 2,
		Action = 3,
	}

	public enum AccountSetting
	{
		CreditWarning = 1,
		ExpireWarning = 2,
		LoginWarning = 3,
		Shortcut = 4,
		DefaultNumber = 5,
		ApiPassword = 6,
		ApiIP = 7,
		SmsTrafficRelay = 8,
		DeliveryTrafficRelay = 9,
	}

	public enum SiteSetting
	{
		Logo = 1,
		Favicon = 2,
		Title = 3,
		Footer = 4,
		SlideShow = 5,
		Description = 6,
		Keywords = 7,
		CompanyName = 8,
	}

	public enum PhoneBookGroupType
	{
		Normal = 1,
		Vas = 2,
		Email = 3,
	}

	public enum ColumnSearchTypeSelect
	{
		TransactionTypeCreditChange = 1,
		TransactionType = 2,
		FishStatus = 3,
		SmsSendType = 4,
		ServiceGroup = 5,
		DeliveryStatus = 6,
		Gender = 7,
		SendStatus = 8,
	}

	public enum RegularContentType
	{
		File = 1,
		URL = 2,
		DB = 3,
	}

	public enum WarningType
	{
		None = 1,
		Sms = 2,
		Email = 3,
	}

	public enum OnlinePaymentParams
	{
		ReferenceId = 1,
		ResCode = 2,
		SaleReferenceId = 3,
		SaleOrderId = 4,
		IsExtendedPanel = 5,
		SalePackageId = 6,
		Ip = 7,
		IsExtendedNumber = 8,
        Token = 9,
        OrderId = 10,
        TerminalNo = 11,
        RRN = 12,
        status = 13

    }

	public enum TypeServiceId
	{
		MCIServiceId = 1,
		MTNServiceId = 2,
		AggServiceId = 3,
	}

	public enum PhoneBookFields
	{
		FirstName,
		LastName,
		NationalCode,
		BirthDate,
		Telephone,
		CellPhone,
		FaxNumber,
		Job,
		Address,
		ZipCode,
		Email,
		Sex,
	}
}
