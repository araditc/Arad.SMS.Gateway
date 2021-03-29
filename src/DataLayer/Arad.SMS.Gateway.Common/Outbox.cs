// --------------------------------------------------------------------
// Copyright (c) 2005-2020 Arad ITC.
//
// Author : Ammar Heidari <ammar@arad-itc.org>
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------

using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Arad.SMS.Gateway.Common
{
	public class Outbox : CommonEntityBase
	{
		private enum TableFields
		{
			ID,
			CheckId,
			ExportDataPageNo,
			ExportDataStatus,
			ExportTxtPageNo,
			ExportTxtStatus,
			SendStatus,
			Price,
			ReceiverCount,
			SavedReceiverCount,
			DeliveredCount,
			FailedCount,
			SentToICTCount,
			DeliveredICTCount,
			BlackListCount,
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
			SmsSendType,
			SmsIdentifier,
			SmsPartIndex,
			RequestXML,
			UserGuid
		}

		public Outbox()
			: base(TableNames.Outboxes.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.BigInt);
			AddField(TableFields.CheckId.ToString(), SqlDbType.NVarChar, 64);
			AddField(TableFields.ExportDataPageNo.ToString(), SqlDbType.Int);
			AddField(TableFields.ExportDataStatus.ToString(), SqlDbType.TinyInt);
			AddField(TableFields.ExportTxtPageNo.ToString(), SqlDbType.Int);
			AddField(TableFields.ExportTxtStatus.ToString(), SqlDbType.TinyInt);
			AddField(TableFields.SendStatus.ToString(), SqlDbType.SmallInt);
			AddField(TableFields.Price.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.ReceiverCount.ToString(), SqlDbType.Int);
			AddField(TableFields.SavedReceiverCount.ToString(), SqlDbType.Int);
			AddField(TableFields.DeliveredCount.ToString(), SqlDbType.Int);
			AddField(TableFields.FailedCount.ToString(), SqlDbType.Int);
			AddField(TableFields.SentToICTCount.ToString(), SqlDbType.Int);
			AddField(TableFields.DeliveredICTCount.ToString(), SqlDbType.Int);
			AddField(TableFields.BlackListCount.ToString(), SqlDbType.Int);
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
			AddField(TableFields.SmsSendType.ToString(), SqlDbType.Int);
			AddField(TableFields.SmsIdentifier.ToString(), SqlDbType.BigInt);
			AddField(TableFields.SmsPartIndex.ToString(), SqlDbType.Int);
			AddField(TableFields.RequestXML.ToString(), SqlDbType.Xml);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid OutboxGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public long ID
		{
			get
			{
				return Helper.GetLong(this[TableFields.ID.ToString()]);
			}
			set
			{
				this[TableFields.ID.ToString()] = value;
			}
		}

		public long CheckId
		{
			get
			{
				return Helper.GetLong(this[TableFields.CheckId.ToString()]);
			}
			set
			{
				this[TableFields.CheckId.ToString()] = value;
			}
		}

		public int ExportDataStatus
		{
			get
			{
				return Helper.GetInt(this[TableFields.ExportDataStatus.ToString()]);
			}
			set
			{
				this[TableFields.ExportDataStatus.ToString()] = value;
			}
		}

		public int ExportTxtStatus
		{
			get
			{
				return Helper.GetInt(this[TableFields.ExportTxtStatus.ToString()]);
			}
			set
			{
				this[TableFields.ExportTxtStatus.ToString()] = value;
			}
		}

		public int SendStatus
		{
			get
			{
				return Helper.GetInt(this[TableFields.SendStatus.ToString()]);
			}
			set
			{
				this[TableFields.SendStatus.ToString()] = value;
			}
		}

		public decimal Price
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.Price.ToString()]);
			}
			set
			{
				this[TableFields.Price.ToString()] = value;
			}
		}

		public int ReceiverCount
		{
			get
			{
				return Helper.GetInt(this[TableFields.ReceiverCount.ToString()]);
			}
			set
			{
				this[TableFields.ReceiverCount.ToString()] = value;
			}
		}

		public DateTime WrapperDateTime
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.WrapperDateTime.ToString()]);
			}
			set
			{
				this[TableFields.WrapperDateTime.ToString()] = value;
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

		public string SenderId
		{
			get
			{
				return Helper.GetString(this[TableFields.SenderId.ToString()]);
			}
			set
			{
				this[TableFields.SenderId.ToString()] = value;
			}
		}

		public int SmsPriority
		{
			get
			{
				return Helper.GetInt(this[TableFields.SmsPriority.ToString()]);
			}
			set
			{
				this[TableFields.SmsPriority.ToString()] = value;
			}
		}

		public bool IsUnicode
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsUnicode.ToString()]);
			}
			set
			{
				this[TableFields.IsUnicode.ToString()] = value;
			}
		}

		public int SmsLen
		{
			get
			{
				return Helper.GetInt(this[TableFields.SmsLen.ToString()]);
			}
			set
			{
				this[TableFields.SmsLen.ToString()] = value;
			}
		}

		public string SmsText
		{
			get
			{
				return Helper.GetString(this[TableFields.SmsText.ToString()]);
			}
			set
			{
				this[TableFields.SmsText.ToString()] = value;
			}
		}

		public int SendingTryCount
		{
			get
			{
				return Helper.GetInt(this[TableFields.SendingTryCount.ToString()]);
			}
			set
			{
				this[TableFields.SendingTryCount.ToString()] = value;
			}
		}

		public DateTime SentDateTime
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.SentDateTime.ToString()]);
			}
			set
			{
				this[TableFields.SentDateTime.ToString()] = value;
			}
		}

		public bool DeliveryNeeded
		{
			get
			{
				return Helper.GetBool(this[TableFields.DeliveryNeeded.ToString()]);
			}
			set
			{
				this[TableFields.DeliveryNeeded.ToString()] = value;
			}
		}

		public int Udh
		{
			get
			{
				return Helper.GetInt(this[TableFields.Udh.ToString()]);
			}
			set
			{
				this[TableFields.Udh.ToString()] = value;
			}
		}

		public string SenderIp
		{
			get
			{
				return Helper.GetString(this[TableFields.SenderIp.ToString()]);
			}
			set
			{
				this[TableFields.SenderIp.ToString()] = value;
			}
		}

		public bool IsFlash
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsFlash.ToString()]);
			}
			set
			{
				this[TableFields.IsFlash.ToString()] = value;
			}
		}

		public long SmsIdentifier
		{
			get
			{
				return Helper.GetLong(this[TableFields.SmsIdentifier.ToString()]);
			}
			set
			{
				this[TableFields.SmsIdentifier.ToString()] = value;
			}
		}

		public int SmsPartIndex
		{
			get
			{
				return Helper.GetInt(this[TableFields.SmsPartIndex.ToString()]);
			}
			set
			{
				this[TableFields.SmsPartIndex.ToString()] = value;
			}
		}

		public string RequestXML
		{
			get
			{
				return Helper.GetString(this[TableFields.RequestXML.ToString()]);
			}
			set
			{
				this[TableFields.RequestXML.ToString()] = value;
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