using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.GeneralLibrary.OnlinePayment;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.Users
{
	public partial class RegisterFish : UIUserControlBase
	{
		protected decimal basePrice;
		protected bool decreaseTax;
		private Guid ParentGuid
		{
			get { return Helper.GetGuid(Session["ParentGuid"]); }
		}

		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			//if (!IsPostBack)
			InitializePage();
		}

		private void InitializePage()
		{
			btnSaveFishPayment.Text = Language.GetString(btnSaveFishPayment.Text);
			btnSaveOnlinePayment.Text = Language.GetString(btnSaveOnlinePayment.Text);
			txtOnlineAmount.Attributes["readonly"] = "readonly";
			txtSmsCount.Attributes["readonly"] = "readonly";

			//lblDate.Text = Persia.Calendar.ConvertToPersian(DateTime.Now).Simple;
			//dtpPaymentDate.Value = Persia.Calendar.ConvertToPersian(DateTime.Now).Simple;


            if (Session["Language"].ToString() == "fa")
            {
                lblDate.Text = DateManager.GetSolarDate(DateTime.Now);
                dtpPaymentDate.Value = DateManager.GetSolarDate(DateTime.Now);
            }
            else
            {
                lblDate.Text = DateTime.Now.ToShortDateString();
                dtpPaymentDate.Value = DateTime.Now.ToShortDateString();
            }

            Common.User user = new Common.User();
			user = Facade.User.LoadUser(UserGuid);
			lblShopperName.Text = string.Format("{0} {1}({2})", user.FirstName, user.LastName, user.CompanyName);
			lblShopperPhone.Text = string.Format("{0}-{1}", user.Phone, user.CompanyPhone);
			lblShopperAddress.Text = user.Address;

			user = Facade.User.LoadUser(ParentGuid);
			lblSellerName.Text = string.Format("{0}", user.CompanyName);
			lblSellerPhone.Text = user.CompanyPhone;
			lblSellerAddress.Text = user.CompanyAddress;

			Guid userGuid = (ParentGuid == Guid.Empty ? UserGuid : ParentGuid);

			DataTable dtGroupPrice = Facade.GroupPrice.GetGroupPrices(UserGuid, ParentGuid);
			if (dtGroupPrice.Rows.Count > 0)
			{
				gridTariff.DataSource = dtGroupPrice;
				gridTariff.DataBind();
			}
			else
				basePrice = Facade.GroupPrice.GetUserBaseSmsPrice(UserGuid, ParentGuid, 0, ref decreaseTax);

			int resultCount = 0;
			DataTable dtAccounts = Facade.AccountInformation.GetPagedAccountInformations(userGuid, "CreateDate", 1, 100, ref resultCount);
			dtAccounts.Columns.Add("TextField", typeof(string));
			Banks bank;

			foreach (DataRow row in dtAccounts.Rows)
			{
				bank = (Banks)Enum.Parse(typeof(Banks), row["Bank"].ToString());
				row["TextField"] = string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}{1}{6}{1}{7}",
																					Language.GetString("Bank"),
																					Environment.NewLine,
																					Language.GetString(bank.ToString()),
																					Language.GetString("Account"),
																					row["AccountNo"],
																					Language.GetString("CardNo"),
																					row["CardNo"],
																					row["Owner"]);
			}

			drpAccount.DataSource = dtAccounts;
			drpAccount.DataTextField = "TextField";
			drpAccount.DataValueField = "Guid";
			drpAccount.DataBind();
			drpAccount.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			btnSaveFishPayment.Attributes["onclick"] = "return validateRequiredFields('account');";
			btnSaveOnlinePayment.Attributes["onclick"] = "return validateRequiredFields('onlinepayment');";

			DataTable dtOnlineAccount = Facade.AccountInformation.GetAccountsIsActiveOnline(userGuid);
			rdbMellat.Disabled = true;
			rdbParsian.Disabled = true;


			foreach (DataRow row in dtOnlineAccount.Rows)
			{
				switch (Helper.GetInt(row["Bank"]))
				{
					case (int)Banks.Mellat:
						rdbMellat.Value = row["Guid"].ToString();
						rdbMellat.Disabled = false;
						rdbMellat.Checked = true;
						break;
					case (int)Banks.Parsian:
						rdbParsian.Value = row["Guid"].ToString();
						rdbParsian.Disabled = false;
						break;
				}
			}
		}

		protected void btnSaveFishPayment_Click(object sender, EventArgs e)
		{
			bool decreaseTax = true;
			Common.Fish fish = new Common.Fish();
			try
			{
				decimal basePrice = Facade.GroupPrice.GetUserBaseSmsPrice(UserGuid, ParentGuid, Helper.GetLong(txtSmsCount.Text), ref decreaseTax);
				decimal tax = 0;
				if (decreaseTax)
					tax = Facade.Transaction.ComputeTax(Helper.GetDecimal(basePrice * Helper.GetInt(txtSmsCount.Text)));

				fish.CreateDate = DateTime.Now;
				fish.BillNumber = txtSerialNumber.Text;
				fish.SmsCount = Helper.GetLong(txtSmsCount.Text);
				fish.Amount = Helper.GetDecimal(txtAccountAmount.Text);
				fish.PaymentDate = DateManager.GetChristianDateForDB(dtpPaymentDate.Value);
				fish.Description = txtDescription.Text;
				fish.Type = (int)TypeFish.Account;
				fish.Status = (int)FishStatus.Checking;
				fish.AccountInformationGuid = Helper.GetGuid(drpAccount.SelectedValue);
				fish.UserGuid = UserGuid;

				if (!Facade.Fish.InsertFishPayment(fish))
					throw new Exception(Language.GetString("InsertRecord"));

				ClientSideScript = string.Format("saveResult('account','ok','{0}')", Language.GetString("InsertRecord"));
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("saveResult('account','error','{0}')", ex.Message);
			}
		}

		protected void btnSaveOnlinePayment_Click(object sender, EventArgs e)
		{
			Common.AccountInformation accountInfo = new Common.AccountInformation();
			Common.Fish fish = new Common.Fish();
			string result = string.Empty;

			try
			{
				if (rdbMellat.Checked)
				{
					if (Helper.GetGuid(rdbMellat.Value) == Guid.Empty)
						throw new Exception(Language.GetString("AccountInvalid"));
					accountInfo = Facade.AccountInformation.LoadAccountInformation(Helper.GetGuid(rdbMellat.Value));
				}
				else if (rdbParsian.Checked)
				{
					if (Helper.GetGuid(rdbParsian.Value) == Guid.Empty)
						throw new Exception(Language.GetString("AccountInvalid"));
					accountInfo = Facade.AccountInformation.LoadAccountInformation(Helper.GetGuid(rdbParsian.Value));
				}

				Common.User parent = Facade.User.LoadUser(ParentGuid);
				if (!parent.IsMainAdmin && parent.Credit < Helper.GetLong(txtOnlineSmsCount.Text))
					throw new Exception(Language.GetString("SystemUnableAssignCredit"));

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
						behPardakht.Amount = Helper.GetLong(txtOnlineAmount.Text);
						behPardakht.CallBackUrl = string.Format("{0}/{1}/{2}", "http://" + Helper.GetLocalDomain(Request.Url.Authority), "PaymentReport", (int)Arad.SMS.Gateway.Business.Banks.Mellat);
						string refID = behPardakht.Request(ref result);

						if (Helper.CheckDataConditions(refID).IsEmpty)
							throw new Exception(Language.GetString("DontAccessToOnlineGateway"));

						#region InsertFish
						fish.ReferenceID = refID;
						fish.CreateDate = DateTime.Now;
						fish.PaymentDate = DateTime.Now;
						fish.SmsCount = Helper.GetLong(txtOnlineSmsCount.Text);
						fish.Amount = Helper.GetDecimal(txtOnlineAmount.Text);
						fish.OrderID = behPardakht.OrderID.ToString();
						fish.Type = (int)Arad.SMS.Gateway.Business.TypeFish.OnLine;
						fish.Status = (int)Arad.SMS.Gateway.Business.FishStatus.Checking;
						fish.AccountInformationGuid = accountInfo.AccountInfoGuid;
						fish.UserGuid = UserGuid;
						Facade.Fish.InsertOnlinePayment(fish);
						#endregion

						ClientSideScript = behPardakht.GotoGateway(refID);
						break;

					case (int)Banks.Parsian:
						ParsianPaymentGateway parsian = new ParsianPaymentGateway();

						if (Helper.CheckDataConditions(accountInfo.PinCode).IsEmpty)
							throw new Exception(Language.GetString("OnlineGatewayInfoIncorrect"));

						parsian.Pin = accountInfo.PinCode;
						parsian.Amount = Helper.GetInt(txtOnlineAmount.Text);
						parsian.CallBackUrl = string.Format("{0}/{1}/{2}", "http://" + Helper.GetLocalDomain(Request.Url.Authority), "PaymentReport", (int)Arad.SMS.Gateway.Business.Banks.Parsian);
						string authority = parsian.Request();

						if (Helper.CheckDataConditions(authority).IsEmpty)
							throw new Exception(Language.GetString("DontAccessToOnlineGateway"));

						#region InsertFish
						fish.ReferenceID = authority;
						fish.CreateDate = DateTime.Now;
						fish.PaymentDate = DateTime.Now;
						fish.SmsCount = Helper.GetLong(txtOnlineSmsCount.Text);
						fish.Amount = Helper.GetDecimal(txtOnlineAmount.Text);
						fish.OrderID = parsian.OrderID.ToString();
						fish.Type = (int)TypeFish.OnLine;
						fish.Status = (int)FishStatus.Checking;
						fish.AccountInformationGuid = accountInfo.AccountInfoGuid;
						fish.UserGuid = UserGuid;
						Facade.Fish.InsertOnlinePayment(fish);
                        #endregion

                        var redirectUrl = string.Format(parsian.PostUrl, authority);
                        //Response.Redirect(redirectUrl, true);

                        Response.Write("<script language='javascript'>self.parent.location='"+ redirectUrl + "';</script>");


                        break;
				}
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("saveResult('online','error','{0}')", ex.Message);
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.RegisterFish);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_Users_RegisterFish;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_Users_RegisterFish.ToString());
		}
	}
}
