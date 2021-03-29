using Common;
using GeneralLibrary.BaseCore;

namespace Business
{
	public class BulkRequest : BusinessEntityBase
	{
		public BulkRequest(DataAccessBase dataAccessProvider = null)
			: base(TableNames.BulkRequests.ToString(), dataAccessProvider) { }

		public bool InsertRequest(string message)
		{
			return ExecuteSPCommand("InsertRequest",
															"@Message", message);
		}
	}
}
