using System;
using System.Web;

namespace GeneralLibrary.Security
{
	public class UserIdentity
	{
		public static bool IsAuthenticated
		{
			get
			{
				if (HttpContext.Current.Session["AuthenticatedUser"] == null)
					return false;
				else
					return true;
			}
		}

		public static AuthenticatedUser AuthenticatedUserInfo
		{
			get
			{
				if (IsAuthenticated)
					return (AuthenticatedUser)HttpContext.Current.Session["AuthenticatedUser"];
				else
					return new AuthenticatedUser();
			}
		}

		public static Guid UserGuid
		{
			get
			{
				if (IsAuthenticated)
					return AuthenticatedUserInfo.UserGuid;
				else
					return Guid.Empty;
			}
		}

		public static Guid ParentGuid
		{
			get
			{
				if (IsAuthenticated)
					return AuthenticatedUserInfo.ParentGuid;
				else
					return Guid.Empty;
			}
		}

		public static string Username
		{
			get
			{
				if (IsAuthenticated)
					return AuthenticatedUserInfo.Username;
				else
					return string.Empty;
			}
		}

		public static string UserDisplayName
		{
			get
			{
				if (IsAuthenticated)
					return AuthenticatedUserInfo.DisplayName;
				else
					return string.Empty;
			}
		}

		public static bool IsAdmin
		{
			get
			{
				if (IsAuthenticated)
					return AuthenticatedUserInfo.IsAdmin;
				else
					return false;
			}
		}

		public static bool IsMainAdmin
		{
			get
			{
				if (IsAuthenticated)
					return AuthenticatedUserInfo.IsMainAdmin;
				else
					return false;
			}
		}

		public static bool IsSuperAdmin
		{
			get
			{
				if (IsAuthenticated)
					return AuthenticatedUserInfo.IsSuperAdmin;
				else
					return false;
			}
		}

		public static bool DocumentIsVerify
		{
			get
			{
				if (IsAuthenticated)
					return AuthenticatedUserInfo.DocumentIsVerify;
				else
					return false;
			}
		}

		public static DateTime ExpireDate
		{
			get
			{
				if (IsAuthenticated)
					return AuthenticatedUserInfo.ExpireDate;
				else
					return DateTime.MinValue;
			}
		}
	}
}
