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
	public class PrivateNumber : CommonEntityBase
	{
		private enum TableFields
		{
			ID,
			Number,
			Price,
			ServiceID,
			MTNServiceId,
			AggServiceId,
			ServicePrice,
			CreateDate,
			ExpireDate,
			Type,
			Priority,
			ReturnBlackList,
			SendToBlackList,
			CheckFilter,
			DeliveryBase,
			HasSLA,
			TryCount,
			Range,
			Regex,
			UseForm,
			IsDeleted,
			ParentGuid,
			OwnerGuid,
			IsRoot,
			IsActive,
			IsDefault,
			IsPublic,
			SendCount,
			RecieveCount,
			SuccessCount,
			SmsSenderAgentGuid,
			SmsTrafficRelayGuid,
			DeliveryTrafficRelayGuid,
			UserGuid,
		}

		public PrivateNumber()
			: base(TableNames.PrivateNumbers.ToString())
		{
			AddField(TableFields.ID.ToString(), SqlDbType.BigInt);
			AddField(TableFields.Number.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.Price.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.ServiceID.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.MTNServiceId.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.AggServiceId.ToString(), SqlDbType.NVarChar, 32);
			AddField(TableFields.ServicePrice.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.ExpireDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.Type.ToString(), SqlDbType.Int);
			AddField(TableFields.Priority.ToString(), SqlDbType.Int);
			AddField(TableFields.ReturnBlackList.ToString(), SqlDbType.Bit);
			AddField(TableFields.SendToBlackList.ToString(), SqlDbType.Bit);
			AddField(TableFields.CheckFilter.ToString(), SqlDbType.Bit);
			AddField(TableFields.DeliveryBase.ToString(), SqlDbType.Bit);
			AddField(TableFields.HasSLA.ToString(), SqlDbType.Bit);
			AddField(TableFields.TryCount.ToString(), SqlDbType.Int);
			AddField(TableFields.Range.ToString(), SqlDbType.NVarChar, 255);
			AddField(TableFields.Regex.ToString(), SqlDbType.NVarChar, 255);
			AddField(TableFields.UseForm.ToString(), SqlDbType.Int);
			AddField(TableFields.ParentGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.OwnerGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.IsRoot.ToString(), SqlDbType.Bit);
			AddField(TableFields.IsActive.ToString(), SqlDbType.Bit);
			AddField(TableFields.IsDefault.ToString(), SqlDbType.Bit);
			AddField(TableFields.IsPublic.ToString(), SqlDbType.Bit);
			AddField(TableFields.SendCount.ToString(), SqlDbType.BigInt);
			AddField(TableFields.RecieveCount.ToString(), SqlDbType.BigInt);
			AddField(TableFields.SuccessCount.ToString(), SqlDbType.BigInt);
			AddField(TableFields.SmsSenderAgentGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.SmsTrafficRelayGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.DeliveryTrafficRelayGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
		}

		public Guid NumberGuid
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
		}

		public string Number
		{
			get
			{
				return Helper.GetString(this[TableFields.Number.ToString()]);
			}
			set
			{
				this[TableFields.Number.ToString()] = value;
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

		public string ServiceID
		{
			get
			{
				return Helper.GetString(this[TableFields.ServiceID.ToString()]);
			}
			set
			{
				this[TableFields.ServiceID.ToString()] = value;
			}
		}

		public string MTNServiceId
		{
			get
			{
				return Helper.GetString(this[TableFields.MTNServiceId.ToString()]);
			}
			set
			{
				this[TableFields.MTNServiceId.ToString()] = value;
			}
		}

		public string AggServiceId
		{
			get
			{
				return Helper.GetString(this[TableFields.AggServiceId.ToString()]);
			}
			set
			{
				this[TableFields.AggServiceId.ToString()] = value;
			}
		}

		public decimal ServicePrice
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.ServicePrice.ToString()]);
			}
			set
			{
				this[TableFields.ServicePrice.ToString()] = value;
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

		public DateTime ExpireDate
		{
			get
			{
				return Helper.GetDateTime(this[TableFields.ExpireDate.ToString()]);
			}
			set
			{
				this[TableFields.ExpireDate.ToString()] = value;
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

		public int Priority
		{
			get
			{
				return Helper.GetInt(this[TableFields.Priority.ToString()]);
			}
			set
			{
				this[TableFields.Priority.ToString()] = value;
			}
		}

		public bool ReturnBlackList
		{
			get
			{
				return Helper.GetBool(this[TableFields.ReturnBlackList.ToString()]);
			}
			set
			{
				this[TableFields.ReturnBlackList.ToString()] = value;
			}
		}

		public bool SendToBlackList
		{
			get
			{
				return Helper.GetBool(this[TableFields.SendToBlackList.ToString()]);
			}
			set
			{
				this[TableFields.SendToBlackList.ToString()] = value;
			}
		}

		public bool CheckFilter
		{
			get
			{
				return Helper.GetBool(this[TableFields.CheckFilter.ToString()]);
			}
			set
			{
				this[TableFields.CheckFilter.ToString()] = value;
			}
		}

		public bool DeliveryBase
		{
			get
			{
				return Helper.GetBool(this[TableFields.DeliveryBase.ToString()]);
			}
			set
			{
				this[TableFields.DeliveryBase.ToString()] = value;
			}
		}

		public bool HasSLA
		{
			get
			{
				return Helper.GetBool(this[TableFields.HasSLA.ToString()]);
			}
			set
			{
				this[TableFields.HasSLA.ToString()] = value;
			}
		}

		public int TryCount
		{
			get
			{
				return Helper.GetInt(this[TableFields.TryCount.ToString()]);
			}
			set
			{
				this[TableFields.TryCount.ToString()] = value;
			}
		}

		public string Range
		{
			get
			{
				return Helper.GetString(this[TableFields.Range.ToString()]);
			}
			set
			{
				this[TableFields.Range.ToString()] = value;
			}
		}

		public string Regex
		{
			get
			{
				return Helper.GetString(this[TableFields.Regex.ToString()]);
			}
			set
			{
				this[TableFields.Regex.ToString()] = value;
			}
		}

		public int UseForm
		{
			get
			{
				return Helper.GetInt(this[TableFields.UseForm.ToString()]);
			}
			set
			{
				this[TableFields.UseForm.ToString()] = value;
			}
		}

		public Guid ParentGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.ParentGuid.ToString()]);
			}
			set
			{
				this[TableFields.ParentGuid.ToString()] = value;
			}
		}

		public Guid OwnerGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.OwnerGuid.ToString()]);
			}
			set
			{
				this[TableFields.OwnerGuid.ToString()] = value;
			}
		}

		public bool IsRoot
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsRoot.ToString()]);
			}
			set
			{
				this[TableFields.IsRoot.ToString()] = value;
			}
		}

		public bool IsActive
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsActive.ToString()]);
			}
			set
			{
				this[TableFields.IsActive.ToString()] = value;
			}
		}

		public bool IsDefault
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsDefault.ToString()]);
			}
			set
			{
				this[TableFields.IsDefault.ToString()] = value;
			}
		}

		public bool IsPublic
		{
			get
			{
				return Helper.GetBool(this[TableFields.IsPublic.ToString()]);
			}
			set
			{
				this[TableFields.IsPublic.ToString()] = value;
			}
		}

		public long SendCount
		{
			get
			{
				return Helper.GetLong(this[TableFields.SendCount.ToString()]);
			}
			set
			{
				this[TableFields.SendCount.ToString()] = value;
			}
		}

		public long RecieveCount
		{
			get
			{
				return Helper.GetLong(this[TableFields.RecieveCount.ToString()]);
			}
			set
			{
				this[TableFields.RecieveCount.ToString()] = value;
			}
		}

		public long SuccessCount
		{
			get
			{
				return Helper.GetLong(this[TableFields.SuccessCount.ToString()]);
			}
			set
			{
				this[TableFields.SuccessCount.ToString()] = value;
			}
		}


		public Guid SmsSenderAgentGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.SmsSenderAgentGuid.ToString()]);
			}
			set
			{
				this[TableFields.SmsSenderAgentGuid.ToString()] = value;
			}
		}

		public Guid SmsTrafficRelayGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.SmsTrafficRelayGuid.ToString()]);
			}
			set
			{
				this[TableFields.SmsTrafficRelayGuid.ToString()] = value;
			}
		}

		public Guid DeliveryTrafficRelayGuid
		{
			get
			{
				return Helper.GetGuid(this[TableFields.DeliveryTrafficRelayGuid.ToString()]);
			}
			set
			{
				this[TableFields.DeliveryTrafficRelayGuid.ToString()] = value;
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
