using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using Business;
using System.Data;

namespace Facade
{
	public class Bulk : FacadeEntityBase
	{
		//public static int GetCountPrefix(Business.SmsSenderAgentReference smsSenderAgentRefrence, string prefix, int type)
		//{
		//	Business.Bulk bulkController = new Business.Bulk();
		//	return bulkController.GetCountPrefix(smsSenderAgentRefrence, prefix, type);
		//}

		//public static int GetCountPostCode(Business.SmsSenderAgentReference smsSenderAgentRefrence, string postCode, int type)
		//{
		//	Business.Bulk bulkController = new Business.Bulk();
		//	return bulkController.GetCountPostCode(smsSenderAgentRefrence, postCode, type);
		//}

		//public static int GetCountProvince(Business.SmsSenderAgentReference smsSenderAgentRefrence, int provinceID, int cityID, int type)
		//{
		//	Business.Bulk bulkController = new Business.Bulk();
		//	return bulkController.GetCountProvince(smsSenderAgentRefrence, provinceID, cityID, type);
		//}

		//public static string[] GetProvinces(Business.SmsSenderAgentReference smsSenderAgentRefrence)
		//{
		//	Business.Bulk bulkController = new Business.Bulk();
		//	return bulkController.GetProvinces(smsSenderAgentRefrence);
		//}

		//public static string[] GetCities(Business.SmsSenderAgentReference smsSenderAgentRefrence, int provinceID)
		//{
		//	Business.Bulk bulkController = new Business.Bulk();
		//	return bulkController.GetCities(smsSenderAgentRefrence, provinceID);
		//}

		public static DataTable GetPagedUserBulks(Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.Bulk bulkController = new Business.Bulk();
			return bulkController.GetPagedUserBulks(userGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		//public static bool InsertBulk(Common.Bulk bulk, string recipients)
		//{
		//	Business.Bulk bulkController = new Business.Bulk();
		//	Business.Recipient recipientController = new Business.Recipient(bulkController.DataAccessProvider);
		//	Common.Recipient recipient = new Common.Recipient();

		//	bulkController.BeginTransaction();
		//	try
		//	{
		//		int countRecipients = Helper.GetInt(Helper.ImportData(recipients, "resultCount"));

		//		int recipientNumberCount = 0;
		//		for (int counterRecipients = 0; counterRecipients < countRecipients; counterRecipients++)
		//			recipientNumberCount += Helper.GetInt(Helper.ImportData(recipients, ("Count" + counterRecipients).ToString()).Replace(",", string.Empty));
		//		bulk.RecipientsNumberCount = recipientNumberCount;

		//		Guid bulkGuid = bulkController.InsertBulk(bulk);
		//		if (bulkGuid == Guid.Empty)
		//			throw new Exception("ErrorRecord");

		//		for (int counterRecipients = 0; counterRecipients < countRecipients; counterRecipients++)
		//		{
		//			recipient.RecipientID = string.Empty;
		//			recipient.Province = Helper.ImportData(recipients, ("Province" + counterRecipients).ToString());
		//			recipient.ProvinceID = Helper.GetInt(Helper.ImportData(recipients, ("ProvinceID" + counterRecipients).ToString()));
		//			recipient.City = Helper.ImportData(recipients, ("City" + counterRecipients).ToString());
		//			recipient.CityID = Helper.GetInt(Helper.ImportData(recipients, ("CityID" + counterRecipients).ToString()));
		//			recipient.Prefix = Helper.ImportData(recipients, ("Prefix" + counterRecipients).ToString());
		//			recipient.PostCode = Helper.ImportData(recipients, ("PostCode" + counterRecipients).ToString());
		//			recipient.NumberRecipientType = Helper.GetInt(Helper.ImportData(recipients, ("NumberRecipientType" + counterRecipients).ToString()));
		//			recipient.RecipientType = Helper.GetInt(Helper.ImportData(recipients, ("RecipientType" + counterRecipients).ToString()));
		//			recipient.FromIndex = Helper.GetInt(Helper.ImportData(recipients, ("FromIndex" + counterRecipients).ToString()));
		//			recipient.Count = Helper.GetInt(Helper.ImportData(recipients, ("Count" + counterRecipients).ToString()).Replace(",", string.Empty));
		//			recipient.SmsSentGuid = bulkGuid;
		//			if (recipientController.Insert(recipient) == Guid.Empty)
		//				throw new Exception("ErrorRecord");
		//		}
		//		bulkController.CommitTransaction();
		//		return true;
		//	}
		//	catch
		//	{
		//		bulkController.RollbackTransaction();
		//		throw;
		//	}
		//}

		public static bool LoadBulk(Guid bulkGuid, Common.Bulk bulk)
		{
			Business.Bulk bulkController = new Business.Bulk();
			return bulkController.Load(bulkGuid, bulk);
		}

		public static DataTable GetBulksByPriority(SmsSenderAgentReference smsSenderAgentRefrence)
		{
			Business.Bulk bulkController = new Business.Bulk();
			return bulkController.GetBulksByPriority(smsSenderAgentRefrence);
		}

		public static void UpdateBulkStatus(string[] bulkGuidList, BulkStatus status)
		{
			Business.Bulk bulkController = new Business.Bulk();
			UpdateBulkStatus(bulkGuidList, status, Business.BulkSendFaildType.None);
		}

		public static void UpdateBulkStatus(string[] bulkGuidList, BulkStatus status, BulkSendFaildType faildType)
		{
			Business.Bulk bulkController = new Business.Bulk();
			string bulkGuids = string.Empty;
			foreach (string bulk in bulkGuidList)
				bulkGuids += "'" + bulk + "',";

			bulkController.UpdateStatus(bulkGuids.TrimEnd(','), status, faildType);
		}

		public static void UpdateBulkStatus(Guid guid, BulkStatus status, BulkSendFaildType bulkSendFaildType)
		{
			Business.Bulk bulkController = new Business.Bulk();
			bulkController.UpdateBulkStatus(guid, status, bulkSendFaildType);
		}

		public static void UpdateBulkID(Guid bulkGuid, string bulkID)
		{
			Business.Bulk bulkController = new Business.Bulk();
			bulkController.UpdateBulkID(bulkGuid, bulkID);
		}

		public static DataTable GetUncertainDeleviryStatusBulk(SmsSenderAgentReference smsSenderAgentRefrence)
		{
			Business.Bulk bulkController = new Business.Bulk();
			return bulkController.GetUncertainDeleviryStatusBulk(smsSenderAgentRefrence);
		}
	}
}
