using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.PhoneBooks
{
	public partial class SaveFileNumber : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid PhoneBookGuid
		{
			get { return Helper.RequestGuid(this, "PhoneBookGuid"); }
		}

		private int Type
		{
			get { return Helper.GetInt(Helper.Request(this, "Type")); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
			GenerateUserField();
		}

		private void InitializePage()
		{
			btnImportFile.Text = Language.GetString(btnImportFile.Text);
			btnSave.Text = Language.GetString(btnSave.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);

			//#region Show Conditions Save Number
			//foreach (Business.CheckNumberScope scope in System.Enum.GetValues(typeof(Business.CheckNumberScope)))
			//	drpCheckNumberScope.Items.Add(new ListItem(Language.GetString(scope.ToString()), ((int)scope).ToString()));
			//#endregion
		}

		private void GenerateUserField()
		{
			HtmlGenericControl lblFieldName;
			TextBox txtField;
			HtmlGenericControl divField;
			HtmlGenericControl divControl;
			StringBuilder fields = new StringBuilder();

			DataTable dataTableUserField = Facade.UserField.GetPhoneBookField(PhoneBookGuid);

			for (int counterField = 0; counterField < dataTableUserField.Rows.Count; counterField++)
			{
				lblFieldName = new HtmlGenericControl("label");
				txtField = new TextBox();

				divField = new HtmlGenericControl("div");
				divField.Attributes.Add("class", "form-group");

				divControl = new HtmlGenericControl("div");
				divControl.Attributes.Add("class", "col-sm-6");

				lblFieldName.ID = string.Format("lblField{0}", dataTableUserField.Rows[counterField]["FieldID"]);
				lblFieldName.Attributes.Add("class", "col-sm-4 control-label");
				lblFieldName.InnerText = dataTableUserField.Rows[counterField]["Title"].ToString();

				txtField.ID = string.Format("Field{0}", dataTableUserField.Rows[counterField]["FieldID"]);
				txtField.Attributes["fieldName"] = string.Format("Field{0}", dataTableUserField.Rows[counterField]["FieldID"]);
				txtField.Text = string.Empty;
				int type = Helper.GetInt(dataTableUserField.Rows[counterField]["Type"]);
				txtField.Attributes.Add("fieldType", type.ToString());

				switch (type)
				{
					case (int)UserFieldTypes.DateTime:
						txtField.CssClass = string.Format("form-control input-sm date");
						break;
					case (int)UserFieldTypes.Number:
						txtField.CssClass = string.Format("form-control input-sm numberInput");
						break;
					default:
						txtField.CssClass = string.Format("form-control input-sm");
						break;
				}

				divControl.Controls.Add(txtField);
				divField.Controls.Add(lblFieldName);
				divField.Controls.Add(divControl);

				pnlUserField.Controls.Add(divField);
			}
		}

		protected void btnImportFile_Click(object sender, EventArgs e)
		{
			try
			{
				string file = hdnFilePath.Value;

				if (!System.IO.File.Exists(Server.MapPath(string.Format("/Uploads/{0}", file))))
					throw new Exception(Language.GetString("FileDoesNotExist"));

				string extension = file.Substring(file.LastIndexOf('.')).TrimStart('.');
				bool firstRowHasColumnNames = chbHeaderRow.Checked;
				DataTable dtb = new DataTable();

				switch (extension.ToLower())
				{
					case "csv":
						dtb = ImportFile.ImportCSV(Server.MapPath(string.Format("/Uploads/{0}", file)), firstRowHasColumnNames, 5);
						break;
					case "xls":
					case "xlsx":
						dtb = ImportFile.ImportExcel(Server.MapPath(string.Format("/Uploads/{0}", file)), firstRowHasColumnNames, 5);
						break;
				}

				gridFile.DataSource = dtb;
				gridFile.DataBind();
			}
			catch (Exception ex)
			{
				ClientSideScript = "saveResult('Error','" + ex.Message + "');";
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.PhoneNumber phoneNumber;
			List<Common.PhoneNumber> lstNumbers = new List<Common.PhoneNumber>();
			string fieldId;
			string saveReport = string.Empty;

			try
			{
				string file = hdnFilePath.Value;

				if (!System.IO.File.Exists(Server.MapPath(string.Format("/Uploads/{0}", file))))
					throw new Exception(Language.GetString("FileDoesNotExist"));

				string extension = file.Substring(file.LastIndexOf('.')).TrimStart('.');
				bool firstRowHasColumnNames = chbHeaderRow.Checked;
				DataTable dtb = new DataTable();

				switch (extension.ToLower())
				{
					case "csv":
						dtb = ImportFile.ImportCSV(Server.MapPath(string.Format("/Uploads/{0}", file)), firstRowHasColumnNames);
						break;
					case "xls":
					case "xlsx":
						dtb = ImportFile.ImportExcel(Server.MapPath(string.Format("/Uploads/{0}", file)), firstRowHasColumnNames);
						break;
				}

				foreach (DataRow row in dtb.Rows)
				{
					phoneNumber = new Common.PhoneNumber();
					phoneNumber.PhoneNumberGuid = Guid.NewGuid();
					phoneNumber.PhoneBookGuid = PhoneBookGuid;
					phoneNumber.CreateDate = DateTime.Now;
					phoneNumber.FirstName = !string.IsNullOrEmpty(txtFirstName.Text) ? row[Helper.GetInt(txtFirstName.Text.Trim()) - 1].ToString() : string.Empty;
					phoneNumber.LastName = !string.IsNullOrEmpty(txtLastName.Text) ? row[Helper.GetInt(txtLastName.Text.Trim()) - 1].ToString() : string.Empty;

					if (!string.IsNullOrEmpty(txtBirthDate.Text))
						phoneNumber.BirthDate = DateManager.GetChristianDateForDB(row[Helper.GetInt(txtBirthDate.Text.Trim()) - 1].ToString());

					if (!string.IsNullOrEmpty(txtSex.Text))
						phoneNumber.Sex = GetSex(row[Helper.GetInt(txtSex.Text.Trim()) - 1].ToString());

					string mobile = !string.IsNullOrEmpty(txtCellPhone.Text) ? Helper.GetLocalMobileNumber(row[Helper.GetInt(txtCellPhone.Text.Trim()) - 1].ToString()) : string.Empty;
					string email = !string.IsNullOrEmpty(txtEmail.Text) ? row[Helper.GetInt(txtEmail.Text.Trim()) - 1].ToString() : string.Empty;

					if (!Helper.CheckDataConditions(email).IsEmail)
						email = string.Empty;

					//if (!Helper.CheckingCellPhone(ref mobile) && !Helper.CheckDataConditions(email).IsEmail)
					//	continue;

					phoneNumber.Email = email;
					phoneNumber.CellPhone = mobile;
					phoneNumber.Job = !string.IsNullOrEmpty(txtJob.Text) ? row[Helper.GetInt(txtJob.Text.Trim()) - 1].ToString() : string.Empty;
					phoneNumber.Telephone = !string.IsNullOrEmpty(txtTelephone.Text) ? row[Helper.GetInt(txtTelephone.Text.Trim()) - 1].ToString() : string.Empty;
					phoneNumber.FaxNumber = !string.IsNullOrEmpty(txtFaxNumber.Text) ? row[Helper.GetInt(txtFaxNumber.Text.Trim()) - 1].ToString() : string.Empty;
					phoneNumber.Address = !string.IsNullOrEmpty(txtAddress.Text) ? row[Helper.GetInt(txtAddress.Text.Trim()) - 1].ToString() : string.Empty;

					fieldId = string.Empty;
					for (int customFieldCounter = 1; customFieldCounter <= 20; customFieldCounter++)
					{
						fieldId = "Field" + customFieldCounter.ToString();
						TextBox customField = pnlUserField.FindControl(fieldId) as TextBox;
						if (customField != null && !string.IsNullOrEmpty(customField.Text))
							SetCustomFieldValue(phoneNumber,
																	customFieldCounter,
																	row[Helper.GetInt(customField.Text.Trim()) - 1].ToString(),
																	(UserFieldTypes)Helper.GetInt(customField.Attributes["FieldType"]));
					}

					lstNumbers.Add(phoneNumber);
				}

				if (!Facade.PhoneNumber.InsertBulkNumbers(lstNumbers, UserGuid))
					throw new Exception(Language.GetString("ErrorRecord"));

				ClientSideScript = "saveResult('OK','" + Language.GetString("InsertRecord") + "');";

				hdnFilePath.Value = string.Empty;
			}
			catch (Exception ex)
			{
				ClientSideScript = "saveResult('Error','" + ex.Message + "');";
			}
		}

		private void SetCustomFieldValue(Common.PhoneNumber phoneNumber, int customFieldId, string value, UserFieldTypes userFieldTypes)
		{
			try
			{
				DateTime date;
				switch (userFieldTypes)
				{
					case UserFieldTypes.Number:
						if (!Helper.CheckDataConditions(value).IsIntNumber)
							throw new Exception();
						break;
					case UserFieldTypes.DateTime:
						date = DateManager.GetChristianDateForDB(value);
						if (date != DateTime.MinValue)
							value = date.ToString();
						break;
				}

				switch (customFieldId)
				{
					case 1:
						phoneNumber.F1 = value;
						break;
					case 2:
						phoneNumber.F2 = value;
						break;
					case 3:
						phoneNumber.F3 = value;
						break;
					case 4:
						phoneNumber.F4 = value;
						break;
					case 5:
						phoneNumber.F5 = value;
						break;
					case 6:
						phoneNumber.F6 = value;
						break;
					case 7:
						phoneNumber.F7 = value;
						break;
					case 8:
						phoneNumber.F8 = value;
						break;
					case 9:
						phoneNumber.F9 = value;
						break;
					case 10:
						phoneNumber.F10 = value;
						break;
					case 11:
						phoneNumber.F11 = value;
						break;
					case 12:
						phoneNumber.F12 = value;
						break;
					case 13:
						phoneNumber.F13 = value;
						break;
					case 14:
						phoneNumber.F14 = value;
						break;
					case 15:
						phoneNumber.F15 = value;
						break;
					case 16:
						phoneNumber.F16 = value;
						break;
					case 17:
						phoneNumber.F17 = value;
						break;
					case 18:
						phoneNumber.F18 = value;
						break;
					case 19:
						phoneNumber.F19 = value;
						break;
					case 20:
						phoneNumber.F20 = value;
						break;
				}
			}
			catch
			{
				throw new Exception(Language.GetString("CustomFieldValueIsInValid"));
			}
		}

		public byte GetSex(string sex)
		{
			if (
				 sex.Trim().ToLower() == "man" ||
				 sex.Trim().ToLower() == "male" ||
				 sex.Trim().ToLower() == "boy" ||
				 sex.Trim().ToLower() == "he")
				return (int)Arad.SMS.Gateway.Business.Gender.Man;
			else if (
				 sex.Trim().ToLower() == "woman" ||
				 sex.Trim().ToLower() == "female" ||
				 sex.Trim().ToLower() == "girl" ||
				 sex.Trim().ToLower() == "she")
				return (int)Arad.SMS.Gateway.Business.Gender.Woman;
			else
				return 0;
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			if (IsModal)
				CloseModal("false");
			else
				Response.Redirect(string.Format("/PageLoader.aspx?c={0}&Guid={1}&Type={2}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_PhoneNumber, Session), PhoneBookGuid, Type));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			return new List<int>();
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_SaveFileNumber;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PhoneBooks_SaveFileNumber.ToString());
		}
	}
}