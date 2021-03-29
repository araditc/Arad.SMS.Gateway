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
using System;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace Arad.SMS.Gateway.Web
{
	public class Global : System.Web.HttpApplication
	{
        void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-Powered-By");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }
        void Application_PreRequestHandlerExecute(object sender, EventArgs e)
		{

			HttpApplication app = sender as HttpApplication;
			string acceptEncoding = app.Request.Headers["Accept-Encoding"];
			Stream prevUncompressedStream = app.Response.Filter;

			if (!(app.Context.CurrentHandler is Page) ||
			app.Request["HTTP_X_MICROSOFTAJAX"] != null)
				return;

			if ((acceptEncoding == null || acceptEncoding.Length == 0) && !app.Request.Browser.Browser.Contains("MSIE"))
				return;

			acceptEncoding = acceptEncoding.ToLower();

			if (acceptEncoding.Contains("gzip") || acceptEncoding == "*" || app.Request.Browser.Browser.Contains("MSIE"))
			{
				// gzip
				app.Response.Filter = new GZipStream(prevUncompressedStream,
				CompressionMode.Compress);
				app.Response.AppendHeader("Content-Encoding", "gzip");

			}
			else if (acceptEncoding.Contains("deflate"))
			{
				// defalte
				app.Response.Filter = new DeflateStream(prevUncompressedStream,
				CompressionMode.Compress);
				app.Response.AppendHeader("Content-Encoding", "deflate");
			}
		}

		protected void Application_Start(object sender, EventArgs e)
		{
			GeneralLibrary.BaseCore.DataAccessBase.GetActiveDataAccessProvider = DataAccessLayer.SqlDataAccess.GetSqlDataAccess;
            Language.ActiveLanguage = Language.AvalibaleLanguages.en;
        }

		protected void Session_Start(object sender, EventArgs e)
		{
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			if (GeneralLibrary.BaseCore.DataAccessBase.GetActiveDataAccessProvider == null)
				GeneralLibrary.BaseCore.DataAccessBase.GetActiveDataAccessProvider = DataAccessLayer.SqlDataAccess.GetSqlDataAccess;
            Session["Language"] = Language.AvalibaleLanguages.en;
            Session["encryptor"] = new CryptorEngine();
		}

		protected void Application_BeginRequest(object sender, EventArgs e) { }

		protected void Application_AuthenticateRequest(object sender, EventArgs e) { }

		protected void Application_Error(object sender, EventArgs e) { }

		protected void Session_End(object sender, EventArgs e) { }

		protected void Application_End(object sender, EventArgs e) { }
	}
}
