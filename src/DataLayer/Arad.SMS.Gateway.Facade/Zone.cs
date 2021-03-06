﻿// --------------------------------------------------------------------
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
	public class Zone : FacadeEntityBase
	{
		public static DataTable GetZones(Guid parentGuid)
		{
			Business.Zone zoneController = new Business.Zone();
			return zoneController.GetZones(parentGuid);
		}

		public static DataTable GetAllParents(Guid childGuid)
		{
			Business.Zone zoneController = new Business.Zone();
			return zoneController.GetAllParents(childGuid);
		}

		public static Common.Zone Load(Guid zoneGuid)
		{
			Business.Zone zoneController = new Business.Zone();
			Common.Zone zone = new Common.Zone();
			zoneController.Load(zoneGuid, zone);
			return zone;
		}
	}
}
