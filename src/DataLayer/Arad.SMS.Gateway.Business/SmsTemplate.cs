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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Arad.SMS.Gateway.Business
{
	public class SmsTemplate : BusinessEntityBase
	{
		public SmsTemplate(DataAccessBase dataAccessProvider = null)
			: base(TableNames.SmsTemplates.ToString(), dataAccessProvider) { }

		public DataTable GetPagedSmsTemplates(string body, Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetSmsTemplate = base.FetchSPDataSet("GetPagedSmsTemplates", //"@GroupTemplateGuid", groupTemplateGuid,
																											 "@Body", body,
																											 "@UserGuid", userGuid,
																											 "@PageNo", pageNo,
																											 "@PageSize", pageSize,
																											 "@SortField", sortField);
			resultCount = Helper.GetInt(dataSetSmsTemplate.Tables[0].Rows[0]["RowCount"]);

			return dataSetSmsTemplate.Tables[1];
		}

		public bool UpdateSmsTemplate(Common.SmsTemplate smsTemplate)
		{
			return base.ExecuteSPCommand("UpdateSmsTemplate", "@Guid", smsTemplate.SmsTemplateGuid,
																												"@Body", smsTemplate.Body);
		}

		public DataTable GetUserSmsTemplates(Guid userGuid)
		{
			return base.FetchSPDataTable("GetUserSmsTemplates","@UserGuid", userGuid);
		}

		//public DataTable GetSmsTemplate(Guid groupTemplateGuid)
		//{
		//	return base.FetchSPDataTable("GetSmsTemplatesOfGroupTemplates", "@Guid", groupTemplateGuid);
		//}
	}
}
