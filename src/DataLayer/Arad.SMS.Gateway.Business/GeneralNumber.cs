using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using Common;

namespace Business
{
	public class GeneralNumber : BusinessEntityBase
	{
		public GeneralNumber(DataAccessBase dataAccessProvider = null)
			: base(TableNames.GeneralNumbers.ToString(), dataAccessProvider) { }

		public bool IsDuplicateNumber(Guid userGuid, Guid generalPhoneBookGuid, Business.CheckNumberScope scope, string cellPhone)
		{
			DataTable dataTableGeneralNumberStatus = new DataTable();
			dataTableGeneralNumberStatus = base.FetchSPDataTable("NumberStatus", "@Scope", (int)scope,
																																								"@CellPhone", cellPhone,
																																								"@GeneralPhoneBookGuid", generalPhoneBookGuid,
																																								"@UserGuid", userGuid);
			return dataTableGeneralNumberStatus.Rows.Count > 0 ? true : false;
		}

		public DataTable GetPagedNumbers(Common.GeneralNumber generalNumber, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet numbersInfo = base.FetchSPDataSet("GetPagedNumbers",
																										 "@GeneralPhoneBookGuid", generalNumber.GeneralPhoneBookGuid,
																										 "@FirstName", generalNumber.FirstName,
																										 "@LastName", generalNumber.LastName,
																										 "@BirthDate", generalNumber.BirthDate,
																										 "@Sex", generalNumber.Sex,
																										 "@CellPhone", generalNumber.CellPhone,
																										 "@Email", generalNumber.Email,
																										 "@Job", generalNumber.Job,
																										 "@Telephone", generalNumber.Telephone,
																										 "@FaxNumber", generalNumber.FaxNumber,
																										 "@Address", generalNumber.Address,
																										 "@PageNo", pageNo,
																										 "@PageSize", pageSize,
																										 "@SortField", sortField);
			resultCount = Helper.GetInt(numbersInfo.Tables[0].Rows[0]["RowCount"]);
			return numbersInfo.Tables[1];
		}

		public bool InsertListNumber(Guid generalPhoneBookGuid, string numbersXml, ref int countNumberDuplicate)
		{
			try
			{
				DataTable dtNumbersInfo = FetchSPDataTable("InsertListNumber", "@NumbersXml", numbersXml,
																																			 "@GeneralPhoneBookGuid", generalPhoneBookGuid);
				countNumberDuplicate = Helper.GetInt(dtNumbersInfo.Rows.Count > 0 ? dtNumbersInfo.Rows[0]["CountDuplicateNumbers"] : 0);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public Common.GeneralNumber LoadNumber(Guid numberGuid)
		{
			Common.GeneralNumber generalNumber = new Common.GeneralNumber();
			DataRow dataRowNumberInfo = base.FetchDataTable("SELECT * FROM [GeneralNumbers] WHERE [IsDeleted]=0 AND [Guid]=@Guid", "@Guid", numberGuid).Rows[0];
			generalNumber.FirstName = Helper.GetString(dataRowNumberInfo["FirstName"]);
			generalNumber.LastName = Helper.GetString(dataRowNumberInfo["LastName"]);
			generalNumber.BirthDate = Helper.GetDateTime(dataRowNumberInfo["Birthdate"].ToString());
			generalNumber.Telephone = Helper.GetString(dataRowNumberInfo["Telephone"]);
			generalNumber.CellPhone = Helper.GetString(dataRowNumberInfo["CellPhone"]);
			generalNumber.FaxNumber = Helper.GetString(dataRowNumberInfo["FaxNumber"]);
			generalNumber.Job = Helper.GetString(dataRowNumberInfo["Job"]);
			generalNumber.Address = Helper.GetString(dataRowNumberInfo["Address"]);
			generalNumber.Email = Helper.GetString(dataRowNumberInfo["Email"]);

			if (!Helper.CheckDataConditions(dataRowNumberInfo["Sex"]).IsEmpty) generalNumber.Sex = Helper.GetInt(dataRowNumberInfo["Sex"]);
			return generalNumber;
		}

		public bool UpdateNumber(Common.GeneralNumber generalNumber, Guid userGuid, Business.CheckNumberScope scope)
		{
			try
			{
				if (!IsDuplicateNumber(userGuid, generalNumber.GeneralPhoneBookGuid, scope, generalNumber.CellPhone))
					throw new Exception(Language.GetString("ErrorDuplicateNumber"));

				return base.ExecuteSPCommand("UpdateNumber", "@Guid", generalNumber.GeneralNumberGuid,
																										"@FirstName", generalNumber.FirstName,
																										"@LastName", generalNumber.LastName,
																										"@BirthDate", generalNumber.BirthDate,
																										"@Telephone", generalNumber.Telephone,
																										"@CellPhone", generalNumber.CellPhone,
																										"@FaxNumber", generalNumber.FaxNumber,
																										"@Job", generalNumber.Job,
																										"@Address", generalNumber.Address,
																										"@Email", generalNumber.Email,
																										"@Sex", generalNumber.Sex);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public long GetCountNumberOfPhoneBook(Guid GeneralPhoneBookGuid)
		{
			return Helper.GetLong(base.FetchDataTable("SELECT COUNT(*) AS [RowCount] FROM [GeneralNumbers] WHERE [GeneralPhoneBookGuid] = @GeneralPhoneBookGuid",
																								"@GeneralPhoneBookGuid", GeneralPhoneBookGuid).Rows[0]["RowCount"]);
		}

		public bool InsertNumber(Common.GeneralNumber generalNumber)
		{
			Guid guid = Guid.NewGuid();
			return base.ExecuteSPCommand("InsertNumber", "@Guid", guid,
																									"@FirstName", generalNumber.FirstName,
																									"@LastName", generalNumber.LastName,
																									"@BirthDate", generalNumber.BirthDate,
																									"@Telephone", generalNumber.Telephone,
																									"@CellPhone", generalNumber.CellPhone,
																									"@FaxNumber", generalNumber.FaxNumber,
																									"@Job", generalNumber.Job,
																									"@Address", generalNumber.Address,
																									"@Email", generalNumber.Email,
																									"@CreateDate", generalNumber.CreateDate,
																									"@Sex", generalNumber.Sex,
																									"@GeneralPhoneBookGuid", generalNumber.GeneralPhoneBookGuid);
		}

		public DataTable GetCountNumberOfOperators(string phoneBookGuid, long downRange, long upRange)
		{
			return FetchSPDataTable("GetCountNumberOfOperators", "@PhoneBookGuid", phoneBookGuid,
																													 "@DownRange", downRange,
																													 "@UpRange", upRange);
		}

		public DataTable GetPagedPhoneNumbers(Guid phoneBookGuid, int pageNo, int pageSize)
		{
			return FetchSPDataTable("GetPagedPhoneNumbers", "@PhoneBookGuid", phoneBookGuid,
																							 "@PageNo", pageNo,
																							 "@PageSize", pageSize);
		}

		public DataTable GetLimitedPagedPhoneNumbers(Guid phoneBookGuid, long downRange, int pageSize)
		{
			return FetchSPDataTable("GetLimitedPagedPhoneNumbers", "@PhoneBookGuid", phoneBookGuid,
																														 "@DownRange", downRange,
																														 "@PageSize", pageSize);
		}

		//public DataTable GetNumbersInfo(Guid phoneBookGuid)
		//{
		//	return FetchDataTable("SELECT Count(*) AS [Count],[Operator] FROM [dbo].[GeneralNumbers] WHERE [GeneralPhoneBookGuid] = @PhoneBookGuid AND [IsDeleted] = 0 GROUP By [Operator]",
		//												"@PhoneBookGuid", phoneBookGuid);
		//}
	}
}