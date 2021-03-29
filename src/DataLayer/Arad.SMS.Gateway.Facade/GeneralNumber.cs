using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GeneralLibrary;
using System.Xml.Linq;

namespace Facade
{
	public class GeneralNumber
	{
		public static DataTable GetPagedNumbers(Common.GeneralNumber generalNumber, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.GeneralNumber generalNumberController = new Business.GeneralNumber();
			return generalNumberController.GetPagedNumbers(generalNumber, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool IsDuplicateNumber(Guid userGuid, Guid generalPhoneBookGuid, Business.CheckNumberScope scope, string cellPhone)
		{
			Business.GeneralNumber generalNumberController = new Business.GeneralNumber();
			return generalNumberController.IsDuplicateNumber(userGuid, generalPhoneBookGuid, scope, cellPhone);
		}

		public static bool InsertNumber(Common.GeneralNumber generalNumber)
		{
			Business.GeneralNumber generalNumberController = new Business.GeneralNumber();
			return generalNumberController.InsertNumber(generalNumber);
		}

		public static bool InsertListNumber(Guid generalPhoneBookGuid, ref List<string> lstNumbers, ref List<string> lstFailedNumbers, ref int countNumberDuplicate)
		{
			Business.GeneralNumber generalNumberController = new Business.GeneralNumber();
			try
			{
				//Outbox.GetCountNumberOfOperators(ref lstNumbers, ref lstFailedNumbers);

				XDocument doc = new XDocument();
				XElement root = new XElement("NewDataSet");

				foreach (string number in lstNumbers)
				{
					XElement element = new XElement("Table");
					element.Add(new XElement("CellPhone", number));
					root.Add(element);
				}
				doc.Add(root);

				return generalNumberController.InsertListNumber(generalPhoneBookGuid, doc.ToString(), ref countNumberDuplicate);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static Common.GeneralNumber LoadNumber(Guid generalNumberGuid)
		{
			Business.GeneralNumber generalNumberController = new Business.GeneralNumber();
			return generalNumberController.LoadNumber(generalNumberGuid);
		}

		public static bool UpdateNumber(Common.GeneralNumber generalNumber, Guid userGuid, Business.CheckNumberScope scope)
		{
			Business.GeneralNumber generalNumberController = new Business.GeneralNumber();
			return generalNumberController.UpdateNumber(generalNumber, userGuid, scope);
		}

		public static bool DeleteGeneralNumber(Guid guid)
		{
			Business.GeneralNumber generalNumberController = new Business.GeneralNumber();
			return generalNumberController.Delete(guid);
		}

		public static long GetCountNumberOfPhoneBook(Guid generalPhoneBookGuid)
		{
			Business.GeneralNumber generalNumberController = new Business.GeneralNumber();
			return generalNumberController.GetCountNumberOfPhoneBook(generalPhoneBookGuid);
		}

		public static bool DeleteNumber(Guid guid)
		{
			Business.GeneralNumber generalNumberController = new Business.GeneralNumber();
			return generalNumberController.Delete(guid);
		}

		//public static Dictionary<Business.Operators, int> GetCountNumberOfOperators(Guid phoneBookGuid, long downRange, long upRange)
		//{
		//	Business.GeneralNumber generalNumberController = new Business.GeneralNumber();
		//	//DataRow dataRowSmsInfo = generalNumberController.GetCountNumberOfOperators(phoneBookGuid.ToString(), downRange, upRange);
		//	Dictionary<Business.Operators, int> operatorCountNumberDictionary = new Dictionary<Business.Operators, int>();
		//	//if (Helper.GetInt(dataRowSmsInfo["MCI"]) > 0)
		//	//  operatorCountNumberDictionary.Add(Business.Operators.MCI, Helper.GetInt(dataRowSmsInfo["MCI"]));
		//	//if (Helper.GetInt(dataRowSmsInfo["Irancell"]) > 0)
		//	//  operatorCountNumberDictionary.Add(Business.Operators.Irancell, Helper.GetInt(dataRowSmsInfo["Irancell"]));
		//	//if (Helper.GetInt(dataRowSmsInfo["RighTel"]) > 0)
		//	//  operatorCountNumberDictionary.Add(Business.Operators.Rightel, Helper.GetInt(dataRowSmsInfo["RightTel"]));
		//	return operatorCountNumberDictionary;
		//}

		public static DataTable GetPagedPhoneNumbers(Guid phoneBookGuid, int pageNo, int pageSize)
		{
			Business.GeneralNumber generalNumberController = new Business.GeneralNumber();
			return generalNumberController.GetPagedPhoneNumbers(phoneBookGuid, pageNo, pageSize);
		}

		public static DataTable GetLimitedPagedPhoneNumbers(Guid phoneBookGuid, long downRange, int pageSize)
		{
			Business.GeneralNumber generalNumberController = new Business.GeneralNumber();
			return generalNumberController.GetLimitedPagedPhoneNumbers(phoneBookGuid, downRange, pageSize);
		}

		//public static Dictionary<Business.Operators, int> GetCountNumberOfOperators(string phoneBookGuid, long downRange, long upRange)
		//{
		//	Business.GeneralNumber generalNumberController = new Business.GeneralNumber();
		//	DataTable dtSmsInfo = generalNumberController.GetCountNumberOfOperators(phoneBookGuid, downRange, upRange);
		//	Dictionary<Business.Operators, int> operatorCountNumberDictionary = new Dictionary<Business.Operators, int>();

		//	foreach (DataRow dataRow in dtSmsInfo.Rows)
		//	{
		//		switch (Helper.GetInt(dataRow["Operator"]))
		//		{
		//			case (int)Business.Operators.MCI:
		//				operatorCountNumberDictionary.Add(Business.Operators.MCI, Helper.GetInt(dataRow["Count"]));
		//				break;
		//			case (int)Business.Operators.MTN:
		//				operatorCountNumberDictionary.Add(Business.Operators.MTN, Helper.GetInt(dataRow["Count"]));
		//				break;
		//			case (int)Business.Operators.Rightel:
		//				operatorCountNumberDictionary.Add(Business.Operators.Rightel, Helper.GetInt(dataRow["Count"]));
		//				break;
		//		}
		//	}
		//	return operatorCountNumberDictionary;
		//}

		//public static DataTable GetNumbersInfo(Guid phoneBookGuid)
		//{
		//	Business.GeneralNumber generalNumberController = new Business.GeneralNumber();
		//	return generalNumberController.GetNumbersInfo(phoneBookGuid);
		//}
	}
}
