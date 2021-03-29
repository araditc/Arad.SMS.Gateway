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
using System.Data;
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Business
{
	public class SmsParser : BusinessEntityBase
	{
		public SmsParser(DataAccessBase dataAccessProvider = null) : base(TableNames.SmsParsers.ToString(), dataAccessProvider) { }

		public bool InsertFilter(Common.SmsParser smsParser)
		{
			return ExecuteSPCommand("InsertFilter",
															"@Guid", smsParser.SmsParserGuid,
															"@Title", smsParser.Title,
															"@Type", smsParser.Type,
															"@CreateDate", smsParser.CreateDate,
															"@FromDateTime", smsParser.FromDateTime,
															"@ToDateTime", smsParser.ToDateTime,
															"@TypeConditionSender", smsParser.TypeConditionSender,
															"@ConditionSender", smsParser.ConditionSender,
															"@Scope", smsParser.Scope,
															"@UserGuid", smsParser.UserGuid,
															"@PrivateNumberGuid", smsParser.PrivateNumberGuid);
		}

		public Guid InsertPoll(Common.SmsParser smsParser, DataTable dtSmsParserOptions)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				ExecuteSPCommand("InsertPoll",
												 "@Guid", guid,
												 "@Title", smsParser.Title,
												 "@Type", smsParser.Type,
												 "@CreateDate", smsParser.CreateDate,
												 "@FromDateTime", smsParser.FromDateTime,
												 "@ToDateTime", smsParser.ToDateTime,
												 "@Scope", smsParser.Scope,
												 "@ReplyPrivateNumberGuid", smsParser.ReplyPrivateNumberGuid,
												 "@ReplySmsText", smsParser.ReplySmsText,
												 "@DuplicatePrivateNumberGuid", smsParser.DuplicatePrivateNumberGuid,
												 "@DuplicateUserSmsText", smsParser.DuplicateUserSmsText,
												 "@UserGuid", smsParser.UserGuid,
												 "@PrivateNumberGuid", smsParser.PrivateNumberGuid,
												 "@Options", dtSmsParserOptions);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}

		public Guid InsertCompetition(Common.SmsParser smsParser, DataTable dtSmsParserOptions)
		{
			Guid guid = Guid.NewGuid();
			try
			{
				ExecuteSPCommand("InsertCompetition",
												 "@Guid", guid,
												 "@Title", smsParser.Title,
												 "@Type", smsParser.Type,
												 "@CreateDate", smsParser.CreateDate,
												 "@FromDateTime", smsParser.FromDateTime,
												 "@ToDateTime", smsParser.ToDateTime,
												 "@Scope", smsParser.Scope,
												 "@ReplyPrivateNumberGuid", smsParser.ReplyPrivateNumberGuid,
												 "@ReplySmsText", smsParser.ReplySmsText,
												 "@DuplicatePrivateNumberGuid", smsParser.DuplicatePrivateNumberGuid,
												 "@DuplicateUserSmsText", smsParser.DuplicateUserSmsText,
												 "@UserGuid", smsParser.UserGuid,
												 "@PrivateNumberGuid", smsParser.PrivateNumberGuid,
												 "@Options", dtSmsParserOptions);
				return guid;
			}
			catch
			{
				guid = Guid.Empty;
				return guid;
			}
		}

		public bool UpdateFilter(Common.SmsParser smsParser)
		{
			return ExecuteSPCommand("UpdateFilter",
															"@Guid", smsParser.SmsParserGuid,
															"@Title", smsParser.Title,
															"@FromDateTime", smsParser.FromDateTime,
															"@ToDateTime", smsParser.ToDateTime,
															"@TypeConditionSender", smsParser.TypeConditionSender,
															"@ConditionSender", smsParser.ConditionSender,
															"@Scope", smsParser.Scope,
															"@PrivateNumberGuid", smsParser.PrivateNumberGuid);
		}

		public bool UpdatePoll(Common.SmsParser smsParser, DataTable dtSmsParserOptions)
		{
			return base.ExecuteSPCommand("UpdatePoll",
																	 "@Guid", smsParser.SmsParserGuid,
																	 "@Title", smsParser.Title,
																	 "@FromDateTime", smsParser.FromDateTime,
																	 "@ToDateTime", smsParser.ToDateTime,
																	 "@Scope", smsParser.Scope,
																	 "@ReplyPrivateNumberGuid", smsParser.ReplyPrivateNumberGuid,
																	 "@ReplySmsText", smsParser.ReplySmsText,
																	 "@DuplicatePrivateNumberGuid", smsParser.DuplicatePrivateNumberGuid,
																	 "@DuplicateUserSmsText", smsParser.DuplicateUserSmsText,
																	 "@PrivateNumberGuid", smsParser.PrivateNumberGuid,
																	 "@Options", dtSmsParserOptions);
		}

		public bool UpdateCompetition(Common.SmsParser smsParser, DataTable dtSmsParserOptions)
		{
			return base.ExecuteSPCommand("UpdateCompetition",
																	 "@Guid", smsParser.SmsParserGuid,
																	 "@Title", smsParser.Title,
																	 "@FromDateTime", smsParser.FromDateTime,
																	 "@ToDateTime", smsParser.ToDateTime,
																	 "@Scope", smsParser.Scope,
																	 "@ReplyPrivateNumberGuid", smsParser.ReplyPrivateNumberGuid,
																	 "@ReplySmsText", smsParser.ReplySmsText,
																	 "@DuplicatePrivateNumberGuid", smsParser.DuplicatePrivateNumberGuid,
																	 "@DuplicateUserSmsText", smsParser.DuplicateUserSmsText,
																	 "@PrivateNumberGuid", smsParser.PrivateNumberGuid,
																	 "@Options", dtSmsParserOptions);
		}

		public DataTable GetPagedSmsParsers(Common.SmsParser smsParser, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetSmsParser = base.FetchSPDataSet("GetPagedSmsParsers",
																										 "@UserGuid", smsParser.UserGuid,
																										 "@Type", smsParser.Type,
																										 "@PageNo", pageNo,
																										 "@PageSize", pageSize,
																										 "@SortField", sortField);

			resultCount = Helper.GetInt(dataSetSmsParser.Tables[0].Rows[0]["RowCount"]);

			return dataSetSmsParser.Tables[1];
		}

		//public DataTable GetSmsParsersOfPrivateNumber(Guid userPrivateNumberGuid)
		//{
		//	return FetchSPDataTable("GetSmsParsersOfPrivateNumber", "@UserPrivateNumberGuid", userPrivateNumberGuid);
		//}

		public bool ActiveSmsParser(Guid smsParserGuid, bool isActive)
		{
			return ExecuteCommand("UPDATE [dbo].[SmsParsers] SET [IsActive] = @IsActive WHERE [Guid] = @Guid", "@Guid", smsParserGuid, "@IsActive", isActive);
		}

		public bool IsDuplicateSmsParserKey(Guid parserGuid, Guid numberGuid, string key)
		{
			return FetchSPDataTable("GetSmsParserKey",
															"@ParserGuid", parserGuid,
															"@NumberGuid", numberGuid,
															"@Key", key).Rows.Count > 0 ? true : false;
		}

		public bool ParseReceiveSms(Guid smsGuid, Guid numberGuid, string smsText, string mobile)
		{
			return ExecuteSPCommand("ParseReceiveSms",
															"@SmsGuid", smsGuid,
															"@PrivateNumberGuid", numberGuid,
															"@SmsText", smsText,
															"@Mobile", mobile);
		}
	}
}