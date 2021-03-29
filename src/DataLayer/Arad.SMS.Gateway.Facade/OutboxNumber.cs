// --------------------------------------------------------------------
// Copyright (c) 2005-2020 Arad ITC.
//
// Author : Ammar Heidari <ammar@arad-itc.org>
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------

using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.SqlLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Arad.SMS.Gateway.Facade
{
	public class OutboxNumber : FacadeEntityBase
	{
		public static DataTable GetUncertainDeliveryStatusSmsTable(SmsSenderAgentReference smsSenderAgentRefrence)
		{
			Business.OutboxNumber smsController = new Business.OutboxNumber();
			return smsController.GetUncertainDeliveryStatusSms(smsSenderAgentRefrence);
		}


		public static bool UpdateSmsSendInfo(string xmlSmsInfo)
		{
			Business.OutboxNumber smsController = new Business.OutboxNumber();
			return smsController.UpdateSmsSendInfo(xmlSmsInfo);
		}

		public static bool DeleteNumbers(Guid guid)
		{
			Business.OutboxNumber smsController = new Business.OutboxNumber();
			return smsController.DeleteNumbers(guid);
		}

		//public static void UpdateDeliveryStatus(long[] messageIDs, long[] deliveryStatus)
		//{
		//	Business.OutboxNumber smsController = new Business.OutboxNumber();
		//	Dictionary<long, List<long>> messageStatus = new Dictionary<long, List<long>>();
		//	string outerSystemMessageID = string.Empty;

		//	for (int i = 0; i < deliveryStatus.Length; i++)
		//	{
		//		if (!messageStatus.ContainsKey(deliveryStatus[i]))
		//			messageStatus.Add(deliveryStatus[i], new List<long>());

		//		messageStatus[deliveryStatus[i]].Add(messageIDs[i]);
		//	}

		//	foreach (int state in messageStatus.Keys)
		//	{
		//		outerSystemMessageID = string.Empty;
		//		foreach (long smsID in messageStatus[state])
		//			outerSystemMessageID += string.Format("'{0}',", smsID);

		//		smsController.UpdateDeliveryStatus(outerSystemMessageID.TrimEnd(','), state);
		//	}
		//}

		//public static void UpdateDeliveryStatus(string[] messageIDs, int[] deliveryStatus)
		//{
		//	Business.OutboxNumber smsController = new Business.OutboxNumber();
		//	Dictionary<int, List<string>> messageStatus = new Dictionary<int, List<string>>();

		//	for (int i = 0; i < deliveryStatus.Length; i++)
		//	{
		//		if (!messageStatus.ContainsKey(deliveryStatus[i]))
		//			messageStatus.Add(deliveryStatus[i], new List<string>());

		//		messageStatus[deliveryStatus[i]].Add(messageIDs[i]);
		//	}

		//	foreach (int state in messageStatus.Keys)
		//	{
		//		string strMessageIDs = string.Empty;
		//		foreach (string messageID in messageStatus[state])
		//			strMessageIDs += string.Format("'{0}',", messageID);

		//		smsController.UpdateDeliveryStatus(strMessageIDs.TrimEnd(','), state);
		//	}
		//}

		//public static void UpdateDeliveryStatus(string batchID, string[] numbers, int[] deliveryStatus)
		//{
		//	Business.OutboxNumber smsController = new Business.OutboxNumber();
		//	Dictionary<int, List<string>> messageStatus = new Dictionary<int, List<string>>();

		//	for (int i = 0; i < deliveryStatus.Length; i++)
		//	{
		//		if (!messageStatus.ContainsKey(deliveryStatus[i]))
		//			messageStatus.Add(deliveryStatus[i], new List<string>());

		//		messageStatus[deliveryStatus[i]].Add(numbers[i]);
		//	}

		//	foreach (int state in messageStatus.Keys)
		//	{
		//		string recievers = string.Empty;
		//		foreach (string number in messageStatus[state])
		//			recievers += string.Format("'{0}',", number);

		//		smsController.UpdateDeliveryStatus(batchID, recievers.TrimEnd(','), state);
		//	}
		//}


        public static bool UpdateGsmDeliveryStatus(DeliveryStatus status, List<string> mobiles, string batchId, DeliveryMessage deliveryMessage)
        {
            try
            {
                Business.OutboxNumber smsController = new Business.OutboxNumber();
                return smsController.UpdateGsmDeliveryStatus(status, mobiles, batchId, deliveryMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool UpdateDeliveryStatus(Guid OutboxGuid, Dictionary<DeliveryStatus, List<string>> messageStatus, long id = 0)
		{
			Business.OutboxNumber smsController = new Business.OutboxNumber();
			try
			{
				foreach (DeliveryStatus state in messageStatus.Keys)
				{
					string recievers = string.Empty;
					foreach (string number in messageStatus[state])
						recievers += string.Format("'{0}',", number);

					smsController.UpdateDeliveryStatus(OutboxGuid, recievers.TrimEnd(','), state, id);
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetPagedSmses(Guid outboxGuid, string query, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			Business.OutboxNumber outboxNumberController = new Business.OutboxNumber();
			return outboxNumberController.GetPagedSmses(outboxGuid, query, pageNo, pageSize, sortField, ref resultCount);
		}

        ///// New 8-3-2019
        public static string GetDeliveryURL(Guid pNumber)
        {
            Common.PrivateNumber privateNumber = new Common.PrivateNumber();
            DeliveryMessage deliveryMessage = new DeliveryMessage();
            privateNumber = Facade.PrivateNumber.LoadNumber(pNumber);
            deliveryMessage.DeliveryRelayGuid = privateNumber.DeliveryTrafficRelayGuid;

            return Facade.TrafficRelay.LoadUrl(deliveryMessage.DeliveryRelayGuid).Url;
        }

		public static bool UpdateDeliveryStatus(List<DeliveryMessage> lstDelivery)
		{
			Business.OutboxNumber outboxNumberController = new Business.OutboxNumber();
			string outerMessageIds = string.Empty;
			string numbers = string.Empty;
			DataTable dtDeliveryRelayInfo = new DataTable();
			DeliveryMessage deliveryMessage;
			try
			{


				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveDelivery, string.Join(Environment.NewLine, lstDelivery.Select(deliver => string.Format("agent:{0}, status:{1}, mobile:{2}, returnId:{3},batchid:{4}", deliver.Agent, deliver.Status, deliver.Mobile, deliver.ReturnId, deliver.BatchId))));

				var deliveryGroup = lstDelivery.Where(msg => msg.Agent != (int)SmsSenderAgentReference.RahyabRG &&
																										 msg.Agent != (int)SmsSenderAgentReference.SLS)
																			 .GroupBy(msg => msg.Status).Select(grp => new { key = grp.Key, lstMessages = grp.ToList() }).ToList();
				foreach (var item in deliveryGroup)
				{
					LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveDelivery, string.Format("status:{0},returnIds:{1}", item.key, string.Join(",", item.lstMessages.Select(msg => string.Format("'{0}'", msg.ReturnId)))));

					outerMessageIds = string.Join(",", item.lstMessages.Select(msg => string.Format("'{0}'", msg.ReturnId)));
					dtDeliveryRelayInfo = outboxNumberController.UpdateDeliveryStatus(outerMessageIds, item.key);
					foreach (DataRow row in dtDeliveryRelayInfo.Rows)
					{
						deliveryMessage = new DeliveryMessage();
						deliveryMessage.BatchId = row["OutboxGuid"].ToString();
						deliveryMessage.Mobile = row["ToNumber"].ToString();
						deliveryMessage.Status = Helper.GetInt(row["DeliveryStatus"]);
						deliveryMessage.PrivateNumberGuid = Helper.GetGuid(row["PrivateNumberGuid"]);
						deliveryMessage.UserGuid = Helper.GetGuid(row["UserGuid"]);

                        ///// New 8-3-2019
                        if (!string.IsNullOrEmpty(GetDeliveryURL(deliveryMessage.PrivateNumberGuid)))
						    ManageQueue.SendMessage(ManageQueue.Queues.DeliveryRelay.ToString(), deliveryMessage, string.Format("DeliveryRelay=> batchid={0},mobile={1},status={2}", deliveryMessage.BatchId, deliveryMessage.Mobile, deliveryMessage.Status));
					}
				}

				var deliveryBatchGroup = lstDelivery.Where(msg => msg.Agent == (int)SmsSenderAgentReference.RahyabRG ||
																													msg.Agent == (int)SmsSenderAgentReference.SLS)
																						.GroupBy(msg => new { msg.BatchId, msg.Status })
																						.Select(grp => new { batchId = grp.Key.BatchId, status = grp.Key.Status, lstMessages = grp.ToList() }).ToList();
				foreach (var item in deliveryBatchGroup)
				{
					LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveDelivery, string.Format("status:{0},batchid:{1},mobiles:{2}", item.status, item.batchId, string.Join(",", item.lstMessages.Select(msg => string.Format("'{0}'", msg.Mobile)))));

					numbers = string.Join(",", item.lstMessages.Select(msg => string.Format("'{0}'", msg.Mobile)));

					dtDeliveryRelayInfo = outboxNumberController.UpdateDeliveryStatus(item.batchId, numbers, item.status);
					foreach (DataRow row in dtDeliveryRelayInfo.Rows)
					{
						deliveryMessage = new DeliveryMessage();
						deliveryMessage.BatchId = row["OutboxGuid"].ToString();
						deliveryMessage.Mobile = row["ToNumber"].ToString();
						deliveryMessage.Status = Helper.GetInt(row["DeliveryStatus"]);
						deliveryMessage.PrivateNumberGuid = Helper.GetGuid(row["PrivateNumberGuid"]);
						deliveryMessage.UserGuid = Helper.GetGuid(row["UserGuid"]);

                        ///// New 8-3-2019
                        if (!string.IsNullOrEmpty(GetDeliveryURL(deliveryMessage.PrivateNumberGuid)))
                            ManageQueue.SendMessage(ManageQueue.Queues.DeliveryRelay.ToString(), deliveryMessage, string.Format("DeliveryRelay=> batchid={0},mobile={1},status={2}", deliveryMessage.BatchId, deliveryMessage.Mobile, deliveryMessage.Status));
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveDelivery, string.Format("UpdateDeliveryStatus Error Message : {0}", ex.Message));
				throw ex;
			}
		}

		public static bool InsertSentMessages(BatchMessage batch)
		{
			Business.OutboxNumber outboxNumberController = new Business.OutboxNumber();
			DataTable dtMessages = new DataTable();
			DataRow row;
			List<string> lstSaveNumber = new List<string>();

			try
			{
				dtMessages.Columns.Add("OutboxGuid", typeof(Guid));
				dtMessages.Columns.Add("ItemId", typeof(string));
				dtMessages.Columns.Add("ToNumber", typeof(string));
				dtMessages.Columns.Add("DeliveryStatus", typeof(byte));
				dtMessages.Columns.Add("SendStatus", typeof(byte));
				dtMessages.Columns.Add("ReturnId", typeof(string));
				dtMessages.Columns.Add("CheckId", typeof(string));
				dtMessages.Columns.Add("SmsSenderAgentReference", typeof(byte));

				foreach (InProgressSms sms in batch.Receivers)
				{
					LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveSentMessage,
																							 string.Format("mobile:{0},returnId:{1},deliverystatus:{2},tryCount:{3},saveToDatabase:{4},typesend:{5}",
																															sms.RecipientNumber, sms.ReturnID, sms.DeliveryStatus, sms.SendTryCount, sms.SaveToDatabase, batch.SmsSendType));
					if (sms.SaveToDatabase)
						continue;

					if ((sms.DeliveryStatus == (int)DeliveryStatus.NotSent) && batch.SmsSendType != (int)SmsSendType.SendBulkSms && sms.SendTryCount <= 20)
					{
						sms.SaveToDatabase = false;
						continue;
					}
					else if ((sms.DeliveryStatus == (int)DeliveryStatus.NotSent) && sms.SendTryCount < batch.MaximumTryCount)
					{
						sms.SaveToDatabase = false;
						continue;
					}

					row = dtMessages.NewRow();

					row["OutboxGuid"] = batch.Guid;
					row["ItemId"] = batch.PageNo;
					row["ToNumber"] = sms.RecipientNumber;
					row["SendStatus"] = Helper.GetByte((int)sms.SendStatus);
					row["DeliveryStatus"] = Helper.GetByte((int)sms.DeliveryStatus);
					row["ReturnId"] = sms.ReturnID;
					row["CheckId"] = sms.CheckID;
					row["SmsSenderAgentReference"] = Helper.GetByte((int)batch.SmsSenderAgentReference);
					sms.SaveToDatabase = true;
					lstSaveNumber.Add(sms.RecipientNumber);

					dtMessages.Rows.Add(row);

					LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveSentMessage,
																							 string.Format("mobile:{0},returnId:{1},deliverystatus:{2},tryCount:{3},saveToDatabase:{4},typesend:{5}",
																															sms.RecipientNumber, sms.ReturnID, sms.DeliveryStatus, sms.SendTryCount, sms.SaveToDatabase, batch.SmsSendType));
				}

				if (outboxNumberController.InsertSentMessages(dtMessages))
				{
					batch.Receivers.RemoveAll(sms => sms.SaveToDatabase == true);
					return true;
				}
				else
				{
					foreach (string receiver in lstSaveNumber)
						batch.Receivers.Where(sms => sms.RecipientNumber == receiver).FirstOrDefault().SaveToDatabase = false;

					return false;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
