﻿// --------------------------------------------------------------------
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

using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.OnlinePayment;
using System;
using System.Web.UI;

namespace Arad.SMS.Gateway.Web.HomePages.Arad
{
	public partial class ExtendedPanel : Page
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid ParentGuid
		{
			get { return Helper.GetGuid(Session["ParentGuid"]); }
		}

		private DateTime ExpireDate
		{
			get { return Helper.GetDateTime(Session["ExpireDate"]); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (UserGuid == Guid.Empty || ExpireDate > DateTime.Now)
				Response.Redirect("~/Index.aspx");

			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnPayment.Text = Language.GetString(btnPayment.Text);

			Common.User user = Facade.User.LoadUser(UserGuid);

			txtPanelPrice.Text = Helper.FormatDecimalForDisplay(user.PanelPrice);
			txtPanelPrice.Enabled = false;
		}

		protected void btnPayment_Click(object sender, EventArgs e)
		{
			Common.Fish fish = new Common.Fish();
			string result = string.Empty;

			try
			{
				if (Helper.GetDecimal(txtPanelPrice.Text) == 0)
				{
					//lblMessage.Text = string.Format("{0}/{1}", UserGuid, ExpireDate);
					if (!Facade.User.UpdateExpireDate(UserGuid, DateTime.Now.AddYears(1)))
						throw new Exception(Language.GetString("ErrorRecord"));

					Response.Redirect("~/Index.aspx");
				}

				Common.AccountInformation accountInfo = Facade.AccountInformation.LoadAccountInformation(Helper.GetGuid(hdnAccountGuid.Value));

				Common.User parent = Facade.User.LoadUser(ParentGuid);

				switch (accountInfo.Bank)
				{
					case (int)Banks.Mellat:
						BehPardakhtMellat behPardakht = new BehPardakhtMellat();

						if (Helper.CheckDataConditions(accountInfo.TerminalID).IsEmpty ||
								Helper.CheckDataConditions(accountInfo.UserName).IsEmpty ||
								Helper.CheckDataConditions(accountInfo.Password).IsEmpty)
							throw new Exception(Language.GetString("OnlineGatewayInfoIncorrect"));

						behPardakht.TerminalID = Helper.GetLong(accountInfo.TerminalID);
						behPardakht.UserName = accountInfo.UserName;
						behPardakht.Password = accountInfo.Password;
						behPardakht.Amount = Helper.GetLong(txtPanelPrice.Text);
						behPardakht.CallBackUrl = string.Format("{0}/{1}/{2}/", "http://" + Helper.GetLocalDomain(Request.Url.Authority), "ExtendedPanel", (int)Banks.Mellat);
						string refID = behPardakht.Request(ref result);

						if (Helper.CheckDataConditions(refID).IsEmpty)
							throw new Exception(Language.GetString("DontAccessToOnlineGateway"));

						#region InsertFish
						fish.ReferenceID = refID;
						fish.CreateDate = DateTime.Now;
						fish.PaymentDate = DateTime.Now;
						fish.SmsCount = 0;
						fish.Amount = Helper.GetDecimal(txtPanelPrice.Text);
						fish.OrderID = behPardakht.OrderID.ToString();
						fish.Type = (int)TypeFish.OnLine;
						fish.Status = (int)FishStatus.Checking;
						fish.AccountInformationGuid = accountInfo.AccountInfoGuid;
						fish.UserGuid = UserGuid;
						Facade.Fish.InsertOnlinePayment(fish);
						#endregion

						Page.ClientScript.RegisterClientScriptBlock(typeof(string), "behpardakht", string.Format("<script language=javascript type='text/javascript'>$(function(){{{0}}});</script>", behPardakht.GotoGateway(refID)));
						break;
				}
			}
			catch (Exception ex)
			{
				lblMessage.Text = ex.Message;
			}
		}
	}
}