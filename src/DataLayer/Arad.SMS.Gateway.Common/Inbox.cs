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
	public class Inbox : CommonEntityBase
	{
		public enum TableFields
		{
			Status,
			Receiver,
			Sender,
			SmsLen,
			SmsText,
			ReceiveDateTime,
			SendDateTime,
			IsUnicode,
			IsSent,
			IsRead,
			ShowAlert,
			SendSmsToUrlCount,
			Udh,
			IsDeleted,
			UserGuid,
			PrivateNumberGuid,
			InboxGroupGuid,
			ParserFormulaGuid
		}

		public Inbox()
			: base(TableNames.Inboxes.ToString())
		{
			AddField(TableFields.Status.ToString(), SqlDbType.SmallInt);
			AddField(TableFields.Receiver.ToString(), SqlDbType.NVarChar, 255);
			AddField(TableFields.Sender.ToString(), SqlDbType.NVarChar, 25);
			AddField(TableFields.SmsLen.ToString(), SqlDbType.Int);
			AddField(TableFields.SmsText.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.ReceiveDateTime.ToString(), SqlDbType.DateTime);
			AddField(TableFields.SendDateTime.ToString(), SqlDbType.DateTime);
			AddField(TableFields.IsUnicode.ToString(), SqlDbType.Bit);
			AddField(TableFields.IsSent.ToString(), SqlDbType.Bit);
			AddField(TableFields.IsRead.ToString(), SqlDbType.Bit);
			AddField(TableFields.ShowAlert.ToString(), SqlDbType.Bit);
			AddField(TableFields.SendSmsToUrlCount.ToString(), SqlDbType.Int);
			AddField(TableFields.Udh.ToString(), SqlDbType.Int);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.PrivateNumberGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.InboxGroupGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.ParserFormulaGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid InboxGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public int Status
		{
			get { return Helper.GetInt(this[TableFields.Status.ToString()]); }
			set { this[TableFields.Status.ToString()] = value; }
		}

		public string Receiver
		{
			get
			{
				return Helper.GetString(this[TableFields.Receiver.ToString()]);
			}
			set
			{
				this[TableFields.Receiver.ToString()] = value;
			}
		}

		public string Sender
		{
			get
			{
				return Helper.GetString(this[TableFields.Sender.ToString()]);
			}
			set
			{
				this[TableFields.Sender.ToString()] = value;
			}
		}

		public int SmsLen
		{
			get { return Helper.GetInt(this[TableFields.SmsLen.ToString()]); }
			set { this[TableFields.SmsLen.ToString()] = value; }
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

		public DateTime ReceiveDateTime
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.ReceiveDateTime.ToString()]);
			}
			set
			{
				this[TableFields.ReceiveDateTime.ToString()] = value;
			}
		}

		public DateTime SendDateTime
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.SendDateTime.ToString()]);
			}
			set
			{
				this[TableFields.SendDateTime.ToString()] = value;
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

		public bool IsSent
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsSent.ToString()]);
			}
			set
			{
				this[TableFields.IsSent.ToString()] = value;
			}
		}

		public bool IsRead
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsRead.ToString()]);
			}
			set
			{
				this[TableFields.IsRead.ToString()] = value;
			}
		}

		public bool ShowAlert
		{
			get
			{
				return Helper.GetBool(this[TableFields.ShowAlert.ToString()]);
			}
			set
			{
				this[TableFields.ShowAlert.ToString()] = value;
			}
		}

		public int SendSmsToUrlCount
		{
			get { return Helper.GetInt(this[TableFields.SendSmsToUrlCount.ToString()]); }
			set { this[TableFields.SendSmsToUrlCount.ToString()] = value; }
		}

		public int Udh
		{
			get { return Helper.GetInt(this[TableFields.Udh.ToString()]); }
			set { this[TableFields.Udh.ToString()] = value; }
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

		public Guid InboxGroupGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.InboxGroupGuid.ToString()]);
			}
			set
			{
				this[TableFields.InboxGroupGuid.ToString()] = value;
			}
		}

		public Guid ParserFormulaGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.ParserFormulaGuid.ToString()]);
			}
			set
			{
				this[TableFields.ParserFormulaGuid.ToString()] = value;
			}
		}
	}
}
