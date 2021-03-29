using DataAccessLayer.Contracts;

namespace DataAccessLayer.Contract
{
	public interface IUnitOfWork
	{
		IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

		void Commit();
	}
}
