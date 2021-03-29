using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GeneralLibrary.BaseCore
{
	public interface ISqlDataAccess<T, Tkey> : IDataAccessBase<T, Tkey>
	{
		int Timeout { get; set; }

		Guid Insert(string tableName, List<SqlParameter> sqlParameterCollection);
		bool Update(string tableName, Guid entityGuid, List<SqlParameter> sqlParameterCollection);
		bool Delete(string tableName, Guid entityGuid);
		DataRow Load(string dataSourceName, Guid entityGuid);

		bool ExecuteCommand(string sqlStatement, params object[] parameters);
		bool ExecuteSPCommand(string actionName, params object[] parameters);

		DataTable SelectDataTable(string statement, params object[] parameters);
		DataTable GetPagedDataTable(string fieldsList, string selectSource, string whereClause, string orderClause, int pageNo, int pageSize, out int resultCount, params object[] parametersInfo);
		DataSet SelectDataSet(string statement, params object[] parameters);
		DataTable SelectSPDataTable(string storedProcedureName, params object[] parameters);
		DataSet SelectSPDataSet(string storedProcedureName, params object[] parameters);


		void BeginTransaction(System.Data.IsolationLevel isolationLevel);
		void BeginTransaction();
		void RollbackTransaction();
		void CommitTransaction();
	}
}
