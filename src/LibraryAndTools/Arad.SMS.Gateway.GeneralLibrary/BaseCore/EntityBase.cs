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

using DatabaseInfo;

namespace Arad.SMS.Gateway.GeneralLibrary.BaseCore
{
	public abstract class EntityBase
	{
		private string tableName = string.Empty;
		private string viewName = string.Empty;

		public string TableName
		{
			get
			{
				return tableName;
			}
			private set
			{
				tableName = DatabaseInfoProvider.DBInfo.DBPrefix + value;
				viewName = DatabaseInfoProvider.DBInfo.DBPrefix + "vw" + value;
			}
		}

		public string FullTableName
		{
			get
			{
				return DatabaseInfoProvider.DBInfo.DataSource + ".dbo." + tableName;
			}
		}

		public string ViewName
		{
			get
			{
				return viewName;
			}
			private set
			{
				viewName = value;
			}
		}

		public string FullViewName
		{
			get
			{
				return DatabaseInfoProvider.DBInfo.DataSource + ".dbo." + viewName;
			}
		}

		public EntityBase(string tableName)
		{
			this.TableName = tableName;
		}

		internal EntityBase()
		{
			this.TableName = string.Empty;
		}

		public string GetSPName(string action)
		{
			return this.TableName + "_" + action;
		}
	}
}
