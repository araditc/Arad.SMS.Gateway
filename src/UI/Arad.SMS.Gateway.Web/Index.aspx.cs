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
using System;
using Arad.SMS.Gateway.URLRewriter.Config;

namespace Arad.SMS.Gateway.Web
{
	public partial class Index : System.Web.UI.Page
	{
		private string DomainName
		{
			get { return Helper.GetDomain(Request.Url.Authority); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if (!IsPostBack)
					Response.Redirect(RewriterConfiguration.GetDefaultRule(DomainName));
			}
			catch { }
		}
	}
}
