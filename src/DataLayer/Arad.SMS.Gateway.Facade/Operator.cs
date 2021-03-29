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
using System.Collections.Generic;
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.Facade
{
	public class Operator : FacadeEntityBase
	{
		public static DataTable GetOperators()
		{
			Business.Operator operatorController = new Business.Operator();
			return operatorController.GetOperators();
		}
		public static Dictionary<int, string> GetOperatorsInfo()
		{
			DataTable dtOperators = GetOperators();
			Dictionary<int, string> operatorsInfo = new Dictionary<int, string>();
			foreach (DataRow row in dtOperators.Rows)
				operatorsInfo.Add(Helper.GetInt(row["ID"]), row["Regex"].ToString());

			return operatorsInfo;
		}

		public static string GetOperatorName(int opt)
		{
			Business.Operator operatorController = new Business.Operator();
			return operatorController.GetOperatorName(opt);
		}
	}
}
