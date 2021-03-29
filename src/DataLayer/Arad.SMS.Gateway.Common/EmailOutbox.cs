using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using System.Data;
using GeneralLibrary;

namespace Common
{
	public class EmailOutbox : CommonEntityBase
	{
		public enum TableFields
		{
			UserEmailSettingGuid,
			Reciever,
			Body,
			Subject,
			AttachmentList,
			DownRange,
			UpRange,
			GroupGuid,
			TypeSend,
			State,
			DateTimeFuture,
			CreateDate,
			Description,
			UserGuid,
			IsDeleted,
		}

		public EmailOutbox()
			: base(TableNames.EmailOutboxes.ToString())
		{
			AddField(TableFields.UserEmailSettingGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.Reciever.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Body.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.Subject.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.AttachmentList.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.DownRange.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.UpRange.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.GroupGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.TypeSend.ToString(), SqlDbType.Int);
			AddField(TableFields.State.ToString(), SqlDbType.Int);
			AddField(TableFields.DateTimeFuture.ToString(), SqlDbType.DateTime);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.Description.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);

			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
		}

		public Guid EmailOutboxGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public Guid UserEmailSettingGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.UserEmailSettingGuid.ToString()]);
			}
			set
			{
				this[TableFields.UserEmailSettingGuid.ToString()] = value;
			}
		}

		public string Reciever
		{
			get
			{
				return Helper.GetString(this[TableFields.Reciever.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteRecieverEmailField");
					HasError = true;
				}
				else
					this[TableFields.Reciever.ToString()] = value;
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

		public string DownRange
		{
			get
			{
				return Helper.GetString(this[TableFields.DownRange.ToString()]);
			}
			set
			{
				this[TableFields.DownRange.ToString()] = value;
			}
		}

		public string UpRange
		{
			get
			{
				return Helper.GetString(this[TableFields.UpRange.ToString()]);
			}
			set
			{
				this[TableFields.UpRange.ToString()] = value;
			}
		}


		public Guid GroupGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.GroupGuid.ToString()]);
			}
			set
			{
				this[TableFields.GroupGuid.ToString()] = value;
			}
		}

		public int TypeSend
		{
			get
			{
				return Helper.GetInt(this[TableFields.TypeSend.ToString()]);
			}
			set
			{
				this[TableFields.TypeSend.ToString()] = value;
			}
		}

		public int State
		{
			get
			{
				return Helper.GetInt(this[TableFields.State.ToString()]);
			}
			set
			{
				this[TableFields.State.ToString()] = value;
			}
		}

		public DateTime DateTimeFuture
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.DateTimeFuture.ToString()]);
			}
			set
			{
				this[TableFields.DateTimeFuture.ToString()] = value;
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

		public Guid UserGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.UserGuid.ToString()]);
			}
			set
			{
				this[TableFields.UserGuid.ToString()] = value;
			}
		}
	}
}
