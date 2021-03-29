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
using System.Web.UI;
using System.Web;
using Arad.SMS.Gateway.GeneralLibrary;

namespace Arad.SMS.Gateway.GeneralLibrary.BaseCore
{
	public abstract class UIUserControlBase : System.Web.UI.UserControl
	{
		private string clientSideScript;
		private string closeModalResultValue;
		private string messageBoxText;
		private string messageBoxTitle;
		private string messageBoxType;

		#region DataGrid Handlers
		private Dictionary<string, Delegate> DataBinders;

		public void AddDataBinderHandlers(string controlName, Delegate dataBindHandler)
		{
			DataBinders.Add(controlName + "_Binder", dataBindHandler);
		}

		public Delegate GetDataBinder(string controlName)
		{
			if (DataBinders.ContainsKey(controlName + "_Binder"))
				return DataBinders[controlName + "_Binder"];
			else
				return null;
		}

		public void AddDataRenderHandlers(string controlName, Delegate dataRenderHandler)
		{
			DataBinders.Add(controlName + "_Render", dataRenderHandler);
		}

		public Delegate GetDataRenderHandler(string controlName)
		{
			if (DataBinders.ContainsKey(controlName + "_Render"))
				return DataBinders[controlName + "_Render"];
			else
				return null;
		}
		#endregion

		public string ClientSideScript
		{
			get { return clientSideScript; }
			set
			{
				if (value.Trim().EndsWith(";"))
					clientSideScript += value.Trim();
				else if (value.Trim() != string.Empty)
					clientSideScript += value.Trim() + ";";
			}
		}
		public string MessageBoxText
		{
			get { return messageBoxText; }
			set
			{
				if (value.Trim() == string.Empty)
					messageBoxText = string.Empty;
				else if (value.Trim().ToLower().EndsWith("<br/>"))
					messageBoxText += value.Trim();
				else
					messageBoxText += value.Trim() + "<br/>";
			}
		}
		public bool IsModal
		{
			get { return Helper.RequestBool(this, "IsModal"); }
		}
		public new System.Web.SessionState.HttpSessionState Session
		{
			get
			{
				return HttpContext.Current.Session;
			}
		}
		public new HttpRequest Request
		{
			get
			{
				return HttpContext.Current.Request;
			}
		}
		public UIUserControlBase()
		{
			ClientSideScript = string.Empty;
			MessageBoxText = string.Empty;
			messageBoxTitle = string.Empty;
			closeModalResultValue = string.Empty;

			DataBinders = new Dictionary<string, Delegate>();
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			CheckServicePermissions();

			string loadedControl = Helper.RequestEncrypted(this, "c") == string.Empty ? Helper.Decrypt(Helper.Request(this, "c")) : Helper.RequestEncrypted(this, "c");

			if (loadedControl != ConfigurationManager.DefaultLoginPageControlID &&
					(ConfigurationManager.CheckLoginExceptions == string.Empty || ("," + ConfigurationManager.CheckLoginExceptions + ",").IndexOf("," + loadedControl + ",") == -1) &&
					Helper.GetGuid(Session["UserGuid"]) == Guid.Empty)
			{
				Session["SessionExpired"] = true;
				Response.Redirect(string.Format("~/PageLoader.aspx?baseError=1&c={0}", Helper.Encrypt(ConfigurationManager.DefaultLoginPageControlID, Session)));
			}

			if (loadedControl != ConfigurationManager.EditProfilePageID &&
					loadedControl != ConfigurationManager.DefaultLoginPageControlID &&
					!Helper.GetBool(Session["IsAuthenticated"]))
			{
				Response.Redirect(string.Format("~/PageLoader.aspx?c={0}", Helper.Encrypt(ConfigurationManager.EditProfilePageID, Session)));
			}
		}

		private void CheckServicePermissions()
		{
			bool isOptionalPermissions = false;
			bool hasPermissions = true;
			int errorServiceEnum = 0;
			string errorServicesArray = string.Empty;
			List<int> servicePermissions = GetServicePermissions(ref isOptionalPermissions);

			if (!isOptionalPermissions)
				hasPermissions = Security.SecurityManager.HasAllServicePermission(Helper.GetGuid(Session["UserGuid"]), ref errorServiceEnum, servicePermissions.ToArray());
			else
			{
				errorServicesArray = String.Join(",", servicePermissions.ToArray());
				hasPermissions = Security.SecurityManager.HasAtLeastOneServicePermission(Helper.GetGuid(Session["UserGuid"]), servicePermissions.ToArray());
			}

			if (!hasPermissions && !IsModal)
				Response.Redirect(string.Format("~/ErrorHandler.aspx?ErrorType={0}&Service={1}&ServiceArray={2}", (errorServiceEnum == 0 ? (int)ErrorType.SeveralServiceError : (int)ErrorType.OneServiceError), errorServiceEnum, errorServicesArray));
			else if (!hasPermissions)
			{
				Response.Clear();
				Response.Write(string.Format("<h2>{0}</h2><h4>{1}</h4><br/><b>{2}</b>",
																	Language.GetString("DearUser"),
																	Language.GetString("YouDontHavePermissionToAccsessFollowService"),
																	Language.GetString(GetUserControlTitle())
																	));
				Response.End();
			}
		}

		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);

			ClientSideScript = MessageBoxText != string.Empty ? string.Format("messageBox('{0}','{1}','alert','{2}');", MessageBoxText, messageBoxTitle, messageBoxType) : string.Empty;
			ClientSideScript = closeModalResultValue != string.Empty ? string.Format("closeModal('{0}');", closeModalResultValue) : string.Empty;

			if (IsModal && !IsPostBack)
				ClientSideScript = "parent.modalLoadCompleted(window);";

			ClientSideScript = "if(parent.loadPageComplete) parent.loadPageComplete();";

			CryptorEngine encryptor = new CryptorEngine();

			if (Page.Request.Url.OriginalString.Contains("PageLoaderLight"))
				ClientSideScript = string.Format("authenticationString = '{0}';validationString = '{1}';", encryptor.Encrypt(Helper.GetUrlWithoutPortNumber(Page.Request.UrlReferrer.OriginalString)), Helper.Encrypt(encryptor.GetObjectData() + "$" + Helper.GetUrlWithoutPortNumber(Page.Request.UrlReferrer.OriginalString), Session));
			else
				ClientSideScript = string.Format("authenticationString = '{0}';validationString = '{1}';", encryptor.Encrypt(Helper.GetUrlWithoutPortNumber(Page.Request.Url.OriginalString)), Helper.Encrypt(encryptor.GetObjectData() + "$" + Helper.GetUrlWithoutPortNumber(Page.Request.Url.OriginalString), Session));

			if (ClientSideScript != string.Empty)
				writer.WriteLine("<script type=\"text/javascript\">var authenticationString;var validationString;$(document).ready(function(){" + ClientSideScript + "});</script>");
		}

		protected void ShowMessageBox(string messageText)
		{
			MessageBoxText = messageText;
		}

		protected void ShowMessageBox(string messageText, string messageBoxTitle)
		{
			MessageBoxText = messageText;
			this.messageBoxTitle = messageBoxTitle;
		}
		protected void ShowMessageBox(string messageText, string messageBoxTitle, string messageType)
		{
			MessageBoxText = messageText;
			this.messageBoxTitle = messageBoxTitle;
			this.messageBoxType = messageType;
		}

		protected void CloseModal(string resultValue)
		{
			closeModalResultValue = resultValue;
		}

		protected abstract List<int> GetServicePermissions(ref bool isOptionalPermissions);

		protected abstract int GetUserControlID();

		protected abstract string GetUserControlTitle();
	}
}
