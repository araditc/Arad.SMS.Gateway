using System;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Facade
{
	public class PriceRange : FacadeEntityBase
	{
		public static DataTable GetPagedPriceRanges(Guid userGuid, Guid groupPriceGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.PriceRange priceRangeController = new Business.PriceRange();
			return priceRangeController.GetPagedPriceRanges(userGuid, groupPriceGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public static Common.PriceRange LoadPriceRange(Guid priceRangeGuid)
		{
			Business.PriceRange priceRangeController = new Business.PriceRange();
			Common.PriceRange priceRange = new Common.PriceRange();
			priceRangeController.Load(priceRangeGuid, priceRange);
			return priceRange;
		}

		public static bool UpdatePriceRange(Common.PriceRange priceRange)
		{
			Business.PriceRange priceRangeController = new Business.PriceRange();
			return priceRangeController.UpdatePriceRange(priceRange);
		}

		public static bool Insert(Common.PriceRange priceRange)
		{
			Business.PriceRange priceRangeController = new Business.PriceRange();
			return priceRangeController.Insert(priceRange) != Guid.Empty ? true : false;
		}

		public static bool DeletePriceRange(Guid priceRangeGuid)
		{
			Business.PriceRange priceRangeController = new Business.PriceRange();
			return priceRangeController.Delete(priceRangeGuid);
		}
	}
}
