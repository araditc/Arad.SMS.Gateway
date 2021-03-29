using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Common
{
	public class Sms : CommonEntityBase
	{
		private enum TableFields
		{
			Reciever,
			SmsBody,
			PrivateNumberGuid,
			UserGuid,
			DeliveryStatus,
			SmsCount,
			EffectiveDateTime,
			SmsSentGuid,
			OuterSystemSmsID,
			CheckingMessageID,
		}

		public Sms()
			: base(TableNames.Smses.ToString())
		{
			AddField(TableFields.Reciever.ToString(), SqlDbType.NVarChar);
			AddField(TableFields.SmsBody.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.PrivateNumberGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.DeliveryStatus.ToString(), SqlDbType.Int);
			AddField(TableFields.SmsCount.ToString(), SqlDbType.Int);
			AddField(TableFields.EffectiveDateTime.ToString(), SqlDbType.DateTime);
			AddField(TableFields.SmsSentGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.OuterSystemSmsID.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.CheckingMessageID.ToString(), SqlDbType.NVarChar, short.MaxValue);
		}

		public Guid SmsGuid
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
				this[TableFields.Reciever.ToString()] = value;
			}
		}

		public string SmsBody
		{
			get
			{
				return Helper.GetString(this[TableFields.SmsBody.ToString()]);
			}
			set
			{
				this[TableFields.SmsBody.ToString()] = value;
			}
		}

		public Guid PrivateNumberGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.PrivateNumberGuid.ToString()]);
			}
			set
			{
				this[TableFields.PrivateNumberGuid.ToString()] = value;
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

		public int DeliveryStatus
		{
			get
			{
				return Helper.GetInt(this[TableFields.DeliveryStatus.ToString()]);
			}
			set
			{
				this[TableFields.DeliveryStatus.ToString()] = value;
			}
		}

		public int SmsCount
		{
			get
			{
				return Helper.GetInt(this[TableFields.SmsCount.ToString()]);
			}
			set
			{
				this[TableFields.SmsCount.ToString()] = value;
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

		public Guid SmsSentGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.SmsSentGuid.ToString()]);
			}
			set
			{
				this[TableFields.SmsSentGuid.ToString()] = value;
			}
		}

		public string OuterSystemSmsID
		{
			get
			{
				return Helper.GetString(this[TableFields.OuterSystemSmsID.ToString()]);
			}
			set
			{
				this[TableFields.OuterSystemSmsID.ToString()] = value;
			}
		}

		public string CheckingMessageID
		{
			get
			{
				return Helper.GetString(this[TableFields.CheckingMessageID.ToString()]);
			}
			set
			{
				this[TableFields.CheckingMessageID.ToString()] = value;
			}
		}
	}
}
