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

using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Arad.SMS.Gateway.Facade
{
	public class UserDocument : FacadeEntityBase
	{
		public static DataTable GetUserDocuments(Guid userGuid)
		{
			try
			{
				Business.UserDocument userDocumentController = new Business.UserDocument();
				return userDocumentController.GetUserDocuments(userGuid);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool Delete(Guid guid)
		{
			Business.UserDocument userDocumentController = new Business.UserDocument();
			return userDocumentController.Delete(guid);
		}

		public static Guid Insert(Common.UserDocument userDocument)
		{
			Business.UserDocument userDocumentController = new Business.UserDocument();
			return userDocumentController.Insert(userDocument);
		}

		public static bool UpdateStatus(Guid guid, UserDocumentStatus status)
		{
			Business.UserDocument userDocumentController = new Business.UserDocument();
			return userDocumentController.UpdateStatus(guid, status);
		}
	}
}
