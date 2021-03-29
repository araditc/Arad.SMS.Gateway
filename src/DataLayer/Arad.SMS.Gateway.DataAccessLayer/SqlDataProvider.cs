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
using System.Text;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.DataAccessLayer
{
	public class SqlDataAccess : DataAccessBase
	{
		public static string DBPrefix { get; private set; }
		public static int QueryTimeout { get; private set; }
		public static string ConnectionString { get; private set; }

		public static DataAccessBase GetSqlDataAccess(string dBPrefix, int queryTimeout, string connectionString)
		{
			if (ConnectionString != connectionString)
			{
				DBPrefix = dBPrefix;
				QueryTimeout = queryTimeout;
				ConnectionString = connectionString;
			}

			SqlDataAccess sqlDataAccess = new SqlDataAccess();

			return sqlDataAccess;
		}

		private SqlDataAccess()
		{
			sqlConnection = new SqlConnection(ConnectionString);
		}

		#region--------- Connection Management ---------
		private SqlConnection sqlConnection;

		private void OpenConnection()
		{
			if (InTransaction)
				throw new Exception("Cannot open connection when in transaction.");

			sqlConnection.Close();
			sqlConnection.Open();
		}

		private void CloseConnection()
		{
			if (InTransaction)
				throw new Exception("Cannot close connection when in transaction.");

			if (sqlConnection.State != ConnectionState.Closed)
				sqlConnection.Close();
		}
		#endregion

		#region---------- Manage Transaction -----------
		private SqlTransaction transaction;
		private int transactionStack = 0;

		private bool InTransaction
		{
			get
			{
				return transactionStack > 0;
			}
		}

		public override void BeginTransaction()
		{
			BeginTransaction(IsolationLevel.ReadCommitted); // SQL Server default.
		}

		public override void BeginTransaction(IsolationLevel isolationLevel)
		{
			if (transactionStack == 0)
			{
				OpenConnection();
				transaction = sqlConnection.BeginTransaction(isolationLevel);
			}
			else
			{
				if (transaction.IsolationLevel != isolationLevel)
					throw new Exception("Unable to change IsolationLevel. Use same level in all BeginTransaction() calls.");
			}

			transactionStack++;
		}

		public override void RollbackTransaction()
		{
			if (!InTransaction)
				throw new Exception("The ROLLBACK TRANSACTION request has no corresponding BEGIN TRANSACTION.");

			try
			{
				transaction.Rollback();
			}
			finally
			{
				transactionStack = 0;
				CloseConnection();
			}
		}

		public override void CommitTransaction()
		{
			if (!InTransaction)
				throw new Exception("The COMMIT TRANSACTION request has no corresponding BEGIN TRANSACTION.");

			if (--transactionStack > 0)
				return;

			transaction.Commit();
			CloseConnection();
		}
		#endregion

		#region------------- General Method ------------
		public override Guid Insert(string tableName, List<SqlParameter> sqlParameterCollection)
		{
			Guid guid = Guid.NewGuid();

			SqlCommand command = BuildCommand(GetSPName(tableName, "Insert"), CommandType.StoredProcedure, sqlParameterCollection);
			SqlParameter parameter = new SqlParameter("@Guid", guid);
			command.Parameters.Add(parameter);

			try
			{
				command.ExecuteNonQuery();
			}
			catch
			{
				guid = Guid.Empty;
				throw;
			}
			finally
			{
				if (!InTransaction)
					CloseConnection();
			}

			return guid;
		}

		public override bool Update(string tableName, Guid entityGuid, List<SqlParameter> sqlParameterCollection)
		{
			SqlCommand command = BuildCommand(GetSPName(tableName, "Update"), CommandType.StoredProcedure, sqlParameterCollection);
			SqlParameter parameter = new SqlParameter("@Guid", entityGuid);

			command.Parameters.Add(parameter);

			try
			{
				command.ExecuteNonQuery();
			}
			finally
			{
				if (!InTransaction)
					CloseConnection();
			}

			return true;
		}

		public override bool Delete(string tableName, Guid entityGuid)
		{
			SqlCommand command = BuildCommand(GetSPName(tableName, "Delete"), CommandType.StoredProcedure);
			SqlParameter parameter = new SqlParameter("@Guid", entityGuid);

			command.Parameters.Add(parameter);

			try
			{
				command.ExecuteNonQuery();
			}
			finally
			{
				if (!InTransaction)
					CloseConnection();
			}

			return true;
		}

		public override DataRow Load(string dataSourceName, Guid entityGuid)
		{
			DataTable dataTable = new DataTable();
			SqlCommand command = BuildCommand(string.Format("SELECT * FROM {0} WHERE [Guid] = @Guid", dataSourceName), CommandType.Text);
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
			SqlParameter parameter = new SqlParameter("@Guid", entityGuid);

			command.Parameters.Add(parameter);

			sqlDataAdapter.SelectCommand = command;

			try
			{
				sqlDataAdapter.Fill(dataTable);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (!InTransaction)
					CloseConnection();
			}

			if (dataTable.Rows.Count == 0)
				return null;

			return dataTable.Rows[0];
		}
		#endregion

		#region------------ Select Block ---------------
		public override DataTable SelectDataTable(string statement, params object[] parameters)
		{
			DataTable dataTable = new DataTable();
			SqlDataAdapter dataAdapter = new SqlDataAdapter();
			SqlCommand command = BuildCommand(statement, CommandType.Text, parameters);

			dataAdapter.SelectCommand = command;

			try
			{
				dataAdapter.Fill(dataTable);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (!InTransaction)
					CloseConnection();
			}

			return dataTable;
		}

		public override DataTable GetPagedDataTable(string fieldsList, string selectSource, string whereClause, string orderClause, int pageNo, int pageSize, out int resultCount, params object[] parametersInfo)
		{
			string finalOrderClause = orderClause.Substring(orderClause.IndexOf(".") + 1);

			StringBuilder sqlStatement = new StringBuilder();

			sqlStatement.Append("SELECT COUNT(*) AS [__RowCount] FROM ");
			sqlStatement.Append(selectSource);
			sqlStatement.Append(" WHERE ");
			sqlStatement.Append(whereClause);
			sqlStatement.Append(";");
			sqlStatement.AppendLine();

			if (pageNo == 1)
			{
				sqlStatement.Append("SELECT TOP ");
				sqlStatement.Append(pageSize);
				sqlStatement.Append(fieldsList);
				sqlStatement.Append(" FROM ");
				sqlStatement.Append(selectSource);
				sqlStatement.Append(" WHERE ");
				sqlStatement.Append(whereClause);
				sqlStatement.Append(" ORDER BY ");
				sqlStatement.Append(orderClause);
			}
			else
			{
				string tempWithCaption = GetCTEName();
				int pageStartRowNumber = ((pageNo - 1) * pageSize) + 1;
				sqlStatement.Append("WITH ");
				sqlStatement.Append(tempWithCaption);
				sqlStatement.Append(" AS (SELECT ROW_NUMBER() OVER (ORDER BY ");
				sqlStatement.Append(orderClause);
				sqlStatement.Append(") AS [__RowNumber], ");
				sqlStatement.Append(fieldsList);
				sqlStatement.Append(" FROM ");
				sqlStatement.Append(selectSource);
				sqlStatement.Append(" WHERE ");
				sqlStatement.Append(whereClause);
				sqlStatement.Append(") SELECT * FROM ");
				sqlStatement.Append(tempWithCaption);
				sqlStatement.Append(" WHERE [__RowNumber] BETWEEN ");
				sqlStatement.Append(pageStartRowNumber);
				sqlStatement.Append(" AND ");
				sqlStatement.Append(pageStartRowNumber + pageSize - 1);
				sqlStatement.Append(" ORDER BY ");
				sqlStatement.Append(finalOrderClause);
			}

			DataSet dstResult = this.SelectDataSet(sqlStatement.ToString(), parametersInfo);
			resultCount = Int32.Parse(dstResult.Tables[0].Rows[0]["__RowCount"].ToString());
			return dstResult.Tables[1];
		}

		public override DataSet SelectDataSet(string statement, params object[] parameters)
		{
			DataSet dataSet = new DataSet();
			SqlDataAdapter dataAdapter = new SqlDataAdapter();
			SqlCommand command = BuildCommand(statement, CommandType.Text, parameters);

			dataAdapter.SelectCommand = command;

			try
			{
				dataAdapter.Fill(dataSet);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (!InTransaction)
					CloseConnection();
			}

			return dataSet;
		}

		public override DataTable SelectSPDataTable(string storedProcedureName, params object[] parameters)
		{
			DataTable dataTable = new DataTable();
			SqlDataAdapter dataAdapter = new SqlDataAdapter();
			SqlCommand command = BuildCommand(storedProcedureName, CommandType.StoredProcedure, parameters);

			dataAdapter.SelectCommand = command;

			try
			{
				dataAdapter.Fill(dataTable);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (!InTransaction)
					CloseConnection();
			}

			return dataTable;
		}

		public override DataSet SelectSPDataSet(string storedProcedureName, params object[] parameters)
		{
			DataSet dataSet = new DataSet();
			SqlDataAdapter dataAdapter = new SqlDataAdapter();
			SqlCommand command = BuildCommand(storedProcedureName, CommandType.StoredProcedure, parameters);

			dataAdapter.SelectCommand = command;

			try
			{
				dataAdapter.Fill(dataSet);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (!InTransaction)
					CloseConnection();
			}

			return dataSet;
		}
		#endregion

		#region------------ Execute Command ------------
		public override bool ExecuteCommand(string sqlStatement, params object[] parameters)
		{
			SqlCommand command = BuildCommand(sqlStatement, CommandType.Text, parameters);

			try
			{
				command.ExecuteNonQuery();
			}
			catch
			{
				throw;
			}
			finally
			{
				if (!InTransaction)
					CloseConnection();
			}

			return true;
		}

		public override bool ExecuteSPCommand(string actionSPName, params object[] parameters)
		{
			SqlCommand command = BuildCommand(actionSPName, CommandType.StoredProcedure, parameters);

			try
			{
				command.ExecuteNonQuery();
			}
			catch
			{
				throw;
			}
			finally
			{
				if (!InTransaction)
					CloseConnection();
			}

			return true;
		}
		#endregion

		#region------------- Build Command -------------
		private SqlCommand BuildCommand(string commandText, CommandType commandType, List<SqlParameter> sqlParameterList)
		{
			SqlCommand sqlCommand = new SqlCommand(commandText);

			sqlCommand.CommandType = commandType;

			if (QueryTimeout != -1)
				sqlCommand.CommandTimeout = QueryTimeout;

			foreach (SqlParameter sqlParameter in sqlParameterList)
				sqlCommand.Parameters.Add(sqlParameter);

			if (!InTransaction)
				OpenConnection();

			sqlCommand.Connection = this.sqlConnection;

			if (InTransaction)
				sqlCommand.Transaction = this.transaction;

			return sqlCommand;
		}

		private SqlCommand BuildCommand(string commandText, CommandType commandType, params object[] parametersInfo)
		{
			SqlParameter sqlParameter;
			List<SqlParameter> sqlParameterList = new List<SqlParameter>();

			if (parametersInfo != null)
			{
				for (int i = 0; i < parametersInfo.Length; i = i + 2)
				{
					sqlParameter = new SqlParameter(parametersInfo[i].ToString(), CheckFormatOfParameter(parametersInfo[i + 1]));
					sqlParameter.Direction = ParameterDirection.Input;

					sqlParameterList.Add(sqlParameter);
				}
			}

			return BuildCommand(commandText, commandType, sqlParameterList);
		}
		#endregion

		public string GetSPName(string tableName, string actionName)
		{
			return string.Format("{0}{1}_{2}", DBPrefix, tableName, actionName);
		}

		private object CheckFormatOfParameter(object fieldValue)
		{
			if (fieldValue == null || fieldValue == DBNull.Value)
				return DBNull.Value;

			if (fieldValue.GetType() == typeof(String))
			{
				string inputString = GetAntiScript(fieldValue).ToString().Replace(Convert.ToChar(1610), Convert.ToChar(1740)); // Convert 'ي' to 'ی'
				inputString = fieldValue.ToString().Replace(Convert.ToChar(1603), Convert.ToChar(1705)); // Convert 'ك' to 'ک'
				return inputString.Replace(Convert.ToChar(223), Convert.ToChar(152)).Replace(Convert.ToChar(236), Convert.ToChar(237));
			}
			else if (fieldValue.GetType() == typeof(DateTime))
			{
				DateTime date = DateTime.Parse(fieldValue.ToString());
				if (date == DateTime.MinValue)
					return DBNull.Value;
				else
					return string.Format("{0}-{1}-{2} {3}:{4}:{5}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
			}
			else
				return fieldValue;
		}

		private object GetAntiScript(object fieldValue)
		{
			string value = fieldValue.ToString();
			string antiScript = value.ToLower().Replace("<script", "<sc?ript");
			if (value.Length != antiScript.Length)
				fieldValue = antiScript;

			return fieldValue;
		}

		public static string GetCTEName()
		{
			int length = 8;
			bool onlyLowerCase = true;
			StringBuilder builder = new StringBuilder();
			Random rnd = new Random();
			int start = onlyLowerCase ? 97 : 48;

			lock (rnd)
			{
				for (int i = 0; i < length; i++)
					builder.Append((char)rnd.Next(start, 122));
			}

			return builder.ToString();
		}
	}
}
