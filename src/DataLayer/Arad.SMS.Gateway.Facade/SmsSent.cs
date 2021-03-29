using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Business;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using Common.Exceptions;

namespace Facade
{
	public class SmsSent : FacadeEntityBase
	{
		#region SelectMethod
		public static Business.Operators GetOperatorType(string phoneNumber)
		{
			if (phoneNumber.StartsWith("+9891") || phoneNumber.StartsWith("9891") || phoneNumber.StartsWith("091"))
				return Business.Operators.MCI;
			else if (phoneNumber.StartsWith("+9893") || phoneNumber.StartsWith("9893") || phoneNumber.StartsWith("093"))
				return Business.Operators.Irancell;
			else if (phoneNumber.StartsWith("+9892") || phoneNumber.StartsWith("9892") || phoneNumber.StartsWith("092"))
				return Business.Operators.Rightel;
			else
				return Business.Operators.MCI;
		}

		public static Dictionary<Business.Operators, int> GetCountNumberOfOperators(ref List<string> lstNumbers, ref List<string> lstFailedNumber)
		{
			try
			{
				Dictionary<Business.Operators, int> operatorCountNumberDictionary = new Dictionary<Business.Operators, int>();

				lstNumbers = lstNumbers.Select<string, string>(s => s.StartsWith("+98") ? "0" + s.Substring(3) : s).ToList();
				lstNumbers = lstNumbers.Select<string, string>(s => s.StartsWith("98") ? "0" + s.Substring(2) : s).ToList();
				lstNumbers = lstNumbers.Select<string, string>(s => s.StartsWith("9") ? "0" + s : s).ToList();

				lstFailedNumber = lstNumbers.Where(s => !Helper.IsCellPhone(s)).ToList();
				lstNumbers = lstNumbers.Where(s => Helper.IsCellPhone(s)).Distinct().ToList();

				if (lstNumbers.Count == 0)
					throw new Exception("CompleteRecieverNumberField");

				int countMCI = lstNumbers.Where(reciever => GetOperatorType(reciever) == Business.Operators.MCI).Count();
				int countIrancell = lstNumbers.Where(reciever => GetOperatorType(reciever) == Business.Operators.Irancell).Count();
				int countOther = lstNumbers.Where(reciever => GetOperatorType(reciever) != Business.Operators.MCI &&
																											GetOperatorType(reciever) != Business.Operators.Irancell).Count();


				operatorCountNumberDictionary.Add(Operators.MCI, countMCI);
				operatorCountNumberDictionary.Add(Operators.Irancell, countIrancell);
				operatorCountNumberDictionary.Add(Operators.Other, countOther);

				return operatorCountNumberDictionary;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetPagedSmses(Common.SmsSent smsSent, Guid smsSenderAgentGuid, string userName, string senderNumber, string fromCreateDate, string fromTime, string toCreateDate, string toTime, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			Business.SmsSent smsSentController = new Business.SmsSent();
			return smsSentController.GetPagedSmses(smsSent, smsSenderAgentGuid, userName, senderNumber, fromCreateDate, fromTime, toCreateDate, toTime, pageNo, pageSize, sortField, ref resultCount);
		}

		public static DataTable GetPagedUserBulks(Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.SmsSent smsSentController = new Business.SmsSent();
			return smsSentController.GetPagedUserBulks(userGuid, sortField, pageNo, pageSize, ref resultCount);
		}
		#endregion

		#region InsertMethods
		public static bool InsertSendSingleSms(Common.SmsSent smsSent, List<string> lstNumbers, ref List<string> lstFailedNumber)
		{
			Business.SmsSent smsSentController = new Business.SmsSent();
			smsSent.SmsSentGuid = Guid.NewGuid();

			smsSentController.BeginTransaction();
			try
			{
				Dictionary<Business.Operators, int> operatorCountNumberDictionary = GetCountNumberOfOperators(ref lstNumbers, ref lstFailedNumber);
				//smsSent.Reciever = string.Join(",", lstNumbers.ToArray());

				#region SmsPrice
				decimal smsPrice = 0;
				int smsPartCount = 1;
				int agent = 0;

				Common.PrivateNumber privateNumber = Facade.PrivateNumber.LoadNumber(smsSent.PrivateNumberGuid);
				List<DataRow> lstOperatorRatio = Facade.SmsSenderAgent.GetOperatorRatio(privateNumber.SmsSenderAgentGuid, ref agent).AsEnumerable().ToList();
				Dictionary<Guid, decimal> dictionaryAgentRatio = Facade.GroupPrice.GetAgentRatio(smsSent.UserGuid);
				smsPartCount = 1;//privateNumber.Type == (int)Business.TypePrivateNumberAccesses.Bulk ? Helper.GetSmsCount(smsSent.SmsBody) : 1;

				decimal operatorRatio = 1;

				switch (false)//(Helper.HasUniCodeCharacter(smsSent.SmsBody))
				{
					case true:
						operatorRatio = Helper.GetDecimal(lstOperatorRatio.Where(row => Helper.GetInt(row["Operator"]) == (int)Operators.MCI &&
																																						Helper.GetInt(row["SmsType"]) == (int)SmsTypes.Farsi).First()["Ratio"], 1);
						smsPrice += operatorCountNumberDictionary[Operators.MCI] * operatorRatio * smsPartCount * Helper.GetDecimal(dictionaryAgentRatio[privateNumber.SmsSenderAgentGuid], 1);

						operatorRatio = Helper.GetDecimal(lstOperatorRatio.Where(row => Helper.GetInt(row["Operator"]) == (int)Operators.Irancell &&
																																						Helper.GetInt(row["SmsType"]) == (int)SmsTypes.Farsi).First()["Ratio"], 1);
						smsPrice += operatorCountNumberDictionary[Operators.Irancell] * operatorRatio * smsPartCount * Helper.GetDecimal(dictionaryAgentRatio[privateNumber.SmsSenderAgentGuid], 1);

						operatorRatio = Helper.GetDecimal(lstOperatorRatio.Where(row => Helper.GetInt(row["Operator"]) == (int)Operators.Other &&
																																						Helper.GetInt(row["SmsType"]) == (int)SmsTypes.Farsi).First()["Ratio"], 1);
						smsPrice += operatorCountNumberDictionary[Operators.Other] * operatorRatio * smsPartCount * Helper.GetDecimal(dictionaryAgentRatio[privateNumber.SmsSenderAgentGuid], 1);
						break;
					case false:
						operatorRatio = Helper.GetDecimal(lstOperatorRatio.Where(row => Helper.GetInt(row["Operator"]) == (int)Operators.MCI &&
																																						Helper.GetInt(row["SmsType"]) == (int)SmsTypes.Latin).First()["Ratio"], 1);
						smsPrice += operatorCountNumberDictionary[Operators.MCI] * operatorRatio * smsPartCount * Helper.GetDecimal(dictionaryAgentRatio[privateNumber.SmsSenderAgentGuid], 1);

						operatorRatio = Helper.GetDecimal(lstOperatorRatio.Where(row => Helper.GetInt(row["Operator"]) == (int)Operators.Irancell &&
																																						Helper.GetInt(row["SmsType"]) == (int)SmsTypes.Latin).First()["Ratio"], 1);
						smsPrice += operatorCountNumberDictionary[Operators.Irancell] * operatorRatio * smsPartCount * Helper.GetDecimal(dictionaryAgentRatio[privateNumber.SmsSenderAgentGuid], 1);

						operatorRatio = Helper.GetDecimal(lstOperatorRatio.Where(row => Helper.GetInt(row["Operator"]) == (int)Operators.Other &&
																																						Helper.GetInt(row["SmsType"]) == (int)SmsTypes.Latin).First()["Ratio"], 1);
						smsPrice += operatorCountNumberDictionary[Operators.Other] * operatorRatio * smsPartCount * Helper.GetDecimal(dictionaryAgentRatio[privateNumber.SmsSenderAgentGuid], 1);
						break;
				}
				#endregion

				//DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
				//if (dtNumberInfo.Rows.Count == 0)
				//throw new Exception(Language.GetString("BlankSenderNumber"));

				//Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
				//dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());
				string decreaseDescription = string.Format(Language.GetString("DecreaseCreditForSendSms"), lstNumbers.Count);

				Transaction.DecreaseCostOfSmsSend(smsSent.UserGuid, Business.SmsSendType.SendSms,
																					smsPrice, decreaseDescription, smsSent.SmsSentGuid, smsSentController.DataAccessProvider);

				XDocument doc = new XDocument();
				XElement root = new XElement("NewDataSet");

				foreach (string reciever in lstNumbers)
				{
					XElement element = new XElement("Table");
					element.Add(new XElement("Status", (int)Business.SmsStatus.Pending));
					element.Add(new XElement("DeliveryStatus", (int)Business.DeliveryStatus.Uncertain));
					element.Add(new XElement("SmsSenderAgentReference", (int)agent));
					element.Add(new XElement("Reciever", reciever));
					root.Add(element);
				}
				doc.Add(root);

				//if (!smsSentController.InsertSendSingleSms(smsSent, doc.ToString()))
				throw new Exception(Language.GetString("ErrorRecord"));

				smsSentController.CommitTransaction();
				return true;
			}
			catch (InnerTransactionException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				smsSentController.RollbackTransaction();
				throw ex;
			}
		}

		public static bool InsertSendGroupSms(Common.SmsSent smsSent)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//smsSent.SmsSentGuid = Guid.NewGuid();

			//Business.SmsTypes smsType = smsSent.Encoding == (int)Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

			//smsSentController.BeginTransaction();
			//try
			//{
			//	if (smsSent.GroupGuid == string.Empty)
			//		throw new Exception(Language.GetString("SelectGroupError"));

			//	Dictionary<Business.Operators, int> operatorCountNumberDictionary = PhoneNumber.GetCountNumberOfOperators(smsSent.GroupGuid, 0, 0, false);

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																												dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	//Transaction.DecreaseCostOfSmsSend(smsSent.UserGuid, Business.SmsSendType.SendGroupSms,
			//	//																	operatorCountNumberDictionary, smsSent.SmsCount, smsType, agent, smsSent.SmsSentGuid, smsSentController.DataAccessProvider);

			//	if (!smsSentController.InsertSendGroupSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent))
			//		throw new Exception(Language.GetString("ErrorRecord"));

			//	smsSentController.CommitTransaction();
			//	return true;
			//}
			//catch (InnerTransactionException ex)
			//{
			//	throw ex;
			//}
			//catch (Exception ex)
			//{
			//	smsSentController.RollbackTransaction();
			//	throw ex;
			//}
			return false;
		}

		public static bool InsertSendSpecialGroupSms(Common.SmsSent smsSent)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//smsSent.SmsSentGuid = Guid.NewGuid();

			//Business.SmsTypes smsType = smsSent.Encoding == (int)Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

			//smsSentController.BeginTransaction();
			//try
			//{
			//	if (smsSent.GroupGuid == string.Empty)
			//		throw new Exception(Language.GetString("SelectGroupError"));

			//	Dictionary<Business.Operators, int> operatorCountNumberDictionary = GeneralNumber.GetCountNumberOfOperators(smsSent.GroupGuid, 0, 0);

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																												dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	//Transaction.DecreaseCostOfSmsSend(smsSent.UserGuid, Business.SmsSendType.SendSpecialGroupSms,
			//	//																	operatorCountNumberDictionary, smsSent.SmsCount, smsType, agent, smsSent.SmsSentGuid, smsSentController.DataAccessProvider);

			//	if (!smsSentController.InsertSendSpecialGroupSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent))
			//		throw new Exception(Language.GetString("ErrorRecord"));

			//	smsSentController.CommitTransaction();
			//	return true;
			//}
			//catch (InnerTransactionException ex)
			//{
			//	throw ex;
			//}
			//catch (Exception ex)
			//{
			//	smsSentController.RollbackTransaction();
			//	throw ex;
			//}
			return false;
		}

		//public static bool InsertSendAdvancedGroupSms(Common.SmsSent smsSent)
		//{
		//	Business.SmsSent smsSentController = new Business.SmsSent();
		//	smsSent.SmsSentGuid = Guid.NewGuid();

		//	Business.SmsTypes smsType = smsSent.Encoding == (int)Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

		//	smsSentController.BeginTransaction();
		//	try
		//	{
		//		if (smsSent.GroupGuid == string.Empty)
		//			throw new Exception(Language.GetString("SelectGroupError"));

		//		long countNumber = smsSent.TypeSend == (int)Business.SmsSendType.SendAdvancedGroupSms ?
		//											 Facade.PhoneNumber.GetCountNumberOfPhoneBook(Helper.GetGuid(smsSent.GroupGuid)) : Facade.GeneralNumber.GetCountNumberOfPhoneBook(Helper.GetGuid(smsSent.GroupGuid));

		//		if (Helper.GetLong(smsSent.UpRange) > countNumber)
		//			throw new Exception(Language.GetString("SendAdvancedGroupSmsUpRangeError"));

		//		smsSent.GroupGuid = !smsSent.GroupGuid.StartsWith("'") ? string.Format("'{0}'", smsSent.GroupGuid) : smsSent.GroupGuid;

		//		Dictionary<Business.Operators, int> operatorCountNumberDictionary = smsSent.TypeSend == (int)Business.SmsSendType.SendAdvancedGroupSms ?
		//																																				PhoneNumber.GetCountNumberOfOperators(smsSent.GroupGuid, Helper.GetLong(smsSent.DownRange), Helper.GetLong(smsSent.UpRange), false) :
		//																																				GeneralNumber.GetCountNumberOfOperators(smsSent.GroupGuid, Helper.GetLong(smsSent.DownRange), Helper.GetLong(smsSent.UpRange));

		//		DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
		//		if (dtNumberInfo.Rows.Count == 0)
		//			throw new Exception(Language.GetString("BlankSenderNumber"));

		//		Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
		//																																													dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

		//		Transaction.DecreaseCostOfSmsSend(smsSent.UserGuid, (Business.SmsSendType)Enum.Parse(typeof(Business.SmsSendType), smsSent.TypeSend.ToString()),
		//																			operatorCountNumberDictionary, smsSent.SmsCount, smsType, agent, smsSent.SmsSentGuid, smsSentController.DataAccessProvider);

		//		if (smsSent.TypeSend == (int)Business.SmsSendType.SendAdvancedGroupSms)
		//		{
		//			if (!smsSentController.InsertSendAdvancedGroupSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent))
		//				throw new Exception(Language.GetString("ErrorRecord"));
		//		}
		//		else if (smsSent.TypeSend == (int)Business.SmsSendType.SendAdvancedSpecialGroupSms)
		//		{
		//			if (!smsSentController.InsertSendAdvancedSpecialGroupSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent))
		//				throw new Exception(Language.GetString("ErrorRecord"));
		//		}

		//		smsSentController.CommitTransaction();
		//		return true;
		//	}
		//	catch (InnerTransactionException ex)
		//	{
		//		throw ex;
		//	}
		//	catch (Exception ex)
		//	{
		//		smsSentController.RollbackTransaction();
		//		throw ex;
		//	}
		//}

		public static bool InsertSendFormatSms(Common.SmsSent smsSent, string selectedItems)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//Dictionary<Business.Operators, int> operatorCountNumberDictionary = new Dictionary<Operators, int>();
			//Business.SmsTypes smsType;

			//smsSentController.BeginTransaction();
			//try
			//{
			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																												dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	List<string> lstFormats = selectedItems.Split('$').ToList();
			//	foreach (string format in lstFormats)
			//	{
			//		smsSent.SmsSentGuid = Guid.NewGuid();
			//		smsSent.SmsFormatGuid = Helper.GetGuid(format.Split(';')[0]);
			//		List<string> lstGroupGuid = format.Split(';')[1].Split(',').ToList();
			//		lstGroupGuid = lstGroupGuid.Select<string, string>(s => !s.StartsWith("'") ? string.Format("'{0}'", s) : s).ToList();
			//		smsSent.GroupGuid = string.Join(",", lstGroupGuid);

			//		if (smsSent.GroupGuid == string.Empty)
			//			continue;

			//		DataTable dataTableSmsInfo = Facade.PhoneNumber.GetCountNumberOfOperatorsForSendSmsFormat(smsSent.SmsFormatGuid, smsSent.GroupGuid);

			//		foreach (DataRow row in dataTableSmsInfo.Rows)
			//		{

			//			operatorCountNumberDictionary.Add((Business.Operators)Helper.GetInt(row["Operator"]), Helper.GetInt(row["Count"]));
			//			smsType = Helper.GetInt(row["Encoding"]) == 1 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

			//			//Transaction.DecreaseCostOfSmsSend(smsSent.UserGuid, Business.SmsSendType.SendFormatSms,
			//			//																	operatorCountNumberDictionary,
			//			//																	Helper.GetInt(row["SmsPartCount"]),
			//			//																	smsType, agent, smsSent.SmsSentGuid, smsSentController.DataAccessProvider);
			//			operatorCountNumberDictionary.Clear();
			//		}


			//		if (!smsSentController.InsertSendFormatSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent))
			//			throw new Exception("ErrorRecord");
			//	}

			//	smsSentController.CommitTransaction();
			//	return true;
			//}
			//catch (InnerTransactionException ex)
			//{
			//	throw ex;
			//}
			//catch (Exception ex)
			//{
			//	smsSentController.RollbackTransaction();
			//	throw ex;
			//}
			return false;
		}

		public static bool InsertSendSmsForUsers(Common.SmsSent smsSent, DataTable dataTableSaveFile, ref int countValidDate, ref int countInvalidDate)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//smsSent.SmsSentGuid = Guid.NewGuid();

			//Business.SmsTypes smsType = smsSent.Encoding == (int)Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

			//smsSentController.BeginTransaction();
			//try
			//{
			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																											dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	smsSent.SmsBody = SerializeXmlSmsBodyForSendUsers(dataTableSaveFile, ref countValidDate, ref countInvalidDate);
			//	if (smsSent.SmsBody == "")
			//		return false;

			//	if (!smsSentController.InsertSendSmsForUsers(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent))
			//		throw new Exception(Language.GetString("ErrorRecord"));

			//	smsSentController.CommitTransaction();
			//	return true;
			//}
			//catch (Exception ex)
			//{
			//	smsSentController.RollbackTransaction();
			//	throw ex;
			//}
			return false;
		}

		public static bool InsertSendSingleFutureSms(Common.SmsSent smsSent, List<string> lstNumbers, ref List<string> lstFailedNumber)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//smsSent.SmsSentGuid = Guid.NewGuid();
			//try
			//{
			//	if (smsSent.DateTimeFuture < DateTime.Now)
			//		throw new Exception(Language.GetString("SendDateTimeIncorrect"));

			//	GetCountNumberOfOperators(ref lstNumbers, ref lstFailedNumber);
			//	smsSent.Reciever = string.Join(",", lstNumbers.ToArray());

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																											dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	if (!smsSentController.InsertSendSingleFutureSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent))
			//		throw new Exception(Language.GetString("ErrorRecord"));

			//	return true;
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		public static bool InsertSendGroupFutureSms(Common.SmsSent smsSent)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	if (smsSent.GroupGuid == string.Empty)
			//		throw new Exception(Language.GetString("SelectGroupError"));

			//	if (smsSent.DateTimeFuture < DateTime.Now)
			//		throw new Exception(Language.GetString("SendDateTimeIncorrect"));

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																											dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	return smsSentController.InsertSendGroupFutureSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent) != Guid.Empty ? true : false;
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		//public static bool InsertSendAdvancedGroupFutureSms(Common.SmsSent smsSent)
		//{
		//	Business.SmsSent smsSentController = new Business.SmsSent();
		//	try
		//	{
		//		if (smsSent.GroupGuid == string.Empty)
		//			throw new Exception(Language.GetString("SelectGroupError"));

		//		if (smsSent.DateTimeFuture < DateTime.Now)
		//			throw new Exception(Language.GetString("SendDateTimeIncorrect"));

		//		long countNumber = smsSent.TypeSend == (int)Business.SmsSendType.SendAdvancedGroupFutureSms ?
		//											 Facade.PhoneNumber.GetCountNumberOfPhoneBook(Helper.GetGuid(smsSent.GroupGuid)) : Facade.GeneralNumber.GetCountNumberOfPhoneBook(Helper.GetGuid(smsSent.GroupGuid));

		//		if (Helper.GetLong(smsSent.UpRange) > countNumber)
		//			throw new Exception(Language.GetString("SendAdvancedGroupSmsUpRangeError"));

		//		smsSent.GroupGuid = !smsSent.GroupGuid.StartsWith("'") ? string.Format("'{0}'", smsSent.GroupGuid) : smsSent.GroupGuid;

		//		DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
		//		if (dtNumberInfo.Rows.Count == 0)
		//			throw new Exception(Language.GetString("BlankSenderNumber"));

		//		Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
		//																																													dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

		//		return smsSentController.InsertSendAdvancedGroupFutureSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent) != Guid.Empty ? true : false;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw ex;
		//	}
		//}

		public static bool InsertSendBirthDateSms(Common.SmsSent smsSent)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	if (smsSent.GroupGuid == string.Empty)
			//		throw new Exception(Language.GetString("SelectGroupError"));

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																												dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	return smsSentController.InsertSendBirthDateSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent) != Guid.Empty ? true : false;

			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		public static bool InsertSendFutureSmsFormat(Common.SmsSent smsSent, string selectedItems)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//smsSentController.BeginTransaction();
			//try
			//{
			//	if (smsSent.DateTimeFuture < DateTime.Now)
			//		throw new Exception(Language.GetString("SendDateTimeIncorrect"));

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																												dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	List<string> lstFormats = selectedItems.Split('$').ToList();
			//	foreach (string format in lstFormats)
			//	{
			//		smsSent.SmsFormatGuid = Helper.GetGuid(format.Split(';')[0]);
			//		List<string> lstGroupGuid = format.Split(';')[1].Split(',').ToList();
			//		lstGroupGuid = lstGroupGuid.Select<string, string>(s => !s.StartsWith("'") ? string.Format("'{0}'", s) : s).ToList();
			//		smsSent.GroupGuid = string.Join(",", lstGroupGuid);

			//		if (smsSent.GroupGuid == string.Empty)
			//			continue;

			//		if (smsSentController.InsertSendFutureSmsFormat(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent) == Guid.Empty)
			//			throw new Exception("ErrorRecord");
			//	}

			//	smsSentController.CommitTransaction();
			//	return true;
			//}
			//catch (Exception ex)
			//{
			//	smsSentController.RollbackTransaction();
			//	throw ex;
			//}
			return false;
		}

		public static bool InsertSendPeriodFutureSingleSms(Common.SmsSent smsSent, List<string> lstNumbers, ref List<string> lstFailedNumber)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	bool isPeriodCorrect = smsSent.EndDateTime >= Helper.AddDate(((Business.SmsSentPeriodType)smsSent.PeriodType).ToString(), smsSent.Period, smsSent.StartDateTime);
			//	if (!isPeriodCorrect)
			//		throw new Exception(Language.GetString("EnteredPeriodIsNotCorrect"));

			//	if (smsSent.EndDateTime < DateTime.Now || smsSent.StartDateTime < DateTime.Now)
			//		throw new Exception(Language.GetString("EnteredPeriodIsNotCorrect"));

			//	GetCountNumberOfOperators(ref lstNumbers, ref lstFailedNumber);
			//	smsSent.Reciever = string.Join(",", lstNumbers.ToArray());

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																											dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	if (!smsSentController.InsertSendPeriodFutureSingleSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent))
			//		throw new Exception(Language.GetString("ErrorRecord"));

			//	return true;
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		public static bool InsertSendPeriodFutureGroupSms(Common.SmsSent smsSent)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	if (smsSent.GroupGuid == string.Empty)
			//		throw new Exception(Language.GetString("SelectGroupError"));

			//	bool isPeriodCorrect = smsSent.EndDateTime >= Helper.AddDate(((Business.SmsSentPeriodType)smsSent.PeriodType).ToString(), smsSent.Period, smsSent.StartDateTime);
			//	if (!isPeriodCorrect)
			//		throw new Exception(Language.GetString("EnteredPeriodIsNotCorrect"));

			//	if (smsSent.EndDateTime < DateTime.Now || smsSent.StartDateTime < DateTime.Now)
			//		throw new Exception(Language.GetString("EnteredPeriodIsNotCorrect"));

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																												dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	return smsSentController.InsertSendPeriodFutureGroupSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent);
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		public static bool InsertSendFutureSmsForUsers(Common.SmsSent smsSent)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	if (smsSent.DateTimeFuture < DateTime.Now)
			//		throw new Exception(Language.GetString("SendDateTimeIncorrect"));

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																											dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	return smsSentController.InsertSendFutureSmsForUsers(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent);
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		public static bool InsertBulk(Common.SmsSent smsSent, string recipients)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																											dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	int countRecipients = Helper.GetInt(Helper.ImportData(recipients, "resultCount"));
			//	int recipientNumberCount = 0;
			//	for (int counterRecipients = 0; counterRecipients < countRecipients; counterRecipients++)
			//		recipientNumberCount += Helper.GetInt(Helper.ImportData(recipients, ("Count" + counterRecipients).ToString()).Replace(",", string.Empty));
			//	smsSent.RecipientsNumberCount = recipientNumberCount;

			//	XDocument doc = new XDocument();
			//	XElement root = new XElement("NewDataSet");

			//	for (int counterRecipients = 0; counterRecipients < countRecipients; counterRecipients++)
			//	{
			//		XElement element = new XElement("Table");
			//		element.Add(new XElement("RecipientID", string.Empty));
			//		element.Add(new XElement("Province", Helper.ImportData(recipients, ("Province" + counterRecipients).ToString())));
			//		element.Add(new XElement("ProvinceID", Helper.GetInt(Helper.ImportData(recipients, ("ProvinceID" + counterRecipients).ToString()))));
			//		element.Add(new XElement("City", Helper.ImportData(recipients, ("City" + counterRecipients).ToString())));
			//		element.Add(new XElement("CityID", Helper.GetInt(Helper.ImportData(recipients, ("CityID" + counterRecipients).ToString()))));
			//		element.Add(new XElement("Prefix", Helper.ImportData(recipients, ("Prefix" + counterRecipients).ToString())));
			//		element.Add(new XElement("PostCode", Helper.ImportData(recipients, ("PostCode" + counterRecipients).ToString())));
			//		element.Add(new XElement("NumberRecipientType", Helper.GetInt(Helper.ImportData(recipients, ("NumberRecipientType" + counterRecipients).ToString()))));
			//		element.Add(new XElement("RecipientType", Helper.GetInt(Helper.ImportData(recipients, ("RecipientType" + counterRecipients).ToString()))));
			//		element.Add(new XElement("FromIndex", Helper.GetInt(Helper.ImportData(recipients, ("FromIndex" + counterRecipients).ToString()))));
			//		element.Add(new XElement("Count", Helper.GetInt(Helper.ImportData(recipients, ("Count" + counterRecipients).ToString()).Replace(",", string.Empty))));
			//		root.Add(element);
			//	}
			//	doc.Add(root);

			//	//return smsSentController.InsertBulk(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent, doc.ToString()) != Guid.Empty ? true : false;
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}
		#endregion

		#region UpdateMethod
		public static bool UpdateSendSingleFutureSms(Common.SmsSent smsSent, List<string> lstNumbers, ref List<string> lstFailedNumber)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	if (smsSent.DateTimeFuture < DateTime.Now)
			//		throw new Exception(Language.GetString("SendDateTimeIncorrect"));

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																											dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	GetCountNumberOfOperators(ref lstNumbers, ref lstFailedNumber);
			//	smsSent.Reciever = string.Join(",", lstNumbers.ToArray());

			//	return smsSentController.UpdateSendSingleFutureSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent);
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		public static bool UpdateSendGroupFutureSms(Common.SmsSent smsSent)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	if (smsSent.GroupGuid == string.Empty)
			//		throw new Exception(Language.GetString("SelectGroupError"));

			//	if (smsSent.DateTimeFuture < DateTime.Now)
			//		throw new Exception(Language.GetString("SendDateTimeIncorrect"));

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																											dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	return smsSentController.UpdateSendGroupFutureSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent);
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		//public static bool UpdateSendAdvancedGroupFutureSms(Common.SmsSent smsSent)
		//{
		//	Business.SmsSent smsSentController = new Business.SmsSent();
		//	try
		//	{
		//		if (smsSent.GroupGuid == string.Empty)
		//			throw new Exception(Language.GetString("SelectGroupError"));

		//		if (smsSent.DateTimeFuture < DateTime.Now)
		//			throw new Exception(Language.GetString("SendDateTimeIncorrect"));

		//		long countNumber = smsSent.TypeSend == (int)Business.SmsSendType.SendAdvancedGroupFutureSms ?
		//											 Facade.PhoneNumber.GetCountNumberOfPhoneBook(Helper.GetGuid(smsSent.GroupGuid)) : Facade.GeneralNumber.GetCountNumberOfPhoneBook(Helper.GetGuid(smsSent.GroupGuid));

		//		if (Helper.GetLong(smsSent.UpRange) > countNumber)
		//			throw new Exception(Language.GetString("SendAdvancedGroupSmsUpRangeError"));

		//		smsSent.GroupGuid = !smsSent.GroupGuid.StartsWith("'") ? string.Format("'{0}'", smsSent.GroupGuid) : smsSent.GroupGuid;

		//		DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
		//		if (dtNumberInfo.Rows.Count == 0)
		//			throw new Exception(Language.GetString("BlankSenderNumber"));

		//		Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
		//																																													dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());


		//		return smsSentController.UpdateSendAdvancedGroupFutureSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent);

		//	}
		//	catch (Exception ex)
		//	{
		//		throw ex;
		//	}
		//}

		public static bool UpdateSendBirthDateSms(Common.SmsSent smsSent)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	if (smsSent.GroupGuid == string.Empty)
			//		throw new Exception(Language.GetString("SelectGroupError"));

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																											dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	return smsSentController.UpdateSendBirthDateSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent);
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		public static bool UpdateSendFutureSmsFormat(Common.SmsSent smsSent, string selectedItems)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	if (smsSent.DateTimeFuture < DateTime.Now)
			//		throw new Exception(Language.GetString("SendDateTimeIncorrect"));

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																												dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	List<string> lstFormats = selectedItems.Split('$').ToList();
			//	foreach (string format in lstFormats)
			//	{
			//		if (smsSent.SmsFormatGuid != Helper.GetGuid(format.Split(';')[0]))
			//			continue;

			//		List<string> lstGroupGuid = format.Split(';')[1].Split(',').ToList();
			//		lstGroupGuid = lstGroupGuid.Select<string, string>(s => !s.StartsWith("'") ? string.Format("'{0}'", s) : s).ToList();
			//		smsSent.GroupGuid = string.Join(",", lstGroupGuid);

			//		if (smsSent.GroupGuid == string.Empty)
			//			throw new Exception(Language.GetString("SelectGroupError"));

			//		if (!smsSentController.UpdateSendFutureSmsFormat(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent))
			//			throw new Exception("ErrorRecord");
			//	}

			//	return true;
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		public static bool UpdateSendPeriodFutureSingleSms(Common.SmsSent smsSent, List<string> lstNumbers, ref List<string> lstFailedNumber)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	bool isPeriodCorrect = smsSent.EndDateTime >= Helper.AddDate(((Business.SmsSentPeriodType)smsSent.PeriodType).ToString(), smsSent.Period, smsSent.StartDateTime);
			//	if (!isPeriodCorrect)
			//		throw new Exception(Language.GetString("EnteredPeriodIsNotCorrect"));

			//	if (smsSent.EndDateTime < DateTime.Now)
			//		throw new Exception(Language.GetString("EnteredPeriodIsNotCorrect"));

			//	GetCountNumberOfOperators(ref lstNumbers, ref lstFailedNumber);
			//	smsSent.Reciever = string.Join(",", lstNumbers.ToArray());

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																											dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	return smsSentController.UpdateSendPeriodFutureSingleSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent);
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		public static bool UpdateSendPeriodFutureGroupSms(Common.SmsSent smsSent)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	if (smsSent.GroupGuid == string.Empty)
			//		throw new Exception(Language.GetString("SelectGroupError"));

			//	bool isPeriodCorrect = smsSent.EndDateTime >= Helper.AddDate(((Business.SmsSentPeriodType)smsSent.PeriodType).ToString(), smsSent.Period, smsSent.StartDateTime);
			//	if (!isPeriodCorrect)
			//		throw new Exception(Language.GetString("EnteredPeriodIsNotCorrect"));

			//	if (smsSent.EndDateTime < DateTime.Now)
			//		throw new Exception(Language.GetString("EnteredPeriodIsNotCorrect"));

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																											dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	return smsSentController.UpdateSendPeriodFutureGroupSms(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent);
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		public static bool UpdateSendFutureSmsForUsers(Common.SmsSent smsSent)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	if (smsSent.DateTimeFuture < DateTime.Now)
			//		throw new Exception(Language.GetString("SendDateTimeIncorrect"));

			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																											dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	return smsSentController.UpdateSendFutureSmsForUsers(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent);

			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		public static bool UpdateSendSmsForUsers(Common.SmsSent smsSent, DataTable dataTableSaveFile, ref int countValidDate, ref int countInvalidDate)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																											dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	smsSent.SmsBody = SerializeXmlSmsBodyForSendUsers(dataTableSaveFile, ref countValidDate, ref countInvalidDate);

			//	return smsSentController.UpdateSendSmsForUsers(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent);

			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		public static bool UpdateBulk(Common.SmsSent smsSent, string recipients)
		{
			//Business.SmsSent smsSentController = new Business.SmsSent();
			//try
			//{
			//	DataTable dtNumberInfo = Facade.UserPrivateNumber.GetAgentOfUserPrivateNumber(smsSent.PrivateNumberGuid);
			//	if (dtNumberInfo.Rows.Count == 0)
			//		throw new Exception(Language.GetString("BlankSenderNumber"));

			//	Business.SmsSenderAgentReference agent = (Business.SmsSenderAgentReference)Enum.Parse(typeof(Business.SmsSenderAgentReference),
			//																																											dtNumberInfo.Rows[0]["SmsSenderAgentReference"].ToString());

			//	int countRecipients = Helper.GetInt(Helper.ImportData(recipients, "resultCount"));
			//	int recipientNumberCount = 0;
			//	for (int counterRecipients = 0; counterRecipients < countRecipients; counterRecipients++)
			//		recipientNumberCount += Helper.GetInt(Helper.ImportData(recipients, ("Count" + counterRecipients).ToString()).Replace(",", string.Empty));
			//	smsSent.RecipientsNumberCount = recipientNumberCount;

			//	XDocument doc = new XDocument();
			//	XElement root = new XElement("NewDataSet");

			//	for (int counterRecipients = 0; counterRecipients < countRecipients; counterRecipients++)
			//	{
			//		XElement element = new XElement("Table");
			//		element.Add(new XElement("RecipientID", string.Empty));
			//		element.Add(new XElement("Province", Helper.ImportData(recipients, ("Province" + counterRecipients).ToString())));
			//		element.Add(new XElement("ProvinceID", Helper.GetInt(Helper.ImportData(recipients, ("ProvinceID" + counterRecipients).ToString()))));
			//		element.Add(new XElement("City", Helper.ImportData(recipients, ("City" + counterRecipients).ToString())));
			//		element.Add(new XElement("CityID", Helper.GetInt(Helper.ImportData(recipients, ("CityID" + counterRecipients).ToString()))));
			//		element.Add(new XElement("Prefix", Helper.ImportData(recipients, ("Prefix" + counterRecipients).ToString())));
			//		element.Add(new XElement("PostCode", Helper.ImportData(recipients, ("PostCode" + counterRecipients).ToString())));
			//		element.Add(new XElement("NumberRecipientType", Helper.GetInt(Helper.ImportData(recipients, ("NumberRecipientType" + counterRecipients).ToString()))));
			//		element.Add(new XElement("RecipientType", Helper.GetInt(Helper.ImportData(recipients, ("RecipientType" + counterRecipients).ToString()))));
			//		element.Add(new XElement("FromIndex", Helper.GetInt(Helper.ImportData(recipients, ("FromIndex" + counterRecipients).ToString()))));
			//		element.Add(new XElement("Count", Helper.GetInt(Helper.ImportData(recipients, ("Count" + counterRecipients).ToString()).Replace(",", string.Empty))));
			//		root.Add(element);
			//	}
			//	doc.Add(root);

			//	return smsSentController.UpdateBulk(smsSent, Helper.GetGuid(dtNumberInfo.Rows[0]["Guid"]), agent, doc.ToString());
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
			return false;
		}

		public static void UpdateSmsSentToFailedState(string xmlInfo)
		{
			Business.SmsSent smsSentController = new Business.SmsSent();
			smsSentController.UpdateState(xmlInfo);
		}

		public static bool RejectSms(Guid smsSentGuid, Business.SmsSentStates smsSentState, Business.SmsSendFailedType failedType, string errorMessage)
		{
			Business.SmsSent smsSentController = new Business.SmsSent();
			return smsSentController.RejectSms(smsSentGuid, smsSentState, failedType, errorMessage);
		}
		#endregion

		public static bool InsertReplySmsForSmsParser(Common.SmsSent smsSent)
		{
			Business.SmsSent smsSentController = new Business.SmsSent();
			return false;//smsSentController.InsertReplySmsForSmsParser(smsSent) != Guid.Empty ? true : false;
		}

		public static bool InsertSendSmsDateField(Common.SmsSent smsSent)
		{
			Business.SmsSent smsSentController = new Business.SmsSent();
			return false;//smsSentController.InsertSendSmsDateField(smsSent);
		}

		public static bool UpdateSendSmsDateField(Common.SmsSent smsSent)
		{
			Business.SmsSent smsSentController = new Business.SmsSent();
			return false;//smsSentController.UpdateSendSmsDateField(smsSent);
		}

		public static void UpdateBulkState(Guid guid, Business.SmsSentStates state, Business.SmsSendFailedType faildType)
		{
			Business.SmsSent smsSentController = new Business.SmsSent();
			smsSentController.UpdateBulkState(guid, state, faildType);
		}

		public static void UpdateBulkID(Guid bulkGuid, string bulkID)
		{
			Business.SmsSent smsSentController = new Business.SmsSent();
			smsSentController.UpdateBulkID(bulkGuid, bulkID);
		}

		public static DataTable GetChartDetailsAtSpecificDate(Guid userGuid, DateTime fromDateTime, DateTime toDateTime, Business.SmsSentStates state, int pageNo, int pageSize, ref int rowCount)
		{
			Business.SmsSent smsSentController = new Business.SmsSent();
			return smsSentController.GetChartDetailsAtSpecificDate(userGuid, fromDateTime, toDateTime, state, pageNo, pageSize, ref rowCount);
		}

		public static DataTable GetUncertainDeleviryStatusBulk(Business.SmsSenderAgentReference smsSenderAgentRefrence)
		{
			Business.SmsSent smsSentController = new Business.SmsSent();
			return smsSentController.GetUncertainDeleviryStatusBulk(smsSenderAgentRefrence);
		}

		public static Common.SmsSent LoadBulk(Guid bulkGuid)
		{
			Business.SmsSent smsSentController = new Business.SmsSent();
			Common.SmsSent smsSent = new Common.SmsSent();
			smsSentController.Load(bulkGuid, smsSent);
			return smsSent;
		}

		public static Common.SmsSent Load(Guid guid)
		{
			Business.SmsSent smsSentController = new Business.SmsSent();
			Common.SmsSent smsSent = new Common.SmsSent();
			smsSentController.Load(guid, smsSent);
			return smsSent;
		}

		public static void SendSmsForUser(Guid userGuid, string smsBody)
		{
			//Common.User user = new Common.User();
			//Common.SmsSent smsSent = new Common.SmsSent();
			//user = Facade.User.LoadUser(userGuid);
			////smsSent.TypeSend = (int)Business.SmsSendType.SendSingleSms;
			//smsSent.CreateDate = DateTime.Now;
			//smsSent.UserGuid = userGuid;
			//smsSent.SmsBody = smsBody;
			//smsSent.SmsCount = Helper.GetSmsCount(smsSent.SmsBody);
			//smsSent.PresentType = (int)Business.Messageclass.Normal;
			//smsSent.PrivateNumberGuid = Helper.GetGuid(Facade.UserPrivateNumber.GetUserPrivateNumbersForSend(userGuid, user.ParentGuid).Rows[0]["NumberGuid"]);
			//smsSent.Encoding = Helper.HasUniCodeCharacter(smsSent.SmsBody) ? (int)Business.Encoding.Utf8 : (int)Business.Encoding.Default;
			//smsSent.State = (int)Business.SmsSentStates.Pending;
			//smsSent.Reciever = user.CellPhone;
			//InsertSendSingleSms(smsSent);
		}

		private static string SerializeXmlSmsBodyForSendUsers(DataTable dataTableSaveFile, ref int countValidDate, ref int countInvalidDate)
		{
			int countRecipients = dataTableSaveFile.Rows.Count;
			XmlSerializer ser = new XmlSerializer(typeof(XmlDocument));
			XmlDocument xmldoc = new XmlDocument();
			XmlDeclaration decl = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", "");
			xmldoc.InsertBefore(decl, xmldoc.DocumentElement);
			XmlElement RootNode = xmldoc.CreateElement("Smses");
			xmldoc.AppendChild(RootNode);

			for (int counterFileRow = 0; counterFileRow < countRecipients; counterFileRow++)
			{
				try
				{
					string SendDate = DateManager.GetChristianDateTime(dataTableSaveFile.Rows[counterFileRow][2].ToString()).ToString();
					XmlNode productNode = xmldoc.CreateElement("Sms");
					XmlAttribute productAttribute = xmldoc.CreateAttribute("id");
					productAttribute.Value = counterFileRow.ToString();
					productNode.Attributes.Append(productAttribute);
					RootNode.AppendChild(productNode);

					XmlNode GuidNode = xmldoc.CreateElement("Guid");
					GuidNode.AppendChild(xmldoc.CreateTextNode(Guid.NewGuid().ToString()));
					productNode.AppendChild(GuidNode);
					XmlNode SmsBody = xmldoc.CreateElement("SmsBody");
					SmsBody.AppendChild(xmldoc.CreateTextNode(dataTableSaveFile.Rows[counterFileRow][1].ToString()));
					productNode.AppendChild(SmsBody);
					XmlNode smsDate = xmldoc.CreateElement("SendDate");

					smsDate.AppendChild(xmldoc.CreateTextNode(SendDate));
					countValidDate++;
					productNode.AppendChild(smsDate);
				}
				catch
				{
					countInvalidDate++;
				}
			}
			if (countRecipients > countInvalidDate)
			{
				StringWriter writer = new StringWriter();
				ser.Serialize(writer, xmldoc);
				writer.Close();
				return xmldoc.InnerXml;
			}
			else
				return "";
		}

	}
}