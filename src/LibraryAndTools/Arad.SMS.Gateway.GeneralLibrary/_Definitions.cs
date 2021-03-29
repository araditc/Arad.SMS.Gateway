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

namespace Arad.SMS.Gateway.GeneralLibrary
{
	public enum SPParameterGenerationType
	{
		ForCraeteTable = 1,
		ForUpdate = 2,
		ForFieldList = 3,
		ForInsert = 4,
		ForParameterList = 5,
		ForProcedureCall = 6,
	}

	public enum ErrorType
	{
		None = 0,
		OneServiceError = 1,
		SeveralServiceError = 2,
		NotEnableJavaScript = 3
	}

	public class CheckDataConditionsResult
	{
		public bool IsEmpty;
		public bool IsIntNumber;
		public bool IsDecimalNumber;
		public bool IsEmail;

		public CheckDataConditionsResult()
		{
			IsEmpty = false;
			IsIntNumber = false;
			IsDecimalNumber = false;
			IsEmail = false;
		}
	}

	public class TableFieldInfo
	{
		public string FieldName;
		public SqlDbType FieldType;
		public System.Type SystemType
		{
			get
			{
				#region Type mapping from SqlDbType to .NET types
				System.Type systemType;
				switch (FieldType)
				{
					case SqlDbType.BigInt:
						systemType = typeof(Int64);
						break;
					case SqlDbType.Int:
						systemType = typeof(Int32);
						break;
					case SqlDbType.SmallInt:
						systemType = typeof(Int16);
						break;
					case SqlDbType.TinyInt:
						systemType = typeof(Byte);
						break;

					case SqlDbType.Bit:
						systemType = typeof(Boolean);
						break;

					case SqlDbType.Date:
					case SqlDbType.DateTime:
					case SqlDbType.DateTime2:
					case SqlDbType.DateTimeOffset:
					case SqlDbType.SmallDateTime:
					case SqlDbType.Time:
						systemType = typeof(DateTime);
						break;

					case SqlDbType.Char:
					case SqlDbType.NChar:
					case SqlDbType.NText:
					case SqlDbType.NVarChar:
					case SqlDbType.Text:
					case SqlDbType.VarChar:
						systemType = typeof(String);
						break;

					case SqlDbType.Decimal:
						systemType = typeof(Decimal);
						break;

					case SqlDbType.Float:
					case SqlDbType.Real:
						systemType = typeof(Double);
						break;

					case SqlDbType.Image:
					case SqlDbType.VarBinary:
						systemType = typeof(byte[]);
						break;

					default:
						systemType = typeof(String);
						break;
				}

				return systemType;
				#endregion
			}
		}
		public short FieldSize;
		public object FieldValue;
		public bool ReadOnly;
		public bool LogChanges;
		public bool IsNumeric
		{
			get
			{
				return
					FieldType == SqlDbType.Int ||
					FieldType == SqlDbType.Decimal ||
					FieldType == SqlDbType.TinyInt ||
					FieldType == SqlDbType.BigInt ||
					FieldType == SqlDbType.Float ||
					FieldType == SqlDbType.Money ||
					FieldType == SqlDbType.Real ||
					FieldType == SqlDbType.SmallInt ||
					FieldType == SqlDbType.SmallMoney;
			}
		}

		public TableFieldInfo(string name, SqlDbType type) : this(name, type, -1, false, false, DBNull.Value) { }

		public TableFieldInfo(string name, SqlDbType type, bool logUpdates) : this(name, type, -1, false, logUpdates, DBNull.Value) { }

		public TableFieldInfo(string name, SqlDbType type, short size) : this(name, type, size, false, false, DBNull.Value) { }

		public TableFieldInfo(string name, SqlDbType type, short size, bool logUpdates) : this(name, type, size, false, logUpdates, DBNull.Value) { }

		public TableFieldInfo(string name, SqlDbType type, short size, bool isReadOnly, bool logUpdates, object initValue)
		{
			this.FieldName = name;
			this.FieldType = type;
			this.FieldSize = size;
			this.FieldValue = initValue;
			this.ReadOnly = isReadOnly;
			this.LogChanges = logUpdates;
		}

		public TableFieldInfo Clone()
		{
			return new TableFieldInfo(this.FieldName, this.FieldType, this.FieldSize, this.ReadOnly, this.LogChanges, this.FieldValue);
		}

		public void SetFieldValueByConversion(string value)
		{
			switch (this.FieldType)
			{
				case SqlDbType.BigInt:
					long longValue;
					if (long.TryParse(value, out longValue))
						this.FieldValue = longValue;
					else
						this.FieldValue = DBNull.Value;
					break;
				case SqlDbType.Binary:
					break;
				case SqlDbType.Bit:
					bool boolValue = Helper.GetBool(value);
					if (boolValue)
						this.FieldValue = boolValue;
					else
						this.FieldValue = DBNull.Value;
					break;
				case SqlDbType.Char:
					char charValue;
					if (char.TryParse(value, out charValue))
						this.FieldValue = charValue;
					else
						this.FieldValue = DBNull.Value;
					break;
				case SqlDbType.Date:
				case SqlDbType.SmallDateTime:
				case SqlDbType.DateTime:
				case SqlDbType.DateTime2:
					DateTime dateTimeValue;
					if (DateTime.TryParse(value, out dateTimeValue))
						this.FieldValue = dateTimeValue;
					else
						this.FieldValue = DBNull.Value;
					break;
				case SqlDbType.DateTimeOffset:
					break;
				case SqlDbType.Float:
					float floatValue;
					if (float.TryParse(value, out floatValue))
						this.FieldValue = floatValue;
					else
						this.FieldValue = DBNull.Value;
					break;
				case SqlDbType.VarBinary:
				case SqlDbType.Image:
					this.FieldValue = Convert.FromBase64String(value);
					break;
				case SqlDbType.Int:
					int intValue;
					if (int.TryParse(value, out intValue))
						this.FieldValue = intValue;
					else
						this.FieldValue = DBNull.Value;
					break;
				case SqlDbType.Money:
				case SqlDbType.SmallMoney:
				case SqlDbType.Real:
				case SqlDbType.Decimal:
					decimal decimalValue;
					if (decimal.TryParse(value, out decimalValue))
						this.FieldValue = decimalValue;
					else
						this.FieldValue = DBNull.Value;
					break;
				case SqlDbType.VarChar:
				case SqlDbType.Xml:
				case SqlDbType.Text:
				case SqlDbType.NText:
				case SqlDbType.NChar:
				case SqlDbType.NVarChar:
					if (value.Length == 0)
						this.FieldValue = DBNull.Value;
					else
						this.FieldValue = Helper.GetStandardizeCharacters(value);
					break;
				case SqlDbType.TinyInt:
				case SqlDbType.SmallInt:
					short shortValue;
					if (short.TryParse(value, out shortValue))
						this.FieldValue = shortValue;
					else
						this.FieldValue = DBNull.Value;
					break;
				case SqlDbType.Structured:
					break;
				case SqlDbType.Time:
					TimeSpan timeValue;
					if (TimeSpan.TryParse(value, out timeValue))
						this.FieldValue = timeValue;
					else
						this.FieldValue = DBNull.Value;
					break;
				case SqlDbType.Timestamp:
					break;
				case SqlDbType.Udt:
					break;
				case SqlDbType.UniqueIdentifier:
					try
					{
						this.FieldValue = new Guid(value);
					}
					catch (FormatException)
					{
						this.FieldValue = DBNull.Value;
					}
					break;
				case SqlDbType.Variant:
					if (value.Length == 0)
						this.FieldValue = DBNull.Value;
					else
						this.FieldValue = (object)Helper.GetStandardizeCharacters(value);
					break;
				default:
					throw new InvalidOperationException(string.Format("No {0} type has been defined", this.FieldType));
			}
		}
	}
}
