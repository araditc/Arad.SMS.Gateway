using System.Linq;

namespace DataAccessLayer.Contracts
{
	public interface IRepository<TEntity> where TEntity : class
	{
		IQueryable<TEntity> GetAll();
		TEntity GetById(int id);
		void Add(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
		void Delete(int id);
	}
}
