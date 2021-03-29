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
	public class RegularContent : CommonEntityBase
	{
		public enum TableFields
		{
			Title,
			Type,
			Config,
			IsActive,
			PeriodType,
			Period,
			EffectiveDateTime,
			WarningType,
			CreateDate,
			StartDateTime,
			EndDateTime,
			IsDeleted,
			PrivateNumberGuid,
			UserGuid
		}

		public RegularContent()
			: base(TableNames.RegularContents.ToString())
		{
			AddField(TableFields.Title.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.Type.ToString(), SqlDbType.TinyInt);
			AddField(TableFields.Config.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.IsActive.ToString(), SqlDbType.Bit);
			AddField(TableFields.PeriodType.ToString(), SqlDbType.TinyInt);
			AddField(TableFields.Period.ToString(), SqlDbType.Int);
			AddField(TableFields.EffectiveDateTime.ToString(), SqlDbType.DateTime);
			AddField(TableFields.WarningType.ToString(), SqlDbType.TinyInt);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.StartDateTime.ToString(), SqlDbType.DateTime);
			AddField(TableFields.EndDateTime.ToString(), SqlDbType.DateTime);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.PrivateNumberGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid RegularContentGuid
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
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteTitleField");
				}
				else
					this[TableFields.Title.ToString()] = value;
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
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteTypeField");
				}
				else
					this[TableFields.Type.ToString()] = value;
			}
		}

		public string Config
		{
			get
			{
				return Helper.GetString(this[TableFields.Config.ToString()]);
			}
			set
			{
				this[TableFields.Config.ToString()] = value;
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

		public int PeriodType
		{
			get
			{
				return Helper.GetInt(this[TableFields.PeriodType.ToString()]);
			}
			set
			{
				this[TableFields.PeriodType.ToString()] = value;
			}
		}

		public int Period
		{
			get
			{
				return Helper.GetInt(this[TableFields.Period.ToString()]);
			}
			set
			{
				this[TableFields.Period.ToString()] = value;
			}
		}

		public DateTime EffectiveDateTime
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.EffectiveDateTime.ToString()]);
			}
			set
			{
				this[TableFields.EffectiveDateTime.ToString()] = value;
			}
		}

		public int WarningType
		{
			get
			{
				return Helper.GetInt(this[TableFields.WarningType.ToString()]);
			}
			set
			{
				this[TableFields.WarningType.ToString()] = value;
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

		public DateTime StartDateTime
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.StartDateTime.ToString()]);
			}
			set
			{
				this[TableFields.StartDateTime.ToString()] = value;
			}
		}

		public DateTime EndDateTime
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.EndDateTime.ToString()]);
			}
			set
			{
				this[TableFields.EndDateTime.ToString()] = value;
			}
		}

		public bool IsDeleted
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsDeleted.ToString()]);
			}
			set
			{
				this[TableFields.IsDeleted.ToString()] = value;
			}
		}

		public Guid PrivateNumberGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.PrivateNumberGuid.ToString()]);
			}
			set
			{
				this[TableFields.PrivateNumberGuid.ToString()] = value;
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
