using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Facade
{
	public class DomainGroupPrices : FacadeEntityBase
	{
		public static DataTable GetPagedDomainGroupPrices(string title, Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.DomainGroupPrices groupPriceController = new Business.DomainGroupPrices();
			return groupPriceController.GetPagedGroupPriceDomains(title, userGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool UpdateDomainGroupPrices(Common.DomainGroupPrices groupPrice)
		{
			Business.DomainGroupPrices groupPriceController = new Business.DomainGroupPrices();
			return groupPriceController.UpdateGroupPriceDomains(groupPrice);
		}

		public static bool Insert(Common.DomainGroupPrices groupPrice)
		{
			Business.DomainGroupPrices groupPriceController = new Business.DomainGroupPrices();
			return groupPriceController.Insert(groupPrice) != Guid.Empty ? true : false;
		}

		public static bool DeleteDomainGroupPrice(Guid groupPriceGuid)
		{
			Business.DomainGroupPrices groupPriceController = new Business.DomainGroupPrices();
			return groupPriceController.Delete(groupPriceGuid);
		}
	}
}