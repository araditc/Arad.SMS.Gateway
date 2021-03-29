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

using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Data;

namespace Arad.SMS.Gateway.Facade
{
	public class SmsFormat : FacadeEntityBase
	{
		public static DataTable GetUserSmsFormats(Guid userGuid)
		{
			Business.SmsFormat SmsFormatController = new Business.SmsFormat();

			return SmsFormatController.GetFormatOfUserPhoneBook(userGuid);
		}

		public static DataTable GetFormatOfPhoneBook(Guid phoneBookGuid)
		{
			Business.SmsFormat smsFormatController = new Business.SmsFormat();
			return smsFormatController.GetFormatOfPhoneBook(phoneBookGuid);
		}

		//public static string GetUserSmsFormats(Guid userGuid, bool showCheckBox)
		//{
		//	StringBuilder tree = new StringBuilder();
		//	StringBuilder phonebook = new StringBuilder();
		//	List<DataRow> lstUserFormat = new List<DataRow>();
		//	List<string> lstFormat = new List<string>();
		//	Business.SmsFormat SmsFormatController = new Business.SmsFormat();
		//	Common.User user = Facade.User.LoadUser(userGuid);

		//	DataTable dataTableFormats = SmsFormatController.GetFormatOfPhoneBook(user.IsAdmin, user.UserGuid);
		//	lstUserFormat = dataTableFormats.AsEnumerable().ToList();

		//	tree.Append("<div class='myList'><ul class='browser filetree'>");
		//	foreach (DataRow row in lstUserFormat)
		//	{
		//		phonebook.Clear();
		//		if (!lstFormat.Contains(row["FormatGuid"].ToString()))
		//		{
		//			lstFormat.Add(row["FormatGuid"].ToString());
		//			List<DataRow> lstPhoneBooks = lstUserFormat.Where(format => Helper.GetGuid(format["FormatGuid"]) == Helper.GetGuid(row["FormatGuid"])).ToList();

		//			foreach (DataRow phoneBookRow in lstPhoneBooks)
		//			{
		//				phonebook.Append(string.Format("<li><span class='folder' Type='group' PhoneBookGuid='{0}'>{1}{2}</span></li>",
		//																						phoneBookRow["PhoneBookGuid"],
		//																						showCheckBox ? "<input onclick='checkBoxControlChecked(this);' type='checkbox'/>" : string.Empty,
		//																						phoneBookRow["PhoneBookName"]));
		//			}

		//			tree.Append(string.Format("<li><span class='root folder' Type='format' FormatGuid='{0}'>{1}{2}</span>{3}</li>",
		//																					row["FormatGuid"],
		//																					showCheckBox ? "<input onclick='checkBoxControlChecked(this);' type='checkbox'/>" : string.Empty,
		//																					row["FormatName"],
		//																					phonebook.Length > 0 ? "<ul>" + phonebook.ToString() + "</ul>" : ""));
		//		}
		//	}
		//	tree.Append("</ul></div>");
		//	return tree.ToString();
		//}

		public static Common.SmsFormat LoadFormat(Guid smsFormatGuid)
		{
			Business.SmsFormat smsFormatController = new Business.SmsFormat();
			Common.SmsFormat smsFormat = new Common.SmsFormat();
			smsFormatController.Load(smsFormatGuid, smsFormat);
			return smsFormat;
		}

		public static bool Delete(Guid smsFormatGuid)
		{
			Business.SmsFormat smsFormatController = new Business.SmsFormat();
			return smsFormatController.Delete(smsFormatGuid);
		}

		public static bool InsertFormatForGroups(Common.SmsFormat smsFormat)
		{
			Business.SmsFormat smsFormatController = new Business.SmsFormat();
			try
			{
				if (smsFormatController.InsertFormat(smsFormat) == Guid.Empty)
					throw new Exception(Language.GetString("ErrorRecord"));

				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool UpdateFormatForGroups(Common.SmsFormat smsFormat)
		{
			Business.SmsFormat smsFormatController = new Business.SmsFormat();
			try
			{
				if (!smsFormatController.UpdateFormat(smsFormat))
					throw new Exception(Language.GetString("ErrorRecord"));

				return true;
			}
			catch
			{
				return false;
			}
		}

		public static DataTable GetPagedAllSmsFormats(Guid userGuid, string formatName, string phoneBookName, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.SmsFormat smsFormatController = new Business.SmsFormat();
			return smsFormatController.GetPagedAllSmsFormats(userGuid, formatName, phoneBookName, sortField, pageNo, pageSize, ref resultCount);
		}

		public static DataTable GetFormatSmsInfo(Guid formatGuid)
		{
			Business.SmsFormat smsFormatController = new Business.SmsFormat();
			return smsFormatController.GetFormatSmsInfo(formatGuid);
		}

		public static string GetFormatText(Guid formatGuid)
		{
			Business.SmsFormat smsFormatController = new Business.SmsFormat();
			return smsFormatController.GetFormatText(formatGuid);
		}
	}
}
