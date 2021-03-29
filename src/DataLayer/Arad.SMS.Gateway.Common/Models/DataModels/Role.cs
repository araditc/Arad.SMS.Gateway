using GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;

namespace Common.Models.DataModels
{
	public class Role : Entity<Role>
	{
		public Guid Guid { get; set; }
		public long ID { get; set; }
		public byte Priority { get; set; }
		public string Title { get; set; }
		public DateTime CreateDate { get; set; }
		public bool IsDefault { get; set; }
		public bool IsSalePackage { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }

		public virtual User UserDefinedRole { get; set; }
		public virtual ICollection<User> OwnersRole { get; set; }
	}
}
