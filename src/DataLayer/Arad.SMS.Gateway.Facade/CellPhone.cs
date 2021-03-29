using GeneralLibrary.BaseCore;
using System.Data;

namespace Facade
{
	public class CellPhone : FacadeEntityBase
	{
		public static DataTable GetPagedPrefixNumbers(string prefix, long downRange, int pageSize)
		{
			Business.CellPhone cellPhoneController = new Business.CellPhone();
			return cellPhoneController.GetPagedPrefixNumbers(prefix, downRange, pageSize);
		}

		public static int GetCountPrefix(string prefix, int type)
		{
			Business.CellPhone cellPhoneController = new Business.CellPhone();
			return cellPhoneController.GetCountPrefix(prefix,type);
		}
	}
}
