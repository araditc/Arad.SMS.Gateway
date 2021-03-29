using GeneralLibrary.BaseCore;

namespace Facade
{
	public class BulkRequest : FacadeEntityBase
	{
		public static bool InsertRequest(string message)
		{
			Business.BulkRequest bulkRequestController = new Business.BulkRequest();
			return bulkRequestController.InsertRequest(message);
		}
	}
}
