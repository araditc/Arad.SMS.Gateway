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
	public class Fish : CommonEntityBase
	{
		public enum TableFields
		{
			ReferenceID,
			CreateDate,
			PaymentDate,
			SmsCount,
			Amount,
			OrderID,
			BillNumber,
			Description,
			Type,
			Status,
			FailedType,
			ReferenceGuid,
			AccountInformationGuid,
			UserGuid,
		}

		public Fish()
			: base(TableNames.Fishes.ToString())
		{
			AddField(TableFields.ReferenceID.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.PaymentDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.SmsCount.ToString(), SqlDbType.BigInt);
			AddField(TableFields.Amount.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.OrderID.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.BillNumber.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Description.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.Type.ToString(), SqlDbType.Int);
			AddField(TableFields.Status.ToString(), SqlDbType.Int);
			AddField(TableFields.FailedType.ToString(), SqlDbType.TinyInt);
			AddField(TableFields.ReferenceGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.AccountInformationGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid FishGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string ReferenceID
		{
			get { return Helper.GetString(this[TableFields.ReferenceID.ToString()]); }
			set { this[TableFields.ReferenceID.ToString()] = value; }
		}

		public DateTime CreateDate
		{
			get { return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); }
			set { this[TableFields.CreateDate.ToString()] = value; }
		}

		public DateTime PaymentDate
		{
			get { return Helper.GetDateTime(this[TableFields.PaymentDate.ToString()]); }
			set { this[TableFields.PaymentDate.ToString()] = value; }
		}

		public long SmsCount
		{
			get { return Helper.GetLong(this[TableFields.SmsCount.ToString()]); }
			set { this[TableFields.SmsCount.ToString()] = value; }
		}

		public decimal Amount
		{
			get { return Helper.GetDecimal(this[TableFields.Amount.ToString()]); }
			set { this[TableFields.Amount.ToString()] = value; }
		}

		public string OrderID
		{
			get { return Helper.GetString(this[TableFields.OrderID.ToString()]); }
			set { this[TableFields.OrderID.ToString()] = value; }
		}

		public string BillNumber
		{
			get { return Helper.GetString(this[TableFields.BillNumber.ToString()]); }
			set { this[TableFields.BillNumber.ToString()] = value; }
		}

		public string Description
		{
			get { return Helper.GetString(this[TableFields.Description.ToString()]); }
			set { this[TableFields.Description.ToString()] = value; }
		}

		public int Type
		{
			get { return Helper.GetInt(this[TableFields.Type.ToString()]); }
			set { this[TableFields.Type.ToString()] = value; }
		}

		public int Status
		{
			get { return Helper.GetInt(this[TableFields.Status.ToString()]); }
			set { this[TableFields.Status.ToString()] = value; }
		}

		public int FailedType
		{
			get { return Helper.GetInt(this[TableFields.FailedType.ToString()]); }
			set { this[TableFields.FailedType.ToString()] = value; }
		}

		public Guid ReferenceGuid
		{
			get { return Helper.GetGuid(this[TableFields.ReferenceGuid.ToString()]); }
			set { this[TableFields.ReferenceGuid.ToString()] = value; }
		}

		public Guid AccountInformationGuid
		{
			get { return Helper.GetGuid(this[TableFields.AccountInformationGuid.ToString()]); }
			set { this[TableFields.AccountInformationGuid.ToString()] = value; }
		}

		public Guid UserGuid
		{
			get { return Helper.GetGuid(this[TableFields.UserGuid.ToString()]); }
			set { this[TableFields.UserGuid.ToString()] = value; }
		}
	}
}
