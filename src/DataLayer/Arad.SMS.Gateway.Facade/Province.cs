using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using System.Data;
using GeneralLibrary;

namespace Facade
{
	public class Province : FacadeEntityBase
	{
		//public static Dictionary<int, string> GetProvinceList()
		//{
		//	Business.Province provinceController = new Business.Province();
		//	Dictionary<int, string> dictionaryProvince = new Dictionary<int, string>();
		//	DataTable dtProvince = provinceController.GetProvinces();

		//	foreach (DataRow row in dtProvince.Rows)
		//		dictionaryProvince.Add(Helper.GetInt(row["ID"]), row["Name"].ToString());

		//	return dictionaryProvince;
		//}

		public static DataTable GetProvinces(Guid countryGuid)
		{
			Business.Province provinceController = new Business.Province();
			return provinceController.GetProvinces(countryGuid);
		}

		//public static int GetCountProvince(int provinceID, int cityID, int type)
		//{
		//	Business.Province provinceController = new Business.Province();
		//	return provinceController.GetCountProvince(provinceID, cityID, type);
		//}

		public static Guid GetProvinceOfCity(Guid cityGuid)
		{
			Business.Province provinceController = new Business.Province();
			return Helper.GetGuid(provinceController.GetProvinceOfCity(cityGuid)["Guid"]);
		}
	}
}
