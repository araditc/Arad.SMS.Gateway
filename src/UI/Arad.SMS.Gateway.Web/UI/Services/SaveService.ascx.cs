using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.Services
{
	public partial class SaveService : UIUserControlBase
	{
		private Guid ServiceGuid
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
			{
				InitializePage();
			}
		}

		private void InitializePage()
		{
			btnSave.Attributes["onclick"] = "return validateRequiredFields('SaveService');";
			btnSave.Text = Language.GetString(btnSave.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);
			int rowCount = 0;

			drpServiceGroup.DataSource = Facade.ServiceGroup.GetPagedServiceGroup(string.Empty, "[CreateDate]", 0, 0, ref rowCount);
			drpServiceGroup.DataTextField = "Title";
			drpServiceGroup.DataValueField = "Guid";
			drpServiceGroup.DataBind();
			drpServiceGroup.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			#region Show UserControls IN DropDownList
			foreach (Business.UserControls userControls in System.Enum.GetValues(typeof(Business.UserControls)))
				drpReferencePage.Items.Add(new ListItem(Language.GetString(userControls.ToString()), ((int)userControls).ToString()));
			drpReferencePage.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			#endregion

			#region  Show Services IN DropDownList
			foreach (Business.Services services in System.Enum.GetValues(typeof(Business.Services)))
				drpReferenceService.Items.Add(new ListItem(Language.GetString(services.ToString()), ((int)services).ToString()));
			drpReferenceService.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			#endregion

			if (ActionType == "edit")
			{
				Common.Service service = new Common.Service();
				service = Facade.Service.LoadService(ServiceGuid);
				txtTitle.Text = service.Title;
				drpReferencePage.SelectedValue = service.ReferencePageKey.ToString();
				drpReferenceService.SelectedValue = service.ReferenceServiceKey.ToString();
				drpServiceGroup.SelectedValue = service.ServiceGroupGuid.ToString();
				txtOrder.Text = service.Order.ToString();
				chbPresentable.Checked = service.Presentable;
				hdnIcon.Value = service.IconAddress;
				hdnLargeIcon.Value = service.LargeIcon;
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.Service service = new Common.Service();
			string uploadTarget = Server.MapPath("~/Images/");
			List<string> lstValidExtention = new List<string>();
			lstValidExtention.Add(".jpg");
			lstValidExtention.Add(".jpeg");
			lstValidExtention.Add(".png");
			lstValidExtention.Add(".gif");
			lstValidExtention.Add(".bmp");

			try
			{
				service.Title = txtTitle.Text;
				service.IconAddress = string.Empty;
				service.LargeIcon = string.Empty;
				service.Presentable = chbPresentable.Checked;
				service.ReferencePageKey = Helper.GetInt(drpReferencePage.SelectedValue);
				service.ReferenceServiceKey = Helper.GetInt(drpReferenceService.SelectedValue);
				service.ServiceGroupGuid = Helper.GetGuid(drpServiceGroup.SelectedValue);
				service.Order = Helper.GetInt(txtOrder.Text);
				service.IconAddress = hdnIcon.Value;
				service.LargeIcon = hdnLargeIcon.Value;
				service.CreateDate = DateTime.Now;

				if (uploadIcon.HasFile)
				{
					string fileExtention = Path.GetExtension(uploadIcon.PostedFile.FileName).ToLower();
					if (!lstValidExtention.Contains(fileExtention))
						throw new Exception((Language.GetString("InvalidFileExtension")));

					string iconPic = Guid.NewGuid().ToString() + fileExtention;
					uploadIcon.SaveAs(uploadTarget + iconPic);
					service.IconAddress = "Images/" + iconPic;
				}

				if (uploadLargeIcon.HasFile)
				{
					string fileExtention = Path.GetExtension(uploadLargeIcon.PostedFile.FileName).ToLower();
					if (!lstValidExtention.Contains(fileExtention))
						throw new Exception((Language.GetString("InvalidFileExtension")));

					string laregIconPic = Guid.NewGuid().ToString() + fileExtention;
					uploadLargeIcon.SaveAs(uploadTarget + laregIconPic);
					service.LargeIcon = "Images/" + laregIconPic;
				}

				if (service.HasError)
					throw new Exception(service.ErrorMessage);

				switch (ActionType)
				{
					case "edit":
						service.ServiceGuid = ServiceGuid;
						if (!Facade.Service.UpdateService(service))
							throw new Exception(Language.GetString("ErrorRecord"));

						break;

					case "insert":
						if (!Facade.Service.Insert(service))
							throw new Exception(Language.GetString("ErrorRecord"));

						break;
				}

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Services_Service, Session)));
			}
			catch (Exception ex)
			{
				ltrResult.Text = string.Format("<div class='bg-danger div-save-result'><span class='fa fa-times fa-2x red'></span>{0}</div>", ex.Message);
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Services_Service, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			if (!Helper.GetBool(Session["IsSuperAdmin"]))
				permissions.Add((int)Arad.SMS.Gateway.Business.Services.SaveService);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Services_SaveService;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Services_SaveService.ToString());
		}
	}
}