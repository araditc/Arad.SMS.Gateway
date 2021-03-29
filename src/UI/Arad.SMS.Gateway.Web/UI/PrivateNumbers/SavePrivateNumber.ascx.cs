using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.PrivateNumbers
{
	public partial class SavePrivateNumber : UIUserControlBase
	{
		private Guid ParentGuid
		{
			get { return Helper.GetGuid(Session["ParentGuid"]); }
		}

		private Guid UserGuid
		{
			get { return Helper.GetGuid(Session["UserGuid"]); }
		}

		private Guid PrivateNumberGuid
		{
			get { return Helper.RequestGuid(this, "Guid"); }
		}

		private bool IsMainAdmin
		{
			get { return Helper.GetBool(Session["IsMainAdmin"]); }
		}

		private string ActionType
		{
			get { return Helper.Request(this, "ActionType"); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			ClientSideScript = "changeUseForm();";

			if (!IsPostBack)
				InitializePage();
		}

		private void InitializePage()
		{
			if (!IsMainAdmin)
				return;

			drpSmsSenderAgent.DataSource = Facade.SmsSenderAgent.GetUserAgents(UserGuid);
			drpSmsSenderAgent.DataTextField = "Name";
			drpSmsSenderAgent.DataValueField = "Guid";
			drpSmsSenderAgent.DataBind();

			foreach (TypePrivateNumberAccesses type in Enum.GetValues(typeof(TypePrivateNumberAccesses)))
				drpType.Items.Add(new ListItem(type.ToString(), ((int)type).ToString()));

			foreach (SendPriority priority in Enum.GetValues(typeof(SendPriority)))
				drpPriority.Items.Add(new ListItem(priority.ToString(), ((int)priority).ToString()));
			drpPriority.SelectedValue = Helper.GetString((int)SendPriority.Normal);

			foreach (PrivateNumberUseForm useForm in Enum.GetValues(typeof(PrivateNumberUseForm)))
				drpUseForm.Items.Add(new ListItem(Language.GetString(useForm.ToString()), ((int)useForm).ToString()));

            if (Session["Language"].ToString() == "fa")
            {
                dtpExpireDate.Value = DateManager.GetSolarDate(DateTime.Now.AddYears(1));
            }
            else
            {
                dtpExpireDate.Value = DateTime.Now.AddYears(1).ToShortDateString();
            }

            //dtpExpireDate.Value = DateManager.GetSolarDate(DateTime.Now.AddYears(1));

			if (ActionType.ToLower() == "edit")
			{
				Common.PrivateNumber privateNumber = Facade.PrivateNumber.LoadNumber(PrivateNumberGuid);
				drpSmsSenderAgent.SelectedValue = Helper.GetString(privateNumber.SmsSenderAgentGuid);
				drpType.SelectedValue = Helper.GetString(privateNumber.Type);
				drpPriority.SelectedValue = privateNumber.Priority.ToString();
				chbIsRoot.Checked = privateNumber.IsRoot;
				chbIsActive.Checked = privateNumber.IsActive;
				chbReturnBlackList.Checked = privateNumber.ReturnBlackList;
				chbSendToBlackList.Checked = privateNumber.SendToBlackList;
				chbCheckFilter.Checked = privateNumber.CheckFilter;
				chbDeliveryBase.Checked = privateNumber.DeliveryBase;
				chbIsSla.Checked = privateNumber.HasSLA;
				drpTryCount.SelectedValue = privateNumber.TryCount.ToString();
				chbIsPublic.Checked = privateNumber.IsPublic;
				txtServiceID.Text = privateNumber.ServiceID;
				txtMTNServiceID.Text = privateNumber.MTNServiceId;
				txtAggServiceID.Text = privateNumber.AggServiceId;
				txtServicePrice.Text = Helper.FormatDecimalForDisplay(privateNumber.ServicePrice);
				dtpExpireDate.Value = privateNumber.ExpireDate != DateTime.MinValue ? DateManager.GetSolarDate(privateNumber.ExpireDate) : string.Empty;
				txtPrice.Text = Helper.FormatDecimalForDisplay(privateNumber.Price);

				switch (privateNumber.UseForm)
				{
					case (int)PrivateNumberUseForm.OneNumber:
						txtNumber.Text = privateNumber.Number;
						drpUseForm.SelectedValue = Helper.GetString((int)PrivateNumberUseForm.OneNumber);
						break;
					case (int)PrivateNumberUseForm.Mask:
						txtMask.Text = privateNumber.Number;
						drpUseForm.SelectedValue = Helper.GetString((int)PrivateNumberUseForm.Mask);
						break;
					case (int)PrivateNumberUseForm.RangeNumber:
						txtRange.Text = privateNumber.Range;
						drpUseForm.SelectedValue = Helper.GetString((int)PrivateNumberUseForm.RangeNumber);
						break;
				}
			}

			btnSave.Text = Language.GetString(btnSave.Text);
			btnCancel.Text = Language.GetString(btnCancel.Text);
			btnSave.Attributes["onclick"] = "return checkValidation();";
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Common.PrivateNumber privateNumber = new Common.PrivateNumber();
			try
			{
				Dictionary<string, string> patterns = new Dictionary<string, string>();
				patterns.Add("multiple", @"\b[1-9][0-9]{3,11}\*$");
				patterns.Add("single", @"\b[1-9][0-9]{3,12}\?{1,10}$");
				List<string> lstSampleNumbers = new List<string>();

				privateNumber.NumberGuid = PrivateNumberGuid;
				privateNumber.SmsSenderAgentGuid = Helper.GetGuid(drpSmsSenderAgent.SelectedValue);
				privateNumber.Type = Helper.GetInt(drpType.SelectedValue);
				privateNumber.Priority = Helper.GetInt(drpPriority.SelectedValue);
				privateNumber.IsRoot = chbIsRoot.Checked;
				privateNumber.IsActive = chbIsActive.Checked;
				privateNumber.ReturnBlackList = chbReturnBlackList.Checked;
				privateNumber.SendToBlackList = chbSendToBlackList.Checked;
				privateNumber.CheckFilter = chbCheckFilter.Checked;
				privateNumber.DeliveryBase = chbDeliveryBase.Checked;
				privateNumber.HasSLA = chbIsSla.Checked;
				privateNumber.TryCount = chbIsSla.Checked ? Helper.GetInt(drpTryCount.SelectedValue) : 0;
				privateNumber.IsPublic = chbIsPublic.Checked;
				privateNumber.CreateDate = DateTime.Now;
                var dateTime = dtpExpireDate.FullDateTime;
                if (Session["Language"].ToString() == "fa")
                {
                    privateNumber.ExpireDate = DateManager.GetChristianDateTimeForDB(dateTime);
                }
                else
                {
                    privateNumber.ExpireDate = DateTime.Parse(dateTime);
                }
                //privateNumber.ExpireDate = DateManager.GetChristianDateTimeForDB(dtpExpireDate.FullDateTime);
				privateNumber.UserGuid = UserGuid;
				privateNumber.ParentGuid = Guid.Empty;
				privateNumber.OwnerGuid = ParentGuid == Guid.Empty ? UserGuid : ParentGuid;
				privateNumber.Price = Helper.GetDecimal(txtPrice.Text);

				switch (Helper.GetInt(drpUseForm.SelectedValue))
				{
					case (int)PrivateNumberUseForm.OneNumber:
						if (Helper.GetLong(txtNumber.Text) == 0)
							throw new Exception("InvalidNumber");

						privateNumber.UseForm = (int)PrivateNumberUseForm.OneNumber;
						privateNumber.Number = Helper.GetLocalPrivateNumber(txtNumber.Text);
						privateNumber.Range = privateNumber.Number;
						privateNumber.Regex = string.Format(@"(^|\s){0}(\s|$)", privateNumber.Number);
						lstSampleNumbers.Add(privateNumber.Number);
						if (!Facade.PrivateNumber.IsValidRange(lstSampleNumbers, privateNumber.Regex, privateNumber.NumberGuid, UserGuid))
							throw new Exception(Language.GetString("DuplicateRange"));
						break;
					case (int)PrivateNumberUseForm.Mask:
						privateNumber.UseForm = (int)PrivateNumberUseForm.Mask;
						privateNumber.Number = txtMask.Text;
						privateNumber.Range = privateNumber.Number;
						privateNumber.Regex = string.Format(@"(^|\s){0}(\s|$)", privateNumber.Number);
						lstSampleNumbers.Add(privateNumber.Number);
						if (!Facade.PrivateNumber.IsValidRange(lstSampleNumbers, privateNumber.Regex, privateNumber.NumberGuid, UserGuid))
							throw new Exception(Language.GetString("DuplicateRange"));
						break;
					case (int)PrivateNumberUseForm.RangeNumber:
						privateNumber.UseForm = (int)PrivateNumberUseForm.RangeNumber;
						privateNumber.Range = txtRange.Text;
						bool rangeIsValid = false;
						int count;
						string sampleNumber = string.Empty;

						if (Regex.IsMatch(txtRange.Text, patterns["multiple"]))
						{
							count = txtRange.Text.Substring(0, txtRange.Text.IndexOf('*')).ToCharArray().Length;
							string number = txtRange.Text.Substring(0, txtRange.Text.IndexOf('*'));
							lstSampleNumbers.Add(number);
							for (int counter = 1; counter <= (14 - count); counter++)
							{
								sampleNumber += "0";
								lstSampleNumbers.Add(number + sampleNumber);
							}

							privateNumber.Regex = @"(^|\s)" + txtRange.Text.Substring(0, txtRange.Text.IndexOf('*')) + "[0-9]{0," + (14 - count) + @"}(\s|$)";
							rangeIsValid = true;
						}
						if (Regex.IsMatch(txtRange.Text, patterns["single"]))
						{
							count = txtRange.Text.ToCharArray().Where(ch => ch == '?').Count();
							string number = txtRange.Text.Substring(0, txtRange.Text.IndexOf('?'));
							lstSampleNumbers.Add(number);
							for (int counter = 1; counter <= count; counter++)
							{
								sampleNumber += "0";
								lstSampleNumbers.Add(number + sampleNumber);
							}

							privateNumber.Regex = @"(^|\s)" + txtRange.Text.Substring(0, txtRange.Text.IndexOf('?')) + "[0-9]{" + count + @"}(\s|$)";
							rangeIsValid = true;
						}

						if (!rangeIsValid)
							throw new Exception(Language.GetString("InvalidRange"));

						if (!Facade.PrivateNumber.IsValidRange(lstSampleNumbers, privateNumber.Regex, privateNumber.NumberGuid, UserGuid))
							throw new Exception(Language.GetString("DuplicateRange"));

						break;
				}

				switch (privateNumber.Type)
				{
					case (int)TypePrivateNumberAccesses.Bulk:
						privateNumber.ServiceID = string.Empty;
						privateNumber.MTNServiceId = string.Empty;
						privateNumber.AggServiceId = string.Empty;
						privateNumber.ServicePrice = 0;
						break;
					default:
						privateNumber.ServiceID = txtServiceID.Text;
						privateNumber.MTNServiceId = txtMTNServiceID.Text;
						privateNumber.AggServiceId = txtAggServiceID.Text;
						privateNumber.ServicePrice = Helper.GetDecimal(txtServicePrice.Text);
						break;
				}

				if (privateNumber.HasError)
					throw new Exception(privateNumber.ErrorMessage);

				switch (ActionType.ToLower())
				{
					case "edit":
						if (!Facade.PrivateNumber.UpdateNumber(privateNumber))
							throw new Exception("ErrorRecord");

						break;
					case "insert":
						if (!Facade.PrivateNumber.Insert(privateNumber))
							throw new Exception("ErrorRecord");

						break;
				}
				Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)UserControls.UI_PrivateNumbers_DefiningPrivateNumber, Session)));
			}
			catch (Exception ex)
			{
				ShowMessageBox(Language.GetString(ex.Message), string.Empty, "danger");
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("/PageLoader.aspx?c={0}", Helper.Encrypt((int)UserControls.UI_PrivateNumbers_DefiningPrivateNumber, Session)));
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.DefinePrivateNumber);
			return permissions;
		}

		protected override int GetUserControlID()
		{
			throw new NotImplementedException();
		}

		protected override string GetUserControlTitle()
		{
			throw new NotImplementedException();
		}
	}
}
