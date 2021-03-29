using System;
using System.Collections.Generic;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.Business;

namespace Arad.SMS.Gateway.Web.UI.Roles
{
	public partial class SaveRole : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid RoleGuid
		{
			get { return Helper.RequestGuid(this, "RoleGuid"); }
		}

		private string ActionType
		{
			get { return Helper.Request(this, "ActionType").ToLower(); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnSave.Text = Language.GetString(btnSave.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);
			btnSave.Attributes["onclick"] = "return validateRequiredFields();";

			if (ActionType == "edit")
			{
				Common.Role role = Facade.Role.LoadRole(RoleGuid);
				txtTitle.Text = role.Title;
				chbDefaultRole.Checked = role.IsDefault;
				ChbIsSalePackage.Checked = role.IsSalePackage;
				txtPrice.Text = Helper.FormatDecimalForDisplay(role.Price);
				txtDescription.Text = role.Description;
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				Common.Role role = new Common.Role();
				role.RoleGuid = RoleGuid;
				role.Title = txtTitle.Text;
				role.Price =Helper.GetDecimal(txtPrice.Text);
				role.Description = txtDescription.Text;
				role.Priority = Helper.GetByte(txtPriority.Text);
				role.IsSalePackage = ChbIsSalePackage.Checked;
				role.IsDefault = chbDefaultRole.Checked;
				role.UserGuid = UserGuid;
				role.CreateDate = DateTime.Now;

				if (role.HasError)
					throw new Exception(role.ErrorMessage);

				switch (ActionType)
				{
					case "edit":
						if (!Facade.Role.UpdateRole(role))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
					case "insert":
						if (!Facade.Role.InsertRole(role))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
				}

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)UserControls.UI_Roles_Role, Session)));
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("result('error','{0}')", ex.Message);
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)UserControls.UI_Roles_Role, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SaveRole);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Roles_SaveRole;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Roles_SaveRole.ToString());
		}
	}
}