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
	public class DataCenter : CommonEntityBase
	{
		public enum TableFields
		{
			Title,
			Type,
			Location,
			Desktop,
			CreateDate,
			IsArchived,
			IsDeleted,
			UserGuid,
		}

		public DataCenter()
			: base(TableNames.DataCenters.ToString())
		{
			AddField(TableFields.Title.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Type.ToString(), SqlDbType.Int);
			AddField(TableFields.Location.ToString(), SqlDbType.Int);
			AddField(TableFields.Desktop.ToString(), SqlDbType.Int);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.IsArchived.ToString(), SqlDbType.Bit);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid DataCenterGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public Guid UserGuid
		{
			get { return Helper.GetGuid(this[TableFields.UserGuid.ToString()]); }
			set { this[TableFields.UserGuid.ToString()] = value; }
		}

		public string Title
		{
			get { return Helper.GetString(this[TableFields.Title.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteTitleField");
				}
				else
					this[TableFields.Title.ToString()] = value;
			}
		}

		public int Type
		{
			get { return Helper.GetInt(this[TableFields.Type.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteDataCenterTypeField");
				}
				else
					this[TableFields.Type.ToString()] = value;
			}
		}

		public int Location
		{
			get { return Helper.GetInt(this[TableFields.Location.ToString()]); }
			set { this[TableFields.Location.ToString()] = value; }
		}

		public int Desktop
		{
			get { return Helper.GetInt(this[TableFields.Desktop.ToString()]); }
			set { this[TableFields.Desktop.ToString()] = value; }
		}

		public bool IsArchived
		{
			get { return Helper.GetBool(this[TableFields.IsArchived.ToString()]); }
			set { this[TableFields.Type.ToString()] = value; }
		}

		public DateTime CreateDate
		{
			get { return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); }
			set { this[TableFields.CreateDate.ToString()] = value; }
		}
	}
}
