using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.PhoneBooks
{
	public partial class UpdateGroup : UIUserControlBase
	{
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
			try
			{
				btnSave.Text = Language.GetString(btnSave.Text);
				btnSave.Attributes["onclick"] = "return validateRequiredFields();";

				foreach (PhoneBookGroupType type in Enum.GetValues(typeof(PhoneBookGroupType)))
					if (type != PhoneBookGroupType.Vas)
						drpType.Items.Add(new ListItem(Language.GetString(type.ToString()), ((int)type).ToString()));

				Common.PhoneBook phoneBook = Facade.PhoneBook.Load(PhoneBookGuid);
				drpType.SelectedValue = phoneBook.Type.ToString();
				txtName.Text = phoneBook.Name;

				if(phoneBook.Type == (int)PhoneBookGroupType.Vas)
				{
					txtName.Enabled = false;
					drpType.Enabled = false;
					btnSave.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				PhoneBookGroupType type=(PhoneBookGroupType)Helper.GetInt( drpType.SelectedValue);
				string name=txtName.Text;
				
				if(string.IsNullOrEmpty(name))
					throw new Exception(Language.GetString("EnterGroupNameWarning"));

				if(!Facade.PhoneBook.UpdateGroup(PhoneBookGuid,type,name))
					throw new Exception(Language.GetString("ErrorRecord"));

				ShowMessageBox(Language.GetString("InsertRecord"), string.Empty, "success");
			}
			catch(Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.PhoneBook);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_UpdateGroup;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PhoneBooks_UpdateGroup.ToString());
		}
	}
}