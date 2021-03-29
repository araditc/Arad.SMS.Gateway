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
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;

namespace Arad.SMS.Gateway.Common
{
	public class UserField : CommonEntityBase
	{
		public enum TableFields
		{
			Field1,
			Field2,
			Field3,
			Field4,
			Field5,
			Field6,
			Field7,
			Field8,
			Field9,
			Field10,
			Field11,
			Field12,
			Field13,
			Field14,
			Field15,
			Field16,
			Field17,
			Field18,
			Field19,
			Field20,
			FieldType1,
			FieldType2,
			FieldType3,
			FieldType4,
			FieldType5,
			FieldType6,
			FieldType7,
			FieldType8,
			FieldType9,
			FieldType10,
			FieldType11,
			FieldType12,
			FieldType13,
			FieldType14,
			FieldType15,
			FieldType16,
			FieldType17,
			FieldType18,
			FieldType19,
			FieldType20,
			PhoneBookGuid,
		}

		public UserField()
			: base(TableNames.UserFields.ToString())
		{
			AddField(TableFields.Field1.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field2.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field3.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field4.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field5.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field6.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field7.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field8.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field9.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field10.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field11.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field12.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field13.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field14.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field15.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field16.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field17.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field18.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field19.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Field20.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.FieldType1.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType2.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType3.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType4.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType5.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType6.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType7.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType8.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType9.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType10.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType11.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType12.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType13.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType14.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType15.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType16.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType17.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType18.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType19.ToString(), SqlDbType.Int);
			AddField(TableFields.FieldType20.ToString(), SqlDbType.Int);
			AddField(TableFields.PhoneBookGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid UserFieldGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string Field1
		{
			get
			{
				return Helper.GetString(this[TableFields.Field1.ToString()]);
			}
			set
			{
				this[TableFields.Field1.ToString()] = value;
			}
		}

		public string Field2
		{
			get
			{
				return Helper.GetString(this[TableFields.Field2.ToString()]);
			}
			set
			{
				this[TableFields.Field2.ToString()] = value;
			}
		}

		public string Field3
		{
			get
			{
				return Helper.GetString(this[TableFields.Field3.ToString()]);
			}
			set
			{
				this[TableFields.Field3.ToString()] = value;
			}
		}

		public string Field4
		{
			get
			{
				return Helper.GetString(this[TableFields.Field4.ToString()]);
			}
			set
			{
				this[TableFields.Field4.ToString()] = value;
			}
		}

		public string Field5
		{
			get
			{
				return Helper.GetString(this[TableFields.Field5.ToString()]);
			}
			set
			{
				this[TableFields.Field5.ToString()] = value;
			}
		}

		public string Field6
		{
			get
			{
				return Helper.GetString(this[TableFields.Field6.ToString()]);
			}
			set
			{
				this[TableFields.Field6.ToString()] = value;
			}
		}

		public string Field7
		{
			get
			{
				return Helper.GetString(this[TableFields.Field7.ToString()]);
			}
			set
			{
				this[TableFields.Field7.ToString()] = value;
			}
		}

		public string Field8
		{
			get
			{
				return Helper.GetString(this[TableFields.Field8.ToString()]);
			}
			set
			{
				this[TableFields.Field8.ToString()] = value;
			}
		}

		public string Field9
		{
			get
			{
				return Helper.GetString(this[TableFields.Field9.ToString()]);
			}
			set
			{
				this[TableFields.Field9.ToString()] = value;
			}
		}

		public string Field10
		{
			get
			{
				return Helper.GetString(this[TableFields.Field10.ToString()]);
			}
			set
			{
				this[TableFields.Field10.ToString()] = value;
			}
		}

		public string Field11
		{
			get
			{
				return Helper.GetString(this[TableFields.Field11.ToString()]);
			}
			set
			{
				this[TableFields.Field11.ToString()] = value;
			}
		}

		public string Field12
		{
			get
			{
				return Helper.GetString(this[TableFields.Field12.ToString()]);
			}
			set
			{
				this[TableFields.Field12.ToString()] = value;
			}
		}

		public string Field13
		{
			get
			{
				return Helper.GetString(this[TableFields.Field13.ToString()]);
			}
			set
			{
				this[TableFields.Field13.ToString()] = value;
			}
		}

		public string Field14
		{
			get
			{
				return Helper.GetString(this[TableFields.Field14.ToString()]);
			}
			set
			{
				this[TableFields.Field14.ToString()] = value;
			}
		}

		public string Field15
		{
			get
			{
				return Helper.GetString(this[TableFields.Field15.ToString()]);
			}
			set
			{
				this[TableFields.Field15.ToString()] = value;
			}
		}

		public string Field16
		{
			get
			{
				return Helper.GetString(this[TableFields.Field16.ToString()]);
			}
			set
			{
				this[TableFields.Field16.ToString()] = value;
			}
		}

		public string Field17
		{
			get
			{
				return Helper.GetString(this[TableFields.Field17.ToString()]);
			}
			set
			{
				this[TableFields.Field17.ToString()] = value;
			}
		}

		public string Field18
		{
			get
			{
				return Helper.GetString(this[TableFields.Field18.ToString()]);
			}
			set
			{
				this[TableFields.Field18.ToString()] = value;
			}
		}

		public string Field19
		{
			get
			{
				return Helper.GetString(this[TableFields.Field19.ToString()]);
			}
			set
			{
				this[TableFields.Field19.ToString()] = value;
			}
		}

		public string Field20
		{
			get
			{
				return Helper.GetString(this[TableFields.Field20.ToString()]);
			}
			set
			{
				this[TableFields.Field20.ToString()] = value;
			}
		}

		public int FieldType1
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType1.ToString()]);
			}
			set
			{
				this[TableFields.FieldType1.ToString()] = value;
			}
		}

		public int FieldType2
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType2.ToString()]);
			}
			set
			{
				this[TableFields.FieldType2.ToString()] = value;
			}
		}

		public int FieldType3
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType3.ToString()]);
			}
			set
			{
				this[TableFields.FieldType3.ToString()] = value;
			}
		}

		public int FieldType4
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType4.ToString()]);
			}
			set
			{
				this[TableFields.FieldType4.ToString()] = value;
			}
		}

		public int FieldType5
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType5.ToString()]);
			}
			set
			{
				this[TableFields.FieldType5.ToString()] = value;
			}
		}

		public int FieldType6
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType6.ToString()]);
			}
			set
			{
				this[TableFields.FieldType6.ToString()] = value;
			}
		}

		public int FieldType7
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType7.ToString()]);
			}
			set
			{
				this[TableFields.FieldType7.ToString()] = value;
			}
		}

		public int FieldType8
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType8.ToString()]);
			}
			set
			{
				this[TableFields.FieldType8.ToString()] = value;
			}
		}

		public int FieldType9
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType9.ToString()]);
			}
			set
			{
				this[TableFields.FieldType9.ToString()] = value;
			}
		}

		public int FieldType10
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType10.ToString()]);
			}
			set
			{
				this[TableFields.FieldType10.ToString()] = value;
			}
		}

		public int FieldType11
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType11.ToString()]);
			}
			set
			{
				this[TableFields.FieldType11.ToString()] = value;
			}
		}

		public int FieldType12
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType12.ToString()]);
			}
			set
			{
				this[TableFields.FieldType12.ToString()] = value;
			}
		}

		public int FieldType13
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType13.ToString()]);
			}
			set
			{
				this[TableFields.FieldType13.ToString()] = value;
			}
		}

		public int FieldType14
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType14.ToString()]);
			}
			set
			{
				this[TableFields.FieldType14.ToString()] = value;
			}
		}

		public int FieldType15
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType15.ToString()]);
			}
			set
			{
				this[TableFields.FieldType15.ToString()] = value;
			}
		}

		public int FieldType16
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType16.ToString()]);
			}
			set
			{
				this[TableFields.FieldType16.ToString()] = value;
			}
		}

		public int FieldType17
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType17.ToString()]);
			}
			set
			{
				this[TableFields.FieldType17.ToString()] = value;
			}
		}

		public int FieldType18
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType18.ToString()]);
			}
			set
			{
				this[TableFields.FieldType18.ToString()] = value;
			}
		}

		public int FieldType19
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType19.ToString()]);
			}
			set
			{
				this[TableFields.FieldType19.ToString()] = value;
			}
		}

		public int FieldType20
		{
			get
			{
				return Helper.GetInt(this[TableFields.FieldType20.ToString()]);
			}
			set
			{
				this[TableFields.FieldType20.ToString()] = value;
			}
		}

		public Guid PhoneBookGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.PhoneBookGuid.ToString()]);
			}
			set
			{
				this[TableFields.PhoneBookGuid.ToString()] = value;
			}
		}
	}
}
