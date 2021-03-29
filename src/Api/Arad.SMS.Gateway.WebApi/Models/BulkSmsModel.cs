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

using Arad.SMS.Gateway.Common;
using System;
using System.Xml.Serialization;

namespace Arad.SMS.Gateway.WebApi.Models
{
	[XmlRoot(ElementName = "BulkInfo")]
	[Serializable()]
	public class BulkSmsModel
	{
		public long Id { get; set; }
		public string SmsText { get; set; }
		public int SmsLen { get; set; }
		public bool IsUnicode { get; set; }
		public int TotalCount { get; set; }
		public SendStatus Status { get; set; }
		public string Receivers { get; set; }
	}
}
