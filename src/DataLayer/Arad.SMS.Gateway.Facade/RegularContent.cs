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

using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Arad.SMS.Gateway.Facade
{
	public class RegularContent : FacadeEntityBase
	{
		public static DataTable GetPagedRegularContents(Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.RegularContent regularContentController = new Business.RegularContent();
			return regularContentController.GetPagedRegularContents(userGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public static DataTable GetRegularContent(Guid userGuid)
		{
			Business.RegularContent regularContentController = new Business.RegularContent();
			return regularContentController.GetRegularContent(userGuid);
		}

		public static Common.RegularContent Load(Guid guid)
		{
			Business.RegularContent regularContentController = new Business.RegularContent();
			Common.RegularContent regularContent = new Common.RegularContent();
			regularContentController.Load(guid, regularContent);
			return regularContent;
		}

		public static bool Insert(Common.RegularContent regularContent)
		{
			Business.RegularContent regularContentController = new Business.RegularContent();
			return regularContentController.Insert(regularContent) != Guid.Empty ? true : false;
		}

		public static bool Update(Common.RegularContent regularContent)
		{
			Business.RegularContent regularContentController = new Business.RegularContent();
			return regularContentController.UpdateRegularContent(regularContent);
		}

		public static bool Delete(Guid guid)
		{
			Business.RegularContent regularContentController = new Business.RegularContent();
			return regularContentController.Delete(guid);
		}

		public static DataTable GetRegularContentForProcess()
		{
			Business.RegularContent regularContentController = new Business.RegularContent();
			return regularContentController.GetRegularContentForProcess();
		}

		public static DataTable GetRegularContentFileType()
		{
			Business.RegularContent regularContentController = new Business.RegularContent();
			return regularContentController.GetRegularContentFileType();
		}

		public static bool SendURLContentToReceiver(Guid regularContentGuid, Guid privateNumberGuid, Guid userGuid, string smsText,
																								Business.SmsSentPeriodType periodType, int period, DateTime effectiveDateTime)
		{
			Business.RegularContent regularContentController = new Business.RegularContent();
			return regularContentController.SendURLContentToReceiver(regularContentGuid, privateNumberGuid, userGuid, smsText, periodType, period, effectiveDateTime);
		}

		public static bool SendDBContentToReceiver(Guid regularContentGuid, Guid privateNumberGuid, Guid userGuid, string smsText,
																							 Business.SmsSentPeriodType periodType, int period, DateTime effectiveDateTime)
		{
			Business.RegularContent regularContentController = new Business.RegularContent();
			return regularContentController.SendDBContentToReceiver(regularContentGuid, privateNumberGuid, userGuid, smsText, periodType, period, effectiveDateTime);
		}
	}
}
