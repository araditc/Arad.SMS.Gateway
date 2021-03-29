using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;

namespace Arad.SMS.Gateway.Web.UI.Accesses
{
	public partial class SaveAccess : UIUserControlBase
	{
		private Guid AccessGuid
		{
			get { return Helper.RequestEncryptedGuid(this, "Guid"); }
		}

		private string ActionType
		{
			get { return Helper.Request(this, "ActionType"); }
		}

			protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				InitializePage();
			}
		}

		private void InitializePage()
		{
			btnSave.Text = Language.GetString("Register");
			int rowCount = 0;

			drpService.DataSource = Facade.Service.GetPagedService(string.Empty, "[CreateDate]", 0, 0, ref rowCount);
			drpService.DataTextField = "Title";
			drpService.DataValueField = "Guid";
			drpService.DataBind();

			#region Add Permissions to Dropdown
			foreach (Business.Permissions access in System.Enum.GetValues(typeof(Business.Permissions)))
				drpAccess.Items.Add(new ListItem(Language.GetString(access.ToString()), ((int)access).ToString()));
			#endregion

			if (ActionType.ToLower() == "edit")
			{
				Common.Access access = new Common.Access();
				access = Facade.Access.LoadAccess(AccessGuid);
				drpService.SelectedValue = access.ServiceGuid.ToString();
				drpAccess.SelectedValue = access.ReferencePermissionsKey.ToString();
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.Access access = new Common.Access();
			switch (ActionType.ToLower())
			{

				case "edit":
					access.AccessGuid = AccessGuid;
					access.ServiceGuid = Helper.GetGuid(drpService.SelectedValue);
					access.ReferencePermissionsKey = Helper.GetInt(drpAccess.SelectedValue);
					if (access.HasError)
						ShowMessageBox(access.ErrorMessage);
					else
						if (Facade.Access.UpdateAccess(access))
							CloseModal("true");
						else
							ShowMessageBox(Language.GetString("ErrorRecord"));
					break;
				case "insert":
					access.ReferencePermissionsKey = Helper.GetInt(drpAccess.SelectedValue);
					access.CreateDate = DateTime.Now;
					access.ServiceGuid = Helper.GetGuid(drpService.SelectedValue);
					if (access.HasError)
						ShowMessageBox(access.ErrorMessage);
					else
						if (Facade.Access.insert(access))
							CloseModal("true");
						else
							ShowMessageBox(Language.GetString("ErrorRecord"));
					break;
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageAccess);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Accesses_SaveAccess;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Accesses_SaveAccess.ToString());
		}
	}
}