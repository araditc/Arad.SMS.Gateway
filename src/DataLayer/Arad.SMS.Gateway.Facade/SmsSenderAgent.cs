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
using System;
using System.Data;

namespace Arad.SMS.Gateway.Facade
{
	public class SmsSenderAgent : FacadeEntityBase
	{
		public static DataTable GetUserAgents(Guid userGuid)
		{
			Business.SmsSenderAgent smsSenderAgentController = new Business.SmsSenderAgent();
			return smsSenderAgentController.GetUserAgents(userGuid);
		}

		public static Common.SmsSenderAgent LoadAgent(Guid smsSenderAgentGuid)
		{
			Business.SmsSenderAgent smsSenderAgentController = new Business.SmsSenderAgent();
			Common.SmsSenderAgent smsSenderAgent = new Common.SmsSenderAgent();
			smsSenderAgentController.Load(smsSenderAgentGuid, smsSenderAgent);
			return smsSenderAgent;
		}

		public static bool UpdateAgent(Common.SmsSenderAgent smsSenderAgent)
		{
			Business.SmsSenderAgent smsSenderAgentController = new Business.SmsSenderAgent();
			try
			{
				return smsSenderAgentController.UpdateAgent(smsSenderAgent);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool Insert(Common.SmsSenderAgent smsSenderAgent)
		{
			Business.SmsSenderAgent smsSenderAgentController = new Business.SmsSenderAgent();
			try
			{
				return smsSenderAgentController.InsertAgent(smsSenderAgent) != Guid.Empty ? true : false;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static Guid GetFirstParentMainAdmin(Guid userGuid)
		{
			Business.SmsSenderAgent smsSenderAgentController = new Business.SmsSenderAgent();
			return smsSenderAgentController.GetFirstParentMainAdmin(userGuid);
		}

		public static void GetAgentInfo(SmsSenderAgentReference smsSenderAgentRefrence, out bool isSendActive, out bool isRecieveActive, out bool isSendBulkActive, out bool checkMessageID)
		{
			Business.SmsSenderAgent smsSenderAgentController = new Business.SmsSenderAgent();
			DataTable smsSenderAgentInfo = smsSenderAgentController.GetSmsSenderAgentInfo(smsSenderAgentRefrence);

			if (smsSenderAgentInfo.Rows.Count > 0)
			{
				isSendActive = Helper.GetBool(smsSenderAgentInfo.Rows[0]["IsSendActive"]);
				isRecieveActive = Helper.GetBool(smsSenderAgentInfo.Rows[0]["IsRecieveActive"]);
				isSendBulkActive = Helper.GetBool(smsSenderAgentInfo.Rows[0]["IsSendBulkActive"]);
				checkMessageID = Helper.GetBool(smsSenderAgentInfo.Rows[0]["checkMessageID"]);
			}
			else
			{
				isSendActive = false;
				isRecieveActive = false;
				isSendBulkActive = false;
				checkMessageID = false;
			}
		}

		public static void ChangeStatus(SmsSenderAgentReference smsSenderAgentRefrence, bool isSendActive, bool isRecieveActive, bool isSendBulkActive)
		{
			Business.SmsSenderAgent smsSenderAgentController = new Business.SmsSenderAgent();
			smsSenderAgentController.ChangeStatus(smsSenderAgentRefrence, isSendActive, isRecieveActive, isSendBulkActive);
		}

		public static decimal GetCredit(SmsSenderAgentReference smsSenderAgentRefrence)
		{
			Business.SmsSenderAgent smsSenderAgentController = new Business.SmsSenderAgent();
			return smsSenderAgentController.GetCredit(smsSenderAgentRefrence);
		}

		public static DataTable GetAgentRatio(Guid agentGuid)
		{
			DataTable dtAgentRatio = Facade.AgentRatio.GetPagedAgentRatio(agentGuid);
			return dtAgentRatio;
		}
	}
}
