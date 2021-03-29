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
	public class Content : CommonEntityBase
	{
		public enum TableFields
		{
			ID,
			Text,
			SmsLen,
			IsUnicode,
			CreateDate,
			IsDeleted,
			RegularContentGuid,
		}

		public Content()
			: base(TableNames.Contents.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.BigInt);
			AddField(TableFields.Text.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.SmsLen.ToString(), SqlDbType.Int);
			AddField(TableFields.IsUnicode.ToString(), SqlDbType.Bit);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.RegularContentGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid ContentGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string Text
		{
			get
			{
				return Helper.GetString(this[TableFields.Text.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteTextField");
				}
				else
					this[TableFields.Text.ToString()] = value;
			}
		}

		public int SmsLen
		{
			get
			{
				return Helper.GetInt(this[TableFields.SmsLen.ToString()]);
			}
			set
			{
				this[TableFields.SmsLen.ToString()] = value;
			}
		}

		public bool IsUnicode
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsUnicode.ToString()]);
			}
			set
			{
				this[TableFields.IsUnicode.ToString()] = value;
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
