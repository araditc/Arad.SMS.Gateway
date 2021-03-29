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
using System.Linq;
using System.Text;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;

namespace Arad.SMS.Gateway.Facade
{
	public class ServiceGroup : FacadeEntityBase
	{
		public static DataTable GetPagedServiceGroup(string title, string sortField, int pageNo, int pageSize, ref int rowCount)
		{
			Business.ServiceGroup serviceGroupController = new Business.ServiceGroup();
			return serviceGroupController.GetPagedServiceGroup(title, sortField, pageNo, pageSize, ref rowCount);
		}

		public static DataTable GetAllGroupsWithServices(Guid userGuid)
		{
			Business.ServiceGroup serviceGroupController = new Business.ServiceGroup();
			return serviceGroupController.GetAllGroupsWithServices(userGuid);
		}

		public static bool Insert(Common.ServiceGroup serviceGroup)
		{
			Business.ServiceGroup serviceGroupController = new Business.ServiceGroup();
			return serviceGroupController.Insert(serviceGroup) != Guid.Empty ? true : false;
		}

		public static Common.ServiceGroup LoadServiceGroup(Guid ServiceGroupGuid)
		{
			Business.ServiceGroup serviceGroupController = new Business.ServiceGroup();
			Common.ServiceGroup serviceGroup = new Common.ServiceGroup();
			serviceGroupController.Load(ServiceGroupGuid, serviceGroup);
			return serviceGroup;
		}

		public static bool UpdateServiceGroup(Common.ServiceGroup serviceGroup)
		{
			Business.ServiceGroup serviceGroupController = new Business.ServiceGroup();
			return serviceGroupController.UpdateServiceGroup(serviceGroup);
		}

		public static bool Delete(Guid serviceGroupGuid)
		{
			Business.ServiceGroup serviceGroupController = new Business.ServiceGroup();
			return serviceGroupController.Delete(serviceGroupGuid);
		}

		public static DataTable GetParentGroups()
		{
			try
			{
				Business.ServiceGroup serviceGroupController = new Business.ServiceGroup();
				return serviceGroupController.GetParentGroups();
			}
			catch
			{
				return new DataTable();
			}
		}

		public static DataTable GetUserGroupsWithServices(Guid userGuid)
		{
			Business.ServiceGroup serviceGroupController = new Business.ServiceGroup();
			return serviceGroupController.GetUserGroupsWithServices(userGuid);
		}
	}
}
