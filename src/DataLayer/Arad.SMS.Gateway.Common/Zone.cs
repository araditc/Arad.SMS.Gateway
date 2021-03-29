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
	public class Zone : CommonEntityBase
	{
		private enum TableFields
		{
			ID,
			Name,
			ISOCode,
			CountryCode,
			CreateDate,
			ParentGuid,
		}

		public Zone()
			: base(TableNames.Zones.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.Int);
			AddField(TableFields.Name.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.ISOCode.ToString(), SqlDbType.Char, 3);
			AddField(TableFields.CountryCode.ToString(), SqlDbType.Char, 5);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.ParentGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid ZoneGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public int ID
		{
			get
			{
				return Helper.GetInt(this[TableFields.ID.ToString()]);
			}
			set
			{
				this[TableFields.ID.ToString()] = value;
			}
		}

		public string Name
		{
			get
			{
				return Helper.GetString(this[TableFields.Name.ToString()]);
			}
			set
			{
				this[TableFields.Name.ToString()] = value;
			}
		}

		public string ISOCode
		{
			get
			{
				return Helper.GetString(this[TableFields.ISOCode.ToString()]);
			}
			set
			{
				this[TableFields.ISOCode.ToString()] = value;
			}
		}

		public string CountryCode
		{
			get
			{
				return Helper.GetString(this[TableFields.CountryCode.ToString()]);
			}
			set
			{
				this[TableFields.CountryCode.ToString()] = value;
			}
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

		public Guid ParentGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.ParentGuid.ToString()]);
			}
			set
			{
				this[TableFields.ParentGuid.ToString()] = value;
			}
		}
	}
}
