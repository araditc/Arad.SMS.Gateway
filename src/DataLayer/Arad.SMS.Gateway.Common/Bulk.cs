using System;
using System.Data;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace Common
{
	public class Bulk : CommonEntityBase
	{
		public enum TableFields
		{
			BulkID,
			SmsBody,
			PresentType,
			Status,
			SmsCount,
			Encoding,
			RecipientsNumberCount,
			SendFaildType,
			SendDateTime,
			CreateDate,
			UserPrivateNumberGuid,
			UserGuid,
		}

		public Bulk()
			: base(TableNames.Bulks.ToString())
		{
			AddField(TableFields.BulkID.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.SmsBody.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.PresentType.ToString(), SqlDbType.Int);
			AddField(TableFields.Status.ToString(), SqlDbType.Int);
			AddField(TableFields.SmsCount.ToString(), SqlDbType.Int);
			AddField(TableFields.Encoding.ToString(), SqlDbType.Int);
			AddField(TableFields.RecipientsNumberCount.ToString(), SqlDbType.Int);
			AddField(TableFields.SendFaildType.ToString(), SqlDbType.Int);
			AddField(TableFields.SendDateTime.ToString(), SqlDbType.DateTime);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.UserPrivateNumberGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid BulkGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string BulkID
		{
			get { return Helper.GetString(this[TableFields.BulkID.ToString()]); }
			set { this[TableFields.BulkID.ToString()] = value; }
		}

		public string SmsBody
		{
			get { return Helper.GetString(this[TableFields.SmsBody.ToString()]); }
			set { this[TableFields.SmsBody.ToString()] = value; }
		}

		public int PresentType
		{
			get { return Helper.GetInt(this[TableFields.PresentType.ToString()]); }
			set { this[TableFields.PresentType.ToString()] = value; }
		}

		public int Status
		{
			get { return Helper.GetInt(this[TableFields.Status.ToString()]); }
			set { this[TableFields.Status.ToString()] = value; }
		}

		public int SmsCount
		{
			get { return Helper.GetInt(this[TableFields.SmsCount.ToString()]); }
			set { this[TableFields.SmsCount.ToString()] = value; }
		}

		public int Encoding
		{
			get { return Helper.GetInt(this[TableFields.Encoding.ToString()]); }
			set { this[TableFields.Encoding.ToString()] = value; }
		}

		public int RecipientsNumberCount
		{
			get { return Helper.GetInt(this[TableFields.RecipientsNumberCount.ToString()]); }
			set { this[TableFields.RecipientsNumberCount.ToString()] = value; }
		}

		public int SendFaildType
		{
			get { return Helper.GetInt(this[TableFields.SendFaildType.ToString()]); }
			set { this[TableFields.SendFaildType.ToString()] = value; }
		}

		public DateTime SendDateTime
		{
			get { return Helper.GetDateTime(this[TableFields.SendDateTime.ToString()]); }
			set { this[TableFields.SendDateTime.ToString()] = value; }
		}

		public DateTime CreateDate
		{
			get { return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); }
			set { this[TableFields.CreateDate.ToString()] = value; }
		}

		public Guid UserPrivateNumberGuid
		{
			get { return Helper.GetGuid(this[TableFields.UserPrivateNumberGuid.ToString()]); }
			set { this[TableFields.UserPrivateNumberGuid.ToString()] = value; }
		}

		public Guid UserGuid
		{
			get { return Helper.GetGuid(this[TableFields.UserGuid.ToString()]); }
			set { this[TableFields.UserGuid.ToString()] = value; }
		}
	}
}
