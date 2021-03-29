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

namespace Arad.SMS.Gateway.ScheduledSms
{
	public partial class ScheduledSmsService : ServiceBase
	{
		private ScheduledThread magfaScheduledThread;
		private ScheduledThread asanakScheduledThread;
		private ScheduledThread armaghanScheduledThread;
		private ScheduledThread aradbulkScheduledThread;
		private ScheduledThread rahyabRGScheduledThread;
		private ScheduledThread rahyabPGScheduledThread;
		private ScheduledThread slsScheduledThread;
		private ScheduledThread shreewebScheduledThread;
		private ScheduledThread aradvasScheduledThread;
		private ScheduledThread socialNetworkScheduledThread;
		private ScheduledThread fffScheduledThread;
		private ScheduledThread gsmScheduledThread;
        private ScheduledThread mobbisScheduledThread;
        private GarbageCollectorThread gcThread;
		public ScheduledSmsService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			try
			{
				Arad.SMS.Gateway.GeneralLibrary.BaseCore.DataAccessBase.GetActiveDataAccessProvider = Arad.SMS.Gateway.DataAccessLayer.SqlDataAccess.GetSqlDataAccess;
				Language.ActiveLanguage = Language.AvalibaleLanguages.fa;

				gcThread = new GarbageCollectorThread(3 * 60 * 1000);
				gcThread.Start();

				//Thread.Sleep(30000);
				magfaScheduledThread = new ScheduledThread(1 * 1000, SmsSenderAgentReference.Magfa);
				asanakScheduledThread = new ScheduledThread(1 * 1000, SmsSenderAgentReference.Asanak);
				armaghanScheduledThread = new ScheduledThread(1 * 1000, SmsSenderAgentReference.Armaghan);
				aradbulkScheduledThread = new ScheduledThread(1 * 1000, SmsSenderAgentReference.AradBulk);
				rahyabRGScheduledThread = new ScheduledThread(1 * 1000, SmsSenderAgentReference.RahyabRG);
				rahyabPGScheduledThread = new ScheduledThread(1 * 1000, SmsSenderAgentReference.RahyabPG);
				slsScheduledThread = new ScheduledThread(1 * 1000, SmsSenderAgentReference.SLS);
				shreewebScheduledThread = new ScheduledThread(1 * 1000, SmsSenderAgentReference.Shreeweb);
				aradvasScheduledThread = new ScheduledThread(1 * 1000, SmsSenderAgentReference.AradVas);
				socialNetworkScheduledThread = new ScheduledThread(1 * 1000, SmsSenderAgentReference.SocialNetworks);
				gsmScheduledThread = new ScheduledThread(1 * 1000, SmsSenderAgentReference.GSMGateway);
				fffScheduledThread = new ScheduledThread(1 * 1000, SmsSenderAgentReference.FFF);
                mobbisScheduledThread = new ScheduledThread(1 * 100, SmsSenderAgentReference.Mobbis);

				magfaScheduledThread.Start();
				asanakScheduledThread.Start();
				armaghanScheduledThread.Start();
				aradbulkScheduledThread.Start();
				rahyabRGScheduledThread.Start();
				rahyabPGScheduledThread.Start();
				slsScheduledThread.Start();
				shreewebScheduledThread.Start();
				aradvasScheduledThread.Start();
				socialNetworkScheduledThread.Start();
				gsmScheduledThread.Start();
				fffScheduledThread.Start();
                mobbisScheduledThread.Start();

            }
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ScheduledSms, string.Format("\r\n------------------OnStart-------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ScheduledSms, string.Format("On Start ScheduledSmsService : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ScheduledSms, string.Format("\r\n--------------------------------------------------------"));
			}
		}

		protected override void OnStop()
		{
			try
			{
				if (magfaScheduledThread != null)
					magfaScheduledThread.Stop();
				if (asanakScheduledThread != null)
					asanakScheduledThread.Stop();
				if (armaghanScheduledThread != null)
					armaghanScheduledThread.Stop();
				if (aradbulkScheduledThread != null)
					aradbulkScheduledThread.Stop();
				if (rahyabRGScheduledThread != null)
					rahyabRGScheduledThread.Stop();
				if (rahyabPGScheduledThread != null)
					rahyabPGScheduledThread.Stop();
				if (slsScheduledThread != null)
					slsScheduledThread.Stop();
				if (shreewebScheduledThread != null)
					shreewebScheduledThread.Stop();
				if (aradvasScheduledThread != null)
					aradvasScheduledThread.Stop();
				if (socialNetworkScheduledThread != null)
					socialNetworkScheduledThread.Stop();
				if (gsmScheduledThread != null)
					gsmScheduledThread.Stop();
				if (fffScheduledThread != null)
					fffScheduledThread.Stop();
                if (mobbisScheduledThread != null)
                    mobbisScheduledThread.Stop();


                if (gcThread != null)
					gcThread.Stop();
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ScheduledSms, string.Format("\r\n------------------OnStop-------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ScheduledSms, string.Format("On Stop ScheduledSmsService : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ScheduledSms, string.Format("\r\n--------------------------------------------------------"));
			}
		}

		protected override void OnShutdown()
		{
			try
			{
				if (magfaScheduledThread != null)
					magfaScheduledThread.Stop();
				if (asanakScheduledThread != null)
					asanakScheduledThread.Stop();
				if (armaghanScheduledThread != null)
					armaghanScheduledThread.Stop();
				if (aradbulkScheduledThread != null)
					aradbulkScheduledThread.Stop();
				if (rahyabRGScheduledThread != null)
					rahyabRGScheduledThread.Stop();
				if (rahyabPGScheduledThread != null)
					rahyabPGScheduledThread.Stop();
				if (slsScheduledThread != null)
					slsScheduledThread.Stop();
				if (shreewebScheduledThread != null)
					shreewebScheduledThread.Stop();
				if (aradvasScheduledThread != null)
					aradvasScheduledThread.Stop();
				if (socialNetworkScheduledThread != null)
					socialNetworkScheduledThread.Stop();
				if (gsmScheduledThread != null)
					gsmScheduledThread.Stop();
				if (fffScheduledThread != null)
					fffScheduledThread.Stop();
                if (mobbisScheduledThread != null)
                    mobbisScheduledThread.Stop();

                if (gcThread != null)
					gcThread.Stop();

				base.OnShutdown();
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ScheduledSms, string.Format("\r\n------------------OnShutdown-------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ScheduledSms, string.Format("On Shutdown ScheduledSmsService : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ScheduledSms, string.Format("\r\n--------------------------------------------------------"));
			}
		}
	}
}
