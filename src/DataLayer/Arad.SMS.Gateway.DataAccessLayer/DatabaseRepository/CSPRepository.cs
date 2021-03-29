using DataAccessLayer.Contracts;
using System;
using System.Linq;

namespace DataAccessLayer
{
	public class CSPRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		public void Add(TEntity entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Delete(TEntity entity)
		{
			throw new NotImplementedException();
		}

		public IQueryable<TEntity> GetAll()
		{
			throw new NotImplementedException();
		}

		public TEntity GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(TEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
