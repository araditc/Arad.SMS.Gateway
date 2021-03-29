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
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Arad.SMS.Gateway.ScheduledSms
{
	public class ScheduledThread : WorkerThread
	{
		private Thread synchronizerSendThread;
		public SmsSenderAgentReference SenderAgentRefrence { get; private set; }
		private static Random random = new Random();
		private static DataTable sentboxDataTable;
		private static int threadCount;
		ScheduledMessage scheduledMessage;
		List<ScheduledMessage> lstScheduledMessage;

		public ScheduledThread(int timeOut, SmsSenderAgentReference agent)
			: base(timeOut)
		{
			this.SenderAgentRefrence = agent;

			this.ThreadException += new System.Threading.ThreadExceptionEventHandler(SendThread_ThreadException);

			threadCount = Helper.GetInt(ConfigurationManager.GetSetting("ThreadCount"));
			lstScheduledMessage = new List<ScheduledMessage>();
		}

		protected override void WorkerThreadFunction()
		{
			if (this.IsStopSignaled)
				return;

			if (synchronizerSendThread == null || !synchronizerSendThread.IsAlive)
			{
				synchronizerSendThread = new Thread(new ThreadStart(SynchronizationSentbox));

				synchronizerSendThread.Start();
			}
		}

		private void SynchronizationSentbox()
		{
			try
			{
				lstScheduledMessage.Clear();
				sentboxDataTable = Arad.SMS.Gateway.Facade.ScheduledSms.GetQueue(threadCount, SenderAgentRefrence);
				foreach (DataRow row in sentboxDataTable.Rows)
				{
					AddRequest(Helper.GetGuid(row["Guid"]),
										 Helper.GetLong(row["ID"]),
										 (SmsSendType)Helper.GetInt(row["TypeSend"]),
										 (ScheduledSmsStatus)Helper.GetInt(row["Status"]),
										 Helper.GetGuid(row["PrivateNumberGuid"]),
										 Helper.GetGuid(row["UserGuid"]),
										 Helper.GetInt(row["SmsSenderAgentreference"]),
										 Helper.GetBool(row["SendSmsAlert"]),
										 Helper.GetTimeSpan(row["StartSendTime"]),
										 Helper.GetTimeSpan(row["EndSendTime"]));
				}

				if (lstScheduledMessage.Count > 0)
					ProcessRequest();
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SendSms, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SendSms, string.Format("\r\nWorkerThreadFunction(ScheduledSms) {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SendSms, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void AddRequest(Guid scheduledGuid, long id, SmsSendType typeSend,
														ScheduledSmsStatus scheduledSmsStatus, Guid privateNumberGuid,
														Guid userGuid, int smsSenderAgentreference, bool sendSmsAlert,
														TimeSpan startSendTime, TimeSpan endSendTime)
		{
			try
			{
				scheduledMessage = new ScheduledMessage();

				scheduledMessage.Guid = scheduledGuid;
                scheduledMessage.Id = id;
				scheduledMessage.TypeSend = typeSend;
				scheduledMessage.Status = scheduledSmsStatus;
				scheduledMessage.PrivateNumberGuid = privateNumberGuid;
				scheduledMessage.UserGuid = userGuid;
				scheduledMessage.SmsSenderAgentReference = smsSenderAgentreference;
				scheduledMessage.SendSmsAlert = sendSmsAlert;
				scheduledMessage.StartSendTime = startSendTime;
				scheduledMessage.EndSendTime = endSendTime;

				lstScheduledMessage.Add(scheduledMessage);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void ProcessRequest()
		{
			var senderThreads = new List<Thread>();

			for (int n = 0; n < lstScheduledMessage.Count; n++)
			{
				try
				{
                    Arad.SMS.Gateway.Facade.ScheduledSms.ProcessRequest(lstScheduledMessage[n].Guid,
																						 lstScheduledMessage[n].Id,
																						 lstScheduledMessage[n].Status,
																						 lstScheduledMessage[n].TypeSend,
																						 lstScheduledMessage[n].SmsSenderAgentReference,
																						 lstScheduledMessage[n].PrivateNumberGuid,
																						 lstScheduledMessage[n].UserGuid,
																						 lstScheduledMessage[n].SendSmsAlert,
																						 lstScheduledMessage[n].StartSendTime,
																						 lstScheduledMessage[n].EndSendTime);
				}
				catch (Exception ex)
				{
					LogController<ServiceLogs>.LogInFile(ServiceLogs.ScheduledSms, string.Format("ProcessRequest.ForEach: {0}", ex.Message));
				}
			}
		}


		private void SendThread_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.ScheduledSms, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.ScheduledSms, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
