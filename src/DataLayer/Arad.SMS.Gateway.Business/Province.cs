using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Business
{
	public class Province : BusinessEntityBase
	{
		public Province(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Provinces.ToString(), dataAccessProvider) { }

		public DataTable GetProvinces(Guid countryGuid)
		{
			return FetchSPDataTable("GetProvinces","@CountryGuid",countryGuid);
		}

		//public int GetCountProvince(int provinceID, int cityID, int type)
		//{
		//	return Helper.GetInt(FetchSPDataTable("GetCountProvince", "@ProvinceID", provinceID,
		//																			"@CityID", cityID,
		//																			"@Type", type).Rows[0]["Count"]);
		//}

		public DataRow GetProvinceOfCity(Guid cityGuid)
		{
			DataTable dt=new DataTable();
			try
			{
				 dt = FetchDataTable("SELECT [Provinces].* FROM [Provinces] INNER JOIN [Cities] ON [Cities].[ProvinceGuid] = [Provinces].[Guid] WHERE [Cities].[Guid] = @CityGuid", "@CityGuid", cityGuid);
				return dt.Rows[0];
			}
			catch
			{
				return dt.NewRow();
			}
		}
	}
}
