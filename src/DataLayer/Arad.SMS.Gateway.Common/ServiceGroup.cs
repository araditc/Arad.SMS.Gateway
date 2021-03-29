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
	public class ServiceGroup : CommonEntityBase
	{
		public enum TableFields
		{
			Title,
			IconAddress,
			LargeIcon,
			Order,
			parentGuid,
			CreateDate,
			IsDeleted,
		}

		public ServiceGroup()
			: base(TableNames.ServiceGroups.ToString())
		{
			AddField(TableFields.Title.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.IconAddress.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.LargeIcon.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.Order.ToString(), SqlDbType.Int);
			AddField(TableFields.parentGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
		}

		public Guid ServiceGroupGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string Title
		{
			get { return Helper.GetString(this[TableFields.Title.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteServiceGroupTitleField");
				}
				else
					this[TableFields.Title.ToString()] = value;
			}
		}

		public string IconAddress
		{
			get { return Helper.GetString(this[TableFields.IconAddress.ToString()]); }
			set { this[TableFields.IconAddress.ToString()] = value; }
		}

		public string LargeIcon
		{
			get { return Helper.GetString(this[TableFields.LargeIcon.ToString()]); }
			set { this[TableFields.LargeIcon.ToString()] = value; }
		}

		public int Order
		{
			get { return Helper.GetInt(this[TableFields.Order.ToString()]); }
			set
			{
				CheckDataConditionsResult result = Helper.CheckDataConditions(value);
				if (result.IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteServiceGroupOrderField");
				}
				else if (!result.IsIntNumber)
				{
					HasError = true;
					ErrorMessage += Language.GetString("InvalidValueServiceGroupOrderField");
				}
				else
					this[TableFields.Order.ToString()] = value;
			}
		}

		public DateTime CreateDate
		{
			get { return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); }
			set { this[TableFields.CreateDate.ToString()] = value; }
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

		public Guid parentGuid
		{
			get { return Helper.GetGuid(this[TableFields.parentGuid.ToString()]); }
			set { this[TableFields.parentGuid.ToString()] = value; }
		}

	}
}
