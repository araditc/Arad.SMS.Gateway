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
using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Facade
{
	public class Data : FacadeEntityBase
	{
		public static DataTable GetUserData(Guid userGuid)
		{
			Business.Data dataController = new Business.Data();
			return dataController.GetUserData(userGuid);
		}

		public static bool InsertData(Common.Data data)
		{
			Business.Data dataController = new Business.Data();
			return dataController.InsertData(data) != Guid.Empty ? true : false;
		}

		public static Common.Data LoadData(Guid dataGuid)
		{
			Business.Data dataController = new Business.Data();
			Common.Data data = new Common.Data();
			dataController.Load(dataGuid, data);
			return data;
		}

		public static DataTable GetData(int dataID)
		{
			Business.Data dataController = new Business.Data();
			return dataController.GetData(dataID);
		}

		public static bool Delete(Guid imageGuid)
		{
			Business.Data dataController = new Business.Data();
			return dataController.Delete(imageGuid);
		}

		public static bool UpdateData(Common.Data data)
		{
			Business.Data dataController = new Business.Data();
			return dataController.UpdateData(data);
		}

		public static DataTable GetPagedData(Guid userGuid, Guid dataCenterGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.Data dataController = new Business.Data();
			return dataController.GetPagedData(userGuid, dataCenterGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		//public static DataTable GetMenusOfDataCenter(Guid dataCenterGuid)
		//{
		//	Business.Data dataController = new Business.Data();
		//	return dataController.GetMenusOfDataCenter(dataCenterGuid);
		//}

		//public static DataTable GetDataOfDataCenter(Guid userGuid, Guid dataCenterGuid, DataCenterType dataCenterType)
		//{
		//	Business.Data dataController = new Business.Data();
		//	return dataController.GetDataOfDataCenter(userGuid, dataCenterGuid, dataCenterType);
		//}

		//public static bool InsertMenu(Common.Data data)
		//{
		//	Business.Data dataController = new Business.Data();
		//	return dataController.InsertMenu(data) != Guid.Empty ? true : false;
		//}

		//public static DataTable GetPagedMenu(Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		//{
		//	Business.Data dataController = new Business.Data();
		//	return dataController.GetPagedMenu(userGuid, sortField, pageNo, pageSize, ref resultCount);
		//}

		//public static bool UpdateMenu(Common.Data data)
		//{
		//	Business.Data dataController = new Business.Data();
		//	return dataController.UpdateMenu(data);
		//}

		//public static DataTable GetNews(Guid domainGuid, DataCenterType dataCenters)
		//{
		//	Business.Data dataController = new Business.Data();
		//	return dataController.GetNews(domainGuid, dataCenters);
		//}

		public static bool ActiveData(Guid dataGuid)
		{
			Business.Data dataController = new Business.Data();
			Common.Data data = new Common.Data();
			try
			{
				if (!dataController.Load(dataGuid, data))
					throw new Exception();

				bool isActive = data.IsActive ? false : true;
				return dataController.ActiveData(dataGuid, isActive);
			}
			catch
			{
				return false;
			}
		}
	}
}
