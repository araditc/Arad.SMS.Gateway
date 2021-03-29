using GeneralLibrary.BaseCore;
using Common;
using System.Data;
using GeneralLibrary;

namespace Business
{
	public class CellPhone:BusinessEntityBase
	{
		public CellPhone(DataAccessBase dataAccessProvider = null)
			: base(TableNames.CellPhones.ToString(), dataAccessProvider) { }

		public DataTable GetPagedPrefixNumbers(string prefix, long downRange, int pageSize)
		{
			return FetchSPDataTable("GetPagedPrefixNumbers", "@Prefix", prefix,
																											"@DownRange", downRange,
																											"@PageSize", pageSize);
		}

		public int GetCountPrefix(string prefix, int type)
		{
			return Helper.GetInt(FetchSPDataTable("GetCountPrefix", "@Prefix", prefix,"@Type",type).Rows[0]["Count"]);
		}
	}
}
