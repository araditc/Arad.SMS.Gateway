using System;
using System.Data;
using GeneralLibrary;

namespace Common
{
	public class EmailBook : GeneralLibrary.BaseCore.CommonEntityBase
	{
		private enum TableFields
		{
			Name,
			CreateDate,
			IsPrivate,
			ParentGuid,
			AdminGuid,
			UserGuid
		}

		public EmailBook()
			: base(TableNames.EmailBooks.ToString())
		{
			AddField(TableFields.Name.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.IsPrivate.ToString(), SqlDbType.Bit);
			AddField(TableFields.ParentGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.AdminGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid EmailBookGuid
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
				if (value != string.Empty)
					this[TableFields.Name.ToString()] = value;
				else
				{
					HasError = true;
					ErrorMessage = Language.GetString("CompleteGroupNameField");
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
				return Helper.GetGuid(this[TableFields.AdminGuid.ToString()]); ;
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
