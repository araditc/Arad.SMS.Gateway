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

using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Arad.SMS.Gateway.Business
{
	public class PhoneNumber : BusinessEntityBase
	{
		public PhoneNumber(DataAccessBase dataAccessProvider = null)
			: base(TableNames.PhoneNumbers.ToString(), dataAccessProvider) { }

		public bool IsDuplicateNumber(Guid userGuid, Guid phoneBookGuid, Business.CheckNumberScope scope, string cellPhone, string email)
		{
			DataTable dataTableNumberStatus = new DataTable();
			dataTableNumberStatus = base.FetchSPDataTable("NumberStatus",
																										"@Scope", (int)scope,
																										"@CellPhone", cellPhone,
																										"@Email", email,
																										"@PhoneBookGuid", phoneBookGuid,
																										"@UserGuid", userGuid);
			return dataTableNumberStatus.Rows.Count > 0 ? true : false;
		}

		public DataTable GetPagedNumbers(Guid phoneBookGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet numbersInfo = base.FetchSPDataSet("GetPagedNumbers",
																								"@PhoneBookGuid", phoneBookGuid,
																								"@Query", query,
																								"@PageNo", pageNo,
																								"@PageSize", pageSize,
																								"@SortField", sortField);
			resultCount = Helper.GetInt(numbersInfo.Tables[0].Rows[0]["RowCount"]);
			return numbersInfo.Tables[1];
		}

		public bool InsertListNumber(Guid userGuid, Guid phoneBookGuid, string numbersXml, ref int countNumberDuplicate, CheckNumberScope scope)
		{
			try
			{
				DataTable dtNumbersInfo = FetchSPDataTable("InsertListNumber",
																									 "@UserGuid", userGuid,
																									 "@PhoneBookGuid", phoneBookGuid,
																									 "@NumbersXml", numbersXml,
																									 "@Scope", (int)scope);
				countNumberDuplicate = Helper.GetInt(dtNumbersInfo.Rows.Count > 0 ? dtNumbersInfo.Rows[0]["CountDuplicateNumbers"] : 0);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool InsertListNumber(Guid userGuid, Guid phoneBookGuid, string numbersXml, CheckNumberScope scope)
		{
			try
			{
				DataTable dtNumbersInfo = FetchSPDataTable("InsertListNumber",
																									 "@UserGuid", userGuid,
																									 "@PhoneBookGuid", phoneBookGuid,
																									 "@NumbersXml", numbersXml,
																									 "@Scope", (int)scope);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool InsertEmailAddress(Guid userGuid, Guid phoneBookGuid, string emailsXml, ref int countEmailDuplicate, CheckEmailScope scope)
		{
			try
			{
				DataTable dtInfo = FetchSPDataTable("InsertEmailAddress",
																						"@UserGuid", userGuid,
																						"@PhoneBookGuid", phoneBookGuid,
																						"@EmailsXml", emailsXml,
																						"@Scope", (int)scope);
				countEmailDuplicate = Helper.GetInt(dtInfo.Rows.Count > 0 ? dtInfo.Rows[0]["CountDuplicateEmails"] : 0);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool UpdateNumber(Common.PhoneNumber phoneNumber, Guid userGuid, Business.CheckNumberScope scope)
		{
			try
			{
				DataTable dataTableNumberStatus = new DataTable();
				if ((int)scope == (int)CheckNumberScope.DeleteDuplicateNumberInGroup)
					dataTableNumberStatus = FetchDataTable(@"SELECT [CellPhone] FROM [PhoneNumbers] 
																									WHERE [IsDeleted]=0 AND [Guid]!= @Guid AND [PhoneBookGuid] = @PhoneBookGuid AND [CellPhone] = @CellPhone AND [Email] = @Email",
																									"@Guid", phoneNumber.PhoneNumberGuid,
																									"@PhoneBookGuid", phoneNumber.PhoneBookGuid,
																									"@CellPhone", phoneNumber.CellPhone,
																									"@Email", phoneNumber.Email);

				else if ((int)scope == (int)CheckNumberScope.DeleteDuplicateNumberInTotalGroup)
					dataTableNumberStatus = FetchDataTable(@"SELECT [CellPhone] FROM 
																									[PhoneNumbers] number
																									INNER JOIN [PhoneBooks] phoneBook ON phoneBook.[Guid]=number.[PhoneBookGuid]
																									WHERE number.[IsDeleted] = 0 AND phoneBook.[IsDeleted] = 0 AND [UserGuid] = @UserGuid AND number.[Guid]!=@Guid AND [CellPhone] = @CellPhone AND [Email] = @Email",
																									"@UserGuid", userGuid,
																									"@Guid", phoneNumber.PhoneNumberGuid,
																									"@CellPhone", phoneNumber.CellPhone,
																									"@Email", phoneNumber.Email);
				if (dataTableNumberStatus.Rows.Count > 0)
					throw new Exception(Language.GetString("ErrorDuplicateNumber"));

				return base.ExecuteSPCommand("UpdateNumber",
																		 "@Guid", phoneNumber.PhoneNumberGuid,
																		 "@FirstName", phoneNumber.FirstName,
																		 "@LastName", phoneNumber.LastName,
																		 "@NationalCode", phoneNumber.NationalCode,
																		 "@BirthDate", phoneNumber.BirthDate,
																		 "@Telephone", phoneNumber.Telephone,
																		 "@CellPhone", phoneNumber.CellPhone,
																		 "@FaxNumber", phoneNumber.FaxNumber,
																		 "@Job", phoneNumber.Job,
																		 "@Address", phoneNumber.Address,
																		 "@Email", phoneNumber.Email,
																		 "@F1", phoneNumber.F1,
																		 "@F2", phoneNumber.F2,
																		 "@F3", phoneNumber.F3,
																		 "@F4", phoneNumber.F4,
																		 "@F5", phoneNumber.F5,
																		 "@F6", phoneNumber.F6,
																		 "@F7", phoneNumber.F7,
																		 "@F8", phoneNumber.F8,
																		 "@F9", phoneNumber.F9,
																		 "@F10", phoneNumber.F10,
																		 "@F11", phoneNumber.F11,
																		 "@F12", phoneNumber.F12,
																		 "@F13", phoneNumber.F13,
																		 "@F14", phoneNumber.F14,
																		 "@F15", phoneNumber.F15,
																		 "@F16", phoneNumber.F16,
																		 "@F17", phoneNumber.F17,
																		 "@F18", phoneNumber.F18,
																		 "@F19", phoneNumber.F19,
																		 "@F20", phoneNumber.F20,
																		 "@Sex", phoneNumber.Sex);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		//public DataTable GetPhoneNumbersByPhoneBookGuid(Guid phoneBookGuid)
		//{
		//	return base.FetchSPDataTable("GetPhoneNumbersByPhoneBookGuid", "@PhoneBookGuid", phoneBookGuid);
		//}

		//public DataTable GetPhoneNumbersByAdvancedSearch(string advancedSearchQuery)
		//{
		//	return base.FetchSPDataTable("GetPhoneNumbersByAdvancedSearch", "@AdvancedSearchQuery", advancedSearchQuery);
		//}

		public long GetCountNumberOfPhoneBook(Guid phoneBookGuid)
		{
			return Helper.GetLong(base.FetchDataTable("SELECT COUNT(*) AS [RowCount] FROM [PhoneNumbers] WHERE [IsDeleted]=0 AND [PhoneBookGuid]=@PhoneBookGuid", "@PhoneBookGuid", phoneBookGuid).Rows[0]["RowCount"]);
		}

		public bool InsertNumber(Common.PhoneNumber phoneNumber)
		{
			Guid guid = Guid.NewGuid();
			return base.ExecuteSPCommand("InsertNumber",
																	 "@Guid", guid,
																	 "@FirstName", phoneNumber.FirstName,
																	 "@LastName", phoneNumber.LastName,
																	 "@NationalCode", phoneNumber.NationalCode,
																	 "@BirthDate", phoneNumber.BirthDate,
																	 "@CreateDate", phoneNumber.CreateDate,
																	 "@Telephone", phoneNumber.Telephone,
																	 "@CellPhone", phoneNumber.CellPhone,
																	 "@FaxNumber", phoneNumber.FaxNumber,
																	 "@Job", phoneNumber.Job,
																	 "@Address", phoneNumber.Address,
																	 "@Email", phoneNumber.Email,
																	 "@F1", phoneNumber.F1,
																	 "@F2", phoneNumber.F2,
																	 "@F3", phoneNumber.F3,
																	 "@F4", phoneNumber.F4,
																	 "@F5", phoneNumber.F5,
																	 "@F6", phoneNumber.F6,
																	 "@F7", phoneNumber.F7,
																	 "@F8", phoneNumber.F8,
																	 "@F9", phoneNumber.F9,
																	 "@F10", phoneNumber.F10,
																	 "@F11", phoneNumber.F11,
																	 "@F12", phoneNumber.F12,
																	 "@F13", phoneNumber.F13,
																	 "@F14", phoneNumber.F14,
																	 "@F15", phoneNumber.F15,
																	 "@F16", phoneNumber.F16,
																	 "@F17", phoneNumber.F17,
																	 "@F18", phoneNumber.F18,
																	 "@F19", phoneNumber.F19,
																	 "@F20", phoneNumber.F20,
																	 "@Sex", phoneNumber.Sex,
																	 "@PhoneBookGuid", phoneNumber.PhoneBookGuid);
		}

		public bool DeleteMultipleNumber(Guid phonebookGuid, string numbers)
		{
			return ExecuteSPCommand("DeleteMultipleNumber",
															"@PhonebookGuid", phonebookGuid,
															"@Numbers", numbers);
		}

		public bool DeleteMultipleNumber(string guidList)
		{
			return ExecuteSPCommand("DeleteMultipleRecord", "@GuidList", guidList);
		}

		public DataTable GetPagedAllNumbers(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			{
				DataSet numbersInfo = base.FetchSPDataSet("GetPagedAllNumbers",
																									"@UserGuid", userGuid,
																									"@Query", query,
																									"@PageNo", pageNo,
																									"@PageSize", pageSize,
																									"@SortField", sortField);
				resultCount = Helper.GetInt(numbersInfo.Tables[0].Rows[0]["RowCount"]);
				return numbersInfo.Tables[1];
			}
		}

		//public DataTable GetPhoneNumbers(List<Guid> scop, List<string> senderNumberList)
		//{
		//	string phoneBookGuids = string.Empty;
		//	string numbers = string.Empty;

		//	foreach (Guid guid in scop)
		//		phoneBookGuids += string.Format("'{0}',", guid.ToString());
		//	phoneBookGuids = phoneBookGuids.Remove(phoneBookGuids.LastIndexOf(','), 1);

		//	foreach (string number in senderNumberList)
		//		numbers += string.Format("'{0}',", number);
		//	numbers = numbers.Remove(numbers.LastIndexOf(','), 1);

		//	return FetchSPDataTable("CheckNumberInScopOfSmsParser", "@PhoneBookGuids", phoneBookGuids,
		//																													"@Numbers", numbers);
		//}

		//public DataTable GetCountNumberOfOperators(string phoneBookGuid, long downRange, long upRange, bool isSendBirthDate)
		//{
		//	return FetchSPDataTable("GetCountNumberOfOperators", "@PhoneBookGuid", phoneBookGuid,
		//																											 "@DownRange", downRange,
		//																											 "@UpRange", upRange,
		//																											 "@IsSendBirthDate", isSendBirthDate);
		//}

		//public DataTable GetPagedPhoneNumbers(Guid phoneBookGuid, int pageNo, int pageSize)
		//{
		//	return FetchSPDataTable("GetPagedPhoneNumbers", "@PhoneBookGuid", phoneBookGuid,
		//																								 "@PageNo", pageNo,
		//																								 "@PageSize", pageSize);
		//}

		//public DataTable GetTodayBirthdaysPagedPhoneNumbers(Guid phoneBookGuid, int pageNo, int pageSize)
		//{
		//	return FetchSPDataTable("GetTodayBirthdaysPagedPhoneNumbers", "@PhoneBookGuid", phoneBookGuid,
		//																																"@PageNo", pageNo,
		//																																"@PageSize", pageSize);
		//}

		//public DataTable GetLimitedPagedPhoneNumbers(Guid phoneBookGuid, long downRange, int pageSize)
		//{
		//	return FetchSPDataTable("GetLimitedPagedPhoneNumbers", "@PhoneBookGuid", phoneBookGuid,
		//																												 "@DownRange", downRange,
		//																												 "@PageSize", pageSize);
		//}

		//public DataTable GetCountNumberOfOperatorsForSendSmsFormat(Guid smsFormatGuid, string groupsGuid)
		//{
		//	return FetchSPDataTable("GetCountNumberOfOperatorsForSendSmsFormat", "@FormatGuid", smsFormatGuid, "@GroupsGuid", groupsGuid);
		//}

		//public DataTable GetPagedSmsFormatPhoneNumbers(Guid smsFormatGuid, int pageNo, int pageSize)
		//{
		//	return FetchSPDataTable("GetPagedSmsFormatPhoneNumbers", "@FormatGuid", smsFormatGuid,
		//																													 "@PageNo", pageNo,
		//																													 "@pageSize", pageSize);
		//}

		//public DataTable GetNumbersInfo(Guid phoneBookGuid)
		//{
		//	return FetchDataTable("SELECT Count(*) AS [Count],[Operator] FROM [dbo].[PhoneNumbers] WHERE [PhoneBookGuid] = @PhoneBookGuid AND [IsDeleted] = 0 GROUP By [Operator]",
		//												"@PhoneBookGuid", phoneBookGuid);
		//}

		public DataTable GetCountPhoneBooksNumberOperator(string phoneBookGuids)
		{
			return FetchSPDataTable("GetCountPhoneBooksNumberOperator", "@PhoneBookGuids", phoneBookGuids);
		}

		public bool InsertBulkNumbers(DataTable dtPhoneNumber, Guid userGuid)
		{
			return ExecuteSPCommand("InsertBulkNumbers",
															"@Numbers", dtPhoneNumber,
															"@UserGuid", userGuid);
		}

		public bool TransferNumber(Guid numberGuid, Guid groupGuid)
		{
			return ExecuteCommand("UPDATE [PhoneNumbers] SET [PhoneBookGuid] = @GroupGuid WHERE [Guid] = @Guid",
														"@Guid", numberGuid,
														"@GroupGuid", groupGuid);
		}
	}
}
