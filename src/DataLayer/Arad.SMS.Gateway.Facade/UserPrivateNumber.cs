using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;
using Business;
using Common;

namespace Facade
{
	public class UserPrivateNumber : FacadeEntityBase
	{
		public static DataTable GetNumberForAssignToUser(Guid userGuid, Business.TypePrivateNumberAccesses type)
		{
			Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
			return userPrivateNumberController.GetNumberForAssignToUser(userGuid, type);
		}

		//public static Common.UserPrivateNumber LoadUserNumber(Guid userNumberGuid)
		//{
		//	Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
		//	Common.UserPrivateNumber userPrivateNumber = new Common.UserPrivateNumber();
		//	userPrivateNumberController.Load(userNumberGuid, userPrivateNumber);
		//	return userPrivateNumber;
		//}

		//public static bool UpdateUserPrivateNumber(Common.UserPrivateNumber userPrivateNumber,
		//																					bool updateChildren,
		//																					bool currentIsActive,
		//																					bool decreaseFromPanel,
		//																					PrivateNumberUseType useType,
		//																					Guid parentGuid)
		//{
		//	Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
		//	userPrivateNumberController.BeginTransaction();
		//	try
		//	{
		//		if (currentIsActive != userPrivateNumber.IsActive && updateChildren)
		//		{
		//			if (UpdateAllChildren(userPrivateNumber.UserPrivateNumberGuid, userPrivateNumber.IsActive, false, parentGuid, userPrivateNumberController))
		//				userPrivateNumber.ActivationUserGuid = parentGuid;
		//			else
		//				throw new Exception(Language.GetString("ErrorRecord"));
		//		}

		//		if (decreaseFromPanel)
		//			Facade.Transaction.Decrease(userPrivateNumber.UserGuid, userPrivateNumber.Price, TypeCreditChanges.PriceNumber, Language.GetString("PriceNumberDecreaseCreditTransaction"), userPrivateNumberController.DataAccessProvider);

		//		if (useType == PrivateNumberUseType.Sold && userPrivateNumber.UseType != (int)PrivateNumberUseType.Sold)
		//		{
		//			if (!UpdateAllChildren(userPrivateNumber.UserPrivateNumberGuid, false, true, parentGuid, userPrivateNumberController))
		//				throw new Exception(Language.GetString("ErrorRecord"));
		//		}
		//		else if (useType == PrivateNumberUseType.Shared && userPrivateNumber.UseType != (int)PrivateNumberUseType.Shared)
		//			if (!UpdateAllChildren(userPrivateNumber.UserPrivateNumberGuid, false, true, parentGuid, userPrivateNumberController))
		//				throw new Exception(Language.GetString("ErrorRecord"));

		//		if (!userPrivateNumberController.UpdateUserPrivateNumber(userPrivateNumber))
		//			throw new Exception(Language.GetString("ErrorRecord"));

		//		userPrivateNumberController.CommitTransaction();
		//		return true;
		//	}
		//	catch (Exception ex)
		//	{
		//		userPrivateNumberController.RollbackTransaction();
		//		throw ex;
		//	}

		//}

		//private static bool UpdateAllChildren(Guid guid, bool isActive, bool isDeleted, Guid activationUserGuid, Business.UserPrivateNumber userPrivateNumberController)
		//{
		//	return userPrivateNumberController.UpdateAllChildren(guid, isActive, isDeleted, activationUserGuid);
		//}

		//public static bool UpdateUserNumberUseType(Common.UserPrivateNumber userPrivateNumber)
		//{
		//	Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
		//	return userPrivateNumberController.UpdateUserNumberUseType(userPrivateNumber);
		//}

		//public static bool HasConflictUserPrivateNumberDateTime(Common.UserPrivateNumber userPrivateNumber)
		//{
		//	Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
		//	return userPrivateNumberController.HasConflictUserPrivateNumberDateTime(userPrivateNumber);
		//}

		//public static bool Insert(Common.UserPrivateNumber userPrivateNumber, PrivateNumberUseType privateNumberUseType)
		//{
		//	Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
		//	userPrivateNumberController.BeginTransaction();
		//	try
		//	{
		//		switch (privateNumberUseType)
		//		{
		//			case PrivateNumberUseType.Sale:
		//				userPrivateNumber.UseType = (int)PrivateNumberUseType.Sale;
		//				privateNumberUseType = PrivateNumberUseType.Sold;
		//				break;
		//			case PrivateNumberUseType.Shared:
		//				userPrivateNumber.UseType = (int)PrivateNumberUseType.SharedUse;
		//				privateNumberUseType = PrivateNumberUseType.Shared;
		//				break;
		//		}

		//		if (userPrivateNumber.DecreaseFromPanel)
		//			Facade.Transaction.Decrease(userPrivateNumber.UserGuid, userPrivateNumber.Price, TypeCreditChanges.PriceNumber, Language.GetString("PriceNumberDecreaseCreditTransaction"), userPrivateNumberController.DataAccessProvider);

		//		if (userPrivateNumberController.Insert(userPrivateNumber) == Guid.Empty)
		//			throw new Exception(Language.GetString("ErrorRecord"));

		//		userPrivateNumber.UseType = (int)privateNumberUseType;
		//		userPrivateNumber.UserPrivateNumberGuid = userPrivateNumber.UserPrivateNumberParentGuid;
		//		if (!userPrivateNumberController.UpdateUserNumberUseType(userPrivateNumber))
		//			throw new Exception(Language.GetString("ErrorRecord"));

		//		userPrivateNumberController.CommitTransaction();
		//		return true;
		//	}
		//	catch (Exception ex)
		//	{
		//		userPrivateNumberController.RollbackTransaction();
		//		throw ex;
		//	}
		//}

		//public static DataTable GetPagedUserPrivateNumber(Common.UserPrivateNumber userPrivateNumber,
		//																									Guid smsSenderAgentGuid,
		//																									string number,
		//																									int activeStatus,
		//																									int expired,
		//																									int useForChildren,
		//																									int useType,
		//																									int priceRange,
		//																									DateTime fromStartDate,
		//																									DateTime toStartDate,
		//																									DateTime fromEndDate,
		//																									DateTime toEndDate,
		//																									string sortField,
		//																									int pageNo,
		//																									int pageSize,
		//																									ref int resultCount)
		//{
		//	Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
		//	return userPrivateNumberController.GetPagedUserPrivateNumber(userPrivateNumber,
		//																															 smsSenderAgentGuid,
		//																															 number,
		//																															 activeStatus,
		//																															 expired,
		//																															 useForChildren,
		//																															 useType,
		//																															 priceRange,
		//																															 fromStartDate,
		//																															 toStartDate,
		//																															 fromEndDate,
		//																															 toEndDate,
		//																															 sortField,
		//																															 pageNo,
		//																															 pageSize,
		//																															 ref resultCount);
		//}

		//public static bool Delete(Guid guid)
		//{
		//	Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
		//	return userPrivateNumberController.Delete(guid);
		//}

		public static DataTable GetPrivateNumberInfo(string privateNumber)
		{
			Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
			return userPrivateNumberController.GetUserPrivateNumberInfoForReceive(privateNumber);
		}

		public static DataTable GetUserPrivateNumbersForSend(Guid userGuid, Guid parentGuid, string privateNumber, PrivateNamberSendType sendType)
		{
			Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
			return userPrivateNumberController.GetUserPrivateNumbersForSend(userGuid, parentGuid, privateNumber, sendType);
		}

		public static DataTable GetUserPrivateNumbersForSend(Guid userGuid, Guid parentGuid)
		{
			Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
			return GetUserPrivateNumbersForSend(userGuid, parentGuid, string.Empty, PrivateNamberSendType.NormalSend);
		}

		public static object GetUserPrivateNumbersForRecieve(Guid userGuid)
		{
			Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
			return userPrivateNumberController.GetUserPrivateNumbersForRecieve(userGuid);
		}

		public static DataTable GetUserPrivateNumbersForSendBulk(Guid userGuid, Guid parentGuid)
		{
			Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
			return GetUserPrivateNumbersForSend(userGuid, parentGuid, string.Empty, PrivateNamberSendType.BulkSend);
		}

		public static SmsSenderAgentReference GetPrivateNumberSenderAgentReference(Guid userPrivateNumberGuid)
		{
			Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
			return userPrivateNumberController.GetSmsSenderAgentReference(userPrivateNumberGuid);
		}

		public static DataTable GetSendUserPrivateNumberForSmsParser(Guid receiveUserPrivateNumberGuid, Guid userGuid)
		{
			Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
			return userPrivateNumberController.GetSendUserPrivateNumberForSmsParser(receiveUserPrivateNumberGuid, userGuid);
		}

		internal static DataTable GetAgentOfUserPrivateNumber(Guid userPrivateNumberGuid)
		{
			Business.UserPrivateNumber userPrivateNumberController = new Business.UserPrivateNumber();
			return userPrivateNumberController.GetAgentOfUserPrivateNumber(userPrivateNumberGuid);
		}
	}
}
