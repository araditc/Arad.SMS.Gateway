using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Common
{
	public class GeneralPhoneBook : CommonEntityBase
	{
		public enum TableFields
		{
			Name,
			CreateDate,
			IsPrivate,
			IsDeleted,
			ParentGuid,
			AdminGuid,
			UserGuid,
		}

		public GeneralPhoneBook()
			: base(TableNames.GeneralPhoneBooks.ToString())
		{
			AddField(TableFields.Name.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.IsPrivate.ToString(), SqlDbType.Bit);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.ParentGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.AdminGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid GeneralPhoneBookGuid
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
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					HasError = true;
					ErrorMessage = Language.GetString("CompleteGroupNameField");
				}
				else
					this[TableFields.Name.ToString()] = value;
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

		public bool IsPrivate
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsPrivate.ToString()]); ;
			}
			set
			{
				this[TableFields.IsPrivate.ToString()] = value;
			}
		}

		public Guid ParentGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.ParentGuid.ToString()]); ;
			}
			set
			{
				this[TableFields.ParentGuid.ToString()] = value;
			}
		}

		public Guid AdminGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.AdminGuid.ToString()]);
			}
			set
			{
				this[TableFields.AdminGuid.ToString()] = value;
			}
		}

		public Guid UserGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.UserGuid.ToString()]); ;
			}
			set
			{
				this[TableFields.UserGuid.ToString()] = value;
			}
		}
	}
}
