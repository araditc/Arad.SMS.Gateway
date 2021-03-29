using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using System.Data;
using GeneralLibrary;

namespace Common
{
	public class EmailSentbox : CommonEntityBase
	{
		public enum TableFields
		{
			Reciever,
			Subject,
			Body,
			SenderEmail,
			EffectiveDateTime,
			Status,
			UserGuid,
			EmailOutboxGuid,
		}

		public EmailSentbox()
			: base(TableNames.EmailSentboxes.ToString())
		{
			AddField(TableFields.Reciever.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.Subject.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.Body.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.SenderEmail.ToString(), SqlDbType.NVarChar, 200);
			AddField(TableFields.EffectiveDateTime.ToString(), SqlDbType.DateTime);
			AddField(TableFields.Status.ToString(), SqlDbType.Int);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.EmailOutboxGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid EmailSentboxGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
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

		public string SenderEmail
		{
			get
			{
				return Helper.GetString(this[TableFields.SenderEmail.ToString()]);
			}
			set
			{
				this[TableFields.SenderEmail.ToString()] = value;
			}
		}

		public DateTime EffectiveDateTime
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.EffectiveDateTime.ToString()]);
			}
			set
			{
				this[TableFields.EffectiveDateTime.ToString()] = value;
			}
		}

		public int Status
		{
			get
			{
				return Helper.GetInt(this[TableFields.Status.ToString()]);
			}
			set
			{
				this[TableFields.Status.ToString()] = value;
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

		public Guid EmailOutboxGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.EmailOutboxGuid.ToString()]);
			}
			set
			{
				this[TableFields.EmailOutboxGuid.ToString()] = value;
			}
		}
	}
}
