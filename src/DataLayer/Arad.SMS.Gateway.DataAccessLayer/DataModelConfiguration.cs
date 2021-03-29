using System.Data.Entity.ModelConfiguration;

namespace DataAccessLayer
{
	public class DataModelConfiguration<T> : EntityTypeConfiguration<T> where T : class
	{
	}
}
