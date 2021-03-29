using Common.Models.DataModels;
using DataAccessLayer;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.EntityConfigurations
{
	public class UserConfig : DataModelConfiguration<User>
	{
		public UserConfig()
		{
			ToTable("Users");

			//Primary Key
			HasKey(user => user.Guid);

			Property(user => user.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			Property(user => user.UserName).HasMaxLength(32).IsRequired();
			Property(user => user.Password).IsRequired();

			HasOptional(user => user.Parent).WithMany(user => user.ManageUsers)
																			.HasForeignKey(user => user.ParentGuid)
																			.WillCascadeOnDelete(false);

			HasRequired(user => user.Role).WithMany(role => role.OwnersRole);
		}
	}
}
