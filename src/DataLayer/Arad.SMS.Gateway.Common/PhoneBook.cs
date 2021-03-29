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
using System.Data;
using System;

namespace Arad.SMS.Gateway.Common
{
	public class PhoneBook : GeneralLibrary.BaseCore.CommonEntityBase
	{
		private enum TableFields
		{
			Type,
			Name,
			ServiceId,
			VASRegisterKeys,
			VASUnsubscribeKeys,
			CreateDate,
			ParentGuid,
			IsPrivate,
			IsDeleted,
			AlternativeUserGuid,
			UserGuid,
			AdminGuid,
		}

		public PhoneBook()
			: base(TableNames.PhoneBooks.ToString())
		{
			AddField(TableFields.Type.ToString(), SqlDbType.TinyInt);
			AddField(TableFields.Name.ToString(), SqlDbType.NVarChar, 64);
			AddField(TableFields.ServiceId.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.VASRegisterKeys.ToString(), SqlDbType.NVarChar, 512);
			AddField(TableFields.VASUnsubscribeKeys.ToString(), SqlDbType.NVarChar, 512);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.ParentGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.IsPrivate.ToString(), SqlDbType.Bit);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.AlternativeUserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.AdminGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid PhoneBookGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public byte Type
		{
			get
			{
				return Helper.GetByte(this[TableFields.Type.ToString()]);
			}
			set
			{
				this[TableFields.Type.ToString()] = value;
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
				if (value != string.Empty)
					this[TableFields.Name.ToString()] = value;
				else
				{
					HasError = true;
					ErrorMessage = Language.GetString("CompleteGroupNameField");
				}
			}
		}

		public string ServiceId
		{
			get
			{
				return Helper.GetString(this[TableFields.ServiceId.ToString()]);
			}
			set
			{
				this[TableFields.ServiceId.ToString()] = value;
			}
		}

		public string VASRegisterKeys
		{
			get
			{
				return Helper.GetString(this[TableFields.VASRegisterKeys.ToString()]);
			}
			set
			{
				this[TableFields.VASRegisterKeys.ToString()] = value;
			}
		}

		public string VASUnsubscribeKeys
		{
			get
			{
				return Helper.GetString(this[TableFields.VASUnsubscribeKeys.ToString()]);
			}
			set
			{
				this[TableFields.VASUnsubscribeKeys.ToString()] = value;
			}
		}

		public DateTime CreateDate
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); ;
			}
			set
			{
				this[TableFields.CreateDate.ToString()] = value;
			}
		}

		public bool IsPrivate
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsPrivate.ToString()]); ;
			}
			set
			{
				this[TableFields.IsPrivate.ToString()] = value;
			}
		}

		public Guid ParentGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.ParentGuid.ToString()]); ;
			}
			set
			{
				this[TableFields.ParentGuid.ToString()] = value;
			}
		}

		public Guid AdminGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.AdminGuid.ToString()]); ;
			}
			set
			{
				this[TableFields.AdminGuid.ToString()] = value;
			}
		}

		public Guid AlternativeUserGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.AlternativeUserGuid.ToString()]); ;
			}
			set
			{
				this[TableFields.AlternativeUserGuid.ToString()] = value;
			}
		}

		public Guid UserGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.UserGuid.ToString()]); ;
			}
			set
			{
				this[TableFields.UserGuid.ToString()] = value;
			}
		}
	}
}
