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
using Arad.SMS.Gateway.GeneralLibrary;

namespace DatabaseInfo
{
	public class DatabaseInfoProvider
	{
		private static DatabaseInfoProvider dbInfo;

		public static DatabaseInfoProvider DBInfo
		{
			get
			{
				if (dbInfo == null)
					dbInfo = new DatabaseInfoProvider();

				return dbInfo;
			}
		}

		public string DataSource
		{
			get { return ConfigurationManager.GetSetting("DataSource"); }
		}

		public string InitialCatalog
		{
			get { return ConfigurationManager.GetSetting("InitialCatalog"); }
		}

		public string UserID
		{
			get { return ConfigurationManager.GetSetting("UserID"); }
		}

		public string Password
		{
			get { return ConfigurationManager.GetSetting("Password"); }
		}

		public int ConnectTimeout
		{
			get { return Helper.GetInt(ConfigurationManager.GetSetting("ConnectTimeout")); }
		}

		public string DBPrefix
		{
			get { return ConfigurationManager.GetSetting("DBPrefix"); }
		}

		public int QueryTimeout
		{
			get { return Helper.GetInt(ConfigurationManager.GetSetting("QueryTimeout")); }
		}

		public bool MultipleActiveResultSets
		{
			get { return Helper.GetBool(ConfigurationManager.GetSetting("MultipleActiveResultSets")); }
		}

		public string ConnectionString
		{
			get
			{
				if (string.IsNullOrEmpty(UserID) || string.IsNullOrEmpty(Password))
				{
					return string.Format("Data Source={0}; Initial Catalog={1}; Integrated Security=SSPI; Connect Timeout={2}; MultipleActiveResultSets={3};", DataSource, InitialCatalog, ConnectTimeout, MultipleActiveResultSets);
				}
				else
				{
					return string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}; Connect Timeout={4}; MultipleActiveResultSets={5};", DataSource, InitialCatalog, UserID, Password, ConnectTimeout, MultipleActiveResultSets);
				}
			}
		}

		public override string ToString()
		{
			System.Text.StringBuilder ret = new System.Text.StringBuilder();
			ret.Append("Database Info:" + Environment.NewLine);
			ret.Append("\tDataSource:     " + this.DataSource + Environment.NewLine);
			ret.Append("\tInitialCatalog: " + this.InitialCatalog + Environment.NewLine);
			ret.Append("\tUserID:         " + this.UserID + Environment.NewLine);
			ret.Append("\tPassword:       " + this.Password + Environment.NewLine);
			ret.Append("\tConnectTimeout: " + this.ConnectTimeout + Environment.NewLine);
			ret.Append("\tDBPrefix:       " + this.DBPrefix + Environment.NewLine);
			ret.Append("\tQueryTimeout:   " + this.QueryTimeout + Environment.NewLine);

			return ret.ToString();
		}
	}
}
