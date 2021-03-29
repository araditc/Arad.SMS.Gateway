using System;
using System.Collections.Generic;
using System.Data;
using Common;
using GeneralLibrary.BaseCore;
using GeneralLibrary;

namespace Business
{
	public class UserSmsRate : BusinessEntityBase
	{
		private static Dictionary<Guid, Dictionary<SmsRateType, decimal>> userSmsRate = new Dictionary<Guid, Dictionary<SmsRateType, decimal>>();

		public UserSmsRate(DataAccessBase dataAccessProvider = null)
			: base(TableNames.UserSmsRates.ToString(), dataAccessProvider)
		{
			this.OnEntityChange += new EntityChangeEventHandler(OnUserSmsRateChange);
		}

		#region Event Handlers
		private void OnUserSmsRateChange(object sender, EntityChangeEventArgs e)
		{
			ClearSmsRateCache(Helper.GetGuid(sender));
			try
			{
				WinServiceHandler.SmsSendWinServiceHandlerChannel().ClearSmsRateCache(Helper.GetGuid(sender));
			}
			catch { }
		}

		public void ClearSmsRateCache(Guid userGuid)
		{
			if (userSmsRate.ContainsKey(userGuid))
				userSmsRate.Remove(userGuid);
		}
		#endregion

		public bool UpdateUserSmsRate(Guid userGuid, Guid groupPriceGuid)
		{
			OnUserSmsRateChange(userGuid, null);
			return base.ExecuteSPCommand("UpdateUserSmsRate", "@UserGuid", userGuid,
																												"@GroupPriceGuid", groupPriceGuid);
		}

		public Dictionary<SmsRateType, decimal> GetUserSmsRate(Guid userGuid)
		{
			if (!userSmsRate.ContainsKey(userGuid))
			{
				DataTable smsRates = base.FetchSPDataTable("GetUserSmsRates", "@UserGuid", userGuid);
				userSmsRate.Add(userGuid, new Dictionary<SmsRateType, decimal>());

				if (smsRates.Rows.Count > 0)
				{
					userSmsRate[userGuid].Add(SmsRateType.FarsiHamrahAval, Helper.GetDecimal(smsRates.Rows[0]["FarsiHamrahAval"]));
					userSmsRate[userGuid].Add(SmsRateType.FarsiIranCell, Helper.GetDecimal(smsRates.Rows[0]["FarsiIranCell"]));
					userSmsRate[userGuid].Add(SmsRateType.LatinHamrahAval, Helper.GetDecimal(smsRates.Rows[0]["LatinHamrahAval"]));
					userSmsRate[userGuid].Add(SmsRateType.LatinIranCell, Helper.GetDecimal(smsRates.Rows[0]["LatinIranCell"]));
				}
			}

			return userSmsRate[userGuid];
		}
	}
}
