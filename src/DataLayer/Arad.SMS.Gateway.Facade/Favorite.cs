using GeneralLibrary.BaseCore;
using System;

namespace Facade
{
	public class Favorite : FacadeEntityBase
	{

		public static object GetFavorites(Guid parentGuid)
		{
			Business.Favorite favoriteController = new Business.Favorite();
			return favoriteController.GetFavorites(parentGuid);
		}
	}
}
