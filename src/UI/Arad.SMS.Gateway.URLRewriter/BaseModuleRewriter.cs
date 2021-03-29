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

using System.Web;
using System;

namespace Arad.SMS.Gateway.URLRewriter
{
	public abstract class BaseModuleRewriter : IHttpModule
	{
		public void Dispose() { }

		public void Init(HttpApplication httpApplication)
		{
			httpApplication.AuthorizeRequest += new System.EventHandler(this.AuthorizeRequest);
		}

		protected void AuthorizeRequest(object sender, EventArgs e)
		{
			HttpApplication app = (HttpApplication)sender;
			Rewrite(app.Request.Path.ToLower(), app);
		}

		protected abstract void Rewrite(string requestedPath, HttpApplication app);
	}
}
