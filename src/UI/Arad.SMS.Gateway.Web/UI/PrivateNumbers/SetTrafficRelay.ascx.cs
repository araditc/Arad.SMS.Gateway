using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.PrivateNumbers
{
	public partial class SetTrafficRelay : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid PrivateNumberGuid
		{
			get { return Helper.RequestGuid(this, "Guid"); }
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

			int resultCount = 0;
			DataTable dtTrafficRelay = Facade.TrafficRelay.GetPagedTrafficRelays(UserGuid, "CreateDate", 0, 0, ref resultCount);
			drpSmsTrafficRelay.DataSource = dtTrafficRelay;
			drpSmsTrafficRelay.DataTextField = "Url";
			drpSmsTrafficRelay.DataValueField = "Guid";
			drpSmsTrafficRelay.DataBind();
			drpSmsTrafficRelay.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			drpDeliveryTrafficRelay.DataSource = dtTrafficRelay;
			drpDeliveryTrafficRelay.DataTextField = "Url";
			drpDeliveryTrafficRelay.DataValueField = "Guid";
			drpDeliveryTrafficRelay.DataBind();
			drpDeliveryTrafficRelay.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			Common.PrivateNumber privateNumber = Facade.PrivateNumber.LoadNumber(PrivateNumberGuid);
			drpSmsTrafficRelay.SelectedValue = privateNumber.SmsTrafficRelayGuid.ToString();
			drpDeliveryTrafficRelay.SelectedValue = privateNumber.DeliveryTrafficRelayGuid.ToString();
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.PrivateNumber privateNumber = new Common.PrivateNumber();
			try
			{
				privateNumber.NumberGuid = PrivateNumberGuid;
				privateNumber.SmsTrafficRelayGuid = Helper.GetGuid(drpSmsTrafficRelay.SelectedValue);
				privateNumber.DeliveryTrafficRelayGuid = Helper.GetGuid(drpDeliveryTrafficRelay.SelectedValue);

				if (!Facade.PrivateNumber.UpdateTrafficRelay(privateNumber))
					throw new Exception(Language.GetString("ErrorRecord"));

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_UserPrivateNumber, Session)));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_UserPrivateNumber, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SetTrafficRelayForPrivateNumber);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_SetTrafficRelay;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PrivateNumbers_SetTrafficRelay.ToString());
		}
	}
}