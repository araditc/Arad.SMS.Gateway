using System;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Facade
{
	public class DesktopMenuLocation : FacadeEntityBase
	{
		public static bool Insert(Common.DesktopMenuLocation desktopMenuLocation)
		{
			Business.DesktopMenuLocation desktopMenuLocationController = new Business.DesktopMenuLocation();
			return desktopMenuLocationController.Insert(desktopMenuLocation) != Guid.Empty ? true : false;
		}

		public static DataTable GetPagedDesktopMenuLocation(Guid domainGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.DesktopMenuLocation desktopMenuLocationController = new Business.DesktopMenuLocation();
			return desktopMenuLocationController.GetPagedDesktopMenuLocation(domainGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool UpdateDesktopMenuLocation(Common.DesktopMenuLocation desktopMenuLocation)
		{
			Business.DesktopMenuLocation desktopMenuLocationController = new Business.DesktopMenuLocation();
			return desktopMenuLocationController.UpdateDesktopMenuLocation(desktopMenuLocation);
		}

		public static Common.DesktopMenuLocation LoadDesktopMenuLocation(Guid desktopMenuLocationGuid)
		{
			Business.DesktopMenuLocation desktopMenuLocationController = new Business.DesktopMenuLocation();
			Common.DesktopMenuLocation desktopMenuLocation = new Common.DesktopMenuLocation();
			desktopMenuLocationController.Load(desktopMenuLocationGuid, desktopMenuLocation);
			return desktopMenuLocation;
		}

		public static bool Delete(Guid desktopMenuLocationGuid)
		{
			Business.DesktopMenuLocation desktopMenuLocationController = new Business.DesktopMenuLocation();
			return desktopMenuLocationController.Delete(desktopMenuLocationGuid);
		}
	}
}
