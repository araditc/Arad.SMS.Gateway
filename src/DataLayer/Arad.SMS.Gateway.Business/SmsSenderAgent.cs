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
using System.Data;
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Business
{
	public class SmsSenderAgent : BusinessEntityBase
	{
		public SmsSenderAgent(DataAccessBase dataAccessProvider = null)
			: base(TableNames.SmsSenderAgents.ToString(), dataAccessProvider)
		{
			this.OnEntityChange += new EntityChangeEventHandler(OnSmsSenderAgentChange);
		}

		#region Event Handlers
		private void OnSmsSenderAgentChange(object sender, EntityChangeEventArgs e)
		{
			try
			{
				if (e != null && e.ActionType == EntityChangeActionTtype.Delete)
					WinServiceHandler.SmsSendWinServiceHandlerChannel().SetStatus((SmsSenderAgentReference)this.GetSmsSenderAgentRefrence(Helper.GetGuid(sender)), false, false, false);
				else if (sender is Common.SmsSenderAgent)
				{
					Common.SmsSenderAgent smsSenderAgent = (sender as Common.SmsSenderAgent);

					WinServiceHandler.SmsSendWinServiceHandlerChannel().SetStatus((SmsSenderAgentReference)smsSenderAgent.SmsSenderAgentReference, smsSenderAgent.IsSendActive, smsSenderAgent.IsRecieveActive, smsSenderAgent.IsSendBulkActive);
				}
			}
			catch { }
		}
		#endregion

		public DataTable GetUserAgents(Guid userGuid)
		{
			return FetchSPDataTable("GetUserAgents", "@userGuid", userGuid);
		}

		public bool UpdateAgent(Common.SmsSenderAgent smsSenderAgent)
		{
			//string checkInfoValidResult = IsSmsSenderAgentInfoUnique(smsSenderAgent.PrimaryKey, smsSenderAgent.SmsSenderAgentReference, smsSenderAgent.Name);
			//if (checkInfoValidResult != "OK")
			//	throw new Exception(checkInfoValidResult);

			bool retValue = false;
			//base.BeginTransaction();
			//try
			//{
			retValue = base.ExecuteSPCommand("UpdateAgent",
																			 "@Guid", smsSenderAgent.SmsSenderAgentGuid,
																			 "@Name", smsSenderAgent.Name,
																			 "@SmsSenderAgentReference", smsSenderAgent.SmsSenderAgentReference,
																			 "@Type", smsSenderAgent.Type,
																			 "@DefaultNumber", smsSenderAgent.DefaultNumber,
																			 "@StartSendTime", smsSenderAgent.StartSendTime,
																			 "@EndSendTime", smsSenderAgent.EndSendTime,
																			 "@SendSmsAlert",smsSenderAgent.SendSmsAlert,
																			 "@IsSendActive", smsSenderAgent.IsSendActive,
																			 "@IsRecieveActive", smsSenderAgent.IsRecieveActive,
																			 "@IsSendBulkActive", smsSenderAgent.IsSendBulkActive,
																			 "@SendBulkIsAutomatic", smsSenderAgent.SendBulkIsAutomatic,
																			 "@CheckMessageID", smsSenderAgent.CheckMessageID,
																			 "@RouteActive", smsSenderAgent.RouteActive,
																			 "@IsSmpp", smsSenderAgent.IsSmpp,
																			 "@Username", smsSenderAgent.Username,
																			 "@Password", smsSenderAgent.Password,
																			 "@SendLink", smsSenderAgent.SendLink,
																			 "@ReceiveLink", smsSenderAgent.ReceiveLink,
																			 "@DeliveryLink", smsSenderAgent.DeliveryLink,
																			 "@Domain", smsSenderAgent.Domain,
																			 "@QueueLength", smsSenderAgent.QueueLength);

			//	if (retValue)
			//		OnSmsSenderAgentChange(smsSenderAgent, null);

			//	base.CommitTransaction();
			//}
			//catch (Exception ex)
			//{
			//	base.RollbackTransaction();
			//	throw ex;
			//}

			return retValue;
		}

		public Guid GetFirstParentMainAdmin(Guid userGuid)
		{
			return GetSPGuidFieldValue("GetFirstParentMainAdmin", "@UserGuid", userGuid);
		}

		public Guid InsertAgent(Common.SmsSenderAgent smsSenderAgent)
		{
			return base.Insert(smsSenderAgent);
		}

		public DataTable GetSmsSenderAgentInfo(SmsSenderAgentReference smsSenderAgentRefrence)
		{
			return base.FetchSPDataTable("GetSmsSenderAgentInfo", "@SmsSenderAgentRefrence", (int)smsSenderAgentRefrence);
		}

		public void ChangeStatus(SmsSenderAgentReference smsSenderAgentRefrence, bool isSendActive, bool isRecieveActive, bool isSendBulkActive)
		{
			base.ExecuteSPCommand("UpdateStatus", "@SmsSenderAgentReference", (int)smsSenderAgentRefrence,
																						"@IsSendActive", isSendActive,
																						"@IsRecieveActive", isRecieveActive,
																						"@IsSendBulkActive", isSendBulkActive);
		}

		public decimal GetCredit(SmsSenderAgentReference smsSenderAgentRefrence)
		{
			try
			{
				return (decimal)WinServiceHandler.SmsSendWinServiceHandlerChannel().GetCredit(smsSenderAgentRefrence);
			}
			catch
			{
				return 0;
			}
		}

		public int GetSmsSenderAgentRefrence(Guid smsSenderAgentGuid)
		{
			return base.GetSPIntFieldValue("GetSmsSenderAgentRefrense", "@SmsSenderAgentGuid", smsSenderAgentGuid);
		}
	}
}
