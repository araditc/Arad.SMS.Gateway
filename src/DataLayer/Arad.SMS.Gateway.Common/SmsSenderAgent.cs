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
	public class SmsSenderAgent : CommonEntityBase
	{
		public enum TableFields
		{
			ID,
			Name,
			SmsSenderAgentReference,
			Type,
			SendSmsAlert,
			IsSendActive,
			IsRecieveActive,
			IsSendBulkActive,
			SendBulkIsAutomatic,
			CheckMessageID,
			CreateDate,
			DefaultNumber,
			StartSendTime,
			EndSendTime,
			RouteActive,
			QueueLength,
			IsSmpp,
			Username,
			Password,
			SendLink,
			ReceiveLink,
			DeliveryLink,
			Domain,
			UserGuid,
		}

		public SmsSenderAgent()
			: base(TableNames.SmsSenderAgents.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.Int);
			AddField(TableFields.Name.ToString(), SqlDbType.NVarChar, 64);
			AddField(TableFields.SmsSenderAgentReference.ToString(), SqlDbType.Int);
			AddField(TableFields.Type.ToString(), SqlDbType.TinyInt);
			AddField(TableFields.SendSmsAlert.ToString(), SqlDbType.Bit);
			AddField(TableFields.IsSendActive.ToString(), SqlDbType.Bit);
			AddField(TableFields.IsRecieveActive.ToString(), SqlDbType.Bit);
			AddField(TableFields.IsSendBulkActive.ToString(), SqlDbType.Bit);
			AddField(TableFields.SendBulkIsAutomatic.ToString(), SqlDbType.Bit);
			AddField(TableFields.CheckMessageID.ToString(), SqlDbType.Bit);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.DefaultNumber.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.StartSendTime.ToString(), SqlDbType.Time, 0);
			AddField(TableFields.EndSendTime.ToString(), SqlDbType.Time, 0);
			AddField(TableFields.RouteActive.ToString(), SqlDbType.Bit);
			AddField(TableFields.IsSmpp.ToString(), SqlDbType.Bit);
			AddField(TableFields.QueueLength.ToString(), SqlDbType.Int);
			AddField(TableFields.Username.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.Password.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.SendLink.ToString(), SqlDbType.NVarChar, 512);
			AddField(TableFields.ReceiveLink.ToString(), SqlDbType.NVarChar, 512);
			AddField(TableFields.DeliveryLink.ToString(), SqlDbType.NVarChar, 512);
			AddField(TableFields.Domain.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid SmsSenderAgentGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public int ID
		{
			get
			{
				return Helper.GetInt(this[TableFields.ID.ToString()]);
			}
		}

		public string Name
		{
			get
			{
				return Helper.GetString(this[TableFields.Name.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteNameField");
					HasError = true;
				}
				else
					this[TableFields.Name.ToString()] = value;
			}
		}

		public bool SendSmsAlert
		{
			get
			{
				return Helper.GetBool(this[TableFields.SendSmsAlert.ToString()]);
			}
			set
			{
				this[TableFields.SendSmsAlert.ToString()] = value;
			}
		}

		public bool IsSendActive
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsSendActive.ToString()]);
			}
			set
			{
				this[TableFields.IsSendActive.ToString()] = value;
			}
		}

		public bool IsRecieveActive
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsRecieveActive.ToString()]);
			}
			set
			{
				this[TableFields.IsRecieveActive.ToString()] = value;
			}
		}

		public bool IsSendBulkActive
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsSendBulkActive.ToString()]);
			}
			set
			{
				this[TableFields.IsSendBulkActive.ToString()] = value;
			}
		}

		public bool SendBulkIsAutomatic
		{
			get
			{
				return Helper.GetBool(this[TableFields.SendBulkIsAutomatic.ToString()]);
			}
			set
			{
				this[TableFields.SendBulkIsAutomatic.ToString()] = value;
			}
		}

		public bool CheckMessageID
		{
			get
			{
				return Helper.GetBool(this[TableFields.CheckMessageID.ToString()]);
			}
			set
			{
				this[TableFields.CheckMessageID.ToString()] = value;
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

		public int Type
		{
			get
			{
				return Helper.GetInt(this[TableFields.Type.ToString()]);
			}
			set
			{
				this[TableFields.Type.ToString()] = value;
			}
		}

		public string DefaultNumber
		{
			get
			{
				return Helper.GetString(this[TableFields.DefaultNumber.ToString()]);
			}
			set
			{
				this[TableFields.DefaultNumber.ToString()] = value;
			}
		}

		public TimeSpan StartSendTime
		{
			get
			{
				return Helper.GetTimeSpan(this[TableFields.StartSendTime.ToString()]);
			}
			set
			{
				this[TableFields.StartSendTime.ToString()] = value;
			}
		}

		public TimeSpan EndSendTime
		{
			get
			{
				return Helper.GetTimeSpan(this[TableFields.EndSendTime.ToString()]);
			}
			set
			{
				this[TableFields.EndSendTime.ToString()] = value;
			}
		}

		public bool RouteActive
		{
			get
			{
				return Helper.GetBool(this[TableFields.RouteActive.ToString()]);
			}
			set
			{
				this[TableFields.RouteActive.ToString()] = value;
			}
		}

		public bool IsSmpp
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsSmpp.ToString()]);
			}
			set
			{
				this[TableFields.IsSmpp.ToString()] = value;
			}
		}

		public int QueueLength
		{
			get
			{
				return Helper.GetInt(this[TableFields.QueueLength.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty ||
					 Helper.GetInt(value) <= 0)
				{
					ErrorMessage += Language.GetString("QueueLengthIsInvalid");
					HasError = true;
				}
				else
					this[TableFields.QueueLength.ToString()] = value;
			}
		}

		public string Username
		{
			get
			{
				return Helper.GetString(this[TableFields.Username.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteUserNameField");
					HasError = true;
				}
				else
					this[TableFields.Username.ToString()] = value;
			}
		}

		public string Password
		{
			get
			{
				return Helper.GetString(this[TableFields.Password.ToString()]);
			}
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompletePasswordField");
					HasError = true;
				}
				else
					this[TableFields.Password.ToString()] = value;
			}
		}

		public string SendLink
		{
			get
			{
				return Helper.GetString(this[TableFields.SendLink.ToString()]);
			}
			set
			{
				this[TableFields.SendLink.ToString()] = value;
			}
		}

		public string ReceiveLink
		{
			get
			{
				return Helper.GetString(this[TableFields.ReceiveLink.ToString()]);
			}
			set
			{
				this[TableFields.ReceiveLink.ToString()] = value;
			}
		}

		public string DeliveryLink
		{
			get
			{
				return Helper.GetString(this[TableFields.DeliveryLink.ToString()]);
			}
			set
			{
				this[TableFields.DeliveryLink.ToString()] = value;
			}
		}

		public string Domain
		{
			get
			{
				return Helper.GetString(this[TableFields.Domain.ToString()]);
			}
			set
			{
				this[TableFields.Domain.ToString()] = value;
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