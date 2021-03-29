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
using System.Linq;
using System.Threading;

namespace Arad.SMS.Gateway.GiveBackCredit
{
	public class GiveBackThread : WorkerThread
	{
		private Thread synchronizerSendThread;
		private DataTable dtRequests;

		public GiveBackThread(int timeOut)
			: base(timeOut)
		{
			this.ThreadException += new ThreadExceptionEventHandler(GiveBackThread_ThreadException);

			dtRequests = new DataTable();
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
				dtRequests.Clear();
				TimeSpan nowTime = DateTime.Now.TimeOfDay;
				List<TimeSpan> lstStartTime = new List<TimeSpan>();
				List<TimeSpan> lstEndTime = new List<TimeSpan>();

				lstStartTime.Add(new TimeSpan(0, 0, 0));
				lstEndTime.Add(new TimeSpan(08, 0, 0));

				//lstStartTime.Add(new TimeSpan(20, 0, 0));
				//lstEndTime.Add(new TimeSpan(0, 0, 0));

				if (lstStartTime.Where(start => start < nowTime).Count() > 0 &&
						lstEndTime.Where(end => end > nowTime).Count() > 0)
				{
					dtRequests = Facade.Outbox.GetOutboxForGiveBackCredit();

					foreach (DataRow row in dtRequests.Rows)
						CalculateGiveBackMessages(Helper.GetGuid(row["Guid"]), Helper.GetString(row["ID"]), (SendStatus)Helper.GetInt(row["SendStatus"]));
				}

				#region Check Expiration Panel
				TimeSpan time = new TimeSpan(8, 0, 0);
				if (DateTime.Now.TimeOfDay == time)
					Facade.User.SendSmsForUserExpired();
				#endregion

				#region Run GarbageCollector For [ScheduledSms]
				time = new TimeSpan(23, 0, 0);
				if (DateTime.Now.TimeOfDay == time)
					Facade.ScheduledSms.GarbageCollector();
				#endregion
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.GiveBackCredit, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.GiveBackCredit, string.Format("\r\n WorkerThreadFunction : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.GiveBackCredit, string.Format("\r\n WorkerThreadFunction : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.GiveBackCredit, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void CalculateGiveBackMessages(Guid guid, string id, SendStatus sendStatus)
		{
			try
			{
				switch (sendStatus)
				{
					case SendStatus.Stored:
					case SendStatus.WatingForSend:
					case SendStatus.IsBeingSent:
						Facade.Outbox.CheckReceiverCount(guid);
						break;
					case SendStatus.Sent:
						Facade.Outbox.GiveBackBlackListAndFailedSend(guid);
						//Facade.Outbox.AddBlackListNumbersToTable(guid);
						break;
					case SendStatus.SentAndGiveBackCredit:
						Facade.Outbox.ArchiveNumbers(guid);
						break;
					case SendStatus.Archiving:
						//ArchiveSendReportFile(id);
						Facade.OutboxNumber.DeleteNumbers(guid);
						break;
				}
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.GiveBackCredit, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.GiveBackCredit, string.Format("\r\n CalculateGiveBackMessages : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.GiveBackCredit, string.Format("\r\n CalculateGiveBackMessages : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.GiveBackCredit, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void GiveBackThread_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.GiveBackCredit, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.GiveBackCredit, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
