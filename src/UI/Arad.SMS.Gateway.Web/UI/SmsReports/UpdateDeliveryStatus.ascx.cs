using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Arad.SMS.Gateway.Web.UI.SmsReports
{
	public partial class UpdateDeliveryStatus : UIUserControlBase
	{
		private Guid OutboxGuid
		{
			get { return Helper.RequestEncryptedGuid(this, "ReferenceGuid"); }
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
			try
			{
				Dictionary<Common.DeliveryStatus, List<string>> messageStatus = new Dictionary<Common.DeliveryStatus, List<string>>();
				List<int> lstValidStatus = new List<int>() { 1, 2, 4, 10, 14 };

				string uploadTarget = Server.MapPath(string.Format("~/Uploads/{0}/", Helper.GetHostOfDomain(Request.Url.Host)));
				List<string> lstValidExtention = new List<string>();
				lstValidExtention.Add("xlsx");
				lstValidExtention.Add("xls");

				if (!Directory.Exists(uploadTarget))
					Directory.CreateDirectory(uploadTarget);

				if (!fileUpload.HasFile)
					throw new Exception((Language.GetString("ErrorSelectFile")));

				string fileExtention = Path.GetExtension(fileUpload.PostedFile.FileName).TrimStart('.');
				if (!lstValidExtention.Contains(fileExtention))
					throw new Exception((Language.GetString("InvalidFileExtension")));

				string file = Path.GetFileName(fileUpload.PostedFile.FileName);
				fileUpload.SaveAs(uploadTarget + file);

				bool firstRowHasColumnNames = true;
				DataTable dtb = new DataTable();

				switch (fileExtention.ToLower())
				{
					case "csv":
						dtb = ImportFile.ImportCSV(Server.MapPath(string.Format("/Uploads/{0}", file)), firstRowHasColumnNames);
						break;
					case "xls":
					case "xlsx":
						dtb = ImportFile.ImportExcel(Server.MapPath(string.Format("/Uploads/{0}", file)), firstRowHasColumnNames);
						break;
				}

				int status;
				string mobile;

				foreach (DataRow row in dtb.Rows)
				{
					mobile = Helper.GetLocalMobileNumber(Helper.GetString(row[0]));
					status = Helper.GetInt(row[1]);

					if (!lstValidStatus.Contains(status))
						continue;

					if (!messageStatus.ContainsKey((Common.DeliveryStatus)status))
						messageStatus.Add((Common.DeliveryStatus)status, new List<string>());

					if (!messageStatus[(Common.DeliveryStatus)status].Contains(mobile))
						messageStatus[(Common.DeliveryStatus)status].Add(mobile);
				}

				if (messageStatus.Count > 0)
					Facade.OutboxNumber.UpdateDeliveryStatus(OutboxGuid, messageStatus);

				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_UserOutbox, Session)));

			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_SmsReports_UserOutbox, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.UpdateDeliveryStatusManually);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)UserControls.UI_SmsReports_UpdateDeliveryStatus;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(UserControls.UI_SmsReports_UpdateDeliveryStatus.ToString());
		}
	}
}