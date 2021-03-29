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

using Owin;
using System.Web.Http;

namespace Arad.SMS.Gateway.WebApi
{
	public class Startup
	{
		public void Configuration(IAppBuilder appBuilder)
		{
			HttpConfiguration config = new HttpConfiguration();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			//config.Filters.Add(new CustomHttpAttribute());
			//config.MessageHandlers.Add(new RequiredAuthenticationAttribute());
			//config.Filters.Add(new AuthorizeAttribute());
			config.MessageHandlers.Add(new MessageLoggingHandler());
			config.Filters.Add(new ExceptionHandlingAttribute());
			config.Formatters.XmlFormatter.UseXmlSerializer = true;
			appBuilder.UseWebApi(config);

		}
	}
}
