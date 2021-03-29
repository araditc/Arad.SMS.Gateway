using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Common
{
	public class Person : CommonEntityBase
	{
		private enum TableFields
		{
			ID,
			FirstName,
			LastName,
			FatherName,
			Gender,
			BirthDate,
			NationalCode,
			ShCode,
			CreateDate,
			IsDeleted,
			ZoneGuid,
		}

		public Person()
			: base(TableNames.Persons.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.BigInt);
			AddField(TableFields.FirstName.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.LastName.ToString(), SqlDbType.NVarChar, 64);
			AddField(TableFields.FatherName.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.Gender.ToString(), SqlDbType.Int);
			AddField(TableFields.BirthDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.NationalCode.ToString(), SqlDbType.Char, 10);
			AddField(TableFields.ShCode.ToString(), SqlDbType.NVarChar, 16);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.ZoneGuid.ToString(), SqlDbType.UniqueIdentifier);
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

		public string FatherName
		{
			get
			{
				return Helper.GetString(this[TableFields.FatherName.ToString()]);
			}
			set
			{
				this[TableFields.FatherName.ToString()] = value;
			}
		}

		public int Gender
		{
			get
			{
				return Helper.GetInt(this[TableFields.Gender.ToString()]);
			}
			set
			{
				this[TableFields.Gender.ToString()] = value;
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

		public string NationalCode
		{
			get
			{
				return Helper.GetString(this[TableFields.NationalCode.ToString()]);
			}
			set
			{
				this[TableFields.NationalCode.ToString()] = value;
			}
		}

		public string ShCode
		{
			get
			{
				return Helper.GetString(this[TableFields.ShCode.ToString()]);
			}
			set
			{
				this[TableFields.ShCode.ToString()] = value;
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
	}
}
