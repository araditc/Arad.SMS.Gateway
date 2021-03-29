using Common;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Business
{
	public class Log : BusinessEntityBase
	{
		public Log(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Logs.ToString(), dataAccessProvider) { }

		public System.Data.DataTable GetPagedLogs(string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetLog = base.FetchSPDataSet("GetPagedLogs",
																							 "@PageNo", pageNo,
																							 "@PageSize", pageSize,
																							 "@SortField", sortField);
			resultCount = Helper.GetInt(dataSetLog.Tables[0].Rows[0]["RowCount"]);

			return dataSetLog.Tables[1];
		}

		public bool InsertLog(Common.Log log)
		{
			return ExecuteSPCommand("Insert",
															"@Type", log.Type,
															"@Source", log.Source,
															"@Name", log.Name,
															"@Text", log.Text,
															"@IPAddress", log.IPAddress,
															"@Browser", log.Browser,
															"@ReferenceGuid", log.ReferenceGuid,
															"@UserGuid", log.UserGuid);
		}
	}
}
