using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.Users
{
	public partial class EditProfile : UIUserControlBase
	{
		protected string username = string.Empty;
		protected int userType;
		public bool EditUser
		{
			get { return Helper.RequestBool(this, "EditUser"); }
		}

		public bool IsReadOnly
		{
			get { return Helper.RequestBool(this, "ReadOnly"); }
		}

		protected Guid UserGuid
		{
			get { return EditUser ? Helper.RequestGuid(this, "UserGuid") : Helper.GetGuid(Session["UserGuid"]); }
		}

		protected bool IsAuthenticated
		{
			get { return Helper.GetBool(Session["IsAuthenticated"]); }
		}

		public EditProfile()
		{
			AddDataBinderHandlers("gridActualUserDocument", new DataBindHandler(gridActualUserDocument_OnDataBind));
			AddDataRenderHandlers("gridActualUserDocument", new CellValueRenderEventHandler(gridActualUserDocument_OnDataRender));
			AddDataBinderHandlers("gridLegalUserDocument", new DataBindHandler(gridLegalUserDocument_OnDataBind));
			AddDataRenderHandlers("gridLegalUserDocument", new CellValueRenderEventHandler(gridLegalUserDocument_OnDataRender));
		}

		public DataTable gridActualUserDocument_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			DataTable dtUserDocuments = Facade.UserDocument.GetUserDocuments(UserGuid);
			DataView dataViewDocument = dtUserDocuments.DefaultView;
			dataViewDocument.RowFilter = string.Format("Type='{0}'", (int)UserType.Actual);
			dtUserDocuments = dataViewDocument.ToTable();

			DataTable dtActualUserDocument = new DataTable();
			DataRow newRow;

			dtActualUserDocument.Columns.Add("Guid", typeof(Guid));
			dtActualUserDocument.Columns.Add("DocumentId", typeof(int));
			dtActualUserDocument.Columns.Add("Status", typeof(int));
			dtActualUserDocument.Columns.Add("Document", typeof(string));
			dtActualUserDocument.Columns.Add("Description", typeof(string));
			dtActualUserDocument.Columns.Add("Path", typeof(string));
			dtActualUserDocument.Columns.Add("File", typeof(string));

			foreach (DataRow row in dtUserDocuments.Rows)
			{
				newRow = dtActualUserDocument.NewRow();

				newRow["Guid"] = Helper.GetGuid(row["guid"]);
				newRow["DocumentId"] = Helper.GetInt(row["Key"]);
				newRow["Status"] = Helper.GetInt(row["Status"]);
				newRow["Description"] = Helper.GetString(row["Description"]);
				newRow["File"] = "-------";
				newRow["Path"] = Helper.GetString(row["Value"]);

				dtActualUserDocument.Rows.Add(newRow);
			}

			return dtActualUserDocument;
		}

		public string gridActualUserDocument_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					if (Helper.GetInt(e.CurrentRow["Status"]) != (int)UserDocumentStatus.Confirmed)
						return string.Format("<span class='ui-icon fa fa-trash-o red' title='{0}' onclick='deleteUserDocument(event);'></span>", Language.GetString("Delete"));
					else
						return string.Empty;
				case "Document":
					return string.Format("<a href='/userdocument/{0}' target='_blank'>{1}</a>",
															 e.CurrentRow["Path"],
															 Language.GetString(((UserDocumentType)Helper.GetInt(e.CurrentRow["DocumentId"])).ToString()));
				case "Status":
					switch (Helper.GetInt(e.CurrentRow["Status"]))
					{
						case (int)UserDocumentStatus.Confirmed:
							return string.Format("<span class='ui-icon fa fa-check green' title='{0}'></span>", Language.GetString("Confirmed"));
						case (int)UserDocumentStatus.Rejected:
							return string.Format("<span class='ui-icon fa fa-times red' title='{0}'></span>", Language.GetString("Rejected"));
						case (int)UserDocumentStatus.Checking:
							return string.Format("<img src='/pic/arrowsloader.gif' title='{0}'></span>", Language.GetString("Checking"));
						default:
							return string.Empty;
					}
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		public DataTable gridLegalUserDocument_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			DataTable dtUserDocuments = Facade.UserDocument.GetUserDocuments(UserGuid);
			DataView dataViewDocument = dtUserDocuments.DefaultView;
			dataViewDocument.RowFilter = string.Format("Type='{0}'", (int)UserType.Legal);
			dtUserDocuments = dataViewDocument.ToTable();

			DataTable dtLegalUserDocument = new DataTable();
			DataRow newRow;

			dtLegalUserDocument.Columns.Add("Guid", typeof(Guid));
			dtLegalUserDocument.Columns.Add("DocumentId", typeof(int));
			dtLegalUserDocument.Columns.Add("Status", typeof(int));
			dtLegalUserDocument.Columns.Add("Description", typeof(string));
			dtLegalUserDocument.Columns.Add("Document", typeof(string));
			dtLegalUserDocument.Columns.Add("Path", typeof(string));
			dtLegalUserDocument.Columns.Add("File", typeof(string));

			foreach (DataRow row in dtUserDocuments.Rows)
			{
				newRow = dtLegalUserDocument.NewRow();

				newRow["Guid"] = Helper.GetGuid(row["guid"]);
				newRow["DocumentId"] = Helper.GetInt(row["Key"]);
				newRow["Status"] = Helper.GetInt(row["Status"]);
				newRow["Description"] = Helper.GetString(row["Description"]);
				newRow["File"] = "-------";
				newRow["Path"] = Helper.GetString(row["Value"]);

				dtLegalUserDocument.Rows.Add(newRow);
			}

			return dtLegalUserDocument;
		}

		public string gridLegalUserDocument_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					if (Helper.GetInt(e.CurrentRow["Status"]) != (int)UserDocumentStatus.Confirmed)
						return string.Format("<span class='ui-icon fa fa-trash-o red' title='{0}' onclick='deleteCompanyDocument(event);'></span>", Language.GetString("Delete"));
					else
						return string.Empty;
				case "Document":
					return string.Format("<a href='/userdocument/{0}' target='_blank'>{1}</a>",
															 e.CurrentRow["Path"],
															 Language.GetString(((UserDocumentType)Helper.GetInt(e.CurrentRow["DocumentId"])).ToString()));
				case "Status":
					switch (Helper.GetInt(e.CurrentRow["Status"]))
					{
						case (int)UserDocumentStatus.Confirmed:
							return string.Format("<span class='ui-icon fa fa-check green' title='{0}'></span>", Language.GetString("Confirmed"));
						case (int)UserDocumentStatus.Rejected:
							return string.Format("<span class='ui-icon fa fa-times red' title='{0}'></span>", Language.GetString("Rejected"));
						case (int)UserDocumentStatus.Checking:
							return string.Format("<img src='/pic/arrowsloader.gif' title='{0}'></span>", Language.GetString("Checking"));
						default:
							return string.Empty;
					}
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
			try
			{
				btnSave.Visible = IsReadOnly ? false : true;
				btnCancel.Visible = EditUser ? true : false;

				drpPersonalDocumentType.Items.Add(new ListItem(Language.GetString(UserDocumentType.NationalCard.ToString()), ((int)UserDocumentType.NationalCard).ToString()));
				drpPersonalDocumentType.Items.Add(new ListItem(Language.GetString(UserDocumentType.BirthCertificate.ToString()), ((int)UserDocumentType.BirthCertificate).ToString()));
				drpPersonalDocumentType.Items.Add(new ListItem(Language.GetString(UserDocumentType.BusinessLicense.ToString()), ((int)UserDocumentType.BusinessLicense).ToString()));

				foreach (UserDocumentType doc in Enum.GetValues(typeof(UserDocumentType)))
				{
					if (doc != UserDocumentType.NationalCard &&
							doc != UserDocumentType.BirthCertificate &&
							doc != UserDocumentType.BusinessLicense)
						drpCompanyDocumentType.Items.Add(new ListItem(Language.GetString(doc.ToString()), ((int)doc).ToString()));
				}

				btnSave.Attributes["onclick"] = string.Format("return validateRequiredFields('{0}');", userType);
				btnSave.Text = Language.GetString(btnSave.Text);
				btnCancel.Text = Language.GetString(btnCancel.Text);

				#region LoadUser
				Common.User user = new Common.User();
				user = Facade.User.LoadUser(UserGuid);
				username = user.UserName;
				userType = user.Type;
				txtFirstName.Text = user.FirstName;
				txtLastName.Text = user.LastName;
				dtpBirthDate.Value = user.BirthDate != DateTime.MinValue ? DateManager.GetSolarDate(user.BirthDate) : string.Empty;
				txtEmail.Text = user.Email;
				txtMobile.Text = user.Mobile;
				txtPhone.Text = user.Phone;
				txtFax.Text = user.FaxNumber;
				txtAddress.Text = user.Address;
				txtFatherName.Text = user.FatherName;
				txtNationalCode.Text = user.NationalCode;
				txtShCode.Text = user.ShCode;
				txtZipCode.Text = user.ZipCode;
				txtCompanyName.Text = user.CompanyName;
				txtCompanyNationalID.Text = user.CompanyNationalId;
				txtEconomicCode.Text = user.EconomicCode;
				txtCompanyCEOName.Text = user.CompanyCEOName;
				txtCompanyCEONationalCode.Text = user.CompanyCEONationalCode;
				txtCompanyCEOMobile.Text = user.CompanyCEOMobile;
				txtCompanyPhone.Text = user.CompanyPhone;
				txtCompanyZipCode.Text = user.CompanyZipCode;
				txtCompanyAddress.Text = user.CompanyAddress;

				var lstUserZones = Facade.Zone.GetAllParents(user.ZoneGuid);

				#region Country
				var lstCountry = Facade.Zone.GetZones(Guid.Empty);
				drpCountry.DataSource = lstCountry;
				drpCountry.DataTextField = "Name";
				drpCountry.DataValueField = "Guid";
				drpCountry.DataBind();

				var country = lstUserZones.Rows.OfType<DataRow>().Where(r => r.Field<Guid>("ParentGuid") == Guid.Empty);
				if (country.Count() > 0)
					drpCountry.SelectedValue = country.First().Field<Guid>("Guid").ToString();
				#endregion

				#region Province
				Guid countryGuid = Helper.GetGuid(drpCountry.SelectedValue);
				drpProvince.DataSource = Facade.Zone.GetZones(countryGuid);
				drpProvince.DataTextField = "Name";
				drpProvince.DataValueField = "Guid";
				drpProvince.DataBind();

				var province = lstUserZones.Rows.OfType<DataRow>().Where(r => r.Field<Guid>("Guid") == user.ZoneGuid);
				if (province.Count() > 0)
					drpProvince.SelectedValue = lstUserZones.Rows.OfType<DataRow>().Where(r => r.Field<Guid>("Guid") == user.ZoneGuid).First().Field<Guid>("ParentGuid").ToString();
				else
					drpProvince.Items.Insert(0, new ListItem(string.Empty, string.Empty));
				#endregion

				#region City
				Guid provinceGuid = Helper.GetGuid(drpProvince.SelectedValue);
				if (provinceGuid != Guid.Empty)
				{
					drpCity.DataSource = Facade.Zone.GetZones(Helper.GetGuid(drpProvince.SelectedValue));
					drpCity.DataTextField = "Name";
					drpCity.DataValueField = "Guid";
					drpCity.DataBind();
					drpCity.SelectedValue = user.ZoneGuid.ToString();
				}
				#endregion

				pnlLegal.Visible = userType == (int)UserType.Legal ? true : false;
				#endregion
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("result('error','{0}');", ex.Message);
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				Common.User user = new Common.User();
				user.UserGuid = UserGuid;
				user.FirstName = txtFirstName.Text;
				user.LastName = txtLastName.Text;
				user.FatherName = txtFatherName.Text;
				user.NationalCode = txtNationalCode.Text;
				user.ShCode = txtShCode.Text;
				user.Email = txtEmail.Text;
				user.Phone = txtPhone.Text;
				user.Mobile = Helper.GetLocalMobileNumber(txtMobile.Text);
				user.Address = txtAddress.Text;
				user.BirthDate = DateManager.GetChristianDateForDB(dtpBirthDate.Value);
				user.FaxNumber = txtFax.Text;
				user.ZoneGuid = Helper.GetGuid(drpCity.SelectedValue);
				user.ZipCode = txtZipCode.Text;
				user.Type = userType;
				user.CompanyName = txtCompanyName.Text;
				user.CompanyNationalId = txtCompanyNationalID.Text;
				user.EconomicCode = txtEconomicCode.Text;
				user.CompanyCEOName = txtCompanyCEOName.Text;
				user.CompanyCEONationalCode = txtCompanyCEONationalCode.Text;
				user.CompanyCEOMobile = Helper.GetLocalMobileNumber(txtCompanyCEOMobile.Text);
				user.CompanyPhone = txtCompanyPhone.Text;
				user.CompanyZipCode = txtCompanyZipCode.Text;
				user.CompanyAddress = txtCompanyAddress.Text;

				//if (Helper.IsCellPhone(user.Mobile) != 1)
				//	throw new Exception(Language.GetString("InvalidMobile"));

				if (!Facade.User.UpdateProfile(user))
					throw new Exception(Language.GetString("ErrorRecord"));

				if (EditUser)
					Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_User, Session)));
				else
					ClientSideScript = string.Format("result('ok','{0}');", Language.GetString("InsertRecord"));
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("result('error','{0}');", ex.Message);
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			if (EditUser && !IsReadOnly)
				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_Users_User, Session)));
			else
				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_NumbersInfo, Session)));
		}

		protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
		{
			drpCity.Items.Clear();

			Guid countryGuid = Helper.GetGuid(drpCountry.SelectedValue);
			drpProvince.DataSource = Facade.Zone.GetZones(countryGuid);
			drpProvince.DataTextField = "Name";
			drpProvince.DataValueField = "Guid";
			drpProvince.DataBind();
			drpProvince.Items.Insert(0, new ListItem(string.Empty, string.Empty));
		}

		protected void drpProvince_SelectedIndexChanged(object sender, EventArgs e)
		{
			Guid provinceGuid = Helper.GetGuid(drpProvince.SelectedValue);
			if (provinceGuid != Guid.Empty)
			{
				drpCity.DataSource = Facade.Zone.GetZones(provinceGuid);
				drpCity.DataTextField = "Name";
				drpCity.DataValueField = "Guid";
				drpCity.DataBind();
				drpCity.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.EditProfile);
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ViewUserEditProfile);
			isOptionalPermissions = true;
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_Users_EditProfile;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_Users_EditProfile.ToString());
		}
	}
}
