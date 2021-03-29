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

using Arad.SMS.Gateway.GeneralLibrary;
using Microsoft.Owin.Hosting;
using System;
using System.ServiceProcess;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.WebApi
{
	public partial class ApiService : ServiceBase
	{
		private IDisposable webApp;

		public ApiService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			DataAccessBase.GetActiveDataAccessProvider = DataAccessLayer.SqlDataAccess.GetSqlDataAccess;
			Language.ActiveLanguage = Language.AvalibaleLanguages.fa;
			string ipServer = string.Empty;
			ipServer = ConfigurationManager.GetSetting("IPServer");

			StartOptions options = new StartOptions();
			options.Urls.Add("http://localhost:8080");
			options.Urls.Add("http://127.0.0.1:8080");
			options.Urls.Add(string.Format("http://{0}:8080", Environment.MachineName));
			if (!string.IsNullOrEmpty(ipServer))
				options.Urls.Add(string.Format("http://{0}:8080", ipServer));
			webApp = WebApp.Start<Startup>(options);
		}

		protected override void OnStop()
		{
			webApp.Dispose();
		}
	}
}
