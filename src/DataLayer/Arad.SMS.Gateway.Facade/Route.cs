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
	public class Route : FacadeEntityBase
	{
		public static DataTable GetPagedRoutes(Guid agentGuid)
		{
			Business.Route routeController = new Business.Route();
			return routeController.GetPagedRoutes(agentGuid);
		}

		public static bool Insert(Common.Route route)
		{
			Business.Route routeController = new Business.Route();
			return routeController.Insert(route) != Guid.Empty ? true : false;
		}

		public static Common.Route Load(Guid routeGuid)
		{
			Common.Route route = new Common.Route();
			Business.Route routeController = new Business.Route();

			routeController.Load(routeGuid, route);
			return route;
		}

		public static bool Update(Common.Route route)
		{
			Business.Route routeController = new Business.Route();
			return routeController.Update(route);
		}
	}
}
