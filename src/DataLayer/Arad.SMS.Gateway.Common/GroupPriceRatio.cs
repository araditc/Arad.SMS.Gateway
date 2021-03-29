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
	class GroupPriceRatio : CommonEntityBase
	{
		public enum TableFields
		{
			GroupPriceGuid,
			AgentGuid,
			Ratio
		}

		public GroupPriceRatio()
			: base(TableNames.GroupPrices.ToString())
		{
			AddField(TableFields.GroupPriceGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.AgentGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.Ratio.ToString(), SqlDbType.Decimal, 18);
		}

		public Guid GroupPriceRatioGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public Guid GroupPriceGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.GroupPriceGuid.ToString()]);
			}
			set
			{
				this[TableFields.GroupPriceGuid.ToString()] = value;
			}
		}

		public Guid AgentGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.AgentGuid.ToString()]);
			}
			set
			{
				this[TableFields.AgentGuid.ToString()] = value;
			}
		}

		public decimal Ratio
		{
			get { return Helper.GetDecimal(this[TableFields.Ratio.ToString()]); }
			set
			{
				if (Helper.GetDecimal(value) < 1)
				{
					ErrorMessage += Language.GetString("InvalidRatio");
					HasError = true;
				}
				else
					this[TableFields.Ratio.ToString()] = value;
			}
		}
	}
}
