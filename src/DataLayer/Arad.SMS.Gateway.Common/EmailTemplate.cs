using System;
using System.Data;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace Common
{
	public class EmailTemplate : CommonEntityBase
	{
		public enum TableFields
		{
			Subject,
			Body,
			CreateDate,
			AttachmentList,
			IsDeleted,
			UserEmailSettingGuid,
		}

		public EmailTemplate()
			: base(TableNames.EmailTemplates.ToString())
		{
			AddField(TableFields.Subject.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.Body.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.AttachmentList.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.UserEmailSettingGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid EmailTemplateGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string Subject
		{
			get
			{
				return Helper.GetString(this[TableFields.Subject.ToString()]);
			}
			set
			{
				this[TableFields.Subject.ToString()] = value;
			}
		}

		public string Body
		{
			get
			{
				return Helper.GetString(this[TableFields.Body.ToString()]);
			}
			set
			{
				this[TableFields.Body.ToString()] = value;
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

		public string AttachmentList
		{
			get
			{
				return Helper.GetString(this[TableFields.AttachmentList.ToString()]);
			}
			set
			{
				this[TableFields.AttachmentList.ToString()] = value;
			}
		}

		public Guid UserEmailSettingGuid
		{
			get { return Helper.GetGuid(this[TableFields.UserEmailSettingGuid.ToString()]); }
			set { this[TableFields.UserEmailSettingGuid.ToString()] = value; }
		}
	}
}
