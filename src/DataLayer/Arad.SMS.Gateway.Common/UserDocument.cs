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
	public class UserDocument : CommonEntityBase
	{
		private enum TableFields
		{
			Type,
			Key,
			Value,
			Status,
			Description,
			CreateDate,
			UserGuid
		}

		public UserDocument()
			: base(TableNames.UserDocuments.ToString())
		{
			AddField(TableFields.Type.ToString(), SqlDbType.Int);
			AddField(TableFields.Key.ToString(), SqlDbType.Int);
			AddField(TableFields.Value.ToString(), SqlDbType.NVarChar, 255);
			AddField(TableFields.Status.ToString(), SqlDbType.Int);
			AddField(TableFields.Description.ToString(), SqlDbType.NVarChar, 512);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid UserDocumentGuid
		{
			get
			{
				return PrimaryKey;
			}
			set
			{
				PrimaryKey = value;
			}
		}

		public int Type
		{
			get
			{
				return Helper.GetInt(this[TableFields.Type.ToString()]);
			}
			set
			{
				this[TableFields.Type.ToString()] = value;
			}
		}

		public int Key
		{
			get
			{
				return Helper.GetInt(this[TableFields.Key.ToString()]);
			}
			set
			{
				this[TableFields.Key.ToString()] = value;
			}
		}

		public string Value
		{
			get
			{
				return Helper.GetString(this[TableFields.Value.ToString()]);
			}
			set
			{
				this[TableFields.Value.ToString()] = value;
			}
		}

		public int Status
		{
			get
			{
				return Helper.GetInt(this[TableFields.Status.ToString()]);
			}
			set
			{
				this[TableFields.Status.ToString()] = value;
			}
		}

		public string Description
		{
			get
			{
				return Helper.GetString(this[TableFields.Description.ToString()]);
			}
			set
			{
				this[TableFields.Description.ToString()] = value;
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
