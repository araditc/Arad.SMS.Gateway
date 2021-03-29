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
using System.Data;

namespace Arad.SMS.Gateway.Common
{
	public class Operators : CommonEntityBase
	{
		public enum TableFields
		{
			ID,
			Title,
			Name,
			Regex,
		}

		public Operators()
			: base(TableNames.Operators.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.TinyInt);
			AddField(TableFields.Title.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.Name.ToString(), SqlDbType.NVarChar, 64);
			AddField(TableFields.Regex.ToString(), SqlDbType.NVarChar, 512);
		}

		public byte ID
		{
			get { return byte.Parse(this[TableFields.ID.ToString()].ToString()); }
			set { this[TableFields.ID.ToString()] = value; }
		}

		public string Title
		{
			get { return Helper.GetString(this[TableFields.Title.ToString()]); }
			set { this[TableFields.Title.ToString()] = value; }
		}

		public string Name
		{
			get { return Helper.GetString(this[TableFields.Name.ToString()]); }
			set { this[TableFields.Name.ToString()] = value; }
		}

		public string Regex
		{
			get { return Helper.GetString(this[TableFields.Regex.ToString()]); }
			set { this[TableFields.Regex.ToString()] = value; }
		}
	}
}
