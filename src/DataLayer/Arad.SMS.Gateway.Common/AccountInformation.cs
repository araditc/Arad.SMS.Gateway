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
	public class AccountInformation : CommonEntityBase
	{
		private enum TableFields
		{
			Owner,
			Branch,
			AccountNo,
			CardNo,
			TerminalID,
			UserName,
			Password,
			PinCode,
			CreateDate,
			Bank,
			IsActive,
			OnlineGatewayIsActive,
			IsDeleted,
			UserGuid,
		}

		public AccountInformation()
			: base(TableNames.AccountInformations.ToString())
		{
			AddField(TableFields.Owner.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Branch.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.AccountNo.ToString(), SqlDbType.NVarChar, 100);
			AddField(TableFields.CardNo.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.TerminalID.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.UserName.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.Password.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.PinCode.ToString(), SqlDbType.NVarChar, 50);
			AddField(TableFields.CreateDate.ToString(), SqlDbType.DateTime);
			AddField(TableFields.Bank.ToString(), SqlDbType.Int);
			AddField(TableFields.IsActive.ToString(), SqlDbType.Bit);
			AddField(TableFields.OnlineGatewayIsActive.ToString(), SqlDbType.Bit);
			AddReadOnlyField(TableFields.IsDeleted.ToString(), SqlDbType.Bit);
			AddField(TableFields.UserGuid.ToString(), SqlDbType.UniqueIdentifier);
		}

		public Guid AccountInfoGuid
		{
			get { return PrimaryKey; }
			set { PrimaryKey = value; }
		}

		public string Owner
		{
			get { return Helper.GetString(this[TableFields.Owner.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteAccountOwnerField");
					HasError = true;
				}
				else
					this[TableFields.Owner.ToString()] = value;
			}
		}

		public string Branch
		{
			get { return Helper.GetString(this[TableFields.Branch.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteBranchField");
					HasError = true;
				}
				else
					this[TableFields.Branch.ToString()] = value;
			}
		}

		public string AccountNo
		{
			get { return Helper.GetString(this[TableFields.AccountNo.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteAccountNoField");
					HasError = true;
				}
				else
					this[TableFields.AccountNo.ToString()] = value;
			}
		}

		public string CardNo
		{
			get { return Helper.GetString(this[TableFields.CardNo.ToString()]); }
			set
			{
				if (Helper.CheckDataConditions(value).IsEmpty)
				{
					ErrorMessage += Language.GetString("CompleteAccountNoField");
					HasError = true;
				}
				else
					this[TableFields.CardNo.ToString()] = value;
			}
		}

		public string TerminalID
		{
			get { return Helper.GetString(this[TableFields.TerminalID.ToString()]); }
			set { this[TableFields.TerminalID.ToString()] = value; }
		}

		public string UserName
		{
			get { return Helper.GetString(this[TableFields.UserName.ToString()]); }
			set { this[TableFields.UserName.ToString()] = value; }
		}

		public string Password
		{
			get { return Helper.GetString(this[TableFields.Password.ToString()]); }
			set { this[TableFields.Password.ToString()] = value; }
		}

		public string PinCode
		{
			get { return Helper.GetString(this[TableFields.PinCode.ToString()]); }
			set { this[TableFields.PinCode.ToString()] = value; }
		}

		public DateTime CreateDate
		{
			get { return Helper.GetDateTime(this[TableFields.CreateDate.ToString()]); }
			set { this[TableFields.CreateDate.ToString()] = value; }
		}

		public int Bank
		{
			get { return Helper.GetInt(this[TableFields.Bank.ToString()]); }
			set { this[TableFields.Bank.ToString()] = value; }
		}

		public bool IsActive
		{
			get { return Helper.GetBool(this[TableFields.IsActive.ToString()]); }
			set { this[TableFields.IsActive.ToString()] = value; }
		}

		public bool OnlineGatewayIsActive
		{
			get { return Helper.GetBool(this[TableFields.OnlineGatewayIsActive.ToString()]); }
			set { this[TableFields.OnlineGatewayIsActive.ToString()] = value; }
		}

		public Guid UserGuid
		{
			get { return Helper.GetGuid(this[TableFields.UserGuid.ToString()]); }
			set { this[TableFields.UserGuid.ToString()] = value; }
		}
	}
}
