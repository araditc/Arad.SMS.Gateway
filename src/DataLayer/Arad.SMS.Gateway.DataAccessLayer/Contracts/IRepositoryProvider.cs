namespace DataAccessLayer.Contracts
{
	public interface IRepositoryProvider
	{
		IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
	}
}
