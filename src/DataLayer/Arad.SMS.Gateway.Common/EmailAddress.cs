using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GeneralLibrary;

namespace Common
{
	public class EmailAddress : GeneralLibrary.BaseCore.CommonEntityBase
	{
		private enum TableFields
		{
			EmailAddress,
			CreateDate,
			IsDeleted,
			EmailBookGuid,
		}

		public EmailAddress()
			: base(TableNames.EmailAddresses.ToString())
		{
			AddField(TableFields.EmailAddress.ToString(), SqlDbType.NVarChar, 200);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.EmailBookGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid EmailGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string Email
		{
			get
			{
				return Helper.GetString(this[TableFields.EmailAddress.ToString()]);
			}
			set
			{
				if (value != string.Empty)
					this[TableFields.EmailAddress.ToString()] = value;
				else
				{
					HasError = true;
					ErrorMessage = Language.GetString("CompleteEmailField");
				}
			}
		}

		public DateTime CreateDate
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); ;
			}
			set
			{
				this[TableFields.CreateDate.ToString()] = value;
			}
		}

		public Guid EmailBookGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.EmailBookGuid.ToString()]); ;
			}
			set
			{
				this[TableFields.EmailBookGuid.ToString()] = value;
			}
		}
	}
}
