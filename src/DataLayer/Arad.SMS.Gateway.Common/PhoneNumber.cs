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

using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Arad.SMS.Gateway.Common
{
	public class PhoneNumber : CommonEntityBase
	{
		public enum TableFields
		{
			ID,
			FirstName,
			LastName,
			NationalCode,
			BirthDate,
			CreateDate,
			Telephone,
			CellPhone,
			FaxNumber,
			Job,
			Address,
			ZipCode,
			Email,
			F1,
			F2,
			F3,
			F4,
			F5,
			F6,
			F7,
			F8,
			F9,
			F10,
			F11,
			F12,
			F13,
			F14,
			F15,
			F16,
			F17,
			F18,
			F19,
			F20,
			Sex,
			Operator,
			IsDeleted,
			ZoneGuid,
			PhoneBookGuid,
		}

		public PhoneNumber()
			: base(TableNames.PhoneNumbers.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.Int);
			AddField(TableFields.FirstName.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.LastName.ToString(), SqlDbType.NVarChar, 64);
			AddField(TableFields.NationalCode.ToString(), SqlDbType.NChar, 10);
			AddField(TableFields.BirthDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.Telephone.ToString(), SqlDbType.NVarChar, 16);
			AddField(TableFields.CellPhone.ToString(), SqlDbType.NVarChar, 16);
			AddField(TableFields.FaxNumber.ToString(), SqlDbType.NVarChar, 16);
			AddField(TableFields.Job.ToString(), SqlDbType.NVarChar, 64);
			AddField(TableFields.Address.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.ZipCode.ToString(), SqlDbType.NChar, 10);
			AddField(TableFields.Email.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F1.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F2.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F3.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F4.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F5.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F6.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F7.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F8.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F9.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F10.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F11.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F12.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F13.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F14.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F15.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F16.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F17.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F18.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F19.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.F20.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.Sex.ToString(), SqlDbType.TinyInt);
			AddField(TableFields.Operator.ToString(), SqlDbType.TinyInt);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.ZoneGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.PhoneBookGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid PhoneNumberGuid
		{
			get
			{
				return PrimaryKey;
			}
			set
			{
				PrimaryKey = value;
			}
		}

		public int ID
		{
			get
			{
				return Helper.GetInt(this[TableFields.ID.ToString()]);
			}
			set
			{
				this[TableFields.ID.ToString()] = value;
			}
		}

		public string FirstName
		{
			get
			{
				return Helper.GetString(this[TableFields.FirstName.ToString()]);
			}
			set
			{
				this[TableFields.FirstName.ToString()] = value;
			}
		}

		public string LastName
		{
			get
			{
				return Helper.GetString(this[TableFields.LastName.ToString()]);
			}
			set
			{
				this[TableFields.LastName.ToString()] = value;
			}
		}

		public string NationalCode
		{
			get
			{
				return Helper.GetString(this[TableFields.NationalCode.ToString()]);
			}
			set
			{
				string error = string.Empty;
				if (!Helper.IsValidNationalCode(value, ref error))
				{
					ErrorMessage += error;
					HasError = true;
				}
				else
					this[TableFields.NationalCode.ToString()] = value;
			}
		}

		public DateTime BirthDate
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.BirthDate.ToString()]);
			}
			set
			{
				this[TableFields.BirthDate.ToString()] = value;
			}
		}

		public DateTime CreateDate
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]);
			}
			set
			{
				this[TableFields.CreateDate.ToString()] = value;
			}
		}

		public string Telephone
		{
			get
			{
				return Helper.GetString(this[TableFields.Telephone.ToString()]);
			}
			set
			{
				this[TableFields.Telephone.ToString()] = value;
			}
		}

		public string CellPhone
		{
			get
			{
				return Helper.GetString(this[TableFields.CellPhone.ToString()]);
			}
			set
			{
				//if (Helper.CheckDataConditions(value).IsEmpty)
				//{
				//	ErrorMessage += Language.GetString("CompleteMobileNoField");
				//	HasError = true;
				//}
				if (Helper.IsCellPhone(value) == 0)
				{
					ErrorMessage += Language.GetString("CellPhoneNotValid");
					HasError = true;
				}
				else
					this[TableFields.CellPhone.ToString()] = value;
			}
		}

		public string FaxNumber
		{
			get
			{
				return Helper.GetString(this[TableFields.FaxNumber.ToString()]);
			}
			set
			{
				this[TableFields.FaxNumber.ToString()] = value;
			}
		}

		public string Job
		{
			get
			{
				return Helper.GetString(this[TableFields.Job.ToString()]);
			}
			set
			{
				this[TableFields.Job.ToString()] = value;
			}
		}

		public byte Sex
		{
			get
			{
				return Helper.GetByte(this[TableFields.Sex.ToString()]);
			}
			set
			{
				this[TableFields.Sex.ToString()] = value;
			}
		}

		public byte Operator
		{
			get
			{
				return Helper.GetByte(this[TableFields.Operator.ToString()]);
			}
			set
			{
				this[TableFields.Operator.ToString()] = value;
			}
		}

		public string Address
		{
			get
			{
				return Helper.GetString(this[TableFields.Address.ToString()]);
			}
			set
			{
				this[TableFields.Address.ToString()] = value;
			}
		}

		public string ZipCode
		{
			get
			{
				return Helper.GetString(this[TableFields.ZipCode.ToString()]);
			}
			set
			{
				this[TableFields.ZipCode.ToString()] = value;
			}
		}

		public string Email
		{
			get
			{
				return Helper.GetString(this[TableFields.Email.ToString()]);
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && !Helper.CheckDataConditions(value).IsEmail)
				{
					ErrorMessage += Language.GetString("EmailFieldIsNotCorectFormat");
					HasError = true;
				}
				else
					this[TableFields.Email.ToString()] = value;
			}
		}

		public string F1
		{
			get
			{
				return Helper.GetString(this[TableFields.F1.ToString()]);
			}
			set
			{
				this[TableFields.F1.ToString()] = value;
			}
		}

		public string F2
		{
			get
			{
				return Helper.GetString(this[TableFields.F2.ToString()]);
			}
			set
			{
				this[TableFields.F2.ToString()] = value;
			}
		}

		public string F3
		{
			get
			{
				return Helper.GetString(this[TableFields.F3.ToString()]);
			}
			set
			{
				this[TableFields.F3.ToString()] = value;
			}
		}

		public string F4
		{
			get
			{
				return Helper.GetString(this[TableFields.F4.ToString()]);
			}
			set
			{
				this[TableFields.F4.ToString()] = value;
			}
		}

		public string F5
		{
			get
			{
				return Helper.GetString(this[TableFields.F5.ToString()]);
			}
			set
			{
				this[TableFields.F5.ToString()] = value;
			}
		}

		public string F6
		{
			get
			{
				return Helper.GetString(this[TableFields.F6.ToString()]);
			}
			set
			{
				this[TableFields.F6.ToString()] = value;
			}
		}

		public string F7
		{
			get
			{
				return Helper.GetString(this[TableFields.F7.ToString()]);
			}
			set
			{
				this[TableFields.F7.ToString()] = value;
			}
		}

		public string F8
		{
			get
			{
				return Helper.GetString(this[TableFields.F8.ToString()]);
			}
			set
			{
				this[TableFields.F8.ToString()] = value;
			}
		}

		public string F9
		{
			get
			{
				return Helper.GetString(this[TableFields.F9.ToString()]);
			}
			set
			{
				this[TableFields.F9.ToString()] = value;
			}
		}

		public string F10
		{
			get
			{
				return Helper.GetString(this[TableFields.F10.ToString()]);
			}
			set
			{
				this[TableFields.F10.ToString()] = value;
			}
		}

		public string F11
		{
			get
			{
				return Helper.GetString(this[TableFields.F11.ToString()]);
			}
			set
			{
				this[TableFields.F11.ToString()] = value;
			}
		}

		public string F12
		{
			get
			{
				return Helper.GetString(this[TableFields.F12.ToString()]);
			}
			set
			{
				this[TableFields.F12.ToString()] = value;
			}
		}

		public string F13
		{
			get
			{
				return Helper.GetString(this[TableFields.F13.ToString()]);
			}
			set
			{
				this[TableFields.F13.ToString()] = value;
			}
		}

		public string F14
		{
			get
			{
				return Helper.GetString(this[TableFields.F14.ToString()]);
			}
			set
			{
				this[TableFields.F14.ToString()] = value;
			}
		}

		public string F15
		{
			get
			{
				return Helper.GetString(this[TableFields.F15.ToString()]);
			}
			set
			{
				this[TableFields.F15.ToString()] = value;
			}
		}

		public string F16
		{
			get
			{
				return Helper.GetString(this[TableFields.F16.ToString()]);
			}
			set
			{
				this[TableFields.F16.ToString()] = value;
			}
		}

		public string F17
		{
			get
			{
				return Helper.GetString(this[TableFields.F17.ToString()]);
			}
			set
			{
				this[TableFields.F17.ToString()] = value;
			}
		}

		public string F18
		{
			get
			{
				return Helper.GetString(this[TableFields.F18.ToString()]);
			}
			set
			{
				this[TableFields.F18.ToString()] = value;
			}
		}

		public string F19
		{
			get
			{
				return Helper.GetString(this[TableFields.F19.ToString()]);
			}
			set
			{
				this[TableFields.F19.ToString()] = value;
			}
		}

		public string F20
		{
			get
			{
				return Helper.GetString(this[TableFields.F20.ToString()]);
			}
			set
			{
				this[TableFields.F20.ToString()] = value;
			}
		}

		public bool IsDeleted
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsDeleted.ToString()]);
			}
			set
			{
				this[TableFields.IsDeleted.ToString()] = value;
			}
		}

		public Guid ZoneGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.ZoneGuid.ToString()]);
			}
			set
			{
				this[TableFields.ZoneGuid.ToString()] = value;
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
