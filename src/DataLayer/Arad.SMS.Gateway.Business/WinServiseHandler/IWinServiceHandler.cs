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
using System.ServiceModel;
using Arad.SMS.Gateway.Common;

namespace Arad.SMS.Gateway.Business
{
	[ServiceContract]
	public interface IWinServiceHandler
	{
		[OperationContract]
		void SetStatus(SmsSenderAgentReference smsSenderAgentRefrence, bool snedStatus, bool receiveStatus, bool sendBulkStatus);

		[OperationContract]
		void ClearPrivateNumberCache(SmsSenderAgentReference smsSenderAgentRefrence);

		[OperationContract]
		float GetCredit(SmsSenderAgentReference smsSenderAgentRefrence);

		[OperationContract]
		void ClearSmsRateCache(Guid userGuid);

		//[OperationContract]
		//int GetCountPrefix(Business.SmsSenderAgentReference smsSenderAgentRefrence, string prefix, int type);

		//[OperationContract]
		//int GetCountPostCode(Business.SmsSenderAgentReference smsSenderAgentRefrence, string postCode, int type);

		//[OperationContract]
		//int GetCountProvince(Business.SmsSenderAgentReference smsSenderAgentRefrence, int provinceID, int cityID, int type);

		//[OperationContract]
		//string[] GetProvinces(Business.SmsSenderAgentReference smsSenderAgentRefrence);

		//[OperationContract]
		//string[] GetCities(Business.SmsSenderAgentReference smsSenderAgentRefrence, int provinceID);
	}
}
