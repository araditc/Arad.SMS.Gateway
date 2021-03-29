using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.GeneralLibrary.OnlinePayment;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Arad.SMS.Gateway.Web.UI.PrivateNumbers
{
	public partial class ExtendedNumber : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid ParentGuid
		{
			get { return Helper.GetGuid(Session["ParentGuid"]); }
		}

		private Guid PrivateNumberGuid
		{
			get { return Helper.RequestGuid(this, "Guid"); }
		}

		Common.PrivateNumber privateNumber;
		protected void Page_Load(object sender, EventArgs e)
		{
			InitializePage();
		}

		private void InitializePage()
		{
			btnPayment.Text = Language.GetString(btnPayment.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);

			privateNumber = Facade.PrivateNumber.LoadNumber(PrivateNumberGuid);
			txtPrice.Text = Helper.FormatDecimalForDisplay(privateNumber.Price);
			txtPrice.Enabled = false;
		}

		protected void btnPayment_Click(object sender, EventArgs e)
		{
			Common.Fish fish = new Common.Fish();
			string result = string.Empty;

			try
			{
				if (Helper.GetDecimal(txtPrice.Text) == 0)
				{
					if (!Facade.PrivateNumber.UpdateExpireDate(PrivateNumberGuid, DateTime.Now.AddYears(1)))
						throw new Exception(Language.GetString("ErrorRecord"));

					Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)UserControls.UI_PrivateNumbers_UserPrivateNumber, Session)));
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
						behPardakht.Amount = Helper.GetLong(txtPrice.Text);
						behPardakht.CallBackUrl = string.Format("{0}/{1}/{2}", "http://" + Helper.GetLocalDomain(Request.Url.Authority), "ExtendedNumber", (int)Banks.Mellat);
						string refID = behPardakht.Request(ref result);

						if (Helper.CheckDataConditions(refID).IsEmpty)
							throw new Exception(Language.GetString("DontAccessToOnlineGateway"));

						#region InsertFish
						fish.ReferenceID = refID;
						fish.CreateDate = DateTime.Now;
						fish.PaymentDate = DateTime.Now;
						fish.SmsCount = 0;
						fish.Amount = Helper.GetDecimal(txtPrice.Text);
						fish.OrderID = behPardakht.OrderID.ToString();
						fish.Type = (int)TypeFish.OnLine;
						fish.Status = (int)FishStatus.Checking;
						fish.ReferenceGuid = PrivateNumberGuid;
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

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)UserControls.UI_PrivateNumbers_UserPrivateNumber, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManagePrivateNumber);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_PrivateNumbers_ExtendedNumber;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_PrivateNumbers_ExtendedNumber.ToString());
		}
	}
}