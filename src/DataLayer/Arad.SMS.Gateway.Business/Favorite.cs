using Common;
using GeneralLibrary.BaseCore;
using System;

namespace Business
{
	public class Favorite : BusinessEntityBase
	{
		public Favorite(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Favorites.ToString(), dataAccessProvider) { }

		public object GetFavorites(Guid parentGuid)
		{
			return FetchSPDataTable("GetFavorites", "@ParentGuid", parentGuid);
		}
	}
}
