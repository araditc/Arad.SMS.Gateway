using System;
using System.Data;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace Common
{
	public class UserEmailSetting : CommonEntityBase
	{
		public enum TableFields
		{
			EmailAddress,
			Password,
			Type,
			CreateDate,
			IsDeleted,
			UserGuid
		}

		public UserEmailSetting()
			: base(TableNames.UserEmailSettings.ToString())
		{
			AddField(TableFields.EmailAddress.ToString(), SqlDbType.NVarChar, 200);
			AddField(TableFields.Password.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Type.ToString(), SqlDbType.Int);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid EmailSettingGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string EmailAddress
		{
			get
			{
				return Helper.GetString(this[TableFields.EmailAddress.ToString()]);
			}
			set
			{
				CheckDataConditionsResult result = Helper.CheckDataConditions(value);
				if (result.IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteEmailField");
					HasError = true;
				}
				else if (!result.IsEmail)
				{
					ErrorMessage += Language.GetString("EmailFieldIsNotCorectFormat");
					HasError = true;
				}
				else
					this[TableFields.EmailAddress.ToString()] = value;
			}
		}

		public string Password
		{
			get
			{
				return Helper.GetString(this[TableFields.Password.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompletePasswordField");
					HasError = true;
				}
				else
					this[TableFields.Password.ToString()] = value;
			}
		}

		public int EmailHostType
		{
			get { return Helper.GetInt(this[TableFields.Type.ToString()]); }
			set { this[TableFields.Type.ToString()] = value; }
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

		public Guid UserGuid
		{
			get { return Helper.GetGuid(this[TableFields.UserGuid.ToString()]); }
			set { this[TableFields.UserGuid.ToString()] = value; }
		}
	}
}
