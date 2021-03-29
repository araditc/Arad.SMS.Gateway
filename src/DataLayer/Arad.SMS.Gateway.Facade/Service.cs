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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Arad.SMS.Gateway.Facade
{
	public class Service : FacadeEntityBase
	{
		public static DataTable GetPagedService(string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.Service serviceController = new Business.Service();
			return serviceController.GetPagedService(query, sortField, pageNo, pageSize, ref resultCount);
		}

		public static DataTable GetServiceOfUserForDeterminePrice(Guid userGuid, Guid parentGuid)
		{
			Business.Service serviceController = new Business.Service();
			return serviceController.GetServiceOfUserForDeterminePrice(userGuid, parentGuid);
		}

		public static bool Insert(Common.Service service)
		{
			Business.Service serviceController = new Business.Service();
			return serviceController.Insert(service) != Guid.Empty ? true : false;
		}

		public static Common.Service LoadService(Guid serviceGuid)
		{
			Business.Service serviceController = new Business.Service();
			Common.Service service = new Common.Service();
			serviceController.Load(serviceGuid, service);
			return service;
		}

		public static bool UpdateService(Common.Service service)
		{
			Business.Service serviceController = new Business.Service();
			return serviceController.UpdateService(service);
		}

		public static bool Delete(Guid serviceGuid)
		{
			Business.Service serviceController = new Business.Service();
			return serviceController.Delete(serviceGuid);
		}

		public static DataTable GetServiceOfUserForDefineGlobalServicePrice(Guid userGuid)
		{
			Business.Service serviceController = new Business.Service();
			return serviceController.GetServiceOfUserForDefineGlobalServicePrice(userGuid);
		}

		private static List<Common.Menu> userMenus = new List<Common.Menu>();
		public static List<Menu> GenerateUserServiceMenu(Guid userGuid, string lang)
		{
			userMenus.Clear();

			DataTable parentServiceGroup = Facade.ServiceGroup.GetParentGroups();
			List<DataRow> serviceList = Facade.ServiceGroup.GetUserGroupsWithServices(userGuid).AsEnumerable().ToList();
			int subMenuCount = 0;
			bool parentHasService = false;

			try
			{
				foreach (DataRow row in parentServiceGroup.Rows)
				{
					subMenuCount = serviceList.Where(service => Helper.GetGuid(service["ParentGuid"]) == Helper.GetGuid(row["GroupGuid"])).Count();
					parentHasService = serviceList.Where(service => Helper.GetGuid(service["ParentGuid"]) == Helper.GetGuid(row["GroupGuid"]) ||
																												Helper.GetGuid(service["GroupGuid"]) == Helper.GetGuid(row["GroupGuid"])).Any();

					if (parentHasService)
					{
                        if (lang == "fa")
                        {
                            var menu = new Common.Menu
                            {
                                Title = row["GroupTitleFa"].ToString(),
                                SmallIcon = row["GroupIcon"].ToString(),
                                LargeIcon = row["GroupLargeIcon"].ToString(),
                                Order = Helper.GetInt(row["Order"]),
                                Path = string.Empty,
                                ActiveLink = false,
                                SubMenuCount = subMenuCount,

                                Children = new List<Common.Menu>(),
                            };
                            menu.Children = GenerateSubMenu(serviceList, Helper.GetGuid(row["GroupGuid"]), false, lang);
                            userMenus.Add(menu);
                        }
                        else
                        {
                            var menu = new Common.Menu
                            {
                                Title = row["GroupTitle"].ToString(),
                                SmallIcon = row["GroupIcon"].ToString(),
                                LargeIcon = row["GroupLargeIcon"].ToString(),
                                Order = Helper.GetInt(row["Order"]),
                                Path = string.Empty,
                                ActiveLink = false,
                                SubMenuCount = subMenuCount,

                                Children = new List<Common.Menu>(),
                            };
                            menu.Children = GenerateSubMenu(serviceList, Helper.GetGuid(row["GroupGuid"]), false, lang);
                            userMenus.Add(menu);
                        }
					}
				}
				return userMenus;
			}
			catch
			{
				return userMenus;
			}
		}

		private static List<Common.Menu> GenerateSubMenu(List<DataRow> serviceList, Guid parentGuid, bool isHelp, string lang)
		{
			List<Common.Menu> subMenu = new List<Common.Menu>();
			List<string> groups = new List<string>();
			int subMenuCount = 0;
			List<DataRow> childrenlist = serviceList.Where(service => Helper.GetGuid(service["ServiceGroupGuid"]) == parentGuid ||
																																Helper.GetGuid(service["ParentGuid"]) == parentGuid).ToList();

			foreach (DataRow service in childrenlist)
			{
				if (Helper.GetGuid(service["ServiceGroupGuid"]) == parentGuid)
				{
					groups.Add(Helper.GetString(service["GroupGuid"]));
                    if (lang == "fa")
                    {
                        var menu1 = new Common.Menu
                        {
                            Title = service["ServiceTitleFa"].ToString(),
                            SmallIcon = service["IconAddress"].ToString(),
                            Order = Helper.GetInt(service["ServiceOrder"]),
                            Path = "/PageLoader.aspx?c=" + Helper.Encrypt(service["ReferencePageKey"], HttpContext.Current.Session),
                            ActiveLink = true,
                            SubMenuCount = subMenuCount,

                            Children = new List<Common.Menu>(),
                        };

                        subMenu.Add(menu1);
                    }
                    else
                    {
                        var menu1 = new Common.Menu
                        {
                            Title = service["ServiceTitle"].ToString(),
                            SmallIcon = service["IconAddress"].ToString(),
                            Order = Helper.GetInt(service["ServiceOrder"]),
                            Path = "/PageLoader.aspx?c=" + Helper.Encrypt(service["ReferencePageKey"], HttpContext.Current.Session),
                            ActiveLink = true,
                            SubMenuCount = subMenuCount,

                            Children = new List<Common.Menu>(),
                        };

                        subMenu.Add(menu1);
                    }
				}

				if (Helper.GetGuid(service["ParentGuid"]) == parentGuid &&
						!groups.Contains(service["GroupGuid"].ToString()))
				{
					groups.Add(service["GroupGuid"].ToString());

					subMenuCount = serviceList.Where(serviceInfo => Helper.GetGuid(serviceInfo["ParentGuid"]) == Helper.GetGuid(service["GroupGuid"])).Count();
                    if (lang == "fa")
                    {
                        var menu = new Common.Menu
                        {
                            Title = service["GroupTitleFa"].ToString(),
                            SmallIcon = service["GroupIcon"].ToString(),
                            LargeIcon = service["GroupLargeIcon"].ToString(),
                            Order = Helper.GetInt(service["GroupOrder"]),
                            Path = string.Empty,
                            ActiveLink = false,
                            SubMenuCount = subMenuCount,

                            Children = new List<Common.Menu>(),
                        };

                        menu.Children = GenerateSubMenu(serviceList, Helper.GetGuid(service["GroupGuid"]), isHelp, lang);
                        subMenu.Add(menu);
                    }
                    else
                    {
                        var menu = new Common.Menu
                        {
                            Title = service["GroupTitle"].ToString(),
                            SmallIcon = service["GroupIcon"].ToString(),
                            LargeIcon = service["GroupLargeIcon"].ToString(),
                            Order = Helper.GetInt(service["GroupOrder"]),
                            Path = string.Empty,
                            ActiveLink = false,
                            SubMenuCount = subMenuCount,

                            Children = new List<Common.Menu>(),
                        };

                        menu.Children = GenerateSubMenu(serviceList, Helper.GetGuid(service["GroupGuid"]), isHelp, lang);
                        subMenu.Add(menu);
                    }
				}
			}
			return subMenu;
		}
	}
}
