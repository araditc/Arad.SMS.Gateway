using System;
using System.Data;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace Common
{
	public class SmsSent : CommonEntityBase
	{
		private enum TableFields
		{
			BatchId,
			ItemId,
			SendStatus,
			WrapperDateTime,
			PrivateNumberGuid,
			SenderId,
			SmsPriority,
			IsUnicode,
			SmsLen,
			SmsText,
			SendingTryCount,
			SentDateTime,
			DeliveryNeeded,
			Udh,
			SenderIp,
			IsFlash,
			UserGuid,
			SmsIdentifier,
			SmsPartIndex,
			//PrivateNumberGuid,
			//UserDateFieldIndex,
			//Reciever,
			//BulkID,
			//SmsBody,
			//SmsCount,
			//Encoding,
			//RecipientsNumberCount,
			//PresentType,
			//DownRange,
			//UpRange,
			//Period,
			//PeriodType,
			//SmsFormatGuid,
			//AdvancedSearchQuery,
			//LastPeriodSendDateTime,
			//TypeSend,
			//State,
			//DateTimeFuture,
			//StartDateTime,
			//EndDateTime,
			//CreateDate,
			//GroupGuid,
			//UserGuid,
			//ParserFormulaGuid,
			//SmsSendFaildType,
			//SmsSendError,
			//DecreaseFromUser,
			//IsDeleted
		}

		public SmsSent()
			: base(TableNames.SmsSents.ToString())
		{
			AddField(TableFields.BatchId.ToString(), SqlDbType.BigInt);
			AddField(TableFields.ItemId.ToString(), SqlDbType.BigInt);
			AddField(TableFields.SendStatus.ToString(), SqlDbType.SmallInt);
			AddField(TableFields.WrapperDateTime.ToString(), SqlDbType.DateTime);
			AddField(TableFields.PrivateNumberGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.SenderId.ToString(), SqlDbType.NVarChar, 255);
			AddField(TableFields.SmsPriority.ToString(), SqlDbType.Int);
			AddField(TableFields.IsUnicode.ToString(), SqlDbType.Bit);
			AddField(TableFields.SmsLen.ToString(), SqlDbType.Int);
			AddField(TableFields.SmsText.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.SendingTryCount.ToString(), SqlDbType.Int);
			AddField(TableFields.SentDateTime.ToString(), SqlDbType.DateTime);
			AddField(TableFields.DeliveryNeeded.ToString(), SqlDbType.Bit);
			AddField(TableFields.Udh.ToString(), SqlDbType.Int);
			AddField(TableFields.SenderIp.ToString(), SqlDbType.NVarChar, 255);
			AddField(TableFields.IsFlash.ToString(), SqlDbType.Bit);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.SmsIdentifier.ToString(), SqlDbType.BigInt);
			AddField(TableFields.SmsPartIndex.ToString(), SqlDbType.Int);
		}

		public Guid SmsSentGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public long BatchId
		{
			get { return Helper.GetLong(this[TableFields.BatchId.ToString()]); }
			set { this[TableFields.BatchId.ToString()] = value; }
		}

		public long ItemId
		{
			get { return Helper.GetLong(this[TableFields.ItemId.ToString()]); }
			set { this[TableFields.ItemId.ToString()] = value; }
		}

		public int SendStatus
		{
			get { return Helper.GetInt(this[TableFields.SendStatus.ToString()]); }
			set { this[TableFields.SendStatus.ToString()] = value; }
		}

		public DateTime WrapperDateTime
		{
			get { return Helper.GetDateTime(this[TableFields.WrapperDateTime.ToString()]); }
			set { this[TableFields.WrapperDateTime.ToString()] = value; }
		}

		public Guid PrivateNumberGuid
		{
			get { return Helper.GetGuid(this[TableFields.PrivateNumberGuid.ToString()]); }
			set { this[TableFields.PrivateNumberGuid.ToString()] = value; }
		}

		public string SenderId
		{
			get { return Helper.GetString(this[TableFields.SenderId.ToString()]); }
			set { this[TableFields.SenderId.ToString()] = value; }
		}

		public int SmsPriority
		{
			get { return Helper.GetInt(this[TableFields.SmsPriority.ToString()]); }
			set { this[TableFields.SmsPriority.ToString()] = value; }
		}

		public bool IsUnicode
		{
			get { return Helper.GetBool(this[TableFields.IsUnicode.ToString()]); }
			set { this[TableFields.IsUnicode.ToString()] = value; }
		}

		public int SmsLen
		{
			get { return Helper.GetInt(this[TableFields.SmsLen.ToString()]); }
			set { this[TableFields.SmsLen.ToString()] = value; }
		}

		public string SmsText
		{
			get { return Helper.GetString(this[TableFields.SmsText.ToString()]); }
			set { this[TableFields.SmsText.ToString()] = value; }
		}

		public int SendingTryCount
		{
			get { return Helper.GetInt(this[TableFields.SendingTryCount.ToString()]); }
			set { this[TableFields.SendingTryCount.ToString()] = value; }
		}

		public DateTime SentDateTime
		{
			get { return Helper.GetDateTime(this[TableFields.SentDateTime.ToString()]); }
			set { this[TableFields.SentDateTime.ToString()] = value; }
		}

		public bool DeliveryNeeded
		{
			get { return Helper.GetBool(this[TableFields.DeliveryNeeded.ToString()]); }
			set { this[TableFields.DeliveryNeeded.ToString()] = value; }
		}

		public int Udh
		{
			get { return Helper.GetInt(this[TableFields.Udh.ToString()]); }
			set { this[TableFields.Udh.ToString()] = value; }
		}

		public string SenderIp
		{
			get { return Helper.GetString(this[TableFields.SenderIp.ToString()]); }
			set { this[TableFields.SenderIp.ToString()] = value; }
		}

		public bool IsFlash
		{
			get { return Helper.GetBool(this[TableFields.IsFlash.ToString()]); }
			set { this[TableFields.IsFlash.ToString()] = value; }
		}

		public Guid UserGuid
		{
			get { return Helper.GetGuid(this[TableFields.UserGuid.ToString()]); }
			set { this[TableFields.UserGuid.ToString()] = value; }
		}

		public long SmsIdentifier
		{
			get { return Helper.GetLong(this[TableFields.SmsIdentifier.ToString()]); }
			set { this[TableFields.SmsIdentifier.ToString()] = value; }
		}

		public int SmsPartIndex
		{
			get { return Helper.GetInt(this[TableFields.SmsPartIndex.ToString()]); }
			set { this[TableFields.SmsPartIndex.ToString()] = value; }
		}

		//public int UserDateFieldIndex
		//{
		//	get
		//	{
		//		return Helper.GetInt(this[TableFields.UserDateFieldIndex.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.UserDateFieldIndex.ToString()] = value;
		//	}
		//}

		//public string Reciever
		//{
		//	get
		//	{
		//		return Helper.GetString(this[TableFields.Reciever.ToString()]);
		//	}
		//	set
		//	{
		//		if (Helper.CheckDataConditions(value).IsEmpty)
		//		{
		//			ErrorMessage += Language.GetString("CompleteRecieverNumberField");
		//			HasError = true;
		//		}
		//		else
		//			this[TableFields.Reciever.ToString()] = value;
		//	}
		//}

		//public string BulkID
		//{
		//	get
		//	{
		//		return Helper.GetString(this[TableFields.BulkID.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.BulkID.ToString()] = value;
		//	}
		//}

		//public string SmsBody
		//{
		//	get
		//	{
		//		return Helper.GetString(this[TableFields.SmsBody.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.SmsBody.ToString()] = value;
		//	}
		//}

		//public int SmsCount
		//{
		//	get
		//	{
		//		return Helper.GetInt(this[TableFields.SmsCount.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.SmsCount.ToString()] = value;
		//	}
		//}

		//public int Encoding
		//{
		//	get
		//	{
		//		return Helper.GetInt(this[TableFields.Encoding.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.Encoding.ToString()] = value;
		//	}
		//}

		//public int RecipientsNumberCount
		//{
		//	get
		//	{
		//		return Helper.GetInt(this[TableFields.RecipientsNumberCount.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.RecipientsNumberCount.ToString()] = value;
		//	}
		//}

		//public int PresentType
		//{
		//	get
		//	{
		//		return Helper.GetInt(this[TableFields.PresentType.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.PresentType.ToString()] = value;
		//	}
		//}

		//public string DownRange
		//{
		//	get
		//	{
		//		return Helper.GetString(this[TableFields.DownRange.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.DownRange.ToString()] = value;
		//	}
		//}

		//public string UpRange
		//{
		//	get
		//	{
		//		return Helper.GetString(this[TableFields.UpRange.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.UpRange.ToString()] = value;
		//	}
		//}

		//public int Period
		//{
		//	get
		//	{
		//		return Helper.GetInt(this[TableFields.Period.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.Period.ToString()] = value;
		//	}
		//}

		//public int PeriodType
		//{
		//	get
		//	{
		//		return Helper.GetInt(this[TableFields.PeriodType.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.PeriodType.ToString()] = value;
		//	}
		//}

		//public Guid SmsFormatGuid
		//{
		//	get
		//	{
		//		return Helper.GetGuid(this[TableFields.SmsFormatGuid.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.SmsFormatGuid.ToString()] = value;
		//	}
		//}

		//public string AdvancedSearchQuery
		//{
		//	get
		//	{
		//		return Helper.GetString(this[TableFields.AdvancedSearchQuery.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.AdvancedSearchQuery.ToString()] = value;
		//	}
		//}

		//public DateTime LastPeriodSendDateTime
		//{
		//	get
		//	{
		//		return Helper.GetDateTime(this[TableFields.LastPeriodSendDateTime.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.LastPeriodSendDateTime.ToString()] = value;
		//	}
		//}

		//public int TypeSend
		//{
		//	get
		//	{
		//		return Helper.GetInt(this[TableFields.TypeSend.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.TypeSend.ToString()] = value;
		//	}
		//}

		//public int State
		//{
		//	get
		//	{
		//		return Helper.GetInt(this[TableFields.State.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.State.ToString()] = value;
		//	}
		//}

		//public DateTime DateTimeFuture
		//{
		//	get
		//	{
		//		return Helper.GetDateTime(this[TableFields.DateTimeFuture.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.DateTimeFuture.ToString()] = value;
		//	}
		//}

		//public DateTime StartDateTime
		//{
		//	get
		//	{
		//		return Helper.GetDateTime(this[TableFields.StartDateTime.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.StartDateTime.ToString()] = value;
		//	}
		//}

		//public DateTime EndDateTime
		//{
		//	get
		//	{
		//		return Helper.GetDateTime(this[TableFields.EndDateTime.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.EndDateTime.ToString()] = value;
		//	}
		//}

		//public DateTime CreateDate
		//{
		//	get
		//	{
		//		return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.CreateDate.ToString()] = value;
		//	}
		//}

		//public string GroupGuid
		//{
		//	get
		//	{
		//		return Helper.GetString(this[TableFields.GroupGuid.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.GroupGuid.ToString()] = value;
		//	}
		//}



		//public Guid ParserFormulaGuid
		//{
		//	get
		//	{
		//		return Helper.GetGuid(this[TableFields.ParserFormulaGuid.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.ParserFormulaGuid.ToString()] = value;
		//	}
		//}

		//public string SmsSendError
		//{
		//	get
		//	{
		//		return Helper.GetString(this[TableFields.SmsSendError.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.SmsSendError.ToString()] = value;
		//	}
		//}

		//public bool DecreaseFromUser
		//{
		//	get { return Helper.GetBool(this[TableFields.DecreaseFromUser.ToString()]); }
		//	set { this[TableFields.DecreaseFromUser.ToString()] = value; }
		//}
	}
}