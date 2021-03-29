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

namespace Arad.SMS.Gateway.WebApi.Models
{
	public class PostPhoneNumberModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public string Telephone { get; set; }
		public string Mobile { get; set; }
		public string FaxNumber { get; set; }
		public string Job { get; set; }
		public string Address { get; set; }
		public string ZipCode { get; set; }
		public string Email { get; set; }
		/// <summary>
		/// Man = 1,Woman = 2
		/// </summary>
		public int Gender { get; set; }
		public Guid ZoneGuid { get; set; }
	}
}
