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
using Arad.SMS.Gateway.Common.Exceptions;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Arad.SMS.Gateway.Facade
{
	public class Transaction : FacadeEntityBase
	{
		private static object lockIncreaseCostOfFaildSend;
		private static object lockDecreaseCostOfSmsSend;
		private static object lockDecreaseCostOfRegisterDomain;

		static Transaction()
		{
			lockIncreaseCostOfFaildSend = new object();
			lockDecreaseCostOfSmsSend = new object();
			lockDecreaseCostOfRegisterDomain = new object();
		}

		public static decimal ComputeTax(decimal amount)
		{
			Business.Setting setting = new Business.Setting();
			decimal tax = Helper.GetDecimal(setting.GetValue((int)Business.MainSettings.Tax));
			//return amount - (amount - (tax * amount) / 100 + (tax * tax * amount) / 10000);
			return (tax * amount) / 100;
		}

		public static bool CalculateBenefit(Guid userGuid, decimal smsCount, Guid transactionGuid, DataAccessBase dataAccessProvider)
		{
			Business.Transaction transactionController = new Business.Transaction(dataAccessProvider);
			return transactionController.CalculateBenefit(userGuid, smsCount, transactionGuid);
		}
		public static Guid Decrease(Guid userGuid, decimal amount, TypeCreditChanges typeCreditChanges, string description, Guid referenceGuid, DataAccessBase dataAccessProvider)
		{
			try
			{
				Business.Transaction transactionController = new Business.Transaction(dataAccessProvider);
				Business.User userController = new Business.User(dataAccessProvider);

				decimal currentCredit = userController.GetUserCredit(userGuid);
				if (currentCredit < amount)
					throw new Exception(Language.GetString("ErrorCredit"));

				Common.Transaction transaction = new Common.Transaction();

				transaction.TypeTransaction = (int)Business.TypeTransactions.Decrease;
				transaction.ReferenceGuid = referenceGuid;
				transaction.TypeCreditChange = (int)typeCreditChanges;
				transaction.Description = description;
				transaction.CreateDate = DateTime.Now;
				transaction.CurrentCredit = currentCredit;
				transaction.Amount = amount;
				transaction.UserGuid = userGuid;
				Guid transactionGuid = transactionController.Insert(transaction);

				if (transactionGuid == Guid.Empty)
					throw new Exception(Language.GetString("ErrorRecord"));

				if (!userController.UpdateCredit(userGuid, transaction.CurrentCredit - amount))
					throw new Exception(Language.GetString("ErrorRecord"));

				return transactionGuid;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static Guid Increase(Guid userGuid, decimal amount, TypeCreditChanges typeCreditChanges, string description, Guid referenceGuid, bool updateGrupPrice, DataAccessBase dataAccessProvider)
		{
			Business.Transaction transactionController = new Business.Transaction(dataAccessProvider);
			Business.User userController = new Business.User(dataAccessProvider);

			Common.Transaction transaction = new Common.Transaction();
			decimal currentCredit = userController.GetUserCredit(userGuid);

			transaction.ReferenceGuid = referenceGuid;
			transaction.TypeTransaction = (int)Business.TypeTransactions.Increase;
			transaction.TypeCreditChange = (int)typeCreditChanges;
			transaction.Description = description;
			transaction.CreateDate = DateTime.Now;
			transaction.CurrentCredit = currentCredit;
			transaction.Amount = amount;
			transaction.UserGuid = userGuid;
			Guid transactionGuid = transactionController.Insert(transaction);

			if (transactionGuid == Guid.Empty)
				throw new Exception(Language.GetString("ErrorRecord"));

			if (updateGrupPrice)
			{
				if (!userController.UpdateCreditAndGroupPrice(userGuid, transaction.CurrentCredit + amount, amount))
					throw new Exception(Language.GetString("ErrorRecord"));
			}
			else
			{
				if (!userController.UpdateCredit(userGuid, transaction.CurrentCredit + amount))
					throw new Exception(Language.GetString("ErrorRecord"));
			}

			return transactionGuid;
		}

		public static bool ChangeCreditByManage(Guid userGuid, bool parentIsMainAdmin, TypeTransactions type, decimal amount, string description)
		{
			Business.Transaction transactionController = new Business.Transaction();
			transactionController.BeginTransaction();
			Guid transactionGuid;

			try
			{
				Common.User user = Facade.User.LoadUser(userGuid);
				switch (type)
				{
					case TypeTransactions.Increase:
						if (!parentIsMainAdmin)
							Decrease(user.ParentGuid, amount, TypeCreditChanges.Manage, string.Format(Language.GetString("ManageIncreaseCreditTransaction"), user.UserName, description), Guid.Empty, transactionController.DataAccessProvider);
						transactionGuid = Increase(userGuid, amount, TypeCreditChanges.Manage, description, Guid.Empty, false, transactionController.DataAccessProvider);
						CalculateBenefit(userGuid, amount, transactionGuid, transactionController.DataAccessProvider);
						break;
					case TypeTransactions.Decrease:
						transactionGuid = Decrease(userGuid, amount, TypeCreditChanges.Manage, description, Guid.Empty, transactionController.DataAccessProvider);
						CalculateBenefit(userGuid, amount * -1, transactionGuid, transactionController.DataAccessProvider);
						if (!parentIsMainAdmin)
							Increase(user.ParentGuid, amount, TypeCreditChanges.Manage, string.Format(Language.GetString("ManageDecreaseCreditTransaction"), user.UserName, description), Guid.Empty, false, transactionController.DataAccessProvider);
						break;
				}
				transactionController.CommitTransaction();
				return true;
			}
			catch (Exception ex)
			{
				transactionController.RollbackTransaction();
				throw ex;
			}
		}

		public static DataTable GetPagedUserTransaction(Guid userGuid, Guid referenceGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.Transaction transactionController = new Business.Transaction();
			return transactionController.GetPagedUserTransaction(userGuid, referenceGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public static DataTable GetPagedUsersTransaction(Guid userGuid, Guid domainGuid, string username, string query, string sortField, int pageNo, int pageSize, ref int resultCount,ref decimal totalCount)
		{
			Business.Transaction transactionController = new Business.Transaction();
			return transactionController.GetPagedUsersTransaction(userGuid, domainGuid, username, query, sortField, pageNo, pageSize, ref resultCount,ref totalCount);
		}

		public static void DecreaseCostOfSmsSend(Guid userGuid, Common.SmsSendType smsSendType, decimal price, string description, Guid smsSentGuid, DataAccessBase dataAccessBase)
		{
			Business.Transaction transactionController = new Business.Transaction(dataAccessBase);
			Business.User userController = new Business.User(dataAccessBase);
			Common.User user = new Common.User();

			lock (lockDecreaseCostOfSmsSend)
			{
				transactionController.BeginTransaction();
				try
				{
					userController.Load(userGuid, user);

					if (price > user.Credit)
						throw new UserCreditNotEnoughException();

					Decrease(userGuid, price, GetTypeCreditChanges(smsSendType), description, smsSentGuid, transactionController.DataAccessProvider);

					transactionController.CommitTransaction();
				}
				catch (Exception ex)
				{
					transactionController.RollbackTransaction();
					throw new InnerTransactionException(ex);
				}
			}
		}

		private static TypeCreditChanges GetTypeCreditChanges(Common.SmsSendType smsSendType)
		{
			switch (smsSendType)
			{
				case Common.SmsSendType.SendSms:
					return Business.TypeCreditChanges.SendSms;
			}

			return Business.TypeCreditChanges.SendSms;
		}

		private static decimal GetOperatorRatio(List<DataRow> lstOperatorRatio, int opt, SmsTypes smsType)
		{
			List<DataRow> optInfo = lstOperatorRatio.Where(row => Helper.GetInt(row["OperatorID"]) == opt &&
																														Helper.GetInt(row["SmsType"]) == (int)smsType).ToList();
			if (optInfo.Count > 0)
				return Helper.GetDecimal(optInfo.First()["Ratio"], 1);
			else
				return 1;
		}

		public static decimal GetSendPrice(Guid userGuid, SmsTypes smsType, int smsPartCount, Guid privateNumberGuid, Dictionary<int, int> operatorNumberCount)
		{

			decimal smsPrice = 0;

			Common.PrivateNumber privateNumber = Facade.PrivateNumber.LoadNumber(privateNumberGuid);
			List<DataRow> lstOperatorRatio = Facade.SmsSenderAgent.GetAgentRatio(privateNumber.SmsSenderAgentGuid).AsEnumerable().ToList();
			Dictionary<Guid, decimal> dictionaryAgentRatio = Facade.GroupPrice.GetAgentRatio(userGuid);
			smsPartCount = privateNumber.Type == (int)Business.TypePrivateNumberAccesses.Bulk ? smsPartCount : 1;

			switch (smsType)
			{
				case SmsTypes.Farsi:
					foreach (KeyValuePair<int, int> opt in operatorNumberCount)
						smsPrice += opt.Value * GetOperatorRatio(lstOperatorRatio, opt.Key, SmsTypes.Farsi) * smsPartCount;
					break;
				case SmsTypes.Latin:
					foreach (KeyValuePair<int, int> opt in operatorNumberCount)
						smsPrice += opt.Value * GetOperatorRatio(lstOperatorRatio, opt.Key, SmsTypes.Latin) * smsPartCount;
					break;
			}

			smsPrice *= dictionaryAgentRatio.ContainsKey(privateNumber.SmsSenderAgentGuid) ? Helper.GetDecimal(dictionaryAgentRatio[privateNumber.SmsSenderAgentGuid]) : 1;
			return smsPrice;
		}

		public static decimal GetSendFormatPrice(Guid userGuid, Guid formatGuid, Guid privateNumberGuid, ref string numbersInfo)
		{
			decimal smsPrice = 0;
			int recipientCount;

			DataTable dtOperators = Operator.GetOperators();
			Common.PrivateNumber privateNumber = PrivateNumber.LoadNumber(privateNumberGuid);
			List<DataRow> lstOperatorRatio = SmsSenderAgent.GetAgentRatio(privateNumber.SmsSenderAgentGuid).AsEnumerable().ToList();
			Dictionary<Guid, decimal> dictionaryAgentRatio = GroupPrice.GetAgentRatio(userGuid);

			List<DataRow> lstInfo = SmsFormat.GetFormatSmsInfo(formatGuid).AsEnumerable().ToList();
			Dictionary<int, int> operatorNumberCount = new Dictionary<int, int>();

			int countUnicode = 0;
			int countLatin = 0;

			int mciCount = 0;
			int mtnCount = 0;
			int otherCount = 0;

			if (privateNumber.Type == (int)TypePrivateNumberAccesses.Bulk)
			{
				foreach (DataRow opt in dtOperators.Rows)
				{
					operatorNumberCount.Add(Helper.GetInt(opt["ID"]), lstInfo.Where(row => Helper.GetInt(row["Operator"]) == Helper.GetInt(opt["ID"])).Select(row => Helper.GetInt(row["Count"])).Sum());

					countUnicode = lstInfo.Where(row => Helper.GetInt(row["Operator"]) == Helper.GetInt(opt["ID"]) &&
																							Helper.GetInt(row["Encoding"]) == 1).Select(row => Helper.GetInt(row["SmsPartCount"]) * Helper.GetInt(row["Count"])).Sum();

					smsPrice += countUnicode * GetOperatorRatio(lstOperatorRatio, Helper.GetInt(opt["ID"]), SmsTypes.Farsi);

					countLatin = lstInfo.Where(row => Helper.GetInt(row["Operator"]) == Helper.GetInt(opt["ID"]) &&
																							 Helper.GetInt(row["Encoding"]) == 0).Select(row => Helper.GetInt(row["SmsPartCount"]) * Helper.GetInt(row["Count"])).Sum();

					smsPrice += countLatin * GetOperatorRatio(lstOperatorRatio, Helper.GetInt(opt["ID"]), SmsTypes.Latin);

					if (Helper.GetInt(opt["ID"]) == 1)//MCI
						mciCount = countUnicode + countLatin;
					else if (Helper.GetInt(opt["ID"]) == 2)//MTN
						mtnCount = countUnicode + countLatin;
					else
						otherCount += countUnicode + countLatin;
				}
			}
			else
			{
				foreach (DataRow opt in dtOperators.Rows)
				{
					operatorNumberCount.Add(Helper.GetInt(opt["ID"]), lstInfo.Where(row => Helper.GetInt(row["Operator"]) == Helper.GetInt(opt["ID"])).Select(row => Helper.GetInt(row["Count"])).Sum());

					countUnicode = lstInfo.Where(row => Helper.GetInt(row["Operator"]) == Helper.GetInt(opt["ID"]) &&
																							Helper.GetInt(row["Encoding"]) == 1).Select(row => Helper.GetInt(row["Count"])).Sum();

					smsPrice += countUnicode * GetOperatorRatio(lstOperatorRatio, Helper.GetInt(opt["ID"]), SmsTypes.Farsi);

					countLatin = lstInfo.Where(row => Helper.GetInt(row["Operator"]) == Helper.GetInt(opt["ID"]) &&
																						Helper.GetInt(row["Encoding"]) == 0).Select(row => Helper.GetInt(row["Count"])).Sum();

					smsPrice += countLatin * GetOperatorRatio(lstOperatorRatio, Helper.GetInt(opt["ID"]), SmsTypes.Latin);

					if (Helper.GetInt(opt["ID"]) == 1)//MCI
						mciCount = countUnicode + countLatin;
					else if (Helper.GetInt(opt["ID"]) == 2)//MTN
						mtnCount = countUnicode + countLatin;
					else
						otherCount += countUnicode + countLatin;
				}
			}

			smsPrice *= dictionaryAgentRatio.ContainsKey(privateNumber.SmsSenderAgentGuid) ? Helper.GetDecimal(dictionaryAgentRatio[privateNumber.SmsSenderAgentGuid]) : 1;

			recipientCount = lstInfo.Select(row => Helper.GetInt(row["Count"])).Sum();

			int totalCount = mciCount + mtnCount + otherCount;

			numbersInfo = "RecipientCount{(" + recipientCount + ")}TotalCount{(" + totalCount + ")}MCI{(" + mciCount + ")}MTN{(" + mtnCount + ")}Other{(" + otherCount + ")}Price{(" + smsPrice + ")}";

			return smsPrice;
		}
	}
}

