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

namespace Arad.SMS.Gateway.SqlLibrary
{
	[Serializable]
	public class Log
	{
		public int Type { get; set; }
		public string Source { get; set; }
		public string Name { get; set; }
		public string Text { get; set; }
		public string IPAddress { get; set; }
		public string Browser { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid ReferenceGuid { get; set; }
		public Guid UserGuid { get; set; }
	}
}