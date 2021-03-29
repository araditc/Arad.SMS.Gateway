using System.IO;
using System.Web;

namespace GeneralLibrary.BaseCore
{
	public class Entity<T>
	{
		private string modelDatabaseInfoPath;

		public Entity()
		{
			modelDatabaseInfoPath = HttpContext.Current.Server.MapPath(string.Format(@"{0}\{1}.xml", ConfigurationManager.GetSetting("ModelDatabaseInfo"), typeof(T).Name));
			modelDatabaseInfoPath = File.Exists(modelDatabaseInfoPath) ? modelDatabaseInfoPath : string.Empty;
		}

		public DatabaseType DBType
		{
			get
			{
				return (DatabaseType)Helper.GetInt(ConfigurationManager.GetSetting("DatabaseType"));
			}
		}
		public string DataSource
		{
			get { return ConfigurationManager.GetSetting("DataSource"); }
		}

		public string DatabaseName
		{
			get { return ConfigurationManager.GetSetting("InitialCatalog"); }
		}

		public string UserID
		{
			get { return ConfigurationManager.GetSetting("UserID"); }
		}

		public string DBPassword
		{
			get { return ConfigurationManager.GetSetting("Password"); }
		}

		public int ConnectTimeout
		{
			get { return Helper.GetInt(ConfigurationManager.GetSetting("ConnectTimeout")); }
		}

		public string DBPrefix
		{
			get { return ConfigurationManager.GetSetting("DBPrefix"); }
		}

		public int QueryTimeout
		{
			get { return Helper.GetInt(ConfigurationManager.GetSetting("QueryTimeout")); }
		}

		public bool MultipleActiveResultSets
		{
			get { return Helper.GetBool(ConfigurationManager.GetSetting("MultipleActiveResultSets")); }
		}

		public string ConnectionSpecification
		{
			get { return ConfigurationManager.GetSetting("ConnectionSpecification"); }
		}

		public string ConnectionString
		{
			get
			{
				if (DBType == DatabaseType.SqlServer)
				{
					if (string.IsNullOrEmpty(UserID) || string.IsNullOrEmpty(DBPassword))
						return string.Format("Data Source={0}; Initial Catalog={1}; Integrated Security=SSPI; Connect Timeout={2}; MultipleActiveResultSets={3};", DataSource, DatabaseName, ConnectTimeout, MultipleActiveResultSets);
					else
						return string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}; Connect Timeout={4}; MultipleActiveResultSets={5};", DataSource, DatabaseName, UserID, DBPassword, ConnectTimeout, MultipleActiveResultSets);
				}
				else if (DBType == DatabaseType.ClusterPoint)
					return ConnectionSpecification;
				else
					return string.Empty;
			}
		}
	}
}
