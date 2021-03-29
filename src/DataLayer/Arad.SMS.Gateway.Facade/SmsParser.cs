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
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Facade
{
	public class SmsParser : FacadeEntityBase
	{
		public static bool InsertFilter(Common.SmsParser smsParser, Common.ParserFormula parserFormula)
		{
			Business.SmsParser smsParserController = new Business.SmsParser();
			Business.ParserFormula parserFormulaController = new Business.ParserFormula();

			smsParserController.BeginTransaction();
			try
			{
				smsParser.SmsParserGuid = Guid.NewGuid();

				if (!smsParserController.InsertFilter(smsParser))
					throw new Exception(Language.GetString("ErrorRecord"));

				parserFormula.SmsParserGuid = smsParser.SmsParserGuid;
				if (!parserFormulaController.InsertFilterOption(parserFormula))
					throw new Exception(Language.GetString("ErrorRecord"));

				smsParserController.CommitTransaction();
				return true;
			}
			catch (Exception ex)
			{
				smsParserController.RollbackTransaction();
				throw ex;
			}
		}

		public static bool InsertPoll(Common.SmsParser smsParser, DataTable dtSmsParserOptions)
		{
			Business.SmsParser smsParserController = new Business.SmsParser();
			try
			{
				dtSmsParserOptions.Columns.Remove("Guid");
				return smsParserController.InsertPoll(smsParser, dtSmsParserOptions) != Guid.Empty ? true : false;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool InsertCompetition(Common.SmsParser smsParser, DataTable dtSmsParserOptions)
		{
			Business.SmsParser smsParserController = new Business.SmsParser();
			try
			{
				dtSmsParserOptions.Columns.Remove("Guid");
				return smsParserController.InsertCompetition(smsParser, dtSmsParserOptions) != Guid.Empty ? true : false;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool UpdateFilter(Common.SmsParser smsParser, Common.ParserFormula parserFormula)
		{
			Business.SmsParser smsParserController = new Business.SmsParser();
			Business.ParserFormula parserFormulaController = new Business.ParserFormula();

			smsParserController.BeginTransaction();
			try
			{
				if (!smsParserController.UpdateFilter(smsParser))
					throw new Exception(Language.GetString("ErrorRecord"));

				parserFormula.SmsParserGuid = smsParser.SmsParserGuid;
				if (!parserFormulaController.InsertFilterOption(parserFormula))
					throw new Exception(Language.GetString("ErrorRecord"));

				smsParserController.CommitTransaction();
				return true;
			}
			catch (Exception ex)
			{
				smsParserController.RollbackTransaction();
				throw ex;
			}
		}

		public static bool UpdatePoll(Common.SmsParser smsParser, DataTable dtSmsParserOptions)
		{
			Business.SmsParser smsParserController = new Business.SmsParser();
			try
			{
				dtSmsParserOptions.Columns.Remove("Guid");
				return smsParserController.UpdatePoll(smsParser, dtSmsParserOptions);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool UpdateCompetition(Common.SmsParser smsParser, DataTable dtSmsParserOptions)
		{
			Business.SmsParser smsParserController = new Business.SmsParser();
			try
			{
				dtSmsParserOptions.Columns.Remove("Guid");
				return smsParserController.UpdateCompetition(smsParser, dtSmsParserOptions);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetPagedSmsParsers(Common.SmsParser smsParser, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.SmsParser smsParserController = new Business.SmsParser();
			return smsParserController.GetPagedSmsParsers(smsParser, sortField, pageNo, pageSize, ref resultCount);
		}

		public static Common.SmsParser LoadSmsParser(Guid guid)
		{
			Business.SmsParser smsParserController = new Business.SmsParser();
			Common.SmsParser smsParser = new Common.SmsParser();
			smsParserController.Load(guid, smsParser);
			return smsParser;
		}

		public static bool Delete(Guid guid)
		{
			Business.SmsParser smsParserController = new Business.SmsParser();
			return smsParserController.Delete(guid);
		}

		//public static DataTable GetSmsParsersOfPrivateNumber(Guid userPrivateNumberGuid)
		//{
		//	Business.SmsParser smsParserController = new Business.SmsParser();
		//	return smsParserController.GetSmsParsersOfPrivateNumber(userPrivateNumberGuid);
		//}

		public static bool ActiveSmsParser(Guid smsParserGuid)
		{
			Business.SmsParser smsParserController = new Business.SmsParser();
			Common.SmsParser smsParser = new Common.SmsParser();
			try
			{
				if (!smsParserController.Load(smsParserGuid, smsParser))
					throw new Exception();

				bool isActive = smsParser.IsActive ? false : true;
				return smsParserController.ActiveSmsParser(smsParserGuid, isActive);
			}
			catch
			{
				return false;
			}
		}

		public static bool IsDuplicateSmsParserKey(Guid parserGuid, Guid numberGuid, string key)
		{
			Business.SmsParser smsParserController = new Business.SmsParser();
			return smsParserController.IsDuplicateSmsParserKey(parserGuid, numberGuid, key);
		}

		public static bool ParseReceiveSms(Guid smsGuid, Guid numberGuid, string smsText, string mobile)
		{
			try
			{
				Business.SmsParser smsParserController = new Business.SmsParser();
				return smsParserController.ParseReceiveSms(smsGuid, numberGuid, smsText, mobile);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
	}
}
