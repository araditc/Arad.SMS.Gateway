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
	public class SmsFormat : BusinessEntityBase
	{
		public SmsFormat(DataAccessBase dataAccessProvider = null)
			: base(TableNames.SmsFormats.ToString(), dataAccessProvider) { }

		public DataTable GetFormatOfPhoneBook(Guid phoneBookGuid)
		{
			return base.FetchSPDataTable("GetFormatOfPhoneBook", "@PhoneBookGuid", phoneBookGuid);
		}

		public DataTable GetFormatOfUserPhoneBook(Guid userGuid)
		{
			return base.FetchSPDataTable("GetFormatsOfUserPhoneBook", "@UserGuid", userGuid);
		}

		public bool UpdateFormat(Common.SmsFormat smsFormat)
		{
			return base.ExecuteSPCommand("UpdateFormat",
																	 "@Guid", smsFormat.SmsFormatGuid,
																	 "@Name", smsFormat.Name,
																	 "@Format", smsFormat.Format);
		}

		public DataTable GetPagedAllSmsFormats(Guid userGuid, string formatName, string phoneBookName, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet smsFormatInfo = base.FetchSPDataSet("GetPagedAllSmsFormats", "@UserGuid", userGuid,
																																					"@FormatName", formatName,
																																					"@PhoneBookName", phoneBookName,
																																					"@PageNo", pageNo,
																																					"@PageSize", pageSize,
																																					"@SortField", sortField);
			resultCount = Helper.GetInt(smsFormatInfo.Tables[0].Rows[0]["RowCount"]);

			return smsFormatInfo.Tables[1];
		}

		public Guid InsertFormat(Common.SmsFormat smsFormat)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				base.ExecuteSPCommand("InsertFormat",
															"@Guid", guid,
															"@Name", smsFormat.Name,
															"@Format", smsFormat.Format,
															"@CreateDate", smsFormat.CreateDate,
															"@PhoneBookGuid", smsFormat.PhoneBookGuid);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}

		public DataTable GetFormatSmsInfo(Guid formatGuid)
		{
			return FetchSPDataTable("GetFormatSmsInfo", "@FormatGuid", formatGuid);
		}

		public string GetFormatText(Guid formatGuid)
		{
			DataTable dt = FetchSPDataTable("GetRawFormat", "@FormatGuid", formatGuid);
			return dt.Rows.Count > 0 ? Helper.GetString(dt.Rows[0]["SmsBody"]) : string.Empty;
		}
	}
}
