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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;

namespace Arad.SMS.Gateway.Facade
{
	public class LoginStat : FacadeEntityBase
	{
		public static DataTable GetUserLoginStats(Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.LoginStat loginStatController = new Business.LoginStat();
			return loginStatController.GetUserLoginStats(userGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool Insert(Common.LoginStat loginStat)
		{
			Business.LoginStat loginStatController = new Business.LoginStat();
			return loginStatController.Insert(loginStat) != Guid.Empty ? true : false;
		}
	}
}
