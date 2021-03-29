using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using Common;
using System.Data;

namespace Business
{
	public class City : BusinessEntityBase
	{
		public City(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Cities.ToString(), dataAccessProvider) { }

		public string[] GetCities(int provinceID)
		{
			DataTable dtCities = FetchSPDataTable("GetCitiesOfProvince", "@ProvinceID", provinceID);
			List<string> cities = new List<string>();
			foreach (DataRow row in dtCities.Rows)
				cities.Add(row["ID"].ToString() + ":" + row["Name"].ToString());

			return cities.ToArray();
		}

		public DataTable GetCities(Guid provinceGuid)
		{
			return FetchDataTable("SELECT * FROM [Cities] WHERE [ProvinceGuid] = @ProvinceGuid", "@ProvinceGuid", provinceGuid);
		}

		public DataTable GetCityNumbers(int provinceID, int cityID, int from, int count, int type)
		{
			return FetchSPDataTable("GetCityNumbers", "@ProvinceID", provinceID,
																							"@CityID", cityID,
																							"@From", from,
																							"@Count", count,
																							"@Type", type);
		}

		public DataTable GetPagedCityNumbers(int provinceID, int cityID, NumberType numberRecipientType, long downRange, int pageSize)
		{
			return FetchSPDataTable("GetPagedCityNumbers","@ProvinceID", provinceID,
																										"@CityID", cityID,
																										"@Type", (int)numberRecipientType,
																										"@DownRange", downRange,
																										"@PageSize", pageSize);
		}
	}
}
