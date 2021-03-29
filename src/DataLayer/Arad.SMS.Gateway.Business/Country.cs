using Common;
using GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public class Country : BusinessEntityBase
	{
		public Country(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Countries.ToString(), dataAccessProvider) { }

		public System.Data.DataTable GetCountries()
		{
			return FetchDataTable("SELECT * FROM [Countries] ORDER BY [ID]");
		}
	}
}
