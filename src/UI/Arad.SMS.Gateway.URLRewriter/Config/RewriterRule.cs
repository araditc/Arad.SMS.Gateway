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

namespace Arad.SMS.Gateway.URLRewriter.Config
{
	public class RewriterRule
	{
		private string lookFor;
		private string sendTo;
		private bool isDefault;

		public string LookFor
		{
			get { return lookFor; }
			set { lookFor = value; }
		}

		public string SendTo
		{
			get { return sendTo; }
			set { sendTo = value; }
		}

		public bool IsDefault
		{
			get { return isDefault; }
			set { isDefault = value; }
		}
	}
}
