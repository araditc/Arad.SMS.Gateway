using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace MessagingSystem.UI.Domains
{
	public partial class SaveUserMessage : UIUserControlBase
	{
		private Guid Guid
		{
			get { return Helper.RequestEncryptedGuid(this, "Guid"); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnSave.Text = Language.GetString(btnSave.Text);

			Common.UserMessage userMessage = Facade.UserMessage.LoadUserMessage(Guid);
			txtCompanyName.Text = userMessage.Name;
			txtEmail.Text = userMessage.Email;
			txtJob.Text = userMessage.Job;
			txtTelephone.Text = userMessage.Telephone;
			txtCellPhone.Text = userMessage.CellPhone;
			txtDescription.Text = userMessage.Description;
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				Common.UserMessage userMessage = new Common.UserMessage();
				userMessage.Name = txtCompanyName.Text;
				userMessage.Email = txtEmail.Text;
				userMessage.Job = txtJob.Text;
				userMessage.Telephone = txtTelephone.Text;
				userMessage.CellPhone = txtCellPhone.Text;
				userMessage.Description = txtDescription.Text;
				if (Facade.UserMessage.UpdateUserMessage(userMessage))
					ShowMessageBox(Language.GetString("InsertRecord"));
				else
					ShowMessageBox(Language.GetString("ErrorRecord"));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Business.Services.ManageUserMessage);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Business.UserControls.UI_Domains_SaveUserMessage;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Domains_SaveUserMessage.ToString());
		}
	}
}