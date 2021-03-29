using GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Facade
{
	public class Log : FacadeEntityBase
	{
		public static DataTable GetPagedLogs(string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.Log logController = new Business.Log();
			return logController.GetPagedLogs(sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool InsertLog(Common.Log log)
		{
			Business.Log logController = new Business.Log();
			return logController.InsertLog(log);
		}
	}
}
