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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;

namespace Arad.SMS.Gateway.Common
{
	public class OutboxNumber : CommonEntityBase
	{
		private enum TableFields
		{
			BatchId,
			ItemId,
			ToNumber,
			DeliveryStatus,
			StatusDateTime,
			ReturnId,
			CheckId,
			SendStatus,
			SendDeliveryStatus,
			SendDeliveryToUrlCount,
			HasSign,
			Operator,
			SmsSenderAgentReference,
			OutboxGuid,
		}

		public OutboxNumber()
			: base(TableNames.OutboxNumbers.ToString())
		{
			AddField(TableFields.BatchId.ToString(), SqlDbType.BigInt);
			AddField(TableFields.ItemId.ToString(), SqlDbType.NVarChar, 64);
			AddField(TableFields.ToNumber.ToString(), SqlDbType.NVarChar, 255);
			AddField(TableFields.DeliveryStatus.ToString(), SqlDbType.SmallInt);
			AddField(TableFields.StatusDateTime.ToString(), SqlDbType.DateTime);
			AddField(TableFields.ReturnId.ToString(), SqlDbType.NVarChar, 255);
			AddField(TableFields.CheckId.ToString(), SqlDbType.NVarChar, 255);
			AddField(TableFields.SendStatus.ToString(), SqlDbType.SmallInt);
			AddField(TableFields.SendDeliveryStatus.ToString(), SqlDbType.Bit);
			AddField(TableFields.SendDeliveryToUrlCount.ToString(), SqlDbType.SmallInt);
			AddField(TableFields.HasSign.ToString(), SqlDbType.Bit);
			AddField(TableFields.Operator.ToString(), SqlDbType.TinyInt);
			AddField(TableFields.SmsSenderAgentReference.ToString(), SqlDbType.Int);
			AddField(TableFields.OutboxGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid OutboxNumberGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public long BatchId
		{
			get { return Helper.GetLong(this[TableFields.BatchId.ToString()]); }
			set { this[TableFields.BatchId.ToString()] = value; }
		}

		public string ItemId
		{
			get { return Helper.GetString(this[TableFields.ItemId.ToString()]); }
			set { this[TableFields.ItemId.ToString()] = value; }
		}

		public string ToNumber
		{
			get { return Helper.GetString(this[TableFields.ToNumber.ToString()]); }
			set { this[TableFields.ToNumber.ToString()] = value; }
		}

		public int DeliveryStatus
		{
			get { return Helper.GetInt(this[TableFields.DeliveryStatus.ToString()]); }
			set { this[TableFields.DeliveryStatus.ToString()] = value; }
		}

		public DateTime StatusDateTime
		{
			get { return Helper.GetDateTime(this[TableFields.StatusDateTime.ToString()]); }
			set { this[TableFields.StatusDateTime.ToString()] = value; }
		}

		public string ReturnId
		{
			get { return Helper.GetString(this[TableFields.ReturnId.ToString()]); }
			set { this[TableFields.ReturnId.ToString()] = value; }
		}

		public string CheckId
		{
			get { return Helper.GetString(this[TableFields.CheckId.ToString()]); }
			set { this[TableFields.CheckId.ToString()] = value; }
		}

		public int SendStatus
		{
			get { return Helper.GetInt(this[TableFields.SendStatus.ToString()]); }
			set { this[TableFields.SendStatus.ToString()] = value; }
		}

		public bool SendDeliveryStatus
		{
			get { return Helper.GetBool(this[TableFields.SendDeliveryStatus.ToString()]); }
			set { this[TableFields.SendDeliveryStatus.ToString()] = value; }
		}

		public int SendDeliveryToUrlCount
		{
			get { return Helper.GetInt(this[TableFields.SendDeliveryToUrlCount.ToString()]); }
			set { this[TableFields.SendDeliveryToUrlCount.ToString()] = value; }
		}

		public bool HasSign
		{
			get { return Helper.GetBool(this[TableFields.HasSign.ToString()]); }
			set { this[TableFields.HasSign.ToString()] = value; }
		}

		public byte Operator
		{
			get { return Helper.GetByte(this[TableFields.Operator.ToString()]); }
			set { this[TableFields.Operator.ToString()] = value; }
		}

		public int SmsSenderAgentReference
		{
			get { return Helper.GetInt(this[TableFields.SmsSenderAgentReference.ToString()]); }
			set { this[TableFields.SmsSenderAgentReference.ToString()] = value; }
		}

		public Guid OutboxGuid
		{
			get { return Helper.GetGuid(this[TableFields.OutboxGuid.ToString()]); }
			set { this[TableFields.OutboxGuid.ToString()] = value; }
		}

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

		//public Guid PrivateNumberGuid
		//{
		//	get
		//	{
		//		return Helper.GetGuid(this[TableFields.PrivateNumberGuid.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.PrivateNumberGuid.ToString()] = value;
		//	}
		//}

		//public Guid UserGuid
		//{
		//	get
		//	{
		//		return Helper.GetGuid(this[TableFields.UserGuid.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.UserGuid.ToString()] = value;
		//	}
		//}

		//public int DeliveryStatus
		//{
		//	get
		//	{
		//		return Helper.GetInt(this[TableFields.DeliveryStatus.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.DeliveryStatus.ToString()] = value;
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

		//public DateTime EffectiveDateTime
		//{
		//	get
		//	{
		//		return Helper.GetDateTime(this[TableFields.EffectiveDateTime.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.EffectiveDateTime.ToString()] = value;
		//	}
		//}



		//public string OuterSystemSmsID
		//{
		//	get
		//	{
		//		return Helper.GetString(this[TableFields.OuterSystemSmsID.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.OuterSystemSmsID.ToString()] = value;
		//	}
		//}

		//public string CheckingMessageID
		//{
		//	get
		//	{
		//		return Helper.GetString(this[TableFields.CheckingMessageID.ToString()]);
		//	}
		//	set
		//	{
		//		this[TableFields.CheckingMessageID.ToString()] = value;
		//	}
		//}
	}
}
