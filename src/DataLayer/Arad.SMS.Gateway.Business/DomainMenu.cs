using Common;
using GeneralLibrary.BaseCore;
using System.Data;
using System;
using GeneralLibrary;

namespace Business
{
	public class DomainMenu : BusinessEntityBase
	{
		public DomainMenu(DataAccessBase dataAccessProvider = null)
			: base(TableNames.DomainMenus.ToString(), dataAccessProvider) { }

		public bool UpdateDomainMenu(Common.DomainMenu domainMenu)
		{
			return base.ExecuteSPCommand("Update", "@Guid", domainMenu.DomainMenuGuid,
																							"@Type", domainMenu.Type,
																							"@Title", domainMenu.Title,
																							"@Priority",domainMenu.Priority,
																							"@Link", domainMenu.Link,
																							"@DataCenterGuid", domainMenu.DataCenterGuid,
																							"@StaticPageReference", domainMenu.StaticPageReference,
																							"@TargetType", domainMenu.TargetType,
																							"@IsActive", domainMenu.IsActive);
		}

		public DataTable GetPagedDomainMenu(Common.DomainMenu domainMenu, int activeStatus, int typeStatus, string sortField, int pageNo, int pageSize, ref int rowCount)
		{
			DataSet domainMenuInfo = base.FetchSPDataSet("GetPagedDomainMenu", "@DomainGuid", domainMenu.DomainGuid,
																																"@Title", domainMenu.Title,
																																"@Type", typeStatus,
																																"@IsActive", (activeStatus == 2 ? DBNull.Value : (object)activeStatus),
																																"@PageNo", pageNo,
																																"@PageSize", pageSize,
																																"@SortField", sortField);
			rowCount = Helper.GetInt(domainMenuInfo.Tables[0].Rows[0]["RowCount"]);

			return domainMenuInfo.Tables[1];
		}

		public DataTable GetDomainMenus(Guid domainGuid)
		{
			return base.FetchSPDataTable("GetDomainMenus", "@DomainGuid", domainGuid);
		}

		public DataTable GetMenusOfDesktop(Guid domainGuid, Desktop desktop)
		{
			return base.FetchSPDataTable("GetMenusOfDesktop", "@DomainGuid", domainGuid, "@Desktop", (int)desktop);
		}

		public DataTable GetDatasOfHomePage(Guid domainGuid)
		{
			return base.FetchSPDataTable("GetDatasOfHomePage", "@DomainGuid", domainGuid);
		}
	}
}
