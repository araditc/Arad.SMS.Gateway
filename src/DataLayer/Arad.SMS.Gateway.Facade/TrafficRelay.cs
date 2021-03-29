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
	public class TrafficRelay : FacadeEntityBase
	{
		public static DataTable GetPagedTrafficRelays(Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.TrafficRelay trafficRelayController = new Business.TrafficRelay();
			return trafficRelayController.GetPagedTrafficRelays(userGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool InsertUrl(Common.TrafficRelay trafficRelay)
		{
			Business.TrafficRelay trafficRelayController = new Business.TrafficRelay();
			return trafficRelayController.InsertUrl(trafficRelay);
		}

		public static bool UpdateUrl(Common.TrafficRelay trafficRelay)
		{
			Business.TrafficRelay trafficRelayController = new Business.TrafficRelay();
			return trafficRelayController.UpdateUrl(trafficRelay);
		}

		public static Common.TrafficRelay LoadUrl(Guid urlGuid)
		{
			Business.TrafficRelay trafficRelayController = new Business.TrafficRelay();
			Common.TrafficRelay trafficRelay = new Common.TrafficRelay();
			trafficRelayController.Load(urlGuid, trafficRelay);
			return trafficRelay;
		}

		public static bool Delete(Guid guid)
		{
			Business.TrafficRelay trafficRelayController = new Business.TrafficRelay();
			return trafficRelayController.Delete(guid);
		}
	}
}
