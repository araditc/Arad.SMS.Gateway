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

using Microsoft.Owin;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Filters;
using Arad.SMS.Gateway.WebApi.Models;

namespace Arad.SMS.Gateway.WebApi
{
	public class ExceptionHandlingAttribute : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext context)
		{
			string contentType = string.Empty;
			if (context.Request.Properties.ContainsKey("MS_OwinContext"))
			{
				OwinContext owinContext = (OwinContext)context.Request.Properties["MS_OwinContext"];
				contentType = owinContext.Request.ContentType;
			}

			if (context.Exception is BusinessException)
			{
				var businessException = context.Exception as BusinessException;
				ResponseMessage rm = new ResponseMessage();
				rm.IsSuccessful = false;
				rm.Message = businessException.Description;
				rm.StatusCode = businessException.StatusCode;

				switch (contentType)
				{
					case "text/xml":
					case "application/xml":
						throw new HttpResponseException(new HttpResponseMessage(businessException.HttpStatusCode)
						{
							Content = new ObjectContent<ResponseMessage>(rm, new IgnoreNamespacesXmlMediaTypeFormatter()),
							ReasonPhrase = businessException.Description
						});

					case "text/json":
					case "application/json":
						throw new HttpResponseException(new HttpResponseMessage(businessException.HttpStatusCode)
						{
							Content = new ObjectContent<ResponseMessage>(rm, new JsonMediaTypeFormatter()),
							ReasonPhrase = businessException.Description
						});
					default:
						throw new HttpResponseException(new HttpResponseMessage(businessException.HttpStatusCode)
						{
							Content = new ObjectContent<ResponseMessage>(rm, new IgnoreNamespacesXmlMediaTypeFormatter()),
							ReasonPhrase = businessException.Description
						});
				}
			}
			else
			{
				ResponseMessage rm = new ResponseMessage();
				rm.IsSuccessful = false;
				rm.StatusCode = (int)ErrorCode.InternalServerError;
				rm.Message = "Internal Server Error";
				rm.StackTrace = context.Exception.StackTrace;
				switch (contentType)
				{
					case "application/xml":
						throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
						{
							Content = new ObjectContent<ResponseMessage>(rm, new IgnoreNamespacesXmlMediaTypeFormatter()),
						});
					case "application/json":
						throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
						{
							Content = new ObjectContent<ResponseMessage>(rm, new JsonMediaTypeFormatter()),
						});
					default:
						throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
						{
							Content = new ObjectContent<ResponseMessage>(rm, new IgnoreNamespacesXmlMediaTypeFormatter()),
						});
				}
			}
		}
	}
}
