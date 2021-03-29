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

namespace Arad.SMS.Gateway.MagfaSmsSender
{
	public enum MagfaSmsSendError
	{
		SendError = 0,
		NotEnoughCrdit = 14,
		ServerError = 15,
		DeactiveAccount = 16,
		ExpiredAccount = 17,
		InvalidUsernameOrPassword = 18,
		AuthenticationFailure = 19,
		ServerBusy = 23,
		NumberAtBlackList = 27,
		InvalidCheckMessageID = 24,
	}
}
