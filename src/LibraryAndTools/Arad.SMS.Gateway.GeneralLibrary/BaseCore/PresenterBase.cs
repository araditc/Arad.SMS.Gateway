using GeneralLibrary.Security;
using System;
using System.Collections.Generic;

namespace GeneralLibrary.BaseCore
{
	public abstract class PresenterBase
	{
		private StandardLayouts layout;
		public StandardLayouts Layout
		{
			get
			{
				return layout;
			}
			set
			{
				layout = value;
			}
		}
		public bool CheckServicePermissions()
		{
			
			bool isOptionalPermissions = false;
			bool hasPermissions = true;
			int errorServiceEnum = 0;
			string errorServicesArray = string.Empty;
			List<int> servicePermissions = GetServicePermissions(ref isOptionalPermissions);

			if (!isOptionalPermissions)
				hasPermissions = SecurityManager.HasAllServicePermission(UserIdentity.UserGuid, ref errorServiceEnum, servicePermissions.ToArray());
			else
			{
				errorServicesArray = string.Join(",", servicePermissions.ToArray());
				hasPermissions = SecurityManager.HasAtLeastOneServicePermission(UserIdentity.UserGuid, servicePermissions.ToArray());
			}

			return hasPermissions;
		}

		public virtual void Load() { }
		public virtual void PreRender() { }
		public virtual void FirstTimeInit() { }
		protected abstract List<int> GetServicePermissions(ref bool isOptionalPermissions);
		protected abstract int GetUserControlID();
		protected abstract string GetUserControlTitle();
	}
}