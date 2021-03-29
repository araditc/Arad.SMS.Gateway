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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.Common
{
	public class DomainSetting : CommonEntityBase
	{
		public enum TableFields
		{
			DomainGuid,
			Key,
			Value
		}

		public DomainSetting()
			: base(TableNames.DomainSettings.ToString())
		{
			AddField(TableFields.DomainGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.Key.ToString(), SqlDbType.Int);
			AddField(TableFields.Value.ToString(), SqlDbType.NVarChar, short.MaxValue);
		}

		public Guid DomainSettingGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public Guid DomainGuid
		{
			get { return Helper.GetGuid(this[TableFields.DomainGuid.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteDomainSettingDomainGuidField");
				}
				else
					this[TableFields.DomainGuid.ToString()] = value;
			}
		}

		public string Key
		{
			get { return Helper.GetString(this[TableFields.Key.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteDomainSettingKeyField");
				}
				else
					this[TableFields.Key.ToString()] = value;
			}
		}

		public string Value
		{
			get { return Helper.GetString(this[TableFields.Value.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage += Language.GetString("CompleteDomainSettingValueField");
				}
				else
					this[TableFields.Value.ToString()] = value;
			}
		}
	}
}
