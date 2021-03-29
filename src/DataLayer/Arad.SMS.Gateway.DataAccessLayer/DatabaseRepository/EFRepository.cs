using DataAccessLayer.Contracts;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DataAccessLayer
{
	public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		public DbContext DbContext { get; set; }
		protected DbSet<TEntity> DbSet { get; set; }

		public EFRepository(DataAccessManager dbContext)
		{
			if (dbContext == null)
				throw new Exception("dbContext");

			DbContext = dbContext;
			DbSet = DbContext.Set<TEntity>();
		}

		public IQueryable<TEntity> GetAll()
		{
			return DbSet;
		}

		public TEntity GetById(int id)
		{
			return DbSet.Find(id);
		}

		public void Add(TEntity entity)
		{
			DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
			if (dbEntityEntry.State != EntityState.Detached)
			{
				dbEntityEntry.State = EntityState.Added;
			}
			else
			{
				DbSet.Add(entity);
			}
		}

		public void Update(TEntity entity)
		{
			DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
			if (dbEntityEntry.State == EntityState.Detached)
			{
				DbSet.Attach(entity);
			}
			dbEntityEntry.State = EntityState.Modified;
		}

		public void Delete(TEntity entity)
		{
			DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
			if (dbEntityEntry.State != EntityState.Deleted)
			{
				dbEntityEntry.State = EntityState.Deleted;
			}
			else
			{
				DbSet.Attach(entity);
				DbSet.Remove(entity);
			}
		}

		public void Delete(int id)
		{
			var entity = GetById(id);
			if (entity == null) return;
			Delete(entity);
		}
	}
}
