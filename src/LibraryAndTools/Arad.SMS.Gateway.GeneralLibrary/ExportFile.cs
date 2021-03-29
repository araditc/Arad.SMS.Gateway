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
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Xsl;

namespace Arad.SMS.Gateway.GeneralLibrary
{
	public class ExportFile
	{
		public enum ExportFormat
		{
			CSV = 1,
			Excel = 2
		};
		public enum ApplicationType
		{
			Web = 1,
			Win = 2
		};

		private System.Web.HttpResponse response;
		private ApplicationType applicationType;

		public ExportFile(ApplicationType applicationType, System.Web.HttpResponse response)
		{
			this.applicationType = applicationType;

			if (this.applicationType == ApplicationType.Web)
				this.response = response;
		}

		/// <summary>
		///  To get all the column headers in the datatable and exorts in CSV / Excel format with all columns
		/// </summary>
		public void ExportDetails(DataTable detailsTable, ExportFormat formatType, string fileName)
		{
			try
			{
				if (detailsTable.Rows.Count == 0)
					throw new Exception("ThereAreNoDetailsToExport");

				DataSet exportDataSet = new DataSet("Export");
				DataTable exportDataTable = detailsTable.Copy();

				exportDataTable.TableName = "Values";
				exportDataSet.Tables.Add(exportDataTable);

				string[] headerList = new string[exportDataTable.Columns.Count];
				string[] filedList = new string[exportDataTable.Columns.Count];

				for (int i = 0; i < exportDataTable.Columns.Count; i++)
				{
					headerList[i] = exportDataTable.Columns[i].ColumnName;
					filedList[i] = ReplaceSpclChars(exportDataTable.Columns[i].ColumnName);
				}

				if (applicationType == ApplicationType.Web)
					ExportForWeb(exportDataSet, headerList, filedList, formatType, fileName);
				else if (applicationType == ApplicationType.Win)
					ExportForWindows(exportDataSet, headerList, filedList, formatType, fileName);
			}
			catch (Exception Ex)
			{
				throw Ex;
			}
		}

		/// <summary>
		/// To get the specified column headers in the datatable and exorts in CSV / Excel format with specified columns
		/// </summary>
		public void ExportDetails(DataTable detailsTable, string[] columnList, ExportFormat formatType, string fileName)
		{
			try
			{
				if (detailsTable.Rows.Count == 0)
					throw new Exception("ThereAreNoDetailsToExport");

				DataSet exportDataSet = new DataSet("Export");
				DataTable exportDataTable = detailsTable.Copy();

				exportDataTable.TableName = "Values";
				exportDataSet.Tables.Add(exportDataTable);

				if (columnList.Length > exportDataTable.Columns.Count)
					throw new Exception("ExportColumnListShouldNotExceedTotalColumns");

				string[] filedList = new string[columnList.Length];

				for (int i = 0; i < columnList.Length; i++)
				{
					if (!exportDataTable.Columns.Contains(columnList[i]))
						throw new Exception("ExportColumnNameNotExistInColumnList");

					filedList[i] = ReplaceSpclChars(columnList[i]);
				}

				if (applicationType == ApplicationType.Web)
					ExportForWeb(exportDataSet, columnList, filedList, formatType, fileName);
				else if (applicationType == ApplicationType.Win)
					ExportForWindows(exportDataSet, columnList, filedList, formatType, fileName);
			}
			catch (Exception Ex)
			{
				throw Ex;
			}
		}

		/// <summary>
		/// To get the specified column headers in the datatable and exorts in CSV / Excel format with specified columns and with specified headers
		/// </summary>
		public void ExportDetails(DataTable detailsTable, string[] columnList, string[] headers, ExportFormat formatType, string fileName)
		{
			try
			{
				if (detailsTable.Rows.Count == 0)
					throw new Exception("ThereAreNoDetailsToExport");

				DataSet exportDataSet = new DataSet("Export");
				DataTable exportDataTable = detailsTable.Copy();

				exportDataTable.TableName = "Values";
				exportDataSet.Tables.Add(exportDataTable);

				if (columnList.Length != headers.Length)
					throw new Exception("ExportColumnListAndHeadersListShouldBeOfSameLength");
				else if (columnList.Length > exportDataTable.Columns.Count || headers.Length > exportDataTable.Columns.Count)
					throw new Exception("ExportColumnListShouldNotExceedTotalColumns");

				string[] filedList = new string[columnList.Length];

				for (int i = 0; i < columnList.Length; i++)
				{
					if (!exportDataTable.Columns.Contains(columnList[i]))
						throw new Exception("ExportColumnNameNotExistInColumnList");

					filedList[i] = ReplaceSpclChars(columnList[i]);
				}

				if (applicationType == ApplicationType.Web)
					ExportForWeb(exportDataSet, headers, filedList, formatType, fileName);
				else if (applicationType == ApplicationType.Win)
					ExportForWindows(exportDataSet, headers, filedList, formatType, fileName);
			}
			catch (Exception Ex)
			{
				throw Ex;
			}
		}

		/// <summary>
		/// Exports dataset into CSV / Excel format
		/// </summary>
		private void ExportForWeb(DataSet exportDataSet, string[] headerList, string[] filedList, ExportFormat formatType, string fileName)
		{
			try
			{
				response.Clear();
				response.Buffer = true;

				if (formatType == ExportFormat.CSV)
				{
					response.ContentType = "text/csv";
					response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
				}
				else
				{
					response.ContentType = "application/vnd.ms-excel";
					response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
				}
					
				MemoryStream memoryStream = new MemoryStream();
				XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
				XmlDataDocument xmlDataDocument = new XmlDataDocument(exportDataSet);
				XslTransform xslTransform = new XslTransform();
				StringWriter stringWriter = new StringWriter();

				CreateStylesheet(xmlTextWriter, headerList, filedList, formatType);
				xmlTextWriter.Flush();
				memoryStream.Seek(0, SeekOrigin.Begin);

				xslTransform.Load(new XmlTextReader(memoryStream), null, null);
				xslTransform.Transform(xmlDataDocument, null, stringWriter, null);
								
				response.Write(stringWriter.ToString());
				stringWriter.Close();
				xmlTextWriter.Close();
				memoryStream.Close();
				response.End();
			}
			catch (ThreadAbortException ex)
			{
				string ErrMsg = ex.Message;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Exports dataset into CSV / Excel format
		/// </summary>
		private void ExportForWindows(DataSet exportDataSet, string[] headerList, string[] filedList, ExportFormat formatType, string fileName)
		{
			try
			{					
				MemoryStream memoryStream = new MemoryStream();
				XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
				XmlDataDocument xmlDataDocument = new XmlDataDocument(exportDataSet);
				XslTransform xslTransform = new XslTransform();
				StringWriter stringWriter = new StringWriter();
				StreamWriter streamWriter = new StreamWriter(fileName);

				CreateStylesheet(xmlTextWriter, headerList, filedList, formatType);
				xmlTextWriter.Flush();
				memoryStream.Seek(0, SeekOrigin.Begin);

				xslTransform.Load(new XmlTextReader(memoryStream), null, null);
				xslTransform.Transform(xmlDataDocument, null, stringWriter, null);

				streamWriter.WriteLine(stringWriter.ToString());
				streamWriter.Close();

				stringWriter.Close();
				xmlTextWriter.Close();
				memoryStream.Close();
			}
			catch (Exception Ex)
			{
				throw Ex;
			}
		}

		/// <summary>
		/// Creates XSLT file to apply on dataset's XML file
		/// </summary>
		private void CreateStylesheet(XmlTextWriter xmlTextWriter, string[] headerList, string[] filedList, ExportFormat formatType)
		{
			try
			{
				// xsl:stylesheet
				string ns = "http://www.w3.org/1999/XSL/Transform";
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument();
				xmlTextWriter.WriteStartElement("xsl", "stylesheet", ns);
				xmlTextWriter.WriteAttributeString("version", "1.0");
				xmlTextWriter.WriteStartElement("xsl:output");
				xmlTextWriter.WriteAttributeString("method", "text");
				xmlTextWriter.WriteAttributeString("version", "4.0");
				xmlTextWriter.WriteEndElement();

				// xsl-template
				xmlTextWriter.WriteStartElement("xsl:template");
				xmlTextWriter.WriteAttributeString("match", "/");

				// xsl:value-of for headers
				for (int i = 0; i < headerList.Length; i++)
				{
					xmlTextWriter.WriteString("\"");
					xmlTextWriter.WriteStartElement("xsl:value-of");
					xmlTextWriter.WriteAttributeString("select", "'" + headerList[i] + "'");
					xmlTextWriter.WriteEndElement(); // xsl:value-of
					xmlTextWriter.WriteString("\"");
					if (i != filedList.Length - 1) 
						xmlTextWriter.WriteString((formatType == ExportFormat.CSV) ? "," : "	");
				}

				// xsl:for-each
				xmlTextWriter.WriteStartElement("xsl:for-each");
				xmlTextWriter.WriteAttributeString("select", "Export/Values");
				xmlTextWriter.WriteString("\r\n");

				// xsl:value-of for data fields
				for (int i = 0; i < filedList.Length; i++)
				{
					xmlTextWriter.WriteString("\"");
					xmlTextWriter.WriteStartElement("xsl:value-of");
					xmlTextWriter.WriteAttributeString("select", filedList[i]);
					xmlTextWriter.WriteEndElement(); // xsl:value-of
					xmlTextWriter.WriteString("\"");

					if (i != filedList.Length - 1) 
						xmlTextWriter.WriteString((formatType == ExportFormat.CSV) ? "," : "	");
				}

				xmlTextWriter.WriteEndElement(); // xsl:for-each
				xmlTextWriter.WriteEndElement(); // xsl-template
				xmlTextWriter.WriteEndElement(); // xsl:stylesheet
				xmlTextWriter.WriteEndDocument();
			}
			catch (Exception Ex)
			{
				throw Ex;
			}
		}

		/// <summary>
		/// Replaces special characters with XML codes
		/// </summary>
		private string ReplaceSpclChars(string fieldName)
		{
			//			space 	-> 	_x0020_
			//			%		-> 	_x0025_
			//			#		->	_x0023_
			//			&		->	_x0026_
			//			/		->	_x002F_

			fieldName = fieldName.Replace(" ", "_x0020_");
			fieldName = fieldName.Replace("%", "_x0025_");
			fieldName = fieldName.Replace("#", "_x0023_");
			fieldName = fieldName.Replace("&", "_x0026_");
			fieldName = fieldName.Replace("/", "_x002F_");
			return fieldName;
		}
	}
}
