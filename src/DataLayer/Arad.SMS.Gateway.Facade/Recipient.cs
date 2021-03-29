using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;
using Business;

namespace Facade
{
	public class Recipient : FacadeEntityBase
	{
		//public static DataTable GetUserBulks(Guid userGuid)
		//{
		//	Business.Recipient recipientController = new Business.Recipient();
		//	return recipientController.GetUserBulks(userGuid);
		//}

		//public static DataTable GetBulkRecipients(Guid bulkGuid)
		//{
		//	Business.Recipient recipientController = new Business.Recipient();
		//	return recipientController.GetBulkRecipients(bulkGuid);
		//}

		//public static void UpdateRecipientID(Guid guid, string recipientID)
		//{
		//	Business.Recipient recipientController = new Business.Recipient();
		//	recipientController.UpdateRecipientID(guid, recipientID);
		//}

		public static Common.Recipient LoadRecipient(Guid recipientGuid)
		{
			Common.Recipient recipient=new Common.Recipient();
			Business.Recipient recipientController = new Business.Recipient();
			recipientController.Load(recipientGuid, recipient);
			return recipient;
		}

		//public static List<Guid> GetBulkGuidRecipients(Guid smsSentGuid)
		//{
		//	List<Guid> recipientGuidList = new List<Guid>();
		//	DataTable dataTableRecipient = GetBulkRecipients(smsSentGuid);
		//	foreach (DataRow row in dataTableRecipient.Rows)
		//		recipientGuidList.Add(Helper.GetGuid(row["Guid"]));
		//	return recipientGuidList;
		//}
	}
}
