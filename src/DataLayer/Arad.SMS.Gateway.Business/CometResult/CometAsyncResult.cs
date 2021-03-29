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
using System.Threading;
using System.Web;

namespace CometResult
{
	public class CometAsyncResult : IAsyncResult
	{
		private bool isCompleted = false;
		private bool completedSynchronously = false;
 
		public DateTime StartTime { get; set; }
		public TimeSpan WaitTime { get; set; }
		public AsyncCallback Callback { get; set; }
		public HttpContext Context { get; set; }
		public bool Result { get; set; }
		public string Message { get; set; }

		/// <summary>
		/// Default constructor. Can be used when request completed
		/// synchronously. In this case must set CompletedSynchronously 
		/// to true.
		/// </summary>
		public CometAsyncResult()
		{
		}

		public CometAsyncResult(HttpContext context, AsyncCallback asyncCallback, int waitTime)
		{
			Context = context;
			Callback = asyncCallback;
			StartTime = DateTime.Now;
			WaitTime = new TimeSpan(0, 0, waitTime);
		}

		#region--IAsyncResult
		public object AsyncState { get { return null; } }
		public WaitHandle AsyncWaitHandle { get { return null; } }
		public bool CompletedSynchronously
		{
			get { return completedSynchronously; }
			set { completedSynchronously = value; }
		}
		public bool IsCompleted
		{
			get { return isCompleted; }
			set { isCompleted = value; }
		}
		#endregion
	}
}
