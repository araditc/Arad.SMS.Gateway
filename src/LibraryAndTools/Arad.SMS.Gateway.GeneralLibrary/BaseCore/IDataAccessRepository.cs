using System.Collections.Generic;

namespace GeneralLibrary.BaseCore
{
	public interface IDataAccessRepository<T, TKey> where T : IViewModel
	{
		IEnumerable<T> GetItems();
		T GetItem(TKey key);
		void AddItem(T newItem);
		void UpdateItem(TKey key, T updatedItem);
		void DeletedItem(TKey key);
	}
}
