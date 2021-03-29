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
	public class RoleService : CommonEntityBase
	{
		public enum TableFields
		{
			Price,
			ServiceGuid,
			RoleGuid,
			IsDefault,
		}

		public RoleService()
			: base(TableNames.RoleServices.ToString())
		{
			AddField(TableFields.Price.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.ServiceGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.RoleGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.IsDefault.ToString(), SqlDbType.Bit);
		}

		public Guid RoleServiceGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
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

		public Guid ServiceGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.ServiceGuid.ToString()]);
			}
			set
			{
				if (value == Guid.Empty)
				{
					HasError = true;
					ErrorMessage = Language.GetString("IncorectServiceInfo");
				}
				else
				{
					this[TableFields.ServiceGuid.ToString()] = value;
				}
			}
		}

		public Guid RoleGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.RoleGuid.ToString()]);
			}
			set
			{
				if (value == Guid.Empty)
				{
					HasError = true;
					ErrorMessage = Language.GetString("SelectRole");
				}
				else
				{
					this[TableFields.RoleGuid.ToString()] = value;
				}
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
