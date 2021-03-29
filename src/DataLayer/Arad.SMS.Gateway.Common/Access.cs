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

namespace Arad.SMS.Gateway.Common
{
	public class Access:CommonEntityBase
	{
		public enum TableFields
		{
			ReferencePermissionsKey,
			IsDeleted,
			CreateDate,
			ServiceGuid,
		}

		public Access():base(TableNames.Accesses.ToString())
		{
			AddField(TableFields.ReferencePermissionsKey.ToString(), SqlDbType.Int);
			AddField(TableFields.ServiceGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
		}

		public Guid AccessGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public bool IsDeleted
		{
			get { return Helper.GetBool(this[TableFields.IsDeleted.ToString()]); }
			set { this[TableFields.IsDeleted.ToString()] = value; }
		}

		public int ReferencePermissionsKey
		{
			get { return Helper.GetInt(this[TableFields.ReferencePermissionsKey.ToString()]); }
			set { this[TableFields.ReferencePermissionsKey.ToString()] = value; }
		}

		public DateTime CreateDate
		{
			get { return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); }
			set { this[TableFields.CreateDate.ToString()] = value; }
		}

		public Guid ServiceGuid
		{
			get { return Helper.GetGuid(this[TableFields.ServiceGuid.ToString()]); }
			set { this[TableFields.ServiceGuid.ToString()] = value; }
		}
	}
}
