using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Arad.SMS.Gateway.Web.UI.RegularContents
{
	public partial class SaveContent : UIUserControlBase
	{
		private Guid RegularContentGuid
		{
			get { return Helper.RequestGuid(this, "RegularContentGuid"); }
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
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			DataTable dtContents = new DataTable();
			dtContents.Columns.Add("Text", typeof(string));

			try
			{
				string uploadTarget = Server.MapPath(string.Format("~/RegularContents/"));
				List<string> lstValidExtention = new List<string>() { ".xls", ".xlsx", ".csv" };
				if (!Directory.Exists(uploadTarget))
					Directory.CreateDirectory(uploadTarget);

				if (!fileUpload.HasFile)
					throw new Exception(Language.GetString("ErrorSelectFile"));

				string fileExtention = Path.GetExtension(fileUpload.PostedFile.FileName).ToLower();
				if (!lstValidExtention.Contains(fileExtention))
					throw new Exception((Language.GetString("InvalidFileExtension")));

				string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
				fileUpload.SaveAs(uploadTarget + fileName);

				bool firstRowHasColumnNames = true;
				List<DataRow> lstContents = new List<DataRow>();

				switch (fileExtention.TrimStart('.').ToLower())
				{
					case "csv":
						lstContents = ImportFile.ImportCSV(Server.MapPath(string.Format("/RegularContents/{0}", fileName)), firstRowHasColumnNames).AsEnumerable().ToList();
						break;
					case "xls":
					case "xlsx":
						lstContents = ImportFile.ImportExcel(Server.MapPath(string.Format("/RegularContents/{0}", fileName)), firstRowHasColumnNames).AsEnumerable().ToList();
						break;
				}

				lstContents.RemoveAll(cnt => string.IsNullOrEmpty(cnt[0].ToString()));

				foreach (DataRow row in lstContents)
					dtContents.Rows.Add(row[0].ToString());

				if (!Facade.Content.InsertContents(RegularContentGuid, dtContents))
					throw new Exception(Language.GetString("ErrorRecord"));

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}&RegularContentGuid={1}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_RegularContents_Content, Session), RegularContentGuid));
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}&RegularContentGuid={1}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_RegularContents_Content, Session), RegularContentGuid));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.RegularContent);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_RegularContents_SaveContent;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_RegularContents_SaveContent.ToString());
		}
	}
}