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
	public partial class Default : System.Web.UI.Page
	{
		public string DomainName
		{
			get { return Helper.GetDomain(Request.Url.Authority); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			try
			{
				int dataID = Helper.RequestInt(this, "cid");
				var content = Facade.Data.GetData(dataID);

				if (content.Rows.Count == 0)
					return;

				ltrTitle.Text = content.Rows[0]["Title"].ToString();
				ltrBody.Text = content.Rows[0]["Content"].ToString();
			}
			catch { }
		}

		//[WebMethod]
		//[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		//public static object[] GetContent(string loc)
		//{
		//	try
		//	{
		//		string domainName = Helper.GetDomain(HttpContext.Current.Request.Url.Authority);
		//		Business.DataLocation location = (Business.DataLocation)Helper.GetInt(loc);
		//		var lstData = Facade.Domain.GetContent(domainName, location, Business.Desktop.Default);

		//		object[] array = new object[lstData.Rows.Count];
		//		int counter = 0;
		//		foreach (DataRow row in lstData.Rows)
		//		{
		//			array[counter] = new
		//			{
		//				title = row["Title"].ToString(),
		//				id = row["ID"],
		//				summary = row["Summary"].ToString(),
		//				hascontent = string.IsNullOrEmpty(row["Content"].ToString()) ? 0 : 1
		//			};
		//			counter++;
		//		}
		//		return array;
		//	}
		//	catch
		//	{
		//		return new object[1];
		//	}
		//}
	}
}