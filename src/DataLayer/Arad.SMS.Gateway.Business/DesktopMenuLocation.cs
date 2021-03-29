using Common;
using GeneralLibrary.BaseCore;
using System.Data;
using System;
using GeneralLibrary;

namespace Business
{
	public class DesktopMenuLocation : BusinessEntityBase
	{
		public DesktopMenuLocation(DataAccessBase dataAccessProvider = null)
			: base(TableNames.DesktopMenuLocations.ToString(), dataAccessProvider) { }

		public bool UpdateDesktopMenuLocation(Common.DesktopMenuLocation desktopMenuLocation)
		{
			return base.ExecuteSPCommand("Update", "@Guid", desktopMenuLocation.DesktopMenuLocationGuid,
																							"@Location", desktopMenuLocation.Location,
																							"@Desktop", desktopMenuLocation.Desktop,
																							"@DomainMenuGuid", desktopMenuLocation.DataCenterGuid);
		}

		public DataTable GetPagedDesktopMenuLocation(Guid domainGuid, string sortField, int pageNo, int pageSize, ref int rowCount)
		{
			DataSet desktopMenuLocationInfo = base.FetchSPDataSet("GetPagedDesktopMenuLocation",
																																"@DomainGuid", domainGuid,
																																"@PageNo", pageNo,
																																"@PageSize", pageSize,
																																"@SortField", sortField);
			rowCount = Helper.GetInt(desktopMenuLocationInfo.Tables[0].Rows[0]["RowCount"]);

			return desktopMenuLocationInfo.Tables[1];
		}

	}
}
