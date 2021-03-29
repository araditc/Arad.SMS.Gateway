using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using Business;

namespace Facade
{
	public class UserSmsRate : FacadeEntityBase
	{
		public static bool UpdateUserSmsRate(Guid userGuid, Guid groupPriceGuid, DataAccessBase dataAccessProvider)
		{
			Business.UserSmsRate userSmsRateController = new Business.UserSmsRate(dataAccessProvider);
			return userSmsRateController.UpdateUserSmsRate(userGuid, groupPriceGuid);
		}

		public static bool UpdateUserSmsRate(Guid userGuid, DataAccessBase dataAccessProvider)
		{
			return UpdateUserSmsRate(userGuid, Facade.User.GetGroupPriceGuid(userGuid), dataAccessProvider);
		}

		public static Dictionary<SmsRateType, decimal> GetUserSmsRate(Guid userGuid)
		{
			Business.UserSmsRate userSmsRateController = new Business.UserSmsRate();
			return userSmsRateController.GetUserSmsRate(userGuid);
		}

		public static void ClearUserSmsRate(Guid userGuid)
		{
			Business.UserSmsRate userSmsRateController = new Business.UserSmsRate();
			userSmsRateController.ClearSmsRateCache(userGuid);
		}

		public static string GetSmsRatesToShowInHeader(Guid userGuid)
		{
			StringBuilder smsRateHtmlTable = new StringBuilder();
			Dictionary<SmsRateType, decimal> smsRates = GetUserSmsRate(userGuid);

			smsRateHtmlTable.Append("<table style=\"width: 100%;\"><tr><td></td>");
			smsRateHtmlTable.Append(string.Format(" <td style=\"padding-bottom: 5px; border-bottom: 1px solid #bbbbbb;\">{0}</td>", Language.GetString("Farsi")));
			smsRateHtmlTable.Append(string.Format(" <td style=\"padding-bottom: 5px; border-bottom: 1px solid #bbbbbb;\">{0}</td>", Language.GetString("Latin")));
			smsRateHtmlTable.Append("</tr><tr>");
			smsRateHtmlTable.Append(string.Format("<td><img src=\"{0}\" title=\"{1}\" /></td>", "MainDesktopFiles/images/Irancell.png", Language.GetString("Irancel")));
			smsRateHtmlTable.Append(string.Format(" <td>{0}</td>", FormatSmsRate(smsRates, SmsRateType.FarsiIranCell)));
			smsRateHtmlTable.Append(string.Format(" <td>{0}</td>", FormatSmsRate(smsRates, SmsRateType.LatinIranCell)));
			smsRateHtmlTable.Append("</tr><tr>");
			smsRateHtmlTable.Append(string.Format("<td><img src=\"{0}\" title=\"{1}\" /></td>", "MainDesktopFiles/images/HamrahAval.png", Language.GetString("HamrahAval")));
			smsRateHtmlTable.Append(string.Format(" <td>{0}</td>", FormatSmsRate(smsRates, SmsRateType.FarsiHamrahAval)));
			smsRateHtmlTable.Append(string.Format(" <td>{0}</td>", FormatSmsRate(smsRates, SmsRateType.LatinHamrahAval)));
			smsRateHtmlTable.Append("</tr><tr></table>");

			return smsRateHtmlTable.ToString();
		}

		private static string FormatSmsRate(Dictionary<SmsRateType, decimal> smsRates, SmsRateType smsRateType)
		{
			decimal smsRate = 0;
			if (smsRates.ContainsKey(smsRateType))
				smsRate = smsRates[smsRateType];

			if (smsRate == 0)
				return "------";
			else
				return Helper.FormatDecimalForDisplay(smsRate);
		}
	}
}
