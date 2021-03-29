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
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Business
{
	public class Role : BusinessEntityBase
	{
		public Role(DataAccessBase dataAccessProvider = null) :
			base(Common.TableNames.Roles.ToString(), dataAccessProvider) { }

		public DataTable GetRoles(Guid userGuid)
		{
			return FetchSPDataTable("GetRoles", "@UserGuid", userGuid);
		}

		public bool UpdateRole(Common.Role role)
		{
			return ExecuteSPCommand("UpdateRole",
															"@Guid", role.RoleGuid,
															"@Title", role.Title,
															"@Priority", role.Priority,
															"@IsDefault", role.IsDefault,
															"@IsSalePackage", role.IsSalePackage,
															"@Price", role.Price,
															"@Description", role.Description);
		}

		public Guid InsertRole(Common.Role role)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				ExecuteSPCommand("InsertRole",
												 "@Guid", guid,
												 "@Title", role.Title,
												 "@Priority", role.Priority,
												 "@CreateDate", role.CreateDate,
												 "@UserGuid", role.UserGuid,
												 "@IsDefault", role.IsDefault,
												 "@IsSalePackage", role.IsSalePackage,
												 "@Price", role.Price,
												 "@Description", role.Description);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}

		public Guid GetDefaultRoleGuid(string domain)
		{
			return base.GetSPGuidFieldValue("GetDefaultRoleGuid", "@Domain", domain);
		}

		public DataTable GetPackage(int PackageId)
		{
			return FetchSPDataTable("GetPackage", "@ID", PackageId);
		}
	}
}
