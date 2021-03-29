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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Facade
{
	public class Role : FacadeEntityBase
	{
		public static DataTable GetRoles(Guid userGuid)
		{
			Business.Role roleController = new Business.Role();
			return roleController.GetRoles(userGuid);
		}

		public static Common.Role LoadRole(Guid roleGuid)
		{
			Business.Role roleController = new Business.Role();
			Common.Role role = new Common.Role();
			roleController.Load(roleGuid, role);
			return role;
		}

		public static bool UpdateRole(Common.Role role)
		{
			Business.Role roleController = new Business.Role();
			return roleController.UpdateRole(role);
		}

		public static bool InsertRole(Common.Role role)
		{
			Business.Role roleController = new Business.Role();
			return roleController.InsertRole(role) != Guid.Empty ? true : false;
		}

		public static bool Delete(Guid roleGuid)
		{
			Business.Role roleController = new Business.Role();
			return roleController.Delete(roleGuid);
		}

		public static Guid GetDefaultRoleGuid(string domain)
		{
			Business.Role roleController = new Business.Role();
			return roleController.GetDefaultRoleGuid(domain);
		}

		public static DataTable GetPackage(int PackageId)
		{
			Business.Role roleController = new Business.Role();
			return roleController.GetPackage(PackageId);
		}
	}
}