using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Web.UI.AccountInformations
{
	public partial class SaveAccount : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid AccountInfoGuid
		{
			get { return Helper.RequestGuid(this, "Guid"); }
		}

		private string ActionType
		{
			get { return Helper.Request(this, "ActionType"); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnSave.Attributes["onclick"] = "return saveAccount();";
			drpBank.Attributes["onchange"] = "showOnlineGatewayInfo();";
			btnSave.Text = Language.GetString("Register");

			foreach (Banks bankes in System.Enum.GetValues(typeof(Banks)))
				drpBank.Items.Add(new ListItem(Language.GetString(bankes.ToString()), ((int)bankes).ToString()));
			drpBank.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			if (ActionType.ToLower() == "edit")
			{
				Common.AccountInformation accountNumber = new Common.AccountInformation();
				accountNumber = Facade.AccountInformation.LoadAccountInformation(AccountInfoGuid);
				txtOwner.Text = accountNumber.Owner;
				drpBank.SelectedValue = accountNumber.Bank.ToString();
				switch (accountNumber.Bank)
				{
					case (int)Banks.Mellat:
						txtTerminalID.Text = accountNumber.TerminalID;
						txtUserName.Text = accountNumber.UserName;
						txtPassword.Text = accountNumber.Password;
						break;
					case (int)Banks.Parsian:
						txtPinCode.Text = accountNumber.PinCode;
						break;
				}

				txtBranch.Text = accountNumber.Branch;
				txtAccountNo.Text = accountNumber.AccountNo;
				txtCardNo.Text = accountNumber.CardNo;
				chbIsActive.Checked = accountNumber.IsActive;
				chbOnlineGatewayIsActive.Checked = accountNumber.OnlineGatewayIsActive;
				ClientSideScript = "showOnlineGatewayInfo();";
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.AccountInformation accountNumber = new Common.AccountInformation();
			try
			{
				accountNumber.Owner = txtOwner.Text.Trim();
				accountNumber.Branch = txtBranch.Text.Trim();
				accountNumber.AccountNo = txtAccountNo.Text.Trim();
				accountNumber.CreateDate = DateTime.Now;
				accountNumber.Bank = Helper.GetInt(drpBank.SelectedValue);
				accountNumber.CardNo = txtCardNo.Text.Trim();
				accountNumber.IsActive = chbIsActive.Checked;
				accountNumber.OnlineGatewayIsActive = chbOnlineGatewayIsActive.Checked;
				accountNumber.UserGuid = UserGuid;

				switch (ActionType.ToLower())
				{
					case "edit":
						accountNumber.AccountInfoGuid = AccountInfoGuid;
						switch (Helper.GetInt(drpBank.SelectedValue))
						{
							case (int)Banks.Mellat:
								accountNumber.TerminalID = txtTerminalID.Text.Trim();
								accountNumber.UserName = txtUserName.Text.Trim();
								accountNumber.Password = txtPassword.Text.Trim();
								break;
							case (int)Banks.Parsian:
								accountNumber.PinCode = txtPinCode.Text;
								break;
							default:
								accountNumber.TerminalID = accountNumber.UserName = accountNumber.Password = string.Empty;
								break;
						}

						if (accountNumber.HasError)
							throw new Exception(accountNumber.ErrorMessage);

						if (!Facade.AccountInformation.UpdateAccount(accountNumber))
							throw new Exception(Language.GetString("ErrorRecord"));

						break;
					case "insert":
						switch (Helper.GetInt(drpBank.SelectedValue))
						{
							case (int)Banks.Mellat:
								accountNumber.TerminalID = txtTerminalID.Text.Trim();
								accountNumber.UserName = txtUserName.Text.Trim();
								accountNumber.Password = txtPassword.Text.Trim();
								break;
							case (int)Banks.Parsian:
								accountNumber.PinCode = txtPinCode.Text;
								break;
						}

						if (accountNumber.HasError)
							throw new Exception(accountNumber.ErrorMessage);

						if (!Facade.AccountInformation.Insert(accountNumber))
							throw new Exception(Language.GetString("ErrorRecord"));

						break;
				}
				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_AccountInformations_AccountInformation, Session)));
			}
			catch (Exception ex)
			{
				ShowMessageBox(Language.GetString(ex.Message), string.Empty, "danger");
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.AccountInformation);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_AccountInformations_SaveAccount;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Helper.GetString(Business.UserControls.UI_AccountInformations_SaveAccount));
		}


	}
}