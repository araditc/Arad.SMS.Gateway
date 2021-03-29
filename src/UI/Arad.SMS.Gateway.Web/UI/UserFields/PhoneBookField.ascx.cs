using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.UserFields
{
	public partial class PhoneBookField : UIUserControlBase
	{
		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid PhoneBookGuid
		{
			get { return Helper.GetGuid(Helper.Request(this, "Guid").Trim('\'')); }
		}

		public PhoneBookField()
		{
			AddDataBinderHandlers("gridPhoneBookFields", new DataBindHandler(gridPhoneBookFields_OnDataBind));
			AddDataRenderHandlers("gridPhoneBookFields", new CellValueRenderEventHandler(gridPhoneBookFields_OnDataRender));
		}

		public DataTable gridPhoneBookFields_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.UserField.GetPhoneBookField(PhoneBookGuid);
		}

		public string gridPhoneBookFields_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<span class='ui-icon fa fa-pencil-square-o blue' onclick=""editField(event);"" title=""{0}""></span>",
																Language.GetString("Edit")) +

								 string.Format(@"<span class='ui-icon fa fa-trash-o red' onClick=""deleteField(event);"" title=""{0}""></span>",
																Language.GetString("Delete"));
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
			gridPhoneBookFields.TopToolbarItems = string.Format("<input type=\"button\" value=\"{0}\" class=\"btn btn-success\" onclick=\"addNewField();\"/>", Language.GetString("New"));
		}

		private void InitializePage()
		{
			btnSave.Text = Language.GetString("Register");
			btnSave.Attributes["onclick"] = "return validateRequiredFields('SaveField');";
			#region Show UserFields IN DropDownList
			foreach (Business.UserFieldTypes userFieldTypes in System.Enum.GetValues(typeof(Business.UserFieldTypes)))
				drpFieldType.Items.Add(new ListItem(Language.GetString(userFieldTypes.ToString()), ((int)userFieldTypes).ToString()));
			#endregion
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				string errorMessage = string.Empty;
				string ActionType = hdnActionType.Value;
				Common.UserField userField = new Common.UserField();
				switch (ActionType.ToLower())
				{
					case "edit":
						int index = Helper.GetInt(hdnIndexField.Value);
						string fieldName = txtTitle.Text;
						if (Facade.UserField.UpdateField(index, fieldName, Helper.GetInt(drpFieldType.SelectedValue), PhoneBookGuid))
							ShowMessageBox(Language.GetString("InsertRecord"), string.Empty, "success");
						break;
					case "insert":
						if (Facade.UserField.InsertField(txtTitle.Text, Helper.GetInt(drpFieldType.SelectedValue), PhoneBookGuid))
							ShowMessageBox(Language.GetString("InsertRecord"), string.Empty, "success");
						break;
				}
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message, string.Empty, "danger");
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.DefinePhoneBookField);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_UserFields_PhoneBookField;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_UserFields_PhoneBookField.ToString());
		}
	}
}
