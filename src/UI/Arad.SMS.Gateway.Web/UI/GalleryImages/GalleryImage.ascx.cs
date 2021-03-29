using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.GalleryImages
{
	public partial class GalleryImage : UIUserControlBase
	{
		public Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		public GalleryImage()
		{
			AddDataBinderHandlers("gridGalleryImages", new DataBindHandler(gridGalleryImages_OnDataBind));
			AddDataRenderHandlers("gridGalleryImages", new CellValueRenderEventHandler(gridGalleryImages_OnDataRender));
		}

		public DataTable gridGalleryImages_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.GalleryImage.GetAllGalleryImage(UserGuid);
		}

		public string gridGalleryImages_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<span class='ui-icon fa fa-play-circle-o green' title=""{0}"" onClick=""activeGalleryImage(event);""></span>",
																Language.GetString("Active")) +

								 string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&GalleryGuid={1}""><span class='ui-icon fa fa-file-image-o orange' title=""{2}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Images_Image, Session),
																e.CurrentRow["Guid"],
																Language.GetString("ManageImage")) +

								 string.Format(@"<a href=""/PageLoader.aspx?c={0}&ActionType=edit&Guid={1}""><span class='ui-icon fa fa-pencil-square-o blue' title=""{2}""></span></a>",
																Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_GalleryImages_SaveGalleryImage, Session),
																e.CurrentRow["Guid"],
																Language.GetString("Edit")) +

								 string.Format(@"<span class='ui-icon fa fa-trash-o red' title=""{0}"" onClick=""deleteRow(event);""></span>",
																Language.GetString("Delete"));
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			gridGalleryImages.TopToolbarItems = string.Format("<a class=\"btn btn-success\" href=\"/PageLoader.aspx?c={0}&ActionType=insert\">{1}</a>",
																												Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_GalleryImages_SaveGalleryImage, Session),
																												Language.GetString("New"));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.GalleryImageList);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_GalleryImages_GalleryImage;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_GalleryImages_GalleryImage.ToString());
		}
	}
}