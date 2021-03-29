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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Arad.SMS.Gateway.Business
{
	internal static class WinServiceHandler
	{
		private static IWinServiceHandler channle = null;
		private static BasicHttpBinding basicHttpBinding;

		public static IWinServiceHandler SmsSendWinServiceHandlerChannel()
		{
			string listeningAddress = GeneralLibrary.ConfigurationManager.GetSetting("WinServiceHandlerSendingAddress");
			EndpointAddress endpointAddress = new EndpointAddress(listeningAddress);
			
			basicHttpBinding = new BasicHttpBinding();
			basicHttpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
			basicHttpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
			basicHttpBinding.ReaderQuotas.MaxDepth = 2147483647;
			basicHttpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
			basicHttpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
			basicHttpBinding.MaxBufferPoolSize = 2147483647;// *(int)Math.Exp(6 * System.Math.Log(10));
			basicHttpBinding.MaxReceivedMessageSize = 2147483647;
			basicHttpBinding.ReceiveTimeout = new TimeSpan(0, 0, 30);
			basicHttpBinding.CloseTimeout = new TimeSpan(0, 0, 30);
			basicHttpBinding.OpenTimeout = new TimeSpan(0, 0, 30);
			basicHttpBinding.SendTimeout = new TimeSpan(0, 10, 0);

			channle = ChannelFactory<IWinServiceHandler>.CreateChannel(basicHttpBinding, endpointAddress);

			return channle;
		}
	}
}
