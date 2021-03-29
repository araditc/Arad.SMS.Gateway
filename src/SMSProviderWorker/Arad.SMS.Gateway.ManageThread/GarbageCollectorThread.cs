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

using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.ManageThread
{
	public class GarbageCollectorThread : WorkerThread
	{
		#region Constructor
		public GarbageCollectorThread(int timeOut) : base(timeOut) { }
		#endregion

		protected override void WorkerThreadFunction()
		{
			long beforeCollect;
			long afterCollect;

			beforeCollect = System.GC.GetTotalMemory(false);
			System.GC.Collect();
			afterCollect = System.GC.GetTotalMemory(true);

			LogController<ServiceLogs>.LogInFile(ServiceLogs.GarbageCollector, string.Format("Garbage Collector:       Memory usage before collection: {0}, after collection: {1}", beforeCollect, afterCollect));
		}
	}
}
