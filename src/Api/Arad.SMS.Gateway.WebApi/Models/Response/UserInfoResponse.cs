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
using System.Threading.Tasks;
using System.Xml.Serialization;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.WebApi.Models
{
	[XmlRoot(ElementName = "ResponseMessage")]
	public class UserInfoResponse : ResponseMessage
	{
		string domainName = string.Empty;
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Mobile { get; set; }
		public string Email { get; set; }
		public DateTime ExpireDate { get; set; }
		public string Domain
		{
			get
			{
				return domainName;
			}
			set
			{
				domainName = Facade.Domain.GetDomainName(Helper.GetGuid(value));
			}
		}
		public decimal Credit { get; set; }
	}
}
