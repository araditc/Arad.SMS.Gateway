using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.IO;

namespace Arad.SMS.Gateway.Web.UI.ServiceGroups
{
	public partial class SaveServiceGroup : UIUserControlBase
	{
		private Guid ServiceGroupGuid
		{
			get { return Helper.RequestGuid(this, "Guid"); }
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
			int rowCount = 0;
			drpMasterGroup.DataSource = Facade.ServiceGroup.GetPagedServiceGroup(string.Empty, "[CreateDate]", 0, 0, ref rowCount);
			drpMasterGroup.DataTextField = "Title";
			drpMasterGroup.DataValueField = "Guid";
			drpMasterGroup.DataBind();
			drpMasterGroup.Items.Insert(0, new ListItem(string.Empty, Guid.Empty.ToString()));

			if (ActionType == "edit")
			{
				Common.ServiceGroup serviceGroup = new Common.ServiceGroup();
				serviceGroup = Facade.ServiceGroup.LoadServiceGroup(ServiceGroupGuid);
				txtTitle.Text = serviceGroup.Title;
				txtOrder.Text = serviceGroup.Order.ToString();
				hdnIcon.Value = serviceGroup.IconAddress;
				hdnLargeIcon.Value = serviceGroup.LargeIcon;
				drpMasterGroup.SelectedValue = Helper.GetGuid(serviceGroup.parentGuid).ToString();
			}

			btnSave.Text = Language.GetString(btnSave.Text);
			btnSave.Attributes["onclick"] = "return validateRequiredFields('AddServiceGroup');";
			btnCancel.Text = Language.GetString(btnCancel.Text);
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.ServiceGroup serviceGroup = new Common.ServiceGroup();
			string uploadTarget = Server.MapPath("~/Images/");
			List<string> lstValidExtention = new List<string>();
			lstValidExtention.Add(".jpg");
			lstValidExtention.Add(".jpeg");
			lstValidExtention.Add(".png");
			lstValidExtention.Add(".gif");
			lstValidExtention.Add(".bmp");

			try
			{
				serviceGroup.ServiceGroupGuid = ServiceGroupGuid;
				serviceGroup.Title = txtTitle.Text;
				serviceGroup.Order = Helper.GetInt(txtOrder.Text);
				serviceGroup.parentGuid = Helper.GetGuid(drpMasterGroup.SelectedValue);
				serviceGroup.IconAddress = hdnIcon.Value;
				serviceGroup.LargeIcon = hdnLargeIcon.Value;
				serviceGroup.CreateDate = DateTime.Now;

				if (uploadIcon.HasFile)
				{
					string fileExtention = Path.GetExtension(uploadIcon.PostedFile.FileName).ToLower();
					if (!lstValidExtention.Contains(fileExtention))
						throw new Exception((Language.GetString("InvalidFileExtension")));

					string iconPic = Guid.NewGuid().ToString() + fileExtention;
					uploadIcon.SaveAs(uploadTarget + iconPic);
					serviceGroup.IconAddress = "Images/" + iconPic;
				}

				if (uploadLargeIcon.HasFile)
				{
					string fileExtention = Path.GetExtension(uploadLargeIcon.PostedFile.FileName).ToLower();
					if (!lstValidExtention.Contains(fileExtention))
						throw new Exception((Language.GetString("InvalidFileExtension")));

					string largeIconPic = Guid.NewGuid().ToString() + fileExtention;
					uploadLargeIcon.SaveAs(uploadTarget + largeIconPic);
					serviceGroup.LargeIcon = "Images/" + largeIconPic;
				}

				if (serviceGroup.HasError)
					throw new Exception(serviceGroup.ErrorMessage);

				switch (ActionType)
				{
					case "edit":
						if (!Facade.ServiceGroup.UpdateServiceGroup(serviceGroup))
							throw new Exception(Language.GetString("ErrorRecord"));

						break;
					case "insert":
						if (!Facade.ServiceGroup.Insert(serviceGroup))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
				}

				ltrResult.Text = string.Format("<div class='bg-success div-save-result'><span class='fa fa-check fa-2x green'></span>{0}</div>", Language.GetString("InsertRecord"));
			}
			catch (Exception ex)
			{
				ltrResult.Text = string.Format("<div class='bg-danger div-save-result'><span class='fa fa-times fa-2x red'></span>{0}</div>", ex.Message);
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_ServiceGroups_ServiceGroup, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SaveServiceGroup);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_ServiceGroups_SaveServiceGroup;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_ServiceGroups_SaveServiceGroup.ToString());
		}
	}
}