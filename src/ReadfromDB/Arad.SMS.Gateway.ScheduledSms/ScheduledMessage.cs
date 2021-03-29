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
using Arad.SMS.Gateway.Common;

namespace Arad.SMS.Gateway.ScheduledSms
{
	public class ScheduledMessage
	{
		public Guid Guid { get; set; }
		public long Id { get; set; }
		public SmsSendType TypeSend { get; set; }
		public ScheduledSmsStatus Status { get; set; }
		public Guid PrivateNumberGuid { get; set; }
		public Guid UserGuid { get; set; }
		public int SmsSenderAgentReference { get; set; }
		public bool SendSmsAlert { get; set; }
		public TimeSpan StartSendTime { get; set; }
		public TimeSpan EndSendTime { get; set; }
	}
}
