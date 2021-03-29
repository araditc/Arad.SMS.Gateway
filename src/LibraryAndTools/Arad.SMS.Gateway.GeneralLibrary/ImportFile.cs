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
using System.Data;
using System.Linq;
using System.Reflection;
using System.Data.OleDb;

namespace Arad.SMS.Gateway.GeneralLibrary
{
	public class ImportFile
	{
		#region	ImportCSV
		public static DataTable ImportCSV(string path, bool hasHeader)
		{
			return ImportCSV(path, hasHeader, 0);
		}

		public static DataTable ImportCSV(string path, bool hasHeader, int maxRow)
		{
			int totalRowCount;
			return ImportCSV(path, hasHeader, 0, out totalRowCount);
		}

		public static DataTable ImportCSV(string path, bool hasHeader, int maxRow, out int totalRowCount)
		{
			IEnumerable<string> enumerableOfAllLines = System.IO.File.ReadLines(path, System.Text.Encoding.Default);
			string[] allLines = enumerableOfAllLines.ToArray();
			DataRow row;
			DataTable dtResualt = new DataTable();
			string[] currentLineItems;
			string cellValue = string.Empty;
			int index = 0;
			int cellsCount = 0;
			bool isEmpty = true;
			totalRowCount = 0;

			if (allLines.Length > 0)
			{
				currentLineItems = allLines[0].Split(',');
				while (cellsCount < currentLineItems.Length)
				{
					if (hasHeader)
						dtResualt.Columns.Add(currentLineItems[cellsCount], typeof(string));
					else
						dtResualt.Columns.Add("Cell" + cellsCount, typeof(string));
					cellsCount++;
				}

				if (hasHeader)
					index = 1;
				else
					index = 0;

				for (int rowCount = index; rowCount < allLines.Length; rowCount++)
				{
					currentLineItems = allLines[rowCount].Split(',');
					row = dtResualt.NewRow();
					isEmpty = true;

					for (int cellIndex = 0; cellIndex < cellsCount; cellIndex++)
					{
						try
						{
							cellValue = currentLineItems[cellIndex];

							if (cellValue != string.Empty)
								isEmpty = false;

							row[cellIndex] = cellValue;
						}
						catch
						{
							row[cellIndex] = DBNull.Value;
						}
					}

					if (!isEmpty && (maxRow == 0 || totalRowCount <= maxRow))
						dtResualt.Rows.Add(row);

					totalRowCount++;
				}
			}
			return dtResualt;
		}
		#endregion

		#region ImportExcel
		public static DataTable ImportExcel(string path, bool hasHeader)
		{
			return ImportExcel(path, hasHeader, 0);
		}

		public static DataTable ImportExcel(string path, bool hasHeader, int maxRow)
		{
			int totalRowCount;
			return ImportExcel(path, hasHeader, maxRow, out totalRowCount);
		}

		public static DataTable ImportExcel(string path, bool hasHeader, int maxRow, out int totalRowCount)
		{
			return GetDataTableWithOLEDB(path, hasHeader, maxRow, out totalRowCount);
		}
		#endregion

		#region GetDataTableFromXLS()
		private static DataTable GetDataTableFromXLS(string excelSheetFilePath, string dataRange, bool hasHeader, int maxRow, out int totalRowCount)
		{
			//Source URL: http://www.devcurry.com/2009/07/import-excel-data-into-aspnet-gridview_06.html

			Microsoft.Office.Interop.Excel.Application excel = null;
			Microsoft.Office.Interop.Excel.Workbook workbook = null;
			Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
			Microsoft.Office.Interop.Excel.Range range = null;
			try
			{
				excel = new Microsoft.Office.Interop.Excel.Application();
				workbook = excel.Workbooks.Open(excelSheetFilePath, Missing.Value, Missing.Value, Missing.Value,
					Missing.Value, Missing.Value, Missing.Value,
					Missing.Value, Missing.Value, Missing.Value,
					Missing.Value, Missing.Value, Missing.Value,
					Missing.Value, Missing.Value);

				//   get   WorkSheet object
				worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
				if (string.IsNullOrEmpty(dataRange))
				{
					range = worksheet.UsedRange;
				}
				else
				{
					#region Decrease the range end ROW boundry to the used range rows count
					string[] rangeBoundries = dataRange.Split(':');
					string endBoundryX = string.Empty;
					string endBoundryY = rangeBoundries[1];
					while (Helper.GetInt(endBoundryY) == 0)
					{
						endBoundryX += endBoundryY.Substring(0, 1);
						endBoundryY = endBoundryY.Substring(1);
					}

					if (Helper.GetInt(endBoundryY) > worksheet.UsedRange.Cells.Rows.Count)
						endBoundryY = worksheet.UsedRange.Cells.Rows.Count.ToString();
					#endregion

					range = worksheet.get_Range(rangeBoundries[0], endBoundryX + endBoundryY);
				}

				DataTable resultsDataTable = new DataTable("dtExcel");
				DataRow resultDataRow;

				int columnsCount = range.Cells.Columns.Count;
				int rowsCount = range.Cells.Rows.Count;
				//  get data columns
				for (int j = 1; j <= columnsCount; j++)
				{
					if (hasHeader)
						resultsDataTable.Columns.Add(((Microsoft.Office.Interop.Excel.Range)range.Cells[1, j]).Text.ToString(), System.Type.GetType("System.String"));
					else
						resultsDataTable.Columns.Add("column" + j, System.Type.GetType("System.String"));
				}

				totalRowCount = 0;
				//  get data in cell    
				for (int i = (hasHeader ? 2 : 1); i <= rowsCount; i++)
				{
					if (maxRow == 0 || totalRowCount <= maxRow)
					{
						resultDataRow = resultsDataTable.NewRow();

						for (int j = 1; j <= columnsCount; j++)
						{
							string cellValue = ((Microsoft.Office.Interop.Excel.Range)range.Cells[i, j]).Text.ToString();
							cellValue = Helper.GetStandardizeCharacters(cellValue, "persian");
							resultDataRow[j - 1] = cellValue;
						}

						resultsDataTable.Rows.Add(resultDataRow);
					}

					totalRowCount++;
				}

				return resultsDataTable;
			}
			finally
			{
				if (range != null)
					System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
				if (worksheet != null)
					System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
				if (workbook != null)
					System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
				if (excel != null)
				{
					excel.Quit();
					System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
				}

				worksheet = null;
				workbook = null;
				excel = null;
				range = null;
				GC.Collect();
			}
		}
		#endregion

		#region GetDataTableWithOLEDB
		private static DataTable GetDataTableWithOLEDB(string excelSheetFilePath, bool hasHeader, int maxRow, out int totalRowCount)
		{
			string query = string.Empty;
			string connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=""Excel 12.0 Xml;HDR={1};""", excelSheetFilePath, hasHeader ? "YES" : "NO");
			DataTable dtExcel = new DataTable();
			DataTable dtRowCount = new DataTable();

			if (maxRow == 0)
				query = "SELECT * FROM [{0}];";
			else
				query = "SELECT TOP " + maxRow + " * FROM [{0}];";

			using (OleDbConnection conn = new OleDbConnection(connectionString))
			{
				conn.Open();
				DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
				List<string> listSheet = new List<string>();
				foreach (DataRow drSheet in dtSheet.Rows)
				{
					if (drSheet["TABLE_NAME"].ToString().Contains("$"))//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
						listSheet.Add(drSheet["TABLE_NAME"].ToString());
				}

				query = string.Format(query, listSheet[0].Trim('\''));
				OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, conn);

				dataAdapter.Fill(dtExcel);

				dataAdapter.SelectCommand.CommandText = string.Format("SELECT COUNT(*) AS [RowCount] FROM [{0}];", listSheet[0].Trim('\''));
				dataAdapter.Fill(dtRowCount);
			}

			totalRowCount = Helper.GetInt(dtRowCount.Rows[0]["RowCount"]);
			return dtExcel;
		}
		#endregion
	}
}
