using GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;

namespace Common.Models.DataModels
{
	public class User : Entity<User>
	{
		public CompanyInfo CompanyInfo { get; set; }
		public User()
		{
			CompanyInfo = new CompanyInfo();
		}

		public Guid Guid { get; set; }
		public Guid ParentGuid { get; set; }
		public long Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string SecondPassword { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FatherName { get; set; }
		public string NationalCode { get; set; }
		public string ZipCode { get; set; }
		public string ShCode { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Mobile { get; set; }
		public string FaxNumber { get; set; }
		public string Address { get; set; }
		public Guid ZoneGuid { get; set; }
		public DateTime BirthDate { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime ExpireDate { get; set; }
		public decimal Credit { get; set; }
		public decimal PanelPrice { get; set; }
		public int Type { get; set; }
		public bool IsActive { get; set; }
		public bool IsAuthenticated { get; set; }
		public bool IsActiveSend { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsSuperAdmin { get; set; }
		public bool IsMainAdmin { get; set; }
		public int MaximumAdmin { get; set; }
		public int MaximumUser { get; set; }
		public int MaximumPhoneNumber { get; set; }
		public int MaximumEmailAddress { get; set; }

		//public Guid DomainGroupPriceGuid { get; set; }
		public Guid PriceGroupGuid { get; set; }
		public bool IsFixPriceGroup { get; set; }
		public Guid DomainGuid { get; set; }

		public virtual User Parent { get; set; }
		public virtual ICollection<User> ManageUsers { get; set; }

		public virtual Role Role { get; set; }
		public virtual ICollection<Role> DefinedRoles { get; set; }
	}
}
