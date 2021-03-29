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
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;

namespace Arad.SMS.Gateway.Common
{
	public class LoginStat : CommonEntityBase
	{
		public enum TableFields
		{
			IP,
			Type,
			CreateDate,
			UserGuid
		}

		public LoginStat()
			: base(TableNames.LoginStats.ToString())
		{
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.IP.ToString(), SqlDbType.NVarChar);
			AddField(TableFields.Type.ToString(), SqlDbType.Int);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
		}

		public Guid LoginStatGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string IP
		{
			get { return Helper.GetString(this[TableFields.IP.ToString()]); }
			set { this[TableFields.IP.ToString()] = value; }
		}

		public int Type
		{
			get { return Helper.GetInt(this[TableFields.Type.ToString()]); }
			set { this[TableFields.Type.ToString()] = value; }
		}

		public Guid UserGuid
		{
			get { return Helper.GetGuid(this[TableFields.UserGuid.ToString()]); }
			set { this[TableFields.UserGuid.ToString()] = value; }
		}

		public DateTime CreateDate
		{
			get { return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); }
			set { this[TableFields.CreateDate.ToString()] = value; }
		}
	}
}
