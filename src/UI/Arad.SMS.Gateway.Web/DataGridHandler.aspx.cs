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

using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Arad.SMS.Gateway.Web
{
	public partial class DataGridHandler : System.Web.UI.Page
	{
		private new static System.Web.SessionState.HttpSessionState Session
		{
			get { return HttpContext.Current.Session; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			string loadedControl = Helper.RequestEncrypted(this, "c");
			if (!CheckUserLogin(loadedControl))
			{
				Response.Write(Language.GetString("SessionExpiredNeedToLoginAgain"));
				return;
			}

			if (Helper.Request(this, "method") == "GetDataToExport")
			{
				GetDataToExport();
			}
		}

		private static bool CheckUserLogin(string loadedControl)
		{
			if (loadedControl != ConfigurationManager.DefaultLoginPageControlID &&
					(ConfigurationManager.CheckLoginExceptions == string.Empty || ("," + ConfigurationManager.CheckLoginExceptions + ",").IndexOf("," + loadedControl + ",") == -1) &&
					Helper.GetGuid(Session["UserGuid"]) == Guid.Empty)
			{
				return false;
			}
			return true;
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static DataGridResult GetDataInfo(string serializeObject, string c, string _search, string nd, int rows, int page, string sidx, string sord)
		{
			int pageControl = GetPageAssemblyValue(c);
			if (!CheckUserLogin(Helper.GetString(pageControl)))
			{
				Session["SessionExpired"] = true;
				throw new Exception("[{(SessionExpired)}]");
			}

			DataGrid dataGrid = ((DataGridSchema)SerializationTools.DeserializeXml(Helper.Decrypt(serializeObject, HttpContext.Current.Session), typeof(DataGridSchema))).GetDataGrid();

			string searchFiletrs = string.Empty;
			string toolbarFilters = string.Empty;

			if (_search == "True")
			{
				string searchOptions = "[" + Helper.GetStreamContent(System.Web.HttpContext.Current.Request.InputStream) + "]";
				JArray array = JArray.Parse(searchOptions);
				foreach (JObject content in array.Children<JObject>())
				{
					foreach (JProperty prop in content.Properties())
					{
						if (prop.Name == "filters")
						{
							toolbarFilters = JObject.Parse(prop.Value.ToString())["rules"].ToString();
						}
						if (prop.Name == "SearchFiletrs")
						{
							searchFiletrs = prop.Value.ToString();
						}
					}
				}
			}

			Assembly currentAssembly = Assembly.GetExecutingAssembly();
			string pageName = string.Format("{0}.{1}", currentAssembly.GetName().Name, ((Business.UserControls)pageControl).ToString().Replace('_', '.'));
			Delegate dataBinderHandler = ((UIUserControlBase)Activator.CreateInstance(currentAssembly.GetType(pageName))).GetDataBinder(dataGrid.ID);
			Delegate dataRenderEvent = ((UIUserControlBase)Activator.CreateInstance(currentAssembly.GetType(pageName))).GetDataRenderHandler(dataGrid.ID);

			return dataGrid.DataBind(sidx, sord, page, rows, searchFiletrs, toolbarFilters, dataBinderHandler, dataRenderEvent);
		}

		private static int GetPageAssemblyValue(string pageUrl)
		{
			string queryString = System.Web.HttpContext.Current.Request.UrlReferrer.Query;

			if (string.IsNullOrEmpty(queryString))
				queryString = pageUrl.Substring(pageUrl.IndexOf('?'));

			queryString = queryString.Substring(queryString.IndexOf("c=") + 2);
			int nextParamIndex = queryString.IndexOf('&');
			if (nextParamIndex != -1)
				queryString = queryString.Substring(0, nextParamIndex);

			return Helper.DecryptInt(queryString, Session);
		}

		public void GetDataToExport()
		{
			try
			{
				string dataGridID = Helper.Request(this, "DataGridID");
				string sortField = Helper.Request(this, "SortField");
				string sortOrder = Helper.Request(this, "SortOrder");
				string exportType = Helper.Request(this, "ExportType");
				string searchFiletrs = Helper.Request(this, "SearchFiletrs");
				string serializeObject = Helper.Request(this, "SerializeObject");
				string toolbarFilters = Helper.Request(this, "ToolbarFilters");

				GeneralTools.DataGrid.DataGrid dataGrid = ((DataGridSchema)SerializationTools.DeserializeXml(Helper.Decrypt(serializeObject, HttpContext.Current.Session), typeof(DataGridSchema))).GetDataGrid();
				int pageControl = GetPageAssemblyValue(Request.Url.ToString());

				Assembly currentAssembly = Assembly.GetExecutingAssembly();
				string pageName = string.Format("{0}.{1}", currentAssembly.GetName().Name, ((Business.UserControls)pageControl).ToString().Replace('_', '.'));
				Delegate dataBinderHandler = ((UIUserControlBase)Activator.CreateInstance(currentAssembly.GetType(pageName))).GetDataBinder(dataGrid.ID);
				Delegate dataRenderEvent = ((UIUserControlBase)Activator.CreateInstance(currentAssembly.GetType(pageName))).GetDataRenderHandler(dataGrid.ID);

				string fileName = string.Empty;
				switch (exportType.ToLower())
				{
					case "pdf":
						fileName = dataGrid.ExportToPdfFile(sortField, sortOrder, searchFiletrs, toolbarFilters, dataBinderHandler, dataRenderEvent);
						byte[] data = File.ReadAllBytes(fileName);
						Response.BinaryWrite(data);
						Response.ContentType = "application/pdf";
						Response.AppendHeader("content-disposition", "attachment;filename=export.pdf");
						Response.End();
						break;

					case "excel":
						Response.Clear();
						Response.AddHeader("content-disposition", "attachment;filename=Test.xls");
						Response.ContentType = "application/ms-excel";
						Response.ContentEncoding = System.Text.Encoding.Unicode;
						Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

						System.IO.StringWriter stringWriter = new System.IO.StringWriter();
						string style = @"<style> .textmode { mso-number-format:\@;} </style>";
						Response.Write(style);
						stringWriter.Write(dataGrid.ExportToExcel(sortField, sortOrder, searchFiletrs, toolbarFilters, dataBinderHandler, dataRenderEvent));

						System.Web.UI.HtmlTextWriter htmlTextWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
						this.RenderControl(htmlTextWriter);

						Response.Write(stringWriter.ToString());
						Response.End();
						break;
				}
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
				Response.End();
			}
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
		public static List<GeneralTools.TreeView.TreeNode> GetTreeNode(string cID, string c, string id, string srch)
		{
			int pageControl = Helper.DecryptInt(c, Session);

			string searchFiletrs = string.Empty;

			Assembly currentAssembly = Assembly.GetExecutingAssembly();
			string pageName = string.Format("{0}.{1}", currentAssembly.GetName().Name, ((Business.UserControls)pageControl).ToString().Replace('_', '.'));

			Delegate dataBinder = ((UIUserControlBase)Activator.CreateInstance(currentAssembly.GetType(pageName))).GetDataBinder(cID);

			return ((GeneralTools.TreeView.DataBindHandler)dataBinder)(id, srch);
		}

		public class ColumnSearchSelect
		{
			public string Text;
			public string Value;
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static List<ColumnSearchSelect> GetColumnSearchData(int id)
		{
			List<ColumnSearchSelect> columnSearchSelect = new List<ColumnSearchSelect>();

			switch (id)
			{
				case (int)ColumnSearchTypeSelect.TransactionTypeCreditChange:
					foreach (TypeCreditChanges typeCreditTransaction in Enum.GetValues(typeof(TypeCreditChanges)))
						columnSearchSelect.Add(new ColumnSearchSelect { Text = Language.GetString(typeCreditTransaction.ToString()), Value = ((int)typeCreditTransaction).ToString() });// drpAdvancedSearchTypeCreditChange.Items.Add(new ListItem(Language.GetString(typeCreditTransaction.ToString()), ((int)typeCreditTransaction).ToString()));
					break;
				case (int)ColumnSearchTypeSelect.TransactionType:
					foreach (TypeTransactions typeTransaction in Enum.GetValues(typeof(TypeTransactions)))
						columnSearchSelect.Add(new ColumnSearchSelect { Text = Language.GetString(typeTransaction.ToString()), Value = ((int)typeTransaction).ToString() });
					break;
				case (int)ColumnSearchTypeSelect.FishStatus:
					foreach (FishStatus status in Enum.GetValues(typeof(FishStatus)))
						columnSearchSelect.Add(new ColumnSearchSelect { Text = Language.GetString(status.ToString()), Value = ((int)status).ToString() });
					break;
				case (int)ColumnSearchTypeSelect.SmsSendType:
					foreach (Common.SmsSendType type in Enum.GetValues(typeof(Common.SmsSendType)))
						columnSearchSelect.Add(new ColumnSearchSelect { Text = Language.GetString(type.ToString()), Value = ((int)type).ToString() });
					break;
				case (int)ColumnSearchTypeSelect.ServiceGroup:
					int rowCount = 0;
					DataTable dtGroups = Facade.ServiceGroup.GetPagedServiceGroup(string.Empty, "CreateDate", 0, 0, ref rowCount);
					foreach (DataRow row in dtGroups.Rows)
						columnSearchSelect.Add(new ColumnSearchSelect { Text = row["Title"].ToString(), Value = row["Guid"].ToString() });
					break;
				case (int)ColumnSearchTypeSelect.DeliveryStatus:
					//foreach (Common.DeliveryStatus status in System.Enum.GetValues(typeof(Common.DeliveryStatus)))
					columnSearchSelect.Add(new ColumnSearchSelect { Text = Language.GetString(Common.DeliveryStatus.SentAndReceivedbyPhone.ToString()), Value = ((int)Common.DeliveryStatus.SentAndReceivedbyPhone).ToString() });
					columnSearchSelect.Add(new ColumnSearchSelect { Text = Language.GetString(Common.DeliveryStatus.HaveNotReceivedToPhone.ToString()), Value = ((int)Common.DeliveryStatus.HaveNotReceivedToPhone).ToString() });
					columnSearchSelect.Add(new ColumnSearchSelect { Text = Language.GetString(Common.DeliveryStatus.SentToItc.ToString()), Value = ((int)Common.DeliveryStatus.SentToItc).ToString() });
					columnSearchSelect.Add(new ColumnSearchSelect { Text = Language.GetString(Common.DeliveryStatus.ReceivedByItc.ToString()), Value = ((int)Common.DeliveryStatus.ReceivedByItc).ToString() });
					columnSearchSelect.Add(new ColumnSearchSelect { Text = Language.GetString(Common.DeliveryStatus.DidNotReceiveToItc.ToString()), Value = ((int)Common.DeliveryStatus.DidNotReceiveToItc).ToString() });
					columnSearchSelect.Add(new ColumnSearchSelect { Text = Language.GetString(Common.DeliveryStatus.NotSent.ToString()), Value = ((int)Common.DeliveryStatus.NotSent).ToString() });
					columnSearchSelect.Add(new ColumnSearchSelect { Text = Language.GetString(Common.DeliveryStatus.BlackList.ToString()), Value = ((int)Common.DeliveryStatus.BlackList).ToString() });
					columnSearchSelect.Add(new ColumnSearchSelect { Text = Language.GetString(Common.DeliveryStatus.BlackListTable.ToString()), Value = ((int)Common.DeliveryStatus.BlackListTable).ToString() });
					break;
				case (int)ColumnSearchTypeSelect.Gender:
					foreach (Gender gender in Enum.GetValues(typeof(Gender)))
						columnSearchSelect.Add(new ColumnSearchSelect { Text = Language.GetString(gender.ToString()), Value = ((int)gender).ToString() });
					break;
				case (int)ColumnSearchTypeSelect.SendStatus:
					foreach (Common.SendStatus status in Enum.GetValues(typeof(Common.SendStatus)))
						columnSearchSelect.Add(new ColumnSearchSelect { Text = Language.GetString(status.ToString()), Value = ((int)status).ToString() });
					break;
			}
			return columnSearchSelect;
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static List<object> GetParserOption(string guid)
		{
			DataTable dtoptions = Facade.ParserFormula.GetParserFormulas(Helper.GetGuid(guid));
			List<object> lstOptions = new List<object>();

			foreach (DataRow row in dtoptions.Rows)
			{
				var obj = new { key = row["Key"], guid = row["Guid"] };
				lstOptions.Add(obj);
			}

			return lstOptions;
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static List<object> GetUserDomains(string userGuid)
		{
			DataTable dtDomains= Facade.Domain.GetAgentChildrenDomains(userGuid);
			List<object> lstDomains = new List<object>();

			foreach (DataRow row in dtDomains.Rows)
			{
				var obj = new { name = row["Name"], guid = row["Guid"] };
				lstDomains.Add(obj);
			}

			return lstDomains;
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public static string GetParserSmsReport(string guid)
		{
			DataTable dtReport = Facade.Inbox.GetParserSmsReport(Helper.GetGuid(guid));
			StringBuilder series = new StringBuilder();

			foreach (DataRow row in dtReport.Rows)
			{
				series.Append(",");
				series.Append("{\"name\":\"" + row["Key"] + "\"");
				series.Append(",\"data\":[" + row["Count"] + "]}");
			}

			return string.Format("[{0}]", series.ToString().TrimStart(','));
		}
	}
}
