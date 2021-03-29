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

namespace Arad.SMS.Gateway.Facade
{
	public class AccountInformation : FacadeEntityBase
	{
		public static bool Insert(Common.AccountInformation accountNumber)
		{
			Business.AccountInformation accountInfoController = new Business.AccountInformation();
			return accountInfoController.Insert(accountNumber) != Guid.Empty ? true : false;
		}

		//public static Guid InsertOtherAccountNumber(Guid userGuid)
		//{
		//	Business.AccountInformation accountInfoController = new Business.AccountInformation();
		//	Common.AccountInformation accountInfo = new Common.AccountInformation();
		//	accountInfo.Owner = "0";
		//	accountInfo.Bank = 0;
		//	accountInfo.Branch = "0";
		//	accountInfo.AccountNo = "0";
		//	//accountInfo.Type = (int)Business.TypeAccountInformation.Other;
		//	accountInfo.IsActive = true;
		//	accountInfo.UserGuid = userGuid;
		//	return accountInfoController.Insert(accountInfo);
		//}

		//public static DataTable GetTypeAccount(Guid userGuid, int type)
		//{
		//	Business.AccountInformation accountInfoController = new Business.AccountInformation();
		//	return accountInfoController.GetTypeAccount(userGuid, type);
		//}

		public static Common.AccountInformation LoadAccountInformation(Guid accountNumberGuid)
		{
			Business.AccountInformation accountInfoController = new Business.AccountInformation();
			Common.AccountInformation accountInfo = new Common.AccountInformation();
			accountInfoController.Load(accountNumberGuid, accountInfo);
			return accountInfo;
		}

		public static bool UpdateAccount(Common.AccountInformation accountInfo)
		{
			Business.AccountInformation accountInfoController = new Business.AccountInformation();
			return accountInfoController.UpdateAccount(accountInfo);
		}

		public static DataTable GetPagedAccountInformations(Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.AccountInformation accountInfoController = new Business.AccountInformation();
			return accountInfoController.GetPagedAccountInformations(userGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool Delete(Guid guid)
		{
			Business.AccountInformation accountInfoController = new Business.AccountInformation();
			return accountInfoController.Delete(guid);
		}

		public static DataTable GetAccountsIsActiveOnline(Guid userGuid)
		{
			Business.AccountInformation accountInfoController = new Business.AccountInformation();
			return accountInfoController.GetAccountsIsActiveOnline(userGuid);
		}

		public static DataTable GetAccountOfReferenceID(string ReferenceID)
		{
			Business.AccountInformation accountInfoController = new Business.AccountInformation();
			return accountInfoController.GetAccountOfReferenceID(ReferenceID);
		}
	}
}
