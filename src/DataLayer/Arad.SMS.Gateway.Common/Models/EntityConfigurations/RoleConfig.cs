using Common.Models.DataModels;
using DataAccessLayer;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.EntityConfigurations
{
	public class RoleConfig : DataModelConfiguration<Role>
	{
		public RoleConfig()
		{
			ToTable("Roles");

			//PrimaryKey
			HasKey(role => role.Guid);

			Property(role => role.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			Property(role => role.Title).IsRequired();

			HasRequired(role => role.UserDefinedRole).WithMany(user => user.DefinedRoles);
		}
	}
}
