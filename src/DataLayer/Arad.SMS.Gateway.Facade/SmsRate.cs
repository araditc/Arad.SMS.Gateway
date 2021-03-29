using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Business;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace Facade
{
	public class SmsRate : FacadeEntityBase
	{
		//internal static bool UpdateUserSmsRate(Guid userGuid, Guid groupPriceGuid, DataAccessBase dataAccessProvider)
		//{
		//  Business.SmsRate smsRateController = new Business.SmsRate(dataAccessProvider);
		//  return smsRateController.UpdateUserSmsRate(userGuid, groupPriceGuid);
		//}

		public static bool UpdateUserSmsRate(Guid userGuid, DataAccessBase dataAccessProvider)
		{
			Business.SmsRate smsRateController = new Business.SmsRate(dataAccessProvider);
			return smsRateController.UpdateUserSmsRate(userGuid);
		}

		public static List<UserSmsRateInfo> GetUserSmsRate(Guid userGuid)
		{
			Business.SmsRate smsRateController = new Business.SmsRate();
			return smsRateController.GetUserSmsRate(userGuid);
		}

		public static void ClearUserSmsRate(Guid userGuid)
		{
			Business.SmsRate smsRateController = new Business.SmsRate();
			smsRateController.ClearSmsRateCache(userGuid);
		}

		public static string GetSmsRatesToShowInHeader(Guid userGuid)
		{
			string smsRateScript = string.Empty;
			List<UserSmsRateInfo> smsRates = GetUserSmsRate(userGuid).ToList();
			DataTable dataTableAgents = Facade.SmsSenderAgent.GetAllAgents();

			smsRateScript += "[";
			foreach (DataRow row in dataTableAgents.Rows)
			{
				smsRateScript += string.Format("['{0}', '{1}', [",
																			row["Guid"].ToString(),
																			row["Name"].ToString());

				smsRateScript += string.Format("['{0}', '{1}', '{2}'], ",
																					Language.GetString(Operators.MCI.ToString()),
																					FormatSmsRate(smsRates, ((Business.SmsSenderAgentReference)Helper.GetInt(row["SmsSenderAgentReference"])), Operators.MCI, SmsTypes.Farsi),
																					FormatSmsRate(smsRates, ((Business.SmsSenderAgentReference)Helper.GetInt(row["SmsSenderAgentReference"])), Operators.MCI, SmsTypes.Latin));
				
				smsRateScript += string.Format("['{0}', '{1}', '{2}'], ",
																					Language.GetString(Operators.MTN.ToString()),
																					FormatSmsRate(smsRates, ((Business.SmsSenderAgentReference)Helper.GetInt(row["SmsSenderAgentReference"])), Operators.MTN, SmsTypes.Farsi),
																					FormatSmsRate(smsRates, ((Business.SmsSenderAgentReference)Helper.GetInt(row["SmsSenderAgentReference"])), Operators.MTN, SmsTypes.Latin));

				smsRateScript += string.Format("['{0}', '{1}', '{2}']",
																					Language.GetString(Operators.Rightel.ToString()),
																					FormatSmsRate(smsRates, ((Business.SmsSenderAgentReference)Helper.GetInt(row["SmsSenderAgentReference"])), Operators.Rightel, SmsTypes.Farsi),
																					FormatSmsRate(smsRates, ((Business.SmsSenderAgentReference)Helper.GetInt(row["SmsSenderAgentReference"])), Operators.Rightel, SmsTypes.Latin));
				smsRateScript += "]],";
			}

			if (smsRateScript.EndsWith(","))
				smsRateScript = smsRateScript.Remove(smsRateScript.Length - 1);

			smsRateScript += "]";

			return smsRateScript;
		}

		private static string FormatSmsRate(List<UserSmsRateInfo> smsRates, SmsSenderAgentReference agent, Operators operators, SmsTypes smsType)
		{
			if (smsRates.Any(rate => rate.Agent == agent && rate.Operator == operators && rate.SmsType == smsType))
				return smsRates.Where(rate => rate.Agent == agent && rate.Operator == operators && rate.SmsType == smsType).First().Rate.ToString();
			else
				return "------";
		}
	}
}
