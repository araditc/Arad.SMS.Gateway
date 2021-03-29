﻿// --------------------------------------------------------------------
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
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Common
{
	public class FailedOnlinePayment : CommonEntityBase
	{
		public enum TableFields
		{
			OrderID,
			ReferenceID,
			Bank,
			CreateDate,
			UserGuid,
			IsDeleted
		}

		public FailedOnlinePayment()
			: base(TableNames.FailedOnlinePayments.ToString())
		{
			AddField(TableFields.OrderID.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.ReferenceID.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.Bank.ToString(), SqlDbType.Int);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
		}

		public Guid FailedOnlinePaymentGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string OrderID
		{
			get { return Helper.GetString(this[TableFields.OrderID.ToString()]); }
			set { this[TableFields.OrderID.ToString()] = value; }
		}

		public string ReferenceID
		{
			get { return Helper.GetString(this[TableFields.OrderID.ToString()]); }
			set { this[TableFields.OrderID.ToString()] = value; }
		}

		public int Bank
		{
			get { return Helper.GetInt(this[TableFields.Bank.ToString()]); }
			set { this[TableFields.Bank.ToString()] = value; }
		}

		public DateTime CreateDate
		{
			get { return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); }
			set { this[TableFields.CreateDate.ToString()] = value; }
		}

		public Guid UserGuid
		{
			get { return Helper.GetGuid(this[TableFields.UserGuid.ToString()]); }
			set { this[TableFields.UserGuid.ToString()] = value; }
		}
	}
}
