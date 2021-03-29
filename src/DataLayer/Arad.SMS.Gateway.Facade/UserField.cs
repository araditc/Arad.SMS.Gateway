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
	public class UserField : FacadeEntityBase
	{
		public static DataTable GetPhoneBookDateField(Guid phoneBookGuid)
		{
			Business.UserField userFieldController = new Business.UserField();
			return userFieldController.GetPhoneBookDateField(phoneBookGuid);
		}

		public static DataTable GetPhoneBookField(Guid phoneBookGuid)
		{
			Business.UserField userFieldController = new Business.UserField();
			return userFieldController.GetPhoneBookField(phoneBookGuid);
		}

		public static bool UpdateField(int index, string fieldValue, int fieldType, Guid phoneBookGuid)
		{
			Business.UserField userFieldController = new Business.UserField();
			return userFieldController.UpdateField(index, fieldValue, fieldType, phoneBookGuid);
		}

		public static bool InsertField(string fieldValue, int fieldType, Guid phoneBookGuid)
		{
			Business.UserField userFieldController = new Business.UserField();
			return userFieldController.InsertField(fieldValue, fieldType, phoneBookGuid);
		}

		public static bool DeleteField(int index, Guid phoneBookGuid)
		{
			Business.UserField userFieldController = new Business.UserField();
			return userFieldController.DeleteField(index, phoneBookGuid);
		}

		public static DataTable GetPagedAllUserFields(Guid userGuid, string fieldName, string phoneBookName, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.UserField userFieldController = new Business.UserField();
			return userFieldController.GetPagedAllUserFields(userGuid, fieldName, phoneBookName, sortField, pageNo, pageSize, ref resultCount);
		}
	}
}
