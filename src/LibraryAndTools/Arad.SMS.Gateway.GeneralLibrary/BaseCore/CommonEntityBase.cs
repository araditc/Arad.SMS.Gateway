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

namespace Arad.SMS.Gateway.GeneralLibrary.BaseCore
{
	public abstract class CommonEntityBase : EntityBase
	{
		private Dictionary<string, TableFieldInfo> fieldList;
		private Dictionary<string, TableFieldInfo> readOnlyFieldList;
		private Dictionary<string, TableFieldInfo> loggingFieldList;
		private string errorMessage;
		private bool hasError;
		private string ip;
		private string browser;

		public bool HasError
		{
			get { return hasError; }
			set { hasError = value; }
		}

		public string ErrorMessage
		{
			get { return errorMessage + "<br/>"; }
			set { errorMessage = value; }
		}

		public string IP
		{
			get { return ip; }
			set { ip = value; }
		}

		public string Browser
		{
			get { return browser; }
			set { browser = value; }
		}

		public Guid PrimaryKey
		{
			get;
			internal protected set;
		}

		public bool HasReadOnlyFields
		{
			get
			{
				return readOnlyFieldList.Count != 0;
			}
		}

		public bool HasLoggingFields
		{
			get
			{
				return loggingFieldList.Count != 0;
			}
		}

		public object this[string key]
		{
			get
			{
				try
				{
					return fieldList[key].FieldValue;
				}
				catch (Exception ex)
				{
					throw new Exception(string.Format("Field '{0}' Not Found: {1}", key, ex.Message));
				}
			}
			set
			{
				try
				{
					fieldList[key].FieldValue = value;
				}
				catch (Exception ex)
				{
					throw new Exception(string.Format("Field '{0}' Not Found: {1}", key, ex.Message));
				}
			}
		}

		#region Constractor
		protected CommonEntityBase(string tableName)
			: base(tableName)
		{
			this.fieldList = new Dictionary<string, TableFieldInfo>();
			this.readOnlyFieldList = new Dictionary<string, TableFieldInfo>();
			this.loggingFieldList = new Dictionary<string, TableFieldInfo>();
			this.HasError = false;
			this.ErrorMessage = string.Empty;
		}
		#endregion

		#region Add Field
		protected virtual bool AddField(string name, SqlDbType type)
		{
			return AddField(name, type, -1, false, false);
		}
		protected virtual bool AddField(string name, SqlDbType type, short size)
		{
			return AddField(name, type, size, false, false);
		}
		protected virtual bool AddField(string name, SqlDbType type, short size, bool logChanges)
		{
			return AddField(name, type, size, false, logChanges);
		}
		protected virtual bool AddField(string name, SqlDbType type, bool logChanges)
		{
			return AddField(name, type, -1, false, logChanges);
		}
		protected internal bool AddField(string name, SqlDbType type, short size, bool readOnly, bool logChanges)
		{
			TableFieldInfo newField = new TableFieldInfo(name, type, size, readOnly, logChanges, DBNull.Value);
			fieldList.Add(name, newField);
			if (readOnly)
				readOnlyFieldList.Add(name, newField.Clone());

			if (logChanges)
				loggingFieldList.Add(name, newField.Clone());

			return true;
		}

		protected virtual bool AddReadOnlyField(string name)
		{
			return AddField(name, SqlDbType.Variant, -1, true, false);
		}
		protected virtual bool AddReadOnlyField(string name, SqlDbType type)
		{
			return AddField(name, type, -1, true, false);
		}
		#endregion

		#region Procedure Parameters Methods
		public List<SqlParameter> GetProcedureParameters()
		{
			SqlParameter parameter;
			List<SqlParameter> sqlParameters = new List<SqlParameter>();

			foreach (TableFieldInfo field in fieldList.Values)
				if (!field.ReadOnly)
				{
					if (field.FieldType != SqlDbType.Image)
					{
						if (field.FieldSize == -1)
							parameter = new SqlParameter("@" + GetSqlVariableName(field.FieldName), field.FieldType);
						else
							parameter = new SqlParameter("@" + GetSqlVariableName(field.FieldName), field.FieldType, field.FieldSize);

						parameter.Direction = ParameterDirection.Input;
						parameter.Value = CheckFormatOfParameter(field.FieldValue);

						sqlParameters.Add(parameter);
					}
				}

			return sqlParameters;
		}
		private string GetProcedureParameters(SPParameterGenerationType generationType)
		{
			string returnValue = string.Empty;
			string fieldParameter;

			switch (generationType)
			{
				case SPParameterGenerationType.ForParameterList:
					foreach (TableFieldInfo field in fieldList.Values)
						if (!field.ReadOnly && field.FieldType != SqlDbType.Image)
						{
							fieldParameter = "@{0} {1}{2},\n";
							if (field.FieldSize == -1)
								fieldParameter = string.Format(fieldParameter, GetSqlVariableName(field.FieldName), field.FieldType.ToString().ToUpper(), string.Empty);
							else
								fieldParameter = string.Format(fieldParameter, GetSqlVariableName(field.FieldName), field.FieldType.ToString().ToUpper(), "(" + (field.FieldSize != short.MaxValue ? field.FieldSize.ToString() : "MAX") + ")");

							returnValue += fieldParameter;
						}
					return returnValue.Substring(0, returnValue.Length - 2);

				case SPParameterGenerationType.ForFieldList:
					foreach (TableFieldInfo field in fieldList.Values)
						if (!field.ReadOnly && field.FieldType != SqlDbType.Image)
						{
							fieldParameter = "[{0}], ";
							fieldParameter = string.Format(fieldParameter, field.FieldName);

							returnValue += fieldParameter;
						}
					return returnValue.Substring(0, returnValue.Length - 2);

				case SPParameterGenerationType.ForInsert:
					foreach (TableFieldInfo field in fieldList.Values)
						if (!field.ReadOnly && field.FieldType != SqlDbType.Image)
						{
							fieldParameter = "@{0}, ";
							fieldParameter = string.Format(fieldParameter, GetSqlVariableName(field.FieldName));

							returnValue += fieldParameter;
						}
					return returnValue.Substring(0, returnValue.Length - 2);

				case SPParameterGenerationType.ForUpdate:
					foreach (TableFieldInfo field in fieldList.Values)
						if (!field.ReadOnly && field.FieldType != SqlDbType.Image)
						{
							fieldParameter = "\t[{0}] = @{1},\n";
							fieldParameter = string.Format(fieldParameter, field.FieldName, GetSqlVariableName(field.FieldName));

							returnValue += fieldParameter;
						}
					return returnValue.Substring(0, returnValue.Length - 2);

				case SPParameterGenerationType.ForCraeteTable:
					foreach (TableFieldInfo field in fieldList.Values)
						if (!field.ReadOnly)
						{
							if (field.FieldType != SqlDbType.Image)
							{
								fieldParameter = "\t[{0}] [{1}] {2} NULL,\n";
								fieldParameter = string.Format(fieldParameter, field.FieldName, field.FieldType.ToString().ToUpper(), field.FieldSize == -1 ? string.Empty : "(" + (field.FieldSize != short.MaxValue ? field.FieldSize.ToString() : "MAX") + ")");

								returnValue += fieldParameter;
							}
							else
							{
								fieldParameter = "\t[{0}] [{1}] {2} NULL,\n" +
									"\t[{0}_filename] [nvarchar] (256) NULL,\n";
								fieldParameter = string.Format(fieldParameter, field.FieldName, field.FieldType.ToString().ToUpper(), field.FieldSize == -1 ? string.Empty : "(" + field.FieldSize + ")");

								returnValue += fieldParameter;
							}
						}
						else if (field.FieldName == "IsDeleted")
						{
							fieldParameter = "\t[{0}] [{1}] {2} NULL,\n";
							fieldParameter = string.Format(fieldParameter, field.FieldName, field.FieldType.ToString().ToUpper(), field.FieldSize == -1 ? string.Empty : "(" + (field.FieldSize != short.MaxValue ? field.FieldSize.ToString() : "MAX") + ")");

							returnValue += fieldParameter;
						}
					return returnValue.Substring(0, returnValue.Length - 2);
				default:
					return string.Empty;
			}
		}
		private string GetSqlVariableName(string field)
		{
			return field.Replace(" ", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty).Replace(".", string.Empty).Replace("؟", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty).Replace("*", string.Empty).Replace("+", string.Empty).Replace(",", string.Empty).Replace("،", string.Empty).Replace("\\", string.Empty).Replace(">", string.Empty).Replace("<", string.Empty).Replace("!", string.Empty).Replace("%", string.Empty);
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
				return string.Format("{0}-{1}-{2} {3}:{4}:{5}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
			}
			else
				return fieldValue;
		}

		private object GetAntiScript(object fieldValue)
		{
			if (fieldValue == null)
				return fieldValue;

			if (fieldValue.GetType() == typeof(String))
			{
				string value = fieldValue.ToString();
				string antiScript = value.ToLower().Replace("<script", "<sc?ript");
				if (value.Length != antiScript.Length)
					fieldValue = antiScript;
			}

			return fieldValue;
		}
		#endregion

		#region SQL Script generator
		public string GetProcedureScript()
		{
			return
				GetDropProceduresScript() +
				GetCreateProceduresScript();
		}
		public string GetDropProceduresScript()
		{
			return
				GetInsertProcedureDrop() + "\nGO\n" +
				GetUpdateProcedureDrop() + "\nGO\n" +
				GetDeleteProcedureDrop() + "\nGO\n" +
				GetSetDataProcedureDrop() + "\nGO\n";
		}
		public string GetCreateProceduresScript()
		{
			return
				GetInsertProcedureCreate() + "\nGO\n" +
				GetUpdateProcedureCreate() + "\nGO\n" +
				GetDeleteProcedureCreate() + "\nGO\n" +
				GetSetDataProcedureCreate() + "\nGO\n";
		}
		public string GetCompleteScripts()
		{
			return
				GetTableScript() + "\n\n\n" +
				GetProcedureScript();
		}
		private string GetTableScript()
		{
			string script = "CREATE TABLE [{0}] (\n" +
				"\t[Guid] UNIQUEIDENTIFIER NOT NULL,\n" +
				"\t[ID] BIGINT IDENTITY(1,1) NOT NULL,\n" +
				"{1}\n" +
				") ON [PRIMARY]\n" +
				"GO\n\n" +
				"ALTER TABLE {0} ADD CONSTRAINT [PK_{0}] PRIMARY KEY NONCLUSTERED ([Guid]) ON [PRIMARY]\n" +
				"GO\n" +
				"CREATE CLUSTERED INDEX IX_{0}_ID ON dbo.{0}([ID])\n" +
				"GO";
			script = string.Format(script,
				TableName,
				GetProcedureParameters(SPParameterGenerationType.ForCraeteTable));
			return script;
		}

		#region Insert/Update/Delete SQL statement generation
		protected string GetInsertStatement()
		{
			string sqlStatement = string.Empty;

			sqlStatement += string.Format("INSERT INTO [{0}]\n" +
				 "\t([Guid], {1})\n" +
				 "VALUES\n" +
				 "\t(@Guid, {2})\n",
				 TableName,
				 GetProcedureParameters(SPParameterGenerationType.ForFieldList),
				 GetProcedureParameters(SPParameterGenerationType.ForInsert));

			return sqlStatement;
		}
		protected string GetUpdateStatement()
		{
			return string.Format("UPDATE [{0}] SET\n" +
				"{1}\n" +
				"WHERE\n" +
				"\t[Guid] = @Guid\n", TableName, GetProcedureParameters(SPParameterGenerationType.ForUpdate)
				);
		}
		protected string GetDeleteStatement()
		{
			if (fieldList.ContainsKey("IsDeleted"))
				return string.Format("UPDATE [{0}] SET\n" +
				"\t[IsDeleted] = 1\n" +
				"WHERE\n" +
				"\t[Guid] = @Guid\n", TableName);
			else
				return string.Format("DELETE FROM\n" +
					"\t[{0}]\n" +
					"WHERE\n" +
					"\t[Guid] = @Guid\n", TableName);
		}
		#endregion
		#region Insert/Update/Delete/SetData Stored Procedure creation statement generation
		public string GetInsertProcedureCreate()
		{
			string storedProcedure = "CREATE PROCEDURE [{0}]\n" +
				"(\n" +
				"@Guid UNIQUEIDENTIFIER,\n" +
				"{1}\n" +
				")\n" +
				"AS\n" +
				GetInsertStatement();

			return string.Format(storedProcedure,
				GetSPName("Insert"),
				GetProcedureParameters(SPParameterGenerationType.ForParameterList));
		}
		public string GetUpdateProcedureCreate()
		{
			string storedProcedure = "CREATE PROCEDURE [{0}]\n" +
				"(\n" +
				"@Guid UNIQUEIDENTIFIER,\n" +
				"{1}\n" +
				")\n" +
				"AS\n" +
				GetUpdateStatement();

			return string.Format(storedProcedure, GetSPName("Update"), GetProcedureParameters(SPParameterGenerationType.ForParameterList));
		}
		public string GetDeleteProcedureCreate()
		{
			string storedProcedure = "CREATE PROCEDURE [{0}]\n" +
				"(\n" +
				"@Guid UNIQUEIDENTIFIER\n" +
				")\n" +
				"AS\n" +
				GetDeleteStatement();

			return string.Format(storedProcedure, GetSPName("Delete"));
		}
		public string GetSetDataProcedureCreate()
		{
			bool imageFieldFound = false;
			string storedProcedure = string.Format("CREATE PROCEDURE {0} (@Guid UNIQUEIDENTIFIER, @FieldName nvarchar(64), @Filename nvarchar(256), @Data image) AS\n", GetSPName("SetData"));

			foreach (TableFieldInfo field in fieldList.Values)
				if (!field.ReadOnly && field.FieldType == SqlDbType.Image)
				{
					imageFieldFound = true;

					storedProcedure += string.Format("\nIF @FieldName = N'{0}'\n\tUPDATE [{1}] SET\n\t\t[{0}] = @Data,\n\t\t[{0}_filename] = @Filename\n\tWHERE [Guid] = @Guid", field.FieldName, TableName);
				}

			if (!imageFieldFound)
				storedProcedure = string.Empty;

			return storedProcedure;
		}
		#endregion
		#region Insert/Update/Delete/SetData Stored Procedure drop statement generation
		public string GetUpdateProcedureDrop()
		{
			return string.Format("if exists (select * from dbo.sysobjects where id = object_id(N'[{0}]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)\nDROP PROCEDURE [{0}]\n", GetSPName("Update"));
		}
		public string GetInsertProcedureDrop()
		{
			return string.Format("if exists (select * from dbo.sysobjects where id = object_id(N'[{0}]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)\nDROP PROCEDURE [{0}]\n", GetSPName("Insert"));
		}
		public string GetDeleteProcedureDrop()
		{
			return string.Format("if exists (select * from dbo.sysobjects where id = object_id(N'[{0}]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)\nDROP PROCEDURE [{0}]\n", GetSPName("Delete"));
		}
		public string GetSetDataProcedureDrop()
		{
			bool imageFieldFound = false;
			string storedProcedure = string.Format("if exists (select * from dbo.sysobjects where id = object_id(N'[{0}]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)\nDROP PROCEDURE [{0}]\n", GetSPName("SetData"));

			foreach (TableFieldInfo field in fieldList.Values)
				if (!field.ReadOnly && field.FieldType == SqlDbType.Image)
					imageFieldFound = true;

			if (!imageFieldFound)
				storedProcedure = string.Empty;

			return storedProcedure;
		}
		#endregion
		#endregion

		public void ClearErrorMessage()
		{
			this.HasError = false;
			this.ErrorMessage = string.Empty;
		}

		public bool Load(DataRow loadedDataRow)
		{
			try
			{
				foreach (TableFieldInfo field in fieldList.Values)
					if (field.FieldType != SqlDbType.Image)
						field.SetFieldValueByConversion(Helper.GetString(loadedDataRow[field.FieldName]));

				if (loadedDataRow.Table.Columns.Contains("Guid"))
					PrimaryKey = Helper.GetGuid(loadedDataRow["Guid"]);
				else
					PrimaryKey = Guid.Empty;

				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}
	}
}
