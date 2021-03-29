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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web
{
	public partial class RecieveSms : System.Web.UI.Page
	{
		Common.Inbox inbox = new Common.Inbox();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				inbox.InboxGroupGuid = Guid.Empty;
				inbox.Sender = Request.QueryString["From"];
				inbox.Receiver = Request.QueryString["To"];
				inbox.SmsText = Request.QueryString["Text"];
				inbox.PrivateNumberGuid = Guid.Empty;
				inbox.UserGuid = Guid.Empty;
				inbox.ShowAlert = false;
				inbox.IsRead = false;
				inbox.ReceiveDateTime = DateTime.Now;

				Facade.Inbox.InsertSms(inbox);
			}
		}
	}
}