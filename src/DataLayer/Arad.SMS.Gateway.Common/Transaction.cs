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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.GeneralLibrary;
using System.Data;

namespace Arad.SMS.Gateway.Common
{
	public class Transaction : CommonEntityBase
	{
		private enum TableFields
		{
			ReferenceGuid,
			TypeTransaction,
			TypeCreditChange,
			Description,
			CreateDate,
			CurrentCredit,
			Amount,
			Benefit,
			UserGuid
		}

		public Transaction()
			: base(TableNames.Transactions.ToString())
		{
			AddField(TableFields.ReferenceGuid.ToString(), SqlDbType.UniqueIdentifier);
			AddField(TableFields.TypeTransaction.ToString(), SqlDbType.Int);
			AddField(TableFields.TypeCreditChange.ToString(), SqlDbType.Int);
			AddField(TableFields.Description.ToString(), SqlDbType.NVarChar, short.MaxValue);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.CurrentCredit.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.Amount.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.Benefit.ToString(), SqlDbType.Decimal, 18);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid TransactionGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public Guid ReferenceGuid
		{
			get 
			{
				return Helper.GetGuid(this[TableFields.ReferenceGuid.ToString()]); 
			}
			set 
			{
				this[TableFields.ReferenceGuid.ToString()] = value;
			}
		}

		public int TypeTransaction
		{
			get
			{
				return Helper.GetInt(this[TableFields.TypeTransaction.ToString()]);
			}
			set
			{
				this[TableFields.TypeTransaction.ToString()] = value;
			}
		}

		public int TypeCreditChange
		{
			get
			{
				return Helper.GetInt(this[TableFields.TypeCreditChange.ToString()]);
			}
			set
			{
				this[TableFields.TypeCreditChange.ToString()] = value;
			}
		}

		public string Description
		{
			get
			{
				return Helper.GetString(this[TableFields.Description.ToString()]);
			}
			set
			{
				this[TableFields.Description.ToString()] = value;
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

		public decimal CurrentCredit
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.CurrentCredit.ToString()]);
			}
			set
			{
				this[TableFields.CurrentCredit.ToString()] = value;
			}
		}

		public decimal Amount
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.Amount.ToString()]);
			}
			set
			{
				this[TableFields.Amount.ToString()] = value;
			}
		}

		public decimal Benefit
		{
			get
			{
				return Helper.GetDecimal(this[TableFields.Benefit.ToString()]);
			}
			set
			{
				this[TableFields.Benefit.ToString()] = value;
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
