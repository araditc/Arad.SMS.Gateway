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
using Arad.SMS.Gateway.Common;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Business
{
	public class PrivateNumber : BusinessEntityBase
	{
		public PrivateNumber(DataAccessBase dataAccessProvider = null)
			: base(TableNames.PrivateNumbers.ToString(), dataAccessProvider)
		{
			this.OnEntityChange += new EntityChangeEventHandler(OnPrivateNumberChange);
		}

		#region Event Handlers
		private void OnPrivateNumberChange(object sender, EntityChangeEventArgs e)
		{
			int smsSenderAgentRefrence;

			if (sender is Common.PrivateNumber)
				smsSenderAgentRefrence = new SmsSenderAgent().GetSmsSenderAgentRefrence(((Common.PrivateNumber)sender).SmsSenderAgentGuid);
			else
				smsSenderAgentRefrence = this.GetSmsSenderAgentReference(Helper.GetGuid(sender));

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

		public DataTable GetPagedNumbers(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet numbersInfo = base.FetchSPDataSet("GetPagedNumbers",
																								"@UserGuid", userGuid,
																								"@Query", query,
																								"@PageNo", pageNo,
																								"@PageSize", pageSize,
																								"@SortField", sortField);
			resultCount = Helper.GetInt(numbersInfo.Tables[0].Rows[0]["RowCount"]);

			return numbersInfo.Tables[1];
		}

		public bool UpdateExpireDate(Guid privateNumberGuid, DateTime expireDate)
		{
			return ExecuteSPCommand("UpdateExpireDate",
															"@Guid", privateNumberGuid,
															"@ExpireDate", expireDate);
		}

		public Guid InsertNumber(Common.PrivateNumber privateNumber)
		{
			try
			{
				Guid newGuid = Guid.Empty;

				newGuid = base.Insert(privateNumber);

				if (newGuid != Guid.Empty)
					OnPrivateNumberChange(privateNumber, null);

				return newGuid;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool UpdateNumber(Common.PrivateNumber privateNumber)
		{
			bool updateCompleted = false;

			updateCompleted = base.ExecuteSPCommand("UpdateNumber",
																							"@Guid", privateNumber.NumberGuid,
																							"@Number", privateNumber.Number,
																							"@Price", privateNumber.Price,
																							"@ServiceID", privateNumber.ServiceID,
																							"@MTNServiceId", privateNumber.MTNServiceId,
																							"@AggServiceId", privateNumber.AggServiceId,
																							"@ServicePrice", privateNumber.ServicePrice,
																							"@ExpireDate", privateNumber.ExpireDate,
																							"@Type", privateNumber.Type,
																							"@Priority", privateNumber.Priority,
																							"@ReturnBlackList", privateNumber.ReturnBlackList,
																							"@SendToBlackList", privateNumber.SendToBlackList,
																							"@CheckFilter", privateNumber.CheckFilter,
																							"@DeliveryBase", privateNumber.DeliveryBase,
																							"@HasSLA", privateNumber.HasSLA,
																							"@TryCount", privateNumber.TryCount,
																							"@Range", privateNumber.Range,
																							"@Regex", privateNumber.Regex,
																							"@UseForm", privateNumber.UseForm,
																							"@IsRoot", privateNumber.IsRoot,
																							"@IsActive", privateNumber.IsActive,
																							"@IsPublic", privateNumber.IsPublic,
																							"@SmsSenderAgentGuid", privateNumber.SmsSenderAgentGuid);

			if (updateCompleted)
				OnPrivateNumberChange(privateNumber, null);

			return updateCompleted;
		}

		public bool UpdateTrafficRelay(Common.PrivateNumber privateNumber)
		{
			return ExecuteSPCommand("UpdateTrafficRelay",
															"@Guid", privateNumber.NumberGuid,
															"@SmsTrafficRelayGuid", privateNumber.SmsTrafficRelayGuid,
															"@DeliveryTrafficRelay", privateNumber.DeliveryTrafficRelayGuid);
		}

		public int GetSmsSenderAgentReference(Guid privateNumberGuid)
		{
			return base.GetIntFieldValue("GetSmsSenderAgentReference", "@PrivateNumberGuid", privateNumberGuid);
		}

		public DataTable GetUserNumbers(Guid userGuid, string query, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			DataSet numbersInfo = base.FetchSPDataSet("GetUserNumbers",
																								"@OwnerGuid", userGuid,
																								"@Query", query,
																								"@PageNo", pageNo,
																								"@PageSize", pageSize,
																								"@SortField", sortField);
			resultCount = Helper.GetInt(numbersInfo.Tables[0].Rows[0]["RowCount"]);

			return numbersInfo.Tables[1];
		}

		public bool AssignNumberToUser(Guid numberGuid, Guid userGuid, string keyword, decimal price, DateTime expireDate)
		{
			return ExecuteSPCommand("AssignNumberToUser",
															"@NumberGuid", numberGuid,
															"@OwnerGuid", userGuid,
															"@Keyword", keyword,
															"@Price", price,
															"@ExpireDate", expireDate);
		}

		public bool AssignRangeNumberToUser(Guid numberGuid, Guid userGuid, decimal price, DateTime expireDate)
		{
			return ExecuteSPCommand("AssignRangeNumberToUser",
															"@NumberGuid", numberGuid,
															"@OwnerGuid", userGuid,
															"@Price", price,
															"@ExpireDate", expireDate);
		}

		public bool AssignSubRangeNumberToUser(Common.PrivateNumber privateNumber, string keyword)
		{
			return ExecuteSPCommand("AssignSubRangeNumberToUser",
															"@Number", privateNumber.Number,
															"@Range", privateNumber.Range,
															"@Regex", privateNumber.Regex,
															"@ParentGuid", privateNumber.ParentGuid,
															"@OwnerGuid", privateNumber.OwnerGuid,
															"@Keyword", keyword,
															"@Price", privateNumber.Price,
															"@ExpireDate", privateNumber.ExpireDate);
		}

		public DataTable GetAllRanges(Guid userGuid)
		{
			return FetchSPDataTable("GetAllRanges", "@UserGuid", userGuid);
		}

		public DataTable GetSubRanges(Guid parentGuid)
		{
			return FetchDataTable("SELECT [Range],[Regex],[Number] FROM [PrivateNumbers] WHERE [ParentGuid] = @ParentGuid AND [IsDeleted] = 0", "@ParentGuid", parentGuid);
		}

		public bool IsDuplicateKeyword(Guid numberGuid, string keyword)
		{
			DataTable dtNumbers = FetchDataTable("SELECT * FROM [dbo].[ReceiveKeywords] WHERE [PrivateNumberGuid] = @NumberGuid AND [Keyword] = @Keyword",
																					 "@NumberGuid", numberGuid,
																					 "@Keyword", keyword);
			return dtNumbers.Rows.Count > 0 ? true : false;
		}

		public DataTable GetUserPrivateNumbersForSend(Guid userGuid)
		{
			return FetchSPDataTable("GetUserPrivateNumbersForSend", "@UserGuid", userGuid);
		}

		public object GetUserPrivateNumbersForSendBulk(Guid userGuid)
		{
			return FetchSPDataTable("GetUserPrivateNumbersForSendBulk", "@UserGuid", userGuid);
		}

		public DataTable GetAgentInfo(Guid numberGuid)
		{
			return FetchSPDataTable("GetAgentInfo", "@NumberGuid", numberGuid);
		}

		public DataTable GetUserPrivateNumbersForReceive(Guid userGuid)
		{
			return FetchSPDataTable("GetUserPrivateNumbersForReceive", "@UserGuid", userGuid);
		}

		public bool SetPublicNumber(Guid numberGuid)
		{
			return ExecuteSPCommand("SetPublicNumber", "@Guid", numberGuid);
		}

		public DataTable GetPagedAssignedLines(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet numbersInfo = base.FetchSPDataSet("GetPagedAssignedLines",
																								"@UserGuid", userGuid,
																								"@Query", query,
																								"@PageNo", pageNo,
																								"@PageSize", pageSize,
																								"@SortField", sortField);

			resultCount = Helper.GetInt(numbersInfo.Tables[0].Rows[0]["RowCount"]);

			return numbersInfo.Tables[1];
		}

		public DataTable GetPagedAllAssignedLine(string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet numbersInfo = base.FetchSPDataSet("GetPagedAllAssignedLines",
																								"@Query", query,
																								"@PageNo", pageNo,
																								"@PageSize", pageSize,
																								"@SortField", sortField);

			resultCount = Helper.GetInt(numbersInfo.Tables[0].Rows[0]["RowCount"]);

			return numbersInfo.Tables[1];
		}

		public bool DeleteNumber(Guid guid)
		{
			return ExecuteSPCommand("DeleteNumber", "@Guid", guid);
		}

		public Guid GetUserNumberGuid(string number, Guid userGuid)
		{
			return GetSPGuidFieldValue("GetUserNumberGuid",
																 "@Number", number,
																 "@UserGuid", userGuid);
		}

		public DataTable GetVASNumber(string serviceId)
		{
			return FetchSPDataTable("GetVASNumber", "@ServiceId", serviceId);
		}

		public string GetServiceId(TypeServiceId typeServiceId, string serviceId)
		{
			return GetStringFieldValue("GetServiceId",
																 "@TypeServiceId", (int)typeServiceId,
																 "@ServiceId", serviceId);
		}

		public DataTable GetPagedAssignedKeywords(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet numbersInfo = base.FetchSPDataSet("GetPagedAssignedKeywords",
																								"@UserGuid", userGuid,
																								"@Query", query,
																								"@PageNo", pageNo,
																								"@PageSize", pageSize,
																								"@SortField", sortField);

			resultCount = Helper.GetInt(numbersInfo.Tables[0].Rows[0]["RowCount"]);

			return numbersInfo.Tables[1];
		}

		public bool DeleteKeyword(Guid guid)
		{
			return ExecuteSPCommand("DeleteKeyword", "@Guid", guid);
		}
	}
}
