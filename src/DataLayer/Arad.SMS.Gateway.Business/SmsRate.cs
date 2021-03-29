using System;
using System.Collections.Generic;
using Common;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;
using System.Linq;


namespace Business
{
	public class SmsRate : BusinessEntityBase
	{
		private static Dictionary<Guid, List<UserSmsRateInfo>> userSmsRate = new Dictionary<Guid, List<UserSmsRateInfo>>();

		public SmsRate(DataAccessBase dataAccessProvider = null)
			: base(TableNames.SmsRates.ToString(), dataAccessProvider)
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

		public bool UpdateUserSmsRate(Guid userGuid)
		{
			//OnUserSmsRateChange(userGuid, null);
			return base.ExecuteSPCommand("UpdateUserSmsRate", "@UserGuid", userGuid);
		}

		public List<UserSmsRateInfo> GetUserSmsRate(Guid userGuid)
		{
			//List<UserSmsRateInfo> lstUserSmsRate = userSmsRate.ContainsKey(userGuid) ? userSmsRate[userGuid] : new List<UserSmsRateInfo>();

			List<UserSmsRateInfo> userSmsInfoList = new List<UserSmsRateInfo>();

			//if (lstUserSmsRate.Count == 0)
			//{
			DataTable smsRates = base.FetchSPDataTable("GetUserSmsRates", "@UserGuid", userGuid);
			if (smsRates.Rows.Count > 0)
			{

				foreach (DataRow row in smsRates.Rows)
				{
					UserSmsRateInfo userFarsiSmsRateInfo = new UserSmsRateInfo();
					UserSmsRateInfo userLatinSmsRateInfo = new UserSmsRateInfo();
					userFarsiSmsRateInfo.SmsType = SmsTypes.Farsi;
					userFarsiSmsRateInfo.Operator = (Operators)Helper.GetInt(row["Operator"]);
					userFarsiSmsRateInfo.Agent = (SmsSenderAgentReference)Helper.GetInt(row["SmsSenderAgentReference"]);
					userFarsiSmsRateInfo.AgentName = Helper.GetString(row["Name"]);
					userFarsiSmsRateInfo.Rate = Helper.GetDecimal(row["Farsi"]);
					userSmsInfoList.Add(userFarsiSmsRateInfo);
					userLatinSmsRateInfo.SmsType = SmsTypes.Latin;
					userLatinSmsRateInfo.Operator = (Operators)Helper.GetInt(row["Operator"]);
					userLatinSmsRateInfo.Agent = (SmsSenderAgentReference)Helper.GetInt(row["SmsSenderAgentReference"]);
					userLatinSmsRateInfo.AgentName = Helper.GetString(row["Name"]);
					userLatinSmsRateInfo.Rate = Helper.GetDecimal(row["Latin"]);
					userSmsInfoList.Add(userLatinSmsRateInfo);
				}
				//if (!userSmsRate.ContainsKey(userGuid))
				//  userSmsRate.Add(userGuid, userSmsInfoList);
				//else
				//  userSmsRate[userGuid] = userSmsInfoList.ToList();
				//}
				//else
				//{
				//  if (!userSmsRate.ContainsKey(userGuid))
				//    userSmsRate.Add(userGuid, new List<UserSmsRateInfo>());
				//  else
				//    userSmsRate[userGuid] = new List<UserSmsRateInfo>();
				//}
			}
			//return userSmsRate[userGuid];
			return userSmsInfoList;
		}
	}
}
