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

namespace Arad.SMS.Gateway.WebApi
{
	public enum Queues
	{
		ReceiveMessagesQueue = 1,
		ApiSendMessage = 2,
		SaveDelivery = 3,
		ConfirmBulk = 4,
		SaveGsmDelivery = 5,
	}

	public enum ErrorCode
	{
		None = 0,
		AccountIsInactive = 1,
		AccountIsExpired = 2,
		AccountIsInvalid = 3,
		BadRequest = 3,
		InternalServerError = 4,
		ReceiversIsEmpty = 5,
		SenderIsInvalid = 6,
		ValidationNotValid = 7,
		IPInvalid = 8,
		ReceiverCountNotValid = 9,
		ServiceIdIsInvalid = 10,
		ReceiverIsInvalid = 11,
		ReceiveSmsIsEmpty = 12,
		PhoneBookGroupIsEmpty = 13,
		AccessDenied = 14,
	}

	public enum VASType
	{
		SendToReceiver = 1,
		SendToGroup = 2,
	}
}
