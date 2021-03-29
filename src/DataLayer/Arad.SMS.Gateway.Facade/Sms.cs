using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using Business;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using Common.Exceptions;

namespace Facade
{
	public class Sms : FacadeEntityBase
	{
		#region SelectMethod
		public static Dictionary<Business.Operators, int> GetCountNumberOfOperators(List<string> lstNumbers)
		{
			try
			{
				Dictionary<Business.Operators, int> operatorCountNumberDictionary = new Dictionary<Business.Operators, int>();

				if (lstNumbers.Count == 0)
					throw new Exception("CompleteRecieverNumberField");

				int countMCI = lstNumbers.Where(reciever => reciever.StartsWith("+9891") || reciever.StartsWith("9891") || reciever.StartsWith("091")).Count();
				int countIrancell = lstNumbers.Where(reciever => reciever.StartsWith("+9893") || reciever.StartsWith("9893") || reciever.StartsWith("093")).Count();
				int countRightel = lstNumbers.Where(reciever => reciever.StartsWith("+9892") || reciever.StartsWith("9892") || reciever.StartsWith("092")).Count();

				operatorCountNumberDictionary.Add(Operators.MCI, countMCI);
				operatorCountNumberDictionary.Add(Operators.Irancell, countIrancell);
				operatorCountNumberDictionary.Add(Operators.Rightel, countRightel);

				return operatorCountNumberDictionary;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static List<long> GetUncertainDeleviryStatusSms(Business.SmsSenderAgentReference smsSenderAgentRefrence)
		{
			Business.Sms smsController = new Business.Sms();
			List<long> smsesInfoList = new List<long>();
			DataTable dtSmses = smsController.GetUncertainDeleviryStatusSms(smsSenderAgentRefrence);
			foreach (DataRow item in dtSmses.Rows)
			{
				smsesInfoList.Add(Helper.GetLong(item["OuterSystemSmsID"]));
				smsesInfoList.Add(Helper.GetLong(item["DeliveryStatus"]));
			}
			return smsesInfoList;
		}

		public static DataTable GetUncertainDeleviryStatusSmsTable(Business.SmsSenderAgentReference smsSenderAgentRefrence)
		{
			Business.Sms smsController = new Business.Sms();
			List<long> smsesInfoList = new List<long>();
			return smsController.GetUncertainDeleviryStatusSms(smsSenderAgentRefrence);
		}

		public static DataTable GetGiveBackCreditSms(SmsSenderAgentReference smsSenderAgentReference)
		{
			Business.Sms smsController = new Business.Sms();
			return smsController.GetGiveBackCreditSms(smsSenderAgentReference);
		}
		#endregion

		#region UpdateMethod
		public static bool UpdateSmsSendInfo(string xmlSmsInfo)
		{
			Business.Sms smsController = new Business.Sms();
			return smsController.UpdateSmsSendInfo(xmlSmsInfo);
		}

		public static void GiveBackCreditToUser(Guid userGuid, SmsSendType smsSendType, Business.Operators operators,
																						int smsPartCount, int encoding,
																						int smsCount, Business.SmsSenderAgentReference agent, Guid smsSentGuid)
		{
			Business.Sms smsController = new Business.Sms();

			Business.SmsTypes smsType = encoding == (int)Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

			smsController.BeginTransaction();
			try
			{
				Transaction.IncreaseCostOfFaildSend(userGuid, smsSendType, operators, smsPartCount, smsType, smsCount, agent, smsSentGuid, smsController.DataAccessProvider);

				if (!smsController.UpdateBlackListSms(userGuid, smsSentGuid, agent, operators, smsPartCount, smsSendType, encoding))
					throw new Exception("ErrorRecord");
				smsController.CommitTransaction();

			}
			catch (InnerTransactionException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				smsController.RollbackTransaction();
				throw ex;
			}
		}
		#endregion

		#region SendFutureSms
		public static bool SendSingleFutureSms(SmsSenderAgentReference smsSenderAgentRefrence, Guid userGuid, Guid smsSentGuid,
																					 Guid sentboxGuid, string reciever, int smsPartCount, Encoding encoding)
		{
			Business.Sms smsController = new Business.Sms();
			Business.SmsTypes smsType = encoding == Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

			smsController.BeginTransaction();
			try
			{
				List<string> lstNumbers = reciever.Split(',').ToList();
				Dictionary<Business.Operators, int> operatorCountNumberDictionary = GetCountNumberOfOperators(lstNumbers);

				//Transaction.DecreaseCostOfSmsSend(userGuid, Business.SmsSendType.SendSms,
				//																	operatorCountNumberDictionary, smsPartCount, smsType, smsSenderAgentRefrence, smsSentGuid, smsController.DataAccessProvider);

				XDocument doc = new XDocument();
				XElement root = new XElement("NewDataSet");

				foreach (string number in lstNumbers)
				{
					XElement element = new XElement("Table");
					element.Add(new XElement("Reciever", number));
					root.Add(element);
				}
				doc.Add(root);

				if (!smsController.SendSingleFutureSms(sentboxGuid, doc.ToString()))
					throw new Exception(Language.GetString("ErrorRecord"));

				smsController.CommitTransaction();
				return true;
			}
			catch (InnerTransactionException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				smsController.RollbackTransaction();
				throw ex;
			}
		}

		public static bool SendGroupFutureSms(SmsSenderAgentReference smsSenderAgentRefrence, Guid userGuid, Guid smsSentGuid,
																					Guid sentboxGuid, string groupGuid, int smsPartCount, Encoding encoding)
		{
			Business.Sms smsController = new Business.Sms();
			Business.SmsTypes smsType = encoding == Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;
			smsController.BeginTransaction();
			try
			{
				Dictionary<Business.Operators, int> operatorCountNumberDictionary = PhoneNumber.GetCountNumberOfOperators(groupGuid, 0, 0, false);

				//Transaction.DecreaseCostOfSmsSend(userGuid, Business.SmsSendType.SendGroupFutureSms,
				//																	operatorCountNumberDictionary, smsPartCount, smsType, smsSenderAgentRefrence, smsSentGuid, smsController.DataAccessProvider);

				if (!smsController.SendGroupFutureSms(sentboxGuid, groupGuid))
					throw new Exception(Language.GetString("ErrorRecord"));

				smsController.CommitTransaction();
				return true;
			}
			catch (InnerTransactionException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				smsController.RollbackTransaction();
				throw ex;
			}
		}

		public static bool SendBirthDateSms(SmsSenderAgentReference smsSenderAgentRefrence, Guid userGuid, Guid smsSentGuid,
																				Guid sentboxGuid, string groupGuid, int smsPartCount, Encoding encoding)
		{
			Business.Sms smsController = new Business.Sms();
			Business.SmsTypes smsType = encoding == Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

			smsController.BeginTransaction();
			try
			{
				Dictionary<Business.Operators, int> operatorCountNumberDictionary = PhoneNumber.GetCountNumberOfOperators(groupGuid, 0, 0, true);

				//Transaction.DecreaseCostOfSmsSend(userGuid, Business.SmsSendType.SendBirthDateSms,
				//																	operatorCountNumberDictionary, smsPartCount, smsType, smsSenderAgentRefrence, smsSentGuid, smsController.DataAccessProvider);

				if (!smsController.SendBirthDateSms(sentboxGuid, groupGuid))
					throw new Exception(Language.GetString("ErrorRecord"));

				smsController.CommitTransaction();
				return true;
			}
			catch (InnerTransactionException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				smsController.RollbackTransaction();
				throw ex;
			}
		}

		public static bool SendAdvancedGroupFutureSms(SmsSenderAgentReference smsSenderAgentRefrence, Guid userGuid, Guid smsSentGuid,
																									Guid sentboxGuid, string groupGuid, int downRange, int upRange, int smsPartCount, Encoding encoding)
		{
			Business.Sms smsController = new Business.Sms();
			Business.SmsTypes smsType = encoding == Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

			smsController.BeginTransaction();
			try
			{
				Dictionary<Business.Operators, int> operatorCountNumberDictionary = PhoneNumber.GetCountNumberOfOperators(groupGuid, downRange, upRange, false);

				//Transaction.DecreaseCostOfSmsSend(userGuid, Business.SmsSendType.SendAdvancedGroupFutureSms,
				//																	operatorCountNumberDictionary, smsPartCount, smsType, smsSenderAgentRefrence, smsSentGuid, smsController.DataAccessProvider);

				if (!smsController.SendAdvancedGroupFutureSms(sentboxGuid, groupGuid, downRange, upRange))
					throw new Exception(Language.GetString("ErrorRecord"));

				smsController.CommitTransaction();
				return true;
			}
			catch (InnerTransactionException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				smsController.RollbackTransaction();
				throw ex;
			}
		}

		public static bool SendSpecialGroupFutureSms(SmsSenderAgentReference smsSenderAgentRefrence, Guid userGuid, Guid smsSentGuid,
																								 Guid sentboxGuid, string groupGuid, int smsPartCount, Encoding encoding)
		{
			Business.Sms smsController = new Business.Sms();
			Business.SmsTypes smsType = encoding == Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;
			smsController.BeginTransaction();
			try
			{
				Dictionary<Business.Operators, int> operatorCountNumberDictionary = GeneralNumber.GetCountNumberOfOperators(groupGuid, 0, 0);

				//Transaction.DecreaseCostOfSmsSend(userGuid, Business.SmsSendType.SendSpecialGroupFutureSms,
				//																	operatorCountNumberDictionary, smsPartCount, smsType, smsSenderAgentRefrence, smsSentGuid, smsController.DataAccessProvider);

				if (!smsController.SendSpecialGroupFutureSms(sentboxGuid, groupGuid))
					throw new Exception(Language.GetString("ErrorRecord"));

				smsController.CommitTransaction();
				return true;
			}
			catch (InnerTransactionException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				smsController.RollbackTransaction();
				throw ex;
			}
		}

		public static bool SendAdvancedSpecialGroupFutureSms(SmsSenderAgentReference smsSenderAgentRefrence, Guid userGuid, Guid smsSentGuid,
																												 Guid sentboxGuid, string groupGuid, int downRange, int upRange, int smsPartCount, Encoding encoding)
		{
			Business.Sms smsController = new Business.Sms();
			Business.SmsTypes smsType = encoding == Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

			smsController.BeginTransaction();
			try
			{
				Dictionary<Business.Operators, int> operatorCountNumberDictionary = GeneralNumber.GetCountNumberOfOperators(groupGuid, downRange, upRange);

				//Transaction.DecreaseCostOfSmsSend(userGuid, Business.SmsSendType.SendAdvancedSpecialGroupFutureSms,
				//																	operatorCountNumberDictionary, smsPartCount, smsType, smsSenderAgentRefrence, smsSentGuid, smsController.DataAccessProvider);

				if (!smsController.SendAdvancedSpecialGroupFutureSms(sentboxGuid, groupGuid, downRange, upRange))
					throw new Exception(Language.GetString("ErrorRecord"));

				smsController.CommitTransaction();
				return true;
			}
			catch (InnerTransactionException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				smsController.RollbackTransaction();
				throw ex;
			}
		}

		public static bool SendFutureSmsFormat(SmsSenderAgentReference smsSenderAgentRefrence, Guid userGuid, Guid smsSentGuid,
																					 Guid sentboxGuid, string groupGuid, Guid formatGuid, int smsPartCount, Encoding encoding)
		{
			Business.Sms smsController = new Business.Sms();
			Dictionary<Business.Operators, int> operatorCountNumberDictionary = new Dictionary<Operators, int>();
			Business.SmsTypes smsType;

			smsController.BeginTransaction();
			try
			{
				DataTable dataTableSmsInfo = Facade.PhoneNumber.GetCountNumberOfOperatorsForSendSmsFormat(formatGuid, groupGuid);

				foreach (DataRow row in dataTableSmsInfo.Rows)
				{
					operatorCountNumberDictionary.Add((Business.Operators)Helper.GetInt(row["Operator"]), Helper.GetInt(row["Count"]));
					smsType = Helper.GetInt(row["Encoding"]) == 1 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

					//Transaction.DecreaseCostOfSmsSend(userGuid, Business.SmsSendType.SendFutureSmsFormat,
					//																	operatorCountNumberDictionary,
					//																	Helper.GetInt(row["SmsPartCount"]),
					//																	smsType, smsSenderAgentRefrence, smsSentGuid, smsController.DataAccessProvider);
					operatorCountNumberDictionary.Clear();
				}

				if (!smsController.SendFutureSmsFormat(sentboxGuid, groupGuid, formatGuid))
					throw new Exception("ErrorRecord");

				smsController.CommitTransaction();
				return true;
			}
			catch (InnerTransactionException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				smsController.RollbackTransaction();
				throw ex;
			}
		}

		public static bool SendPeriodFutureSingleSms(SmsSenderAgentReference smsSenderAgentRefrence, Guid userGuid, Guid smsSentGuid, DateTime sendDateTime,
																								 Guid sentboxGuid, string reciever, DateTime startDateTime, DateTime endDateTime,
																								 int period, SmsSentPeriodType periodType, int smsPartCount, Encoding encoding)
		{
			Business.Sms smsController = new Business.Sms();
			Business.SmsTypes smsType = encoding == Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

			smsController.BeginTransaction();
			try
			{
				DateTime nextDateTime = Helper.AddDate(periodType.ToString(), period, sendDateTime);
				bool sendIsFinish = false;
				if (nextDateTime > endDateTime)
					sendIsFinish = true;

				List<string> lstNumbers = reciever.Split(',').ToList();
				Dictionary<Business.Operators, int> operatorCountNumberDictionary = GetCountNumberOfOperators(lstNumbers);

				//Transaction.DecreaseCostOfSmsSend(userGuid, Business.SmsSendType.SendPeriodFutureSingleSms,
				//																	operatorCountNumberDictionary, smsPartCount, smsType, smsSenderAgentRefrence, smsSentGuid, smsController.DataAccessProvider);

				XDocument doc = new XDocument();
				XElement root = new XElement("NewDataSet");

				foreach (string number in lstNumbers)
				{
					XElement element = new XElement("Table");
					element.Add(new XElement("Reciever", number));
					root.Add(element);
				}
				doc.Add(root);


				if (!smsController.SendPeriodFutureSingleSms(sentboxGuid, doc.ToString(), nextDateTime, sendIsFinish))
					throw new Exception(Language.GetString("ErrorRecord"));

				smsController.CommitTransaction();
				return true;
			}
			catch (InnerTransactionException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				smsController.RollbackTransaction();
				throw ex;
			}
		}

		public static bool SendPeriodFutureGroupSms(SmsSenderAgentReference smsSenderAgentRefrence, Guid userGuid, Guid smsSentGuid,
																								string groupGuid, DateTime sendDateTime, Guid sentboxGuid, DateTime startDateTime, DateTime endDateTime,
																								int period, SmsSentPeriodType periodType, int smsPartCount, Encoding encoding)
		{
			Business.Sms smsController = new Business.Sms();
			Business.SmsTypes smsType = encoding == Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;
			smsController.BeginTransaction();
			try
			{
				DateTime nextDateTime = Helper.AddDate(periodType.ToString(), period, sendDateTime);
				bool sendIsFinish = false;
				if (nextDateTime > endDateTime)
					sendIsFinish = true;

				Dictionary<Business.Operators, int> operatorCountNumberDictionary = PhoneNumber.GetCountNumberOfOperators(groupGuid, 0, 0, false);

				//Transaction.DecreaseCostOfSmsSend(userGuid, Business.SmsSendType.SendPeriodFutureGroupSms,
				//																	operatorCountNumberDictionary, smsPartCount, smsType, smsSenderAgentRefrence, smsSentGuid, smsController.DataAccessProvider);

				if (!smsController.SendPeriodFutureGroupSms(sentboxGuid, groupGuid, nextDateTime, sendIsFinish))
					throw new Exception(Language.GetString("ErrorRecord"));

				smsController.CommitTransaction();
				return true;
			}
			catch (InnerTransactionException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				smsController.RollbackTransaction();
				throw ex;
			}
		}

		public static bool SendFutureSmsForUsers(SmsSenderAgentReference smsSenderAgentRefrence, Guid userGuid,
																						 Guid smsSentGuid, Guid sentboxGuid, int smsPartCount, Encoding encoding)
		{
			//Business.Sms smsController = new Business.Sms();
			//Dictionary<Business.Operators, int> operatorCountNumberDictionary = User.GetCountUserNumberOfOperators(userGuid);

			//Business.SmsTypes smsType = encoding == Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

			//smsController.BeginTransaction();
			//try
			//{
			//  Transaction.DecreaseCostOfSmsSend(userGuid, Business.SmsSendType.SendFutureSmsForUsers, operatorCountNumberDictionary,
			//                                    smsPartCount, smsType, smsSenderAgentRefrence, smsSentGuid);

			//  if (!smsController.SendFutureSmsForUsers(sentboxGuid))
			//    throw new Exception(Language.GetString("ErrorRecord"));

			//  smsController.CommitTransaction();
			//  return true;
			//}
			//catch (Exception ex)
			//{
			//  smsController.RollbackTransaction();
			//  throw ex;
			//}
			return false;
		}

		public static void SendSmsForUsers(SmsSenderAgentReference smsSenderAgentRefrence, Guid userGuid,
																			 string userRoleGuid, Guid smsSentGuid, Guid sentboxGuid, string xmlSmsBody, bool decreaseFromUser, ref Guid SmsBodyGuid)
		{
			Business.Sms smsController = new Business.Sms();
			Business.SmsTypes smsType;
			string smsBody = string.Empty;

			try
			{
				DataView dvXmlSmsBody = new DataView(Helper.DeSerializeXml(xmlSmsBody));
				dvXmlSmsBody.RowFilter = string.Format(" CONVERT(SendDate,System.DateTime) <= '{0}' ", Helper.GetDateTimeForDB(DateTime.Now));

				for (int counterXmlNode = 0; counterXmlNode < dvXmlSmsBody.Count; counterXmlNode++)
				{
					smsController.BeginTransaction();
					try
					{
						smsBody = dvXmlSmsBody[counterXmlNode]["SmsBody"].ToString();
						SmsBodyGuid = Helper.GetGuid(dvXmlSmsBody[counterXmlNode]["Guid"]);
						if (!decreaseFromUser)
						{
							Dictionary<Business.Operators, int> operatorCountNumberDictionary = User.GetCountRoleNumberOfOperators(userGuid, Helper.GetGuid(userRoleGuid));
							smsType = Helper.HasUniCodeCharacter(smsBody) ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;
							//Transaction.DecreaseCostOfSmsSend(userGuid, Business.SmsSendType.SendSmsForUsers,
							//																	operatorCountNumberDictionary,
							//																	Helper.GetSmsCount(smsBody),
							//																	smsType, smsSenderAgentRefrence,
							//																	smsSentGuid, smsController.DataAccessProvider);
						}
						//smsController.SendSmsForUsers(sentboxGuid, Helper.GetGuid(dvXmlSmsBody[counterXmlNode]["Guid"]), Helper.GetGuid(userRoleGuid),
						//															decreaseFromUser, (int)Business.SmsSendType.SendSingleFutureSms);
						smsController.CommitTransaction();
					}
					catch (InnerTransactionException ex)
					{
						throw ex;
					}
					catch (Exception ex)
					{
						smsController.RollbackTransaction();
						throw ex;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool SendPrefixSms(SmsSenderAgentReference smsSenderAgentRefrence, Guid userGuid, Guid smsSentGuid,
																		 int recipientNumberCount, Guid sentboxGuid, int smsPartCount, Encoding encoding)
		{
			Business.Sms smsController = new Business.Sms();
			Dictionary<Business.Operators, int> operatorCountNumberDictionary = new Dictionary<Operators, int>();
			operatorCountNumberDictionary.Add(Operators.MCI, recipientNumberCount);

			Business.SmsTypes smsType = encoding == Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

			smsController.BeginTransaction();
			try
			{
				//Transaction.DecreaseCostOfSmsSend(userGuid, Business.SmsSendType.SendPrefixSms, operatorCountNumberDictionary,
				//																	smsPartCount, smsType, smsSenderAgentRefrence, smsSentGuid, smsController.DataAccessProvider);

				if (!smsController.SendPrefixSms(sentboxGuid))
					throw new Exception(Language.GetString("ErrorRecord"));

				smsController.CommitTransaction();
				return true;
			}
			catch (InnerTransactionException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				smsController.RollbackTransaction();
				throw ex;
			}
		}

		public static bool SendCitySms(SmsSenderAgentReference smsSenderAgentRefrence, Guid userGuid, Guid smsSentGuid,
																	 int recipientNumberCount, Guid sentboxGuid, int smsPartCount, Encoding encoding)
		{
			Business.Sms smsController = new Business.Sms();
			Dictionary<Business.Operators, int> operatorCountNumberDictionary = new Dictionary<Operators, int>();
			operatorCountNumberDictionary.Add(Operators.MCI, recipientNumberCount);

			Business.SmsTypes smsType = encoding == Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

			smsController.BeginTransaction();
			try
			{
				//Transaction.DecreaseCostOfSmsSend(userGuid, Business.SmsSendType.SendCitySms, operatorCountNumberDictionary,
				//																	smsPartCount, smsType, smsSenderAgentRefrence, smsSentGuid, smsController.DataAccessProvider);

				if (!smsController.SendCitySms(sentboxGuid))
					throw new Exception(Language.GetString("ErrorRecord"));

				smsController.CommitTransaction();
				return true;
			}
			catch (InnerTransactionException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				smsController.RollbackTransaction();
				throw ex;
			}
		}

		public static bool SendPostalCodeSms(SmsSenderAgentReference smsSenderAgentRefrence, Guid userGuid, Guid smsSentGuid,
																				 int recipientNumberCount, Guid sentboxGuid, int smsPartCount, Encoding encoding)
		{
			Business.Sms smsController = new Business.Sms();
			Dictionary<Business.Operators, int> operatorCountNumberDictionary = new Dictionary<Operators, int>();
			operatorCountNumberDictionary.Add(Operators.MCI, recipientNumberCount);

			Business.SmsTypes smsType = encoding == Business.Encoding.Utf8 ? Business.SmsTypes.Farsi : Business.SmsTypes.Latin;

			smsController.BeginTransaction();
			try
			{
				//Transaction.DecreaseCostOfSmsSend(userGuid, Business.SmsSendType.SendPostalCodeSms, operatorCountNumberDictionary,
				//																	smsPartCount, smsType, smsSenderAgentRefrence, smsSentGuid, smsController.DataAccessProvider);

				if (!smsController.SendPostalCodeSms(sentboxGuid))
					throw new Exception(Language.GetString("ErrorRecord"));

				smsController.CommitTransaction();
				return true;
			}
			catch (InnerTransactionException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				smsController.RollbackTransaction();
				throw ex;
			}
		}
		#endregion

		public static void InsertSms(Common.Sms sms)
		{
			Business.Sms smsController = new Business.Sms();
			smsController.Insert(sms);
		}

		//public static void UpdateDeliveryStatus(long messageID, int deliveryStatus)
		//{
		//	Business.Sms smsController = new Business.Sms();
		//	smsController.UpdateDeliveryStatus(messageID, deliveryStatus);
		//}

		public static void UpdateDeliveryStatus(long[] messageIDs, long[] deliveryStatus)
		{
			Business.Sms smsController = new Business.Sms();
			Dictionary<long, List<long>> messageStatus = new Dictionary<long, List<long>>();
			string outerSystemMessageID = string.Empty;

			for (int i = 0; i < deliveryStatus.Length; i++)
			{
				if (!messageStatus.ContainsKey(deliveryStatus[i]))
					messageStatus.Add(deliveryStatus[i], new List<long>());

				messageStatus[deliveryStatus[i]].Add(messageIDs[i]);
			}

			foreach (int state in messageStatus.Keys)
			{
				outerSystemMessageID = string.Empty;
				foreach (long smsID in messageStatus[state])
					outerSystemMessageID += string.Format("'{0}',", smsID);

				smsController.UpdateDeliveryStatus(outerSystemMessageID.TrimEnd(','), state);
			}
		}

		public static void UpdateDeliveryStatus(string[] messageIDs, int[] deliveryStatus)
		{
			Business.Sms smsController = new Business.Sms();
			Dictionary<int, List<string>> messageStatus = new Dictionary<int, List<string>>();

			for (int i = 0; i < deliveryStatus.Length; i++)
			{
				if (!messageStatus.ContainsKey(deliveryStatus[i]))
					messageStatus.Add(deliveryStatus[i], new List<string>());

				messageStatus[deliveryStatus[i]].Add(messageIDs[i]);
			}

			foreach (int state in messageStatus.Keys)
			{
				string strMessageIDs = string.Empty;
				foreach (string messageID in messageStatus[state])
					strMessageIDs += string.Format("'{0}',", messageID);

				smsController.UpdateDeliveryStatus(strMessageIDs.TrimEnd(','), state);
			}
		}

		public static void UpdateDeliveryStatus(string batchID, string[] numbers, int[] deliveryStatus)
		{
			Business.Sms smsController = new Business.Sms();
			Dictionary<int, List<string>> messageStatus = new Dictionary<int, List<string>>();

			for (int i = 0; i < deliveryStatus.Length; i++)
			{
				if (!messageStatus.ContainsKey(deliveryStatus[i]))
					messageStatus.Add(deliveryStatus[i], new List<string>());

				messageStatus[deliveryStatus[i]].Add(numbers[i]);
			}

			foreach (int state in messageStatus.Keys)
			{
				string recievers = string.Empty;
				foreach (string number in messageStatus[state])
					recievers += string.Format("'{0}',", number);

				smsController.UpdateDeliveryStatus(batchID, recievers.TrimEnd(','), state);
			}
		}

		public static void UpdateDeliveryStatus(long[][] deliveryStatus)
		{
			Business.Sms smsController = new Business.Sms();
			UpdateDeliveryStatus(deliveryStatus[0], deliveryStatus[1]);
		}

		public static DataTable GetPagedSmses(Common.Sms sms, Guid smsSenderAgentGuid, string userName, string senderNumber, string fromEffectiveDate, string fromTime, string toEffectiveDate, string toTime, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			Business.Sms smsController = new Business.Sms();
			return smsController.GetPagedSmses(sms, smsSenderAgentGuid, userName, senderNumber, fromEffectiveDate, fromTime, toEffectiveDate, toTime, pageNo, pageSize, sortField, ref resultCount);
		}

		public static DataSet GetCountSmsInPeriodDate(Guid userGuid)
		{
			Business.Sms smsController = new Business.Sms();
			return smsController.GetCountSmsInPeriodDate(userGuid);
		}

		public static DataTable GetBlackListNumber(Guid smsSentGuid, SmsSendError smsSendError)
		{
			Business.Sms smsController = new Business.Sms();
			return smsController.GetBlackListNumber(smsSentGuid, smsSendError);
		}

		public static void UpdateFailedSms(Common.Sms sms)
		{
			Business.Sms smsController = new Business.Sms();
			smsController.UpdateFailedSms(sms);
		}

		public static DataTable GetFailedSmsByPriority(SmsSenderAgentReference smsSenderAgentRefrence, int count)
		{
			Business.Sms smsController = new Business.Sms();
			return smsController.GetFailedSmsByPriority(smsSenderAgentRefrence, count);
		}

		public static DataTable GetFailedSms(SmsSenderAgentReference smsSenderAgentRefrence, int count)
		{
			Business.Sms smsController = new Business.Sms();
			return smsController.GetFailedSms(smsSenderAgentRefrence, count);
		}

		public static DataTable GetSentboxQueue(SmsSenderAgentReference smsSenderAgentRefrence, int agentQueueSize, int threadCount)
		{
			Business.Sms smsController = new Business.Sms();
			return smsController.GetSentboxQueue(smsSenderAgentRefrence, agentQueueSize, threadCount);
		}

		public static List<long> GetDeleviryUserSms(SmsSenderAgentReference smsSenderAgentReference, Guid userGuid)
		{
			throw new NotImplementedException();
		}
	}
}
