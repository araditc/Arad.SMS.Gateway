using GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
	public class Country : FacadeEntityBase
	{
		public static DataTable GetCountries()
		{
			Business.Country countryController = new Business.Country();
			return countryController.GetCountries();
		}
	}
}
