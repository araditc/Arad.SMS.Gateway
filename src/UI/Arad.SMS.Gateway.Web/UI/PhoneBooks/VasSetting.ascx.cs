using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using Arad.SMS.Gateway.Business;
using System.Web.UI.WebControls;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.PhoneBooks
{
	public partial class VasSetting : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid PhoneBookGuid
		{
			get { return Helper.GetGuid(Helper.Request(this, "Guid").Trim('\'')); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnSave.Text = Language.GetString(btnSave.Text);

			foreach (PhoneBookGroupType type in Enum.GetValues(typeof(PhoneBookGroupType)))
				drpType.Items.Add(new ListItem(Language.GetString(type.ToString()), ((int)type).ToString()));
			drpType.SelectedValue = Helper.GetString((int)PhoneBookGroupType.Vas);

			Common.PhoneBook phoneBook= Facade.PhoneBook.Load(PhoneBookGuid);
			drpType.SelectedValue = phoneBook.Type.ToString();
			txtServiceId.Text = phoneBook.ServiceId;
			txtOwner.Text = Facade.User.GetUserName(phoneBook.AlternativeUserGuid);
			txtRegisterKeys.Text = phoneBook.VASRegisterKeys;
			txtUnsubscribeKeys.Text = phoneBook.VASUnsubscribeKeys;
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.PhoneBook phoneBook = new Common.PhoneBook();

			try
			{
				phoneBook.PhoneBookGuid = PhoneBookGuid;
				phoneBook.Type = Helper.GetByte(drpType.SelectedValue);
				phoneBook.ServiceId = txtServiceId.Text;
				phoneBook.VASRegisterKeys = txtRegisterKeys.Text;
				phoneBook.VASUnsubscribeKeys = txtUnsubscribeKeys.Text;

				DataTable dtUser = Facade.User.GetUser(txtOwner.Text);
				phoneBook.AlternativeUserGuid = dtUser.Rows.Count > 0 ? Helper.GetGuid(dtUser.Rows[0]["Guid"]) : Guid.Empty;

				if (!Facade.PhoneBook.UpdateVasSetting(phoneBook))
					throw new Exception(Language.GetString("ErrorRecord"));

				ShowMessageBox(Language.GetString("InsertRecord"), string.Empty, "success");
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.PhoneBookVasSetting);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_VasSetting;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PhoneBooks_VasSetting.ToString());
		}
	}
}