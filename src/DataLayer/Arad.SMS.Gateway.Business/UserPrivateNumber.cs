using System;
using System.Data;
using Common;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace Business
{
	public class UserPrivateNumber : BusinessEntityBase
	{
		public UserPrivateNumber(DataAccessBase dataAccessProvider = null)
			: base(TableNames.UserPrivateNumbers.ToString(), dataAccessProvider)
		{
			this.OnEntityChange += new EntityChangeEventHandler(OnUserPrivateNumberChange);
		}

		#region Event Handlers
		private void OnUserPrivateNumberChange(object sender, EntityChangeEventArgs e)
		{
			int smsSenderAgentRefrence;

			if (sender is Common.UserPrivateNumber)
				smsSenderAgentRefrence = new SmsSenderAgent().GetSmsSenderAgentRefrence(((Common.UserPrivateNumber)sender).PrivateNumberGuid);
			else
				smsSenderAgentRefrence = (int)this.GetSmsSenderAgentReference(Helper.GetGuid(sender));

			if (smsSenderAgentRefrence != 0)
			{
				try
				{
					WinServiceHandler.SmsSendWinServiceHandlerChannel().ClearPrivateNumberCache((SmsSenderAgentReference)smsSenderAgentRefrence);
				}
				catch { }
			}
		}
		#endregion

		public DataTable GetNumberForAssignToUser(Guid userGuid, Business.TypePrivateNumberAccesses type)
		{
			return base.FetchSPDataTable("GetUserPrivateNumberForAssignToUser", "@UserGuid", userGuid, "@Type", (int)type);
		}

		public bool UpdateUserPrivateNumber(Common.UserPrivateNumber userPrivateNumber)
		{
			bool isUpdateCompleted = false;

			isUpdateCompleted = base.ExecuteSPCommand("UpdateUserPrivateNumber", "@Guid", userPrivateNumber.UserPrivateNumberGuid,
																																					"@StartDate", userPrivateNumber.StartDate,
																																					"@EndDate", userPrivateNumber.EndDate,
																																					"@IsActive", userPrivateNumber.IsActive,
																																					"@UseForChildren", userPrivateNumber.UseForChildren,
																																					"@DecreaseFromPanel", userPrivateNumber.DecreaseFromPanel,
																																					"@UseType", userPrivateNumber.UseType,
																																					"@Price", userPrivateNumber.Price,
																																					"@ActivationUserGuid", userPrivateNumber.ActivationUserGuid
																																					);

			if (isUpdateCompleted)
				OnUserPrivateNumberChange(userPrivateNumber, null);

			return isUpdateCompleted;
		}

		public bool UpdateUserNumberUseType(Common.UserPrivateNumber userPrivateNumber)
		{
			bool isUpdateCompleted = false;

			isUpdateCompleted = base.ExecuteSPCommand("UpdateUserNumberUseType", "@Guid", userPrivateNumber.UserPrivateNumberGuid,
																																						"@UseType", userPrivateNumber.UseType);

			if (isUpdateCompleted)
				OnUserPrivateNumberChange(userPrivateNumber, null);

			return isUpdateCompleted;
		}

		public bool UpdateAllChildren(Guid guid, bool isActive, bool isDeleted, Guid activationUserGuid)
		{
			bool isUpdateCompleted = false;

			isUpdateCompleted = base.ExecuteSPCommand("UpdateAllChildren", "@RootGuid", guid,
																																			"@IsDeleted", isDeleted,
																																			"@IsActive", isActive,
																																			"@ActivationUserGuid", activationUserGuid);

			if (isUpdateCompleted)
				OnUserPrivateNumberChange(guid, null);

			return isUpdateCompleted;
		}

		public bool HasConflictUserPrivateNumberDateTime(Common.UserPrivateNumber userPrivateNumber)
		{
			DataTable dataTableNumber = base.FetchSPDataTable("CheckDateTimeForPrivateNumber", "@RootGuid", userPrivateNumber.UserPrivateNumberGuid,
																																											"@StartDate", userPrivateNumber.StartDate,
																																											"@EndDate", userPrivateNumber.EndDate);

			return dataTableNumber.Rows.Count > 0;
		}

		public DataTable GetUserPrivateNumbersForSend(Guid userGuid, Guid parentGuid, string privateNumber, PrivateNamberSendType sendType)
		{
			return base.FetchSPDataTable("GetUserPrivateNumbersForSend", "@UserGuid", userGuid,
																																	 "@ParentGuid", parentGuid,
																																	 "@SendType", (int)sendType,
																																	 "@PrivateNumber", privateNumber);
		}

		public DataTable GetPagedUserPrivateNumber(Common.UserPrivateNumber userPrivateNumber,
																							 Guid smsSenderAgentGuid,
																							 string number,
																							 int activeStatus,
																							 int expired,
																							 int useForChildren,
																							 int useType,
																							 int priceRange,
																							 DateTime fromStartDate,
																							 DateTime toStartDate,
																							 DateTime fromEndDate,
																							 DateTime toEndDate,
																							 string sortField,
																							 int pageNo,
																							 int pageSize,
																							 ref int resultCount)
		{
			DataSet userNumbersInfo = base.FetchSPDataSet("GetPagedUserPrivateNumber", "@UserGuid", userPrivateNumber.UserGuid,
																																"@SmsSenderAgentGuid", smsSenderAgentGuid,
																																"@Number", number,
																																"@Type", userPrivateNumber.Type,
																																"@IsActive", (activeStatus == 2 ? DBNull.Value : (object)activeStatus),
																																"@UseType", (useType == -1 ? DBNull.Value : (object)useType),
																																"@Expired", (expired == 2 ? DBNull.Value : (object)expired),
																																"@UseForChildren", (useForChildren == 2 ? DBNull.Value : (object)useForChildren),
																																"@FromStartDate", fromStartDate,
																																"@ToStartDate", toStartDate,
																																"@FromEndDate", fromEndDate,
																																"@ToEndDate", toEndDate,
																																"@Price", userPrivateNumber.Price,
																																"@PriceRange", (priceRange == 2 ? DBNull.Value : (object)priceRange),
																																"@PageNo", pageNo,
																																"@PageSize", pageSize,
																																"@SortField", sortField);
			resultCount = Helper.GetInt(userNumbersInfo.Tables[0].Rows[0]["RowCount"]);

			return userNumbersInfo.Tables[1];
		}

		public DataTable GetUserPrivateNumberInfoForReceive(string privateNumber)
		{
			return base.FetchSPDataTable("GetUserPrivateNumberInfoForReceive", "@PrivateNumber", privateNumber);
		}

		public object GetUserPrivateNumbersForRecieve(Guid userGuid)
		{
			return FetchSPDataTable("GetUserPrivateNumberForReceive", "@UserGuid", userGuid);
		}

		public SmsSenderAgentReference GetSmsSenderAgentReference(Guid userPrivateNumberGuid)
		{
			return (SmsSenderAgentReference)base.GetSPIntFieldValue("GetSmsSenderAgentReference", "@UserPrivateNumberGuid", userPrivateNumberGuid);
		}


		public DataTable GetSendUserPrivateNumberForSmsParser(Guid receiveUserPrivateNumberGuid, Guid userGuid)
		{
			return FetchSPDataTable("GetSendUserPrivateNumberForSmsParser", "@ReceiveUserPrivateNumberGuid", receiveUserPrivateNumberGuid,
																																		 "@UserGuid", userGuid);
		}

		public DataTable GetAgentOfUserPrivateNumber(Guid userPrivateNumberGuid)
		{
			return FetchSPDataTable("GetAgentOfUserPrivateNumber", "@Guid", userPrivateNumberGuid);
		}
	}
}