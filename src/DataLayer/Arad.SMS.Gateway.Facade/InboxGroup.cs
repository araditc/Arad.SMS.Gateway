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
	public class InboxGroup : FacadeEntityBase
	{
		//public static Common.InboxGroup LoadInboxGroup(Guid guid)
		//{
		//	Business.SmsParser inboxGroupController = new Business.SmsParser();
		//	Common.InboxGroup inboxGroup = new Common.InboxGroup();
		//	inboxGroupController.Load(guid, inboxGroup);
		//	return inboxGroup;
		//}

		public static bool Insert(Common.InboxGroup inboxGroup)
		{
			Business.InboxGroup inboxGroupController = new Business.InboxGroup();
			return inboxGroupController.InsertInboxGroup(inboxGroup) != Guid.Empty ? true : false;
		}

		public static bool UpdateInboxGroup(Common.InboxGroup inboxGroup)
		{
			Business.InboxGroup inboxGroupController = new Business.InboxGroup();
			return inboxGroupController.UpdateInboxGroup(inboxGroup);
		}

		public static bool Delete(Guid guid)
		{
			Business.InboxGroup inboxGroupController = new Business.InboxGroup();
			return inboxGroupController.Delete(guid);
		}

		//public static string GetUserInboxGroup(Guid userGuid, bool showNumberCount, bool showCheckBox)
		//{
		//	string tree = string.Empty;
		//	Business.InboxGroup inboxGroupController = new Business.InboxGroup();
		//	DataTable dataTableInboxGroup = GetUserInboxGroups(userGuid);

		//	DataTable dataTableRoot = GetRoot();

		//	if (dataTableRoot.Rows.Count == 0)
		//		return string.Empty;

		//	Guid rootGuid = Helper.GetGuid(dataTableRoot.Rows[0]["Guid"].ToString());

		//	tree += "<div class='myList'><ul class='browser filetree'>";
		//	tree += string.Format("<li><span class='root folder' guid='{0}'>{1}{2}</span><ul id='child'>",
		//																														Guid.Empty,
		//																														showCheckBox ? "<input onclick='checkBoxControlChecked(this);' type='checkbox'/>" : string.Empty,
		//																														dataTableRoot.Rows[0]["Title"].ToString());

		//	GenerateTree(dataTableInboxGroup, rootGuid, showNumberCount, showCheckBox, ref tree);

		//	tree += "</ul></li></ul></div>";

		//	return tree;
		//}

		//public static DataTable GetRoot()
		//{
		//	Business.InboxGroup inboxGroupController=new Business.InboxGroup();
		//	return inboxGroupController.GetRoot();
		//}

		public static DataTable GetUserInboxGroups(Guid userGuid, Guid parentNodeGuid, string name)
		{
			Business.InboxGroup inboxGroupController = new Business.InboxGroup();
			return inboxGroupController.GetUserInboxGroups(userGuid, parentNodeGuid, name);
		}

		public static DataTable GetUserInboxGroups(Guid userGuid)
		{
			Business.InboxGroup inboxGroupController = new Business.InboxGroup();
			return inboxGroupController.GetUserInboxGroups(userGuid);
		}
		//private static void GenerateTree(DataTable dataTableInboxGroup, Guid root, bool showNumberCount, bool showCheckBox, ref string tree)
		//{
		//	DataView dataViewInboxGroup = dataTableInboxGroup.DefaultView;
		//	dataViewInboxGroup.RowFilter = string.Format("ParentGuid='{0}'", root);
		//	DataTable dataTableChildren = new DataTable();
		//	dataTableChildren = dataViewInboxGroup.ToTable();
		//	if (dataTableChildren.Rows.Count > 0)
		//		tree += "<ul>";
		//	for (int childrenCounter = 0; childrenCounter < dataTableChildren.Rows.Count; childrenCounter++)
		//	{
		//		tree += string.Format("<li><span class='folder' guid='{0}' isActive='True'>{1}{2}{3}</span>",
		//																	dataTableChildren.Rows[childrenCounter]["Guid"].ToString(),
		//																	showCheckBox ? "<input id='Checkbox" + (childrenCounter + 1) + "' onclick='checkBoxControlChecked(this);' type='checkbox'/>" : string.Empty,
		//																	dataTableChildren.Rows[childrenCounter]["Title"].ToString(),
		//																	showNumberCount ? (" (" + dataTableChildren.Rows[childrenCounter]["CountInboxNumbers"].ToString() + ")") : string.Empty);

		//		Guid childrenGuid = Helper.GetGuid(dataTableChildren.Rows[childrenCounter]["Guid"].ToString());
		//		GenerateTree(dataTableInboxGroup, childrenGuid, showNumberCount, showCheckBox, ref tree);
		//		tree += "</li>";
		//	}
		//	if (dataTableChildren.Rows.Count > 0)
		//		tree += "</ul>";
		//}

		//public static bool ChangeParent(Guid groupGuid, Guid parentGuid)
		//{
		//	Business.InboxGroup inboxGroupController = new Business.InboxGroup();
		//	if (parentGuid == Guid.Empty)
		//		parentGuid = Helper.GetGuid(inboxGroupController.GetRoot().Rows[0]["Guid"]);

		//	return inboxGroupController.UpdateParent(groupGuid, parentGuid);
		//}

		public static bool DeleteItemFromInboxGroups(Guid groupGuid)
		{
			Business.InboxGroup inboxGroupController = new Business.InboxGroup();
			return inboxGroupController.Delete(groupGuid);
		}

		public static bool EditItemInGroups(Common.InboxGroup inboxGroup)
		{
			Business.InboxGroup inboxGroupController = new Business.InboxGroup();
			try
			{
				if (!inboxGroupController.UpdateName(inboxGroup))
					throw new Exception(Language.GetString("ErrorRecord"));

				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static Guid InsertItemInInboxGroups(Common.InboxGroup inboxGroup)
		{
			Business.InboxGroup inboxGroupController = new Business.InboxGroup();
			return inboxGroupController.Insert(inboxGroup);
		}

		//public static string GetGroupName(Guid groupGuid)
		//{
		//	Business.InboxGroup inboxGroupController = new Business.InboxGroup();
		//	return inboxGroupController.GetGroupName(groupGuid);
		//}
	}
}
