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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Facade
{
	public class FilterWord : FacadeEntityBase
	{
		public static DataTable GetWords()
		{
			Business.FilterWord filterWordController = new Business.FilterWord();
			return filterWordController.GetWords();
		}

		public static bool Delete(Guid guid)
		{
			Business.FilterWord filterWordController = new Business.FilterWord();
			return filterWordController.Delete(guid);
		}

		public static Guid Insert(Common.FilterWord filterWord)
		{
			Business.FilterWord filterWordController = new Business.FilterWord();
			filterWord.FilterWordGuid = Guid.NewGuid();
			return filterWordController.Insert(filterWord);
		}
	}
}
