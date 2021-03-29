using System;
using System.Data;
using Common;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Collections.Generic;

namespace Business
{
	public class Bulk : BusinessEntityBase
	{
		public Bulk(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Bulks.ToString(), dataAccessProvider)	{ }

		//public string CreateBulk(Common.Bulk bulk, string senderNumber)
		//{
		//  return magfaBulkWebService.createBulk(bulk.SmsBody, senderNumber, bulk.SmsType, DateManager.GetSolarDate(bulk.SendDateTime), bulk.SendDateTime.Hour, bulk.SendDateTime.Minute);
		//}

		//public string AddRecipientsByCityProvince(Common.Recipient recipient, string bulkID)
		//{
		//  return magfaBulkWebService.addRecipientsByCityProvince(bulkID, recipient.ProvinceID, recipient.CityID, recipient.NumberRecipientType, recipient.FromIndex, recipient.Count);
		//}

		//public static string AddRecipientsByCityProvince(Common.Recipient recipient, string bulkID, ref string errorMessage)
		//{
		//  string recipientID = recipientController.AddRecipientsByCityProvince(recipient, bulkID);
		//  foreach (BulkError bulkError in System.Enum.GetValues(typeof(BulkError)))
		//    if (recipientID == ((int)bulkError).ToString())
		//      errorMessage = Language.GetString(bulkError.ToString());

		//  if (errorMessage == string.Empty && Helper.GetInt(recipientID) != 0)
		//  {
		//    Guid guid = recipientController.Insert(recipient);
		//    if (guid == Guid.Empty)
		//      errorMessage = Language.GetString("ErrorRecord");
		//  }

		//  return errorMessage;
		//}

		//public int GetCountPrefix(SmsSenderAgentReference smsSenderAgentReference, string prefix, int type)
		//{
		//	return WinServiceHandler.SmsSendWinServiceHandlerChannel().GetCountPrefix(smsSenderAgentReference, prefix, type);
		//}

		//public int GetCountPostCode(SmsSenderAgentReference smsSenderAgentReference, string postCode, int type)
		//{
		//	return WinServiceHandler.SmsSendWinServiceHandlerChannel().GetCountPostCode(smsSenderAgentReference, postCode, type);
		//}

		//public int GetCountProvince(SmsSenderAgentReference smsSenderAgentReference, int provinceID, int cityID, int type)
		//{
		//	return WinServiceHandler.SmsSendWinServiceHandlerChannel().GetCountProvince(smsSenderAgentReference, provinceID, cityID, type);
		//}

		//public string[] GetProvinces(SmsSenderAgentReference smsSenderAgentReference)
		//{
		//	return WinServiceHandler.SmsSendWinServiceHandlerChannel().GetProvinces(smsSenderAgentReference);
		//}

		//public string[] GetCities(SmsSenderAgentReference smsSenderAgentReference, int provinceID)
		//{
		//	return WinServiceHandler.SmsSendWinServiceHandlerChannel().GetCities(smsSenderAgentReference, provinceID);
		//}

		public DataTable GetPagedUserBulks(Guid userGuid, string sortField, int pageNo, int pageSize, ref int rowCount)
		{
			DataSet dataSetBulkInfo = base.FetchSPDataSet("GetPagedUserBulks", "@UserGuid", userGuid,
																																 "@PageNo", pageNo,
																																 "@PageSize", pageSize,
																																 "@SortField", sortField);
			rowCount = Helper.GetInt(dataSetBulkInfo.Tables[1].Rows[0]["RowCount"]);

			return dataSetBulkInfo.Tables[0];

		}

		public DataTable GetBulksByPriority(SmsSenderAgentReference smsSenderAgentRefrence)
		{
			return FetchSPDataTable("GetBulkByPriority", "@SmsSenderAgentRefrence", (int)smsSenderAgentRefrence);
		}

		public void UpdateStatus(string bulkGuids, BulkStatus status, BulkSendFaildType faildType)
		{
			ExecuteSPCommand("UpdateBulkListStatus", "@BulkGuids", bulkGuids,
																							"@Status", (int)status,
																							"@FaildType", (int)faildType);
		}

		public void UpdateBulkStatus(Guid guid, BulkStatus status, BulkSendFaildType bulkSendFaildType)
		{
			ExecuteSPCommand("UpdateBulkStatus", "@BulkGuid", guid,
																					"@Status", (int)status,
																					"@SendFaildType", (int)bulkSendFaildType);
		}

		public Guid InsertBulk(Common.Bulk bulk)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				ExecuteSPCommand("InsertBulk", "@Guid", guid,
																					"@BulkID", bulk.BulkID,
																					"@SmsBody", bulk.SmsBody,
																					"@PresentType", bulk.PresentType,
																					"@Status", bulk.Status,
																					"@SmsCount", bulk.SmsCount,
																					"@Encoding", bulk.Encoding,
																					"@RecipientsNumberCount", bulk.RecipientsNumberCount,
																					"@SendDateTime", bulk.SendDateTime,
																					"@CreateDate", bulk.CreateDate,
																					"@UserPrivateNumberGuid", bulk.UserPrivateNumberGuid,
																					"@UserGuid", bulk.UserGuid);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
			}
			return guid;
		}

		public void UpdateBulkID(Guid bulkGuid, string bulkID)
		{
			ExecuteSPCommand("UpdateBulkID", "@BulkGuid", bulkGuid, "@BulkID", bulkID);
		}

		public DataTable GetUncertainDeleviryStatusBulk(SmsSenderAgentReference smsSenderAgentRefrence)
		{
			return FetchSPDataTable("GetUncertainDeleviryStatusBulk", "@SmsSenderAgentRefrence", smsSenderAgentRefrence);
		}
	}
}
