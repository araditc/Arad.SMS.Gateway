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
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Arad.SMS.Gateway.WebApi
{
	public class IPAuthenticationAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			string ipAddress = string.Empty;
			List<string> trafficIpValids = ConfigurationManager.GetSetting("TrafficValidIP").Split(',').ToList();

			if (actionContext.Request.Properties.ContainsKey("MS_OwinContext"))
			{
				OwinContext owinContext = (OwinContext)actionContext.Request.Properties["MS_OwinContext"];
				ipAddress = owinContext.Request.RemoteIpAddress;
			}

			if (!trafficIpValids.Contains(ipAddress))
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.IPInvalid, Language.GetString("IPInvalid"));

			base.OnActionExecuting(actionContext);
		}
	}
}
