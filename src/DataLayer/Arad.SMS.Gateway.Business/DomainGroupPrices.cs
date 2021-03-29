using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;
using Common;

namespace Business
{
	public class DomainGroupPrices : BusinessEntityBase
	{
		public DomainGroupPrices(DataAccessBase dataAccessProvider = null)
			: base(TableNames.DomainGroupPrices.ToString(), dataAccessProvider)
		{ }
		public DataTable GetPagedGroupPriceDomains(string title, Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetGroupPrice = base.FetchSPDataSet("GetPagedDomainGroupPrice", "@Title", title,
																																									"@UserGuid", userGuid,
																																									"@PageNo", pageNo,
																																									"@PageSize", pageSize,
																																									"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetGroupPrice.Tables[0].Rows[0]["RowCount"]);
			return dataSetGroupPrice.Tables[1];
		}

		public bool UpdateGroupPriceDomains(Common.DomainGroupPrices groupPrice)
		{
			return base.ExecuteSPCommand("UpdateDomainGroupPrice", "Guid", groupPrice.DomainGroupPriceGuid,
																														 "Title", groupPrice.Title);
		}
	}
}
