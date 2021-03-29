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
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;

namespace Arad.SMS.Gateway.Common
{
	public class Domain : CommonEntityBase
	{
		public enum TableFields
		{
			Name,
			CreateDate,
			Desktop,
			DefaultPage,
			Theme,
			IsDeleted,
			UserGuid,
		}

		public Domain()
			: base(TableNames.Domains.ToString())
		{
			AddField(TableFields.Name.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.Desktop.ToString(), SqlDbType.Int);
			AddField(TableFields.DefaultPage.ToString(), SqlDbType.Int);
			AddField(TableFields.Theme.ToString(), SqlDbType.Int);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid DomainGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string Name
		{
			get { return Helper.GetString(this[TableFields.Name.ToString()]); }
			set 
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteDomainNameField");
					HasError = true;
				}
				else
					this[TableFields.Name.ToString()] = value; 
			}
		}

		public DateTime CreateDate
		{
			get { return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); }
			set { this[TableFields.CreateDate.ToString()] = value; }
		}

		public int Desktop
		{
			get
			{
				return Helper.GetInt(this[TableFields.Desktop.ToString()]);
			}
			set
			{
				this[TableFields.Desktop.ToString()] = value;
			}
		}

		public int DefaultPage
		{
			get
			{
				return Helper.GetInt(this[TableFields.DefaultPage.ToString()]);
			}
			set
			{
				this[TableFields.DefaultPage.ToString()] = value;
			}
		}

		public int Theme
		{
			get
			{
				return Helper.GetInt(this[TableFields.Theme.ToString()]);
			}
			set
			{
				this[TableFields.Theme.ToString()] = value;
			}
		}

		public Guid UserGuid
		{
			get { return Helper.GetGuid(this[TableFields.UserGuid.ToString()]); }
			set { this[TableFields.UserGuid.ToString()] = value; }
		}
	}
}
