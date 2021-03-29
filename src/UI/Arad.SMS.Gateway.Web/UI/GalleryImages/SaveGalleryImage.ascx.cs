using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;

namespace Arad.SMS.Gateway.Web.UI.GalleryImages
{
	public partial class SaveGalleryImage : UIUserControlBase
	{
		private Guid GalleryImageGuid
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
			btnSave.Attributes["onclick"] = "return validateRequiredFields();";
			btnSave.Text = Language.GetString(btnSave.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);

			if (ActionType == "edit")
			{
				Common.GalleryImage galleryImage = Facade.GalleryImage.LoadGalleryImage(GalleryImageGuid);
				txtTitle.Text = galleryImage.Title;
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.GalleryImage galleryImage = new Common.GalleryImage();
			try
			{
				galleryImage.UserGuid = UserGuid;
				galleryImage.Title = Helper.GetString(txtTitle.Text);
				galleryImage.CreateDate = DateTime.Now;

				if (galleryImage.HasError)
					throw new Exception(galleryImage.ErrorMessage);

				switch (ActionType)
				{
					case "insert":
						if (!Facade.GalleryImage.Insert(galleryImage))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
					case "edit":
						galleryImage.GalleryImageGuid = GalleryImageGuid;
						if (!Facade.GalleryImage.Update(galleryImage))
							throw new Exception(Language.GetString("ErrorRecord"));
						break;
				}

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_GalleryImages_GalleryImage, Session)));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_GalleryImages_GalleryImage, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.SaveGalleryImage);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_GalleryImages_SaveGalleryImage;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_GalleryImages_SaveGalleryImage.ToString());
		}
	}
}