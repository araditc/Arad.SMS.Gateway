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
using System.Threading.Tasks;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.Common;
using System.Data;

namespace Arad.SMS.Gateway.Business
{
	public class PhoneBookRegularContent : BusinessEntityBase
	{
		public PhoneBookRegularContent(DataAccessBase dataAccessProvider = null)
			: base(TableNames.PhoneBookRegularContents.ToString(), dataAccessProvider) { }

		public DataTable GetRegularContents(Guid phoneBookGuid)
		{
			return FetchSPDataTable("GetRegularContents", "@PhoneBookGuid", phoneBookGuid);
		}
	}
}
