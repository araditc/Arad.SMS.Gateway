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

namespace Arad.SMS.Gateway.Web.HomePages.Arad
{
	public partial class ForgotPassword : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            btnForgotPassword.Text = Language.GetString("SendwithSMS");
        }

		protected void btnForgotPassword_Click(object sender, EventArgs e)
		{
			try
			{
				Random rnd = new Random();
				bool isHuman = SampleCaptcha.Validate(CaptchaCodeTextBox.Text);
				CaptchaCodeTextBox.Text = null;

				if (!isHuman)
					throw new Exception(Language.GetString("IncorrectCaptcha"));

				string rawPassword = rnd.Next(10000, 1000000000).ToString();
				string newPassword = Helper.GetMd5Hash(rawPassword);
				string username = txtUsername.Text;

				if (!Facade.User.RetrievePassword(username, rawPassword, newPassword))
					throw new Exception(Language.GetString("ErrorRecord"));

				lblMessage.Attributes["style"] = "color:green";
				lblMessage.Text = Language.GetString("SendSmsSuccessful");
			}
			catch (Exception ex)
			{
				lblMessage.Text = ex.Message;
			}
		}
	}
}
