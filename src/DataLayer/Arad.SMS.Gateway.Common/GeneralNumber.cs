using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Common
{
	public class GeneralNumber : CommonEntityBase
	{
		public enum TableFields
		{
			FirstName,
			LastName,
			Telephone,
			CellPhone,
			FaxNumber,
			Job,
			Address,
			Email,
			BirthDate,
			CreateDate,
			Sex,
			IsDeleted,
			GeneralPhoneBookGuid,
		}

		public GeneralNumber()
			: base(TableNames.GeneralNumbers.ToString())
		{
			AddField(TableFields.FirstName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.LastName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Telephone.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CellPhone.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.FaxNumber.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Job.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Address.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.Email.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.BirthDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.Sex.ToString(), SqlDbType.Int);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.GeneralPhoneBookGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid GeneralNumberGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
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
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteMobileNoField");
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

		public int Sex
		{
			get
			{
				return Helper.GetInt(this[TableFields.Sex.ToString()]);
			}
			set
			{
				this[TableFields.Sex.ToString()] = value;
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

		public string Email
		{
			get
			{
				return Helper.GetString(this[TableFields.Email.ToString()]);
			}
			set
			{
				if (!Helper.CheckDataConditions(value).IsEmail)
				{
					ErrorMessage += Language.GetString("EmailFieldIsNotCorectFormat");
					HasError = true;
				}
				else
					this[TableFields.Email.ToString()] = value;
			}
		}

		public Guid GeneralPhoneBookGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.GeneralPhoneBookGuid.ToString()]);
			}
			set
			{
				this[TableFields.GeneralPhoneBookGuid.ToString()] = value;
			}
		}
	}
}
