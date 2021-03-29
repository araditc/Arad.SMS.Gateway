using System.Data;
using GeneralLibrary.BaseCore;
using System;

namespace Facade
{
	public class City : FacadeEntityBase
	{
		public static string[] GetCities(int provinceID)
		{
			Business.City cityController = new Business.City();
			return cityController.GetCities(provinceID);
		}

		public static DataTable GetCities(Guid provinceGuid)
		{
			Business.City cityController = new Business.City();
			return cityController.GetCities(provinceGuid);
		}

		public static DataTable GetPagedCityNumbers(int provinceID, int cityID, Business.NumberType numberRecipientType, long downRange, int pageSize)
		{
			Business.City cityController = new Business.City();
			return cityController.GetPagedCityNumbers(provinceID, cityID,numberRecipientType, downRange, pageSize);
		}
	}
}
