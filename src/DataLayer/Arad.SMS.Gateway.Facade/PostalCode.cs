using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Facade
{
	public class PostalCode : FacadeEntityBase
	{
		public static int GetCountPostCode(string postCode)
		{
			Business.PostalCode postalCodeController = new Business.PostalCode();
			return postalCodeController.GetCountPostCode(postCode);
		}

		public static DataTable GetPagedPostalCodeNumbers(string postalCode, long downRange, int pageSize)
		{
			Business.PostalCode postalCodeController = new Business.PostalCode();
			return postalCodeController.GetPagedPostalCodeNumbers(postalCode, downRange, pageSize);
		}
	}
}