using System;
using System.Data;
using GeneralLibrary;

namespace Business
{
	public class UserAccess:DataAccessLayer.DataAccess
	{
		private Guid userGuid;
		private Guid accessGuid;
		private bool hasError;
		private string errorMessage;


		public bool HasError
		{
			get { return hasError; }
			set { hasError = value; }
		}

		public string ErrorMessage
		{
			get { return errorMessage + "<br/>"; }
			set { errorMessage = value; }
		}

		public Guid UserGuid
		{
			get { return userGuid; }
			set { userGuid = value; }
		}

		public Guid AccessGuid
		{
			get { return accessGuid; }
			set { accessGuid = value; }
		}

		public UserAccess()
			: base("UserAccesses")
		{
			HasError = false;
			ErrorMessage = "";
		}

		public DataTable GetUserAccess()
		{
			return base.SelectSPDataTable("GetUserAccess", "@UserGuid", UserGuid);
		}

		public bool insert()
		{
			return base.ExecuteSPCommand("Insert", "@UserGuid", UserGuid, "@AccessGuid", AccessGuid);
		}

		public bool insertUserAccess(string[] accessGuidArray)
		{
			base.BeginTransaction();
			UserGuid = userGuid;
			if (!DeleteAllAccessOfUser())
				return false;
			else
				for (int counterAccessArray = 0; counterAccessArray < accessGuidArray.Length; counterAccessArray++)
				{
					AccessGuid = Helper.GetGuid(accessGuidArray[counterAccessArray]);
					if (!base.ExecuteSPCommand("Insert", "@UserGuid", UserGuid, "@AccessGuid", AccessGuid))
						return false;
				}

			base.CommitTransaction();
			return true;
		}

		public bool DeleteAccess()
		{
			return base.ExecuteSPCommand("DeleteAccess", "@UserGuid", UserGuid, "@AccessGuid", AccessGuid);
		}

		public bool DeleteAllAccessOfUser()
		{

			return base.ExecuteSPCommand("DeleteAllAccessOfUser", "@UserGuid", UserGuid);
		}

	}
}
