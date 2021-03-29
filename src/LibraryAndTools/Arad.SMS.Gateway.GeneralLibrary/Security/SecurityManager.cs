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
using System.Data;

namespace Arad.SMS.Gateway.GeneralLibrary.Security
{
	public class SecurityManager : BaseCore.BusinessEntityBase
	{
		private static IDictionary<Guid, IList<int>> servicePermissions;
		private static IDictionary<Guid, IList<int>> accessPermissions;

		static SecurityManager()
		{
			servicePermissions = new Dictionary<Guid, IList<int>>();
			accessPermissions = new Dictionary<Guid, IList<int>>();
		}

		private SecurityManager() { }

		#region-------------- Service Permissions --------------------------
		public static bool HasServicePermission(Guid userGuid, int service)
		{
			if (!servicePermissions.ContainsKey(userGuid))
			{
				DataTable userServiceList = new SecurityManager().FetchSPDataTableWithFullSPName("Users_GetUserServices", "@UserGuid", userGuid);
				servicePermissions.Add(userGuid, new List<int>());
				foreach (DataRow userService in userServiceList.Rows)
				{
					//if (Helper.GetBool(userService["IsDefault"]) || Helper.GetBool(userService["BuyService"]))
						servicePermissions[userGuid].Add(Helper.GetInt(userService["ReferenceServiceKey"]));
				}
			}
			return servicePermissions[userGuid].Contains(service);
		}

		public static bool HasAllServicePermission(Guid userGuid, ref int errorService, params int[] services)
		{
			foreach (int service in services)
				if (!HasServicePermission(userGuid, service))
				{
					errorService = service;
					return false;
				}

			return true;
		}

		public static bool HasAtLeastOneServicePermission(Guid userGuid, params int[] services)
		{
			foreach (int service in services)
				if (HasServicePermission(userGuid, service))
					return true;

			return false;
		}

		public static void ClearServicePermissionCache(Guid userGuid)
		{
			servicePermissions.Remove(userGuid);
		}
		#endregion

		#region-------------- Access Permissions --------------------------
		public static bool HasAccessPermission(Guid userGuid, int access)
		{
			if (!accessPermissions.ContainsKey(userGuid))
			{
				DataTable userServiceList = new SecurityManager().FetchSPDataTableWithFullSPName("Users_GetUserAccesses", "@UserGuid", userGuid);
				accessPermissions.Add(userGuid, new List<int>());
				foreach (DataRow userService in userServiceList.Rows)
					accessPermissions[userGuid].Add(Helper.GetInt(userService["ReferencePermissionsKey"]));
			}

			return accessPermissions[userGuid].Contains(access);
		}

		public static bool HasAllAccessPermission(Guid userGuid, ref int errorAccess, params int[] accesses)
		{
			foreach (int access in accesses)
				if (!HasAccessPermission(userGuid, access))
				{
					errorAccess = access;
					return false;
				}

			return true;
		}

		public static bool HasAtLeastOneAccessPermission(Guid userGuid, params int[] accesses)
		{
			foreach (int access in accesses)
				if (HasAccessPermission(userGuid, access))
					return true;

			return false;
		}

		public static void ClearAccessPermissionCache(Guid userGuid)
		{
			accessPermissions.Remove(userGuid);
		}
		#endregion
	}
}
