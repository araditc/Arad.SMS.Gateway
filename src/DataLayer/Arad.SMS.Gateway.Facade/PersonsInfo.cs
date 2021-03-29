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
using System.Collections.Generic;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.Facade
{
	public class PersonsInfo : FacadeEntityBase
	{
		public static int GetCount(Guid zoneGuid, string prefix, string zipcode, Business.NumberType type, int opt)
		{
			Business.PersonsInfo personController = new Business.PersonsInfo();
			return personController.GetCount(zoneGuid, prefix, zipcode, type, opt);
		}

		public static DataTable GetPagedBlackListNumbers(string query, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			Business.PersonsInfo personController = new Business.PersonsInfo();
			return personController.GetPagedBlackListNumbers(query, pageNo, pageSize, sortField, ref resultCount);
		}

		public static bool UpdateBlackListStatus(Guid guid,bool isBlackList)
		{
			Business.PersonsInfo personController = new Business.PersonsInfo();
			return personController.UpdateBlackListStatus(guid,isBlackList);
		}

		public static bool UpdateBlackListStatus(List<string> lstNumbers, bool isBlackList)
		{
			Business.PersonsInfo personController = new Business.PersonsInfo();
			DataTable dtNumbers = new DataTable();
			dtNumbers.Columns.Add("Mobile", typeof(string));
			DataRow row;
			foreach (string number in lstNumbers)
			{
				row = dtNumbers.NewRow();

				if (Helper.IsCellPhone(number) > 0)
					row["Mobile"] = Helper.GetLocalMobileNumber(number);

				dtNumbers.Rows.Add(row);
			}

			return personController.UpdateBlackListStatus(dtNumbers, isBlackList);
		}
	}
}
