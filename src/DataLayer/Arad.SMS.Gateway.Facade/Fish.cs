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
	public class Fish : FacadeEntityBase
	{
		public static bool InsertFishPayment(Common.Fish fish)
		{
			Business.Fish fishController = new Business.Fish();
			try
			{
				if (fish.Amount <= 0)
					throw new Exception(Language.GetString("IncorrectAmount"));
				if (Helper.CheckDataConditions(fish.BillNumber).IsEmpty)
					throw new Exception(Language.GetString("IsEmptyFishNumber"));
				if (Helper.CheckDataConditions(fish.PaymentDate).IsEmpty)
					throw new Exception(Language.GetString("CompletePaymentDate"));
				if (Fish.IsDuplicateBillNumber(fish.BillNumber))
					throw new Exception(Language.GetString("IsDuplicateFishNumber"));

				return fishController.InsertFishPayment(fish);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private static bool IsDuplicateBillNumber(string billNumber)
		{
			Business.Fish fishController = new Business.Fish();
			return fishController.IsDuplicateBillNumber(billNumber);
		}

		public static bool InsertOnlinePayment(Common.Fish fish)
		{
			Business.Fish fishController = new Business.Fish();

			try
			{
				if (fish.Amount <= 0)
					throw new Exception(Language.GetString("IncorrectAmount"));

				fishController.InsertOnlinePayment(fish);
				return true;
			}
			catch
			{
				throw;
			}
		}

		public static Common.Fish LoadFish(Guid fishGuid)
		{
			Business.Fish fishController = new Business.Fish();
			Common.Fish fish = new Common.Fish();
			fishController.Load(fishGuid, fish);
			return fish;
		}

		public static bool UpdateStatus(Guid fishGuid, Business.FishStatus status)
		{
			Business.Fish fishController = new Business.Fish();
			return fishController.UpdateStatus(fishGuid, status, Guid.Empty);
		}

		public static bool UpdateDescription(Guid fishGuid, string description, Business.FishStatus status)
		{
			Business.Fish fishController = new Business.Fish();
			return fishController.UpdateDescription(fishGuid, description, status);
		}

		public static DataTable GetPagedFishesForConfirm(string query, Guid parentGuid, Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount, ref int totalSmsCount, ref decimal totalPrice)
		{
			Business.Fish fishController = new Business.Fish();
			return fishController.GetPagedFishesForConfirm(query, parentGuid, userGuid, sortField, pageNo, pageSize, ref resultCount, ref totalSmsCount, ref totalPrice);
		}

		public static DataTable GetPagedUserFishes(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.Fish fishController = new Business.Fish();
			return fishController.GetPagedUserFishes(userGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool ConfirmFish(Guid userGuid, decimal smsCount, Business.TypeCreditChanges typeCreditChanges, string descriptionIncrease, Guid fishGuid)
		{
			Business.Fish fishController = new Business.Fish();
			Common.User user = new Common.User();
			string descriptionDecrease = string.Empty;

			Common.Fish fish = Fish.LoadFish(fishGuid);

			if (fish.Status != (int)Business.FishStatus.Confirmed)
			{
				fishController.BeginTransaction();
				try
				{
					user = Facade.User.LoadUser(userGuid);
					descriptionDecrease = string.Format(Language.GetString("DecreasePaymentTransaction"), user.UserName, smsCount, fish.BillNumber);

					if (!user.IsMainAdmin)
						Transaction.Decrease(user.ParentGuid, smsCount, typeCreditChanges, descriptionDecrease, fishGuid, fishController.DataAccessProvider);
					Guid transactionGuid = Facade.Transaction.Increase(userGuid, smsCount, typeCreditChanges, descriptionIncrease, fishGuid, !user.IsFixPriceGroup, fishController.DataAccessProvider);

					if (!fishController.UpdateStatus(fishGuid, Business.FishStatus.Confirmed, transactionGuid))
						throw new Exception(Language.GetString("ErrorRecord"));

					fishController.CommitTransaction();
					return true;
				}
				catch (Exception ex)
				{
					fishController.RollbackTransaction();
					throw ex;
				}
			}
			else
				return true;
		}

		public static bool ConfirmOnlineFish(Guid userGuid, decimal smsCount, Business.TypeCreditChanges typeCreditChanges,
																				 string descriptionIncrease, Guid fishGuid, long billNumber)
		{
			Business.Fish fishController = new Business.Fish();
			Common.User user = new Common.User();
			string descriptionDecrease = string.Empty;

			Common.Fish fish = Fish.LoadFish(fishGuid);

			if (fish.Status != (int)Business.FishStatus.Confirmed)
			{
				fishController.BeginTransaction();
				try
				{
					user = User.LoadUser(userGuid);
					descriptionDecrease = string.Format(Language.GetString("DecreasePaymentTransaction"), user.UserName, smsCount, billNumber);

					Common.User parent = User.LoadUser(user.ParentGuid);

					if (!parent.IsMainAdmin)
						Transaction.Decrease(user.ParentGuid, smsCount, typeCreditChanges, descriptionDecrease, fish.FishGuid, fishController.DataAccessProvider);
					Guid transactionGuid = Transaction.Increase(userGuid, smsCount, typeCreditChanges, descriptionIncrease, fishGuid, !user.IsFixPriceGroup, fishController.DataAccessProvider);

					if (!fishController.UpdateOnlineFish(fishGuid, Business.FishStatus.Confirmed, billNumber, transactionGuid))
						throw new Exception(Language.GetString("ErrorRecord"));

					fishController.CommitTransaction();
					return true;
				}
				catch
				{
					fishController.RollbackTransaction();
					return false;
				}
			}
			else
				return true;
		}
	}
}
