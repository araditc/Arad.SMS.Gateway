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

using System;
using System.Collections.Generic;
using System.Data;
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Linq;
using Arad.SMS.Gateway.SqlLibrary;



namespace Arad.SMS.Gateway.Business
{
	public class OutboxNumber : BusinessEntityBase
	{
		public OutboxNumber(DataAccessBase dataAccessProvider = null)
			: base(TableNames.OutboxNumbers.ToString(), dataAccessProvider) { }

		public DataTable GetUncertainDeliveryStatusSms(SmsSenderAgentReference smsSenderAgentRefrence)
		{
			return FetchSPDataTable("GetUncertainDeliveryStatusSms", "@SmsSenderAgentRefrence", (int)smsSenderAgentRefrence);
		}

		public bool UpdateSmsSendInfo(string xmlSmsInfo)
		{
			return ExecuteSPCommand("UpdateSmsSendInfo", "@XmlSmsInfo", xmlSmsInfo);
		}

		public bool DeleteNumbers(Guid guid)
		{
			return ExecuteSPCommand("DeleteNumbers", "@OutboxGuid", guid);
		}

		public DataTable UpdateDeliveryStatus(string outerSystemMessageID, int state)
		{
			return FetchSPDataTable("UpdateSmsDeliveryStatus", "@OuterSystemSmsIDs", outerSystemMessageID, "@DeliveryStatus", state);
		}

		public DataTable UpdateDeliveryStatus(string batchID, string numbers, int state)
		{
			return FetchSPDataTable("UpdateSmsDeliveryStatusByNumber", "@BatchID", batchID, "@Numbers", numbers, "@DeliveryStatus", state);
		}

		public bool UpdateDeliveryStatus(Guid outboxGuid, string numbers, DeliveryStatus state, long id = 0)
		{
			return ExecuteSPCommand("UpdateSmsDeliveryStatusManually",
															"@OutboxGuid", outboxGuid,
															"@ID", id,
															"@Numbers", numbers,
															"@DeliveryStatus", (int)state);
		}

		public DataTable GetPagedSmses(Guid outboxGuid, string query, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			DataSet dataSetSms = base.FetchSPDataSet("GetPagedSmses",
																							 "@OutboxGuid", outboxGuid,
																							 "@Query", query,
																							 "@PageNo", pageNo,
																							 "@PageSize", pageSize,
																							 "@SortField", sortField);
			resultCount = Helper.GetInt(dataSetSms.Tables[0].Rows[0]["RowCount"]);
			return dataSetSms.Tables[1];
		}

		public bool UpdateDeliveryStatus(DataTable dtDeliveryStatus)
		{
			return ExecuteSPCommand("UpdateDeliveryStatus", "@DeliveryStatus", dtDeliveryStatus);
		}

		public bool InsertSentMessages(DataTable dtMessages)
		{
			return ExecuteSPCommand("InsertSentMessages", "@SentMessage", dtMessages);
		}

		public bool UpdateGsmDeliveryStatus(DeliveryStatus status,List<string> mobiles, string batchId, DeliveryMessage deliveryMessage)
		{
            int msgstatus = 3;
            if (status == DeliveryStatus.SentAndReceivedbyPhone)
                msgstatus = 1;
            if (status == DeliveryStatus.NotSent)
                msgstatus = 10;

            try
			{
				string mobile = string.Empty; ;
				DataTable dtSavedNumbers = FetchSPDataTable("UpdateGsmDeliveryStatus",
																										"@Status", (int)status,
																										"@Mobiles", string.Join(",", mobiles.Select(number => string.Format("'{0}'", number))),
																										"@ReturnId", batchId);
				foreach (DataRow row in dtSavedNumbers.Rows)
				{
					mobile = row["Mobile"].ToString();

                    deliveryMessage.BatchId = row["OutboxGuid"].ToString();
                    deliveryMessage.PrivateNumberGuid = Helper.GetGuid(row["PrivateNumberGuid"]);
                    deliveryMessage.UserGuid = Helper.GetGuid(row["UserGuid"]);

                    deliveryMessage.Mobile = mobile;
                    deliveryMessage.Status = msgstatus;
                    ManageQueue.SendMessage(ManageQueue.Queues.DeliveryRelay.ToString(), deliveryMessage, string.Format("DeliveryRelay=> batchid={0},mobile={1},status={2}", batchId, mobile, msgstatus));

                    if (mobiles.Contains(mobile))
						mobiles.Remove(mobile);
				}

                return true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
	}
}
