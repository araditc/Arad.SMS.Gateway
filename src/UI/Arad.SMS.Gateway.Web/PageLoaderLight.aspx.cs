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
using System.Web.UI;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.Web
{
	public partial class PageLoaderLight : System.Web.UI.Page
	{
		protected string InlineScript = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			Business.UserControls userControl = Business.UserControls.UI_Home;

			if (Request.Params["CallBack"] != null)
			{
				if (Request.Params["CallBack"] == "loadLogin")
				{
					Session.Remove("UserName");
					Session.Remove("UserGuid");
				}
			}

			if (Request.Params["c"] != null)
			{
				int controlID = Helper.RequestEncryptedInt(this, "c");

				try
				{
					if (controlID == 0)
						throw new Exception();

					userControl = (Business.UserControls)controlID;
				}
				catch
				{
					if (Session["SessionExpired"] == null)
					{
						Session["SessionExpired"] = true;
						InlineScript = "relaodMainPage();";
					}
				}
			}
			else
				InlineScript = "relaodMainPage();";

			if (InlineScript == string.Empty)
			{
				Control control = LoadControl(userControl);
				mainPanel.Controls.Add(control);
			}
		}

		private Control LoadControl(Business.UserControls userControl)
		{
			Page.Title = Language.GetString(userControl.ToString());
			string path = "~/" + userControl.ToString().Replace("_", "/") + ".ascx";
			return LoadControl(path);
		}
	}
}
