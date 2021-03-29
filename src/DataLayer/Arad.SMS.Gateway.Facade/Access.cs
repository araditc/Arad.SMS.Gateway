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
	public class Access:FacadeEntityBase
	{
		public static DataTable GetPagedAccess(Guid serviceGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.Access accessController = new Business.Access();
			return accessController.GetPagedAccess(serviceGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool insert(Common.Access access)
		{
			Business.Access accessController = new Business.Access();
			if (accessController.Insert(access) != Guid.Empty)
				return true;
			else
				return false;
		}

		public static bool UpdateAccess(Common.Access access)
		{
			Business.Access accessController = new Business.Access();
			return accessController.UpdateAccess(access);
		}

		public static Common.Access LoadAccess(Guid AccessGuid)
		{
			Business.Access accessController = new Business.Access();
			Common.Access access = new Common.Access();
			accessController.Load(AccessGuid, access);
			return access;
		}

		public static bool Delete(Guid AccessGuid)
		{
			Business.Access accessController = new Business.Access();
			return accessController.Delete(AccessGuid);
		}
	}
}
