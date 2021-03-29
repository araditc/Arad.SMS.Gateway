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
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Business
{
	public class UserField : BusinessEntityBase
	{
		public UserField(DataAccessBase dataAccessProvider = null)
			: base(TableNames.UserFields.ToString(), dataAccessProvider) { }

		public DataTable GetPhoneBookDateField(Guid phoneBookGuid)
		{
			return FetchDataTable("SELECT * FROM [UserFields] WHERE [PhoneBookGuid] = @PhoneBookGuid", "@PhoneBookGuid", phoneBookGuid);
		}
			public DataTable GetPhoneBookField(Guid phoneBookGuid)
		{
			DataTable dataTableTempField = new DataTable();
			dataTableTempField.Columns.Add("Guid", typeof(Guid));
			dataTableTempField.Columns.Add("FieldID", typeof(int));
			dataTableTempField.Columns.Add("Title", typeof(string));
			dataTableTempField.Columns.Add("Type", typeof(int));
			dataTableTempField.Columns.Add("TypeName", typeof(string));
			dataTableTempField.Columns.Add("Deletable", typeof(bool));
			bool allowDelete;

			DataSet dataSetPhoneBookFieldInfo = base.FetchSPDataSet("GetPhoneBookField", "@PhoneBookGuid", phoneBookGuid);

			DataTable dataTablePhoneBookField = dataSetPhoneBookFieldInfo.Tables[0];
			DataTable dataTableCountValueField = dataSetPhoneBookFieldInfo.Tables[1];

			if (dataTablePhoneBookField.Rows.Count > 0)
			{
				for (int counterField = 0; counterField < 20; counterField++)
				{
					if (dataTablePhoneBookField.Rows[0]["Field" + (counterField + 1)].ToString().Length > 0)
					{
						allowDelete = Helper.GetInt(dataTableCountValueField.Rows[0]["CountField" + (counterField + 1)]) > 0 ? false : true;
						dataTableTempField.Rows.Add(dataTablePhoneBookField.Rows[0]["Guid"],
																				counterField + 1,
																				dataTablePhoneBookField.Rows[0]["Field" + (counterField + 1)].ToString(),
																				Helper.GetInt(dataTablePhoneBookField.Rows[0]["FieldType" + (counterField + 1)]),
																				Language.GetString(((Business.UserFieldTypes)(Helper.GetInt(dataTablePhoneBookField.Rows[0]["FieldType" + (counterField + 1)]))).ToString()),
																				allowDelete);
					}
				}
			}
			return dataTableTempField;
		}

		public bool CheckFieldNameValid(int index, string title, Guid phoneBookGuid)
		{
			DataTable dataTable = new DataTable();
			string where = string.Empty;
			for (int counter = 1; counter <= 20; counter++)
				if (counter != index)
				{
					if (where != string.Empty)
						where += " OR";
					where += "[Field" + counter + "]=@Title";
				}
			dataTable = base.FetchDataTable("SELECT COUNT(*) AS [Count] FROM [UserFields] WHERE (" + where + ") AND [PhoneBookGuid]=@PhoneBookGuid", "@Title", title, "@PhoneBookGuid", phoneBookGuid);
			return Helper.GetInt(dataTable.Rows[0]["Count"]) == 0;
		}

		public bool UpdateField(int index, string fieldValue, int fieldType, Guid phoneBookGuid)
		{
			if (fieldValue.Trim() == string.Empty)
				throw new Exception("CompleteUserFieldTitle");
			if (CheckFieldNameValid(index, fieldValue, phoneBookGuid))
			{
				return base.ExecuteSPCommand("UpdateField", "@PhoneBookGuid", phoneBookGuid,
																										"@Index", index,
																										"@FieldValue", fieldValue,
																										"@FieldType", fieldType);
			}
			else
				throw new Exception("DuplicateFieldName");
		}

		public bool DeleteField(int index, Guid phoneBookGuid)
		{
			return base.ExecuteSPCommand("DeleteField", "@PhoneBookGuid", phoneBookGuid, "@Index", index);
		}

		public bool InsertField(string fieldValue, int fieldType, Guid phoneBookGuid)
		{
			DataTable dataTablePhoneBookFields = GetPhoneBookField(phoneBookGuid);
			if (fieldValue.Trim() == string.Empty)
				throw new Exception("CompleteUserFieldTitle");
			int index = 0;
			if (dataTablePhoneBookFields.Rows.Count == 20)
				throw new Exception("ErrorUserFieldRange");
			if (dataTablePhoneBookFields.Rows.Count > 0)
			{
				for (int counterField = 0; counterField < dataTablePhoneBookFields.Rows.Count; counterField++)
				{
					if (Helper.GetInt(dataTablePhoneBookFields.Rows[counterField]["FieldID"]) != (counterField + 1))
					{
						index = (counterField + 1);
						break;
					}
				}
				if (index == 0)
					return UpdateField((dataTablePhoneBookFields.Rows.Count) + 1, fieldValue, fieldType, phoneBookGuid);
				else
					return UpdateField(index, fieldValue, fieldType, phoneBookGuid);
			}
			else
				return base.ExecuteSPCommand("InsertField", "@Guid", Guid.NewGuid(),
																									 "@PhoneBookGuid", phoneBookGuid,
																									 "@Index", (index + 1),
																									 "@FieldValue", fieldValue,
																									 "@FieldType", fieldType);
		}

		public DataTable GetPagedAllUserFields(Guid userGuid, string fieldName, string phoneBookName, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet userFieldInfo = base.FetchSPDataSet("GetPagedAllUserFields", "@UserGuid", userGuid,
																																					"@FieldName", fieldName,
																																					"@PhoneBookName", phoneBookName,
																																					"@PageNo", pageNo,
																																					"@PageSize", pageSize,
																																					"@SortField", sortField);
			resultCount = Helper.GetInt(userFieldInfo.Tables[0].Rows[0]["RowCount"]);

			return userFieldInfo.Tables[1];
		}
	}
}
