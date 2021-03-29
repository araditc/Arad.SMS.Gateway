using System.Data.Entity;

namespace DataAccessLayer
{
	public abstract class DataAccessManager : DbContext
	{
		public DataAccessManager() : base(nameOrConnectionString: "Connection")
		{
			Database.SetInitializer<DataAccessManager>(null);
			//Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
		}

		protected abstract override void OnModelCreating(DbModelBuilder modelBuilder);
	}
}
