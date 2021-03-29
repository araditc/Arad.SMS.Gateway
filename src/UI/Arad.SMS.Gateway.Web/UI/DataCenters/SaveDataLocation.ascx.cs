using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arad.SMS.Gateway.Business;

namespace Arad.SMS.Gateway.Web.UI.DataCenters
{
	public partial class SaveDataLocation : UIUserControlBase
	{
		private Guid DataCenterGuid
		{
			get { return Helper.RequestGuid(this, "DataCenterGuid"); }
		}

		private string DomainName
		{
			get { return Helper.GetDomain(Request.Url.Authority); }
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

			DataTable dataTableDomainInfo = Facade.Domain.GetDomainInfo(DomainName);

			int desktop = Helper.GetInt(dataTableDomainInfo.Rows[0]["Desktop"]);

			drpDesktop.Items.Add(new ListItem(Language.GetString(Business.Desktop.Default.ToString()), ((int)Arad.SMS.Gateway.Business.Desktop.Default).ToString()));
			drpDesktop.Items.Add(new ListItem(Language.GetString(((Business.Desktop)desktop).ToString()), (desktop).ToString()));
			drpDesktop.Items.Insert(0, new ListItem("", ""));

			foreach (DataLocation location in System.Enum.GetValues(typeof(DataLocation)))
				drpLocation.Items.Add(new ListItem(Language.GetString(location.ToString()), ((int)location).ToString()));
			drpLocation.Items.Insert(0, new ListItem("", ""));

			Common.DataCenter dataCenter = Facade.DataCenter.LoadDataCenter(DataCenterGuid);
			drpLocation.SelectedValue = dataCenter.Location.ToString();
			drpDesktop.SelectedValue = dataCenter.Desktop.ToString();
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.DataCenter dataCenter = new Common.DataCenter();
			try
			{
				dataCenter.DataCenterGuid = DataCenterGuid;
				dataCenter.Location = Helper.GetInt(drpLocation.SelectedValue);
				dataCenter.Desktop = Helper.GetInt(drpDesktop.SelectedValue);

				if (!Facade.DataCenter.UpdateLocation(dataCenter))
					throw new Exception(Language.GetString("ErrorRecord"));

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_DataCenters_DataCenter, Session)));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_DataCenters_DataCenter, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SaveDataLocation);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_DataCenters_SaveDataLocation;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_DataCenters_SaveDataLocation.ToString());
		}
	}
}