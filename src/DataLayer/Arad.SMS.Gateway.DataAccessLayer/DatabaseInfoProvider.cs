using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
	public class DatabaseInfoProvider
	{
		private static DatabaseInfoProvider dbInfo;
		private string dataSource;
		private string initialCatalog;
		private string userID;
		private string password;
		private int connectTimeout;
		private string dbPrefix;
		private int queryTimeout;

		private DatabaseInfoProvider()
		{
			this.dataSource = ".";
			this.initialCatalog = "SMS";
			this.userID = "sa";
			this.password = "12345";
			this.connectTimeout = 20;
			this.dbPrefix = "";
			this.queryTimeout = 20;
		}

		public static DatabaseInfoProvider DBInfo
		{
			get
			{
				if (dbInfo == null)
					dbInfo = new DatabaseInfoProvider();

				return dbInfo;
			}
		}

		public string DataSource
		{
			get { return dataSource; }
			private set { dataSource = value; }
		}

		public string InitialCatalog
		{
			get { return initialCatalog; }
			private set { initialCatalog = value; }
		}

		public string UserID
		{
			get { return userID; }
			private set { userID = value; }
		}

		public string Password
		{
			get { return password; }
			private set { password = value; }
		}

		public int ConnectTimeout
		{
			get { return connectTimeout; }
			private set { connectTimeout = value; }
		}

		public string DBPrefix
		{
			get { return DBInfo.dbPrefix; }
			private set { DBInfo.dbPrefix = value; }
		}

		public int QueryTimeout
		{
			get { return queryTimeout; }
			private set { queryTimeout = value; }
		}

		public string ConnectionString
		{
			get
			{
				if (string.IsNullOrEmpty(UserID) || string.IsNullOrEmpty(Password))
				{
					return string.Format("Data Source={0}; Initial Catalog={1}; Integrated Security=SSPI; Connect Timeout={2}", DataSource, InitialCatalog, ConnectTimeout);
				}
				else
				{
					return string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}; Connect Timeout={4}", DataSource, InitialCatalog, UserID, Password, ConnectTimeout);
				}
			}
		}

		public override string ToString()
		{
			System.Text.StringBuilder ret = new System.Text.StringBuilder();
			ret.Append("Database Info:" + Environment.NewLine);
			ret.Append("\tDataSource:     " + this.DataSource + Environment.NewLine);
			ret.Append("\tInitialCatalog: " + this.InitialCatalog + Environment.NewLine);
			ret.Append("\tUserID:         " + this.UserID + Environment.NewLine);
			ret.Append("\tPassword:       " + this.Password + Environment.NewLine);
			ret.Append("\tConnectTimeout: " + this.ConnectTimeout + Environment.NewLine);
			ret.Append("\tDBPrefix:       " + this.DBPrefix + Environment.NewLine);
			ret.Append("\tQueryTimeout:   " + this.QueryTimeout + Environment.NewLine);

			return ret.ToString();
		}
	}
}
