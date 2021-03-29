using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.Domains
{
	public partial class DomainSetting : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
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
			try
			{
				Guid domainGuid = Facade.Domain.GetUserDomain(UserGuid);
				if (domainGuid == Guid.Empty)
					throw new Exception(Language.GetString("UserDoNotHaveDomain"));

				btnSave.Text = Language.GetString(btnSave.Text);

				drpGallery.DataSource = Facade.GalleryImage.GetAllGalleryImage(UserGuid);
				drpGallery.DataTextField = "Title";
				drpGallery.DataValueField = "Guid";
				drpGallery.DataBind();
				drpGallery.Items.Insert(0, new ListItem(string.Empty, string.Empty));

				DataTable dataTableDomainSettings = Facade.DomainSetting.GetDomainSettings(domainGuid);
				foreach (DataRow row in dataTableDomainSettings.Rows)
				{
					string value = row["Value"].ToString();
					switch (Helper.GetInt(row["Key"]))
					{
						case (int)SiteSetting.CompanyName:
							txtCompanyName.Text = value;
							break;
						case (int)SiteSetting.Title:
							txtTitle.Text = value;
							break;
						case (int)SiteSetting.Footer:
							txtFooter.Text = value;
							break;
						case (int)SiteSetting.SlideShow:
							drpGallery.SelectedValue = value;
							break;
						case (int)SiteSetting.Description:
							txtDescription.Text = value;
							break;
						case (int)SiteSetting.Keywords:
							txtKeywords.Text = value;
							break;
					}
				}
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				Guid domainGuid = Facade.Domain.GetUserDomain(UserGuid);
				if (domainGuid == Guid.Empty)
					throw new Exception(Language.GetString("UserDoNotHaveDomain"));

				string uploadTarget = Server.MapPath(string.Format("~/Images/{0}/", Helper.GetHostOfDomain(Request.Url.Host)));
				List<string> lstValidExtention = new List<string>();
				lstValidExtention.Add(".jpg");
				lstValidExtention.Add(".jpeg");
				lstValidExtention.Add(".png");
				lstValidExtention.Add(".gif");
				lstValidExtention.Add(".bmp");

				if (!Directory.Exists(uploadTarget))
					Directory.CreateDirectory(uploadTarget);

				Dictionary<int, string> dictionarySetting = new Dictionary<int, string>();
				DataTable dtSettings = new DataTable();
				DataRow row;
				dtSettings.Columns.Add("Key", typeof(int));
				dtSettings.Columns.Add("Value", typeof(string));

				if (fileUploadLogo.HasFile)
				{
					string fileExtention = Path.GetExtension(fileUploadLogo.PostedFile.FileName).ToLower();
					if (!lstValidExtention.Contains(fileExtention))
						throw new Exception((Language.GetString("InvalidFileExtension")));

					string pic = Path.GetFileName(fileUploadLogo.PostedFile.FileName);
					fileUploadLogo.SaveAs(uploadTarget + pic);
					dictionarySetting.Add((int)SiteSetting.Logo, pic);
				}

				if (fileUploadFavicon.HasFile)
				{
					string fileExtention = Path.GetExtension(fileUploadFavicon.PostedFile.FileName).ToLower();
					if (!lstValidExtention.Contains(fileExtention))
						throw new Exception((Language.GetString("InvalidFileExtension")));

					string pic = Path.GetFileName(fileUploadFavicon.PostedFile.FileName);
					fileUploadFavicon.SaveAs(uploadTarget + pic);
					dictionarySetting.Add((int)SiteSetting.Favicon, pic);
				}

				dictionarySetting.Add((int)SiteSetting.CompanyName, txtCompanyName.Text);
				dictionarySetting.Add((int)SiteSetting.Title, txtTitle.Text);
				dictionarySetting.Add((int)SiteSetting.Footer, txtFooter.Text);
				dictionarySetting.Add((int)SiteSetting.SlideShow, drpGallery.SelectedValue.ToString());
				dictionarySetting.Add((int)SiteSetting.Description, txtDescription.Text);
				dictionarySetting.Add((int)SiteSetting.Keywords, txtKeywords.Text);

				foreach (KeyValuePair<int, string> item in dictionarySetting)
				{
					row = dtSettings.NewRow();

					row["Key"] = item.Key;
					row["Value"] = item.Value;
					dtSettings.Rows.Add(row);
				}

				if (!Facade.DomainSetting.InsertDomainSetting(domainGuid, dtSettings))
					throw new Exception(Language.GetString("ErrorRecord"));

				ShowMessageBox(Language.GetString("InsertRecord"), string.Empty, "success");
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.DomainSetting);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Domains_DomainSetting;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Domains_DomainSetting.ToString());
		}
	}
}