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
using System.Linq;
using System.Text;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;

namespace Arad.SMS.Gateway.Facade
{
	public class SmsTemplate : FacadeEntityBase
	{
		public static bool Insert(Common.SmsTemplate smsTemplate)
		{
			Business.SmsTemplate smsTemplateController = new Business.SmsTemplate();
			return smsTemplateController.Insert(smsTemplate) != Guid.Empty ? true : false;
		}

		public static DataTable GetPagedSmsTemplates(string body , Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.SmsTemplate smsTemplateController = new Business.SmsTemplate();
			return smsTemplateController.GetPagedSmsTemplates(body, userGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool UpdateSmsTemplate(Common.SmsTemplate smsTemplate)
		{
			Business.SmsTemplate smsTemplateController = new Business.SmsTemplate();
			return smsTemplateController.UpdateSmsTemplate(smsTemplate);
		}

		public static bool DeleteSmsTemplate(Guid smsTemplateGuid)
		{
			Business.SmsTemplate smsTemplateController = new Business.SmsTemplate();
			return smsTemplateController.Delete(smsTemplateGuid);
		}

		public static Common.SmsTemplate LoadSmsTemplate(Guid smsTemplateGuid)
		{
			Business.SmsTemplate smsTemplateController = new Business.SmsTemplate();
			Common.SmsTemplate smsTemplate = new Common.SmsTemplate();
			smsTemplateController.Load(smsTemplateGuid, smsTemplate);
			return smsTemplate;
		}

		//public static DataTable GetSmsTemplate(Guid groupTemplateGuid)
		//{
		//	Business.SmsTemplate smsTemplateController = new Business.SmsTemplate();
		//	return smsTemplateController.GetSmsTemplate(groupTemplateGuid);
		//}
	}
}
