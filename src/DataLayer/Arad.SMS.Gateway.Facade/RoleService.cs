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
using System.Data;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Xml.Linq;

namespace Arad.SMS.Gateway.Facade
{
	public class RoleService : FacadeEntityBase
	{
		public static DataTable GetServiceOfRole(Guid userGuid, Guid roleGuid)
		{
			Business.RoleService roleServiceController = new Business.RoleService();
			return roleServiceController.GetServiceOfRole(userGuid, roleGuid);
		}

		public static DataSet GetRoleServices(Guid userGuid)
		{
			Business.RoleService roleServiceController = new Business.RoleService();
			return roleServiceController.GetRoleServices(userGuid);
		}

		public static bool InsertRoleServices(Dictionary<Guid, decimal> dictionaryServicePrice, List<Guid> lstSelectedRow, Guid roleGuid)
		{
			Business.RoleService roleServiceController = new Business.RoleService();
			Common.RoleService roleService = new Common.RoleService();
			roleServiceController.BeginTransaction();
			try
			{
				roleService.RoleGuid = roleGuid;
				if (dictionaryServicePrice.Count > 0 || lstSelectedRow.Count > 0)
				{
					if (!roleServiceController.DeleteServicesOfRole(roleGuid))
						throw new Exception(Language.GetString("ErrorRecord"));

					XDocument doc = new XDocument();
					XElement root = new XElement("NewDataSet");

					foreach (KeyValuePair<Guid, decimal> service in dictionaryServicePrice)
					{
						if (service.Value < 0) continue;

						XElement element = new XElement("Table");
						element.Add(new XElement("ServiceGuid", service.Key));
						element.Add(new XElement("Price", service.Value));
						element.Add(new XElement("IsDefault", lstSelectedRow.Contains(service.Key) ? true : false));
						root.Add(element);

						if (lstSelectedRow.Contains(service.Key))
							lstSelectedRow.Remove(service.Key);
					}

					foreach (Guid defaultService in lstSelectedRow)
					{
						XElement element = new XElement("Table");
						element.Add(new XElement("ServiceGuid", defaultService));
						element.Add(new XElement("Price", 0));
						element.Add(new XElement("IsDefault", true));
						root.Add(element);
					}

					doc.Add(root);

					if (!roleServiceController.InsertService(doc.ToString(), roleGuid))
						throw new Exception(Language.GetString("ErrorRecord"));
				}
				roleServiceController.CommitTransaction();
				return true;
			}
			catch (Exception ex)
			{
				roleServiceController.RollbackTransaction();
				throw ex;
			}
		}
	}
}
