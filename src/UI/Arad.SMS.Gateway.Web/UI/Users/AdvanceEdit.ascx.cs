using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.Users
{
	public partial class AdvanceEdit : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.RequestGuid(this, "UserGuid"); }
		}

		private Guid CurrentUserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid ParentGuid
		{
			get { return Helper.RequestGuid(this, "ParentGuid"); }
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
				btnCancel.Text = Language.GetString(btnCancel.Text);
				btnSave.Attributes["onclick"] = "return validateSubmit();";

				#region GetDomains
				int resultCount = 0;
				Guid domainUserGuid = Facade.SmsSenderAgent.GetFirstParentMainAdmin(UserGuid);
				drpDomain.DataSource = Facade.Domain.GetPagedDomains(domainUserGuid, string.Empty, "CreateDate", 0, 0, ref resultCount);
				drpDomain.DataTextField = "Name";
				drpDomain.DataValueField = "Guid";
				drpDomain.DataBind();
				drpDomain.Items.Insert(0, new ListItem(string.Empty, Guid.Empty.ToString()));

				drpDomain.Enabled = Helper.GetBool(Session["IsMainAdmin"]);
				#endregion

				#region Roles
				drpRole.DataSource = Facade.Role.GetRoles(CurrentUserGuid);
				drpRole.DataTextField = "Title";
				drpRole.DataValueField = "Guid";
				drpRole.DataBind();
				drpRole.Items.Insert(0, new ListItem(string.Empty, string.Empty));
				#endregion

				#region GroupPrice
				drpGroupPrice.DataSource = Facade.GroupPrice.GetPagedGroupPrices(CurrentUserGuid);
				drpGroupPrice.DataTextField = "Title";
				drpGroupPrice.DataValueField = "Guid";
				drpGroupPrice.DataBind();
				drpGroupPrice.Items.Insert(0, new ListItem(string.Empty, string.Empty));
				#endregion

				#region user type
				foreach (UserType type in System.Enum.GetValues(typeof(UserType)))
					drpType.Items.Add(new ListItem(Language.GetString(type.ToString()), ((int)type).ToString()));
				#endregion

				#region LoadUser
				Common.User user = Facade.User.LoadUser(UserGuid);
				txtUserName.Text = user.UserName;
				hdnPassword.Value = user.Password;
				txtPassword.Text = string.Empty;
				txtMaximumAdmin.Text = Helper.GetString(user.MaximumAdmin);
				txtMaximumUser.Text = Helper.GetString(user.MaximumUser);
				txtMaximumPhoenNumber.Text = user.MaximumPhoneNumber.ToString();
				txtMaximumEmailAddress.Text = user.MaximumEmailAddress.ToString();
				txtPanelPrice.Text = Helper.FormatDecimalForDisplay(user.PanelPrice);
				dtpExpireDate.Value = user.ExpireDate != DateTime.MinValue ? DateManager.GetSolarDate(user.ExpireDate) : string.Empty;
				drpDomain.SelectedValue = user.DomainGuid.ToString();
				drpRole.SelectedValue = user.RoleGuid.ToString();
				drpGroupPrice.SelectedValue = user.PriceGroupGuid.ToString();
				chbIsActive.Checked = user.IsActive;
				chbIsFixGroupPrice.Checked = user.IsFixPriceGroup;
				chbIsAuthenticated.Checked = user.IsAuthenticated;
				drpType.SelectedValue = user.Type.ToString();
				#endregion
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("result('error','{0}');", ex.Message);
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				Common.User user = new Common.User();
				user.UserGuid = UserGuid;
				user.UserName = txtUserName.Text;
				if (!Facade.User.CheckUserNameValid(user.UserName, user.UserGuid))
					throw new Exception(Language.GetString("UserNameIsDuplicate"));

				user.Password = txtPassword.Text != string.Empty ? txtPassword.Text : hdnPassword.Value;
				user.MaximumAdmin = Helper.GetInt(txtMaximumAdmin.Text);
				user.MaximumUser = Helper.GetInt(txtMaximumUser.Text);
				user.MaximumPhoneNumber = Helper.GetInt(txtMaximumPhoenNumber.Text);
				user.MaximumEmailAddress = Helper.GetInt(txtMaximumEmailAddress.Text);
				user.DomainGuid = Helper.GetGuid(drpDomain.SelectedValue);
				user.PanelPrice = Helper.GetDecimal(txtPanelPrice.Text);
				//user.ExpireDate = DateManager.GetChristianDateTimeForDB(dtpExpireDate.FullDateTime);
                var dateTime = dtpExpireDate.FullDateTime;
                if (Session["Language"].ToString() == "fa")
                {
                    user.ExpireDate = DateManager.GetChristianDateTimeForDB(dateTime);
                }
                else
                {
                    user.ExpireDate = DateTime.Parse(dateTime);
                }
                user.RoleGuid = Helper.GetGuid(drpRole.SelectedValue);
				user.PriceGroupGuid = Helper.GetGuid(drpGroupPrice.SelectedValue);
				user.IsActive = chbIsActive.Checked;
				user.IsFixPriceGroup = chbIsFixGroupPrice.Checked;
				user.IsAuthenticated = chbIsAuthenticated.Checked;
				user.ParentGuid = ParentGuid;
				user.Type = Helper.GetInt(drpType.SelectedValue);

				if (!Facade.User.AdvanceUpdate(user))
					throw new Exception(Language.GetString("ErrorRecord"));

				//ClientSideScript = string.Format("result('ok','{0}');", Language.GetString("InsertRecord"));
				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_User, Session)));
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("result('error','{0}');", ex.Message);
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_User, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManageUser);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_Users_AdvanceEdit;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_Users_AdvanceEdit.ToString());
		}
	}
}
