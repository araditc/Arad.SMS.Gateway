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
	public class AgentRatio : CommonEntityBase
	{
		public enum TableFields
		{
			SmsType,
			Ratio,
			CreateDate,
			OperatorID,
			SmsSenderAgentGuid,
		}

		public AgentRatio()
			: base(TableNames.AgentRatio.ToString())
		{
			AddField(TableFields.SmsType.ToString(), SqlDbType.TinyInt);
			AddField(TableFields.Ratio.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.OperatorID.ToString(), SqlDbType.TinyInt);
			AddField(TableFields.SmsSenderAgentGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid AgentRatioGuid
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

		public byte SmsType
		{
			get
			{
				return Helper.GetByte(this[TableFields.SmsType.ToString()].ToString());
			}
			set
			{
				if (Helper.GetByte(value) == 0)
				{
					ErrorMessage += Language.GetString("SmsTypeNotDefine");
					HasError = true;
				}
				else
					this[TableFields.SmsType.ToString()] = value;
			}
		}

		public decimal Ratio
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.Ratio.ToString()]);
			}
			set
			{
				if (Helper.GetDecimal(value) == 0)
				{
					ErrorMessage += Language.GetString("RatioNotValid");
					HasError = true;
				}
				else
					this[TableFields.Ratio.ToString()] = value;
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

		public byte OperatorID
		{
			get
			{
				return byte.Parse(this[TableFields.OperatorID.ToString()].ToString());
			}
			set
			{
				if (Helper.GetByte(value) == 0)
				{
					ErrorMessage += Language.GetString("OperatorNotValid");
					HasError = true;
				}
				else
					this[TableFields.OperatorID.ToString()] = value;
			}
		}

		public Guid SmsSenderAgentGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.SmsSenderAgentGuid.ToString()]);
			}
			set
			{
				if (Helper.GetGuid(value) == Guid.Empty)
				{
					ErrorMessage += Language.GetString("SmsSenderAgentNotValid");
					HasError = true;
				}
				else
					this[TableFields.SmsSenderAgentGuid.ToString()] = value;
			}
		}
	}
}
