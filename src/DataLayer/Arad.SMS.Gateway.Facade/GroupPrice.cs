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
using System.Text;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Arad.SMS.Gateway.Facade
{
	public class GroupPrice : FacadeEntityBase
	{
		public static bool Insert(Common.GroupPrice groupPrice, string agentRatio)
		{
			Business.GroupPrice groupPriceController = new Business.GroupPrice();
			try
			{
				XDocument doc = new XDocument();
				XElement root = new XElement("NewDataSet");
				int countOperator = Helper.GetInt(Helper.ImportData(agentRatio, "resultCount"));

				for (int counterOpearor = 0; counterOpearor < countOperator; counterOpearor++)
				{
					XElement element = new XElement("Table");
					element.Add(new XElement("AgentID", Helper.ImportData(agentRatio, ("AgentID" + counterOpearor))));
					element.Add(new XElement("Ratio", Helper.ImportDecimalData(agentRatio, ("NewRatio" + counterOpearor))));
					root.Add(element);
				}
				doc.Add(root);

				groupPrice.AgentRatio = doc.ToString();
				return groupPriceController.Insert(groupPrice) != Guid.Empty ? true : false;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static DataTable GetPagedGroupPrices(Guid userGuid)
		{
			Business.GroupPrice groupPriceController = new Business.GroupPrice();
			return groupPriceController.GetPagedGroupPrices(userGuid);
		}

		public static bool UpdateGroupPrice(Common.GroupPrice groupPrice, string agentRatio,bool isMainAdmin)
		{
			Business.GroupPrice groupPriceController = new Business.GroupPrice();
			decimal currentRatio;
			decimal newRatio;
			try
			{
				XDocument doc = new XDocument();
				XElement element;
				XElement root = new XElement("NewDataSet");
				int countOperator = Helper.GetInt(Helper.ImportData(agentRatio, "resultCount"));

				for (int counter = 0; counter < countOperator; counter++)
				{
					element = new XElement("Table");
					element.Add(new XElement("AgentID", Helper.ImportData(agentRatio, ("AgentID" + counter))));
					currentRatio = Helper.ImportDecimalData(agentRatio, ("CurrentRatio" + counter));
					newRatio = Helper.ImportDecimalData(agentRatio, ("NewRatio" + counter));
					if (newRatio < currentRatio && !isMainAdmin)
						throw new Exception(Language.GetString("RatioNotValid"));

					element.Add(new XElement("Ratio", Helper.ImportDecimalData(agentRatio, ("NewRatio" + counter))));
					root.Add(element);
				}
				doc.Add(root);

				groupPrice.AgentRatio = doc.ToString();
				return groupPriceController.UpdateGroupPrice(groupPrice);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool DeleteGroupPrice(Guid groupPriceGuid)
		{
			Business.GroupPrice groupPriceController = new Business.GroupPrice();
			return groupPriceController.Delete(groupPriceGuid);
		}

		public static Common.GroupPrice LoadGroupPrice(Guid groupPriceGuid)
		{
			Business.GroupPrice groupPriceController = new Business.GroupPrice();
			Common.GroupPrice groupPrice = new Common.GroupPrice();
			groupPriceController.Load(groupPriceGuid, groupPrice);
			return groupPrice;
		}

		public static Guid GetDefaultGroupPrice(string domain)
		{
			Business.GroupPrice groupPriceController = new Business.GroupPrice();
			return groupPriceController.GetDefaultGroupPrice(domain);
		}

		public static DataTable GetGroupPrices(Guid userGuid, Guid parentGuid)
		{
			Business.GroupPrice groupPriceController = new Business.GroupPrice();

			Common.User user = User.LoadUser(userGuid);
			parentGuid = parentGuid == Guid.Empty ? userGuid : parentGuid;
			if (!user.IsFixPriceGroup)
				return groupPriceController.GetGroupPrices(parentGuid);
			else
				return new DataTable();
		}

		public static decimal GetUserBaseSmsPrice(Guid userGuid, Guid parentGuid, long smsCount, ref bool decreaseTax)
		{
			Business.GroupPrice groupPriceController = new Business.GroupPrice();
			return groupPriceController.GetUserBaseSmsPrice(userGuid, parentGuid, smsCount, ref decreaseTax);
		}

		public static bool CheckExistRange(Guid userGuid, Guid groupPriceGuid, long minimumMessage, long maximumMessage)
		{
			Business.GroupPrice groupPriceController = new Business.GroupPrice();
			DataTable dtRanges = groupPriceController.CheckExistRange(userGuid);
			bool isValid = true;

			foreach (DataRow row in dtRanges.Rows)
			{
				if ((minimumMessage >= Helper.GetLong(row["MinimumMessage"]) && minimumMessage <= Helper.GetLong(row["MaximumMessage"])) ||
						(maximumMessage >= Helper.GetLong(row["MinimumMessage"]) && maximumMessage <= Helper.GetLong(row["MaximumMessage"])))
				{
					if (Helper.GetGuid(row["Guid"]) != groupPriceGuid)
						isValid = false;
					break;
				}
			}

			return isValid;
		}

		public static Dictionary<Guid, decimal> GetAgentRatio(Guid UserGuid)
		{
			Dictionary<Guid, decimal> agentRatio = new Dictionary<Guid, decimal>();
			Common.User user = User.LoadUser(UserGuid);
			Common.GroupPrice groupPrice = GroupPrice.LoadGroupPrice(user.PriceGroupGuid);

			if (groupPrice.AgentRatio != string.Empty)
			{
				var xelement = XElement.Parse(groupPrice.AgentRatio);
				List<XElement> lstAgentElement = xelement.Elements("Table").ToList();

				foreach (var item in lstAgentElement)
					agentRatio.Add(Helper.GetGuid(item.Element("AgentID").Value), Helper.GetDecimal(item.Element("Ratio").Value));
			}
			return agentRatio;
		}

		public static Dictionary<Guid, decimal> GetAgentRatio(string agentRatio, bool isXml)
		{
			Dictionary<Guid, decimal> dicAgentRatio = new Dictionary<Guid, decimal>();
			decimal currentRatio;
			decimal newRatio;

			if (!isXml)
			{
				XDocument doc = new XDocument();
				XElement element;
				XElement root = new XElement("NewDataSet");
				int countOperator = Helper.GetInt(Helper.ImportData(agentRatio, "resultCount"));

				for (int counter = 0; counter < countOperator; counter++)
				{
					element = new XElement("Table");
					element.Add(new XElement("AgentID", Helper.ImportData(agentRatio, ("AgentID" + counter))));
					currentRatio = Helper.ImportDecimalData(agentRatio, ("CurrentRatio" + counter));
					newRatio= Helper.ImportDecimalData(agentRatio, ("NewRatio" + counter));
					if (newRatio < currentRatio)
						throw new Exception(Language.GetString("RatioNotValid"));

					element.Add(new XElement("Ratio", Helper.ImportDecimalData(agentRatio, ("NewRatio" + counter))));
					root.Add(element);
				}
				doc.Add(root);

				agentRatio = doc.ToString();
			}

			if (agentRatio != string.Empty)
			{
				var xelement = XElement.Parse(agentRatio);
				List<XElement> lstAgentElement = xelement.Elements("Table").ToList();

				foreach (var item in lstAgentElement)
					dicAgentRatio.Add(Helper.GetGuid(item.Element("AgentID").Value), Helper.GetDecimal(item.Element("Ratio").Value));
			}
			return dicAgentRatio;
		}

		public static void CompareAgentRatio(decimal basePrice, bool decreaseTax, string enterAgentRatio, Guid parentGuid)
		{
			try
			{
				decimal userRatio;
				Guid parentGroupGuid = User.GetGroupPriceGuid(parentGuid);
				Common.GroupPrice parentGroupPrice = LoadGroupPrice(parentGroupGuid);
				if (basePrice < parentGroupPrice.BasePrice)
					throw new Exception(Language.GetString("BaseGroupPriceIsLow"));

				Dictionary<Guid, decimal> parentAgentRatio = GetAgentRatio(parentGroupPrice.AgentRatio, true);
				Dictionary<Guid, decimal> userAgentRatio = GetAgentRatio(enterAgentRatio, false);

				foreach (KeyValuePair<Guid, decimal> parentRatio in parentAgentRatio)
				{
					userRatio = userAgentRatio.Keys.Contains(parentRatio.Key) ? userAgentRatio[parentRatio.Key] : 1;

					if (userRatio < Helper.GetDecimal(parentRatio.Value, 1))
						throw new Exception(Language.GetString("RatioNotValid"));
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
