using GeneralLibrary;

namespace DatabaseInfo
{
	public interface IDatabaseInfoProvider
	{
		DataSourceType Type { get; }
		string DataSource { get; set; }
		string DatabaseName { get; set; }
		string Username { get; set; }
		string Password { get; set; }
		int ConnectTimeout { get; set; }
		string DBPrefix { get; set; }
		int QueryTimeout { get; set; }
		bool MultipleActiveResultSets { get; set; }
		string ConnectionString { get; }
	}
}
