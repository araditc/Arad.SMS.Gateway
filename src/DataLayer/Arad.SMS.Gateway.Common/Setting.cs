﻿// --------------------------------------------------------------------
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
	public class Setting:CommonEntityBase
	{
		private enum TableFields
		{
			Key,
			Value
		}

		public Setting()
			: base(TableNames.Settings.ToString())
		{
			AddField(TableFields.Key.ToString(), SqlDbType.Int);
			AddField(TableFields.Value.ToString(), SqlDbType.NVarChar, short.MaxValue);
		}

		public Guid SettingGuid
		{
			get{return PrimaryKey;}
			set { PrimaryKey = value; }
		}

		public int Key
		{
			get
			{
				return Helper.GetInt(this[TableFields.Key.ToString()]);
			}
			set
			{
				this[TableFields.Key.ToString()] = value;
			}
		}

		public string Value
		{
			get
			{
				return Helper.GetString(this[TableFields.Value.ToString()]);
			}
			set
			{
				this[TableFields.Value.ToString()] = value;
			}
		}
	}
}
