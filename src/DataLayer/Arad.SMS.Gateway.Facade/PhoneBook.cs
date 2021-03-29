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
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary;
using GeneralTools.TreeView;

namespace Arad.SMS.Gateway.Facade
{
    public class PhoneBook
	{
		public static DataTable GetPhoneBookOfUser(Guid userGuid)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			return phoneBookController.GetPhoneBookUser(userGuid);
		}

		public static DataTable GetPhoneBookOfUser(Guid userGuid, Guid parentNodeGuid, string name, bool loadAllPhoneBook = false)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			return phoneBookController.GetPhoneBookUser(userGuid, parentNodeGuid, name, loadAllPhoneBook);
		}

		public static List<TreeNode> GetAllPhoneBookOfUser(Guid userGuid)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			try
			{
				List<DataRow> lstGroups = phoneBookController.GetPhoneBookUser(userGuid, Guid.Empty, string.Empty, true).AsEnumerable().ToList();
				List<DataRow> lstRoots = lstGroups.Where(row => Helper.GetGuid(row["ParentGuid"]) == Guid.Empty).ToList();
				var nodes = new List<TreeNode>();

				foreach (DataRow row in lstRoots)
				{
					var node = new TreeNode();
					node.id = string.Format("'{0}'", row["Guid"]);
					node.state = "open";
					node.text = row["Name"].ToString();
					node.attributes = new { count = row["CountPhoneNumbers"].ToString(), type = Helper.GetInt(row["Type"], 1) };
					node.children = GenerateTree(lstGroups, Helper.GetGuid(row["Guid"]));
					nodes.Add(node);
				}
				return nodes;
			}
			catch
			{
				return new List<TreeNode>();
			}
		}

		private static List<TreeNode> GenerateTree(List<DataRow> lstGroups, Guid rootGuid)
		{
			try
			{
				List<DataRow> lstChildren = lstGroups.Where(row => Helper.GetGuid(row["ParentGuid"]) == rootGuid).ToList();
				var nodes = new List<TreeNode>();

				foreach (DataRow row in lstChildren)
				{
					var node = new TreeNode();
					node.id = string.Format("'{0}'", row["Guid"]);
					node.state = "open";
					node.text = row["Name"].ToString();
					node.attributes = new { count = row["CountPhoneNumbers"].ToString(), type = Helper.GetInt(row["Type"], 1) };
					node.children = GenerateTree(lstGroups, Helper.GetGuid(row["Guid"]));
					nodes.Add(node);
				}

				return nodes;
			}
			catch
			{
				return new List<TreeNode>();
			}
		}

		public static Guid InsertItemInPhoneBook(Common.PhoneBook phoneBook)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			Business.User userController = new Business.User();
			Common.User user = new Common.User();

			try
			{
				//if (!phoneBookController.CheckingName(phoneBook.Name, phoneBook.UserGuid))
				//	return Guid.Empty;
				userController.Load(phoneBook.UserGuid, user);

				phoneBook.AdminGuid = user.IsAdmin ? phoneBook.UserGuid : user.ParentGuid;
				phoneBook.CreateDate = DateTime.Now;
				phoneBook.IsPrivate = false;

				return phoneBookController.Insert(phoneBook);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool EditItemInPhoneBook(Common.PhoneBook phoneBook)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			try
			{
				if (!phoneBookController.UpdateName(phoneBook))
					throw new Exception(Language.GetString("ErrorRecord"));

				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool DeleteItemFromPhoneBook(Guid phoneBookGuid)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			return phoneBookController.Delete(phoneBookGuid);
		}

		//public static bool ChangeParent(Guid phoneBookGuid, Guid parentGuid)
		//{
		//	Business.PhoneBook phoneBookController = new Business.PhoneBook();
		//	if (parentGuid == Guid.Empty)
		//		parentGuid = Helper.GetGuid(phoneBookController.GetRoot().Rows[0]["Guid"]);

		//	return phoneBookController.UpdateParent(phoneBookGuid, parentGuid);
		//}

		public static int GetCountNumberUser(Guid userGuid)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			return phoneBookController.GetCountNumberUser(userGuid);
		}

		public static string GetName(Guid phoneBookGuid)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			Common.PhoneBook phoneBook = new Common.PhoneBook();
			phoneBookController.Load(phoneBookGuid, phoneBook);

			return phoneBook.Name;
		}

		//public static string GetPhoneBookName(Guid guid)
		//{
		//	Business.PhoneBook phoneBookController = new Business.PhoneBook();
		//	return phoneBookController.GetPhoneBookName(guid);
		//}

		public static Common.PhoneBook Load(Guid phoneBookGuid)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			Common.PhoneBook phoneBook = new Common.PhoneBook();
			phoneBookController.Load(phoneBookGuid, phoneBook);
			return phoneBook;
		}

		public static bool UpdateVasSetting(Common.PhoneBook phoneBook)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			return phoneBookController.UpdateVasSetting(phoneBook);
		}

		public static bool RegisterService(string mobile, string serviceId, ref bool numberExist)
		{
			try
			{
				Business.PhoneBook phoneBookController = new Business.PhoneBook();
				return phoneBookController.RegisterService(mobile, serviceId, ref numberExist);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool UnSubscribeService(string mobile, string serviceId, ref bool numberExist)
		{
			try
			{
				Business.PhoneBook phoneBookController = new Business.PhoneBook();
				return phoneBookController.UnSubscribeService(mobile, serviceId, ref numberExist);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetActiveServices(string mobile)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			return phoneBookController.GetActiveServices(mobile);
		}

		public static DataTable GetDisabledServices(string mobile)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			return phoneBookController.GetDisabledServices(mobile);
		}

		public static DataTable GetAllServices()
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			return phoneBookController.GetAllServices();
		}

		public static Guid GetUserVasGroup(int groupId, string serviceId, Guid userGuid)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			return phoneBookController.GetUserVasGroup(groupId, serviceId, userGuid);
		}

		public static Guid RecipientIsRegisteredToVasGroup(int groupId, string receiver)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			return phoneBookController.RecipientIsRegisteredToVasGroup(groupId, receiver);
		}

		public static DataTable GetPhoneBookInfo(string phoneBookGuids)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			return phoneBookController.GetPhoneBookInfo(phoneBookGuids);
		}

		public static bool CheckPhoneBookType(int numberType, string phoneBookGuids)
		{
			try
			{
				List<DataRow> lstPhoneBookTypes = GetPhoneBookInfo(phoneBookGuids).AsEnumerable().ToList();
				int vasTypeCount = lstPhoneBookTypes.Where(type => Helper.GetInt(type["Type"]) == (int)Business.PhoneBookGroupType.Vas).Count();
				int normalTypeCount = lstPhoneBookTypes.Where(type => Helper.GetInt(type["Type"]) == (int)Business.PhoneBookGroupType.Normal).Count();

				if (numberType == (int)Business.TypePrivateNumberAccesses.Bulk && vasTypeCount > 0)
					throw new Exception(Language.GetString("SenderIdToVasGroupIsInvalid"));

				if (numberType != (int)Business.TypePrivateNumberAccesses.Bulk && normalTypeCount > 0)
					throw new Exception(Language.GetString("SenderIdToGroupIsInvalid"));

				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool UpdateGroup(Guid phoneBookGuid, Business.PhoneBookGroupType type, string name)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			return phoneBookController.UpdateGroup(phoneBookGuid, type, name);
		}

		public static void GetUserMaximumRecordInfo(Guid userGuid, ref int mobileCount, ref int emailCount)
		{
			Business.PhoneBook phoneBookController = new Business.PhoneBook();
			DataTable dtInfo = phoneBookController.GetUserMaximumRecordInfo(userGuid);
			if (dtInfo.Rows.Count > 0)
			{
				mobileCount = Helper.GetInt(dtInfo.Rows[0]["MobileCount"]);
				emailCount = Helper.GetInt(dtInfo.Rows[0]["EmailCount"]);
			}
		}
	}
}
