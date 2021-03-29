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
	public class DomainPrice:BusinessEntityBase
	{
		public DomainPrice(DataAccessBase dataAccessProvider = null)
			: base(TableNames.DomainPrices.ToString(), dataAccessProvider) { }

		public DataTable GetDomainPrice(Guid userGuid, Business.DomainExtention extention, int period)
		{
			return  base.FetchSPDataTable("GetDomainPrice", "@UserGuid", userGuid,
																											"@Extention", (int)extention,
																											"@Period", period);
		}

		public DataTable GetPagedDomainPrices(Guid domainGroupPriceGuid, Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetDomainPrices= base.FetchSPDataSet("GetPagedDomainPrice","@DomainGroupPriceGuid",domainGroupPriceGuid,
																																							"@UserGuid", userGuid,
																																							"@PageNo", pageNo,
																																							"@PageSize", pageSize,
																																							"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetDomainPrices.Tables[0].Rows[0]["RowCount"]);
			return dataSetDomainPrices.Tables[1];
		}

		public bool UpdateDomainPrice(Common.DomainPrice domainPrice)
		{
			return base.ExecuteSPCommand("UpdateDomainPrice", "Guid", domainPrice.DomainPriceGuid,
																												"DomainGroupPriceGuid", domainPrice.DomainGroupPriceGuid,
																												"Extention",domainPrice.Extention,
																												"Period",domainPrice.Period,
																												"Price",domainPrice.Price);
		}
	}
}
