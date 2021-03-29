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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;
using Arad.SMS.Gateway.Business;
using System.Collections.Generic;
using System.Linq;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.Facade
{
	public class DataCenter : FacadeEntityBase
	{
		public static DataTable GetUserDataCenter(Guid userGuid, DataCenterType dataCenterType)
		{
			Business.DataCenter dataCenterController = new Business.DataCenter();
			return dataCenterController.GetUserDataCenter(userGuid, dataCenterType);
		}

		private static List<Common.Menu> domainMenu = new List<Common.Menu>();
		public static List<Common.Menu> GetDomainMenu(Guid domainGuid, DataLocation location, Desktop desktop)
		{
			domainMenu.Clear();
			int subMenuCount = 0;

			try
			{
				Business.DataCenter dataCenterController = new Business.DataCenter();
				List<DataRow> lstItems = dataCenterController.GetDomainMenu(domainGuid, location, desktop).AsEnumerable().ToList();
				subMenuCount = lstItems.Where(item => Helper.GetGuid(item["ParentGuid"]) == Helper.GetGuid(item["Guid"])).Count();

				foreach (DataRow row in lstItems.Where(item => Helper.GetGuid(item["ParentGuid"]) == Guid.Empty))
				{
					var menu = new Common.Menu
					{
						ID = Helper.GetInt(row["ID"]),
						Title = row["Title"].ToString(),
						Order = Helper.GetInt(row["Priority"]),
						SubMenuCount = subMenuCount,

						Children = new List<Common.Menu>(),
					};

					menu.Children = GetSubMenu(lstItems, Helper.GetGuid(row["Guid"]));
					domainMenu.Add(menu);
				}
				return domainMenu;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private static List<Common.Menu> GetSubMenu(List<DataRow> lstItems, Guid parentGuid)
		{
			List<Common.Menu> subMenu = new List<Common.Menu>();
			List<DataRow> childrenlist = lstItems.Where(item => Helper.GetGuid(item["ParentGuid"]) == parentGuid).ToList();
			try
			{
				foreach (DataRow row in childrenlist)
				{
					var menu = new Common.Menu
					{
						ID = Helper.GetInt(row["ID"]),
						Title = row["Title"].ToString(),
						Order = Helper.GetInt(row["Priority"]),
						SubMenuCount = lstItems.Where(item => Helper.GetGuid(item["ParentGuid"]) == Helper.GetGuid(row["Guid"])).Count(),

						Children = new List<Common.Menu>(),
					};

					menu.Children = GetSubMenu(lstItems, Helper.GetGuid(row["Guid"]));
					subMenu.Add(menu);
				}
				return subMenu;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool Insert(Common.DataCenter dataCenter)
		{
			Business.DataCenter dataCenterController = new Business.DataCenter();
			return dataCenterController.Insert(dataCenter) != Guid.Empty ? true : false;
		}

		public static Common.DataCenter LoadDataCenter(Guid dataCenterGuid)
		{
			Business.DataCenter dataCenterController = new Business.DataCenter();
			Common.DataCenter dataCenter = new Common.DataCenter();
			dataCenterController.Load(dataCenterGuid, dataCenter);
			return dataCenter;
		}

		public static bool Delete(Guid dataCenterGuid)
		{
			Business.DataCenter dataCenterController = new Business.DataCenter();
			return dataCenterController.Delete(dataCenterGuid);
		}

		public static bool Update(Common.DataCenter dataCenter)
		{
			Business.DataCenter dataCenterController = new Business.DataCenter();
			return dataCenterController.UpdateDataCenter(dataCenter);
		}

		public static bool UpdateLocation(Common.DataCenter dataCenter)
		{
			Business.DataCenter dataCenterController = new Business.DataCenter();
			return dataCenterController.UpdateLocation(dataCenter);
		}
	}
}
