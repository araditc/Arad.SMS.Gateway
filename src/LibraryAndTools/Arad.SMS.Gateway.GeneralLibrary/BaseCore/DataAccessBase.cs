// --------------------------------------------------------------------
// Copyright (c) 2005-2020 Arad ITC.
//
// Author : Ammar Heidari <ammar@arad-itc.org>
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Arad.SMS.Gateway.GeneralLibrary.BaseCore
{
	public abstract class DataAccessBase
	{
		public delegate DataAccessBase GetDataAccessObject(string dBPrefix, int queryTimeout, string connectionString);

		public static GetDataAccessObject GetActiveDataAccessProvider;

		#region------------- General Method ------------
		public abstract Guid Insert(string tableName, List<SqlParameter> sqlParameterCollection);
		public abstract bool Update(string tableName, Guid entityGuid, List<SqlParameter> sqlParameterCollection);
		public abstract bool Delete(string tableName, Guid entityGuid);
		public abstract DataRow Load(string dataSourceName, Guid entityGuid);
		#endregion

		#region------------ Execute Command ------------
		public abstract bool ExecuteCommand(string sqlStatement, params object[] parameters);
		public abstract bool ExecuteSPCommand(string actionName, params object[] parameters);
		#endregion

		#region------------ Select Block ---------------
		public abstract DataTable SelectDataTable(string statement, params object[] parameters);
		public abstract DataTable GetPagedDataTable(string fieldsList, string selectSource, string whereClause, string orderClause, int pageNo, int pageSize, out int resultCount, params object[] parametersInfo);
		public abstract DataSet SelectDataSet(string statement, params object[] parameters);
		public abstract DataTable SelectSPDataTable(string storedProcedureName, params object[] parameters);
		public abstract DataSet SelectSPDataSet(string storedProcedureName, params object[] parameters);
		#endregion

		#region---------- Manage Transaction -----------
		public abstract void BeginTransaction(System.Data.IsolationLevel isolationLevel);
		public abstract void BeginTransaction()	;
		public abstract void RollbackTransaction();
		public abstract void CommitTransaction();
		#endregion
	}
}
