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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.Common
{
	public class Role : CommonEntityBase
	{
		public enum TableFields
		{
			ID,
			Priority,
			Title,
			CreateDate,
			UserGuid,
			IsDefault,
			IsDeleted,
			IsSalePackage,
			Price,
			Description
		}

		public Role()
			: base(TableNames.Roles.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.BigInt);
			AddField(TableFields.Priority.ToString(), SqlDbType.TinyInt);
			AddField(TableFields.Title.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.IsDefault.ToString(), SqlDbType.Bit);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddReadOnlyField(TableFields.IsSalePackage.ToString(), SqlDbType.Bit);
			AddField(TableFields.Price.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.Description.ToString(), SqlDbType.NVarChar, short.MaxValue);
		}

		public Guid RoleGuid
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

		public long ID
		{
			get
			{
				return Helper.GetLong(this[TableFields.ID.ToString()]);
			}
			set
			{
				this[TableFields.ID.ToString()] = value;
			}
		}

		public byte Priority
		{
			get
			{
				return Helper.GetByte(this[TableFields.Priority.ToString()]);
			}
			set
			{
				this[TableFields.Priority.ToString()] = value;
			}
		}

		public string Title
		{
			get
			{
				return Helper.GetString(this[TableFields.Title.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteTitleField");
					HasError = true;
				}
				else
				{
					this[TableFields.Title.ToString()] = value;
				}
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

		public bool IsDefault
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsDefault.ToString()]);
			}
			set
			{
				this[TableFields.IsDefault.ToString()] = value;
			}
		}

		public bool IsSalePackage
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsSalePackage.ToString()]);
			}
			set
			{
				this[TableFields.IsSalePackage.ToString()] = value;
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

		public decimal Price
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.Price.ToString()]);
			}
			set
			{
				if (value >= 0)
					this[TableFields.Price.ToString()] = value;
				else
				{
					ErrorMessage += Language.GetString("PriceValueIsNotValid");
					HasError = true;
				}
			}
		}
	}
}