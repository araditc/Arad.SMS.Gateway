using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;

namespace Arad.SMS.Gateway.Web.UI.TrafficRelays
{
	public partial class SaveTrafficRelay : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public Guid UrlGuid
		{
			get { return Helper.RequestGuid(this, "UrlGuid"); }
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

			if (ActionType == "edit")
			{
				Common.TrafficRelay trafficRelay = new Common.TrafficRelay();
				trafficRelay = Facade.TrafficRelay.LoadUrl(UrlGuid);
				txtTitle.Text = trafficRelay.Title;
				txtUrl.Text = trafficRelay.Url;
				drpTryCount.SelectedValue = trafficRelay.TryCount.ToString();
				chbIsActive.Checked = trafficRelay.IsActive;
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.TrafficRelay trafficRelay = new Common.TrafficRelay();
			try
			{
				trafficRelay.Title = txtTitle.Text;
				trafficRelay.Url = txtUrl.Text;
				trafficRelay.IsActive = chbIsActive.Checked;
				trafficRelay.TryCount = Helper.GetInt(drpTryCount.SelectedValue);
				trafficRelay.CreateDate = DateTime.Now;
				trafficRelay.UserGuid = UserGuid;

				if (trafficRelay.HasError)
					throw new Exception(trafficRelay.ErrorMessage);

				switch (ActionType)
				{
					case "edit":
						trafficRelay.TrafficRelayGuid = UrlGuid;
						if (!Facade.TrafficRelay.UpdateUrl(trafficRelay))
							throw new Exception(Language.GetString("ErrorRecord"));

						break;

					case "insert":
						if (!Facade.TrafficRelay.InsertUrl(trafficRelay))
							throw new Exception(Language.GetString("ErrorRecord"));

						break;
				}

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_TrafficRelays_TrafficRelay, Session)));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_TrafficRelays_TrafficRelay, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SaveTrafficRelay);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_TrafficRelays_SaveTrafficRelay;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_TrafficRelays_SaveTrafficRelay.ToString());
		}
	}
}