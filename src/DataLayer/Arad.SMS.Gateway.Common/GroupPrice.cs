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
	public class GroupPrice : CommonEntityBase
	{
		public enum TableFields
		{
			Title,
			MinimumMessage,
			MaximumMessage,
			BasePrice,
			DecreaseTax,
			AgentRatio,
			CreateDate,
			UserGuid,
			IsDefault,
			IsPrivate,
			IsDeleted,
		}

		public GroupPrice()
			: base(TableNames.GroupPrices.ToString())
		{
			AddField(TableFields.Title.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.MinimumMessage.ToString(), SqlDbType.BigInt);
			AddField(TableFields.MaximumMessage.ToString(), SqlDbType.BigInt);
			AddField(TableFields.BasePrice.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.DecreaseTax.ToString(), SqlDbType.Bit);
			AddField(TableFields.AgentRatio.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.IsDefault.ToString(), SqlDbType.Bit);
			AddField(TableFields.IsPrivate.ToString(), SqlDbType.Bit);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
		}

		public Guid GroupPriceGuid
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

		public long MinimumMessage
		{
			get
			{
				return Helper.GetLong(this[TableFields.MinimumMessage.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteMinimumRangeField");
					HasError = true;
				}
				//else if (Helper.GetLong(value) == 0)
				//{
				//	ErrorMessage += Language.GetString("InvalidMinimumRangeField");
				//	HasError = true;
				//}
				else
					this[TableFields.MinimumMessage.ToString()] = value;
			}
		}

		public long MaximumMessage
		{
			get
			{
				return Helper.GetLong(this[TableFields.MaximumMessage.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteMaximumRangeField");
					HasError = true;
				}
				//else if (Helper.GetLong(value) == 0)
				//{
				//	ErrorMessage += Language.GetString("InvalidMaximumRangeField");
				//	HasError = true;
				//}
				else
					this[TableFields.MaximumMessage.ToString()] = value;
			}
		}

		public decimal BasePrice
		{
			get { return Helper.GetDecimal(this[TableFields.BasePrice.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompletePriceField");
					HasError = true;
				}
				else if (Helper.GetDecimal(value) <= 0)
				{
					ErrorMessage += Language.GetString("InvalidPrice");
					HasError = true;
				}
				else
					this[TableFields.BasePrice.ToString()] = value;
			}
		}

		public bool DecreaseTax
		{
			get { return Helper.GetBool(this[TableFields.DecreaseTax.ToString()]); }
			set
			{
				this[TableFields.DecreaseTax.ToString()] = value;
			}
		}

		public string AgentRatio
		{
			get { return Helper.GetString(this[TableFields.AgentRatio.ToString()]); }
			set { this[TableFields.AgentRatio.ToString()] = value; }
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

		public bool IsPrivate
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsPrivate.ToString()]);
			}
			set
			{
				this[TableFields.IsPrivate.ToString()] = value;
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
	}
}