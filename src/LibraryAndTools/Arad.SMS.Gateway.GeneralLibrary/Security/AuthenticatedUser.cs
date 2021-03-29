using System;

namespace GeneralLibrary.Security
{
	public class AuthenticatedUser
	{
		public Guid UserGuid { get; set; }
		public Guid ParentGuid { get; set; }
		public string Username { get; set; }
		public string DisplayName { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsMainAdmin { get; set; }
		public bool IsSuperAdmin { get; set; }
		public bool DocumentIsVerify { get; set; }
		public DateTime ExpireDate { get; set; }
	}
}
