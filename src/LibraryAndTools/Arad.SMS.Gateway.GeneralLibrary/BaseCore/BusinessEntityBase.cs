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
using System.Data;
using System.Data.SqlClient;
using DatabaseInfo;

namespace Arad.SMS.Gateway.GeneralLibrary.BaseCore
{
	public abstract class BusinessEntityBase : EntityBase
	{
		private DataAccessBase dataAccessProvider;
		public DataAccessBase DataAccessProvider
		{
			get
			{
				if (dataAccessProvider == null)
					dataAccessProvider = DataAccessBase.GetActiveDataAccessProvider(DatabaseInfoProvider.DBInfo.DBPrefix, DatabaseInfoProvider.DBInfo.QueryTimeout, DatabaseInfoProvider.DBInfo.ConnectionString);

				return dataAccessProvider;
			}

			private set
			{
				dataAccessProvider = value;
			}
		}

		public BusinessEntityBase(string tableName, DataAccessBase dataAccess = null)
			: base(tableName)
		{
			this.DataAccessProvider = dataAccess;
		}

		internal BusinessEntityBase() { }

		#region Events
		public delegate void EntityChangeEventHandler(object sender, EntityChangeEventArgs e);
		public event EntityChangeEventHandler OnEntityChange;
		#endregion

		#region------------- General Method ------------
		public Guid Insert(CommonEntityBase commonObject)
		{
			List<SqlParameter> sqlParameters = commonObject.GetProcedureParameters();
			Guid newEntityGuid = Guid.Empty;

			this.BeginTransaction();
			try
			{
				newEntityGuid = DataAccessProvider.Insert(this.TableName, sqlParameters);

				if (OnEntityChange != null)
				{
					commonObject.PrimaryKey = newEntityGuid;
					this.OnEntityChange(commonObject, new EntityChangeEventArgs(EntityChangeActionTtype.Insert));
				}

				this.CommitTransaction();
			}
			catch
			{
				this.RollbackTransaction();
				throw;
			}

			return newEntityGuid;
		}

		public bool Update(CommonEntityBase commonObject)
		{
			List<SqlParameter> sqlParameters = commonObject.GetProcedureParameters();

			this.BeginTransaction();
			try
			{
				DataAccessProvider.Update(this.TableName, commonObject.PrimaryKey, sqlParameters);

				if (OnEntityChange != null)
					this.OnEntityChange(commonObject, new EntityChangeEventArgs(EntityChangeActionTtype.Insert));

				this.CommitTransaction();
			}
			catch
			{
				this.RollbackTransaction();
				throw;
			}

			return true;
		}

		public bool Delete(Guid entityGuid)
		{
			this.BeginTransaction();
			try
			{
				DataAccessProvider.Delete(this.TableName, entityGuid);
				if (OnEntityChange != null)
					this.OnEntityChange(entityGuid, new EntityChangeEventArgs(EntityChangeActionTtype.Insert));

				this.CommitTransaction();
			}
			catch
			{
				this.RollbackTransaction();
				throw;
			}

			return true;
		}

		public bool Load(Guid entityGuid, CommonEntityBase commonObject)
		{
			if (commonObject == null)
				throw new Exception("Common object not initialized");

			DataRow loadedDataRow;
			if (commonObject.HasReadOnlyFields)
				loadedDataRow = DataAccessProvider.Load(this.ViewName, entityGuid);
			else
				loadedDataRow = DataAccessProvider.Load(this.TableName, entityGuid);

			return commonObject.Load(loadedDataRow);
		}
		#endregion

		#region ------------- Get Field Value -----------
		protected object GetFieldValue(string statement, params object[] parameters)
		{
			try
			{
				return FetchDataTable(statement, parameters).Rows[0][0];
			}
			catch
			{
				return DBNull.Value;
			}
		}

		protected object GetSPFieldValue(string spName, params object[] parameters)
		{
			try
			{
				return FetchSPDataTable(spName, parameters).Rows[0][0];
			}
			catch
			{
				return DBNull.Value;
			}
		}

		protected string GetStringFieldValue(string statement, params object[] parameters)
		{
			return Helper.GetString(GetFieldValue(statement, parameters));
		}

		protected string GetSPStringFieldValue(string spName, params object[] parameters)
		{
			return Helper.GetString(GetSPFieldValue(spName, parameters));
		}

		protected Guid GetGuidFieldValue(string statement, params object[] parameters)
		{
			return Helper.GetGuid(GetFieldValue(statement, parameters));
		}

		protected Guid GetSPGuidFieldValue(string spName, params object[] parameters)
		{
			return Helper.GetGuid(GetSPFieldValue(spName, parameters));
		}

		protected int GetIntFieldValue(string statement, params object[] parameters)
		{
			return Helper.GetInt(GetFieldValue(statement, parameters));
		}

		protected int GetSPIntFieldValue(string spName, params object[] parameters)
		{
			return Helper.GetInt(GetSPFieldValue(spName, parameters));
		}

		protected long GetLongFieldValue(string statement, params object[] parameters)
		{
			return Helper.GetLong(GetFieldValue(statement, parameters));
		}

		protected long GetSPLongFieldValue(string spName, params object[] parameters)
		{
			return Helper.GetLong(GetSPFieldValue(spName, parameters));
		}

		protected decimal GetDecimalFieldValue(string statement, params object[] parameters)
		{
			return Helper.GetDecimal(GetFieldValue(statement, parameters));
		}

		protected decimal GetSPDecimalFieldValue(string spName, params object[] parameters)
		{
			return Helper.GetDecimal(GetSPFieldValue(spName, parameters));
		}

		protected bool GetBoolFieldValue(string statement, params object[] parameters)
		{
			return Helper.GetBool(GetFieldValue(statement, parameters));
		}

		protected bool GetSPBoolFieldValue(string spName, params object[] parameters)
		{
			return Helper.GetBool(GetSPFieldValue(spName, parameters));
		}
		#endregion

		#region------ Fetch Data With Statement --------
		protected DataTable FetchDataTable(string statement, params object[] parameters)
		{
			return DataAccessProvider.SelectDataTable(statement, parameters);
		}

		protected DataTable FetchPagedDataTable(string fieldsList, string selectSource, string whereClause, string orderClause, int pageNo, int pageSize, out int resultCount, params object[] parametersInfo)
		{
			return DataAccessProvider.GetPagedDataTable(fieldsList, selectSource, whereClause, orderClause, pageNo, pageSize, out resultCount, parametersInfo);
		}

		protected DataSet FetchDataSet(string statement, params object[] parameters)
		{
			return DataAccessProvider.SelectDataSet(statement, parameters);
		}

		protected DataTable FetchSPDataTable(string spName, params object[] parameters)
		{
			return DataAccessProvider.SelectSPDataTable(GetSPName(spName), parameters);
		}

		internal DataTable FetchSPDataTableWithFullSPName(string spName, params object[] parameters)
		{
			return DataAccessProvider.SelectSPDataTable(spName, parameters);
		}

		protected DataSet FetchSPDataSet(string spName, params object[] parameters)
		{
			return DataAccessProvider.SelectSPDataSet(GetSPName(spName), parameters);
		}
		#endregion

		#region------------ Execute Command ------------
		protected bool ExecuteCommand(string sqlStatement, params object[] parameters)
		{
			return DataAccessProvider.ExecuteCommand(sqlStatement, parameters);
		}

		protected bool ExecuteSPCommand(string actionName, params object[] parameters)
		{
			return DataAccessProvider.ExecuteSPCommand(GetSPName(actionName), parameters);
		}
		#endregion

		#region---------- Manage Transaction -----------
		public void BeginTransaction()
		{
			DataAccessProvider.BeginTransaction();
		}

		public void CommitTransaction()
		{
			DataAccessProvider.CommitTransaction();
		}

		public void RollbackTransaction()
		{
			DataAccessProvider.RollbackTransaction();
		}
		#endregion
	}
}
