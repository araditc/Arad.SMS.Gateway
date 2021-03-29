using System;
using System.Collections.Generic;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;
using GeneralTools.DataGrid;

namespace Arad.SMS.Gateway.Web.UI.Images
{
	public partial class Image : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid GalleryImageGuid
		{
			get { return Helper.RequestGuid(this, "GalleryGuid"); }
		}

		public Image()
		{
			AddDataBinderHandlers("gridImages", new DataBindHandler(gridImages_OnDataBind));
			AddDataRenderHandlers("gridImages", new CellValueRenderEventHandler(gridImages_OnDataRender));
		}

		public DataTable gridImages_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.Image.GetImagesOfGallery(GalleryImageGuid);
		}

		public string gridImages_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<span class='ui-icon fa fa-play-circle-o green' title=""{0}"" onClick=""activeImage(event);""></span>",
																Language.GetString("Active")) +

								 string.Format(@"<span class='ui-icon fa fa-picture-o red' title=""{0}"" onClick=""showImage(event);""></span>",
																	Language.GetString("ShowImage"))+
								
								 string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&Guid={1}&GalleryGuid={2}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{3}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Images_SaveImage, Session),
																e.CurrentRow["Guid"],
																GalleryImageGuid,
																Language.GetString("Edit")) +

								 string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteRow(event);""></span>",
																Language.GetString("Delete"));
				case "ImagePath":
					return Helper.GetString(e.CurrentRow[sender.FieldName]);
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
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
			gridImages.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&ActionType=insert&GalleryGuid={1}\">{2}</a>",
																									Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Images_SaveImage, Session),
																									GalleryImageGuid,
																									Language.GetString("New"));

			gridImages.TopToolbarItems += string.Format("<a class=\"btn btn-default\" href=\"/PageLoader.aspx?c={0}\">{1}</a>",
																									Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_GalleryImages_GalleryImage, Session),
																									Language.GetString("Cancel"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ImagesList);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Images_Image;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Images_Image.ToString());
		}
	}
}