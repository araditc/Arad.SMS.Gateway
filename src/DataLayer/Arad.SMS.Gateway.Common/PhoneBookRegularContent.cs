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

using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Arad.SMS.Gateway.Common
{
	public class PhoneBookRegularContent : CommonEntityBase
	{
		public enum TableFields
		{
			CreateDate,
			PhoneBookGuid,
			RegularContentGuid,
		}

		public PhoneBookRegularContent()
			: base(TableNames.PhoneBookRegularContents.ToString())
		{
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.PhoneBookGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.RegularContentGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public DateTime CreateDate
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]);
			}
			set
			{
				this[TableFields.CreateDate.ToString()] = value;
			}
		}

		public Guid PhoneBookGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.PhoneBookGuid.ToString()]);
			}
			set
			{
				this[TableFields.PhoneBookGuid.ToString()] = value;
			}
		}

		public Guid RegularContentGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.RegularContentGuid.ToString()]);
			}
			set
			{
				this[TableFields.RegularContentGuid.ToString()] = value;
			}
		}
	}
}
