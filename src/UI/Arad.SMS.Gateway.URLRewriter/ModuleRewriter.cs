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
using System.Text.RegularExpressions;
using Arad.SMS.Gateway.URLRewriter.Config;

namespace Arad.SMS.Gateway.URLRewriter
{
	public class ModuleRewriter : BaseModuleRewriter
	{
		protected override void Rewrite(string requestedPath, System.Web.HttpApplication app)
		{
			try
			{
				RewriterRuleCollection rules = RewriterConfiguration.GetConfig(Helper.GetDomain(app.Request.Url.Authority)).Rules;

				for (int counterRule = 0; counterRule < rules.Count; counterRule++)
				{
					string lookFor = "^" + RewriterUtils.ResolveUrl(app.Context.Request.ApplicationPath, rules[counterRule].LookFor) + "$";

					Regex re = new Regex(lookFor, RegexOptions.IgnoreCase);

					if (re.IsMatch(requestedPath))
					{
						string sendToUrl = RewriterUtils.ResolveUrl(app.Context.Request.ApplicationPath, re.Replace(requestedPath, rules[counterRule].SendTo));
						RewriterUtils.RewriteUrl(app.Context, sendToUrl);
						break;
					}
				}
			}
			catch { }
		}
	}
}
