using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.Users
{
	public partial class DetermineGroupPrice : UIUserControlBase
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
		//private Guid UserGuid
		//{
		//	get { return Helper.RequestEncryptedGuid(this, "UserGuid"); }
		//}

		//private Guid ParentGuid
		//{
		//	get { return Helper.RequestEncryptedGuid(this, "ParentGuid"); }
		//}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnSave.Text = Language.GetString("Register");
			btnCancel.Text = Language.GetString("Cancel");
			Common.User user = Facade.User.LoadUser(UserGuid);

			#region GroupPrice
			drpGroupPrice.DataSource = Facade.GroupPrice.GetPagedGroupPrices(CurrentUserGuid);
			drpGroupPrice.DataTextField = "Title";
			drpGroupPrice.DataValueField = "Guid";
			drpGroupPrice.DataBind();
			drpGroupPrice.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			#endregion

			drpGroupPrice.SelectedValue = user.PriceGroupGuid.ToString();
			chbIsFixGroupPrice.Checked = user.IsFixPriceGroup;
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				Guid groupPriceGuid = Helper.GetGuid(drpGroupPrice.SelectedValue);
				if (!Facade.User.UpdateGroupPrice(UserGuid, groupPriceGuid, Guid.Empty))
					throw new Exception(Language.GetString("ErrorRecord"));

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_User, Session)));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
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
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Users_DetermineGroupPrice;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Users_DetermineGroupPrice.ToString());
		}
	}
}