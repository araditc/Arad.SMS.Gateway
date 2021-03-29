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
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Arad.SMS.Gateway.WebApi.Models;

namespace Arad.SMS.Gateway.WebApi
{
	public abstract class MessageHandler : DelegatingHandler
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			string ipAddress = string.Empty;

			if (request.Properties.ContainsKey("MS_OwinContext"))
			{
				OwinContext owinContext = (OwinContext)request.Properties["MS_OwinContext"];
				ipAddress = owinContext.Request.RemoteIpAddress;
			}

			var corrId = string.Format("{0}{1}", DateTime.Now.Ticks, Thread.CurrentThread.ManagedThreadId);
			var requestInfo = string.Format("{0} {1} {2}", request.Method, request.RequestUri, ipAddress);

			var requestMessage = await request.Content.ReadAsByteArrayAsync();

			await IncommingMessageAsync(corrId, requestInfo, requestMessage);

			var response = await base.SendAsync(request, cancellationToken);

			byte[] responseMessage;

			if (response.IsSuccessStatusCode)
			{
				responseMessage = Encoding.UTF8.GetBytes(response.StatusCode.ToString());
				if (response.Content != null)
					responseMessage = await response.Content.ReadAsByteArrayAsync();
				await OutgoingMessageAsync(corrId, requestInfo, responseMessage);
			}
			else
			{
				string ex = response.Content.ReadAsStringAsync().Result;
				responseMessage = Encoding.UTF8.GetBytes(ex);
				await OutgoingMessageAsync(corrId, requestInfo, responseMessage);
				XmlSerializer mySerializer = new XmlSerializer(typeof(ResponseMessage));
				MemoryStream myMemoryStream =	new MemoryStream(responseMessage);
				ResponseMessage rm = (ResponseMessage)mySerializer.Deserialize(myMemoryStream);
				rm.StackTrace = rm.Message;
				response.Content = new ObjectContent<ResponseMessage>(rm, new IgnoreNamespacesXmlMediaTypeFormatter());
			}

			return response;
		}

		protected abstract Task OutgoingMessageAsync(string corrId, string requestInfo, byte[] message);

		protected abstract Task IncommingMessageAsync(string corrId, string requestInfo, byte[] message);
	}
}
