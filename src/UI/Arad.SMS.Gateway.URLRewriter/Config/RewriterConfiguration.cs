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
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Arad.SMS.Gateway.URLRewriter.Config
{
	[Serializable()]
	[XmlRoot("RewriterConfig")]
	public class RewriterConfiguration
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

		private RewriterRuleCollection rules;

		public RewriterRuleCollection Rules
		{
			get
			{
				return rules;
			}
			set
			{
				rules = value;
			}
		}

		public static RewriterConfiguration GetConfig(string domain)
		{
			try
			{
				if (ConfigCache == null)
					InitializeConfiguration();

				string theme = GetSetting(domain);

				//if (HttpContext.Current.Cache["RewriterConfig"] == null)
				HttpContext.Current.Cache.Insert("RewriterConfig", WebConfigurationManager.GetSection("RewriterConfig", @"~/HomePages/" + theme + "/Web.config"));

				return (RewriterConfiguration)HttpContext.Current.Cache["RewriterConfig"];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static string GetDefaultRule(string domain)
		{
			if (ConfigCache == null)
				InitializeConfiguration();

			string theme = GetSetting(domain);
			string defaultlookForUrl = string.Empty;

			if (HttpContext.Current.Cache["RewriterConfig"] == null)
				HttpContext.Current.Cache.Insert("RewriterConfig", WebConfigurationManager.GetSection("RewriterConfig", @"~/HomePages/" + theme + "/Web.config"));

			RewriterRuleCollection rules = ((RewriterConfiguration)HttpContext.Current.Cache["RewriterConfig"]).Rules;

			for (int counterRule = 0; counterRule < rules.Count; counterRule++)
			{
				if (rules[counterRule].IsDefault)
				{
					defaultlookForUrl = rules[counterRule].LookFor;
					break;
				}
			}

			return defaultlookForUrl;
		}

		#region--------- Config Cache Method ------------------
		public static Dictionary<string, string> ConfigCache;

		public static string GetSetting(string key)
		{
			if (ConfigCache == null)
				InitializeConfiguration();

			key = key.ToLower();

			if (ConfigCache.ContainsKey(key))
				return ConfigCache[key];
			else if (ConfigCache.ContainsKey("other"))
				return ConfigCache["other"];
			else
				return string.Empty;
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
			XDocument xDocument = XDocument.Load(ApplicationPhysicalPath + "Web.config");

			XElement configs = xDocument.Element("configuration").Element("DomainConfig");

			foreach (XElement config in configs.Elements("domain"))
				SetSetting(config.Attribute("url").Value.ToLower(), config.Attribute("theme").Value);
		}

		public static void InitializeConfiguration()
		{
			ConfigCache = new Dictionary<string, string>();
			ReadConfigurationFile();
		}
		#endregion
	}
}
