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
using System.Xml.Linq;

namespace Arad.SMS.Gateway.Facade
{
	public class PhoneNumber : FacadeEntityBase
	{
		//#region InsertMethod
		//#endregion

		//#region UpdateMethod
		//#endregion

		//#region SelectMethod
		//public static DataTable GetCountNumberOfOperatorsForSendSmsFormat(Guid smsFormatGuid, string groupsGuid)
		//{
		//	Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
		//	return phoneNumberController.GetCountNumberOfOperatorsForSendSmsFormat(smsFormatGuid, groupsGuid);
		//}

		public static Dictionary<string, string> GetFileNumberInfo(string path, List<string> lstNumbers)
		{
			int correctNumber = 0;
			int duplicateNumber = 0;
			Dictionary<string, string> fileInfo = new Dictionary<string, string>();

			try
			{
				if (!string.IsNullOrEmpty(path))
				{
					DataTable dtb = new DataTable();
					string fileExtention = path.Substring(path.LastIndexOf('.'));

					switch (fileExtention.Trim('.').ToLower())
					{
						case "csv":
							dtb = ImportFile.ImportCSV(path, false);
							break;
						case "xls":
						case "xlsx":
							dtb = ImportFile.ImportExcel(path, false);
							break;
					}

					foreach (DataRow row in dtb.Rows)
					{
						if (Helper.IsCellPhone(row[0].ToString()) > 0)
						{
							correctNumber++;
							lstNumbers.Add(row[0].ToString());
						}
					}

					duplicateNumber = dtb.Rows.Count - lstNumbers.GroupBy(number => number).Count();

					fileInfo.Add("TotalNumberCount", dtb.Rows.Count.ToString());
					fileInfo.Add("CorrectNumberCount", correctNumber.ToString());
					fileInfo.Add("DuplicateNumberCount", duplicateNumber.ToString());
				}
				return fileInfo;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static Dictionary<int, int> GetCountPhoneBooksNumberOperator(string phoneBooksGuid)
		{
			Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
			Dictionary<int, int> dictionaryOperatorsCountNumber = new Dictionary<int, int>();
			DataTable dt = phoneNumberController.GetCountPhoneBooksNumberOperator(phoneBooksGuid);

			foreach (DataRow row in dt.Rows)
				dictionaryOperatorsCountNumber.Add(Helper.GetInt(row["Operator"]), Helper.GetInt(row["Count"]));

			return dictionaryOperatorsCountNumber;
		}

		public static DataTable GetPagedNumbers(Guid phoneBookGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
			return phoneNumberController.GetPagedNumbers(phoneBookGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public static DataTable GetPagedAllNumbers(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
			return phoneNumberController.GetPagedAllNumbers(userGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool IsDuplicateNumber(Guid userGuid, Guid phoneBookGuid, Business.CheckNumberScope scope, string cellPhone, string email)
		{
			Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
			return phoneNumberController.IsDuplicateNumber(userGuid, phoneBookGuid, scope, cellPhone, email);
		}

		public static bool InsertNumber(Common.PhoneNumber phoneNumber)
		{
			Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
			return phoneNumberController.InsertNumber(phoneNumber);
		}

		public static bool InsertListNumber(Guid userGuid,
																				Guid phoneBookGuid,
																				ref List<string> lstNumbers,
																				ref List<string> lstFailedNumbers,
																				ref int countNumberDuplicate,
																				Business.CheckNumberScope scope)
		{
			Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
			int mobileCount = 0;
			int emailCount = 0;

			try
			{
				Outbox.GetCountNumberOfOperators(ref lstNumbers, ref lstFailedNumbers);

				if (lstNumbers.Count == 0)
					throw new Exception(Language.GetString("CompleteMobileNoField"));

				Common.User user = User.LoadUser(userGuid);
				PhoneBook.GetUserMaximumRecordInfo(userGuid, ref mobileCount, ref emailCount);

				if (user.MaximumPhoneNumber != -1 &&
						user.MaximumPhoneNumber <= mobileCount &&
						user.MaximumPhoneNumber <= (mobileCount + lstNumbers.Count))
					throw new Exception(Language.GetString("ErrorMaximumPhoneNumber"));

				XDocument doc = new XDocument();
				XElement root = new XElement("NewDataSet");

				foreach (string number in lstNumbers)
				{
					XElement element = new XElement("Table");
					element.Add(new XElement("CellPhone", number));
					root.Add(element);
				}
				doc.Add(root);

				return phoneNumberController.InsertListNumber(userGuid, phoneBookGuid, doc.ToString(), ref countNumberDuplicate, scope);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool InsertListNumber(Guid userGuid,
																				 Guid phoneBookGuid,
																				 ref List<string> lstNumbers,
																				 Business.CheckNumberScope scope)
		{
			Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
			int mobileCount = 0;
			int emailCount = 0;

			try
			{
				Outbox.GetCountNumberOfOperators(ref lstNumbers);

				if (lstNumbers.Count == 0)
					throw new Exception(Language.GetString("CompleteMobileNoField"));

				Common.User user = User.LoadUser(userGuid);
				PhoneBook.GetUserMaximumRecordInfo(userGuid, ref mobileCount, ref emailCount);

				if (user.MaximumPhoneNumber != -1 &&
						user.MaximumPhoneNumber <= mobileCount &&
						user.MaximumPhoneNumber <= (mobileCount + lstNumbers.Count))
					throw new Exception(Language.GetString("ErrorMaximumPhoneNumber"));

				XDocument doc = new XDocument();
				XElement root = new XElement("NewDataSet");

				foreach (string number in lstNumbers)
				{
					XElement element = new XElement("Table");
					element.Add(new XElement("CellPhone", number));
					root.Add(element);
				}
				doc.Add(root);

				return phoneNumberController.InsertListNumber(userGuid, phoneBookGuid, doc.ToString(), scope);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool InsertListEmail(Guid userGuid, Guid phoneBookGuid, ref List<string> lstEmails, ref List<string> lstFailedEmails,
																			ref int countEmailDuplicate, Business.CheckEmailScope scope)
		{
			Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
			int mobileCount = 0;
			int emailCount = 0;

			try
			{
				Helper.CheckEmailsList(ref lstEmails, ref lstFailedEmails);

				Common.User user = Facade.User.LoadUser(userGuid);
				PhoneBook.GetUserMaximumRecordInfo(userGuid, ref mobileCount, ref emailCount);

				if (user.MaximumEmailAddress != -1 && user.MaximumEmailAddress <= emailCount && user.MaximumEmailAddress <= (emailCount + lstEmails.Count))
					throw new Exception(Language.GetString("ErrorMaximumEmailAddress"));


				XDocument doc = new XDocument();
				XElement root = new XElement("NewDataSet");

				foreach (string number in lstEmails)
				{
					XElement element = new XElement("Table");

					element.Add(new XElement("Email", number));
					root.Add(element);
				}
				doc.Add(root);

				return phoneNumberController.InsertEmailAddress(userGuid, phoneBookGuid, doc.ToString(), ref countEmailDuplicate, scope);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static Common.PhoneNumber LoadNumber(Guid phoneNumberGuid)
		{
			Common.PhoneNumber phoneNumber = new Common.PhoneNumber();
			Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
			phoneNumberController.Load(phoneNumberGuid, phoneNumber);
			return phoneNumber;
		}

		public static bool UpdateNumber(Common.PhoneNumber phoneNumber, Guid userGuid, Business.CheckNumberScope scope)
		{
			try
			{
				Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
				return phoneNumberController.UpdateNumber(phoneNumber, userGuid, scope);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool DeleteNumber(Guid guid)
		{
			Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
			return phoneNumberController.Delete(guid);
		}

		//public static DataTable GetPhoneNumbers(Guid phoneBookGuid)
		//{
		//	Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
		//	return phoneNumberController.GetPhoneNumbersByPhoneBookGuid(phoneBookGuid);
		//}

		//public static DataTable GetAdvancedPhoneNumbers(string advancedSearch)
		//{
		//	Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
		//	return phoneNumberController.GetPhoneNumbersByAdvancedSearch(advancedSearch);
		//}

		public static long GetCountNumberOfPhoneBook(Guid phoneBookGuid)
		{
			Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
			return phoneNumberController.GetCountNumberOfPhoneBook(phoneBookGuid);
		}

		public static bool DeleteMultipleNumber(string guidList)
		{
			Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
			return phoneNumberController.DeleteMultipleNumber(guidList);
		}

		public static bool DeleteMultipleNumber(Guid phonebookGuid, List<string> numbers)
		{
			try
			{
				Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
				Dictionary<int, int> operatorNumberCount = Outbox.GetCountNumberOfOperators(ref numbers);

				if (operatorNumberCount.Values.Sum() == 0)
					throw new Exception(Language.GetString("RecieverListIsEmpty"));

				return phoneNumberController.DeleteMultipleNumber(phonebookGuid, string.Join(",", numbers.Select<string, string>(number => string.Format("'{0}'", number))));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		//public static List<string> GetPhoneNumbers(List<Guid> scop, List<string> senderNumberList)
		//{
		//	Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
		//	DataTable dataTableValidNumbers = phoneNumberController.GetPhoneNumbers(scop, senderNumberList);

		//	List<string> validNumberList = new List<string>();
		//	foreach (DataRow row in dataTableValidNumbers.Rows)
		//		validNumberList.Add(row["CellPhone"].ToString());

		//	return validNumberList;
		//}

		//public static Dictionary<Business.Operators, int> GetCountNumberOfOperators(Guid phoneBookGuid, long downRange, long upRange, bool isSendBirthDate)
		//{
		//  Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
		//  DataRow dataRowSmsInfo = phoneNumberController.GetCountNumberOfOperators(phoneBookGuid, downRange, upRange, isSendBirthDate);
		//  Dictionary<Business.Operators, int> operatorCountNumberDictionary = new Dictionary<Business.Operators, int>();
		//  if (Helper.GetInt(dataRowSmsInfo["MCI"]) > 0)
		//    operatorCountNumberDictionary.Add(Business.Operators.MCI, Helper.GetInt(dataRowSmsInfo["MCI"]));
		//  if (Helper.GetInt(dataRowSmsInfo["Irancell"]) > 0)
		//    operatorCountNumberDictionary.Add(Business.Operators.Irancell, Helper.GetInt(dataRowSmsInfo["Irancell"]));
		//  if (Helper.GetInt(dataRowSmsInfo["RighTel"]) > 0)
		//    operatorCountNumberDictionary.Add(Business.Operators.Rightel, Helper.GetInt(dataRowSmsInfo["RighTel"]));
		//  return operatorCountNumberDictionary;
		//}

		//public static Dictionary<Business.Operators, int> GetCountNumberOfOperators(string phoneBookGuid, long downRange, long upRange, bool isSendBirthDate)
		//{
		//	Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
		//	DataTable dtSmsInfo = phoneNumberController.GetCountNumberOfOperators(phoneBookGuid, downRange, upRange, isSendBirthDate);
		//	Dictionary<Business.Operators, int> operatorCountNumberDictionary = new Dictionary<Business.Operators, int>();

		//	foreach (DataRow dataRow in dtSmsInfo.Rows)
		//	{
		//		switch (Helper.GetInt(dataRow["Operator"]))
		//		{
		//			case (int)Business.Operators.MCI:
		//				operatorCountNumberDictionary.Add(Business.Operators.MCI, Helper.GetInt(dataRow["Count"]));
		//				break;
		//			case (int)Business.Operators.Irancell:
		//				operatorCountNumberDictionary.Add(Business.Operators.Irancell, Helper.GetInt(dataRow["Count"]));
		//				break;
		//			case (int)Business.Operators.Rightel:
		//				operatorCountNumberDictionary.Add(Business.Operators.Rightel, Helper.GetInt(dataRow["Count"]));
		//				break;
		//		}
		//	}
		//	return operatorCountNumberDictionary;
		//}

		//public static DataTable GetPagedPhoneNumbers(Guid phoneBookGuid, int pageNo, int pageSize)
		//{
		//	Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
		//	return phoneNumberController.GetPagedPhoneNumbers(phoneBookGuid, pageNo, pageSize);
		//}

		//public static DataTable GetTodayBirthdaysPagedPhoneNumbers(Guid phoneBookGuid, int pageNo, int pageSize)
		//{
		//	Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
		//	return phoneNumberController.GetTodayBirthdaysPagedPhoneNumbers(phoneBookGuid, pageNo, pageSize);
		//}

		//public static DataTable GetLimitedPagedPhoneNumbers(Guid phoneBookGuid, long downRange, int pageSize)
		//{
		//	Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
		//	return phoneNumberController.GetLimitedPagedPhoneNumbers(phoneBookGuid, downRange, pageSize);
		//}

		//public static DataTable GetPagedSmsFormatPhoneNumbers(Guid smsFormatGuid, int pageNo, int pageSize)
		//{
		//	Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
		//	return phoneNumberController.GetPagedSmsFormatPhoneNumbers(smsFormatGuid, pageNo, pageSize);
		//}

		//public static DataTable GetNumbersInfo(Guid phoneBookGuid)
		//{
		//	Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
		//	return phoneNumberController.GetNumbersInfo(phoneBookGuid);
		//}

		public static bool InsertBulkNumbers(List<Common.PhoneNumber> lstNumbers, Guid userGuid)
		{
			Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
			DataTable dtPhoneNumber = new DataTable();
			DataRow row;

			try
			{
				dtPhoneNumber.Columns.Add("Guid", typeof(Guid));
				dtPhoneNumber.Columns.Add("FirstName", typeof(string));
				dtPhoneNumber.Columns.Add("LastName", typeof(string));
				dtPhoneNumber.Columns.Add("BirthDate", typeof(DateTime));
				dtPhoneNumber.Columns.Add("CreateDate", typeof(DateTime));
				dtPhoneNumber.Columns.Add("Telephone", typeof(string));
				dtPhoneNumber.Columns.Add("CellPhone", typeof(string));
				dtPhoneNumber.Columns.Add("FaxNumber", typeof(string));
				dtPhoneNumber.Columns.Add("Job", typeof(string));
				dtPhoneNumber.Columns.Add("Address", typeof(string));
				dtPhoneNumber.Columns.Add("Email", typeof(string));
				dtPhoneNumber.Columns.Add("F1", typeof(string));
				dtPhoneNumber.Columns.Add("F2", typeof(string));
				dtPhoneNumber.Columns.Add("F3", typeof(string));
				dtPhoneNumber.Columns.Add("F4", typeof(string));
				dtPhoneNumber.Columns.Add("F5", typeof(string));
				dtPhoneNumber.Columns.Add("F6", typeof(string));
				dtPhoneNumber.Columns.Add("F7", typeof(string));
				dtPhoneNumber.Columns.Add("F8", typeof(string));
				dtPhoneNumber.Columns.Add("F9", typeof(string));
				dtPhoneNumber.Columns.Add("F10", typeof(string));
				dtPhoneNumber.Columns.Add("F11", typeof(string));
				dtPhoneNumber.Columns.Add("F12", typeof(string));
				dtPhoneNumber.Columns.Add("F13", typeof(string));
				dtPhoneNumber.Columns.Add("F14", typeof(string));
				dtPhoneNumber.Columns.Add("F15", typeof(string));
				dtPhoneNumber.Columns.Add("F16", typeof(string));
				dtPhoneNumber.Columns.Add("F17", typeof(string));
				dtPhoneNumber.Columns.Add("F18", typeof(string));
				dtPhoneNumber.Columns.Add("F19", typeof(string));
				dtPhoneNumber.Columns.Add("F20", typeof(string));
				dtPhoneNumber.Columns.Add("Sex", typeof(int));
				dtPhoneNumber.Columns.Add("PhoneBookGuid", typeof(Guid));

				foreach (Common.PhoneNumber number in lstNumbers)
				{
					row = dtPhoneNumber.NewRow();

					row["Guid"] = number.PhoneNumberGuid;
					row["FirstName"] = number.FirstName;
					row["LastName"] = number.LastName;
					if (number.BirthDate != DateTime.MinValue)
						row["BirthDate"] = number.BirthDate;
					row["CreateDate"] = number.CreateDate;
					row["Telephone"] = number.Telephone;
					row["CellPhone"] = number.CellPhone;
					row["FaxNumber"] = number.FaxNumber;
					row["Job"] = number.Job;
					row["Address"] = number.Address;
					row["Email"] = number.Email;
					row["F1"] = number.F1;
					row["F2"] = number.F2;
					row["F3"] = number.F3;
					row["F4"] = number.F4;
					row["F5"] = number.F5;
					row["F6"] = number.F6;
					row["F7"] = number.F7;
					row["F8"] = number.F8;
					row["F9"] = number.F9;
					row["F10"] = number.F10;
					row["F11"] = number.F11;
					row["F12"] = number.F12;
					row["F13"] = number.F13;
					row["F14"] = number.F14;
					row["F15"] = number.F15;
					row["F16"] = number.F16;
					row["F17"] = number.F17;
					row["F18"] = number.F18;
					row["F19"] = number.F19;
					row["F20"] = number.F20;
					row["Sex"] = number.Sex;
					row["PhoneBookGuid"] = number.PhoneBookGuid;

					dtPhoneNumber.Rows.Add(row);
				}

				if (dtPhoneNumber.Rows.Count == 0)
					throw new Exception(Language.GetString("ErrorExistRecord"));

				return phoneNumberController.InsertBulkNumbers(dtPhoneNumber, userGuid);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool TransferNumber(Guid numberGuid, Guid groupGuid)
		{
			Business.PhoneNumber phoneNumberController = new Business.PhoneNumber();
			return phoneNumberController.TransferNumber(numberGuid, groupGuid);
		}
	}
}
