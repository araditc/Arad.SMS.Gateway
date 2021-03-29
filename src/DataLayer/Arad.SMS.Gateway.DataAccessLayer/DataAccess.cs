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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;

namespace Arad.SMS.Gateway.DataAccessLayer
{
	public class DataAccess
	{
		private string tableName;
		private int timeout;

		private static SqlConnection connection;
		private SqlCommand command;
		private SqlDataAdapter dataAdapter;
		private SqlParameter parameter;

		private static SqlTransaction transaction;

		protected DataAccess(string tableName)
		{
			this.tableName = tableName;
			this.timeout = 20;
		}

		static DataAccess()
		{
			IntialazeConnection();
		}

		#region------------- General Method ------------
		protected Guid Insert(params object[] parameters)
		{
			Guid guid = Guid.NewGuid();

			command = new SqlCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = string.Format("{0}_Insert", tableName);
			command.Transaction = transaction;
			command.Connection = connection;
			command.CommandTimeout = timeout;

			parameter = new SqlParameter("@Guid", guid);
			command.Parameters.Add(parameter);

			for (int paramCounter = 0; paramCounter < parameters.Length; paramCounter += 2)
			{
				parameter = new SqlParameter(parameters[paramCounter].ToString(), CheckUnicode(parameters[paramCounter + 1]));
				command.Parameters.Add(parameter);
			}

			try
			{
				OpenConnection();
				command.ExecuteNonQuery();
			}
			catch
			{
				CheckTransaction(false);
				guid = Guid.Empty;
			}

			return guid;
		}

		protected bool Update(params object[] parameters)
		{
			command = new SqlCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = string.Format("{0}_Update", tableName);
			command.Transaction = transaction;
			command.Connection = connection;
			command.CommandTimeout = timeout;

			for (int paramCounter = 0; paramCounter < parameters.Length; paramCounter += 2)
			{
				parameter = new SqlParameter(parameters[paramCounter].ToString(), CheckUnicode(parameters[paramCounter + 1]));
				command.Parameters.Add(parameter);
			}

			try
			{
				OpenConnection();
				command.ExecuteNonQuery();
			}
			catch
			{
				CheckTransaction(false);
				return false;
			}

			return true;
		}

		protected bool Delete(Guid guid)
		{
			command = new SqlCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = string.Format("{0}_Delete", tableName);
			command.Transaction = transaction;
			command.Connection = connection;
			command.CommandTimeout = timeout;

			parameter = new SqlParameter("@Guid", guid);
			command.Parameters.Add(parameter);

			try
			{
				OpenConnection();
				command.ExecuteNonQuery();
			}
			catch
			{
				CheckTransaction(false);
				return false;
			}

			return true;
		}

		protected DataRow Load(Guid guid)
		{
			DataTable dataTable = new DataTable();
			command = new SqlCommand();
			dataAdapter = new SqlDataAdapter();

			command.CommandType = CommandType.Text;
			command.CommandText = string.Format("SELECT * FROM {0} WHERE [Guid] = @Guid", tableName);
			command.Transaction = transaction;
			command.Connection = connection;
			command.CommandTimeout = timeout;

			parameter = new SqlParameter("@Guid", guid);
			command.Parameters.Add(parameter);

			dataAdapter.SelectCommand = command;

			try
			{
				OpenConnection();
				dataAdapter.Fill(dataTable);
			}
			catch (Exception ex)
			{
				CheckTransaction(false);
				throw ex;
			}

			if (dataTable.Rows.Count == 0)
				return null;

			return dataTable.Rows[0];
		}

		#endregion

		#region------------- Select Block ------------------
		protected DataTable SelectDataTable(string statement, params object[] parameters)
		{
			DataTable dataTable = new DataTable();
			dataAdapter = new SqlDataAdapter();
			command = new SqlCommand();

			command.CommandType = CommandType.Text;
			command.CommandText = statement;
			command.Transaction = transaction;
			command.Connection = connection;
			command.CommandTimeout = timeout;

			for (int paramCounter = 0; paramCounter < parameters.Length; paramCounter += 2)
			{
				parameter = new SqlParameter(parameters[paramCounter].ToString(), CheckUnicode(parameters[paramCounter + 1]));
				command.Parameters.Add(parameter);
			}

			dataAdapter.SelectCommand = command;

			try
			{
				OpenConnection();
				dataAdapter.Fill(dataTable);
			}
			catch (Exception ex)
			{
				CheckTransaction(false);
				throw ex;
			}

			return dataTable;
		}

		protected DataSet SelectDataSet(string statement, params object[] parameters)
		{
			DataSet dataSet = new DataSet();
			dataAdapter = new SqlDataAdapter();
			command = new SqlCommand();

			command.CommandType = CommandType.Text;
			command.CommandText = statement;
			command.Transaction = transaction;
			command.Connection = connection;
			command.CommandTimeout = timeout;

			for (int paramCounter = 0; paramCounter < parameters.Length; paramCounter += 2)
			{
				parameter = new SqlParameter(parameters[paramCounter].ToString(), CheckUnicode(parameters[paramCounter + 1]));
				command.Parameters.Add(parameter);
			}

			dataAdapter.SelectCommand = command;

			try
			{
				OpenConnection();
				dataAdapter.Fill(dataSet);
			}
			catch (Exception ex)
			{
				CheckTransaction(false);
				throw ex;
			}

			return dataSet;
		}

		protected DataTable SelectSPDataTable(string selectSPName, params object[] parameters)
		{
			DataTable dataTable = new DataTable();
			dataAdapter = new SqlDataAdapter();
			command = new SqlCommand();

			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = tableName + "_" + selectSPName;
			command.Transaction = transaction;
			command.Connection = connection;
			command.CommandTimeout = timeout;

			for (int paramCounter = 0; paramCounter < parameters.Length; paramCounter += 2)
			{
				parameter = new SqlParameter(parameters[paramCounter].ToString(), CheckUnicode(parameters[paramCounter + 1]));
				command.Parameters.Add(parameter);
			}

			dataAdapter.SelectCommand = command;

			try
			{
				OpenConnection();
				dataAdapter.Fill(dataTable);
			}
			catch (Exception ex)
			{
				CheckTransaction(false);
				throw ex;
			}

			return dataTable;
		}

		protected DataSet SelectSPDataSet(string selectSPName, params object[] parameters)
		{
			DataSet dataSet = new DataSet();
			dataAdapter = new SqlDataAdapter();
			command = new SqlCommand();

			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = tableName + "_" + selectSPName;
			command.Transaction = transaction;
			command.Connection = connection;
			command.CommandTimeout = timeout;

			for (int paramCounter = 0; paramCounter < parameters.Length; paramCounter += 2)
			{
				parameter = new SqlParameter(parameters[paramCounter].ToString(), CheckUnicode(parameters[paramCounter + 1]));
				command.Parameters.Add(parameter);
			}

			dataAdapter.SelectCommand = command;

			try
			{
				OpenConnection();
				dataAdapter.Fill(dataSet);
			}
			catch (Exception ex)
			{
				CheckTransaction(false);
				throw ex;
			}

			return dataSet;
		}
		#endregion

		#region------------- Execute Command ------------
		protected bool ExecuteCommand(string actionName, params object[] parameters)
		{
			Guid guid = Guid.NewGuid();

			command = new SqlCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = tableName + "_" + actionName;
			command.Transaction = transaction;
			command.Connection = connection;
			command.CommandTimeout = timeout;

			for (int paramCounter = 0; paramCounter < parameters.Length; paramCounter += 2)
			{
				parameter = new SqlParameter(parameters[paramCounter].ToString(), CheckUnicode(parameters[paramCounter + 1]));
				command.Parameters.Add(parameter);
			}

			try
			{
				OpenConnection();
				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				CheckTransaction(false);
				throw ex;
			}

			return true;
		}

		protected bool ExecuteSPCommand(string actionName, params object[] parameters)
		{
			Guid guid = Guid.NewGuid();

			command = new SqlCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = tableName + "_" + actionName;
			command.Transaction = transaction;
			command.Connection = connection;
			command.CommandTimeout = timeout;

			for (int paramCounter = 0; paramCounter < parameters.Length; paramCounter += 2)
			{
				parameter = new SqlParameter(parameters[paramCounter].ToString(), CheckUnicode(parameters[paramCounter + 1]));
				command.Parameters.Add(parameter);
			}

			try
			{
				OpenConnection();
				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				CheckTransaction(false);
				throw ex;
			}

			return true;
		}
		#endregion

		protected void BeginTransaction()
		{
			OpenConnection();
			transaction = connection.BeginTransaction();
		}

		protected void CommitTransaction()
		{
			CheckTransaction(true);
		}

		protected void RollbackTransaction()
		{
			CheckTransaction(false);
		}

		private void CheckTransaction(bool succeed)
		{
			if (transaction != null)
			{
				if (succeed)
					transaction.Commit();
				else
					transaction.Rollback();

				transaction = null;
			}
		}

		private string CheckUnicode(object inputObject)
		{
			if (inputObject == null)
				return DBNull.Value.ToString();

			decimal[] persianDigitsUnicode = { 1632, 1633, 1634, 1635, 1636, 1637, 1638, 1639, 1640, 1641 };
			decimal[] arabicDigitsUnicode = { 1776, 1777, 1778, 1779, 1780, 1781, 1782, 1783, 1784, 1785 };
			decimal[] englishDigitsUnicode = { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57 };

			string inputString = inputObject.ToString().Replace(Convert.ToChar(1610), Convert.ToChar(1740)); // Convert 'ي' to 'ی'
			inputString = inputObject.ToString().Replace(Convert.ToChar(1603), Convert.ToChar(1705)); // Convert 'ك' to 'ک'
			inputString= inputString.Replace(Convert.ToChar(223), Convert.ToChar(152)).Replace(Convert.ToChar(236), Convert.ToChar(237));

			//convert persian/arabic number with latin number
			for (int x = 0; x <= 9; x++)
			{
				inputString = inputString.ToCharArray().Contains((char)persianDigitsUnicode[x]) ? inputString.Replace((char)persianDigitsUnicode[x], (char)englishDigitsUnicode[x]) : inputString;
				inputString = inputString.ToCharArray().Contains((char)arabicDigitsUnicode[x]) ? inputString.Replace((char)arabicDigitsUnicode[x], (char)englishDigitsUnicode[x]) : inputString;
			}

			return inputString;
		}

		private void OpenConnection()
		{
			if (connection == null)
			{
				IntialazeConnection();
				connection.Open();
			}
			else if (connection.State == ConnectionState.Closed)
				connection.Open();
		}

		private static void IntialazeConnection()
		{
			connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ToString());
		}
	}
}
