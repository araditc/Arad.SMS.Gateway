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

using System.Text;
using System.Threading.Tasks;

namespace Arad.SMS.Gateway.WebApi
{
	public class MessageLoggingHandler : MessageHandler
	{
		protected override async Task IncommingMessageAsync(string correlationId, string requestInfo, byte[] message)
		{
			await Task.Run(() =>
					GeneralLibrary.LogController<Common.ServiceLogs>.LogInFile(Common.ServiceLogs.WebAPI, string.Format("{0} - Request: {1}\r\n{2}", correlationId, requestInfo, Encoding.UTF8.GetString(message))));
		}

		protected override async Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message )
		{
			await Task.Run(() =>
					GeneralLibrary.LogController<Common.ServiceLogs>.LogInFile(Common.ServiceLogs.WebAPI, string.Format("{0} - Response: {1}\r\n{2}", correlationId, requestInfo, Encoding.UTF8.GetString(message))));
		}
	}
}
