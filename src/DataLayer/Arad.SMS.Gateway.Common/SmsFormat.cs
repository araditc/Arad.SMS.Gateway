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
	public class SmsFormat : CommonEntityBase
	{
		public enum TableFields
		{
			Name,
			Format,
			IsDeleted,
			CreateDate,
			PhoneBookGuid,
		}

		public SmsFormat()
			: base(TableNames.SmsFormats.ToString())
		{
			AddField(TableFields.Name.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Format.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.PhoneBookGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid SmsFormatGuid
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

		public string Name
		{
			get
			{
				return Helper.GetString(this[TableFields.Name.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteSmsFormatNameField");
					HasError = true;
				}
				else
					this[TableFields.Name.ToString()] = value;
			}
		}

		public string Format
		{
			get
			{
				return Helper.GetString(this[TableFields.Format.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteSmsFormatField");
					HasError = true;
				}
				else
					this[TableFields.Format.ToString()] = value;
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
	}
}