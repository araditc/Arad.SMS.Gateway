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
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Business
{
	public class Data : BusinessEntityBase
	{
		public Data(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Datas.ToString(), dataAccessProvider) { }

		public DataTable GetUserData(Guid userGuid)
		{
			return base.FetchSPDataTable("GetUserData", "@UserGuid", userGuid);
		}

		public DataTable GetPagedData(Guid userGuid, Guid dataCenterGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetNews = base.FetchSPDataSet("GetPagedData",
																								"@UserGuid", userGuid,
																								"@DataCenterGuid", dataCenterGuid,
																								"@PageNo", pageNo,
																								"@PageSize", pageSize,
																								"@SortField", sortField);
			resultCount = Helper.GetInt(dataSetNews.Tables[0].Rows[0]["RowCount"]);

			return dataSetNews.Tables[1];
		}

		public bool UpdateData(Common.Data data)
		{
			return base.ExecuteSPCommand("UpdateData",
																	 "@Guid", data.DataGuid,
																	 "@Title", data.Title,
																	 "@Priority", data.Priority,
																	 "@Summary", data.Summary,
																	 "@Content", data.Content,
																	 "@Keywords", data.Keywords,
																	 "@FromDate", data.FromDate,
																	 "@ToDate", data.ToDate,
																	 "@ParentGuid", data.ParentGuid,
																	 "@DataCenterGuid", data.DataCenterGuid);
		}

		//public DataTable GetMenusOfDataCenter(Guid DataCenterGuid)
		//{
		//	return base.FetchSPDataTable("GetMenusOfDataCenter", "@DataCenterGuid", DataCenterGuid);
		//}

		public Guid InsertData(Common.Data data)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				base.ExecuteSPCommand("InsertData",
															"@Guid", guid,
															"@Title", data.Title,
															"@Priority", data.Priority,
															"@Summary", data.Summary,
															"@Content", data.Content,
															"@Keywords", data.Keywords,
															"@FromDate", data.FromDate,
															"@ToDate", data.ToDate,
															"@CreateDate", data.CreateDate,
															"@ParentGuid", data.ParentGuid,
															"@DataCenterGuid", data.DataCenterGuid);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}

		//public DataTable GetDataOfDataCenter(Guid userGuid, Guid dataCenterGuid, DataCenterType dataCenterType)
		//{
		//	return base.FetchSPDataTable("GetDataOfDataCenter", "@UserGuid", userGuid,
		//																											"@DataCenterGuid", dataCenterGuid,
		//																											"@DataCenterType", (int)dataCenterType);
		//}

		//public Guid InsertMenu(Common.Data data)
		//{
		//	Guid guid = Guid.NewGuid();
		//	try
		//	{
		//		base.ExecuteSPCommand("InsertMenu", "@Guid", guid,
		//																				"@Title", data.Title,
		//																				"@Priority", data.Priority,
		//																				"@ParentGuid", data.ParentGuid,
		//																				"@Content", data.Content,
		//																				"@Keywords", data.Keywords,
		//																				"@CreateDate", data.CreateDate,
		//																				"@IsActive", data.IsActive,
		//																				"@ShowInHomePage", data.ShowInHomePage,
		//																				"@DataCenterGuid", data.DataCenterGuid);
		//		return guid;
		//	}
		//	catch
		//	{
		//		guid = Guid.Empty;
		//		return guid;
		//	}
		//}

		//public DataTable GetPagedMenu(Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		//{
		//	DataSet dataSetNews = base.FetchSPDataSet("GetPagedMenu", "@UserGuid", userGuid,
		//																														"@PageNo", pageNo,
		//																														"@PageSize", pageSize,
		//																														"@SortField", sortField);
		//	resultCount = Helper.GetInt(dataSetNews.Tables[0].Rows[0]["RowCount"]);

		//	return dataSetNews.Tables[1];
		//}

		//public bool UpdateMenu(Common.Data data)
		//{
		//	return base.ExecuteSPCommand("UpdateMenu", "@Guid", data.DataGuid,
		//																							"@Title", data.Title,
		//																							"@Priority", data.Priority,
		//																							"@ParentGuid", data.ParentGuid,
		//																							"@Content", data.Content,
		//																							"@Keywords", data.Keywords,
		//																							"@IsActive", data.IsActive,
		//																							"@ShowInHomePage", data.ShowInHomePage,
		//																							"@DataCenterGuid", data.DataCenterGuid);
		//}

		//public DataTable GetNews(Guid domainGuid, DataCenterType dataCenters)
		//{
		//	return FetchSPDataTable("GetNews", "@DomainGuid", domainGuid, "@DataCenterType", (int)dataCenters);
		//}

		//public DataTable Load(int dataID)
		//{
		//	return base.FetchDataTable("SELECT * FROM [Datas] WHERE [ID] = @ID", "@ID", dataID);
		//}

		public bool ActiveData(Guid dataGuid, bool isActive)
		{
			return ExecuteCommand("UPDATE [dbo].[Datas] SET [IsActive] = @IsActive WHERE [Guid] = @Guid", "@Guid", dataGuid, "@IsActive", isActive);
		}

		public DataTable GetData(int dataID)
		{
			return FetchDataTable("SELECT * FROM [Datas] WHERE [ID] = @ID", "@ID", dataID);
		}
	}
}
