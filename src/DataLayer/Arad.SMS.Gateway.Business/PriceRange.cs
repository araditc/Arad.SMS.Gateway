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
	public class PriceRange : BusinessEntityBase
	{
		public PriceRange(DataAccessBase dataAccessProvider = null)
			: base(TableNames.PriceRanges.ToString(), dataAccessProvider) { }

		public DataTable GetPagedPriceRanges(Guid userGuid, Guid groupPriceGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetPriceRange = base.FetchSPDataSet("GetPagedPriceRanges", "@UserGuid", userGuid,
																																					"@GroupPriceGuid", groupPriceGuid,
																																					"@PageNo", pageNo,
																																					"@PageSize", pageSize,
																																					"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetPriceRange.Tables[0].Rows[0]["RowCount"]);
			return dataSetPriceRange.Tables[1];
		}

		public bool UpdatePriceRange(Common.PriceRange priceRange)
		{
			return base.ExecuteSPCommand("UpdatePriceRange", "@Guid", priceRange.PriceRangeGuid,
																											"@Price", priceRange.Ratio,
																											"@GroupPriceGuid", priceRange.GroupPriceGuid,
																											"@SmsSenderAgentGuid", priceRange.SmsSenderAgentGuid);
		}
	}
}
