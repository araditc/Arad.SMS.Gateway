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
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading;

namespace Arad.SMS.Gateway.RegularContent
{
	public class ProcessRegularContentThread : WorkerThread
	{
		private Thread synchronizerProcessRegularContentThread;
		private DataTable dtRegularContents;
		private SqlConnection connection;
		private SqlCommand command;
		private SqlDataAdapter dataAdapter;
		public ProcessRegularContentThread(int timeOut)
			: base(timeOut)
		{
			this.ThreadException += new System.Threading.ThreadExceptionEventHandler(RegularContentThread_ThreadException);
		}

		protected override void WorkerThreadFunction()
		{
			if (this.IsStopSignaled)
				return;

			if (synchronizerProcessRegularContentThread == null || !synchronizerProcessRegularContentThread.IsAlive)
			{
				synchronizerProcessRegularContentThread = new Thread(new ThreadStart(SynchronizationProcessRegularContent));

				synchronizerProcessRegularContentThread.Start();
			}
		}

		private void SynchronizationProcessRegularContent()
		{
			try
			{
				dtRegularContents.Clear();
				dtRegularContents = Facade.RegularContent.GetRegularContentForProcess();

				foreach (DataRow row in dtRegularContents.Rows)
					ProcessRegularContent(Helper.GetGuid(row["Guid"]),
																Helper.GetGuid(row["PrivateNumberGuid"]),
																(Business.RegularContentType)(Helper.GetInt(row["Type"])),
																Helper.GetString(row["Config"]),
																(Business.WarningType)(Helper.GetInt(row["WarningType"])),
																Helper.GetGuid(row["UserGuid"]),
																(Business.SmsSentPeriodType)Helper.GetInt(row["PeriodType"]),
																Helper.GetInt(row["Period"]),
																Helper.GetDateTime(row["EffectiveDateTime"]));

				#region process regularcontent file type
				TimeSpan nowTime = DateTime.Now.TimeOfDay;
				List<TimeSpan> lstStartTime = new List<TimeSpan>();
				List<TimeSpan> lstEndTime = new List<TimeSpan>();

				lstStartTime.Add(new TimeSpan(0, 0, 0));
				lstEndTime.Add(new TimeSpan(08, 0, 0));

				lstStartTime.Add(new TimeSpan(20, 0, 0));
				lstEndTime.Add(new TimeSpan(0, 0, 0));

				if (lstStartTime.Where(start => start < nowTime).Count() > 0 &&
						lstEndTime.Where(end => end > nowTime).Count() > 0)
				{
					dtRegularContents.Clear();
					dtRegularContents = Facade.RegularContent.GetRegularContentFileType();
					foreach (DataRow row in dtRegularContents.Rows)
						SendContentToReceivers(
							Helper.GetGuid(row["Guid"]),
							Helper.GetGuid(row["PrivateNumberGuid"]),
							Helper.GetGuid(row["UserGuid"]),
							(Business.SmsSentPeriodType)Helper.GetInt(row["PeriodType"]),
							Helper.GetInt(row["Period"]),
							Helper.GetDateTime(row["EffectiveDateTime"])
						);
				}
				#endregion
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n WorkerThreadFunction : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n WorkerThreadFunction : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void ProcessRegularContent(Guid regularContentGuid, Guid privateNumberGuid, Business.RegularContentType type,
																			 string config, Business.WarningType warningType, Guid userGuid, Business.SmsSentPeriodType periodType,
																			 int period, DateTime effectiveDateTime)
		{
			try
			{
				string response = string.Empty;
				Business.RegularContentSerialization regularContentSerialization = (Business.RegularContentSerialization)GeneralLibrary.SerializationTools.DeserializeXml(config, typeof(Business.RegularContentSerialization));

				switch (type)
				{
					case Business.RegularContentType.URL:
						try
						{
							using (var wb = new WebClient())
							{
								response = wb.DownloadString(regularContentSerialization.URL);
							}
						}
						catch (Exception ex)
						{
							LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n-------------------------------------------------------------------------"));

							LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n RegularContentType.URL URL: {0}", regularContentSerialization.URL));
							LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n RegularContentType.URL Response: {0}", response));
							LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n RegularContentType.URL DownloadString: {0}", ex.Message));
							LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n-------------------------------------------------------------------------"));

							switch (warningType)
							{
								case Business.WarningType.Sms:
									//Facade.ScheduledSms.InsertSms()
									break;
							}

							throw ex;
						}

						if (!string.IsNullOrEmpty(response))
							Facade.RegularContent.SendURLContentToReceiver(regularContentGuid, privateNumberGuid, userGuid, response, periodType, period, effectiveDateTime);

						break;
					case Business.RegularContentType.DB:
						try
						{
							DataTable dataTable = new DataTable();
							dataAdapter = new SqlDataAdapter();
							command = new SqlCommand();
							connection = new SqlConnection(regularContentSerialization.ConnectionString);

							command.CommandType = CommandType.Text;
							command.CommandText = regularContentSerialization.Query;
							command.Connection = connection;
							command.CommandTimeout = 60;

							dataAdapter.SelectCommand = command;

							connection.Open();
							dataAdapter.Fill(dataTable);

							if (dataTable.Rows.Count > 0)
								response = dataTable.Rows[0][regularContentSerialization.Field].ToString();
						}
						catch (Exception ex)
						{
							LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n-------------------------------------------------------------------------"));
							LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n RegularContentType.DB Guid: {0}", regularContentGuid));
							LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n RegularContentType.DB Error: {0}", ex.Message));
							LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n-------------------------------------------------------------------------"));

							switch (warningType)
							{
								case Business.WarningType.Sms:
									//Facade.ScheduledSms.InsertSms()
									break;
							}

							throw ex;
						}

						if (!string.IsNullOrEmpty(response))
							Facade.RegularContent.SendDBContentToReceiver(regularContentGuid, privateNumberGuid, userGuid, response, periodType, period, effectiveDateTime);
						break;
				}
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n ProcessRegularContent : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n ProcessRegularContent : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void SendContentToReceivers(Guid regularContentGuid, Guid privateNumberGuid,
																				Guid userGuid, Business.SmsSentPeriodType periodType, int period,
																				DateTime effectiveDateTime)
		{
			try
			{
				Facade.Content.SendContentToReceiver(regularContentGuid, privateNumberGuid, userGuid, periodType, period, effectiveDateTime);
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n ProcessRegularContent : {0}", ex.Message));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n ProcessRegularContent : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void RegularContentThread_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.RegularContent, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
