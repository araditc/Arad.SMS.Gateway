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
	public class TrafficRelay : CommonEntityBase
	{
		public enum TableFields
		{
			Title,
			Url,
			TryCount,
			CreateDate,
			IsActive,
			IsDeleted,
			UserGuid,
		}

		public TrafficRelay()
			: base(TableNames.TrafficRelays.ToString())
		{
			AddField(TableFields.Title.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.Url.ToString(), SqlDbType.NVarChar, 512);
			AddField(TableFields.TryCount.ToString(), SqlDbType.Int);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.IsActive.ToString(), SqlDbType.Bit);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid TrafficRelayGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string Title
		{
			get
			{
				return Helper.GetString(this[TableFields.Title.ToString()]);
			}
			set
			{
				this[TableFields.Title.ToString()] = value;
			}
		}

		public string Url
		{
			get
			{
				return Helper.GetString(this[TableFields.Url.ToString()]);
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
					this[TableFields.Url.ToString()] = value;
				else
				{
					HasError = true;
					ErrorMessage = Language.GetString("CompleteUrlField");
				}
			}
		}

		public int TryCount
		{
			get
			{
				return Helper.GetInt(this[TableFields.TryCount.ToString()]);
			}
			set
			{
				this[TableFields.TryCount.ToString()] = value;
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

		public bool IsActive
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsActive.ToString()]);
			}
			set
			{
				this[TableFields.IsActive.ToString()] = value;
			}
		}

		public Guid UserGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.UserGuid.ToString()]);
			}
			set
			{
				this[TableFields.UserGuid.ToString()] = value;
			}
		}
	}
}
