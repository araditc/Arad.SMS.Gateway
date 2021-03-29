using Common.Models;
using DataAccessLayer.ModelConfigs;
using System.Data.Entity;

namespace DataAccessLayer
{
	public class Context : DbContext, IUnitOfWork
	{
		public Context() : base("Connection")
		{
			Database.SetInitializer<Context>(null);
			//Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }


		public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
		{
			return base.Set<TEntity>();
		}


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new UserConfig());
		}
	}
}
