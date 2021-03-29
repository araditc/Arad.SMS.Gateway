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
using System.ComponentModel.DataAnnotations;

namespace Arad.SMS.Gateway.WebApi.Models
{
	public class GetPAKServiceModel
	{
		private string mobile;

		[Required(ErrorMessage = "Enter Service ID")]
		public string PAKServiceId
		{
			get;
			set;
		}

		[Required(ErrorMessage = "Enter Mobile")]
		public string Mobile
		{
			get
			{
				return mobile;
			}
			set
			{
				mobile = Helper.GetLocalMobileNumber(value);
			}
		}

		public int Type
		{
			get;
			set;
		}
	}
}
