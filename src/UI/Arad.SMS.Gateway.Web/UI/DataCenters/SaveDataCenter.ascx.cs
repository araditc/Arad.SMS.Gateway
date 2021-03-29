using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.DataCenters
{
	public partial class SaveDataCenter : UIUserControlBase
	{
		private Guid DataCenterGuid
		{
			get { return Helper.RequestGuid(this, "Guid"); }
		}

		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
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
			btnSave.Attributes["onclick"] = "return validateRequiredFields('SaveDataCenter');";
			btnSave.Text = Language.GetString(btnSave.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);

			foreach (Business.DataCenterType type in System.Enum.GetValues(typeof(Business.DataCenterType)))
			{
				if (type == Business.DataCenterType.All)
					continue;
				drpType.Items.Add(new ListItem(Language.GetString(type.ToString()), ((int)type).ToString()));
			}
			drpType.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			if (ActionType == "edit")
			{
				Common.DataCenter dataCenter = Facade.DataCenter.LoadDataCenter(DataCenterGuid);
				txtTitle.Text = dataCenter.Title;
				drpType.SelectedValue = dataCenter.Type.ToString();
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.DataCenter dataCenter = new Common.DataCenter();
			try
			{
				dataCenter.UserGuid = UserGuid;
				dataCenter.Title = txtTitle.Text;
				dataCenter.CreateDate = DateTime.Now;
				dataCenter.Type = Helper.GetInt(drpType.SelectedValue);

				if (dataCenter.HasError)
					throw new Exception(dataCenter.ErrorMessage);

				switch (ActionType)
				{
					case "insert":
						if (!Facade.DataCenter.Insert(dataCenter))
							throw new Exception(Language.GetString("ErrorRecord"));

						break;
					case "edit":
						dataCenter.DataCenterGuid = DataCenterGuid;
						if (!Facade.DataCenter.Update(dataCenter))
							throw new Exception(Language.GetString("ErrorRecord"));

						break;
				}

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
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SaveDataCenter);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_DataCenters_SaveDataCenter;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_DataCenters_SaveDataCenter.ToString());
		}
	}
}