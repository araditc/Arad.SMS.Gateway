using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Facade
{
	public class DomainPrice:FacadeEntityBase
	{
		public static Common.DomainPrice Load(Guid guid)
		{
			Business.DomainPrice DomainPriceController = new Business.DomainPrice();
			Common.DomainPrice domainPrice = new Common.DomainPrice();
			DomainPriceController.Load(guid, domainPrice);
			return domainPrice;
		}

		public static DataTable GetPagedDomainPrices(Guid domainGroupPriceGuid,Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.DomainPrice DomainPriceController = new Business.DomainPrice();
			return DomainPriceController.GetPagedDomainPrices(domainGroupPriceGuid,userGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool UpdateDomainPrice(Common.DomainPrice domainPrice)
		{
			Business.DomainPrice DomainPriceController = new Business.DomainPrice();
			return DomainPriceController.UpdateDomainPrice(domainPrice);
		}

		public static DataTable GetDataTableDomainPrice(Guid userGuid, Business.DomainExtention extention, int period)
		{
			Business.DomainPrice DomainPriceController = new Business.DomainPrice();
			return DomainPriceController.GetDomainPrice(userGuid, extention, period);
		}

		public static decimal GetDomainPrice(Guid userGuid, Business.DomainExtention extention, int period)
		{
			Business.DomainPrice DomainPriceController = new Business.DomainPrice();
			DataTable dataTableDomainPrice= DomainPriceController.GetDomainPrice(userGuid, extention, period);
			if (dataTableDomainPrice.Rows.Count > 0)
				return Helper.GetDecimal(dataTableDomainPrice.Rows[0]["Price"]);
			else
				return -1;
		}

		public static bool Insert(Common.DomainPrice domainPrice)
		{
			Business.DomainPrice groupPriceController = new Business.DomainPrice();
			return groupPriceController.Insert(domainPrice) != Guid.Empty ? true : false;
		}
		
		public static bool DeleteDomainPrice(Guid PriceDomainGuid)
		{
			Business.DomainPrice groupPriceController = new Business.DomainPrice();
			return groupPriceController.Delete(PriceDomainGuid);
		}
	}
}
