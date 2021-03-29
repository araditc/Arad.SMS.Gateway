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
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Arad.SMS.Gateway.Facade
{
	public class Outbox : FacadeEntityBase
	{
		#region SelectMethod
		public static Dictionary<int, int> GetCountNumberOfOperators(ref List<string> lstNumbers, ref List<string> lstFailedNumber)
		{
			try
			{
				Dictionary<int, int> operatorCountNumberDictionary = new Dictionary<int, int>();
				Dictionary<int, string> operatorsInfo = Operator.GetOperatorsInfo();

				lstNumbers = lstNumbers.Select<string, string>(s => Helper.CheckingCellPhone(ref s) ? s : s).ToList();

				lstFailedNumber = lstNumbers.Where(s => operatorsInfo.Where(opt => Regex.IsMatch(s, opt.Value)).FirstOrDefault().Key == 0).Distinct().ToList();
				lstNumbers = lstNumbers.Where(s => operatorsInfo.Where(opt => Regex.IsMatch(s, opt.Value)).FirstOrDefault().Key > 0).Distinct().ToList();

				foreach (KeyValuePair<int, string> opt in operatorsInfo)
					operatorCountNumberDictionary.Add(Helper.GetInt(opt.Key), lstNumbers.Where(reciever => Regex.IsMatch(reciever, opt.Value)).Count());

				return operatorCountNumberDictionary;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static Dictionary<int, int> GetCountNumberOfOperators(ref List<string> lstNumbers)
		{
			try
			{
				Dictionary<int, int> operatorCountNumberDictionary = new Dictionary<int, int>();
				Dictionary<int, string> operatorsInfo = Operator.GetOperatorsInfo();

                // بر مبنای بین الملل می کند
				lstNumbers = lstNumbers.Select<string, string>(s => Helper.CheckingCellPhone(ref s) ? s : s).ToList();
                // چک کردن دیتابیس برای دیتابیس
				lstNumbers = lstNumbers.Where(s => operatorsInfo.Where(opt => Regex.IsMatch(s, opt.Value)).FirstOrDefault().Key > 0).Distinct().ToList();

				foreach (KeyValuePair<int, string> opt in operatorsInfo)
					operatorCountNumberDictionary.Add(Helper.GetInt(opt.Key), lstNumbers.Where(reciever => Regex.IsMatch(reciever, opt.Value)).Count());

				return operatorCountNumberDictionary;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static void AddCountNumberOfOperatorsToDictionary(ref List<string> lstNumbers, ref Dictionary<int, int> operatorCountNumberDictionary)
		{
			try
			{
				Dictionary<int, string> operatorsInfo = Facade.Operator.GetOperatorsInfo();

				lstNumbers = lstNumbers.Select<string, string>(s => Helper.CheckingCellPhone(ref s) ? s : s).ToList();
				lstNumbers = lstNumbers.Where(s => operatorsInfo.Where(opt => Regex.IsMatch(s, opt.Value)).FirstOrDefault().Key > 0).Distinct().ToList();

				foreach (KeyValuePair<int, string> opt in operatorsInfo)
				{
					if (operatorCountNumberDictionary.ContainsKey(opt.Key))
						operatorCountNumberDictionary[opt.Key] += lstNumbers.Where(reciever => Regex.IsMatch(reciever, opt.Value)).Count();
					else
						operatorCountNumberDictionary.Add(opt.Key, lstNumbers.Where(reciever => Regex.IsMatch(reciever, opt.Value)).Count());
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetPagedSmses(Guid userGuid, Guid referenceGuid, string query, int pageNo, int pageSize, string sortField, ref int resultCount, ref int totalReceiverCount, ref decimal totalPrice)
		{
			Business.Outbox smsSentController = new Business.Outbox();
			return smsSentController.GetPagedSmses(userGuid, referenceGuid, query, pageNo, pageSize, sortField, ref resultCount, ref totalReceiverCount, ref totalPrice);
		}

		public static DataTable GetPagedUserSmses(Guid userGuid, Guid domainGuid, string query,
																							int pageNo, int pageSize, string sortField, ref int resultCount,
																							ref int totalReceiverCount, ref decimal totalPrice,
																							ref Dictionary<DeliveryStatus,Tuple<int,int>> dictionaryDelivery)
		{
			Business.Outbox smsSentController = new Business.Outbox();
			return smsSentController.GetPagedUserSmses(userGuid, domainGuid, query, pageNo, pageSize, sortField, ref resultCount, ref totalReceiverCount, ref totalPrice, ref dictionaryDelivery);
		}

		public static DataTable GetPagedUserManualSmses(string query, int pageNo, int pageSize, string sortField, ref int resultCount, ref int totalReceiverCount, ref decimal totalPrice)
		{
			Business.Outbox smsSentController = new Business.Outbox();
			return smsSentController.GetPagedUserManualSmses(query, pageNo, pageSize, sortField, ref resultCount, ref totalReceiverCount, ref totalPrice);
		}
		#endregion

		public static Common.Outbox Load(Guid guid)
		{
			Business.Outbox smsSentController = new Business.Outbox();
			Common.Outbox smsSent = new Common.Outbox();
			smsSentController.Load(guid, smsSent);
			return smsSent;
		}

		public static DataTable GetSendQueue(SmsSenderAgentReference smsSenderAgentRefrence, int agentQueueSize, int threadCount)
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.GetSendQueue(smsSenderAgentRefrence, agentQueueSize, threadCount);
		}

		public static DataTable GetOutboxReport(Guid referenceGuid)
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.GetOutboxReport(referenceGuid);
		}

		public static DataTable GetExportDataRequest(int recordCount)
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.GetExportDataRequest(recordCount);
		}

		public static bool UpdateExportDataRequest(DataTable dtSaveRequest)
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.UpdateExportDataRequest(dtSaveRequest);
		}

		public static DataTable GetPagedExportData(Guid guid, int pageNo, int pageSize)
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.GetPagedExportData(guid, pageNo, pageSize);
		}

		public static DataTable GetPagedExportText(Guid guid, int pageNo, int pageSize)
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.GetPagedExportText(guid, pageNo, pageSize);
		}

		public static bool SetOutboxExportDataStatus(Guid outboxGuid)
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.SetOutboxExportDataStatus(outboxGuid);
		}

		public static bool SetOutboxExportTxtStatus(Guid outboxGuid)
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.SetOutboxExportTxtStatus(outboxGuid);
		}

		public static DataTable GetOutboxForGiveBackCredit()
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.GetOutboxForGiveBackCredit();
		}

		public static bool GiveBackBlackListAndFailedSend(Guid guid)
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.GiveBackBlackListAndFailedSend(guid);
		}

		public static bool AddBlackListNumbersToTable(Guid guid)
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.AddBlackListNumbersToTable(guid);
		}

		public static bool ResendFailedSms(Guid outboxGuid)
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.ResendFailedSms(outboxGuid);
		}

		public static bool ArchiveNumbers(Guid guid)
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.ArchiveNumbers(guid);
		}

		public static bool CheckReceiverCount(Guid guid)
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.CheckReceiverCount(guid);
		}

		public static bool UpdateStatus(Guid guid, SendStatus status, long id = 0)
		{
			Business.Outbox outboxController = new Business.Outbox();
			return outboxController.UpdateStatus(guid, status, id);
		}
	}
}