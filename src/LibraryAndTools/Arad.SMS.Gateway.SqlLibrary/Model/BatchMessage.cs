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

namespace Arad.SMS.Gateway.SqlLibrary
{
	[Serializable]
	public class BatchMessage
	{
		public string QueueMessageId { get; set; }
		public int SmsSenderAgentReference { get; set; }
		public Guid UserGuid { get; set; }
		public long Id { get; set; }
		public Guid Guid { get; set; }
		public int SmsSendType { get; set; }
		public string CheckId { get; set; }
		public int MaximumTryCount { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Domain { get; set; }
		public string SendLink { get; set; }
		public string ReceiveLink { get; set; }
		public string DeliveryLink { get; set; }
		public string SenderNumber { get; set; }
		public Guid PrivateNumberGuid { get; set; }
		public string ServiceId { get; set; }
		public string SmsText { get; set; }
		public int SmsLen { get; set; }
		public long SmsIdentifier { get; set; }
		public int SmsPartIndex { get; set; }
		public bool IsUnicode { get; set; }
		public bool IsFlash { get; set; }
		public int TotalCount { get; set; }
		public List<InProgressSms> Receivers { get; set; }
		public List<Guid> ReferenceGuid { get; set; }
		public string QueueName { get; set; }
		public int PageNo { get; set; }

        public string VoiceURL { get; set; }

        public int VoiceMessageId { get; set; }

    }
}
