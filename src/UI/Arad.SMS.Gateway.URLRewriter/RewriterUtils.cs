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
using System.Reflection;
using System.Web;

namespace Arad.SMS.Gateway.URLRewriter
{
	internal class RewriterUtils
	{
		internal static void RewriteUrl(HttpContext context, string sendToUrl)
		{
			string x, y;
			RewriteUrl(context, sendToUrl, out x, out y);
		}

		internal static void RewriteUrl(HttpContext context, string sendToUrl, out string sendToUrlLessQString, out string filePath)
		{
			PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

			if (context.Request.QueryString.Count > 0)
			{
				if (sendToUrl.IndexOf('?') != -1)
				{
					string[] param = sendToUrl.Substring(sendToUrl.IndexOf('?') + 1).Split('&');

					foreach (string p in param)
					{
						string key = p.Split('=')[0];
						string value = p.Split('=')[1];
						if (context.Request.QueryString[key] != null)
						{
							isreadonly.SetValue(context.Request.QueryString, false, null);
							context.Request.QueryString.Remove(key);
						}
					}

					if (context.Request.QueryString.Count > 0)
						sendToUrl += "&" + context.Request.QueryString.ToString();
				}
				else
					sendToUrl += "?" + context.Request.QueryString.ToString();
			}

			string queryString = String.Empty;
			sendToUrlLessQString = sendToUrl;
			if (sendToUrl.IndexOf('?') > 0)
			{
				sendToUrlLessQString = sendToUrl.Substring(0, sendToUrl.IndexOf('?'));
				queryString = sendToUrl.Substring(sendToUrl.IndexOf('?') + 1);
			}

			filePath = string.Empty;
			filePath = context.Server.MapPath(sendToUrlLessQString);

			context.RewritePath(sendToUrlLessQString.ToLower(), String.Empty, queryString);
		}

		internal static string ResolveUrl(string appPath, string url)
		{
			if (url.Length == 0 || url[0] != '~')
				return url;
			else
			{
				if (url.Length == 1)
					return appPath;
				if (url[1] == '/' || url[1] == '\\')
				{
					if (appPath.Length > 1)
						return appPath + "/" + url.Substring(2);
					else
						return "/" + url.Substring(2);
				}
				else
				{
					if (appPath.Length > 1)
						return appPath + "/" + url.Substring(1);
					else
						return appPath + url.Substring(1);
				}
			}
		}
	}
}
