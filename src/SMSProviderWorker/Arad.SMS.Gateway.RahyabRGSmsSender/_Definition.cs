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

namespace Arad.SMS.Gateway.RahyabRGSmsSender
{
	public class RahyabResponse
	{
		public string SourceAddress { get; set; }
		public string DestAddress { get; set; }
		public string Status { get; set; }
		public string Response { get; set; }
		public string SMSID { get; set; }

 //        شماره فرستنده پیامک ارسالی
 //شماره گیرنده پیامک ارسالی
 //وضعیت ارسال
 //توصیف وضعیت پیامک ارسال شده

	}
	public enum RahyabSmsSendError
	{
		SendError = -1,
		Access_Denied = 0,
		Insufficient_Balance_Amount,
		BLACKLISTED_DESTINATION,
		INVALID_DEST_ADDRESS,
		INVALID_DEST_NETWORK,
		UNREACHABLE_DEST_NETWORK,
		SOURCE_ADDRESS_NOT_ALLOWED,
		SUSPENDED_SERVICE_NUMBER,
		INVALID_SOURCE_ADDRESS,
		THROTTLING_ERROR,
	}
}
