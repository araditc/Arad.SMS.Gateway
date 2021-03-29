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
using System.Reflection;

namespace Arad.SMS.Gateway.GeneralLibrary
{
	public abstract class GuidEnum<T> where T : new()
	{
		public Dictionary<Guid, string> Get()
		{
			Dictionary<Guid, string> result = new Dictionary<Guid, string>();
			foreach (FieldInfo fieldInfo in this.GetType().GetFields())
			{
				if (fieldInfo.IsStatic)
					result.Add((Guid)fieldInfo.GetValue(null), fieldInfo.Name);
			}

			return result;
		}

		static private Dictionary<Guid, string> values;
		static public Dictionary<Guid, string> Values
		{
			get
			{
				if (values == null)
				{
					values = new Dictionary<Guid, string>();
					foreach (FieldInfo fieldInfo in typeof(T).GetFields())
					{
						if (fieldInfo.IsStatic)
							values.Add((Guid)fieldInfo.GetValue(null), fieldInfo.Name);
					}
				}
				return values;
			}
		}

		public Dictionary<Guid, string> GetValues()
		{
			return Values;
		}
	}
}
