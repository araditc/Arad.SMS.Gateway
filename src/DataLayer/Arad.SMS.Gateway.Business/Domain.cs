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
using System.Collections.Generic;
using System.Linq;

namespace Arad.SMS.Gateway.Business
{
	public class Domain : BusinessEntityBase
	{
		private static Dictionary<string, Tuple<Desktop, DefaultPages, Theme>> domainInfoCache;

		public Domain(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Domains.ToString(), dataAccessProvider)
		{
			this.OnEntityChange += new EntityChangeEventHandler(OnDomainChange);
		}

		#region EventHandler
		private void OnDomainChange(object sender, EntityChangeEventArgs e)
		{
			if (sender is Common.Domain)
				domainInfoCache.Remove(((Common.Domain)sender).Name);
			else if (e != null && e.ActionType == EntityChangeActionTtype.Delete)
				domainInfoCache.Remove(this.GetDomainName(Helper.GetGuid(sender)));
		}
		#endregion

		public DataTable GetPagedDomains(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetDomains = base.FetchSPDataSet("GetPagedDomains",
																									 "@UserGuid", userGuid,
																									 "@Query", query,
																									 "@PageNo", pageNo,
																									 "@PageSize", pageSize,
																									 "@SortField", sortField);

			resultCount = Helper.GetInt(dataSetDomains.Tables[0].Rows[0]["RowCount"]);

			return dataSetDomains.Tables[1];
		}

		public bool UpdateName(Common.Domain domain)
		{
			try
			{
				if (FetchDataTable("SELECT * FROM [Domains] WHERE [IsDeleted]= 0 AND [Name] = @Name AND [Guid] != @DomainGuid", "@Name", domain.Name, "@DomainGuid", domain.DomainGuid).Rows.Count > 1)
					throw new Exception(Language.GetString("DuplicateDomainName"));

				if (!ExecuteSPCommand("UpdateDomain",
															"@Guid", domain.DomainGuid,
															"@Name", domain.Name,
															"@Desktop", domain.Desktop,
															"@DefaultPage", domain.DefaultPage,
															"@Theme", domain.Theme,
															"@UserGuid", domain.UserGuid))
					throw new Exception(Language.GetString("ErrorRecord"));

				OnDomainChange(domain, null);

				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool InsertDomain(Common.Domain domain)
		{
			try
			{
				if (GetCountDomainName(domain.Name) > 0)
					throw new Exception(Language.GetString("DuplicateDomainName"));

				if (Insert(domain) == Guid.Empty)
					throw new Exception(Language.GetString("ErrorRecord"));

				OnDomainChange(domain, null);

				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public int GetCountDomainName(string domainName)
		{
			DataTable dataTabledomain = base.FetchSPDataTable("CheckValidDomainName", "@Name", domainName);
			return dataTabledomain.Rows.Count;
		}

		public Guid GetDomainGuid(string domainName)
		{
			DataTable dataTableDomain = base.FetchSPDataTable("GetDomainGuid", "@Name", domainName);
			if (dataTableDomain.Rows.Count > 0)
				return Helper.GetGuid(dataTableDomain.Rows[0]["Guid"]);
			else
				return Guid.Empty;
		}

		public string GetDomainName(Guid domainGuid)
		{
			return base.GetSPFieldValue("GetDomainName", "@DomainGuid", domainGuid).ToString();
		}

		public Guid GetGuidAdminOfDomain(string domainName)
		{
			DataTable dataTableDomain = base.FetchSPDataTable("GetGuidAdminOfDomain", "@Name", domainName);
			if (dataTableDomain.Rows.Count > 0)
				return Helper.GetGuid(dataTableDomain.Rows[0]["UserGuid"]);
			else
				return Guid.Empty;
		}

		public DataTable GetDomainInfo(string domainName)
		{
			return base.FetchSPDataTable("GetDomainInfo", "@DomainName", domainName);
		}

		public void GetDomainInfo(string domainName, out Desktop desktop, out DefaultPages defaultPage, out Theme theme)
		{
			if (domainInfoCache == null)
				domainInfoCache = new Dictionary<string, Tuple<Desktop, DefaultPages, Theme>>();

			if (domainInfoCache.ContainsKey(domainName))
			{
				desktop = domainInfoCache[domainName].Item1;
				defaultPage = domainInfoCache[domainName].Item2;
				theme = domainInfoCache[domainName].Item3;
			}
			else
			{
				DataTable domainInfo = GetDomainInfo(domainName);

				if (domainInfo.Rows.Count > 0)
				{
					desktop = (Business.Desktop)Helper.GetInt(domainInfo.Rows[0]["Desktop"]);
					defaultPage = (Business.DefaultPages)Helper.GetInt(domainInfo.Rows[0]["DefaultPage"]);
					theme = (Business.Theme)Helper.GetInt(domainInfo.Rows[0]["Theme"]);
				}
				else
				{
					desktop = Business.Desktop.MainDesktop;
					defaultPage = DefaultPages.Default;
					theme = Business.Theme.Blitzer;
				}

				domainInfoCache.Add(domainName, new Tuple<Desktop, DefaultPages, Theme>(desktop, defaultPage, theme));
			}
		}

		public DataTable GetAgentChildrenDomains(string userGuid)
		{
			return FetchSPDataTable("GetAgentChildrenDomains", "@UserGuid", userGuid);
		}

		public DataTable GetDomainSlideShow(string domainName)
		{
			return FetchSPDataTable("GetDomainSlideShow", "@DomainName", domainName);
		}

		public DataTable GetDomain(Guid domainGuid)
		{
			return FetchSPDataTable("GetDomain", "@Guid", domainGuid);
		}

		public DataTable GetSalePackages(string domainName, Dictionary<Guid, List<string>> dictionaryServices, string lang)
		{
			try
			{
				DataSet dataSetPackages = FetchSPDataSet("GetSalePackages", "@DomainName", domainName);

				var lstServices = dataSetPackages.Tables[1];

				foreach (DataRow row in lstServices.Rows)
				{
					Guid roleGuid = Helper.GetGuid(row["RoleGuid"]);

					if (!dictionaryServices.ContainsKey(roleGuid))
					{
                        if(lang=="fa")
                        {
                            var lstRoleServices = lstServices.Rows.OfType<DataRow>()
                                            .Where(o => o.Field<Guid>("RoleGuid") == roleGuid)
                                            .Select(o => o.Field<string>("TitleFa"))
                                            .ToList();
                            dictionaryServices.Add(roleGuid, lstRoleServices);
                        }else
                        {
                            var lstRoleServices = lstServices.Rows.OfType<DataRow>()
                                            .Where(o => o.Field<Guid>("RoleGuid") == roleGuid)
                                            .Select(o => o.Field<string>("Title"))
                                            .ToList();
                            dictionaryServices.Add(roleGuid, lstRoleServices);
                        }
					}
				}

				return dataSetPackages.Tables[0];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataTable GetContent(string domainName, DataLocation location, Desktop desktop)
		{
			try
			{
				return FetchSPDataTable("GetContent",
																"@DomainName", domainName,
																"@Location", (int)location,
																"@Desktop", (int)desktop);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public Guid GetUserDomain(Guid userGuid)
		{
			try
			{
				var domains = FetchDataTable("SELECT * FROM [Domains] WHERE [UserGuid] = @UserGuid AND [IsDeleted] = 0", "@UserGuid", userGuid);
				return domains.Rows.Count > 0 ? Helper.GetGuid(domains.Rows[0]["Guid"]) : Guid.Empty;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
