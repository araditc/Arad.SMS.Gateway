using Common;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Business
{
	public class BlackList: BusinessEntityBase
	{
		public BlackList(DataAccessBase dataAccessProvider = null)
			: base(TableNames.BlackList.ToString(), dataAccessProvider) { }

		public DataTable GetPagedNumbers(string query, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			DataSet dataSetOutBox = base.FetchSPDataSet("GetPagedNumbers",
																									"@Query", query,
																									"@PageNo", pageNo,
																									"@PageSize", pageSize,
																									"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetOutBox.Tables[0].Rows[0]["RowCount"]);

			return dataSetOutBox.Tables[1];
		}

		public bool InsertListNumber(DataTable dtNumbers)
		{
			return ExecuteSPCommand("InsertListNumber", "@Numbers", dtNumbers); ;
		}
	}
}
