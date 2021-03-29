using System;
using GeneralLibrary.BaseCore;
using System.Data;
using GeneralLibrary;
using System.Text;
using System.Collections.Generic;

namespace Facade
{
	public class DomainMenu : FacadeEntityBase
	{
		public static bool Insert(Common.DomainMenu domainMenu)
		{
			Business.DomainMenu domainMenuController = new Business.DomainMenu();
			return domainMenuController.Insert(domainMenu) != Guid.Empty ? true : false;
		}

		public static bool UpdateDomainMenu(Common.DomainMenu domainMenu)
		{
			Business.DomainMenu domainMenuController = new Business.DomainMenu();
			return domainMenuController.UpdateDomainMenu(domainMenu);
		}

		public static Common.DomainMenu LoadDomainMenu(Guid domainGuid)
		{
			Business.DomainMenu domainMenuController = new Business.DomainMenu();
			Common.DomainMenu domainMenu = new Common.DomainMenu();
			domainMenuController.Load(domainGuid, domainMenu);
			return domainMenu;
		}

		public static bool Delete(Guid domainMenuGuid)
		{
			Business.DomainMenu domainMenuController = new Business.DomainMenu();
			return domainMenuController.Delete(domainMenuGuid);
		}

		public static DataTable GetPagedDomainMenu(Common.DomainMenu domainMenu, int activeStatus, int typeStatus, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.DomainMenu domainMenuController = new Business.DomainMenu();
			return domainMenuController.GetPagedDomainMenu(domainMenu, activeStatus, typeStatus, sortField, pageNo, pageSize, ref resultCount);
		}

		public static DataTable GetDomainMenus(Guid domainGuid)
		{
			Business.DomainMenu domainMenuController = new Business.DomainMenu();
			return domainMenuController.GetDomainMenus(domainGuid);
		}

		public static string GetMenusOfDesktop(string domainName, Business.Desktop desktop, Business.DomainMenuLocation menuLocation)
		{
			try
			{
				Business.DomainMenu domainMenuController = new Business.DomainMenu();
				string target;
				StringBuilder menus = new StringBuilder();
				DataTable domainInfo = Facade.Domain.GetDomainInfo(domainName);
				DataTable dataTableMenus = domainMenuController.GetMenusOfDesktop(Helper.GetGuid(domainInfo.Rows[0]["Guid"]), desktop);

				foreach (DataRow dataRowMenu in dataTableMenus.Rows)
				{
					Business.DomainMenuType menuType = (Business.DomainMenuType)Helper.GetInt(dataRowMenu["Type"]);
					Business.DomainNameTargetType targetType = (Business.DomainNameTargetType)(Helper.GetInt(dataRowMenu["TargetType"]));
					target = string.Empty;

					switch (targetType)
					{
						case Business.DomainNameTargetType.SelfPage:
							target = "_self";
							break;
						case Business.DomainNameTargetType.BlankPage:
							target = "_blank";
							break;
					}

					Business.DomainMenuLocation location = (Business.DomainMenuLocation)Helper.GetInt(dataRowMenu["Location"]);
					if (location == menuLocation)
					{
						switch (menuType)
						{
							case Business.DomainMenuType.ExternalUrl:
								menus.Append(string.Format(@"<li><a target='{0}' href='{1}'>{2}</a></li>"
																						, target
																						, Helper.GetString(dataRowMenu["Link"])
																						, Helper.GetString(dataRowMenu["Title"])));
								break;
							case Business.DomainMenuType.DataCenter:
								DataTable dataTableDataMenu = Facade.Data.GetMenusOfDataCenter(Helper.GetGuid(dataRowMenu["DataCenterGuid"]));
								menus.Append(GenerateMenu(dataTableDataMenu, (Business.DefaultPages)Helper.GetInt(domainInfo.Rows[0]["DefaultPage"])));
								break;
						}
					}
				}

				return menus.ToString();
			}
			catch { return string.Empty; }
		}

		private static string GenerateMenu(DataTable dataTableDataMenu, Business.DefaultPages defaultPage)
		{
			StringBuilder menu = new StringBuilder();
			DataView dataViewMenu = dataTableDataMenu.DefaultView;
			dataViewMenu.RowFilter = string.Format("ParentGuid='{0}'", Guid.Empty);
			DataTable dataTableParent = dataViewMenu.ToTable();
			string href;

			foreach (DataRow row in dataTableParent.Rows)
			{
				href = Helper.GetString(row["Content"]).Length > 0 ?
						string.Format("/{0}{1}/{2}", (int)Business.ContentType.Menu, row["ID"].ToString(), row["Title"]) : "#";
				menu.Append(string.Format("<li><a href='{0}'>{1}</a>", href, row["Title"]));
				menu.Append(GenerateSubMenu(dataTableDataMenu, Helper.GetGuid(row["Guid"]), defaultPage));
				menu.Append("</li>");
			}

			return menu.ToString();
		}

		private static string GenerateSubMenu(DataTable dataTableDataMenu, Guid root, Business.DefaultPages defaultPage)
		{
			DataView dataViewMenu = dataTableDataMenu.DefaultView;
			dataViewMenu.RowFilter = string.Format("ParentGuid='{0}'", root);
			DataTable dataTableChildren = dataViewMenu.ToTable();
			StringBuilder subMenu = new StringBuilder();
			string href;

			if (dataTableChildren.Rows.Count > 0)
				subMenu.Append("<ul>");

			foreach (DataRow row in dataTableChildren.Rows)
			{
				href = Helper.GetString(row["Content"]).Length > 0 ?
						string.Format("/{0}{1}/{2}", (int)Business.ContentType.Menu, row["ID"], row["Title"]) :
						"#";

				subMenu.Append(string.Format("<li><a href='{0}'>{1}</a>", href, row["Title"]));
				subMenu.Append(GenerateSubMenu(dataTableDataMenu, Helper.GetGuid(row["Guid"]), defaultPage));
				subMenu.Append("</li>");
			}

			if (dataTableChildren.Rows.Count > 0)
				subMenu.Append("</ul>");

			return subMenu.ToString();
		}

		public static DataTable GetDatasOfHomePage(string domainName)
		{
			Business.DomainMenu domainMenuController = new Business.DomainMenu();
			DataTable domainInfo = Facade.Domain.GetDomainInfo(domainName);
			return domainMenuController.GetDatasOfHomePage(Helper.GetGuid(domainInfo.Rows[0]["Guid"]));
		}

		private static List<Common.Menu> lstMenu = new List<Common.Menu>();
		public static List<Common.Menu> GetMenusOfDesktop(string domainName, Business.Desktop desktop)
		{
			try
			{
				lstMenu.Clear();
				Business.DomainMenu domainMenuController = new Business.DomainMenu();
				Business.DomainMenuType menuType;

				DataTable domainInfo = Facade.Domain.GetDomainInfo(domainName);
				DataTable dataTableMenus = domainMenuController.GetMenusOfDesktop(Helper.GetGuid(domainInfo.Rows[0]["Guid"]), desktop);

				foreach (DataRow row in dataTableMenus.Rows)
				{
					var menu = new Common.Menu();

					menuType = (Business.DomainMenuType)Helper.GetInt(row["Type"]);
					switch (menuType)
					{
						case Business.DomainMenuType.ExternalUrl:
							menu.Type = (int)menuType;
							menu.Path = row["Link"].ToString();
							menu.Title = row["Title"].ToString();
							menu.Target = Helper.GetInt(row["TargetType"]) == (int)Business.DomainNameTargetType.BlankPage ? "_blank" : "_self";
							menu.Children = new List<Common.Menu>();
							menu.Location = Helper.GetInt(row["Location"]);
							lstMenu.Add(menu);
							break;

						case Business.DomainMenuType.DataCenter:
							DataTable dataTableDataMenu = Facade.Data.GetMenusOfDataCenter(Helper.GetGuid(row["DataCenterGuid"]));
							DataView dataViewMenu = dataTableDataMenu.DefaultView;
							dataViewMenu.RowFilter = string.Format("ParentGuid='{0}'", Guid.Empty);
							DataTable dataTableParent = dataViewMenu.ToTable();

							foreach (DataRow rowData in dataTableParent.Rows)
							{
								var menu1 = new Common.Menu();
								menu1.Path = Helper.GetString(rowData["Content"]).Length > 0 ? string.Format("/{0}{1}/{2}", (int)Business.ContentType.Menu, rowData["ID"].ToString(), rowData["Title"]) : "#";
								menu1.Title = rowData["Title"].ToString();
								menu1.Type = (int)menuType;
								menu1.Target = "_self";

								menu1.Children = GenerateDataMenu(dataTableDataMenu, Helper.GetGuid(row["Guid"]), (Business.DefaultPages)Helper.GetInt(domainInfo.Rows[0]["DefaultPage"]));
								menu1.Location=Helper.GetInt(row["Location"]);
								lstMenu.Add(menu1);
							}
							break;
					}
				}

				return lstMenu;
			}
			catch { return lstMenu; }
		}

		private static List<Common.Menu> GenerateDataMenu(DataTable dataTableDataMenu, Guid root, Business.DefaultPages defaultPage)
		{
			DataView dataViewMenu = dataTableDataMenu.DefaultView;
			dataViewMenu.RowFilter = string.Format("ParentGuid='{0}'", root);
			DataTable dataTableChildren = dataViewMenu.ToTable();

			List<Common.Menu> lstDataMenu = new List<Common.Menu>();
			var menu = new Common.Menu();

			foreach (DataRow row in dataTableChildren.Rows)
			{
				menu.Path = Helper.GetString(row["Content"]).Length > 0 ? string.Format("/{0}{1}/{2}", (int)Business.ContentType.Menu, row["ID"], row["Title"]) : "#";
				menu.Title = row["Title"].ToString();
				menu.Type = (int)Business.DomainMenuType.DataCenter;
				menu.Target = "_self";

				menu.Children = GenerateDataMenu(dataTableDataMenu, Helper.GetGuid(row["Guid"]), defaultPage);
				lstDataMenu.Add(menu);
			}

			return lstDataMenu;
		}
	}
}
