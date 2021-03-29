using GeneralLibrary;

namespace DatabaseInfo
{
	public class CpsDatabaseInfo : IDatabaseInfoProvider
	{
		private string dataSource;
		private string dbPrefix;
		private string databaseName;
		private string username;
		private string password;

		public string DataSource
		{
			get
			{
				return dataSource;
			}
			set
			{
				dataSource = value;
			}
		}

		public string DBPrefix
		{
			get
			{
				return dbPrefix;
			}
			set
			{
				dbPrefix = value;
			}
		}

		public string DatabaseName
		{
			get
			{
				if (string.IsNullOrEmpty(databaseName))
					return ConfigurationManager.GetCPSSetting("Storage");
				else
					return databaseName;
			}
			set
			{
				databaseName = value;
			}
		}

		public string Username
		{
			get
			{
				if (string.IsNullOrEmpty(username))
					return ConfigurationManager.GetCPSSetting("UserID");
				else
					return username;
			}
			set
			{
				username = value;
			}
		}

		public string Password
		{
			get
			{
				if (string.IsNullOrEmpty(password))
					return ConfigurationManager.GetCPSSetting("Password");
				else
					return password;
			}
			set
			{
				password = value;
			}
		}

		public string ConnectionString
		{
			get
			{
				return ConfigurationManager.GetCPSSetting("ConnectionURL");
			}
		}
	}
}
