using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.Images
{
	public partial class SaveImage : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid ImageGuid
		{
			get { return Helper.RequestGuid(this, "Guid"); }
		}

		private Guid GalleryImageGuid
		{
			get { return Helper.RequestGuid(this, "GalleryGuid"); }
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
			btnSave.Attributes["onclick"] = "return validateRequiredFields();";
			btnSave.Text = Language.GetString(btnSave.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);

			drpContent.DataSource = Facade.Data.GetUserData(UserGuid);
			drpContent.DataTextField = "Title";
			drpContent.DataValueField = "Guid";
			drpContent.DataBind();
			drpContent.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			if (ActionType == "edit")
			{
				Common.Image image = Facade.Image.LoadImage(ImageGuid);
				txtTitle.Text = image.Title;
				txtDescription.Text = image.Description;
				hdnImagePath.Value = image.ImagePath;
				drpContent.SelectedValue = image.DataGuid.ToString();
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.Image image = new Common.Image();

			try
            {
                var domain = Helper.GetHostOfDomain(Request.Url.Authority);

                string uploadTarget = Server.MapPath(string.Format("~/Images/GalleryImage/{0}/", domain));
				List<string> lstValidExtention = new List<string>();
				lstValidExtention.Add(".jpg");
				lstValidExtention.Add(".jpeg");
				lstValidExtention.Add(".png");
				lstValidExtention.Add(".gif");
				lstValidExtention.Add(".bmp");


				image.GalleryImageGuid = GalleryImageGuid;
				image.DataGuid = Helper.GetGuid(drpContent.SelectedValue);
				image.Title = txtTitle.Text;
				image.Description = txtDescription.Text;
				image.CreateDate = DateTime.Now;
				image.ImagePath = hdnImagePath.Value;
				image.IsActive = true;

				if (!Directory.Exists(uploadTarget))
					Directory.CreateDirectory(uploadTarget);

				if (uploadImage.HasFile)
				{
					string fileExtention = Path.GetExtension(uploadImage.PostedFile.FileName).ToLower();
					if (!lstValidExtention.Contains(fileExtention))
						throw new Exception((Language.GetString("InvalidFileExtension")));

					string pic = uploadImage.PostedFile.FileName;
					uploadImage.SaveAs(uploadTarget + pic);
					image.ImagePath = pic;
				}

				if (image.HasError)
					throw new Exception(image.ErrorMessage);

				switch (ActionType)
				{
					case "insert":
						if (!Facade.Image.Insert(image))
							throw new Exception(Language.GetString("ErrorRecord"));

						break;
					case "edit":
						image.ImageGuid = ImageGuid;
						if (!Facade.Image.Update(image))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
				}

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}&GalleryGuid={1}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Images_Image, Session), GalleryImageGuid));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}&GalleryGuid={1}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Images_Image, Session), GalleryImageGuid));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SaveImage);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Images_SaveImage;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Images_SaveImage.ToString());
		}

	}
}
