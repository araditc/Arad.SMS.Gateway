using Common;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Business
{
	public class PostalCode : BusinessEntityBase
	{
		public PostalCode(DataAccessBase dataAccessProvider = null)
			: base(TableNames.PostalCodes.ToString(), dataAccessProvider) { }

		public int GetCountPostCode(string postCode)
		{
			return Helper.GetInt(FetchSPDataTable("GetCountPostalCode", "@PostalCode", postCode).Rows[0]["Count"]);
		}

		public DataTable GetPostalCodeNumbers(string postalCode, int from, int count)
		{
			return FetchSPDataTable("GetPostalCodeNumbers", "@PostalCode", postalCode,
																										 "@From", from,
																										 "@Count", count);
		}

		public DataTable GetPagedPostalCodeNumbers(string postalCode, long downRange, int pageSize)
		{
			return FetchSPDataTable("GetPagedPostalCodeNumbers",
																"@PostalCode", postalCode,
																"@DownRange", downRange,
																"@PageSize", pageSize);
		}
	}
}
