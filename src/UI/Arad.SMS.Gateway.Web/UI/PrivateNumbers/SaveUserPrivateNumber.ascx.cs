using Arad.SMS.Gateway.Business;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace Arad.SMS.Gateway.Web.UI.PrivateNumbers
{
	public partial class SaveUserPrivateNumber : UIUserControlBase
	{
		private Guid UserPrivateNumberGuid
		{
			get { return Helper.RequestEncryptedGuid(this, "Guid"); }
		}

		private Guid ParentGuid
		{
			get { return Helper.RequestGuid(this, "ParentGuid"); }
		}

		private Guid UserGuid
		{
			get { return Helper.RequestGuid(this, "UserGuid"); }
		}

		private string Number
		{
			get { return Helper.Request(this, "Number"); }
		}

		private string ActionType
		{
			get { return Helper.Request(this, "ActionType"); }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				Initializepage();
		}

		private void Initializepage()
		{
			btnSave.Text = Language.GetString(btnSave.Text);
			int resultCount = 0;
			DataTable dtNumbers = Facade.PrivateNumber.GetUserNumbers(ParentGuid, string.Empty, 0, 0, "[CreateDate]", ref resultCount);
			dtNumbers.Columns.Add("TextField", typeof(string));
			dtNumbers.Columns.Add("ValueField", typeof(string));

			foreach (DataRow row in dtNumbers.Rows)
			{
				row["ValueField"] = string.Format("{0};{1}", row["UseForm"], row["Guid"]);
				switch (Helper.GetInt(row["UseForm"]))
				{
					case (int)PrivateNumberUseForm.OneNumber:
					case (int)PrivateNumberUseForm.Mask:
						row["TextField"] = row["Number"];
						break;
					case (int)PrivateNumberUseForm.RangeNumber:
						row["TextField"] = string.Format("{0}", row["Range"].ToString());
						break;
				}
			}

			drpNumber.DataSource = dtNumbers;
			drpNumber.DataValueField = "ValueField";
			drpNumber.DataTextField = "TextField";
			drpNumber.DataBind();
			drpNumber.Items.Insert(0, new ListItem(string.Empty, string.Empty));
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				Common.PrivateNumber savePrivateNumber = new Common.PrivateNumber();

				string selectedNumber = drpNumber.SelectedValue;
				int numberType = Helper.GetInt(selectedNumber.Split(';')[0]);
				Guid numberGuid = Helper.GetGuid(selectedNumber.Split(';')[1]);
				string keyword = txtKeyword.Text.Trim();
				decimal price = Helper.GetDecimal(txtPrice.Text);
                var dateTime = dtpExpireDate.FullDateTime;
                DateTime expireDate = new DateTime();
                if (Session["Language"].ToString() == "fa")
                {
                    expireDate = DateManager.GetChristianDateTimeForDB(dateTime);
                }
                else
                {
                    expireDate = DateTime.Parse(dateTime);
                }
                //DateTime expireDate = DateManager.GetChristianDateTimeForDB(dtpExpireDate.FullDateTime);

				Common.PrivateNumber privateNumber = Facade.PrivateNumber.LoadNumber(numberGuid);

				Dictionary<string, string> patterns = new Dictionary<string, string>();
				patterns.Add("multiple", @"\b[1-9][0-9]{3,11}\*$");
				patterns.Add("single", @"\b[1-9][0-9]{3,12}\?{1,10}$");

				switch (numberType)
				{
					case (int)PrivateNumberUseForm.OneNumber:
					case (int)PrivateNumberUseForm.Mask:
						if (!Facade.PrivateNumber.AssignNumberToUser(numberGuid, UserGuid, keyword, price, expireDate))
							throw new Exception("ErrorRecord");
						break;
					case (int)PrivateNumberUseForm.RangeNumber:
						bool rangeIsValid = false;
						int count;
						string sampleNumber = string.Empty;
						string regex = string.Empty;

						if (txtRange.Text == privateNumber.Number)
						{
							if (!Facade.PrivateNumber.AssignNumberToUser(numberGuid, UserGuid, keyword, price, expireDate))
								throw new Exception("ErrorRecord");
						}
						else if (txtRange.Text == privateNumber.Range)
						{
							if (!Facade.PrivateNumber.AssignRangeNumberToUser(numberGuid, UserGuid,price,expireDate))
								throw new Exception("ErrorRecord");
						}
						else
						{
							savePrivateNumber.Price = price;
							savePrivateNumber.ExpireDate = expireDate;
							if (Regex.IsMatch(txtRange.Text, patterns["multiple"]))
							{

								count = txtRange.Text.Substring(0, txtRange.Text.IndexOf('*')).ToCharArray().Length;
								for (int counter = 1; counter <= (14 - count); counter++)
									sampleNumber += "0";

								sampleNumber = txtRange.Text.Substring(0, txtRange.Text.IndexOf('*')) + sampleNumber;

								regex = @"(^|\s)" + txtRange.Text.Substring(0, txtRange.Text.IndexOf('*')) + "[0-9]{0," + (14 - count) + @"}(\s|$)";
								if (Regex.IsMatch(sampleNumber, privateNumber.Regex) && Regex.IsMatch(sampleNumber, regex))
								{
									rangeIsValid = true;
									savePrivateNumber.Number = string.Empty;
									savePrivateNumber.Range = txtRange.Text;
									savePrivateNumber.Regex = regex;
									savePrivateNumber.ParentGuid = numberGuid;
									savePrivateNumber.OwnerGuid = UserGuid;
									keyword = string.Empty;
								}
							}
							else if (Regex.IsMatch(txtRange.Text, patterns["single"]))
							{
								count = txtRange.Text.ToCharArray().Where(ch => ch == '?').Count();
								for (int counter = 1; counter <= count; counter++)
									sampleNumber += "0";

								sampleNumber = txtRange.Text.Substring(0, txtRange.Text.IndexOf('?')) + sampleNumber;

								regex = @"(^|\s)" + txtRange.Text.Substring(0, txtRange.Text.IndexOf('?')) + "[0-9]{" + count + @"}(\s|$)";
								if (Regex.IsMatch(sampleNumber, privateNumber.Regex) && Regex.IsMatch(sampleNumber, regex))
								{
									rangeIsValid = true;
									savePrivateNumber.Number = string.Empty;
									savePrivateNumber.Range = txtRange.Text;
									savePrivateNumber.Regex = regex;
									savePrivateNumber.ParentGuid = numberGuid;
									savePrivateNumber.OwnerGuid = UserGuid;
									keyword = string.Empty;
								}
							}
							else if (Regex.IsMatch(txtRange.Text, privateNumber.Regex))
							{
								sampleNumber = txtRange.Text;
								rangeIsValid = true;
								savePrivateNumber.Number = txtRange.Text;
								savePrivateNumber.Range = txtRange.Text;
								savePrivateNumber.Regex = regex = string.Format(@"(^|\s){0}(\s|$)", txtRange.Text);
								savePrivateNumber.ParentGuid = numberGuid;
								savePrivateNumber.OwnerGuid = UserGuid;
								keyword = txtKeyword.Text;
							}

							if (!rangeIsValid)
								throw new Exception(Language.GetString("InvalidRange"));

							if (!Facade.PrivateNumber.IsValidSubRange(sampleNumber, numberGuid, regex, keyword))
								throw new Exception(Language.GetString("DuplicateRange"));

							if (!Facade.PrivateNumber.AssignSubRangeNumberToUser(savePrivateNumber, keyword))
								throw new Exception("ErrorRecord");
						}
						break;
				}
				Response.Redirect(string.Format("/PageLoader.aspx?c={0}&UserGuid={1}", Helper.Encrypt((int)Arad.SMS.Gateway.Business.UserControls.UI_PrivateNumbers_AssignPrivateNumberToUsers, Session), UserGuid));
			}
			catch (Exception ex)
			{
				ClientSideScript = string.Format("saveFailed('{0}');", Language.GetString(ex.Message));
			}
		}

		protected override List<int> GetServicePermissions(ref bool isOptionalPermissions)
		{
			List<int> permissions = new List<int>();
			permissions.Add((int)Arad.SMS.Gateway.Business.Services.ManagePrivateNumber);
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
