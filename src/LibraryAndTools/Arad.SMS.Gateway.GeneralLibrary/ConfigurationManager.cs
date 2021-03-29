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
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Arad.SMS.Gateway.GeneralLibrary
{
	public class ConfigurationManager
	{
		public static string ApplicationPhysicalPath
		{
			get
			{
				string applicationPhysicalPath = string.Empty;
				if (ConfigCache != null)
					applicationPhysicalPath = GetSetting("ApplicationPhysicalPath");

				if (string.IsNullOrEmpty(applicationPhysicalPath) && System.Web.HttpContext.Current != null)
					applicationPhysicalPath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath;

				if (string.IsNullOrEmpty(applicationPhysicalPath))
				{
					applicationPhysicalPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(ConfigurationManager)).CodeBase);
					applicationPhysicalPath = applicationPhysicalPath.StartsWith("filC:\\") ? applicationPhysicalPath.Substring(6) : applicationPhysicalPath;
				}

				if (!string.IsNullOrEmpty(applicationPhysicalPath))
				{
					applicationPhysicalPath = applicationPhysicalPath.TrimEnd(System.IO.Path.DirectorySeparatorChar) + System.IO.Path.DirectorySeparatorChar;
					SetSetting("ApplicationPhysicalPath", applicationPhysicalPath);
				}

				return applicationPhysicalPath;
			}
		}

		public static string ApplicationTempFolder
		{
			get
			{
				return GetSetting("ApplicationTempFolder");
			}
		}

		public static string DefaultLoginPageControlID
		{
			get
			{
				string defaultLoginPageControlID = GetSetting("DefaultLoginPageControlID");

				if (string.IsNullOrEmpty(defaultLoginPageControlID))
					throw new Exception("Default login page controlID not defined");

				return defaultLoginPageControlID;
			}
		}

		public static string EditProfilePageID
		{
			get
			{
				string editProfilePageID = GetSetting("EditProfilePageID");

				if (string.IsNullOrEmpty(editProfilePageID))
					throw new Exception("Profile page controlID not defined");

				return editProfilePageID;
			}
		}

		public static string DataGridAvailablePageSizes
		{
			get
			{
				return GetSetting("DataGridAvailablePageSizes");
			}
		}

		public static string CheckLoginExceptions
		{
			get
			{
				return GetSetting("CheckLoginExceptions");
			}
		}

		public static string DefaultDataGridPageSizes
		{
			get
			{
				return GetSetting("DefaultDataGridPageSizes");
			}
		}

		#region--------- Config Cache Method ------------------
		private static Dictionary<string, string> ConfigCache;

		public static string GetSetting(string key)
		{
			if (ConfigCache == null)
				InitializeConfiguration();

			key = key.ToLower();

			if (ConfigCache.ContainsKey(key))
				return ConfigCache[key];
			else
				return string.Empty;
		}

		public static Dictionary<string, string> GetSettingWithSpecificPrefix(string startKeyWith)
		{
			if (ConfigCache == null)
				InitializeConfiguration();

			startKeyWith = startKeyWith.ToLower();
			Dictionary<string, string> dictionaryValues = new Dictionary<string, string>();

			List<string> lstKeys = ConfigCache.Keys.Where(k => k.StartsWith(startKeyWith)).ToList();

			foreach (string key in lstKeys)
				dictionaryValues.Add(key, GetSetting(key));

			return dictionaryValues;
		}

		public static string GetAttributeValue(string key, string attr)
		{
			XDocument xDocument = XDocument.Load(ConfigurationManager.ApplicationPhysicalPath + "Configurations\\Configortion.config");

			XElement configs = xDocument.Element("configuration").Element("appSettings");
			XElement selectedKey = configs.Elements("add").Where(k => k.Attribute("key").Value.ToLower() == key.ToLower()).FirstOrDefault();

			return selectedKey.Attribute(attr).Value;
		}

		private static void SetSetting(string key, string value)
		{
			if (ConfigCache == null)
				InitializeConfiguration();

			key = key.ToLower();

			if (!ConfigCache.ContainsKey(key))
				ConfigCache.Add(key, value);
			else
				ConfigCache[key] = value;
		}

		private static void ReadConfigurationFile()
		{
			XDocument xDocument = XDocument.Load(ConfigurationManager.ApplicationPhysicalPath + "Configurations\\Configortion.config");

			XElement configs = xDocument.Element("configuration").Element("appSettings");

			foreach (XElement config in configs.Elements("add"))
				SetSetting(config.Attribute("key").Value.ToLower(), config.Attribute("value").Value);
		}

		private static void InitializeConfiguration()
		{
			ConfigCache = new Dictionary<string, string>();
			ReadConfigurationFile();
		}
		#endregion
	}
}
