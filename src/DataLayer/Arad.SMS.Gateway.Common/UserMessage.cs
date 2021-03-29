using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using System.Data;
using GeneralLibrary;

namespace Common
{
	public class UserMessage : CommonEntityBase
	{
		public enum TableFields
		{
			Name,
			CreateDate,
			Email,
			Telephone,
			CellPhone,
			Job,
			Description,
			DomainGuid,
			IsDeleted,
		}

		public UserMessage()
			: base(TableNames.UserMessages.ToString())
		{
			AddField(TableFields.Name.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.Email.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Telephone.ToString(), SqlDbType.NVarChar,50 );
			AddField(TableFields.CellPhone.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Job.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Description.ToString(), SqlDbType.NVarChar,short.MaxValue);
			AddField(TableFields.DomainGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
		}

		public Guid UserMessageGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string Name
		{
			get
			{
				return Helper.GetString(this[TableFields.Name.ToString()]);
			}
			set
			{
					this[TableFields.Name.ToString()] = value;
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

		public string Email
		{
			get
			{
				return Helper.GetString(this[TableFields.Email.ToString()]);
			}
			set
			{
				CheckDataConditionsResult result = Helper.CheckDataConditions(value);
					this[TableFields.Email.ToString()] = value;
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
					this[TableFields.CellPhone.ToString()] = value;
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

		public string Description
		{
			get
			{
				return Helper.GetString(this[TableFields.Description.ToString()]);
			}
			set
			{
				this[TableFields.Description.ToString()] = value;
			}
		}

		public Guid DomainGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.DomainGuid.ToString()]);
			}
			set
			{
				this[TableFields.DomainGuid.ToString()] = value;
			}
		}
	}
}