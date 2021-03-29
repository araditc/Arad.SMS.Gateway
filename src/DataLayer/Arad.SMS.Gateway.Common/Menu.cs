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

namespace Arad.SMS.Gateway.Common
{
	public class Menu
	{
		public int ID { get; set; }
		public Guid Guid { get; set; }
		public int Order { get; set; }
		public string Title { get; set; }
		public string Path { get; set; }
		public string SmallIcon { get; set; }
		public string LargeIcon { get; set; }
		public bool ActiveLink { get; set; }
		public int SubMenuCount { get; set; }
		public int Type { get; set; }
		public string Target { get; set; }
		public int Location { get; set; }

		public List<Menu> Children { get; set; }
	}
}
