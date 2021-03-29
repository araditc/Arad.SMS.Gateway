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
	public class ParserFormula : CommonEntityBase
	{
		public enum TableFields
		{
			Title,
			Key,
			PhoneNo,
			IsCorrect,
			//Type,
			Condition,
			//ReactionType,
			Priority,
			Counter,
			ReactionExtention,
			SmsParserGuid,
		}

		public ParserFormula()
			: base(TableNames.ParserFormulas.ToString())
		{
			AddField(TableFields.Title.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Key.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.PhoneNo.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.IsCorrect.ToString(), SqlDbType.Bit);
			//AddField(TableFields.Type.ToString(), SqlDbType.Int);
			AddField(TableFields.Condition.ToString(), SqlDbType.Int);
			//AddField(TableFields.ReactionType.ToString(), SqlDbType.Int);
			AddField(TableFields.Priority.ToString(), SqlDbType.Int);
			AddField(TableFields.Counter.ToString(), SqlDbType.Int);
			AddField(TableFields.ReactionExtention.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.SmsParserGuid.ToString(), SqlDbType.UniqueIdentifier);
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

		public string Key
		{
			get
			{
				return Helper.GetString(this[TableFields.Key.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteKeyField");
					HasError = true;
				}
				else
					this[TableFields.Key.ToString()] = value;
			}
		}

		public string PhoneNo
		{
			get
			{
				return Helper.GetString(this[TableFields.PhoneNo.ToString()]);
			}
			set
			{
				this[TableFields.PhoneNo.ToString()] = value;
			}
		}

		public bool IsCorrect
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsCorrect.ToString()]);
			}
			set
			{
				this[TableFields.IsCorrect.ToString()] = value;
			}
		}

		//public int Type
		//{
		//	get
		//	{
		//		return Helper.GetInt(this[TableFields.Type.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.Type.ToString()] = value;
		//	}
		//}

		public int Condition
		{
			get
			{
				return Helper.GetInt(this[TableFields.Condition.ToString()]);
			}
			set
			{
				this[TableFields.Condition.ToString()] = value;
			}
		}

		//public int ReactionType
		//{
		//	get
		//	{
		//		return Helper.GetInt(this[TableFields.ReactionType.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.ReactionType.ToString()] = value;
		//	}
		//}

		public int Priority
		{
			get
			{
				return Helper.GetInt(this[TableFields.Priority.ToString()]);
			}
			set
			{
				this[TableFields.Priority.ToString()] = value;
			}
		}

		public int Counter
		{
			get
			{
				return Helper.GetInt(this[TableFields.Counter.ToString()]);
			}
			set
			{
				this[TableFields.Counter.ToString()] = value;
			}
		}

		public string ReactionExtention
		{
			get
			{
				return Helper.GetString(this[TableFields.ReactionExtention.ToString()]);
			}
			set
			{
				this[TableFields.ReactionExtention.ToString()] = value;
			}
		}

		public Guid SmsParserGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.SmsParserGuid.ToString()]);
			}
			set
			{
				this[TableFields.SmsParserGuid.ToString()] = value;
			}
		}
	}
}
