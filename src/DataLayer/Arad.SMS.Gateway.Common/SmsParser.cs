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
	public class SmsParser : CommonEntityBase
	{
		public enum TableFields
		{
			Title,
			Type,
			CreateDate,
			FromDateTime,
			ToDateTime,
			TypeConditionSender,
			ConditionSender,
			ReplyPrivateNumberGuid,
			ReplySmsText,
			DuplicatePrivateNumberGuid,
			DuplicateUserSmsText,
			Scope,
			IsActive,
			IsDeleted,
			UserGuid,
			PrivateNumberGuid,
		}

		public SmsParser()
			: base(TableNames.SmsParsers.ToString())
		{
			AddField(TableFields.Title.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Type.ToString(), SqlDbType.Int);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.FromDateTime.ToString(), SqlDbType.DateTime);
			AddField(TableFields.ToDateTime.ToString(), SqlDbType.DateTime);
			AddField(TableFields.TypeConditionSender.ToString(), SqlDbType.Int);
			AddField(TableFields.ConditionSender.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.ReplyPrivateNumberGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.ReplySmsText.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.DuplicatePrivateNumberGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.DuplicateUserSmsText.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.Scope.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.IsActive.ToString(), SqlDbType.Bit);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.PrivateNumberGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid SmsParserGuid
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
					ErrorMessage += Language.GetString("CompleteTitleField");
					HasError = true;
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
				this[TableFields.Type.ToString()] = value;
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

		public DateTime FromDateTime
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.FromDateTime.ToString()]);
			}
			set
			{
				this[TableFields.FromDateTime.ToString()] = value;
			}
		}

		public DateTime ToDateTime
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.ToDateTime.ToString()]);
			}
			set
			{
				this[TableFields.ToDateTime.ToString()] = value;
			}
		}

		public int TypeConditionSender
		{
			get
			{
				return Helper.GetInt(this[TableFields.TypeConditionSender.ToString()]);
			}
			set
			{
				this[TableFields.TypeConditionSender.ToString()] = value;
			}
		}

		public string ConditionSender
		{
			get
			{
				return Helper.GetString(this[TableFields.ConditionSender.ToString()]);
			}
			set
			{
				this[TableFields.ConditionSender.ToString()] = value;
			}
		}

		public Guid ReplyPrivateNumberGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.ReplyPrivateNumberGuid.ToString()]);
			}
			set
			{
				this[TableFields.ReplyPrivateNumberGuid.ToString()] = value;
			}
		}

		public string ReplySmsText
		{
			get
			{
				return Helper.GetString(this[TableFields.ReplySmsText.ToString()]);
			}
			set
			{
				this[TableFields.ReplySmsText.ToString()] = value;
			}
		}

		public Guid DuplicatePrivateNumberGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.DuplicatePrivateNumberGuid.ToString()]);
			}
			set
			{
				this[TableFields.DuplicatePrivateNumberGuid.ToString()] = value;
			}
		}

		public string DuplicateUserSmsText
		{
			get
			{
				return Helper.GetString(this[TableFields.DuplicateUserSmsText.ToString()]);
			}
			set
			{
				this[TableFields.DuplicateUserSmsText.ToString()] = value;
			}
		}

		public Guid Scope
		{
			get
			{
				return Helper.GetGuid(this[TableFields.Scope.ToString()]);
			}
			set
			{
				this[TableFields.Scope.ToString()] = value;
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
	}
}
