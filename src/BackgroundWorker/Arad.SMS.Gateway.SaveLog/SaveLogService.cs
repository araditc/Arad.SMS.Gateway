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
using Arad.SMS.Gateway.ManageThread;
using System;
using System.ServiceProcess;

namespace SaveLog
{
	public partial class SaveLogService : ServiceBase
	{
		private LogThread saveThread;
		private GarbageCollectorThread gcThread;
		public SaveLogService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			try
			{
				gcThread = new GarbageCollectorThread(3 * 60 * 1000);
				gcThread.Start();

				saveThread = new LogThread(5 * 1000);

				saveThread.Start();
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("\r\n------------------OnStart-------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("On Start SaveLogService : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("\r\n--------------------------------------------------------"));
			}
		}

		protected override void OnStop()
		{
			try
			{
				if (saveThread != null)
					saveThread.Stop();

				if (gcThread != null)
					gcThread.Stop();
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("\r\n------------------OnStop-------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("On Stop SaveLogService : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("\r\n--------------------------------------------------------"));
			}
		}

		protected override void OnShutdown()
		{
			try
			{
				if (saveThread != null)
					saveThread.Stop();

				if (gcThread != null)
					gcThread.Stop();

				base.OnShutdown();
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("\r\n------------------OnShutdown-------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("On Shutdown SaveLogService : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.Log, string.Format("\r\n--------------------------------------------------------"));
			}
		}
	}
}
