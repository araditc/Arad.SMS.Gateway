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

using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Arad.SMS.Gateway.WebApi
{
	public class RequiredAuthenticationAttribute : AuthorizationFilterAttribute
	{
		public override void OnAuthorization(HttpActionContext actionContext)
		{
			string ipAddress = string.Empty;
			string username = string.Empty;
			string password = string.Empty;
			string challengeString = Helper.GetMd5Hash(Helper.RandomString()).ToLower();
			Common.User user = new Common.User();

			if (actionContext.Request.Properties.ContainsKey("MS_OwinContext"))
			{
				OwinContext owinContext = (OwinContext)actionContext.Request.Properties["MS_OwinContext"];
				ipAddress = owinContext.Request.RemoteIpAddress;
			}

			if (actionContext.Request.Method.ToString().ToLower() == "post")
			{
				var authorizeHeader = actionContext.Request.Headers.Authorization;
				if (authorizeHeader == null ||
						!authorizeHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) ||
						String.IsNullOrEmpty(authorizeHeader.Parameter))
					throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.BadRequest, Language.GetString("BadRequest"));


				var encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
				var credintials = encoding.GetString(Convert.FromBase64String(authorizeHeader.Parameter));

				username = credintials.Split(':')[0];
				password = credintials.Split(':')[1];
			}
			else
			{
				if (actionContext.Request.Properties.ContainsKey("MS_QueryNameValuePairs"))
				{
					var queryString = (KeyValuePair<string, string>[])actionContext.Request.Properties["MS_QueryNameValuePairs"];
					username = queryString.Where(param => param.Key.ToLower() == "username").First().Value;
					password = queryString.Where(param => param.Key.ToLower() == "password").First().Value;
				}
			}

			user.UserName = username;

			string accountPassword = string.Empty;
			string apiPassword = string.Empty;
			Facade.UserSetting.GetUserWebAPIPassword(username, ref accountPassword, ref apiPassword);

			if (!string.IsNullOrEmpty(apiPassword))
			{
				password = Helper.GetMd5Hash(challengeString + Helper.GetMd5Hash(password).ToLower());
				apiPassword = Helper.GetMd5Hash(challengeString + apiPassword);
				if (challengeString == string.Empty || apiPassword.ToLower() != password.ToLower())
					throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.AccountIsInvalid, Language.GetString("AccountIsInvalid"));

				user.Password = Helper.GetMd5Hash(challengeString + accountPassword);
			}
			else
			{
				user.Password = Helper.GetMd5Hash(challengeString + Helper.GetMd5Hash(password).ToLower());
			}

			bool isLoginValid = Facade.User.LoginUser(user, challengeString, false);

			if (!isLoginValid)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.AccountIsInvalid, Language.GetString("AccountIsInvalid"));
			if (!user.IsActive)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.AccountIsInactive, Language.GetString("AccountIsInactive"));
			if (user.ExpireDate < DateTime.Now)
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.AccountIsExpired, Language.GetString("AccountIsExpired"));

			string ip = Facade.UserSetting.GetSettingValue(user.UserGuid, AccountSetting.ApiIP);
			if (string.IsNullOrEmpty(ip))
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.IPInvalid, Language.GetString("IPInvalid"));

			List<String> validIPs = ip.Split(',').ToList();
			if (validIPs.Contains("0.0.0.0") || validIPs.Contains(ipAddress))
			{
				MyPrincipal principal = new MyPrincipal(new GenericIdentity(user.UserName), (new[] { user.RoleGuid.ToString() }));
				principal.UserDetails = user;
				Thread.CurrentPrincipal = principal;
				return;
			}
			else
				throw new BusinessException(HttpStatusCode.Accepted, ErrorCode.IPInvalid, Language.GetString("IPInvalid"));
		}
	}
}

