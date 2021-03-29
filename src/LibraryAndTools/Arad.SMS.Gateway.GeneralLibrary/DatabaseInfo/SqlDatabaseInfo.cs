using System;
using GeneralLibrary;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseInfo
{
	public class SqlDatabaseInfo : IDatabaseInfoProvider
	{
		private static Dictionary<string, string> SqlConfigCache;
		//private string dataSource;
		//private string dbPrefix;
		//private string databaseName;
		//private string username;
		//private string password;
		//private int connectTimeout;
		//private int queryTimeout;
		//private bool multipleActiveResultSets;
		public SqlDatabaseInfo(string dbInfoFilePath = "")
		{
			if(dbInfoFilePath==string.Empty)
			SqlConfigCache = ConfigurationManager.ReadConfigurationFile();
			//else

		}

		public string DataSource
		{
			get
			{
				return SqlConfigCache[string.Format("{0}-DataSource", DataSourceType.SqlServer)];
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
				if (string.IsNullOrEmpty(dbPrefix))
					return ConfigurationManager.GetSetting("DBPrefix");
				else
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
					return ConfigurationManager.GetSQLSetting("InitialCatalog");
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
					return ConfigurationManager.GetSQLSetting("UserID");
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
					return ConfigurationManager.GetSQLSetting("Password");
				else
					return password;
			}
			set
			{
				password = value;
			}
		}

		public int ConnectTimeout
		{
			get
			{
				if (Helper.GetInt(connectTimeout) == 0)
					return Helper.GetInt(ConfigurationManager.GetSQLSetting("ConnectTimeout"));
				else
					return connectTimeout;
			}
			set
			{
				connectTimeout = value;
			}
		}

		public int QueryTimeout
		{
			get
			{
				if (Helper.GetInt(queryTimeout) == 0)
					return Helper.GetInt(ConfigurationManager.GetSQLSetting("QueryTimeout"));
				else
					return queryTimeout;
			}
			set
			{
				queryTimeout = value;
			}
		}

		public bool MultipleActiveResultSets
		{
			get
			{
				if (Helper.GetInt(multipleActiveResultSets) == 0)
					return Helper.GetBool(ConfigurationManager.GetSQLSetting("MultipleActiveResultSets"));
				else
					return multipleActiveResultSets;
			}
			set
			{
				multipleActiveResultSets = value;
			}
		}

		public string ConnectionString
		{
			get
			{
				if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
				{
					return string.Format("Data Source={0}; Initial Catalog={1}; Integrated Security=SSPI; Connect Timeout={2}; MultipleActiveResultSets={3};", DataSource, DatabaseName, ConnectTimeout, MultipleActiveResultSets);
				}
				else
				{
					return string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}; Connect Timeout={4}; MultipleActiveResultSets={5};", DataSource, DatabaseName, Username, Password, ConnectTimeout, MultipleActiveResultSets);
				}
			}
		}
	}
}
