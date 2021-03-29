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

using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.ManageThread;
using System;
using System.ServiceProcess;

namespace TerafficRelay
{
	public partial class TrafficRelayService : ServiceBase
	{
		private SmsRelayThread smsRelayThread;
		private DeliveryRelayThread deliveryRelayThread;
		private GarbageCollectorThread gcThread;
		public TrafficRelayService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			try
			{
				DataAccessBase.GetActiveDataAccessProvider = Arad.SMS.Gateway.DataAccessLayer.SqlDataAccess.GetSqlDataAccess;
				Language.ActiveLanguage = Language.AvalibaleLanguages.fa;

				gcThread = new GarbageCollectorThread(3 * 60 * 1000);
				gcThread.Start();

				smsRelayThread = new SmsRelayThread(5 * 1000);
				deliveryRelayThread = new DeliveryRelayThread(5 * 1000);

				smsRelayThread.Start();
				deliveryRelayThread.Start();
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("On OnStart : {0}", ex.Message));
			}
		}

		protected override void OnStop()
		{
			try
			{
				if (smsRelayThread != null)
					smsRelayThread.Stop();

				if (deliveryRelayThread != null)
					deliveryRelayThread.Stop();

				if (gcThread != null)
					gcThread.Stop();
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("\r\n------------------OnStop-------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("On Stop TrafficRelayService : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("\r\n--------------------------------------------------------"));
			}
		}

		protected override void OnShutdown()
		{
			try
			{
				if (smsRelayThread != null)
					smsRelayThread.Stop();

				if (deliveryRelayThread != null)
					deliveryRelayThread.Stop();

				if (gcThread != null)
					gcThread.Stop();

				base.OnShutdown();
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("\r\n------------------OnShutdown-------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("On Shutdown TrafficRelayService : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.TrafficRelay, string.Format("\r\n--------------------------------------------------------"));
			}
		}
	}
}
