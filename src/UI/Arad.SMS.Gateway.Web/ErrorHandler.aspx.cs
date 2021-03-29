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

namespace Arad.SMS.Gateway.Web
{
	public partial class ErrorHandler : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ltrError.Text = string.Empty;

			ErrorType errorType = (ErrorType)Helper.RequestInt(this, "ErrorType");

			switch (errorType)
			{
				case ErrorType.OneServiceError:
					int service = Helper.RequestInt(this, "Service");
					ltrError.Text = string.Format("<h2>{0}</h2><h4>{1}</h4>",
																	Language.GetString("DearUser"), 
																	Language.GetString("YouDontHavePermissionToAccsessFollowService"));
					break;
	
				case ErrorType.SeveralServiceError:
					break;

				case ErrorType.NotEnableJavaScript:
						ltrError.Text = string.Format("<h2>{0}</h2><h4>{1}</h4><br/>{2}",
																	Language.GetString("DearUser"),
																	Language.GetString("JavaScriptInYourBrowserIsDisableOrYourBrowserDontSupportJavaScript"),
																	Language.GetString("ToUseSitePleaseActiveJavaScriptOrUseAnOtherBrowser")
																	);
					break;
			}
		}
	}
}