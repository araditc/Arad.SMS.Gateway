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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Collections.Generic;

namespace Arad.SMS.Gateway.Facade
{
	public class Domain : FacadeEntityBase
	{
		public static bool InsertDomain(Common.Domain domain)
		{
			Business.Domain domainController = new Business.Domain();
			return domainController.InsertDomain(domain);
		}

		public static DataTable GetPagedDomains(Guid userGuid, string query, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.Domain domainController = new Business.Domain();
			return domainController.GetPagedDomains(userGuid, query, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool UpdateName(Common.Domain domain)
		{
			Business.Domain domainController = new Business.Domain();
			return domainController.UpdateName(domain);
		}

		public static bool Delete(Guid guidDomain)
		{
			Business.Domain domainController = new Business.Domain();
			return domainController.Delete(guidDomain);
		}

		public static Guid GetDomainGuid(string domainName)
		{
			Business.Domain domainController = new Business.Domain();
			if (domainName.StartsWith("www."))
				domainName = domainName.Remove(0, 4);
			return domainController.GetDomainGuid(domainName);
		}

		public static Guid GetGuidAdminOfDomain(string domainName)
		{
			Business.Domain domainController = new Business.Domain();
			return domainController.GetGuidAdminOfDomain(domainName);
		}

		public static Common.Domain Load(Guid guid)
		{
			Business.Domain domainController = new Business.Domain();
			Common.Domain domain = new Common.Domain();
			domainController.Load(guid, domain);
			return domain;
		}

		public static DataTable GetDomainInfo(string domainName)
		{
			Business.Domain domainController = new Business.Domain();
			return domainController.GetDomainInfo(domainName);
		}

		public static void GetDomainInfo(string domainName, out Business.Desktop desktop, out Business.DefaultPages defaultPage, out Business.Theme theme)
		{
			Business.Domain domainController = new Business.Domain();
			domainController.GetDomainInfo(domainName, out desktop, out defaultPage, out theme);
		}

		public static DataTable GetDomainSlideShow(string domainName)
		{
			Business.Domain domainController = new Business.Domain();
			return domainController.GetDomainSlideShow(domainName);
		}

		public static DataTable GetDomain(Guid domainGuid)
		{
			Business.Domain domainController = new Business.Domain();
			return domainController.GetDomain(domainGuid);
		}

		public static string GetDomainName(Guid domainGuid)
		{
			Business.Domain domainController = new Business.Domain();
			DataTable dtDomainInfo = domainController.GetDomain(domainGuid);
			return dtDomainInfo.Rows.Count > 0 ? dtDomainInfo.Rows[0]["Name"].ToString() : string.Empty;
		}

		public static DataTable GetSalePackages(string domainName, Dictionary<Guid, List<string>> dictionaryServices, string lang)
		{
			try
			{
				Business.Domain domainController = new Business.Domain();
				return domainController.GetSalePackages(domainName, dictionaryServices, lang);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetContent(string domainName, Business.DataLocation location, Business.Desktop desktop)
		{
			try
			{
				Business.Domain domainController = new Business.Domain();
				return domainController.GetContent(domainName, location, desktop);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static Guid GetUserDomain(Guid userGuid)
		{
			try
			{
				Business.Domain domainController = new Business.Domain();
				return domainController.GetUserDomain(userGuid);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetAgentChildrenDomains(string userGuid)
		{
			Business.Domain domainController = new Business.Domain();
			return domainController.GetAgentChildrenDomains(userGuid);
		}
	}
}
