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
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.ManageThread;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;

namespace Arad.SMS.Gateway.ExportData
{
	public class ExportThread : WorkerThread
	{
		InProgressRequest inprogressRequest;
		List<InProgressRequest> lstInProgressRequest = new List<InProgressRequest>();
		private Thread synchronizerSendThread;
		static private string saveFileAddress;
		static private int rowsPerSheet;
		static private DataTable resultsData = new DataTable();
		private DataTable dtRequests;
		DataTable dtSaveRequest;
		private DataRow newRow;
		static private int threadCount;
		public ExportThread(int timeOut)
			: base(timeOut)
		{
			try
			{
				this.ThreadException += new ThreadExceptionEventHandler(ExportThread_ThreadException);

				dtRequests = new DataTable();
				dtSaveRequest = new DataTable();
				dtSaveRequest.Columns.Add("Guid", typeof(Guid));
				dtSaveRequest.Columns.Add("PageNo", typeof(int));
				dtSaveRequest.Columns.Add("Status", typeof(byte));
				dtSaveRequest.Columns.Add("TxtPageNo", typeof(int));
				dtSaveRequest.Columns.Add("TxtStatus", typeof(byte));
				dtSaveRequest.Columns.Add("SendStatus", typeof(byte));


				saveFileAddress = ConfigurationManager.GetSetting("SaveFileAddress");
				rowsPerSheet = Helper.GetInt(ConfigurationManager.GetSetting("RowsPerSheet"));
				threadCount = Helper.GetInt(ConfigurationManager.GetSetting("ThreadCount"));
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n ExportThread constructed : {0}", ex.ToString()));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n ExportThread constructed : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		protected override void WorkerThreadFunction()
		{
			try
			{
				if (this.IsStopSignaled)
					return;

				if (synchronizerSendThread == null || !synchronizerSendThread.IsAlive)
				{
					synchronizerSendThread = new Thread(new ThreadStart(SynchronizationExportData));

					synchronizerSendThread.Start();
				}
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n ExportThread WorkerThreadFunction : {0}", ex.ToString()));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n ExportThread WorkerThreadFunction : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void SynchronizationExportData()
		{
			try
			{
				dtRequests.Clear();
				dtSaveRequest.Clear();
				lstInProgressRequest.Clear();

				dtRequests = Facade.Outbox.GetExportDataRequest(threadCount);

				foreach (DataRow row in dtRequests.Rows)
				{
					inprogressRequest = new InProgressRequest();

					inprogressRequest.Guid = Helper.GetGuid(row["Guid"]);
					inprogressRequest.Id = Helper.GetLong(row["ID"]);
					inprogressRequest.PageNo = Helper.GetInt(row["PageNo"]) == 0 ? 1 : Helper.GetInt(row["PageNo"]);
					inprogressRequest.ExportStatus = (ExportDataStatus)Helper.GetInt(row["Status"]);
					inprogressRequest.TxtPageNo = Helper.GetInt(row["TxtPageNo"]) == 0 ? 1 : Helper.GetInt(row["TxtPageNo"]);
					inprogressRequest.ExportTxtStatus = (ExportDataStatus)Helper.GetInt(row["TxtStatus"]);
					inprogressRequest.SendStatus = (SendStatus)Helper.GetInt(row["SendStatus"]);

					lstInProgressRequest.Add(inprogressRequest);
				}

				if (lstInProgressRequest.Count > 0)
					Export();
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n SynchronizationExportData : {0}", ex.ToString()));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n SynchronizationExportData : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void Export()
		{
			try
			{
				int necessaryThread = lstInProgressRequest.Count;

				var threads = new List<Thread>();

				for (int i = 0; i < necessaryThread; i++)
				{
					try
					{
						var request = lstInProgressRequest[i];

						var thread = new Thread(() => ExportData(request));
						thread.Start();

						threads.Add(thread);
					}
					catch (Exception ex)
					{
						LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("create thread for necessaryThread: {0}", ex.Message));
					}
				}

				foreach (var thread in threads)
					thread.Join();

				UpdateRequest();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void UpdateRequest()
		{
			try
			{
				foreach (InProgressRequest rq in lstInProgressRequest)
				{
					newRow = dtSaveRequest.NewRow();

					newRow["Guid"] = rq.Guid;
					newRow["PageNo"] = rq.PageNo;
					newRow["Status"] = (int)rq.ExportStatus;
					newRow["TxtPageNo"] = rq.TxtPageNo;
					newRow["TxtStatus"] = (int)rq.ExportTxtStatus;
					newRow["SendStatus"] = (int)rq.SendStatus;

					dtSaveRequest.Rows.Add(newRow);
				}

				Facade.Outbox.UpdateExportDataRequest(dtSaveRequest);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void ExportData(InProgressRequest request)
		{
			try
			{
				if (request.ExportStatus == ExportDataStatus.Complete && request.ExportTxtStatus == ExportDataStatus.Complete)
				{
					request.ExportStatus = ExportDataStatus.Archived;
					return;
				}

				if (request.ExportStatus == ExportDataStatus.Get)
					ExportToExcel(request);

				if (request.ExportTxtStatus == ExportDataStatus.Get)
					ExportToText(request);

			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n-------------------------------------------------------------------------"));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n ExportData Method : {0}", ex.ToString()));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n ExportData Method : {0}", ex.StackTrace));
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("\r\n-------------------------------------------------------------------------"));
			}
		}

		private void ExportToText(InProgressRequest request)
		{
			bool firstTime = request.PageNo == 1 ? true : false;
			bool completed = false;
			string rootDirectory = string.Format(@"{0}{1}{2}", saveFileAddress, Path.DirectorySeparatorChar, request.Id);

			if (!Directory.Exists(rootDirectory))
				Directory.CreateDirectory(rootDirectory);

			string filePath = string.Format("{0}{1}{2}.txt", rootDirectory, Path.DirectorySeparatorChar, request.Id);

			if (firstTime && File.Exists(filePath))
				File.Delete(filePath);

			#region GerRecords
			DataTable dtResult = Facade.Outbox.GetPagedExportText(request.Guid, request.TxtPageNo, rowsPerSheet);
			completed = dtResult.Rows.Count == 0 ? true : false;
			#endregion

			FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter streamWriter = new StreamWriter(fileStream);

			streamWriter.BaseStream.Seek(0, SeekOrigin.End);

			foreach (DataRow row in dtResult.Rows)
				streamWriter.WriteLine(string.Format("{0}{1}", row["Receiver"], Environment.NewLine));

			streamWriter.Flush();
			streamWriter.Close();

			streamWriter.Dispose();
			fileStream.Dispose();

			request.TxtPageNo++;
		}

		private void ExportToExcel(InProgressRequest request)
		{
			string rootDirectory = string.Format(@"{0}{1}{2}", saveFileAddress, Path.DirectorySeparatorChar, request.Id);

			if (!Directory.Exists(rootDirectory))
				Directory.CreateDirectory(rootDirectory);

			string fileName = string.Format(@"{0}\{1}\{1}.xlsx", saveFileAddress, request.Id);
			//LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("fileName: {0}", fileName));

			bool firstTime = request.PageNo == 1 ? true : false;
			bool completed = false;
			//Delete the file if it exists. 
			if (firstTime && File.Exists(fileName))
			{
				File.Delete(fileName);
				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("file deleted: {0}", fileName));
			}

			uint sheetId = 1; //Start at the first sheet in the Excel workbook.

			#region GerRecords
			DataTable resultsData = Facade.Outbox.GetPagedExportData(request.Guid, request.PageNo, rowsPerSheet);
			completed = resultsData.Rows.Count == 0 ? true : false;
			#endregion

			if (firstTime)
			{
				//This is the first time of creating the excel file and the first sheet.
				// Create a spreadsheet document by supplying the filepath.
				// By default, AutoSave = true, Editable = true, and Type = xlsx.
				SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook);

				LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("file create: {0}", fileName));
				// Add a WorkbookPart to the document.
				WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
				workbookpart.Workbook = new Workbook();

				// Add a WorksheetPart to the WorkbookPart.
				var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
				var sheetData = new SheetData();
				worksheetPart.Worksheet = new Worksheet(sheetData);


				var bold1 = new Bold();
				CellFormat cf = new CellFormat();


				// Add Sheets to the Workbook.
				Sheets sheets;
				sheets = spreadsheetDocument.WorkbookPart.Workbook.
						AppendChild<Sheets>(new Sheets());

				// Append a new worksheet and associate it with the workbook.
				var sheet = new Sheet()
				{
					Id = spreadsheetDocument.WorkbookPart.
							GetIdOfPart(worksheetPart),
					SheetId = sheetId,
					Name = "Sheet" + sheetId
				};
				sheets.Append(sheet);

				//Add Header Row.
				var headerRow = new Row();
				foreach (DataColumn column in resultsData.Columns)
				{
					var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
					headerRow.AppendChild(cell);
				}
				sheetData.AppendChild(headerRow);

				foreach (DataRow row in resultsData.Rows)
				{
					var newRow = new Row();
					foreach (DataColumn col in resultsData.Columns)
					{
						var cell = new Cell
						{
							DataType = CellValues.String,
							CellValue = new CellValue(row[col].ToString())
						};
						newRow.AppendChild(cell);
					}

					sheetData.AppendChild(newRow);
				}
				workbookpart.Workbook.Save();

				spreadsheetDocument.Close();
			}
			else if (!completed)
			{
				// Open the Excel file that we created before, and start to add sheets to it.
				var spreadsheetDocument = SpreadsheetDocument.Open(fileName, true);

				var workbookpart = spreadsheetDocument.WorkbookPart;
				if (workbookpart.Workbook == null)
					workbookpart.Workbook = new Workbook();

				var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
				var sheetData = new SheetData();
				worksheetPart.Worksheet = new Worksheet(sheetData);
				var sheets = spreadsheetDocument.WorkbookPart.Workbook.Sheets;

				if (sheets.Elements<Sheet>().Any())
				{
					//Set the new sheet id
					sheetId = sheets.Elements<Sheet>().Max(s => s.SheetId.Value) + 1;
				}
				else
				{
					sheetId = 1;
				}

				// Append a new worksheet and associate it with the workbook.
				var sheet = new Sheet()
				{
					Id = spreadsheetDocument.WorkbookPart.
							GetIdOfPart(worksheetPart),
					SheetId = sheetId,
					Name = "Sheet" + sheetId
				};
				sheets.Append(sheet);

				//Add the header row here.
				var headerRow = new Row();

				foreach (DataColumn column in resultsData.Columns)
				{
					var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
					headerRow.AppendChild(cell);
				}
				sheetData.AppendChild(headerRow);

				foreach (DataRow row in resultsData.Rows)
				{
					var newRow = new Row();

					foreach (DataColumn col in resultsData.Columns)
					{
						var cell = new Cell
						{
							DataType = CellValues.String,
							CellValue = new CellValue(row[col].ToString())
						};
						newRow.AppendChild(cell);
					}

					sheetData.AppendChild(newRow);
				}

				workbookpart.Workbook.Save();

				// Close the document.
				spreadsheetDocument.Close();
			}

			request.PageNo++;

			if (completed)
				request.ExportStatus = ExportDataStatus.Archived;
		}

		private void ExportThread_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("Exception Message : {0}", e.Exception.Message));
			LogController<ServiceLogs>.LogInFile(ServiceLogs.ExportData, string.Format("Exception : {0}", e.Exception.StackTrace));
		}
	}
}
