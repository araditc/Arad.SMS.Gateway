using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.PhoneBooks
{
	public partial class SaveSingleNumber : UIUserControlBase
	{
		protected Guid PhoneNumberGuid
		{
			get { return Helper.RequestGuid(this, "PhoneNumberGuid"); }
		}

		protected Guid PhoneBookGuid
		{
			get { return Helper.RequestGuid(this, "PhoneBookGuid"); }
		}

		protected string ActionType
		{
			get { return Helper.Request(this, "ActionType").ToLower(); }
		}

		protected int Type
		{
			get { return Helper.GetInt(Helper.Request(this, "Type")); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			btnCancel.Text = Language.GetString("Cancel");

			foreach (Business.Gender sex in Enum.GetValues(typeof(Business.Gender)))
				drpSex.Items.Add(new ListItem(Language.GetString(sex.ToString()), ((int)sex).ToString()));
			drpSex.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			foreach (Business.CheckNumberScope scope in Enum.GetValues(typeof(Business.CheckNumberScope)))
				drpCheckNumberScope.Items.Add(new ListItem(Language.GetString(scope.ToString()), ((int)scope).ToString()));

			if (ActionType == "edit")
			{
				string customField = string.Empty;
				Common.PhoneNumber phoneNumber = Facade.PhoneNumber.LoadNumber(PhoneNumberGuid);

				txtFirstName.Text = phoneNumber.FirstName;
				txtLastName.Text = phoneNumber.LastName;
				txtNationalCode.Text = phoneNumber.NationalCode;
				if (phoneNumber.BirthDate != DateTime.MinValue)
					dtpBirthDate.Value = DateManager.GetSolarDate(phoneNumber.BirthDate);
				txtTelephone.Text = phoneNumber.Telephone;
				txtCellPhone.Text = phoneNumber.CellPhone;
				txtFaxNumber.Text = phoneNumber.FaxNumber;
				txtJob.Text = phoneNumber.Job;
				txtAddress.Text = phoneNumber.Address;
				txtEmail.Text = phoneNumber.Email;
				drpSex.SelectedValue = Helper.GetString(phoneNumber.Sex);

				if (!Helper.CheckDataConditions(phoneNumber.F1).IsEmpty)
					customField += "Field1{(" + phoneNumber.F1 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F2).IsEmpty)
					customField += "Field2{(" + phoneNumber.F2 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F3).IsEmpty)
					customField += "Field3{(" + phoneNumber.F3 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F4).IsEmpty)
					customField += "Field4{(" + phoneNumber.F4 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F5).IsEmpty)
					customField += "Field5{(" + phoneNumber.F5 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F6).IsEmpty)
					customField += "Field6{(" + phoneNumber.F6 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F7).IsEmpty)
					customField += "Field7{(" + phoneNumber.F7 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F8).IsEmpty)
					customField += "Field8{(" + phoneNumber.F8 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F9).IsEmpty)
					customField += "Field9{(" + phoneNumber.F9 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F10).IsEmpty)
					customField += "Field10{(" + phoneNumber.F10 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F11).IsEmpty)
					customField += "Field11{(" + phoneNumber.F11 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F12).IsEmpty)
					customField += "Field12{(" + phoneNumber.F12 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F13).IsEmpty)
					customField += "Field13{(" + phoneNumber.F13 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F14).IsEmpty)
					customField += "Field14{(" + phoneNumber.F14 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F15).IsEmpty)
					customField += "Field15{(" + phoneNumber.F15 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F16).IsEmpty)
					customField += "Field16{(" + phoneNumber.F16 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F17).IsEmpty)
					customField += "Field17{(" + phoneNumber.F17 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F18).IsEmpty)
					customField += "Field18{(" + phoneNumber.F18 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F19).IsEmpty)
					customField += "Field19{(" + phoneNumber.F19 + ")}";
				if (!Helper.CheckDataConditions(phoneNumber.F20).IsEmpty)
					customField += "Field20{(" + phoneNumber.F20 + ")}";

				ClientSideScript = "customField('" + GenerateCustomField(customField) + "')";
			}
			else
				ClientSideScript = "customField('" + GenerateCustomField(string.Empty) + "')";
		}

		private string GenerateCustomField(string value)
		{
			DataTable dataTableUserField = Facade.UserField.GetPhoneBookField(PhoneBookGuid);
			string fields = string.Empty;

			foreach (DataRow row in dataTableUserField.Rows)
			{
				fields += "Field" + row["FieldID"] + "_value{(" + Helper.ImportData(value, "Field" + row["FieldID"]) + ")}";
				fields += "Field" + row["FieldID"] + "_type{(" + row["Type"] + ")}";
				fields += "Field" + row["FieldID"] + "_title{(" + row["Title"] + ")}";
			}

			return fields;
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}&Guid={1}&Type={2}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_PhoneNumber, Session), PhoneBookGuid, Type));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.AddSingleNumber);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_PhoneBooks_SaveSingleNumber;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_PhoneBooks_SaveSingleNumber.ToString());
		}
	}
}