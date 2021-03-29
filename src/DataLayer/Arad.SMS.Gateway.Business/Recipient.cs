using System;
using System.Data;
using Common;
using GeneralLibrary.BaseCore;

namespace Business
{
	public class Recipient : BusinessEntityBase
	{
		public Recipient(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Recipients.ToString(), dataAccessProvider)
		{ }

		//public DataTable GetUserBulks(Guid userGuid)
		//{
		//	return base.FetchSPDataTable("GetUserBulks", "@UserGuid", userGuid);
		//}

		//public DataTable GetBulkRecipients(Guid bulkGuid)
		//{
		//	return base.FetchSPDataTable("GetBulkRecipients", "@BulkGuid", bulkGuid);
		//}

		//public void UpdateRecipientID(Guid guid, string recipientID)
		//{
		//	ExecuteSPCommand("UpdateRecipientID", "@Guid", guid,
		//																				"@RecipientID", recipientID);
		//}

		//public bool DeleteBulkRecipients(Guid smsSentGuid)
		//{
		//	return ExecuteSPCommand("DeleteBulkRecipients", "@Guid", smsSentGuid);
		//}
	}
}
