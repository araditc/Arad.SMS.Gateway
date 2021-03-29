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
	public class ScheduledSms : CommonEntityBase
	{
		private enum TableFields
		{
			ID,
			PrivateNumberGuid,
			SmsText,
			PresentType,
			Encoding,
			SmsLen,
			FilePath,
			SmsPattern,
			DownRange,
			UpRange,
			Period,
			PeriodType,
			SendPageNo,
			SendPageSize,
			RequestXML,
			TypeSend,
			DateTimeFuture,
			StartDateTime,
			EndDateTime,
			CreateDate,
			ReferenceGuid,
			SmsSenderAgentReference,
			SmsSendFaildType,
			Status,
			SmsSendError,
			IsDeleted,
			UserGuid,
            VoiceURL,
            VoiceMessageId,
        }

		public ScheduledSms()
			: base(TableNames.ScheduledSmses.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.BigInt);
			AddField(TableFields.PrivateNumberGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.SmsText.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.PresentType.ToString(), SqlDbType.Int);
			AddField(TableFields.Encoding.ToString(), SqlDbType.Int);
			AddField(TableFields.SmsLen.ToString(), SqlDbType.Int);
			AddField(TableFields.FilePath.ToString(), SqlDbType.NVarChar, 128);
			AddField(TableFields.SmsPattern.ToString(), SqlDbType.NVarChar, 1024);
			AddField(TableFields.DownRange.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.UpRange.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Period.ToString(), SqlDbType.Int);
			AddField(TableFields.PeriodType.ToString(), SqlDbType.Int);
			AddField(TableFields.SendPageNo.ToString(), SqlDbType.Int);
			AddField(TableFields.SendPageSize.ToString(), SqlDbType.Int);
			AddField(TableFields.RequestXML.ToString(), SqlDbType.Xml);
			AddField(TableFields.TypeSend.ToString(), SqlDbType.Int);
			AddField(TableFields.DateTimeFuture.ToString(), SqlDbType.DateTime);
			AddField(TableFields.StartDateTime.ToString(), SqlDbType.DateTime);
			AddField(TableFields.EndDateTime.ToString(), SqlDbType.DateTime);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.ReferenceGuid.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.SmsSenderAgentReference.ToString(), SqlDbType.Int);
			AddField(TableFields.SmsSendFaildType.ToString(), SqlDbType.Int);
			AddField(TableFields.Status.ToString(), SqlDbType.Int);
			AddField(TableFields.SmsSendError.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
            AddField(TableFields.VoiceURL.ToString(), SqlDbType.NVarChar, short.MaxValue);
            AddField(TableFields.VoiceMessageId.ToString(), SqlDbType.Int);
        }

		public Guid ScheduledSmsGuid
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

		public Guid PrivateNumberGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.PrivateNumberGuid.ToString()]);
			}
			set
			{
				if (Helper.GetGuid(value) == Guid.Empty)
				{
					ErrorMessage += Language.GetString("InvalidSenderNumber");
					HasError = true;
				}
				else
					this[TableFields.PrivateNumberGuid.ToString()] = value;
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
				if (value == string.Empty)
				{
					ErrorMessage += Language.GetString("BlankMessage");
					HasError = true;
				}
				else
					this[TableFields.SmsText.ToString()] = value;
			}
		}

		public int PresentType
		{
			get
			{
				return Helper.GetInt(this[TableFields.PresentType.ToString()]);
			}
			set
			{
				this[TableFields.PresentType.ToString()] = value;
			}
		}

		public int Encoding
		{
			get
			{
				return Helper.GetInt(this[TableFields.Encoding.ToString()]);
			}
			set
			{
				this[TableFields.Encoding.ToString()] = value;
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

		public string FilePath
		{
			get
			{
				return Helper.GetString(this[TableFields.FilePath.ToString()]);
			}
			set
			{
				this[TableFields.FilePath.ToString()] = value;
			}
		}

		public string SmsPattern
		{
			get
			{
				return Helper.GetString(this[TableFields.SmsPattern.ToString()]);
			}
			set
			{
				this[TableFields.SmsPattern.ToString()] = value;
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

		public int Period
		{
			get
			{
				return Helper.GetInt(this[TableFields.Period.ToString()]);
			}
			set
			{
				this[TableFields.Period.ToString()] = value;
			}
		}

		public int PeriodType
		{
			get
			{
				return Helper.GetInt(this[TableFields.PeriodType.ToString()]);
			}
			set
			{
				this[TableFields.PeriodType.ToString()] = value;
			}
		}

		public int SendPageNo
		{
			get
			{
				return Helper.GetInt(this[TableFields.SendPageNo.ToString()]);
			}
			set
			{
				this[TableFields.SendPageNo.ToString()] = value;
			}
		}

		public int SendPageSize
		{
			get
			{
				return Helper.GetInt(this[TableFields.SendPageSize.ToString()]);
			}
			set
			{
				this[TableFields.SendPageSize.ToString()] = value;
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

		public DateTime StartDateTime
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.StartDateTime.ToString()]);
			}
			set
			{
				this[TableFields.StartDateTime.ToString()] = value;
			}
		}

		public DateTime EndDateTime
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.EndDateTime.ToString()]);
			}
			set
			{
				this[TableFields.EndDateTime.ToString()] = value;
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

		public string ReferenceGuid
		{
			get
			{
				return Helper.GetString(this[TableFields.ReferenceGuid.ToString()]);
			}
			set
			{
				this[TableFields.ReferenceGuid.ToString()] = value;
			}
		}

		public int SmsSenderAgentReference
		{
			get
			{
				return Helper.GetInt(this[TableFields.SmsSenderAgentReference.ToString()]);
			}
			set
			{
				this[TableFields.SmsSenderAgentReference.ToString()] = value;
			}
		}

		public int SmsSendFaildType
		{
			get
			{
				return Helper.GetInt(this[TableFields.SmsSendFaildType.ToString()]);
			}
			set
			{
				this[TableFields.SmsSendFaildType.ToString()] = value;
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

		public string SmsSendError
		{
			get
			{
				return Helper.GetString(this[TableFields.SmsSendError.ToString()]);
			}
			set
			{
				this[TableFields.SmsSendError.ToString()] = value;
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

        public int VoiceMessageId
        {
            get
            {
                return Helper.GetInt(this[TableFields.VoiceMessageId.ToString()]);
            }
            set
            {
                this[TableFields.VoiceMessageId.ToString()] = value;
            }
        }

        public string VoiceURL
        {
            get
            {
                return Helper.GetString(this[TableFields.VoiceURL.ToString()]);
            }
            set
            {
                this[TableFields.VoiceURL.ToString()] = value;
            }
        }
    }
}
