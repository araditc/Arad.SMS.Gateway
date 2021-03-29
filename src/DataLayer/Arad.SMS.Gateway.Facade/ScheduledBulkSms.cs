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

using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Arad.SMS.Gateway.Facade
{
	public class ScheduledBulkSms : FacadeEntityBase
	{
		public static DataTable GetQueue(int count)
		{
			Business.ScheduledBulkSms scheduledSmsController = new Business.ScheduledBulkSms();
			return scheduledSmsController.GetQueue(count);
		}

		public static bool ProcessRequest(Guid guid,
																			long id,
																			Common.ScheduledSmsStatus scheduledSmsStatus,
																			Guid privateNumberGuid,
																			Guid userGuid,
																			int smsSenderAgentReference,
																			bool sendBulkIsAutomatic,
																			bool sendSmsAlert,
																			TimeSpan startSendTime,
																			TimeSpan endSendTime)
		{
			Business.ScheduledBulkSms scheduledSmsController = new Business.ScheduledBulkSms();
			return scheduledSmsController.ProcessRequest(guid, id, scheduledSmsStatus, privateNumberGuid,
																									 userGuid, smsSenderAgentReference, sendBulkIsAutomatic,
																									 sendSmsAlert, startSendTime, endSendTime);
		}
	}
}
