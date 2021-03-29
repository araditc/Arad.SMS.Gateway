using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using GeneralTools.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;

namespace Arad.SMS.Gateway.Web.UI.UserFields
{
	public partial class SmsFormat : UIUserControlBase
	{
		protected Guid PhoneBookGuid
		{
			get { return Helper.GetGuid(Helper.Request(this, "Guid").Trim('\'')); }
		}

		public SmsFormat()
		{
			AddDataBinderHandlers("gridFormats", new DataBindHandler(gridFormats_OnDataBind));
			AddDataRenderHandlers("gridFormats", new CellValueRenderEventHandler(gridFormats_OnDataRender));
		}

		public DataTable gridFormats_OnDataBind(string sortField, string searchFiletrs, string toolbarFilters, string userData, int pageNo, int pageSize, ref int resultCount, ref string customData)
		{
			return Facade.SmsFormat.GetFormatOfPhoneBook(PhoneBookGuid);
		}

		public string gridFormats_OnDataRender(DataGridColumnInfo sender, CellValueRenderEventArgs e)
		{
			switch (sender.FieldName)
			{
				case "Action":
					return string.Format(@"<span class='ui-icon fa fa-pencil-square-o blue' onclick=""editFormat(event);"" title=""{0}""></span>",
																Language.GetString("Edit")) +

								 string.Format(@"<span class='ui-icon fa fa-trash-o red' onClick=""deleteFormat(event);"" title=""{0}""></span>",
																Language.GetString("Delete"));
			}
			return Helper.GetString(e.CurrentRow[sender.FieldName]);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				InitializePage();
			gridFormats.TopToolbarItems = string.Format("<a  class=\"btn btn-success\" onclick=\"addNewFormat();\">{0}</a>", Language.GetString("New"));
		}

		private void InitializePage()
		{
			string field = string.Empty;
			field += "<li id='Li-10' field='FIRSTNAME' draftText='[نام]' class='Field'>نام</li>";
			field += "<li id='Li-9' field='LASTNAME' draftText='[نام خانوادگی]' class='Field'>نام خانوادگی</li>";
			field += "<li id='Li-9' field='NATIONALCODE' draftText='[کدملی]' class='Field'>کد ملی</li>";
			field += "<li id='Li-8' field='BIRTHDATE' draftText='[تاریخ تولد ]' class='Field'>تاریخ تولد</li>";
			field += "<li id='Li-7' field='SEX' draftText='[ آقای / خانم ]' class='Field'>جنسیت</li>";
			field += "<li id='Li-6' field='CELLPHONE' draftText='[تلفن همراه ]' class='Field'>تلفن همراه</li>";
			field += "<li id='Li-5' field='EMAIL' draftText='[ایمیل ]' class='Field'>ایمیل</li>";
			field += "<li id='Li-4' field='JOB' draftText='[شغل ]' class='Field'>شغل</li>";
			field += "<li id='Li-3' field='TELEPHONE' draftText='[تلفن ]' class='Field'>تلفن</li>";
			field += "<li id='Li-2' field='FAXNUMBER' draftText='[فاکس ]' class='Field'>فاکس</li>";
			field += "<li id='Li-1' field='ADDRESS' draftText='[آدرس ]' class='Field'>آدرس</li>";
			field += "<hr/>";
			DataTable dataTableField = Facade.UserField.GetPhoneBookField(PhoneBookGuid);
			for (int counterPhoneBookField = 0; counterPhoneBookField < dataTableField.Rows.Count; counterPhoneBookField++)
			{
				field += "<li id='Li" + counterPhoneBookField + "' field='FIELD" + dataTableField.Rows[counterPhoneBookField]["FieldID"] + "@$!$@" + dataTableField.Rows[counterPhoneBookField]["Title"] + "' draftText='[" + dataTableField.Rows[counterPhoneBookField]["Title"] + "]' class='Field'>" + dataTableField.Rows[counterPhoneBookField]["Title"] + "</li>";
			}
			literalField.Text = field;
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.AddSmsFormat);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			return (int)Arad.SMS.Gateway.Business.UserControls.UI_UserFields_SmsFormat;
		}

		protected override string GetUserControlTitle()
		{
			return Language.GetString(Business.UserControls.UI_UserFields_SmsFormat.ToString());
		}
	}
}