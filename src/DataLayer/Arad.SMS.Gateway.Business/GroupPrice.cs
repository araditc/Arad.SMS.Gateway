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
using System.Data;
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Business
{
	public class GroupPrice : BusinessEntityBase
	{
		public GroupPrice(DataAccessBase dataAccessProvider = null)
			: base(TableNames.GroupPrices.ToString(), dataAccessProvider) { }

		public DataTable GetPagedGroupPrices(Guid userGuid)
		{
			return FetchSPDataTable("GetPagedGroupPrices", "@UserGuid", userGuid);
		}

		public bool UpdateGroupPrice(Common.GroupPrice groupPrice)
		{
			return base.ExecuteSPCommand("UpdateGroupPrice",
																	 "@Guid", groupPrice.GroupPriceGuid,
																	 "@Title", groupPrice.Title,
																	 "@MinimumMessage", groupPrice.MinimumMessage,
																	 "@MaximumMessage", groupPrice.MaximumMessage,
																	 "@BasePrice", groupPrice.BasePrice,
																	 "@DecreaseTax", groupPrice.DecreaseTax,
																	 "@AgentRatio", groupPrice.AgentRatio,
																	 "@IsPrivate", groupPrice.IsPrivate,
																	 "@IsDefault", groupPrice.IsDefault);
		}

		public bool CheckGroupPriceName(string title, Guid userGuid)
		{
			DataTable dataTableCheckGroupPriceName = base.FetchSPDataTable("CheckGroupPriceName", "@Title", title, "@UserGuid", userGuid);
			return dataTableCheckGroupPriceName.Rows.Count > 0 ? false : true;
		}

		public Guid GetDefaultGroupPrice(string domain)
		{
			return base.GetSPGuidFieldValue("GetDefaultGroupPrice", "@Domain", domain);
		}

		public DataTable GetGroupPrices(Guid userGuid)
		{
			return FetchSPDataTable("GetGroupPrices", "@UserGuid", userGuid);
		}

		public decimal GetUserBaseSmsPrice(Guid userGuid, Guid parentGuid, long smsCount, ref bool decreaseTax)
		{
			DataTable dtGroupPrice = FetchSPDataTable("GetUserBaseSmsPrice", "@UserGuid", userGuid,
																																			 "@ParentGuid", parentGuid,
																																			 "@SmsCount", smsCount);
			if (dtGroupPrice.Rows.Count > 0)
			{
				decreaseTax = Helper.GetBool(dtGroupPrice.Rows[0]["DecreaseTax"]);
				return Helper.GetDecimal(dtGroupPrice.Rows[0]["BasePrice"]);
			}
			else
				return 150;

		}

		public DataTable CheckExistRange(Guid userGuid)
		{
			return FetchDataTable("SELECT * FROM [GroupPrices] WHERE [IsDeleted] = 0 AND [UserGuid] = @UserGuid", "@UserGuid", userGuid);
		}
	}
}
