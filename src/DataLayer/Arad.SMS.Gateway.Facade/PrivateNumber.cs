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

using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Arad.SMS.Gateway.Business;

namespace Arad.SMS.Gateway.Facade
{
	public class PrivateNumber : FacadeEntityBase
	{
		public static bool Insert(Common.PrivateNumber privateNumber)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.InsertNumber(privateNumber) != Guid.Empty ? true : false;
		}

		public static Common.PrivateNumber LoadNumber(Guid numberGuid)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			Common.PrivateNumber privateNumber = new Common.PrivateNumber();

			privateNumberController.Load(numberGuid, privateNumber);
			return privateNumber;
		}

		public static bool UpdateNumber(Common.PrivateNumber privatenumber)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();

			try
			{
				return privateNumberController.UpdateNumber(privatenumber);
			}
			catch
			{
				throw;
			}
		}

		public static bool UpdateExpireDate(Guid privateNumberGuid, DateTime expireDate)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.UpdateExpireDate(privateNumberGuid, expireDate);
		}

		public static bool UpdateTrafficRelay(Common.PrivateNumber privateNumber)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.UpdateTrafficRelay(privateNumber);
		}

		public static DataTable GetPagedNumbers(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.GetPagedNumbers(userGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool DeleteNumber(Guid guid)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.DeleteNumber(guid);
		}

		public static DataTable GetUserNumbers(Guid userGuid, string query, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.GetUserNumbers(userGuid, query, pageNo, pageSize, sortField, ref resultCount);
		}

		public static bool AssignNumberToUser(Guid numberGuid, Guid userGuid, string keyword, decimal price, DateTime expireDate)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			try
			{
				if (!Helper.CheckDataConditions(keyword).IsEmpty)
				{
					if (privateNumberController.IsDuplicateKeyword(numberGuid, keyword))
						throw new Exception(Language.GetString("PrivateNumberWithKeyWordIsDuplicate"));
				}

				return privateNumberController.AssignNumberToUser(numberGuid, userGuid, keyword, price, expireDate);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool AssignRangeNumberToUser(Guid numberGuid, Guid userGuid, decimal price, DateTime expireDate)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.AssignRangeNumberToUser(numberGuid, userGuid, price, expireDate);
		}

		public static DataTable GetUserPrivateNumbersForReceive(Guid userGuid)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.GetUserPrivateNumbersForReceive(userGuid);
		}

		public static DataTable GetUserPrivateNumbersForSend(Guid userGuid)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.GetUserPrivateNumbersForSend(userGuid);
		}

		public static object GetUserPrivateNumbersForSendBulk(Guid userGuid)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.GetUserPrivateNumbersForSendBulk(userGuid);
		}

		public static bool SetPublicNumber(Guid numberGuid)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.SetPublicNumber(numberGuid);
		}

		public static DataTable GetPagedAssignedLines(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.GetPagedAssignedLines(userGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public static DataTable GetPagedAllAssignedLine(string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.GetPagedAllAssignedLine(query, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool IsValidRange(List<string> lstSampleNumbers, string regex, Guid numberGuid, Guid userGuid)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			try
			{
				List<DataRow> lstRanges = privateNumberController.GetAllRanges(userGuid).AsEnumerable().ToList();
				bool isValid = true;
				foreach (DataRow row in lstRanges.Where(row => Helper.GetInt(row["UseForm"]) == (int)PrivateNumberUseForm.RangeNumber))
				{
					foreach (string sample in lstSampleNumbers)
					{
						if (Regex.IsMatch(sample, row["Regex"].ToString()))
						{
							isValid = false;
							break;
						}
					}
					if (!isValid)
						break;
				}

				if (isValid)
				{
					foreach (DataRow row in lstRanges.Where(row => Helper.GetInt(row["UseForm"]) != (int)PrivateNumberUseForm.RangeNumber))
					{
						if (Regex.IsMatch(row["Number"].ToString(), regex) && Helper.GetGuid(row["Guid"]) != numberGuid)
						{
							isValid = false;
							break;
						}
					}
				}

				return isValid;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool IsValidSubRange(string sampleNumber, Guid numberGuid, string regex, string keyword)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			try
			{
				List<DataRow> lstRanges = privateNumberController.GetSubRanges(numberGuid).AsEnumerable().ToList();
				bool isValid = true;
				foreach (DataRow row in lstRanges.Where(row => string.IsNullOrEmpty(row["Number"].ToString())))
				{
					if (Regex.IsMatch(sampleNumber, row["Regex"].ToString()))
					{
						isValid = false;
						break;
					}
				}

				if (isValid)
				{
					foreach (DataRow row in lstRanges.Where(row => !string.IsNullOrEmpty(row["Number"].ToString())))
					{
						if (Regex.IsMatch(row["Number"].ToString(), regex) && row["Keyword"].ToString() == keyword)
						{
							isValid = false;
							break;
						}
					}
				}

				if (isValid)
				{

				}

				return isValid;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool BulkIsActive(Guid numberGuid)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			DataTable dtAgentInfo = privateNumberController.GetAgentInfo(numberGuid);
			return dtAgentInfo.Rows.Count > 0 ? Helper.GetBool(dtAgentInfo.Rows[0]["IsSendBulkActive"]) : false;
		}

		public static bool AssignSubRangeNumberToUser(Common.PrivateNumber privateNumber, string keyword)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.AssignSubRangeNumberToUser(privateNumber, keyword);
		}

		public static Guid GetUserNumberGuid(string number, Guid userGuid)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.GetUserNumberGuid(number, userGuid);
		}

		public static DataTable GetVASNumber(string serviceId)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.GetVASNumber(serviceId);
		}

		public static DataTable GetPagedAssignedKeywords(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.GetPagedAssignedKeywords(userGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool DeleteKeyword(Guid guid)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.DeleteKeyword(guid);
		}

		public static string GetServiceId(TypeServiceId typeServiceId, string serviceId)
		{
			Business.PrivateNumber privateNumberController = new Business.PrivateNumber();
			return privateNumberController.GetServiceId(typeServiceId, serviceId);
		}
	}
}
